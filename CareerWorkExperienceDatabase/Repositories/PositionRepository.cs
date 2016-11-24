using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionRepository
    {
        private readonly PositionTypeRepository positionTypeRepo;
        private readonly PositionCategoryRepository positionCategoryRepo;

        private string SQLQuery = "SELECT * FROM Positions";
        

        public PositionRepository()
        {
            positionTypeRepo = new PositionTypeRepository();
            positionCategoryRepo = new PositionCategoryRepository();
        }

        private Position dataReaderToPosition(SqlDataReader sqlRow)
        {
            return new Position()
            {
                ID = Parsers.ParseInt(sqlRow["ID"].ToString()),
                Name = sqlRow["Name"].ToString(),
                Description = sqlRow["Description"].ToString(),
                BusinessID = Parsers.ParseInt(sqlRow["BusinessID"].ToString()),
                PositionTypeID = Parsers.ParseInt(sqlRow["PositionTypeID"].ToString()),
                Seasonal = Parsers.ParseBool(sqlRow["Seasonal"].ToString()),
                StartDate = Parsers.ParseDate(sqlRow["StartDate"].ToString()),
                EndDate = Parsers.ParseDate(sqlRow["EndDate"].ToString()),
                LastUpdated = Parsers.ParseDate(sqlRow["LastUpdated"].ToString()),
                LastUpdatedBy = sqlRow["LastUpdatedBy"].ToString(),
                Expires = Parsers.ParseDate(sqlRow["Expires"].ToString()),
                SearchTags = sqlRow["SearchTags"].ToString(),
                CategoryIDs = positionCategoryRepo.GetCategoriesForPosition(Parsers.ParseInt(sqlRow["ID"].ToString()))
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
                    sqlCommand.CommandText = "SELECT Positions.* FROM Positions LEFT OUTER JOIN Businesses ON Positions.BusinessID=Businesses.ID WHERE BUsinesses.Interested=1";
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


        public List<Position> GetRecentlyUpdated(int count)
        {
            List<Position> returnMe = new List<Position>();

            if (count > 0)
            {
                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = connection;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "SELECT TOP " + count + " Positions.* FROM Positions LEFT OUTER JOIN Businesses ON Positions.BusinessID=Businesses.ID WHERE BUsinesses.Interested=1 ORDER BY ";
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

        public void Put(Position thisPosition)
        {
            
        }

        public void Update(Position thisPosition)
        {
            
        }

    }
}
