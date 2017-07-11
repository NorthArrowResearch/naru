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

        private string DatabaseStructureSQLFile { get; set; }
        private string DatabaseContentsSQLFile { get; set; }

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

        private static string BuildConnectionString(string sFilePath)
        {
            return string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;foreign keys=true;", sFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="sqlVersionQuery"></param>
        /// <param name="nMinSupporterVersion"></param>
        /// <remarks>This constructor is also used during the creation of new databases.
        /// So do not check for the existance of the file on disck because it might not be present yet.</remarks>
        public DBManager(string sFilePath, string sqlVersionQuery, int nMinSupporterVersion
            , string sDBStructureSQL, string sDBContentsSQL)
        {
            if (string.IsNullOrEmpty(sFilePath))
                throw new Exception("Empty database file path");

            SQLVersion = sqlVersionQuery;
            FilePath = new System.IO.FileInfo(sFilePath);
            DatabaseStructureSQLFile = sDBStructureSQL;
            DatabaseContentsSQLFile = sDBContentsSQL;
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

        public void CreateDatabase()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load the database structure and contents SQL files.
            // Do this first so that any problems loading the file occur before the new database is created on disk.

            string sqlStructure = LoadSQLStatements(DatabaseStructureSQLFile, "structure");

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Verify that the database file does not already exist, then create it
            try
            {
                if (FilePath.Exists)
                    throw new Exception("The database file path already exists.");

                SQLiteConnection.CreateFile(FilePath.FullName);
            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Error generating new SQLite database file", ex);
                ex2.Data["File Path"] = FilePath.FullName;
                throw ex2;
            }

            using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
            {
                dbCon.Open();

                SQLiteCommand dbCom = new SQLiteCommand("PRAGMA foreign_keys = off;", dbCon);
                dbCom.ExecuteNonQuery();

                SQLiteTransaction dbTrans = dbCon.BeginTransaction();

                try
                {
                    dbCom = new SQLiteCommand(sqlStructure, dbTrans.Connection, dbTrans);
                    dbCom.ExecuteNonQuery();

                    LoadDataStatements(ref dbTrans, DatabaseContentsSQLFile, "contents");
                    
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

        private string LoadSQLStatements(string sFileName, string sSQLType)
        {
            string sPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), DatabaseStructureSQLFile);
            if (!System.IO.File.Exists(sPath))
            {
                Exception ex = new Exception(string.Format("The SQL {0} file does not exist.", sSQLType));
                ex.Data["File Path"] = sPath;
                throw ex;
            }

            string sSQLStatements = string.Empty;
            try
            {
                sSQLStatements = System.IO.File.ReadAllText(sPath);

                if (string.IsNullOrEmpty(sSQLStatements))
                    throw new Exception("The SQL file is empty.");
            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception(string.Format("Error loading statements from {0} SQL file.", sSQLType), ex);
                ex2.Data["File Path"] = sPath;
                throw ex2;
            }

            return sSQLStatements;
        }


        private void LoadDataStatements(ref SQLiteTransaction dbTrans, string filenameWildcard, string sSQLType)
        {
            // Find all SQL files in the Database folder that have the wildcard pattern. These should be the data insert queries (e.g. Workbench_data_USGS_Gages.sql).
            string dbDefinitionDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Database");
            string sSearchPath = string.Format("*{0}*.sql", filenameWildcard);
            foreach (string sFilePath in System.IO.Directory.GetFiles(dbDefinitionDir, sSearchPath))
            {
                // Determine the data table from the filename that follows the keyword _data_ and preceeds the file suffix.
                Regex re = new Regex(string.Format(".*_data_(.*).sql", filenameWildcard));
                Match tableNameMatch = re.Match(sFilePath);
                string sTableName = tableNameMatch.Groups[1].ToString();

                // Replace the table placeholder text with the actual database table name
                string sSQLStatements = System.IO.File.ReadAllText(sFilePath);
                sSQLStatements = sSQLStatements.Replace("INSERT INTO table", string.Format("INSERT INTO {0}", sTableName));

                SQLiteCommand dbCom = new SQLiteCommand(sSQLStatements, dbTrans.Connection, dbTrans);
                dbCom.ExecuteNonQuery();
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
