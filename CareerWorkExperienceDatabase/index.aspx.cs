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
        CategoryRepository categoryRepo = new CategoryRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load all (interested) positions
            PositionRepository positionRepo = new PositionRepository();
            List<Position> allPositions = positionRepo.GetInterestedPositions();
            
            // Subtract the ones that aren't filled somehow?
            
            // Load updated positions 
            List<Position> lastUpdatedPositions = new List<Position>();
            lastUpdatedPositions = positionRepo.GetRecentlyUpdated(25);

            // Put the positions into a dictionary, so we can count their categories
            Dictionary<int, List<Position>> positionsByCategoryID = new Dictionary<int, List<Position>>();
            List<Position> positionsWithoutCategories = new List<Position>();

            foreach(Position pos in allPositions)
            {
                if (pos.Categories.Count == 0)
                {
                    positionsWithoutCategories.Add(pos);
                }

                foreach(Category cat in pos.Categories)
                {
                    if (!positionsByCategoryID.ContainsKey(cat.ID))
                    {
                        positionsByCategoryID.Add(cat.ID, new List<Position>());
                    }
                    positionsByCategoryID[cat.ID].Add(pos);
                }
            }
                        
            Dictionary<Category, List<Position>> positionsByCategory = new Dictionary<Category, List<Position>>();
            if (positionsWithoutCategories.Count > 0)
            {
                positionsByCategory.Add(new Category() { Name = "Uncategorized", ID = 0, Description = "Positions without a category" }, positionsWithoutCategories);
            }


            foreach(int categoryID in positionsByCategoryID.Keys)
            {
                Category cat = categoryRepo.Get(categoryID);
                if (cat != null) {
                    positionsByCategory.Add(cat, positionsByCategoryID[cat.ID]);
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