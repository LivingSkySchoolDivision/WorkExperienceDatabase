﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class CategoryRepository
    {
        private string SQLQuery = "SELECT * FROM Categories";

        private Dictionary<int, Category> _cache;

        public CategoryRepository()
        {
            _cache = new Dictionary<int, Category>();

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
                            Category cat = dataReaderToCategory(dataReader);
                            if (cat != null)
                            {
                                _cache.Add(cat.ID, cat);
                            }
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }
        }

        private Category dataReaderToCategory(SqlDataReader sqlRow)
        {
            return new Category()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                COPSCategoryID = Parsers.ParseInt(sqlRow["COPSCategoryID"].ToString()),
                Name = sqlRow["Name"].ToString(),
                Description = sqlRow["Description"].ToString(),
            };
        }
        

        public Category Get(int categoryID)
        {
            if (_cache.ContainsKey(categoryID))
            {
                return _cache[categoryID];
            } else
            {
                return null;
            }
        }


        public List<Category> Get(List<int> categoryIDs)
        {
            List<Category> returnMe = new List<Category>();

            foreach(int id in categoryIDs)
            {
                if (_cache.ContainsKey(id))
                {
                    if (!returnMe.Contains(_cache[id]))
                    {
                        returnMe.Add(_cache[id]);
                    }
                }
            }

            return returnMe;
        }

        public List<Category> GetAll()
        {
            return _cache.Values.ToList();
        }

        public List<Category> Find(string name)
        {
            List<Category> returnMe = new List<Category>();

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
                            returnMe.Add(dataReaderToCategory(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

    }
}
 