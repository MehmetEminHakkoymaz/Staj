using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.IO;

namespace WpfApp1.Data
{
    public class DatabaseHelper
    {
        private static string dbPath = "UserDatabase.db";
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Execute(@"
                        CREATE TABLE Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL
                        )");

                    // Admin kullanıcısını oluştur
                    connection.Execute(@"
                        INSERT INTO Users (Username, Password, Role) 
                        VALUES (@username, @password, @role)",
                        new { username = "admin", password = "admin123", role = "admin" });
                }
            }
        }

        public static void CreateProjectTable(string tableName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(@$"
                    CREATE TABLE IF NOT EXISTS {tableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NULL,
                        Sample TEXT NULL,  
                        DateTime TEXT NULL,
                        ElapsedTime TEXT NULL,
                        VesselType TEXT NULL,
                        TemperatureValue TEXT NULL,
                        TemperatureTarget TEXT NULL,
                        StirrerValue TEXT NULL,
                        StirrerTarget TEXT NULL,
                        pHValue TEXT NULL,
                        pHTarget TEXT NULL,
                        pO2Value TEXT NULL,
                        pO2Target TEXT NULL,
                        Gas1Value TEXT NULL,
                        Gas1Target TEXT NULL,
                        Gas2Value TEXT NULL,
                        Gas2Target TEXT NULL,
                        Gas3Value TEXT NULL,
                        Gas3Target TEXT NULL,
                        Gas4Value TEXT NULL,
                        Gas4Target TEXT NULL,
                        FoamValue TEXT NULL,
                        FoamTarget TEXT NULL,
                        RedoxValue TEXT NULL,
                        RedoxTarget TEXT NULL,
                        TurbidityValue TEXT NULL,
                        TurbidityTarget TEXT NULL,
                        BalanceValue TEXT NULL,
                        BalanceTarget TEXT NULL,
                        AirFlowValue TEXT NULL,
                        AirFlowTarget TEXT NULL,
                        Gas2FlowValue TEXT NULL,
                        Gas2FlowTarget TEXT NULL,
                        Gas3FlowValue TEXT NULL,
                        Gas3FlowTarget TEXT NULL,
                        Gas4FlowValue TEXT NULL,
                        Gas4FlowTarget TEXT NULL,
                        Gas5FlowValue TEXT NULL,
                        Gas5FlowTarget TEXT NULL,
                        ExitTurbidityValue TEXT NULL,
                        ExitTurbidityTarget TEXT NULL,
                        ExitBalanceValue TEXT NULL,
                        ExitBalanceTarget TEXT NULL,
                        Pump1Value TEXT NULL,
                        Pump1Target TEXT NULL,
                        Pump2Value TEXT NULL,
                        Pump2Target TEXT NULL,
                        Pump3Value TEXT NULL,
                        Pump3Target TEXT NULL,
                        Pump4Value TEXT NULL,
                        Pump4Target TEXT NULL,
                        Pump5Value TEXT NULL,
                        Pump5Target TEXT NULL,
                        Pump6Value TEXT NULL,
                        Pump6Target TEXT NULL
                    )");
            }
        }

        public static void LogData(string tableName, Dictionary<string, string> values)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Tüm kolon isimleri ile sabit bir sorgu oluştur
                    var sql = @$"INSERT INTO {tableName} (
                        Username, Sample, DateTime, ElapsedTime, VesselType,
                        TemperatureValue, TemperatureTarget, StirrerValue, StirrerTarget,
                        pHValue, pHTarget, pO2Value, pO2Target,
                        Gas1Value, Gas1Target, Gas2Value, Gas2Target,
                        Gas3Value, Gas3Target, Gas4Value, Gas4Target,
                        FoamValue, FoamTarget, RedoxValue, RedoxTarget,
                        TurbidityValue, TurbidityTarget, BalanceValue, BalanceTarget,
                        AirFlowValue, AirFlowTarget, Gas2FlowValue, Gas2FlowTarget,
                        Gas3FlowValue, Gas3FlowTarget, Gas4FlowValue, Gas4FlowTarget,
                        Gas5FlowValue, Gas5FlowTarget,
                        ExitTurbidityValue, ExitTurbidityTarget,
                        ExitBalanceValue, ExitBalanceTarget,
                        Pump1Value, Pump1Target, Pump2Value, Pump2Target,
                        Pump3Value, Pump3Target, Pump4Value, Pump4Target,
                        Pump5Value, Pump5Target, Pump6Value, Pump6Target
                    ) VALUES (
                        @Username, @Sample, @DateTime, @ElapsedTime, @VesselType,
                        @TemperatureValue, @TemperatureTarget, @StirrerValue, @StirrerTarget,
                        @pHValue, @pHTarget, @pO2Value, @pO2Target,
                        @Gas1Value, @Gas1Target, @Gas2Value, @Gas2Target,
                        @Gas3Value, @Gas3Target, @Gas4Value, @Gas4Target,
                        @FoamValue, @FoamTarget, @RedoxValue, @RedoxTarget,
                        @TurbidityValue, @TurbidityTarget, @BalanceValue, @BalanceTarget,
                        @AirFlowValue, @AirFlowTarget, @Gas2FlowValue, @Gas2FlowTarget,
                        @Gas3FlowValue, @Gas3FlowTarget, @Gas4FlowValue, @Gas4FlowTarget,
                        @Gas5FlowValue, @Gas5FlowTarget,
                        @ExitTurbidityValue, @ExitTurbidityTarget,
                        @ExitBalanceValue, @ExitBalanceTarget,
                        @Pump1Value, @Pump1Target, @Pump2Value, @Pump2Target,
                        @Pump3Value, @Pump3Target, @Pump4Value, @Pump4Target,
                        @Pump5Value, @Pump5Target, @Pump6Value, @Pump6Target
                    )";

                    // Tüm parametreler için varsayılan değerleri ayarla
                    var parameters = new DynamicParameters();
                    var allColumns = new[]
                    {
                        "Username", "Sample", "DateTime", "ElapsedTime", "VesselType",
                        "TemperatureValue", "TemperatureTarget", "StirrerValue", "StirrerTarget",
                        "pHValue", "pHTarget", "pO2Value", "pO2Target",
                        "Gas1Value", "Gas1Target", "Gas2Value", "Gas2Target",
                        "Gas3Value", "Gas3Target", "Gas4Value", "Gas4Target",
                        "FoamValue", "FoamTarget", "RedoxValue", "RedoxTarget",
                        "TurbidityValue", "TurbidityTarget", "BalanceValue", "BalanceTarget",
                        "AirFlowValue", "AirFlowTarget", "Gas2FlowValue", "Gas2FlowTarget",
                        "Gas3FlowValue", "Gas3FlowTarget", "Gas4FlowValue", "Gas4FlowTarget",
                        "Gas5FlowValue", "Gas5FlowTarget",
                        "ExitTurbidityValue", "ExitTurbidityTarget",
                        "ExitBalanceValue", "ExitBalanceTarget",
                        "Pump1Value", "Pump1Target", "Pump2Value", "Pump2Target",
                        "Pump3Value", "Pump3Target", "Pump4Value", "Pump4Target",
                        "Pump5Value", "Pump5Target", "Pump6Value", "Pump6Target"
                    };

                    // Tüm kolonlar için değer ata
                    foreach (var column in allColumns)
                    {
                        parameters.Add($"@{column}", values.ContainsKey(column) ? values[column] : "");
                    }

                    // Sorguyu çalıştır
                    var result = connection.Execute(sql, parameters);
                    Console.WriteLine($"Affected rows: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LogData: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public static void DebugTableStructure(string tableName)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var columns = connection.Query<dynamic>($"PRAGMA table_info({tableName})");

                    Console.WriteLine($"\nTablo yapısı ({tableName}):");
                    foreach (var column in columns)
                    {
                        Console.WriteLine($"Sütun: {column.name}, Tip: {column.type}, Null?: {column.notnull == 0}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tablo yapısı kontrolünde hata: {ex.Message}");
            }
        }
    }
}
