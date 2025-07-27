using Library_Management_Portal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;


namespace Library_Management_Portal.Controllers
{
    public class UserController : Controller
    {
        private readonly string connStr;

        public UserController()
        {
             connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }
        //ConfigurationManager.AppSettings["ConnString"];

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(userModel model)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO tbl_Users (Username, Password, Email,SecurityQuestion,SecurityAnswer) VALUES (@Username, @Password, @Email,@SecurityQuestion,@SecurityAnswer)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@SecurityQuestion", model.SecurityQuestion);
                    cmd.Parameters.AddWithValue("@SecurityAnswer", model.SecurityAnswer);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something Went Wrong..Please Try Again";

            }

            ViewBag.Message = "Registration is successful!!! Welcome  "+model.Username;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(string username, string password)
        {
           
            //string connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString; 
            bool isValidUser = false;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(*) FROM tbl_Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                isValidUser = count > 0;
                conn.Close();
            }

            if (isValidUser)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ForgotPassword(forgotPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.SecurityQuestion))
            {
               
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT SecurityQuestion FROM Tbl_Users WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    conn.Open();
                    var question = cmd.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(question))
                    {
                        ViewBag.SecurityQuestion = question;
                    }
                    else
                    {
                        ViewBag.Error = "Username not found.";
                    }
                }

                return View(model);
            }
            else
            {
                
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"SELECT COUNT(*) FROM Tbl_Users 
                                 WHERE Username = @Username AND SecurityAnswer = @Answer";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Answer", model.SecurityAnswer);
                    conn.Open();

                    int match = (int)cmd.ExecuteScalar();
                    if (match > 0)
                    {
                        string updateQuery = "UPDATE Tbl_Users SET Password = @Password WHERE Username = @Username";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@Password", model.NewPassword);
                        updateCmd.Parameters.AddWithValue("@Username", model.Username);
                        updateCmd.ExecuteNonQuery();

                        ViewBag.Message = "Password Reset successfully!";
                      //  return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ViewBag.Error = "Incorrect security answer.";
                    }
                }

                return View(model);
            }

        }

    }
}
