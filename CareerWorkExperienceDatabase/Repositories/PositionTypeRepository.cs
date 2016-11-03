using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase.Repositories
{
    public class PositionTypeRepository
    {
        private string SQLQuery = "SELECT * FROM PositionTypes";

        private PositionType dataReaderToPositionType(SqlDataReader sqlRow)
        {
            return new PositionType()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString(),
            };
        }

        public List<PositionType> GetAll()
        {
            List<PositionType> returnMe = new List<PositionType>();

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
                            returnMe.Add(dataReaderToPositionType(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public PositionType Get(int PositionTypeID)
        {
            PositionType returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + PositionTypeID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToPositionType(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }
    }
}