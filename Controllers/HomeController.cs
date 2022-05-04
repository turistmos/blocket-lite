using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using blocket_lite.Models;
using blocket_lite.Models.ProductViewModel;
using System.Security.Cryptography;
using System.Text;

//1', '2', '3','4','5','6','7'); DELETE FROM products3 WHERE price='0'; --
namespace blocket_lite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public static string filter;
    public static string filterToUser;
    public static string userLoggedIn;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public RedirectResult Show_clothes()
    {
        filter = "cloths";
        return Redirect("https://localhost:7296/Home/Index");
    }
    public RedirectResult Show_vehicles()
    {
        filter = "vehicles";
        return Redirect("https://localhost:7296/Home/Index");
    }
    public RedirectResult Show_all()
    {
        filter = "all";
        return Redirect("https://localhost:7296/Home/Index");
    }
    public IActionResult Index()
    {
        ViewBag.user = userLoggedIn;
        // hämtar alla objekt i databasen 
        var itemListModel = GetAllItems(filter);
        return View(itemListModel);
    }

    public IActionResult Privacy()
    {
        ViewBag.user = userLoggedIn;
        return View();

    }

    public IActionResult AddNewProduct()
    {
        ViewBag.user = userLoggedIn;
        return View();
    }

    public IActionResult register()
    {
        ViewBag.user = userLoggedIn;
        return View();
    }
    public IActionResult Login()
    {
        ViewBag.user = userLoggedIn;
        return View();
    }
    public IActionResult userLoginSuccess()
    {
        ViewBag.user = userLoggedIn;
        return View();
    }
    public IActionResult MyProducts()
    {
        ViewBag.user = userLoggedIn;
        var userItemList = GetUserItems();
        return View(userItemList);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    //Lägger in values från formulär till databasen.
    public RedirectResult Insert(ItemModel product)
    {

        if (product.category == "vehicle")
        {
            using (SqliteConnection con =
           new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    string txtSQL = "INSERT INTO products4 (category,title,price,description,image,miles,year,color,username) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8)";


                    con.Open();

                    tableCmd.CommandText = txtSQL;

                    tableCmd.Parameters.AddWithValue("@0", product.category);
                    tableCmd.Parameters.AddWithValue("@1", product.title);
                    tableCmd.Parameters.AddWithValue("@2", product.price);
                    tableCmd.Parameters.AddWithValue("@3", product.description);
                    tableCmd.Parameters.AddWithValue("@4", product.image);
                    tableCmd.Parameters.AddWithValue("@5", product.miles);
                    tableCmd.Parameters.AddWithValue("@6", product.year);
                    tableCmd.Parameters.AddWithValue("@7", product.color);
                    tableCmd.Parameters.AddWithValue("@8", userLoggedIn);

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
        }
        else if (product.category == "cloths")
        {
            {
                using (SqliteConnection con =
               new SqliteConnection("Data Source=db.sqlite"))
                {
                    using (var tableCmd = con.CreateCommand())
                    {
                        string txtSQL = "INSERT INTO products4 (category,title,price,description,image,gender,size,color,username) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8)";
                        con.Open();
                        tableCmd.CommandText = txtSQL;
                        tableCmd.Parameters.AddWithValue("@0", product.category);
                        tableCmd.Parameters.AddWithValue("@1", product.title);
                        tableCmd.Parameters.AddWithValue("@2", product.price);
                        tableCmd.Parameters.AddWithValue("@3", product.description);
                        tableCmd.Parameters.AddWithValue("@4", product.image);
                        tableCmd.Parameters.AddWithValue("@5", product.gender);
                        tableCmd.Parameters.AddWithValue("@6", product.size);
                        tableCmd.Parameters.AddWithValue("@7", product.color);
                        tableCmd.Parameters.AddWithValue("@8", userLoggedIn);


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
            }
        }


        return Redirect("https://localhost:7296/");
    }
    //hämtar alla produkter från databasen till en lista.
    internal ItemViewModel GetAllItems(string filter)
    {
        List<ItemModel> itemList = new();

        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                if (filter == "cloths")
                {
                    tableCmd.CommandText = "SELECT * FROM products4 WHERE category = 'cloths'";
                }
                else if (filter == "vehicles")
                {
                    tableCmd.CommandText = "SELECT * FROM products4 WHERE category = 'vehicle'";
                }
                else
                {
                    tableCmd.CommandText = "SELECT * FROM products4 ORDER BY price";
                }
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemList.Add(
                                new ItemModel
                                {
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9)
                                });
                        }
                    }
                    else
                    {
                        return new ItemViewModel
                        {
                            ItemList = itemList
                        };
                    }
                };
            }
        }

        return new ItemViewModel
        {
            ItemList = itemList
        };
    }
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
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {

                con.Open();


                tableCmd.CommandText = "CREATE TABLE " + user.username + "(productID INTEGER);";

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
    //Jämför användarna i databasen med i
    public RedirectResult UserLogin(UsersModel userLogin)
    {
        userLoggedIn = userLogin.username;
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
                    return Redirect("https://localhost:7296/home/userLoginSuccess");
                }

            }
        }

        return Redirect("https://localhost:7296/home/");
    }

    internal ItemViewModel GetUserItems()
    {
        List<ItemModel> userItemList = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM products4 WHERE username ='"+ userLoggedIn+"'";
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userItemList.Add(
                                new ItemModel
                                {
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9)
                                });
                        }
                    }
                    else
                    {
                        return new ItemViewModel
                        {
                            userItemList = userItemList
                        };
                    }
                };
                 return new ItemViewModel
                 {
                            userItemList = userItemList
                 };
            }

        }



    }
    internal ItemViewModel itemsLikedByUser()
    {
        List<ItemModel> likedItems = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM products4 WHERE username ='" + userLoggedIn + "'";
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            likedItems.Add(
                                new ItemModel
                                {
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9)
                                });
                        }
                    }
                    else
                    {
                        return new ItemViewModel
                        {
                            userItemList = likedItems
                        };
                    }
                };
                return new ItemViewModel
                {
                    userItemList = likedItems
                };
            }

        }

    }


}

