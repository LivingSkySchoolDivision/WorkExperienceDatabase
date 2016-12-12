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
                Interested = Parsers.ParseBool(sqlRow["Interested"].ToString())
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

        public void Insert(Business thisBusiness)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = 
                        "INSERT INTO " + 
                        "Businesses(Name,Address,CityID,ContactName,ContactPhone,ContactEmail,LastUpdated,LastUpdatedBy,Expires,RqInterview,RqCriminalRecordCheck,SpecialRequirements,Interested) " +
                        "VALUES(@NAME,@ADDRESS,@CITYID,@CONTACTNAME,@CONTACTPHONE,@CONTACTEMAIL,@LASTUPDATED,@LASTUPDATEDBY,@EXPIRES,@RQINTERVIEW,@RQCRIMINALRECORDCHECK,@SPECIALREQUIREMENTS,@INTERESTED)";

                    sqlCommand.Parameters.AddWithValue("NAME",thisBusiness.Name);
                    sqlCommand.Parameters.AddWithValue("ADDRESS", thisBusiness.Address);
                    sqlCommand.Parameters.AddWithValue("CITYID", thisBusiness.CityID);
                    sqlCommand.Parameters.AddWithValue("CONTACTNAME", thisBusiness.ContactName);
                    sqlCommand.Parameters.AddWithValue("CONTACTPHONE", thisBusiness.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("CONTACTEMAIL", thisBusiness.ContactEmail);
                    sqlCommand.Parameters.AddWithValue("LASTUPDATED", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("LASTUPDATEDBY", ""); // TODO: Come back to this later
                    sqlCommand.Parameters.AddWithValue("EXPIRES", thisBusiness.Expires);
                    sqlCommand.Parameters.AddWithValue("RQINTERVIEW", thisBusiness.RqInterview);
                    sqlCommand.Parameters.AddWithValue("RQCRIMINALRECORDCHECK", thisBusiness.RqCriminalRecordCheck);
                    sqlCommand.Parameters.AddWithValue("SPECIALREQUIREMENTS", thisBusiness.SpecialRequirements);
                    sqlCommand.Parameters.AddWithValue("INTERESTED", thisBusiness.Interested);

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }

        public void Update(Business thisBusiness)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "UPDATE businesses SET " +
                       "name=@NAME, address=@ADDRESS, cityid=@CITYID, contactname=@CONTACTNAME, contactphone=@CONTACTPHONE, " + 
                       "contactemail=@CONTACTEMAIL, lastupdated=@LASTUPDATED, lastupdatedby=@LASTUPDATEDBY, expires=@EXPIRES, rqinterview=@RQINTERVIEW, " +
                       "rqcriminalrecordcheck=@RQCRIMINALRECORDCHECK, specialrequirements=@SPECIALREQUIREMENTS, interested=@INTERESTED " + 
                       "WHERE id=@ID";

                    sqlCommand.Parameters.AddWithValue("ID", thisBusiness.ID);
                    sqlCommand.Parameters.AddWithValue("NAME", thisBusiness.Name);
                    sqlCommand.Parameters.AddWithValue("ADDRESS", thisBusiness.Address);
                    sqlCommand.Parameters.AddWithValue("CITYID", thisBusiness.CityID);
                    sqlCommand.Parameters.AddWithValue("CONTACTNAME", thisBusiness.ContactName);
                    sqlCommand.Parameters.AddWithValue("CONTACTPHONE", thisBusiness.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("CONTACTEMAIL", thisBusiness.ContactEmail);
                    sqlCommand.Parameters.AddWithValue("LASTUPDATED", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("LASTUPDATEDBY", ""); // TODO: Come back to this later
                    sqlCommand.Parameters.AddWithValue("EXPIRES", thisBusiness.Expires);
                    sqlCommand.Parameters.AddWithValue("RQINTERVIEW", thisBusiness.RqInterview);
                    sqlCommand.Parameters.AddWithValue("RQCRIMINALRECORDCHECK", thisBusiness.RqCriminalRecordCheck);
                    sqlCommand.Parameters.AddWithValue("SPECIALREQUIREMENTS", thisBusiness.SpecialRequirements);
                    sqlCommand.Parameters.AddWithValue("INTERESTED", thisBusiness.Interested);

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        } 
           

    }
}