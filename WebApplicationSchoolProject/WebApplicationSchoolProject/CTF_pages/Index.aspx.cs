using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApplicationSchoolProject.CTF_pages
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["Username"] != null)
            {
                Response.Redirect("Quests.aspx");
            }
            
            /*
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Quests.aspx");
            }
            */
        }
    }
}