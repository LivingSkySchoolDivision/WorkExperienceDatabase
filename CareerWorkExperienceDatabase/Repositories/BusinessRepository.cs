using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class BusinessRepository
    {
        private string SQLQuery = "SELECT * FROM Businesses";

       
        
        private Business dataReaderToBusiness(SqlDataReader sqlRow)
        {
            return new Business()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),               
                Name = sqlRow["Name"].ToString(),
                Address = sqlRow["Address"].ToString(),
                CityID = Parsers.ParseInt(sqlRow["CityID"].ToString()),
                ContactEmail = sqlRow["ContactEmail"].ToString(),
                ContactName = sqlRow["ContactName"].ToString(),
                ContactPhone = sqlRow["ContactPhone"].ToString(),
                LastUpdated = Parsers.ParseDate(sqlRow["LastUpdated"].ToString()),
                LastUpdatedBy = sqlRow["LastUpdatedBy"].ToString(),
                Expires = Parsers.ParseDate(sqlRow["Expires"].ToString()),
                RqInterview = Parsers.ParseBool(sqlRow["RqInterview"].ToString()),
                RqCriminalRecordCheck = Parsers.ParseBool(sqlRow["RqCriminalRecordCheck"].ToString()),
                SpecialRequirements = sqlRow["SpecialRequirements"].ToString(),
                NotInterested = Parsers.ParseBool(sqlRow["NotInterested"].ToString())
            };
        }

        public List<Business> GetAll()
       {
            List<Business> returnMe = new List<Business>();

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
                            returnMe.Add(dataReaderToBusiness(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public List<Business> GetInterestedBusinesses()
        {
            List<Business> returnMe = new List<Business>();

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
                            returnMe.Add(dataReaderToBusiness(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public Business Get(int businessID)
        {
            Business returnMe = null;

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
                            returnMe = dataReaderToBusiness(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;                    
        }

        public List<Business> Find(string name)
        {
            List<Business> returnMe = new List<Business>();

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
                            returnMe.Add(dataReaderToBusiness(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

    }
}