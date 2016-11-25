using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CareerWorkExperienceDatabase
{
    public partial class index : System.Web.UI.Page
    {
        BusinessRepository businessRepo = new BusinessRepository();
        CityRepository cityRepo = new CityRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load all (interested) positions
            PositionRepository positionRepo = new PositionRepository();
            List<Position> allPositions = positionRepo.GetInterestedPositions();
            
            // Subtract the ones that aren't filled somehow?

            // Load all categories
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();

            // Load updated positions 
            List<Position> lastUpdatedPositions = new List<Position>();
            lastUpdatedPositions = positionRepo.GetRecentlyUpdated(25);


            // Put the positions into a dictionary
            Dictionary<Category, List<Position>> positionsByCategory = new Dictionary<Category, List<Position>>();

            Category uncategorized = new Category
            {
                ID = -1,
                Name = "Uncategorized"
            };

            foreach(Position pos in allPositions)
            {
                // Get a list of categories this position is in
                List<int> thisPositionCategories = pos.CategoryIDs;

                // Add each of those categories to the dictionary if they don't already exist
                foreach (int catID in thisPositionCategories)
                {
                    Category thisCategory = categoryRepository.Get(catID);

                    // If this category exists
                    if (thisCategory != null)
                    {
                        if (!positionsByCategory.ContainsKey(thisCategory))
                        {
                            positionsByCategory.Add(thisCategory, new List<Position>());
                        }
                    }

                    if (!positionsByCategory[thisCategory].Contains(pos))
                    {
                        positionsByCategory[thisCategory].Add(pos);
                    }
                }

                if (thisPositionCategories.Count == 0)
                {
                    if (!positionsByCategory.ContainsKey(uncategorized))
                    {
                        positionsByCategory.Add(uncategorized, new List<Position>());
                    }
                    positionsByCategory[uncategorized].Add(pos);
                }
            }        
                
                
                                    
            // Build list of categories
            // Only want to display categories that have positions in them
            // Display the number of positions in each category

            litCategories.Text += "<div class=\"category_list\">";
            foreach(Category cat in positionsByCategory.Keys.OrderBy(c => c.Name))
            {
                litCategories.Text += "<a href=\"byCategory.aspx?id=" + cat.ID + "\">" + cat.Name + " (" + positionsByCategory[cat].Count() +  ")</a>";
            }
            litCategories.Text += "</div>";


            CityRepository cityRepository = new CityRepository();
            List<City> allCities = cityRepository.GetAll();
            litCities.Text += "<div class='floating_menu'>";
            foreach(City city in allCities.OrderBy(c => c.Name))
            {
                litCities.Text += "<a href=\"byLocation.aspx?id=" + city.ID + "\">" + city.Name + "</a>";
            }
            litCities.Text += "</div>";


            // Displaying list of recently updated positions
            litUpdatedPositions.Text= "<div class='updatedPositions'>";
            foreach (Position pos in lastUpdatedPositions)
            {
                litUpdatedPositions.Text += displayUpdatedPosition(pos);
            }
            litUpdatedPositions.Text += "</div>";

        }

        string displayUpdatedPosition(Position position)
        {
            string returnMe = string.Empty;

            Business positionBusiness = businessRepo.Get(position.BusinessID);
            
            if (positionBusiness != null)
            {
                City positionCity = cityRepo.Get(positionBusiness.CityID);
                if (positionCity != null)
                {
                    returnMe += "<div class='position'>";

                    returnMe += "<div class='positionName'><a href='viewPosition.aspx?id=" + position.ID + "'>" + position.Name + "</a></div>";
                    returnMe += "<div class='businessName'>" + positionBusiness.Name +"</div>";
                    returnMe += "<div class='locationName'>" + positionCity.Name + "</div>";

                    returnMe += "</div>";
                }
            }

            return returnMe;
        }
    }
}