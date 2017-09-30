using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Xml;

namespace naru.db.sqlite
{
    public sealed class DBCon
    {
        #region Singleton Implementation

        // Singleton pattern take from version #3 on this link
        // http://csharpindepth.com/Articles/General/Singleton.aspx

        private static DBCon instance;
        private static readonly object padlock = new object();
        private static System.IO.FileInfo m_fiDatabasePath;
        private const string m_sRootConnectionStringLocal = "Data Source={0};Version=3;Pooling=True;Max Pool Size=100;foreign keys=true;";
        private const string m_sRootConnectionStringMaster = "server={0};uid={1};pwd={2};database={3};";

        private static Dictionary<string, string> SessionSettings;

        public static DBCon Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DBCon();
                    }
                    return instance;
                }
            }
        }

        #endregion
        
        public static string ConnectionString
        {
            get
            {
                if (m_fiDatabasePath is System.IO.FileInfo)
                    return string.Format(m_sRootConnectionStringLocal, m_fiDatabasePath.FullName);
                else
                    return string.Empty;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || !System.IO.File.Exists(value))
                    m_fiDatabasePath = null;
                else
                    m_fiDatabasePath = new System.IO.FileInfo(value);
            }
        }

        public static string BuildConnectionString(string sDBPath)
        {
            return string.Format(m_sRootConnectionStringLocal, sDBPath);
        }

        public static void CloseDatabase()
        {
            ConnectionString = string.Empty;
        }

        public static string DatabasePath
        {
            get
            {
                if (m_fiDatabasePath is System.IO.FileInfo)
                    return m_fiDatabasePath.FullName;
                else
                    return string.Empty;
            }
        }

        private DBCon()
        {
            // deliberately private and empty constructor.
        }

        public static void SetSessionSetting(string sKey, string sValue)
        {
            if (SessionSettings == null)
                SessionSettings = new Dictionary<string, string>();

            SessionSettings[sKey] = sValue;
        }

        public static string GetSessionSetting(string sKey)
        {
            string sValue = string.Empty;
            if (SessionSettings != null)
            {
                if (SessionSettings.ContainsKey(sKey))
                    sValue = SessionSettings[sKey];
            }
            return sValue;
        }
    }
}