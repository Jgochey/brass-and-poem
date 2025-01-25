using System.Formats.Tar;
using BrassAndPoem;

//create a "products" variable here to include at least five Product instances. Give them appropriate ProductTypeIds.
List<Product> products = new List<Product>
{
    new Product { Name = "Trumpet", Price = 10.00m, ProuctTypeId = 1 },
    new Product { Name = "Trombone", Price = 20.00m, ProuctTypeId = 1 },
    new Product { Name = "Saxophone", Price = 30.00m, ProuctTypeId = 1 },
    new Product { Name = "Poe", Price = 40.00m, ProuctTypeId = 2 },
    new Product { Name = "Longfellow", Price = 50.00m, ProuctTypeId = 2 }
};

//create a "productTypes" variable here with a List of ProductTypes, and add "Brass" and "Poem" types to the List. 
List<ProductType> productTypes = new List<ProductType>
{
    new ProductType { Id = 1, Title = "Brass" },
    new ProductType { Id = 2, Title = "Poem" }
};

//put your greeting here
Console.WriteLine("Welcome to Brass & Poem! Please select an option from the following:");

//implement your loop here
string readresult = null;
bool continueLoop = true;

do
{
    DisplayMenu();

} while (continueLoop == true);


void DisplayMenu()
{
    Console.WriteLine("1. Display All Products.");
    Console.WriteLine("2. Delete an existing Product.");
    Console.WriteLine("3. Add a new Product.");
    Console.WriteLine("4. Update an exisiting Product.");
    Console.WriteLine("5. Exit.");
    readresult = Console.ReadLine();
    switch (readresult)
    {
        case "1":
            DisplayAllProducts(products, productTypes);
            break;
        case "2":
            DeleteProduct(products, productTypes);
            break;
        case "3":
            AddProduct(products, productTypes);
            break;
        case "4":
            UpdateProduct(products, productTypes);
            break;
        case "5":
            Console.WriteLine("Goodbye!");
            continueLoop = false;
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            return;
    }
}


void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    Console.Clear();

    var combinedProducts = products.Join(productTypes,
product => product.ProuctTypeId, productType => productType.Id,
(product, productType) => new { productInfo = $"{product.Name} is available for ${product.Price}", productTypeName = productType.Title });

    int index = 1;
    foreach (var entry in combinedProducts)
    {
        Console.WriteLine($"{index}. {entry.productInfo} - Product Type: {entry.productTypeName}");
        index++;
    }
    Console.WriteLine("Press any key to return to the main menu.");
    Console.ReadKey();
}


void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    string readResult = null;

    Console.Clear();
    int index = 1;
    foreach (var product in products)
    {
        Console.WriteLine($"{index}. {product.Name}");
        index++;
    }
    Console.WriteLine("Which product would you like to remove?");

    try
    {
        readResult = Console.ReadLine();
        int selection = Convert.ToInt32(readResult);
        Console.WriteLine($"{products[selection - 1].Name} removed successfully.");
        products.RemoveAt(selection - 1);
    }
    catch (Exception)
    {
        Console.WriteLine("Invalid entry. Please try again.");
    }

    return;
}


void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    string readResult = null;
    string newName = "";
    decimal newPrice = 0m;
    int newProuctTypeId = 0;
    bool validEntry = false;

    Console.Clear();
    Console.WriteLine("Enter the name of the product you would like to add.");
    readResult = Console.ReadLine();

    do
    {
        if (!string.IsNullOrWhiteSpace(readResult))
        {
            newName = readResult;
            validEntry = true;
        }
        else
        {
            Console.WriteLine("Invalid entry. Please enter a name for the product.");
            readResult = Console.ReadLine();
        }
    } while (validEntry == false);

    validEntry = false;

    do
    {
        Console.WriteLine("Enter the price of the product you would like to add.");
        readResult = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(readResult))
            try
            {
                newPrice = Convert.ToDecimal(readResult);
                validEntry = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid entry. Please try again.");
                readResult = Console.ReadLine();

            }
    } while (validEntry == false);

    validEntry = false;

    do
    {
        Console.WriteLine("Is this product Brass or a Poem?");
        Console.WriteLine("1. Brass");
        Console.WriteLine("2. Poem");
        readResult = Console.ReadLine();

        if (readResult == "1")
        {
            newProuctTypeId = 1;
            validEntry = true;
        }
        else if (readResult == "2")
        {
            newProuctTypeId = 2;
            validEntry = true;
        }
        else
        {
            Console.WriteLine("Invalid entry. Please try again.");
            readResult = Console.ReadLine();
        }
    } while (validEntry == false);

    products.Add(new Product { Name = newName, Price = newPrice, ProuctTypeId = newProuctTypeId });
}


void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    string readResult = null;
    string newName = "";
    decimal newPrice = 0m;
    int newProuctTypeId = 0;
    bool validEntry = false;
    int selection = 0;

    Console.Clear();

    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }

    Console.WriteLine("Which product would you like to update?");
    readResult = Console.ReadLine();

    if (!string.IsNullOrEmpty(readResult))
    {
        do
        {
            selection = Convert.ToInt32(readResult);

            if (selection < 1 || selection > products.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid selection. Please try again.");
                readResult = Console.ReadLine();
            }
            else
            {
                validEntry = true;
            }

        } while (validEntry == false);


        do
        {
            Console.WriteLine("Enter the new name of the product.");
            newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                products[selection - 1].Name = newName;
                validEntry = true;
            }
            else
            {
                Console.WriteLine("Invalid entry. Please try again.");
                readResult = Console.ReadLine();
            }

        } while (validEntry == false);

        validEntry = false;

        do
        {
            Console.WriteLine("Enter the new price of the product.");
            readResult = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(readResult))
                try
                {
                    newPrice = Convert.ToDecimal(readResult);
                    products[selection - 1].Price = newPrice;
                    validEntry = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                    readResult = Console.ReadLine();

                }
        } while (validEntry == false);

        validEntry = false;

        do
        {
            Console.WriteLine("Is this product a Brass or a Poem?");
            Console.WriteLine("1. Brass");
            Console.WriteLine("2. Poem");
            readResult = Console.ReadLine();
            try
            {
                if (readResult == "1")
                {
                    newProuctTypeId = 1;
                }
                else if (readResult == "2")
                {
                    newProuctTypeId = 2;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                    readResult = Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid entry. Please try again.");
                readResult = Console.ReadLine();
            }

            products[selection - 1].ProuctTypeId = newProuctTypeId;
            validEntry = true;

        } while (validEntry == false);

    }
}


// don't move or change this!
public partial class Program { }
