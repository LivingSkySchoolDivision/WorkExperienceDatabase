using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionFlagRepository
    {
        private readonly Dictionary<int, PositionFlag> _cache;

        public PositionFlagRepository()
        {
            _cache = new Dictionary<int, PositionFlag>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "SELECT * FROM PositionFlags";
                    sqlCommand.Connection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            int id = Parsers.ParseInt(dataReader["id"].ToString());
                            _cache.Add(id, new PositionFlag()
                            {
                                ID = id,
                                Name = dataReader["Name"].ToString(),
                                Description = dataReader["Description"].ToString(),
                                Icon = dataReader["Icon"].ToString()
                            });
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }
        }

        public List<PositionFlag> Get(List<string> ids)
        {
            // Convert list to a list of ints
            List<int> ids_as_ints = new List<int>();

            foreach(string id in ids)
            {
                int parsedID = Parsers.ParseInt(id);
                if (parsedID > 0)
                {
                    if (!ids_as_ints.Contains(parsedID))
                    {
                        ids_as_ints.Add(parsedID);
                    }
                }
            }

            return Get(ids_as_ints);
        }

        public List<PositionFlag> Get(List<int> ids)
        {
            List<PositionFlag> returnMe = new List<PositionFlag>();

            foreach(int id in ids)
            {
                if (_cache.ContainsKey(id))
                {
                    returnMe.Add(_cache[id]);
                }
            }

            return returnMe;
        }
    }
}