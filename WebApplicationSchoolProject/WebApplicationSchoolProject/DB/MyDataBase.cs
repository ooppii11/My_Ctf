using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web;
using WebApplicationSchoolProject.Cryptography_And_Security;


public class MyDataBase
{
    private static readonly string defaultDBName = "projectDB.MDB";
    private static OleDbConnection GetConnection(string dbName)
    {
        string location = HttpContext.Current.Server.MapPath($"~/{dbName}");
        OleDbConnection con = new OleDbConnection();
        con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + location;
        con.Open();
        return con;
    }

    static public OleDbCommand GetCommand(string sqlStr) // for static connection
    {
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = GetConnection(MyDataBase.defaultDBName);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlStr;
        return cmd;
    }


    static public OleDbCommand GetCommand(string sqlStr, OleDbConnection con)
    {
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlStr;
        return cmd;
    }

    //not save
    static public int ExecuteNonQuery(string strSql)
    {
        int rowsAffected;
        OleDbConnection con = GetConnection(MyDataBase.defaultDBName);
        OleDbCommand cmd = GetCommand(strSql, con);
        try
        {
            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (OleDbException ex)
        { throw ex; }
        finally
        { cmd.Connection.Close(); }
        return rowsAffected;
    }

    static public int ExecuteNonQuery(string strSql, params OleDbParameter[] parameters)
    {
        int rowsAffected;
        using (OleDbConnection con = GetConnection(MyDataBase.defaultDBName))
        {
            using (OleDbCommand cmd = GetCommand(strSql, con))
            {
                try
                {
                    cmd.Parameters.AddRange(parameters);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    throw ex;
                }
            }
        }
        return rowsAffected;
    }

    //not save
    static public Object ExecuteScalar(string strSql)
    {
        OleDbConnection con = GetConnection(MyDataBase.defaultDBName);
        OleDbCommand cmd = GetCommand(strSql, con);
        Object obj = cmd.ExecuteScalar();
        con.Close();
        return obj;
    }

    static public Object ExecuteScalar(string strSql, params OleDbParameter[] parameters)
    {
        using (OleDbConnection con = GetConnection(MyDataBase.defaultDBName))
        {
            using (OleDbCommand cmd = GetCommand(strSql, con))
            {
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
    }


    static public DataTable GetFullTable(string Table)  // get a full table
    {
        string strSql = "select * from " + Table;
        return GetTable(strSql);
    }

    static public DataTable GetTable(string strSql, params OleDbParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (OleDbConnection con = GetConnection(MyDataBase.defaultDBName))
        {
            using (OleDbCommand cmd = GetCommand(strSql, con))
            {
                cmd.Parameters.AddRange(parameters);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd))
                {
                    dataAdapter.Fill(dt);
                }
            }
        }
        return dt;
    }

    static public DataTable GetFullTable(string Table, object whereStr)
    {
        if (whereStr == null || whereStr.Equals(""))
            return GetFullTable(Table);
        else
        {
            string strSql = "select * from " + Table + " where " + whereStr;
            return GetTable(strSql);
        }
    }

    //not save
    static public DataTable GetTable(string strSql)
    {
        DataTable dt = new DataTable();
        OleDbConnection con = GetConnection(MyDataBase.defaultDBName);
        OleDbCommand cmd = GetCommand(strSql, con);
        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(strSql, con);
        dataAdapter.Fill(dt);
        return dt;
    }
    static bool IsTableExists(OleDbConnection connection, string tableName)
    {
        using (OleDbCommand command = new OleDbCommand())
        {
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM [{tableName}] WHERE 1=0";

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (OleDbException ex)
            {
                return false;
            }
        }
    }

    static public void CreateTable(string tableName, string[] columnDefinitions, string dbName = "projectDB.MDB")
    {
        using (OleDbConnection connection = GetConnection(dbName))
        {
            if (!IsTableExists(connection, tableName))
            {
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"CREATE TABLE [{tableName}] ({string.Join(", ", columnDefinitions)});";
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public static void CreateTables()
    {
        // Create Users table
        string[] usersColumns = { "user_id AUTOINCREMENT PRIMARY KEY", "username TEXT UNIQUE NOT NULL", "password_hash TEXT NOT NULL" };
        CreateTable("Users", usersColumns);

        // Create Challenges table
        // MEMO changed to Long Text
        string[] challengesColumns = { "challenge_id AUTOINCREMENT PRIMARY KEY", "title TEXT NOT NULL", "category TEXT", "points INT NOT NULL", "difficulty TEXT NOT NULL", "instructions LONGTEXT", "description LONGTEXT", "hint LONGTEXT", "file_path TEXT NOT NULL", "flag_hash TEXT NOT NULL" };
        CreateTable("Challenges", challengesColumns);

        // Create Solved_Challenges table
        string[] solvedChallengesColumns = { "user_id INT", "challenge_id INT", "solved_at TIMESTAMP", "FOREIGN KEY (user_id) REFERENCES Users(user_id)", "FOREIGN KEY (challenge_id) REFERENCES Challenges(challenge_id)", "PRIMARY KEY (user_id, challenge_id)" };
        CreateTable("Solved_Challenges", solvedChallengesColumns);

        // Create User_Scores table
        string[] userScoresColumns = { "user_id INT PRIMARY KEY", "score INT", "FOREIGN KEY (user_id) REFERENCES Users(user_id)" };
        CreateTable("User_Scores", userScoresColumns);
    }

    public static void InsertChallenges()
    {
        string query = "SELECT COUNT(title) FROM Challenges WHERE title = @Title";
        OleDbParameter parm = new OleDbParameter("@Title", OleDbType.VarChar) { Value = "Ceaser salad" };
        if (Int32.Parse(ExecuteScalar(query, parm).ToString()) == 0)
        {
            query = "INSERT INTO Challenges ([title], [category], [points], [difficulty], [instructions], [description], [hint], [file_path], [flag_hash]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";


            OleDbParameter[] parameters =
            {

                new OleDbParameter("@Title", OleDbType.VarChar) { Value = "Ceaser salad" },
                new OleDbParameter("@Category", OleDbType.VarChar) { Value = "Crypt" },
                new OleDbParameter("@Points", OleDbType.Integer) { Value = 40 },
                new OleDbParameter("@Difficulty", OleDbType.VarChar) { Value = "Easy" },
                new OleDbParameter("@Instructions", OleDbType.LongVarChar) { Value = "Can you find the password? Enter the password as flag in the following form: My_CTF{some_text}" },
                new OleDbParameter("@Description", OleDbType.LongVarChar) { Value = "Our Caesar got confused after he substituted his meal while he wrote the letter about his shifted ideas, and he used some weird ciphers. Can you decrypt it?" },
                new OleDbParameter("@Hint", OleDbType.LongVarChar) { Value = "Caesar cipher (also known as a type of shift cipher) and substitution cipher" },
                new OleDbParameter("@File_Path", OleDbType.VarChar) { Value = "Challenges/Caesar/Data.txt" },
                new OleDbParameter("@Flag_hash", OleDbType.VarChar) { Value = Crypto.Hash("My_ctf{this is a very good lunch}".ToLower()) }
            };

            ExecuteNonQuery(query, parameters);
        }
    }


}
