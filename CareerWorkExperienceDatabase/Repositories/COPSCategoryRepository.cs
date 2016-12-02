using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class COPSCategoryRepository
    {
        private readonly Dictionary<int, COPSCategory> _cache;
        private readonly Dictionary<int, List<int>> _categoryMappings;
       
        public COPSCategoryRepository()
        {
            _cache = new Dictionary<int, COPSCategory>();
            _categoryMappings = new Dictionary<int, List<int>>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = connection;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "SELECT * FROM COPSCategories";
                        sqlCommand.Connection.Open();

                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                COPSCategory parsedCOPSCat = dataReaderToCOPSCategory(dataReader);
                                if (parsedCOPSCat != null)
                                {
                                    _cache.Add(parsedCOPSCat.ID, parsedCOPSCat);
                                }
                            }
                        }
                        sqlCommand.Connection.Close();
                    }
                }

                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = connection;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "SELECT * FROM CategoryCOPSCategories";
                        sqlCommand.Connection.Open();

                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                int CategoryID = Parsers.ParseInt(dataReader["CategoryID"].ToString());
                                int COPSCategoryID = Parsers.ParseInt(dataReader["COPSCategoryID"].ToString());

                                if (!_categoryMappings.ContainsKey(CategoryID))
                                {
                                    _categoryMappings.Add(CategoryID, new List<int>());
                                }
                                _categoryMappings[CategoryID].Add(COPSCategoryID);
                            }
                        }
                        sqlCommand.Connection.Close();
                    }
                }
            }

        }

        private COPSCategory dataReaderToCOPSCategory(SqlDataReader sqlRow)
        {
            return new COPSCategory()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString(),
                Description = sqlRow["Description"].ToString(),
                Icon = sqlRow["Icon"].ToString(),
                Number = Parsers.ParseInt(sqlRow["CategoryNumber"].ToString())
            };
        }

        public List<COPSCategory> GetAll()
        {
            return _cache.Values.ToList();
        }

        public COPSCategory Get(int COPSCategoryID)
        {
            if (_cache.ContainsKey(COPSCategoryID))
            {
                return _cache[COPSCategoryID];
            }
            return null;
        }
        
        public List<COPSCategory> Get(List<int> COPSCategoryIDs)
        {
            List<COPSCategory> returnMe = new List<COPSCategory>();

            List<int> foundIDs = new List<int>();
            foreach(int COPSID in COPSCategoryIDs)
            {
                if (!foundIDs.Contains(COPSID))
                {
                    if (_cache.ContainsKey(COPSID))
                    {
                        returnMe.Add(_cache[COPSID]);
                        foundIDs.Add(COPSID);
                    }                   
                }
            }

            return returnMe.OrderBy(c => c.Number).ToList();
        }

        public List<COPSCategory> GetForCategory(int categoryID)
        {
            if (_categoryMappings.ContainsKey(categoryID))
            {
                return Get(_categoryMappings[categoryID]);
            }
            return new List<COPSCategory>();
        }

        public List<COPSCategory> GetForCategory(Category category)
        {
            return GetForCategory(category.ID);
        }
    }
}