using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using blocket_lite.Models;
using blocket_lite.Models.ProductViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
<<<<<<< HEAD

namespace blocket_lite.Controllers;

public class UserController : Controller
{
    public static bool loggedIn = true;
    public IActionResult Login()
    {
        ViewBag.user = HomeController.userLoggedIn;
        return View();
    }

    public IActionResult userLoginSuccess()
    {
        ViewBag.user = HomeController.userLoggedIn;
        return View();
    }

    public IActionResult register()
    {
        ViewBag.user = HomeController.userLoggedIn;
        return View();
    }
    //Logga in function.
    public RedirectResult UserLogin(UsersModel userLogin)
    {

        List<UsersModel> userList = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM users";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userList.Add(
                                new UsersModel
                                {
                                    username = reader.GetString(0),
                                    password = reader.GetString(1),
                                    salt = reader.GetString(2)
                                });
                        }
                    }
                };
            }
        }


        for (int i = 0; i < userList.Count; i++)
        {
            if (userLogin.username == userList[i].username)
            {
                //omvandlar pswd login-fältet till en hash
                byte[] data = Encoding.ASCII.GetBytes(userLogin.password + userList[i].salt);
                data = new SHA256Managed().ComputeHash(data);
                String hash = Encoding.ASCII.GetString(data);

                if (userList[i].password == hash)
                {
                    HomeController.userLoggedIn = userLogin.username;
                    return Redirect("https://localhost:7296/User/userLoginSuccess");
                }

            }
        }

        return Redirect("https://localhost:7296/user/Login/");
    }

    // registrering:
    //sparar ned ett hashat lösen till databasen
    public RedirectResult UserInsert(UsersModel user)
    {

        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[16];
        rng.GetBytes(buff);
        string salt = Convert.ToBase64String(buff);


        byte[] data = Encoding.ASCII.GetBytes(user.password + salt);
        data = new SHA256Managed().ComputeHash(data);
        String hash = Encoding.ASCII.GetString(data);
        string txtSQL = "INSERT INTO users (username,password,salt) Values(@0,@1,@2)";


        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {

                con.Open();


                tableCmd.CommandText = txtSQL;

                tableCmd.Parameters.AddWithValue("@0", user.username);
                tableCmd.Parameters.AddWithValue("@1", hash);
                tableCmd.Parameters.AddWithValue("@2", salt);

                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }
        //skapar ett nytt sql-table för varje användare
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {

                con.Open();


                tableCmd.CommandText = "CREATE TABLE " + user.username + "(category TEXT, title TEXT, price INTEGER, description TEXT,image TEXT, ProductID INTEGER);";

                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }


        return Redirect("https://localhost:7296/home");
    }
    public RedirectResult loggOut()
    {
        HomeController.userLoggedIn = null;
        return Redirect("https://localhost:7296/home");
=======
using System;

namespace blocket_lite.Controllers {

    public class UserController : Controller
    {

        public IActionResult Login()
        {
            ViewBag.user = HomeController.userLoggedIn;
            return View();
        }

        public IActionResult userLoginSuccess()
        {
            ViewBag.user = HomeController.userLoggedIn;
            return View();
        }

        public IActionResult register()
        {
            ViewBag.user = HomeController.userLoggedIn;
            return View();
        }
        //Logga in function.
        public RedirectResult UserLogin(UsersModel userLogin)
        {

            List<UsersModel> userList = new();
            using (SqliteConnection con =
            new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = "SELECT * FROM users";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userList.Add(
                                    new UsersModel
                                    {
                                        username = reader.GetString(0),
                                        password = reader.GetString(1),
                                        salt = reader.GetString(2)
                                    });
                            }
                        }
                    };
                }
            }


            for (int i = 0; i < userList.Count; i++)
            {
                if (userLogin.username == userList[i].username)
                {
                    //omvandlar pswd login-fältet till en hash
                    byte[] data = Encoding.ASCII.GetBytes(userLogin.password + userList[i].salt);
                    data = new SHA256Managed().ComputeHash(data);
                    String hash = Encoding.ASCII.GetString(data);

                    if (userList[i].password == hash)
                    {
                        HomeController.userLoggedIn = userLogin.username;
                        return Redirect("https://localhost:7296/User/userLoginSuccess");
                    }

                }
            }

            return Redirect("https://localhost:7296/home/");
        }

        // registrering:
        //sparar ned ett hashat lösen till databasen
        public RedirectResult UserInsert(UsersModel user)
        {

            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[16];
            rng.GetBytes(buff);
            string salt = Convert.ToBase64String(buff);


            byte[] data = Encoding.ASCII.GetBytes(user.password + salt);
            data = new SHA256Managed().ComputeHash(data);
            String hash = Encoding.ASCII.GetString(data);
            string txtSQL = "INSERT INTO users (username,password,salt) Values(@0,@1,@2)";


            using (SqliteConnection con =
            new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {

                    con.Open();


                    tableCmd.CommandText = txtSQL;

                    tableCmd.Parameters.AddWithValue("@0", user.username);
                    tableCmd.Parameters.AddWithValue("@1", hash);
                    tableCmd.Parameters.AddWithValue("@2", salt);

                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //skapar ett nytt sql-table för varje användare
            using (SqliteConnection con =
            new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {

                    con.Open();


                    tableCmd.CommandText = "CREATE TABLE " + user.username + "(category TEXT, title TEXT, price INTEGER, description TEXT,image TEXT, ProductID INTEGER, Cart INTEGER);";

                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
            }


            return Redirect("https://localhost:7296/home");
        }
>>>>>>> c297ae0fcb752dcd822bf4c24eb09f4717043273
    }
}