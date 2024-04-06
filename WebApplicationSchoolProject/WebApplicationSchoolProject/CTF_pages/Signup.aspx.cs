using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationSchoolProject.Cryptography_And_Security;

namespace WebApplicationSchoolProject.CTF_pages
{
    public partial class Signup : System.Web.UI.Page
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

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (!IsValidUsernameAndPass(newUsername, newPassword))
            {
                return;
            }

            if (!DoesUserExist(newUsername))
            {
                int rowsAffected = AddNewUserToDB(newUsername, newPassword);

                if (rowsAffected > 0)
                {
                    Session["Username"] = newUsername;

                    
                    Session["userID"] = GetUserIDfromDB();

                    AddNewUserScoreToDB();


                    //FormsAuthentication.SetAuthCookie(newUsername, false);
                    //User.Identity.Name
                    Response.Redirect("Instructions.aspx");
                }
                else
                {
                    lblErrorMessage.Text = "An error occurred while signing up. Please try again.";
                }
            }
            else
            {
                lblErrorMessage.Text = "The user already exists. Please choose a different username.";
            }

        }

        private bool IsValidUsernameAndPass(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblErrorMessage.Text = "Please enter both username and password.";
                return false;
            }

            var strongPasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if (!strongPasswordRegex.IsMatch(password))
            {
                lblErrorMessage.Text = "Password must be at least 8 English characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
                return false;
            }

            return true;
        }

        private bool DoesUserExist(string username)
        {
            int count;
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            {
                OleDbParameter[] parameters =
                {
                    new OleDbParameter("@Username", OleDbType.VarChar) { Value = username },
                };
                count = (int)MyDataBase.ExecuteScalar(query, parameters);
            }
            return count > 0;
        }

        private int AddNewUserToDB(string newUsername, string newPassword) 
        {
            string query = "INSERT INTO Users ([username], [password_hash]) VALUES (?, ?)";
            OleDbParameter[] parameters =
            {
                    new OleDbParameter("@username", OleDbType.VarChar) { Value = newUsername },
                    new OleDbParameter("@password_hash", OleDbType.VarChar) { Value = Crypto.Hash(newPassword) }
                };
            int rowsAffected = MyDataBase.ExecuteNonQuery(query, parameters);
            return rowsAffected;
        }

        private int GetUserIDfromDB()
        {
            string query = "SELECT user_id FROM Users WHERE username = @Username";
            OleDbParameter parameter = new OleDbParameter("@Username", OleDbType.VarChar) { Value = Session["Username"] };

            int user_id = Convert.ToInt32(MyDataBase.ExecuteScalar(query, parameter));

            return user_id;
        }

        private void AddNewUserScoreToDB()
        {
            string query = "INSERT INTO User_Scores (user_id, score) VALUES (?, 0)";
            OleDbParameter parameter = new OleDbParameter("@User_id", OleDbType.VarChar) { Value = Session["userID"] };
            
            MyDataBase.ExecuteNonQuery(query, parameter);
        }
    }
}