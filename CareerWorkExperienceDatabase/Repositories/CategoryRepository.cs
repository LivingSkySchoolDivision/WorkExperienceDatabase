using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class CategoryRepository
    {
        private COPSCategoryRepository COPSCategoryRepo;
        private string SQLQuery = "SELECT * FROM Categories";

        private Dictionary<int, Category> _cache;

        public CategoryRepository()
        {
            COPSCategoryRepo = new COPSCategoryRepository();

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
            int categoryID = Parsers.ParseInt(sqlRow["ID"].ToString());
            return new Category()
            {
                ID = categoryID,
                Name = sqlRow["Name"].ToString(),
                Description = sqlRow["Description"].ToString(),
                COPSCategories = COPSCategoryRepo.GetForCategory(categoryID)
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

        public void Insert(Category thisCategory)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText =
                        "INSERT INTO " +
                        "Categories(Name,Description,COPSCategories) " +
                        "VALUES(@NAME,@DESCRIPTION,@COPSCATEGORIES)";

                    sqlCommand.Parameters.AddWithValue("NAME", thisCategory.Name);
                    sqlCommand.Parameters.AddWithValue("DESCRIPTION", thisCategory.Description);
                    sqlCommand.Parameters.AddWithValue("COPSCATEGORIES", thisCategory.COPSCategories);
                    
                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }

        public void Update(Category thisCategory)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "UPDATE categories SET " +
                       "name=@NAME, Description=@DESCRIPTION, COPSCategories=@COPSCATEGORIES " + 
                       "WHERE id=@ID";

                    sqlCommand.Parameters.AddWithValue("ID", thisCategory.ID);
                    sqlCommand.Parameters.AddWithValue("NAME", thisCategory.Name);
                    sqlCommand.Parameters.AddWithValue("DESCRIPTION", thisCategory.Description);
                    sqlCommand.Parameters.AddWithValue("COPSCATEGORIES", thisCategory.COPSCategories);
                    

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }
    }
}
 