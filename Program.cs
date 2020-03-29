using Npgsql;
using System;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            String connectionString = "Server=localhost;" +
                                      "Port=5432;" +
                                      "UserId=postgres;" +
                                      "Password=2x0x0x0;" +
                                      "Database=constructioncompany;";

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            Console.Read();
        }
    }
}
