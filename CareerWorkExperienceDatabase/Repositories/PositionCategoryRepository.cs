using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionCategoryRepository
    {
        private string SQLQuery = "SELECT * FROM PositionCategories";
        
        public List<int> GetPositionsForCategories(int categoryID)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public List<int> GetCategoriesForPosition(int positionID)
        {
            List<int> returnMe = new List<int>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = SQLQuery + " WHERE PositionID=" + positionID;
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            int categoryID = Parsers.ParseInt(dataReader["CategoryID"].ToString());
                            if (categoryID > 0)
                            {
                                if (!returnMe.Contains(categoryID))
                                {
                                    returnMe.Add(categoryID);
                                }
                            }
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }
    }
}