using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace WebApplicationSchoolProject.CTF_utilities
{
    public class Challenge
    {
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Status { get; set; } = "";
        public int Points { get; set; } = 0;
        public int Solvers { get; set; } = 0;
        public string Instructions { get; set; } = "";
        public string Description { get; set; } = "";
        public string Hint { get; set; } = "";

        public string FilePath { get; set; } = "";

        public Challenge(string challengeName, string status, int solvers)
        {
            string strSql = "SELECT * FROM Challenges WHERE title = @ChallengeName";
            OleDbParameter parameter = new OleDbParameter("@ChallengeName", OleDbType.VarChar);
            parameter.Value = challengeName;
            DataTable table = MyDataBase.GetTable(strSql, parameter);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                Title = row["title"]?.ToString();
                Category = row["category"]?.ToString();
                Points = Int32.Parse(row["points"]?.ToString());
                Instructions = row["instructions"]?.ToString();
                Description = row["description"]?.ToString();
                Hint = row["hint"]?.ToString();
                FilePath = row["file_path"]?.ToString();
                Status = status;
                Solvers = solvers;
            }
            else 
            {
                Console.WriteLine("No challenge found with the given name.");
            }
        }

        

        public Challenge(DataRow row, string status, int solvers)
        {
            Title = row["title"]?.ToString();
            Category = row["category"]?.ToString();
            Points = Int32.Parse(row["points"]?.ToString());
            Instructions = row["instructions"]?.ToString();
            Description = row["description"]?.ToString();
            Hint = row["hint"]?.ToString();
            FilePath = row["file_path"]?.ToString();
            Status = status;
            Solvers = solvers;
        }

        public Challenge(string title, string category, string status, int points, int solvers, string instructions, string dsescription, string hint, string file_path) 
        {
            Title = title;
            Category = category;
            Status = status;
            Points = points;
            Solvers = solvers;
            Instructions = instructions;
            Description = dsescription;
            Hint = hint;
            FilePath = file_path;
        }

        
    }
}