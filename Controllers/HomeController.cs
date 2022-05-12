﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using blocket_lite.Models;
using blocket_lite.Models.ProductViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;

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

    public IActionResult MyProducts()
    {
        ViewBag.user = userLoggedIn;
        var userItemList = GetUserItems();
        return View(userItemList);
    }
    public IActionResult LikedByUser()
    {
        ViewBag.user = userLoggedIn;
        var userLikedItems = GetUserLikedItems();
        return View(userLikedItems);
    }
    public IActionResult UserCart()
    {
        ViewBag.user = userLoggedIn;
        var userCartItems = GetUserCartItems();
        return View(userCartItems);
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
                                    image = reader.GetString(9),
                                    ProductID = reader.GetInt32(11)

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

    internal ItemViewModel GetUserItems()
    {
        List<ItemModel> userItemList = new();
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
                            userItemList.Add(
                                new ItemModel
                                {
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9),
                                    ProductID = reader.GetInt32(11)

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
    //lägger till items i tablet 
    public RedirectResult itemsLikedByUser(int id)
    {

        List<ItemModel> likedItems = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();

                tableCmd.CommandText = "SELECT * FROM products4 WHERE ProductID ='" + id + "'";
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
                                    image = reader.GetString(9),
                                    ProductID = reader.GetInt32(11)
                                });

                        }
                    }
                    else
                    {
                        return Redirect("https://localhost:7296/Home/Index");
                    }
                };




            }

        }
        // Kolla om produkten redan är gillad.
        using (SqliteConnection con =
          new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                string txtSQL = "SELECT * FROM " + userLoggedIn + " WHERE ProductID ='" + id + "'AND CART = '0'";


                con.Open();

                tableCmd.CommandText = txtSQL;
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        return Redirect("https://localhost:7296/Home/Index");
                    }
                    else
                    {

                    }

                };

            }
            likedItems = likedItems;

        }
        using (SqliteConnection con =
          new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                string txtSQL = "INSERT INTO " + userLoggedIn + " (category,title,price,description,image,ProductID,Cart) VALUES (@0,@1,@2,@3,@4,@5,@6)";


                con.Open();

                tableCmd.CommandText = txtSQL;

                tableCmd.Parameters.AddWithValue("@0", likedItems[0].category);
                tableCmd.Parameters.AddWithValue("@1", likedItems[0].title);
                tableCmd.Parameters.AddWithValue("@2", likedItems[0].price);
                tableCmd.Parameters.AddWithValue("@3", likedItems[0].description);
                tableCmd.Parameters.AddWithValue("@4", likedItems[0].image);
                tableCmd.Parameters.AddWithValue("@5", likedItems[0].ProductID);
                tableCmd.Parameters.AddWithValue("@6", 0);


                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            likedItems = likedItems;
            return Redirect("https://localhost:7296/Home/Index");
        }

    }
    internal ItemViewModel GetUserLikedItems()
    {
        List<ItemModel> userLikedItems = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM " + userLoggedIn + " WHERE Cart ='0'";
                try
                {
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userLikedItems.Add(
                                    new ItemModel
                                    {
                                        category = reader.GetString(0),
                                        title = reader.GetString(1),
                                        price = reader.GetInt32(2),
                                        description = reader.GetString(3),
                                        image = reader.GetString(4),
                                        ProductID = reader.GetInt32(5)

                                    });
                            }
                        }
                        else
                        {
                            return new ItemViewModel
                            {
                                userLikedItems = userLikedItems
                            };
                        }
                    };
                    return new ItemViewModel
                    {
                        userLikedItems = userLikedItems
                    };

                }
                catch
                {
                    return new ItemViewModel
                    {
                        userLikedItems = userLikedItems
                    };
                }

            }
        }

    }
    public RedirectResult itemsUnLikedByUser(int id)
    {
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "DELETE FROM " + userLoggedIn + " WHERE productID= " + id + " AND CART = '0'";

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
        return Redirect("https://localhost:7296/Home/LikedByUser");
    }

    public RedirectResult addToCart(int id)
    {
        List<ItemModel> addedToCartList = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();

                tableCmd.CommandText = "SELECT * FROM products4 WHERE ProductID ='" + id + "'";
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            addedToCartList.Add(
                                new ItemModel
                                {
                                    category = reader.GetString(0),
                                    title = reader.GetString(1),
                                    price = reader.GetInt32(2),
                                    description = reader.GetString(3),
                                    image = reader.GetString(9),
                                    ProductID = reader.GetInt32(11)
                                });

                        }
                    }
                    else
                    {
                        return Redirect("https://localhost:7296/Home/register");
                    }
                };




            }

        }
        // Kolla om produkten redan är tillagd i kundkorgen
        using (SqliteConnection con =
          new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                string txtSQL = "SELECT * FROM " + userLoggedIn + " WHERE ProductID ='" + id + "' AND CART = '1'";


                con.Open();

                tableCmd.CommandText = txtSQL;
                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        return Redirect("https://localhost:7296/Home/Index");
                    }
                    else
                    {

                    }

                };

            }
            addedToCartList = addedToCartList;

        }
        using (SqliteConnection con =
          new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                string txtSQL = "INSERT INTO " + userLoggedIn + " (category,title,price,description,image,ProductID,Cart) VALUES (@0,@1,@2,@3,@4,@5,@6)";


                con.Open();

                tableCmd.CommandText = txtSQL;

                tableCmd.Parameters.AddWithValue("@0", addedToCartList[0].category);
                tableCmd.Parameters.AddWithValue("@1", addedToCartList[0].title);
                tableCmd.Parameters.AddWithValue("@2", addedToCartList[0].price);
                tableCmd.Parameters.AddWithValue("@3", addedToCartList[0].description);
                tableCmd.Parameters.AddWithValue("@4", addedToCartList[0].image);
                tableCmd.Parameters.AddWithValue("@5", addedToCartList[0].ProductID);
                tableCmd.Parameters.AddWithValue("@6", 1);


                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            addedToCartList = addedToCartList;
            return Redirect("https://localhost:7296/Home/Index");
        }

    }
    internal ItemViewModel GetUserCartItems()
    {
        List<ItemModel> addedToCartList = new();
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM " + userLoggedIn + " WHERE Cart ='1'";
                try
                {
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                addedToCartList.Add(
                                    new ItemModel
                                    {
                                        category = reader.GetString(0),
                                        title = reader.GetString(1),
                                        price = reader.GetInt32(2),
                                        description = reader.GetString(3),
                                        image = reader.GetString(4),
                                        ProductID = reader.GetInt32(5)

                                    });
                            }
                        }
                        else
                        {
                            return new ItemViewModel
                            {
                                addedToCartList = addedToCartList
                            };
                        }
                    };
                    return new ItemViewModel
                    {
                        addedToCartList = addedToCartList
                    };

                }
                catch
                {
                    return new ItemViewModel
                    {
                        addedToCartList = addedToCartList
                    };
                }

            }
        }

    }

    public RedirectResult itemsDeletedFromCart(int id)
    {
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "DELETE FROM " + userLoggedIn + " WHERE productID= " + id + " AND CART = '0'";

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
        return Redirect("https://localhost:7296/Home/UserCart");
    }



}

