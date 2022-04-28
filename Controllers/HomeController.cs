using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using blocket_lite.Models;
using blocket_lite.Models.ProductViewModel;


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
        
    }

    public IActionResult AddNewProduct()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    //Lägger in values från formulär till databasen.
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
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
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
                    {
                        while (reader.Read())
                        {
                            itemList.Add(
                                new ItemModel
                                {
                                    category= reader.GetString(0),
                                    title= reader.GetString(1),
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
                            ItemList=itemList
                        };
                    }
                };
            }
        }

        return new ItemViewModel 
        {
            ItemList= itemList
        };
    }


}

