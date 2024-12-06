using System;
using AssetTracker;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace AssetTracker
{
    public class Program
    {
        private static MyDbContext MyDb = new MyDbContext();  // MyDbContext is initialized here

        public static void MenuCommand()
        {
            string[] menuOptions = new string[] { "Assets according to Office", "Assets by Purchase Date", "Get statistics", "Add Product", "Update product info", "Delete product", "Quit Session" };
            int menuSelect = 0;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Hello and welcome to AssetTracker! Please make a choice:");
                Console.ResetColor();

                // Loop through the menu options
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelect)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine(menuOptions[i] + " <--");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                // Get user input for menu navigation
                var keyPressed = Console.ReadKey();

                // Handle navigation and selection
                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1)
                {
                    menuSelect++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                {
                    menuSelect--;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    Console.Clear();  // Clear screen after option is selected

                    try
                    {
                        switch (menuSelect)
                        {
                            case 0:
                                // If option 0 is selected, proceed to display assets by office
                                Console.WriteLine("Option 0 selected - Assets according to Office");
                                var completeList = MyDb.Assets.Include(t => t.Products).ToList();

                                Console.WriteLine("Products by Office:");
                                foreach (var asset in completeList)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(asset.Asset_type);
                                    Console.ResetColor();

                                    // Print headers with fixed width padding
                                    Console.WriteLine(
                                        " - " +
                                        "Office".PadRight(15) + " " +
                                        "Model".PadRight(20) + " " +
                                        "Brand".PadRight(15) + " " +
                                        "Purchase Date".PadRight(15) + " " +
                                        "Price".PadRight(15) + " " +
                                        "Currency".PadRight(10) + " " +
                                        "In USD".PadRight(15)
                                    );

                                    // Print a separator line
                                    Console.WriteLine(
                                        "".PadRight(15 + 25 + 20 + 15 + 15 + 10 + 15, '-')
                                    );

                                    foreach (var product in asset.Products.OrderBy(p => p.Office).ThenBy(p => p.PurchaseData))
                                    {
                                        double inUSD = DoConversion.PerformConversion(product.Currency, "USD", product.LocalPrice);
                                        double toUSD = Math.Round(inUSD, 1);
                                        formatDate checkDate = new formatDate();
                                        int difference = checkDate.RedDate(checkDate.start, product.PurchaseData);

                                        // Align data for product row
                                        string office = product.Office.PadRight(15);
                                        string model = product.Model.PadRight(20);
                                        string brand = product.Brand.PadRight(15);
                                        string purchaseDate = product.PurchaseData.ToString("yyyy-MM-dd").PadRight(15);
                                        string price = product.LocalPrice.ToString("0.00").PadRight(15);
                                        string currency = product.Currency.PadRight(10);
                                        string usd = toUSD.ToString("0.00").PadRight(15);

                                        if (difference > 33)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {usd}");
                                            Console.ResetColor();
                                        }
                                        else if (difference > 30 && difference < 33)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {usd}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {usd}");
                                        }
                                    }
                                }
                                break;

                            case 1:
                                var listByDate = MyDb.Assets.Include(t => t.Products).ToList();
                                // For option 1, display assets by purchase date (keep empty for now)                            

                                Console.WriteLine("Option 1 selected - Assets by Purchase Date");

                                Console.WriteLine("Products by Purchase Date:");
                                foreach (var asset in listByDate)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(asset.Asset_type);
                                    Console.ResetColor();

                                    // Print headers with fixed width padding
                                    Console.WriteLine(
                                        " - " +
                                        "Office".PadRight(15) + " " +
                                        "Model".PadRight(20) + " " +
                                        "Brand".PadRight(15) + " " +
                                        "Purchase Date".PadRight(15) + " " +
                                        "Price".PadRight(15) + " " +
                                        "Currency".PadRight(10) + " " +
                                        "In USD".PadRight(15)
                                    );

                                    // Print a separator line
                                    Console.WriteLine(
                                        "".PadRight(15 + 25 + 20 + 15 + 15 + 10 + 15, '-')
                                    );

                                    foreach (var product in asset.Products.OrderBy(p => p.PurchaseData))
                                    {
                                        double inDollars = DoConversion.PerformConversion(product.Currency, "USD", product.LocalPrice);
                                        double toDollars = Math.Round(inDollars, 1);
                                        formatDate checkDate = new formatDate();
                                        int difference = checkDate.RedDate(checkDate.start, product.PurchaseData);

                                        // Align data for product row
                                        string office = product.Office.PadRight(15);
                                        string model = product.Model.PadRight(20);
                                        string brand = product.Brand.PadRight(15);
                                        string purchaseDate = product.PurchaseData.ToString("yyyy-MM-dd").PadRight(15);
                                        string price = product.LocalPrice.ToString("0.00").PadRight(15);
                                        string currency = product.Currency.PadRight(10);
                                        string dollars = toDollars.ToString("0.00").PadRight(15);

                                        if (difference > 33)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {dollars}");
                                            Console.ResetColor();
                                        }
                                        else if (difference > 30 && difference < 33)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {dollars}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.WriteLine($" - {office} {model} {brand} {purchaseDate} {price} {currency} {dollars}");
                                        }
                                    }
                                }
                                break;

                            case 2:
                                getStatistics();
                                break;

                            case 3:
                                AddProductsMenu();
                                break;

                            case 4:
                                UpdateProductsMenu();
                                break;

                            case 5:
                                DeleteProductsMenu();
                                break;

                            case 6:
                                // Option for quitting
                                Console.WriteLine("Option 4 selected - Quitting");
                                Console.WriteLine("Save and Quit. Goodbye!");
                                Environment.Exit(0);
                                break;

                            default:
                                Console.WriteLine("Invalid option.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: " + ex.Message);
                        Console.ResetColor();
                    }

                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();  // Wait for user to press a key before returning to the menu
                }
            }
        }

        public static void getStatistics()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option - enter a number");
                Console.WriteLine("1. View productcount per Brand");
                Console.WriteLine("2. Get number of products in database");
                Console.WriteLine("3. View productcount per Office");
                Console.WriteLine("9. Return to main menu");

                var userKey = Console.ReadKey(true).Key;

                switch (userKey) {

                    case ConsoleKey.D1:
                        

                        var sortAccordingToBrand = MyDb.Products.GroupBy(p => p.Brand).ToList();
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(">>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<");

                        Console.ForegroundColor= ConsoleColor.DarkCyan;
                        Console.WriteLine("Amount of products of each Brand");
                        Console.ResetColor();

                        foreach (var group in sortAccordingToBrand)
                        {
                            Console.WriteLine($"Brand: {group.Key}, Count: {group.Count()}");
                        }


                        Console.WriteLine("Press any key to return to main menu");
                        Console.Read();
                        break;

                    case ConsoleKey.D2:
                        var totalProductCount = MyDb.Products.Count();

                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(">>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<");
                        

                        Console.WriteLine("Total number of products in database: " + totalProductCount);
                        Console.WriteLine("Press any key to return to main menu");
                        Console.Read();
                        break;

                    case ConsoleKey.D3:

                        var groupByOffice = MyDb.Products.GroupBy(p => p.Office).ToList();
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(">>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Amount of products per office");
                        Console.ResetColor();

                        foreach (var group in groupByOffice)
                        {
                            Console.WriteLine($"Office: {group.Key}, Productcount: {group.Count()}");
                        }

                        Console.WriteLine("Press any key to return to main menu");
                        Console.Read();
                        break;

                    case ConsoleKey.D9:
                        return;
                        break;

                    default:

                        Console.WriteLine("Invalid option");
                        break;
                
                }

            }

        }


        public static void AddProductsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add New Product:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("9. Return to Main Menu");

                var choice = Console.ReadKey(true).Key;

                switch (choice)
                {
                    case ConsoleKey.D1:  // Add a new product
                        Console.WriteLine("Enter product details: ");

                        // Office input
                        string office;
                        do
                        {
                            Console.WriteLine("Enter Office:");
                            office = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(office))
                            {
                                Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                            }
                        } while (string.IsNullOrWhiteSpace(office));

                        // Brand input
                        string brand;
                        do
                        {
                            Console.WriteLine("Enter Brand: ");
                            brand = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(brand))
                            {
                                Console.WriteLine("Brand cannot be empty. Please enter a valid brand.");
                            }
                        } while (string.IsNullOrWhiteSpace(brand));

                        // Model input
                        string model;
                        do
                        {
                            Console.WriteLine("Enter Model:");
                            model = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(model))
                            {
                                Console.WriteLine("Model cannot be empty. Please enter a valid model.");
                            }
                        } while (string.IsNullOrWhiteSpace(model));

                        // Price input
                        double localPrice = 0;
                        string? userInput;
                        do
                        {
                            Console.WriteLine("Enter Price in local currency: ");
                            userInput = Console.ReadLine();
                            if (!double.TryParse(userInput, out localPrice) || localPrice <= 0)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid positive price.");
                            }
                        } while (localPrice <= 0);

                        Console.WriteLine($"The price entered is {localPrice}");

                        // Currency input
                        string currency;
                        do
                        {
                            Console.WriteLine("Enter Currency: ");
                            currency = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(currency))
                            {
                                Console.WriteLine("Currency cannot be empty. Please enter a valid currency.");
                            }
                        } while (string.IsNullOrWhiteSpace(currency));

                        // Purchase Date input
                        DateTime purchaseDate;
                        string dateInput;
                        do
                        {
                            Console.WriteLine("Enter Purchase Date (yyyy-mm-dd): ");
                            dateInput = Console.ReadLine();
                            if (!DateTime.TryParse(dateInput, out purchaseDate))
                            {
                                Console.WriteLine("Invalid date format, please enter a valid date.");
                            }
                        } while (!DateTime.TryParse(dateInput, out purchaseDate));

                        // AssetsId input
                        int assetsId = 0;
                        do
                        {
                            Console.WriteLine("Enter AssetsId (1 for computer and 2 for phone): ");
                            string assetsInput = Console.ReadLine();
                            if (!int.TryParse(assetsInput, out assetsId) || (assetsId != 1 && assetsId != 2))
                            {
                                Console.WriteLine("Invalid AssetsId, please enter 1 for computer or 2 for phone.");
                            }
                        } while (assetsId != 1 && assetsId != 2);

                        // Create new product object
                        Products newProduct = new Products
                        {
                            Office = office,
                            Brand = brand,
                            Model = model,
                            LocalPrice = localPrice,
                            Currency = currency,
                            PurchaseData = purchaseDate,
                            AssetsId = assetsId
                        };

                        // Add error handling for database operations
                        try
                        {
                            var existingProduct = MyDb.Products
                                .FirstOrDefault(p => p.Office == newProduct.Office &&
                                                     p.Brand == newProduct.Brand &&
                                                     p.Model == newProduct.Model &&
                                                     p.AssetsId == newProduct.AssetsId);

                            if (existingProduct == null)
                            {
                                MyDb.Products.Add(newProduct);
                                MyDb.SaveChanges();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Product successfully added to the database!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An identical product already exists in this Office!");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: Unable to save the product. {ex.Message}");
                            Console.ResetColor();
                        }

                        break;

                    case ConsoleKey.D9:  // Return to the main menu
                        Console.WriteLine("Returning to main menu...");
                        return;  // Exits the AddProductsMenu method (breaks out of the loop)

                    default:
                        Console.WriteLine("Invalid option. Please press 1 to add a product or 9 to return to the main menu.");
                        break;
                }

                // Optionally, add a delay or message to let the user know the product was added before they choose again
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }


        public static void UpdateProductsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update a Product:");
                Console.WriteLine("1. Update Product");
                Console.WriteLine("9. Return to Main Menu");

                var choice = Console.ReadKey(true).Key;

                switch (choice)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("We need to search for your product in the database. Enter product details of the product you want to update so we can help you: ");

                        // Office input
                        string office;
                        do
                        {
                            Console.WriteLine("Enter Office:");
                            office = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(office))
                            {
                                Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                            }
                        } while (string.IsNullOrWhiteSpace(office));

                        // Brand input
                        string brand;
                        do
                        {
                            Console.WriteLine("Enter Brand: ");
                            brand = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(brand))
                            {
                                Console.WriteLine("Brand cannot be empty. Please enter a valid brand.");
                            }
                        } while (string.IsNullOrWhiteSpace(brand));

                        // Model input
                        string model;
                        do
                        {
                            Console.WriteLine("Enter Model:");
                            model = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(model))
                            {
                                Console.WriteLine("Model cannot be empty. Please enter a valid model.");
                            }
                        } while (string.IsNullOrWhiteSpace(model));

                        // Purchase Date input
                        DateTime purchaseDate;
                        string userInput;
                        do
                        {
                            Console.WriteLine("Enter Purchase Date (yyyy-mm-dd): ");
                            userInput = Console.ReadLine();
                            if (!DateTime.TryParse(userInput, out purchaseDate))
                            {
                                Console.WriteLine("Invalid date format, please enter a valid date.");
                            }
                        } while (!DateTime.TryParse(userInput, out purchaseDate));

                        // AssetsId input
                        int assetsId = 0;
                        do
                        {
                            Console.WriteLine("Enter assetsId (1 for computer and 2 for phone): ");
                            string assetsInput = Console.ReadLine();
                            if (!int.TryParse(assetsInput, out assetsId) || (assetsId != 1 && assetsId != 2))
                            {
                                Console.WriteLine("Invalid AssetsId, please enter 1 for computer or 2 for phone.");
                            }
                        } while (assetsId != 1 && assetsId != 2);

                        // Search for the product in the database
                        var productFound = MyDb.Products.FirstOrDefault(p => p.Office == office && p.Brand == brand && p.Model == model && p.PurchaseData == purchaseDate && p.AssetsId == assetsId);

                        if (productFound == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The product can not be found in the database");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            // Ask user what property they want to update
                            Console.WriteLine("Which property do you want to change? Type a number");
                            Console.WriteLine("1. Office\n2. Brand\n3. Model\n4. Purchase date\n5. AssetsId");
                            string? userChoice = Console.ReadLine();

                            // Use a switch-case to handle property updates
                            switch (userChoice)
                            {
                                case "1":
                                    string newOffice;
                                    do
                                    {
                                        Console.WriteLine("Which Office shall we update the product to?");
                                        newOffice = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(newOffice))
                                        {
                                            Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                                        }
                                    } while (string.IsNullOrWhiteSpace(newOffice));
                                    productFound.Office = newOffice;
                                    break;

                                case "2":
                                    string newBrand;
                                    do
                                    {
                                        Console.WriteLine("Which brand shall the product have instead?");
                                        newBrand = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(newBrand))
                                        {
                                            Console.WriteLine("Brand cannot be empty. Please enter a valid brand.");
                                        }
                                    } while (string.IsNullOrWhiteSpace(newBrand));
                                    productFound.Brand = newBrand;
                                    break;

                                case "3":
                                    string newModel;
                                    do
                                    {
                                        Console.WriteLine("Which model shall the product have instead?");
                                        newModel = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(newModel))
                                        {
                                            Console.WriteLine("Model cannot be empty. Please enter a valid model.");
                                        }
                                    } while (string.IsNullOrWhiteSpace(newModel));
                                    productFound.Model = newModel;
                                    break;

                                case "4":
                                    DateTime newPurchaseDate;
                                    string userDateInput;
                                    do
                                    {
                                        Console.WriteLine("Which is the correct purchase date? (yyyy-mm-dd)");
                                        userDateInput = Console.ReadLine();
                                        if (!DateTime.TryParse(userDateInput, out newPurchaseDate))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Invalid date format. Please use the format yyyy-mm-dd.");
                                            Console.ResetColor();
                                        }
                                    } while (!DateTime.TryParse(userDateInput, out newPurchaseDate));
                                    productFound.PurchaseData = newPurchaseDate;
                                    break;

                                case "5":
                                    int newAssetsId;
                                    do
                                    {
                                        Console.WriteLine("Which assetsId shall the product have instead?");
                                        string assetsChoice = Console.ReadLine();
                                        if (!int.TryParse(assetsChoice, out newAssetsId) || (newAssetsId != 1 && newAssetsId != 2))
                                        {
                                            Console.WriteLine("Invalid AssetsId, please enter 1 for computer or 2 for phone.");
                                        }
                                    } while (newAssetsId != 1 && newAssetsId != 2);
                                    productFound.AssetsId = newAssetsId;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice. Returning to main menu.");
                                    return;
                            }

                            // After updating the properties, update the database
                            try
                            {
                                MyDb.Products.Update(productFound);
                                MyDb.SaveChanges();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Product updated successfully.");
                                Console.ResetColor();
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Error: Unable to update the product. {ex.Message}");
                                Console.ResetColor();
                            }

                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.ReadKey();
                        }
                        break;

                    case ConsoleKey.D9:
                        Console.WriteLine("Returning to main menu...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please press 1 to update a product or 9 to return to the main menu.");
                        break;
                }
            }
        }



        public static void DeleteProductsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete a Product:");
                Console.WriteLine("1. Delete Product");
                Console.WriteLine("9. Return to Main Menu");

                var choice = Console.ReadKey(true).Key;

                switch (choice)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Enter product details of the product you want to delete: ");

                        // Office input
                        string office;
                        do
                        {
                            Console.WriteLine("Enter Office:");
                            office = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(office))
                            {
                                Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                            }
                        } while (string.IsNullOrWhiteSpace(office));

                        // Brand input
                        string brand;
                        do
                        {
                            Console.WriteLine("Enter Brand: ");
                            brand = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(brand))
                            {
                                Console.WriteLine("Brand cannot be empty. Please enter a valid brand.");
                            }
                        } while (string.IsNullOrWhiteSpace(brand));

                        // Model input
                        string model;
                        do
                        {
                            Console.WriteLine("Enter Model:");
                            model = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(model))
                            {
                                Console.WriteLine("Model cannot be empty. Please enter a valid model.");
                            }
                        } while (string.IsNullOrWhiteSpace(model));

                        // Purchase Date input
                        DateTime dateOfPurchase;
                        string enteredDate;
                        do
                        {
                            Console.WriteLine("Enter Purchase Date (yyyy-mm-dd): ");
                            enteredDate = Console.ReadLine();
                            if (!DateTime.TryParse(enteredDate, out dateOfPurchase))
                            {
                                Console.WriteLine("Invalid date format, please enter a valid date.");
                            }
                        } while (!DateTime.TryParse(enteredDate, out dateOfPurchase));

                        // AssetsId input
                        int assetsId = 0;
                        do
                        {
                            Console.WriteLine("Enter assetsId (1 for computer and 2 for phone): ");
                            string assetsInput = Console.ReadLine();
                            if (!int.TryParse(assetsInput, out assetsId) || (assetsId != 1 && assetsId != 2))
                            {
                                Console.WriteLine("Invalid AssetsId, please enter 1 for computer or 2 for phone.");
                            }
                        } while (assetsId != 1 && assetsId != 2);

                        // Search for the product in the database
                        var productExisting = MyDb.Products.FirstOrDefault(p => p.Office == office && p.Brand == brand && p.Model == model && p.PurchaseData == dateOfPurchase && p.AssetsId == assetsId);

                        if (productExisting == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The product cannot be found in the database.");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            try
                            {
                                // Proceed with product deletion and save changes
                                MyDb.Products.Remove(productExisting);
                                MyDb.SaveChanges();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Product deleted successfully.");
                                Console.ResetColor();
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Error: Unable to delete the product. {ex.Message}");
                                Console.ResetColor();
                            }

                            Console.WriteLine("Press any key to return to the Main Menu...");
                            Console.ReadKey();  // Wait for the user to press a key
                            break;
                        }

                    case ConsoleKey.D9:
                        Console.WriteLine("Returning to main menu...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please press 1 to delete a product or 9 to return to the main menu.");
                        break;
                }
            }
        }


        public static string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }

        // Utility method for getting a valid DateTime input
        private static DateTime GetValidDate()
        {
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Invalid date format, please enter a valid date (yyyy-mm-dd).");
            }
            return date;
        }

        // Utility method for getting a valid integer input
        private static int GetValidInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid number, please enter a valid number.");
            }
            return result;
        }

        static void Main(string[] args)
        {

            try
            {
                // List of AssetTypes to be added
                var products = new List<Products>
{
    // Old Products (Computers - AssetId 1)
    new Products { Office = "New York", Brand = "MacBook", Model = "Pro 16-inch", LocalPrice = 2300, Currency = "EUR", PurchaseData = DateTime.Now.AddYears(-2), AssetsId = 1 },
    new Products { Office = "London", Brand = "Asus", Model = "ZenBook 14", LocalPrice = 1500, Currency = "GBP", PurchaseData = DateTime.Now.AddYears(-1).AddMonths(-6), AssetsId = 1 },
    new Products { Office = "Berlin", Brand = "Lenovo", Model = "ThinkPad X1 Carbon", LocalPrice = 1800, Currency = "EUR", PurchaseData = DateTime.Now.AddMonths(-3), AssetsId = 1 },
    new Products { Office = "Tokyo", Brand = "Apple", Model = "MacBook Air", LocalPrice = 1200, Currency = "JPY", PurchaseData = DateTime.Now.AddYears(-3), AssetsId = 1 },
    new Products { Office = "Paris", Brand = "Dell", Model = "XPS 13", LocalPrice = 1600, Currency = "EUR", PurchaseData = DateTime.Now.AddYears(-2).AddMonths(-6), AssetsId = 1 },
    new Products { Office = "San Francisco", Brand = "HP", Model = "Spectre x360", LocalPrice = 1400, Currency = "USD", PurchaseData = DateTime.Now.AddMonths(-1), AssetsId = 1 },
    new Products { Office = "Madrid", Brand = "Microsoft", Model = "Surface Laptop 3", LocalPrice = 1300, Currency = "EUR", PurchaseData = DateTime.Now.AddMonths(-8), AssetsId = 1 },
    new Products { Office = "Sydney", Brand = "Lenovo", Model = "ThinkPad L15", LocalPrice = 900, Currency = "AUD", PurchaseData = DateTime.Now.AddMonths(-5), AssetsId = 1 },

    // Old Products (Phones - AssetId 2)
    new Products { Office = "New York", Brand = "Apple", Model = "iPhone 13", LocalPrice = 1200, Currency = "USD", PurchaseData = DateTime.Now.AddMonths(-3), AssetsId = 2 },
    new Products { Office = "London", Brand = "Samsung", Model = "Galaxy S21", LocalPrice = 1000, Currency = "GBP", PurchaseData = DateTime.Now.AddYears(-1).AddMonths(-4), AssetsId = 2 },
    new Products { Office = "Berlin", Brand = "Google", Model = "Pixel 6", LocalPrice = 750, Currency = "EUR", PurchaseData = DateTime.Now.AddMonths(-1), AssetsId = 2 },
    new Products { Office = "Tokyo", Brand = "Apple", Model = "iPhone 14", LocalPrice = 1300, Currency = "JPY", PurchaseData = DateTime.Now.AddMonths(-7), AssetsId = 2 },
    new Products { Office = "Paris", Brand = "OnePlus", Model = "OnePlus 9 Pro", LocalPrice = 950, Currency = "EUR", PurchaseData = DateTime.Now.AddMonths(-2), AssetsId = 2 },
    new Products { Office = "San Francisco", Brand = "Google", Model = "Pixel 5", LocalPrice = 700, Currency = "USD", PurchaseData = DateTime.Now.AddMonths(-6), AssetsId = 2 },

    // New Products (Computers - AssetId 1) - Less than 6 months from 3 years ago
    new Products { Office = "New York", Brand = "MacBook", Model = "Pro 16-inch", LocalPrice = 2300, Currency = "EUR", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(5), AssetsId = 1 },
    new Products { Office = "London", Brand = "Asus", Model = "ZenBook 14", LocalPrice = 1500, Currency = "GBP", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(4), AssetsId = 1 },
    new Products { Office = "Paris", Brand = "Dell", Model = "XPS 13", LocalPrice = 1600, Currency = "EUR", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(3), AssetsId = 1 },

    // New Products (Phones - AssetId 2) - Less than 6 months from 3 years ago
    new Products { Office = "Berlin", Brand = "Samsung", Model = "Galaxy S21", LocalPrice = 1000, Currency = "EUR", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(5), AssetsId = 2 },
    new Products { Office = "Tokyo", Brand = "Apple", Model = "iPhone 13", LocalPrice = 1200, Currency = "USD", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(2), AssetsId = 2 },
    new Products { Office = "Madrid", Brand = "Google", Model = "Pixel 5", LocalPrice = 700, Currency = "USD", PurchaseData = DateTime.Now.AddYears(-3).AddMonths(1), AssetsId = 2 }
};


                // Add the products to the Products table
                foreach (var product in products)
                {
                    var existingProduct = MyDb.Products
                        .FirstOrDefault(p => p.Office == product.Office &&
                                             p.Brand == product.Brand &&
                                             p.Model == product.Model &&
                                             p.AssetsId == product.AssetsId);

                    if (existingProduct == null)
                    {
                        MyDb.Products.Add(product); // Add product if it doesn't already exist
                    }
                }


                // Save changes to the database
                MyDb.SaveChanges();



                // Now run the menu command
                MenuCommand();
            }
            catch (Exception ex)
            {
                // Handle the exception by logging or displaying an error message
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Details: " + ex.ToString());
            }

        }






    }
}






    

