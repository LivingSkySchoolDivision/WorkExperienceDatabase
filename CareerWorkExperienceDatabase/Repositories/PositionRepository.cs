using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase.Repositories
{
    public class PositionRepository
    {

        private string SQLQuery = "SELECT * FROM Businesses";



        private Position dataReaderToPosition(SqlDataReader sqlRow)
        {
            return new Position()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString(),
                Description = sqlRow["Description"].ToString(),
                BusinessID = Parsers.ParseInt(sqlRow["BusinessID"].ToString()),
                PositionTypeID = Parsers.ParseInt(sqlRow["PositiontypeID"].ToString()),
                Seasonal = Parsers.ParseBool(sqlRow["Seasona1"].ToString()),
                StartDate = Parsers.ParseDate(sqlRow["StartDate"].ToString()),
                EndDate = Parsers.ParseDate(sqlRow["EndDate"].ToString()),
                LastUpdated = Parsers.ParseDate(sqlRow["LastUpdated"].ToString()),
                LastUpdatedBy = sqlRow["LastUpdatedBy"].ToString(),
                Expires = Parsers.ParseDate(sqlRow["Expires"].ToString()),
                SearchTags = sqlRow["SearchTags"].ToString()
            };
        }

        public List<Position> GetAll()
        {
            List<Position> returnMe = new List<Position>();

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
                            returnMe.Add(dataReaderToPosition(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public List<Position> GetInterestedPositions()
        {
            List<Position> returnMe = new List<Position>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE NotInterested=0";
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe.Add(dataReaderToPosition(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public Position Get(int ID)
        {
            Position returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + ID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToPosition(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }



        public List<Position> Find(string name)
        {
            List<Position> returnMe = new List<Position>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE Name LIKE '%" + name + "%'";
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe.Add(dataReaderToPosition(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

    }
}
