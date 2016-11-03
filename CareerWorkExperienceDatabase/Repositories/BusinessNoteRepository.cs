using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase.Repositories
{
    public class BusinessNoteRepository
    {
        private string SQLQuery = "SELECT * FROM BusinessNote";

        private BusinessNote dataReaderToBusinessNote(SqlDataReader sqlRow)
        {
            return new BusinessNote()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                BusinessID = Parsers.ParseInt(sqlRow["BusinessID"].ToString()),
                Notes = (sqlRow["Notes"].ToString()),
                Private = Parsers.ParseBool(sqlRow["Private"].ToString()),
            };
        }

        public List<BusinessNote> GetAll()
        {
            List<BusinessNote> returnMe = new List<BusinessNote>();

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
                            returnMe.Add(dataReaderToBusinessNote(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public BusinessNote Get(int businessID)
        {
            BusinessNote returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + businessID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToBusinessNote(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

      


    }
}