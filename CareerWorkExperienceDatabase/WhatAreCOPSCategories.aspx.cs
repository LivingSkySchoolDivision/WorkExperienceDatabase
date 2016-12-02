using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CareerWorkExperienceDatabase
{
    public partial class WhatAreCOPSCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblCategories.Rows.Clear();
            COPSCategoryRepository COPSRepo = new COPSCategoryRepository();

            foreach(COPSCategory category in COPSRepo.GetAll())
            {
                TableRow newRow = new TableRow();

                newRow.Cells.Add(new TableCell()
                {
                    Text = "<a href=\"/byCOPSCategory.aspx?id=" + category.ID + "\"><img src=\"/Images/COPSBadges/" + category.Icon + "\" alt=\"" + category.Name + "\"></a>"
                });


                newRow.Cells.Add(new TableCell()
                {
                    Text = "<B>" + category.Name + "</b><BR>" + category.Description,
                    VerticalAlign = VerticalAlign.Top
                });

                tblCategories.Rows.Add(newRow);
            }
        }
    }
}