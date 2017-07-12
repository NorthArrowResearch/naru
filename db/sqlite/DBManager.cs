using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace naru.db.sqlite
{
    public abstract class DBManager
    {
        public System.IO.FileInfo FilePath { get; internal set; }
        public string SQLVersion { get; internal set; }
        public int MinimumSupportedVersion { get; internal set; }

        private string DatabaseStructureSQLFileName { get; set; }
        private string DatabaseContentsWildcardPattern { get; set; }

        public override string ToString()
        {
            return ConnectionString;
        }

        public string ConnectionString
        {
            get
            {
                return BuildConnectionString(FilePath.FullName);
            }
        }

        /// <summary>
        /// The full absolute path to the file containing the SQL that creates all the tables and views
        /// </summary>
        /// <remarks>This is the executable folder with the Database structure file name appended. If the
        /// file name has a folder as part of it (.e.g. "Database\myfile.sql") then it is included in this path</remarks>
        public string DatabaseStructureFilePath
        {
            get
            {
                string sPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), DatabaseStructureSQLFileName);
                if (!System.IO.File.Exists(sPath))
                    throw new Exception("The database structure file does not exist.");
                return sPath;
            }
        }

        /// <summary>
        /// Return a connection string from a file path
        /// </summary>
        /// <param name="sFilePath">Full, absolute file path to a SQLite database</param>
        /// <returns>SQLite connection string</returns>
        /// <remarks>Static method, so can be used without instantiating the class</remarks>
        private static string BuildConnectionString(string sFilePath)
        {
            return string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;foreign keys=true;", sFilePath);
        }

        /// <summary>
        /// Pure virtual Constructor. Must be inherited.
        /// </summary>
        /// <param name="sFilePath">Full absolute file path to a SQLite database</param>
        /// <param name="sqlVersionQuery">SQL command that is used to determine the version of the database</param>
        /// <param name="nMinSupporterVersion">Minimum version of the database supported for upgrade</param>
        /// <param name="sDBStructureSQL">Relative path from the software executable where the database structure SQL file is stored (e.g. "Database\myfile.sql")</param>
        /// <param name="sDBDataWildcard">Wildcard used to find lookup data SQL files in the same folder as the structure SQL file</param>
        /// <remarks>This constructor is also used during the creation of new databases.
        /// So do not check for the existance of the file on disck because it might not be present yet.
        /// 
        /// Each of the data SQL files must have the corresponding database table name after this DataWildcard.
        /// For example if the wildcard is "_data_" an example file name might be mytool_data_MyTable.sql</remarks>
        public DBManager(string sFilePath, string sqlVersionQuery, int nMinSupporterVersion
            , string sDBStructureSQL, string sDBDataWildcard)
        {
            if (string.IsNullOrEmpty(sFilePath))
                throw new Exception("Empty database file path");

            SQLVersion = sqlVersionQuery;
            FilePath = new System.IO.FileInfo(sFilePath);
            DatabaseStructureSQLFileName = sDBStructureSQL;
            DatabaseContentsWildcardPattern = sDBDataWildcard;
        }

        public int GetDBVersion()
        {
            int nVersion = 0;
            using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand(SQLVersion, dbCon);
                object objVersion = dbCom.ExecuteScalar();
                if (objVersion == null || objVersion == DBNull.Value)
                    throw new Exception("Failed to retrieve database version");

                if (!int.TryParse(objVersion.ToString(), out nVersion))
                    throw new Exception("Failed to convert database version to integer value");
            }
            return nVersion;
        }

        public bool RequiresUpdrade(int nRequiredVersion)
        {
            int nCurrentVersion = GetDBVersion();

            if (nCurrentVersion > nRequiredVersion)
            {
                Exception ex = new Exception("The current database version is greater than the required verison");
                ex.Data["Current Version"] = nCurrentVersion;
                ex.Data["Required Version"] = nRequiredVersion;
                throw ex;
            }

            return nRequiredVersion > nCurrentVersion;
        }

        /// <summary>
        /// Creates a new database file, complete with tables, views and lookup data
        /// </summary>
        public void CreateDatabase()
        {
            string sqlStructureSQL = string.Empty;
            List<string> sqlContentsSQL = null;

            try
            {
                // Load the structure and data SQL first so any problems occur before DB created on disk
                LoadDBStructureStatements(out sqlStructureSQL);
                LoadDataStatements(out sqlContentsSQL);

                // Create the empty SQLite database file
                CreateEmptyDatabase();

                // Build the DB structure and populate lookup tables
                PopulateEmptyDatabase(sqlStructureSQL, sqlContentsSQL);
            }
            catch (Exception ex)
            {
                ex.Data["Database File Path"] = FilePath.FullName;
                ex.Data["Database Structure File Path"] = DatabaseStructureFilePath;
                ex.Data["Lookup Data Wildard"] = DatabaseContentsWildcardPattern;
                throw;
            }
        }

        /// <summary>
        /// Open the text file that contains the structure SQL statements and reads them into a string
        /// </summary>
        /// <param name="sqlDBStructure"></param>
        private void LoadDBStructureStatements(out string sqlDBStructure)
        {
            sqlDBStructure = System.IO.File.ReadAllText(DatabaseStructureFilePath);

            if (string.IsNullOrEmpty(sqlDBStructure))
                throw new Exception("The database structure file is empty.");
        }

        /// <summary>
        /// Creates the actual SQLite database file on disk
        /// </summary>
        /// <remarks>Note that the database will have no tables, views or data etc.</remarks>
        private void CreateEmptyDatabase()
        {
            // Verify that the output new database file does not already exist, then create it
            if (FilePath.Exists)
                throw new Exception("The database file path already exists.");

            SQLiteConnection.CreateFile(FilePath.FullName);
        }

        /// <summary>
        /// Find all text files containing lookup data and load the SQL commands in each file into a list
        /// </summary>
        /// <param name="sqlDataStatements">List of lookup data insert commands. One item in list per database lookup table.</param>
        private void LoadDataStatements(out List<string> sqlDataStatements)
        {
            sqlDataStatements = new List<string>();

            // Find all SQL files that insert lookup data into the database. These should be the data insert queries (e.g. Workbench_data_USGS_Gages.sql).
            string sSearchPath = string.Format("*{0}*.sql", DatabaseContentsWildcardPattern);
            foreach (string sFilePath in System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(DatabaseStructureFilePath), sSearchPath))
            {
                // Determine the data table from the filename that follows the keyword _data_ and preceeds the file suffix.
                Regex re = new Regex(string.Format(".*_data_(.*).sql", DatabaseContentsWildcardPattern));
                Match tableNameMatch = re.Match(sFilePath);
                string sTableName = tableNameMatch.Groups[1].ToString();

                // Replace the table placeholder text with the actual database table name
                string sSQLStatements = System.IO.File.ReadAllText(sFilePath);
                sSQLStatements = sSQLStatements.Replace("INSERT INTO table", string.Format("INSERT INTO {0}", sTableName));
                sqlDataStatements.Add(sSQLStatements);
            }
        }

        /// <summary>
        /// Takes and empty SQLite database file and executes the commands to create tables, views and fill lookup data tables
        /// </summary>
        /// <param name="sqlStructure">All the SQL commands to create the database structure. Separated by semicolon</param>
        /// <param name="sqlData">List of SQL commands to populate lookup tables with data. Each list item is a separate lookup table</param>
        private void PopulateEmptyDatabase(string sqlStructure, List<string> sqlData)
        {
            // Loop over each table that has lookup data defined and insert it.
            // Ensure referential integrity is off to make this insensitive to the order of the files 
            using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand("PRAGMA foreign_keys = off;", dbCon);
                dbCom.ExecuteNonQuery();

                // Not strictly needed because errors are irrecoverable, but speeds up the operation.
                SQLiteTransaction dbTrans = dbCon.BeginTransaction();

                try
                {
                    // Build the database structure
                    dbCom = new SQLiteCommand(sqlStructure, dbTrans.Connection, dbTrans);
                    dbCom.ExecuteNonQuery();

                    // Populate each lookup table with data
                    foreach (string sqlDataCommand in sqlData)
                    {
                        dbCom = new SQLiteCommand(sqlDataCommand, dbTrans.Connection, dbTrans);
                        dbCom.ExecuteNonQuery();
                    }

                    // Turn referential integrity back on
                    dbCom = new SQLiteCommand("PRAGMA foreign_keys = on;", dbTrans.Connection, dbTrans);
                    dbCom.ExecuteNonQuery();

                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    ex.Data["File Path"] = FilePath.FullName;
                    throw;
                }
            }
        }

        public void Upgrade(int nRequiredVersion)
        {
            if (!RequiresUpdrade(nRequiredVersion))
                return;

            using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
            {
                SQLiteTransaction dbTrans = dbCon.BeginTransaction();

                try
                {
                    UpgradeMaster(ref dbTrans, nRequiredVersion);
                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    throw;
                }
            }
        }

        protected abstract void UpgradeMaster(ref SQLiteTransaction dbTrans, int nRequiredVersion);
        protected abstract void BaseInstall(ref SQLiteTransaction dbTrans);
    }
}
