using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class ActivePositionRepository
    {
        private string SQLQuery = "SELECT * FROM ActivePositions";

        private ActivePosition dataReaderToActivePosition(SqlDataReader sqlRow)
        {
            return new ActivePosition()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                PositionID = Parsers.ParseInt(sqlRow["PositionID"].ToString()),
                StudentName = sqlRow["StudentName"].ToString(),
                Start = Parsers.ParseDate(sqlRow["Start"].ToString()),
                End = Parsers.ParseDate(sqlRow["End"].ToString())
            };

        }

        public List<ActivePosition> GetAll()
        {
            List<ActivePosition> returnMe = new List<ActivePosition>();

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
                            returnMe.Add(dataReaderToActivePosition(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }
      
            return returnMe;
        }

        public ActivePosition Get(int PositionID)
        {
            ActivePosition returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + PositionID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToActivePosition(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public List<ActivePosition> Find(string name)
        {
            List<ActivePosition> returnMe = new List<ActivePosition>();

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
                            returnMe.Add(dataReaderToActivePosition(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

    }
     
}