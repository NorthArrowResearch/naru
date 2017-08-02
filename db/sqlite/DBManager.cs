using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace naru.db.sqlite
{
    public class DBManager
    {
        public System.IO.FileInfo FilePath { get; internal set; }
        public string SQLVersion { get; internal set; }
        public int MinimumSupportedVersion { get; internal set; }

        private System.IO.DirectoryInfo diDBFolder { get; set; }
        private System.IO.FileInfo fiDBStructureSQL { get; set; }
        private Dictionary<long, System.IO.FileInfo> UpdateSQLFiles { get; set; }

        public enum UpgradeStates
        {
            MatchesCurrentVersion,
            RequiresUpgrade,
            BelowMinimumVersion,
            ExceedsCurrentVersion
        }

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
        /// Public method for checking whether a DB requires upgrading
        /// </summary>
        /// <param name="nRequiredVersion"></param>
        /// <returns></returns>
        public UpgradeStates CheckUpgradeStatus(int nRequiredVersion)
        {
            return CheckUpgradeStatus(GetDBVersion(), MinimumSupportedVersion, nRequiredVersion);
        }

        /// <summary>
        /// Private unit testable method for checking whether a DB requires upgrading
        /// </summary>
        /// <param name="nCurrentVersion"></param>
        /// <param name="nMinimumVersion"></param>
        /// <param name="nRequiredVersion"></param>
        /// <returns></returns>
        private UpgradeStates CheckUpgradeStatus(int nCurrentVersion, int nMinimumVersion, int nRequiredVersion)
        {
            if (nCurrentVersion == nRequiredVersion)
                return UpgradeStates.MatchesCurrentVersion;
            else
            {
                if (nCurrentVersion < MinimumSupportedVersion)
                    return UpgradeStates.BelowMinimumVersion;
                else
                {
                    if (nCurrentVersion > nRequiredVersion)
                        return UpgradeStates.ExceedsCurrentVersion;
                    else
                        return UpgradeStates.RequiresUpgrade;
                }
            }
        }

        ///// <summary>
        ///// The full absolute path to the file containing the SQL that creates all the tables and views
        ///// </summary>
        ///// <remarks>This is the executable folder with the Database structure file name appended. If the
        ///// file name has a folder as part of it (.e.g. "Database\myfile.sql") then it is included in this path</remarks>
        //public string DatabaseStructureFilePath
        //{
        //    get
        //    {
        //        string sPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), DatabaseStructureSQLFileName);
        //        if (!System.IO.File.Exists(sPath))
        //            throw new Exception("The database structure file does not exist.");
        //        return sPath;
        //    }
        //}

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
        /// <param name="sDBFolder">Folder name relative to the executable where the database structure and upgrade files can be found. e.g. Database</param>
        /// <param name="sDBStructure">Name of the SQL file that contains the latest database structure definition (e.g. latest.sql)</param>
        /// <param name="sDBUpdate">Search pattern for database update SQL files. Must use Windows wildcards (e.g. \update_*.sql)</param>
        /// <remarks>This constructor is also used during the creation of new databases.
        /// So do not check for the existance of the file on disck because it might not be present yet.
        /// 
        /// Each of the data SQL files must have the corresponding database table name after this DataWildcard.
        /// For example if the wildcard is "_data_" an example file name might be mytool_data_MyTable.sql</remarks>
        public DBManager(string sFilePath, string sqlVersionQuery, int nMinSupporterVersion, string sDBFolder, string sDBStructure, string sDBUpdate)
        {
            if (string.IsNullOrEmpty(sFilePath))
                throw new Exception("Empty database file path");

            SQLVersion = sqlVersionQuery;
            FilePath = new System.IO.FileInfo(sFilePath);

            diDBFolder = new System.IO.DirectoryInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), sDBFolder));
            if (!diDBFolder.Exists)
            {
                Exception ex = new Exception("The database SQL folder does not exist alongside the software executable.");
                ex.Data["Folder"] = diDBFolder.FullName;
                throw ex;
            }

            fiDBStructureSQL = new System.IO.FileInfo(System.IO.Path.Combine(diDBFolder.FullName, sDBStructure));
            if (!fiDBStructureSQL.Exists)
            {
                Exception ex = new Exception("The database structure SQL folder does not exist.");
                ex.Data["DB Structure File"] = fiDBStructureSQL.FullName;
                throw ex;
            }

            UpdateSQLFiles = new Dictionary<long, System.IO.FileInfo>();
            Regex re = new Regex(@"(\d{3})\.sql$");
            foreach (System.IO.FileInfo fiFile in diDBFolder.GetFiles(sDBUpdate, System.IO.SearchOption.TopDirectoryOnly))
            {
                Match ma = re.Match(fiFile.FullName);
                if (ma is Match && ma.Groups.Count == 2)
                {
                    long nVersion = long.Parse(ma.Groups[1].Value);
                    UpdateSQLFiles[nVersion] = fiFile;
                }
            }
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

        /// <summary>
        /// Creates a new database file, complete with tables, views and lookup data
        /// </summary>
        public void CreateDatabase()
        {
            try
            {
                // Load the structure and data SQL first so any problems occur before DB created on disk
                string sqlDBStructure = System.IO.File.ReadAllText(fiDBStructureSQL.FullName);

                // Create the empty SQLite database file
                SQLiteConnection.CreateFile(FilePath.FullName);

                // Loop over each table that has lookup data defined and insert it.
                // Ensure referential integrity is off to make this insensitive to the order of the files 
                using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
                {
                    dbCon.Open();
                    SQLiteCommand dbCom = new SQLiteCommand(sqlDBStructure, dbCon);
                    dbCom.ExecuteNonQuery();

                    // Turn referential integrity back on
                    dbCom = new SQLiteCommand("PRAGMA foreign_keys = on;", dbCon);
                    dbCom.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ex.Data["Database File Path"] = FilePath.FullName;
                ex.Data["Database Structure File Path"] = fiDBStructureSQL.FullName;
                throw;
            }
        }

        /// <summary>
        /// Updates an existing database to the argument required version.
        /// </summary>
        /// <param name="nRequiredVersion">The required version of the end result</param>
        /// <remarks>
        /// 1. Does nothing if the database is already at the required version.
        /// 2. Throws error if one or more upgrade files are missing.
        /// 3. Turns off referential integrity until all upgrades are complete.
        /// 4. Performs upgrades in sequential order until required version is achieved.
        /// 5. Rolls back database if anything goes wrong</remarks>
        public void Upgrade(int nRequiredVersion)
        {
            if (CheckUpgradeStatus(nRequiredVersion) != UpgradeStates.RequiresUpgrade)
                return;

            // Verify that all required update files exist **before** attempting any changes
            int nCurrentVersion = GetDBVersion();
            List<int> MissingVersions = new List<int>();
            for (int nVersion = nCurrentVersion + 1; nVersion <= nRequiredVersion; nVersion++)
                if (!UpdateSQLFiles.ContainsKey(nVersion))
                    MissingVersions.Add(nVersion);

            if (MissingVersions.Count > 0)
            {
                Exception ex = new Exception("One or more required update files are missing.");
                ex.Data["Missing Versions"] = string.Join(",", MissingVersions.Select(x => x.ToString()).ToArray<string>());
                throw ex;
            }

            using (SQLiteConnection dbCon = new SQLiteConnection(ConnectionString))
            {
                dbCon.Open();

                // Turn referential integrity off
                SQLiteCommand dbCom = new SQLiteCommand("PRAGMA foreign_keys = off;", dbCon);
                dbCom.ExecuteNonQuery();

                SQLiteTransaction dbTrans = dbCon.BeginTransaction();

                try
                {
                    for (int nVersion = nCurrentVersion + 1; nVersion <= nRequiredVersion; nVersion++)
                    {
                        // Load the update from file and execute the SQL commands
                        string sqlUpdate = System.IO.File.ReadAllText(UpdateSQLFiles[nVersion].FullName);

                        dbCom = new SQLiteCommand(sqlUpdate, dbTrans.Connection, dbTrans);
                        dbCom.ExecuteNonQuery();
                    }

                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    throw;
                }
                finally
                {
                    // Turn referential integrity back on
                    dbCom = new SQLiteCommand("PRAGMA foreign_keys = on;", dbCon);
                    dbCom.ExecuteNonQuery();
                }
            }
        }
    }
}
