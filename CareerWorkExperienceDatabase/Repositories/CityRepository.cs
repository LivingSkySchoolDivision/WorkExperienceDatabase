﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class CityRepository
    {
        private string SQLQuery = "SELECT * FROM cities";



        private City dataReaderToCity(SqlDataReader sqlRow)
        {
            return new City()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString(),
             
            };
        }

        public List<City> GetAll()
        {
            List<City> returnMe = new List<City>();

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
                            returnMe.Add(dataReaderToCity(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }


        public City Get(int businessID)
        {
            City returnMe = null;

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
                            returnMe = dataReaderToCity(dataReader);
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }


        public void Insert(City thisCity)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText =
                        "INSERT INTO " +
                        "Cities(Name) " +
                        "VALUES(@NAME)";

                    sqlCommand.Parameters.AddWithValue("NAME", thisCity.Name);
                

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }

        public void Update(City thisCity)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "UPDATE cities SET " +
                       "name=@NAME " +
                       "WHERE id=@ID";

                    sqlCommand.Parameters.AddWithValue("ID", thisCity.ID);
                    sqlCommand.Parameters.AddWithValue("NAME", thisCity.Name);
                   

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }
    }
}