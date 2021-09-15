using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector //IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               CreationDate
                                          FROM Journal";

                    List<Journal> entries = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal entry = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                        };
                        entries.Add(entry);
                    }

                    reader.Close();

                    return entries;
                }
            }
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, TextContent, CreationDate)
                                        OUTPUT INSERTED.Id
                                        VALUES (@title, @textContent, @creationDate)";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@textContent", journal.TextContent);
                    cmd.Parameters.AddWithValue("@creationDate", journal.CreationDate);

                    int id = (int)cmd.ExecuteScalar();

                    journal.Id = id;
                }
            }
        }
        
    }
}