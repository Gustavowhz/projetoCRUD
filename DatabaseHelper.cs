using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms; 

namespace projetoCRUD
{
    public static class DatabaseHelper
    {
        // Caminho relativo ajustado para sua estrutura de pastas
        private static string _databasePath = Path.Combine(
            Application.StartupPath,
            "..", "..", "Database", "usuarios.db"
        );

        private static string _connectionString = $"Data Source={_databasePath};Version=3;";

        public static void InicializarBancoDeDados()
        {
            var dir = Path.GetDirectoryName(_databasePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);  // Cria a pasta se não existir
            }
            // Garante que o diretório existe
            Directory.CreateDirectory(Path.GetDirectoryName(_databasePath));

            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);

                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(@"
                        CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Senha TEXT NOT NULL
                        )", connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}