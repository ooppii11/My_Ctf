using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using WebApplicationSchoolProject.Cryptography_And_Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web.Security;



namespace WebApplicationSchoolProject.CTF_pages
{
    public partial class Login : System.Web.UI.Page
    {
        public string LoginErrorMsg { get; set; } = "";

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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND password_hash = @Password";
            OleDbParameter[] parameters =
            {
                new OleDbParameter("@Username", OleDbType.VarChar) { Value = username },
                new OleDbParameter("@Password", OleDbType.VarChar) { Value = Crypto.Hash(password) }
            };
            int count = (int)MyDataBase.ExecuteScalar(query, parameters);

            if (count > 0)
            {
                Session["Username"] = username;
                //FormsAuthentication.SetAuthCookie(username, false);
                //User.Identity.Name
                Response.Redirect("Quests.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Invalid Login information";
            }
        }
    }
}