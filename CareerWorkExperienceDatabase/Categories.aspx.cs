using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CareerWorkExperienceDatabase
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();

            litCategories.Text += "<div class=\"category_list\">";
            foreach (Category cat in allCategories.OrderBy(c => c.Name))
            {
                litCategories.Text += "<a href=\"/byCategory.aspx?id=" + cat.ID + "\">" + cat.Name + "</a>";
            }
            litCategories.Text += "</div>";
        }
    }
}