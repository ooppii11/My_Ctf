using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using WebApplicationSchoolProject.CTF_utilities;
using System.Data.OleDb;
using System.Data;
using System.IO;
using WebApplicationSchoolProject.Cryptography_And_Security;
using System.Web.Security;
using System.Security.Cryptography;
using System.Collections;
using System.Data.Common;
using System.Text.RegularExpressions;



namespace WebApplicationSchoolProject.CTF_pages
{
    public partial class Quests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            /*
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Index.aspx");
            }
            */
            else
            {
                string requestedHashFilePath = Request.QueryString["file"];
                if (!string.IsNullOrEmpty(requestedHashFilePath))
                {
                    string filePath = GetFileFromWhitelist(requestedHashFilePath);
                    if (filePath != null)
                    {
                        ServeFile(filePath);
                    }
                    else
                    {
                        Response.Write("Access to the requested file is not allowed.");
                    }
                }
            }
        }
        private string GetFileFromWhitelist(string filePath)
        {
            //maybe get from the db...
            List<string> allowedFilePaths = new List<string>
            {
                "Challenges/Caesar/Data.txt"
            };

            foreach (string allowedPath in allowedFilePaths)
            {
                if (string.Equals(filePath, Crypto.Hash(allowedPath), StringComparison.OrdinalIgnoreCase))
                {
                    return allowedPath;
                }
            }

            return null;
        }
        private void ServeFile(string fileName)
        {
            string basePath = Server.MapPath("~/Files/");
            string filePath = Path.Combine(basePath, fileName);

            if (!filePath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
            {
                Response.StatusCode = 403;
                Response.End();
                return;
            }

            if (System.IO.File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                Response.StatusCode = 404;
                Response.End();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            //FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Index.aspx");
        }

        protected List<Challenge> GetChallengesFromDatabase()
        {
            List<Challenge> Challenges = new List<Challenge>();
            string strSql = "SELECT * FROM Challenges";
            DataTable table = MyDataBase.GetTable(strSql);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows) 
                {
                    int solvers = 0;
                    int challengeId = Convert.ToInt32(row["challenge_id"]);
                    strSql = $"SELECT COUNT(user_id) FROM Solved_Challenges WHERE challenge_id = {challengeId}";
                    object result = MyDataBase.ExecuteScalar(strSql);
                    if (result != null)
                    {
                        solvers = Convert.ToInt32(result);
                    }

                    Challenges.Add(new Challenge(row, HasUserSolvedChallenge(Session["Username"].ToString(), challengeId) ? "solved" : "", solvers));
                }
            }
            else
            {
                Console.WriteLine("No challenges found");
            }

            return Challenges;
        }

       

        protected void btnSubmitFlag_Click(object sender, EventArgs e)
        {
            string challengeTitle = hiddenField.Value;
            string submittedFlag = Request.Form[$"flag_{challengeTitle}"].ToLower();

            string query = "SELECT challenge_id, points, flag_hash FROM Challenges WHERE title = @Title";
            OleDbParameter parameter = new OleDbParameter("@Title", OleDbType.VarChar) { Value = challengeTitle.Replace("_", " ") };
            DataTable table = MyDataBase.GetTable(query, parameter);

            int points = Convert.ToInt32(table.Rows[0]["points"]);
            int challengeID = Convert.ToInt32(table.Rows[0]["challenge_id"]);

            if (HasUserSolvedChallenge(Session["Username"].ToString(), challengeID))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "updateLabelAndCardStatus", "updateLabelAndCardStatus('" + "You already solved this challenge." + "', '" + "red" + "', '" + challengeTitle + "');", true);
                return;
            }

            if (string.IsNullOrEmpty(submittedFlag) || !IsFlagValid(submittedFlag))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "updateLabelAndCardStatus", "updateLabelAndCardStatus('" + "Please enter a valid flag. My_ctf{some_text}" + "', '" + "red" + "', '" + challengeTitle + "');", true);
                return;
            }
            
            if (table.Rows.Count > 0)
            {

                string flagHash = table.Rows[0][2].ToString();
                if (Crypto.Hash(submittedFlag) == flagHash)
                {
                    CacheUserID();
                    UpdateUserScore(points);
                    AddToSolvedChallenges(challengeTitle.Replace("_", " "));
                    ClientScript.RegisterStartupScript(this.GetType(), "updateLabelAndCardStatus", "updateLabelAndCardStatus('" + "Correct! Well done!" + "', '" + "green" + "', '" + challengeTitle + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "updateLabelAndCardStatus", "updateLabelAndCardStatus('" + "Incorrect flag. Please try again." + "', '" + "red" + "', '" + challengeTitle + "');", true);
                }

            }
            else 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "updateLabelAndCardStatus", "updateLabelAndCardStatus('" + "There is no challenge with the provided title. Are you trying to hack me?" + "', '" + "red" + "', '" + challengeTitle + "');", true);
            }
            
        }

        private bool IsFlagValid(string flag)
        {
            var correctFlagRegex = new Regex(@"My_ctf\{[^}]+\}", RegexOptions.IgnoreCase);
            if (!correctFlagRegex.IsMatch(flag))
            {
                return false;
            }
            return true;
        }

        private void UpdateUserScore(int points)
        {
            string query = $"UPDATE User_Scores SET score = (score + @Points) WHERE user_id = @User_id";
            OleDbParameter[] parameters = new OleDbParameter[] {
                new OleDbParameter("@Points", OleDbType.Integer) { Value = points },
                new OleDbParameter("@User_id", OleDbType.Integer) { Value = Session["userID"] },
            };
            MyDataBase.ExecuteNonQuery(query, parameters);
        }

        private void AddToSolvedChallenges(string challengeTitle)
        {
            string query = "SELECT challenge_id FROM Challenges WHERE Title = @Challenge_title";

            OleDbParameter[] parameters = new OleDbParameter[]
            {
                new OleDbParameter("@Challenge_title", OleDbType.VarChar) { Value = challengeTitle },
            };

            int challenge_id = Convert.ToInt32(MyDataBase.ExecuteScalar(query, parameters));


            query = "INSERT INTO solved_challenges (user_id, challenge_id) VALUES (?, ?)";
            parameters = new OleDbParameter[]
            {
                new OleDbParameter("@User_id", OleDbType.Integer) { Value = Session["UserID"] },
                new OleDbParameter("@Challenge_id", OleDbType.Integer) { Value = challenge_id }
            };
            MyDataBase.ExecuteNonQuery(query, parameters);
        }

        private void CacheUserID()
        {
            if (Session["userID"] == null)
            {
                string query = "SELECT user_id FROM Users WHERE username = @Username";
                OleDbParameter parameter = new OleDbParameter("@Username", OleDbType.VarChar) { Value = Session["Username"] };
                int user_id = Convert.ToInt32(MyDataBase.ExecuteScalar(query, parameter));
                Session["userID"] = user_id;
            }
        }
        protected bool HasUserSolvedChallenge(string userName, int challengeId)
        {
            string strSql = @"
            SELECT 
                COUNT(*) 
            FROM 
                Solved_Challenges
            INNER JOIN 
                Users ON Solved_Challenges.user_id = Users.user_id
            WHERE 
                Users.username = @UserName 
            AND 
                Solved_Challenges.challenge_id = @ChallengeId";

            OleDbParameter[] parameters = {
                new OleDbParameter("@UserName", OleDbType.VarChar) { Value = userName },
                new OleDbParameter("@ChallengeId", OleDbType.Integer) { Value = challengeId }
            };

            int count = Convert.ToInt32(MyDataBase.ExecuteScalar(strSql, parameters));

            return count > 0;
        }
    }
}


