using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class AccountModel
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public Account AuthenticateUser(Account account)
        {
            Account instanceAccount = new Account();
            if (UserAuthenticated(account.UserName, account.Password))
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand command = new SqlCommand("spGetRole", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter paramUserName = new SqlParameter("@UserName", account.UserName);
                    SqlParameter paramPassword = new SqlParameter("@Password", account.Password);

                    command.Parameters.Add(paramUserName);
                    command.Parameters.Add(paramPassword);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        instanceAccount.UserName = reader["UserName"].ToString();
                        instanceAccount.Password = reader["Password"].ToString();
                        instanceAccount.Role     = reader["Role"].ToString();
                    }
                }
            }
            return instanceAccount;
        }
        private bool UserAuthenticated(string UserName, string Password)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramUserName = new SqlParameter("@UserName", UserName);
                SqlParameter paramPassword = new SqlParameter("@Password", Password);

                cmd.Parameters.Add(paramUserName);
                cmd.Parameters.Add(paramPassword);

                con.Open();

                int ReturnCode = (int)cmd.ExecuteScalar();

                return ReturnCode == 1;
            }
        }
        public bool Registeruser(Registration registration)
        {
            using (SqlConnection con = new SqlConnection (CS))
            {
                SqlCommand cmd = new SqlCommand ("spRegisterUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramUserName = new SqlParameter ("@UserName", registration.UserName);
                SqlParameter paramPassword = new SqlParameter ("@Password", registration.Password);
                SqlParameter paramRole     = new SqlParameter ("@Role", "User");
                SqlParameter paramEmail    = new SqlParameter ("@Email", registration.Email);

                cmd.Parameters.Add (paramUserName);
                cmd.Parameters.Add (paramPassword);
                cmd.Parameters.Add (paramRole);
                cmd.Parameters.Add (paramEmail);

                con.Open ();
                int ReturnCode = (int)cmd.ExecuteScalar ();

                if (ReturnCode == -1)
                    return false;
                else
                    return true;
            }
        }
        public List<Account> AllUsers
        {
            get
            {
                List<Account> listOfAllUsers = new List<Account> ();

                using (SqlConnection con = new SqlConnection (CS))
                {
                    SqlCommand cmd = new SqlCommand ("select * from Users", con);

                    con.Open ();
                    SqlDataReader reader = cmd.ExecuteReader ();

                    while (reader.Read ())
                    {
                        Account account = new Account ()
                        {
                            UserName = reader ["UserName"].ToString (),
                            Password = reader ["Password"].ToString (),
                            Role = reader ["Role"].ToString ()
                        };
                        listOfAllUsers.Add (account);
                    }
                }
                return listOfAllUsers;
            }
        }
    }
}