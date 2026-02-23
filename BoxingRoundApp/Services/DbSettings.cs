using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Services
{
    public static class DbSettings
    {
        public const string DatabaseName = "boxingroundapp.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
    }
}
