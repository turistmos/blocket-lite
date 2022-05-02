using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using blocket_lite.Models;
using blocket_lite.Models.ProductViewModel;
<<<<<<< HEAD
=======
using System.Security.Cryptography;
using System.Text;
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f


namespace blocket_lite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public static string filter;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public RedirectResult Show_clothes()
    {
<<<<<<< HEAD
     filter="cloths";
     return Redirect("https://localhost:7296/Home/Index");
    }
      public RedirectResult Show_vehicles()
    {
     filter="vehicles";
     return Redirect("https://localhost:7296/Home/Index");
    }
       public RedirectResult Show_all()
    {
     filter="all";
     return Redirect("https://localhost:7296/Home/Index");
=======
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
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
    }
    public IActionResult Index()
    {
        // hämtar alla objekt i databasen 
        var itemListModel = GetAllItems(filter);
        return View(itemListModel);
    }

    public IActionResult Privacy()
    {
        return View();
<<<<<<< HEAD
        
=======

>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
    }

    public IActionResult AddNewProduct()
    {
        return View();
    }

<<<<<<< HEAD
=======
    public IActionResult register()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult userLoginSuccess()
    {
        return View();
    }



>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    //Lägger in values från formulär till databasen.
<<<<<<< HEAD
     public RedirectResult Insert(ItemModel product)
   {
       if(product.category=="vehicle")
       {
        using (SqliteConnection con = 
       new SqliteConnection("Data Source=db.sqlite"))
       {
           using (var tableCmd = con.CreateCommand())
           {
               con.Open();
               tableCmd.CommandText = $"INSERT INTO products3 (category,title,price,description,image,miles,year,color) VALUES ('{product.category}','{product.title}','{product.price}','{product.description}','{product.image}','{product.miles}','{product.year}','{product.color}')";
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
       else if (product.category=="cloths")
       {
          {
        using (SqliteConnection con = 
       new SqliteConnection("Data Source=db.sqlite"))
       {
           using (var tableCmd = con.CreateCommand())
           {
               con.Open();
               tableCmd.CommandText = $"INSERT INTO products3 (category,title,price,description,image,gender,size,color) VALUES ('{product.category}','{product.title}','{product.price}','{product.description}','{product.image}','{product.gender}','{product.size}','{product.color}')";
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
=======
    public RedirectResult Insert(ItemModel product)
    {
        if (product.category == "vehicle")
        {
            using (SqliteConnection con =
           new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT INTO products3 (category,title,price,description,image,miles,year,color) VALUES ('{product.category}','{product.title}','{product.price}','{product.description}','{product.image}','{product.miles}','{product.year}','{product.color}')";
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
                        con.Open();
                        tableCmd.CommandText = $"INSERT INTO products3 (category,title,price,description,image,gender,size,color) VALUES ('{product.category}','{product.title}','{product.price}','{product.description}','{product.image}','{product.gender}','{product.size}','{product.color}')";
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
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
<<<<<<< HEAD
                if(filter=="cloths")
                {
                tableCmd.CommandText= "SELECT * FROM products3 WHERE category = 'cloths'";
                }
                else if(filter=="vehicles")
                {
                    tableCmd.CommandText= "SELECT * FROM products3 WHERE category = 'vehicle'";
                }
                else
                {
                tableCmd.CommandText= "SELECT * FROM products3 ORDER BY price";
                }
                using (var reader = tableCmd.ExecuteReader())
                {
                    if(reader.HasRows)
=======
                if (filter == "cloths")
                {
                    tableCmd.CommandText = "SELECT * FROM products3 WHERE category = 'cloths'";
                }
                else if (filter == "vehicles")
                {
                    tableCmd.CommandText = "SELECT * FROM products3 WHERE category = 'vehicle'";
                }
                else
                {
                    tableCmd.CommandText = "SELECT * FROM products3 ORDER BY price";
                }
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
                    {
                        while (reader.Read())
                        {
                            itemList.Add(
                                new ItemModel
                                {
<<<<<<< HEAD
                                    category= reader.GetString(0),
                                    title= reader.GetString(1),
=======
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9)
                                });
                        }
                    }
<<<<<<< HEAD
                    else 
                    {
                        return new ItemViewModel
                        {
                            ItemList=itemList
=======
                    else
                    {
                        return new ItemViewModel
                        {
                            ItemList = itemList
>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f
                        };
                    }
                };
            }
        }

<<<<<<< HEAD
        return new ItemViewModel 
        {
            ItemList= itemList
        };
    }
=======
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
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"INSERT INTO users (username, password, salt) VALUES ('{user.username}','{hash}','{salt}')";
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

>>>>>>> ff36e440a3c6581372b6a50ca99e6ad71581237f


}

