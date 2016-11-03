using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionReviewRepository
    {
        private string SQLQuery = "SELECT * FROM PositionReview";

        private PositionReview dataReaderToPositionReview(SqlDataReader sqlRow)
        {
            return new PositionReview()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                PositionID = Parsers.ParseInt(sqlRow["PositionID"].ToString()),
                Username = sqlRow["Username"].ToString(),
                Comment  = sqlRow["Comment"].ToString(),
                Score = Parsers.ParseInt(sqlRow["Score"].ToString()),
                StaffReviewer = Parsers.ParseBool(sqlRow["SStaffReviewer"].ToString()),
                Private = Parsers.ParseBool(sqlRow["Private"].ToString())
            };
        }

        public List<PositionReview> GetAll()
        {
            List<PositionReview> returnMe = new List<PositionReview>();

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
                            returnMe.Add(dataReaderToPositionReview(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public PositionReview Get(int PositionReviewID)
        {
            PositionReview returnMe = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE ID=" + PositionReviewID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe = dataReaderToPositionReview(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }
    }
}