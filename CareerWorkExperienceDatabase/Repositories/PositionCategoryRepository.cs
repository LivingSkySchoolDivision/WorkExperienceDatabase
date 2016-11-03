using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionCategoryRepository
    {
        private string SQLQuery = "SELECT * FROM PositionCategory";

        private PositionCategory dataReaderToPositionCategory(SqlDataReader sqlRow)
        {
            return new PositionCategory()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString()
            };
        }

        public List<PositionCategory> GetAll()
        {
            List<PositionCategory> returnMe = new List<PositionCategory>();

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
                            returnMe.Add(dataReaderToPositionCategory(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public PositionCategory Get(int PositionCategoryID)
        {
            PositionCategory returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + PositionCategoryID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToPositionCategory(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }
    }
}