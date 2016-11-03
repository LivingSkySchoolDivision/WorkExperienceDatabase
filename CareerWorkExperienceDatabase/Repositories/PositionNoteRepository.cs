using CareerWorkExperienceDatabase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionNoteRepository
    {
        private string SQLQuery = "SELECT * FROM PositionNotes";

        private PositionNote dataReaderToPositionNote(SqlDataReader sqlRow)
        {
            return new PositionNote()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                PositionID = Parsers.ParseInt(sqlRow["PositionID"].ToString()),
                Note = sqlRow["Note"].ToString()
            };
        }

        public List<PositionNote> GetAll()
        {
            List<PositionNote> returnMe = new List<PositionNote>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe.Add(dataReaderToPositionNote(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public PositionNote Get(int PositionNoteID)
        {
            PositionNote returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" +  PositionNoteID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToPositionNote(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }
    }
}