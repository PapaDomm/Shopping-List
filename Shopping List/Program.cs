//Globals
Dictionary<string, decimal> catalog = createDictionary();

bool runProgram = true;

while (runProgram)
{
    List<string> cart = new List<string>();
    List<decimal> cartPrice = new List<decimal>();
    goShopping(catalog, cart, cartPrice);

    Console.WriteLine("\nPress any key....");
    Console.ReadKey();
    Console.Clear();
    Console.Write("Would you like to start a new cart?(Y/N): ");
    string answer  = Console.ReadLine().ToLower().Trim();

    while (answer != "y" && answer != "n")
    {
        Console.WriteLine("\nPlease answer Y or N");
        Console.Write("\nWould you like to start a new cart?: ");
        answer = Console.ReadLine().ToLower().Trim();
    }

    if (answer != "y")
    {
        runProgram = false;
    }
}







































static void goShopping(Dictionary<string, decimal> catalog, List<string> cart, List<decimal> cartPrice)
{
    bool stillShopping = true;

    while (stillShopping)
    {
        Console.Clear();
        displayCatalog(catalog);
        addToCart(catalog, cart, cartPrice);

        stillShopping = askShopper();
    }

    displayCart(catalog, cart, cartPrice);
}






static void addToCart(Dictionary<string, decimal> catalog, List<string> cart, List<decimal> cartPrice)
{
    Console.Write($"\nPlease enter an Item Number 1-{catalog.Count} to add the item to your cart: ");

    int custChoice = -1;
    while (!int.TryParse(Console.ReadLine(), out custChoice) || custChoice < 1 || custChoice > catalog.Count)
    {
        Console.Write($"\nPlease enter a valid Item Number 1-{catalog.Count} to add the item to your cart: ");
    }
    custChoice--;

    Console.WriteLine("\nYou've purchased the following item");
    Console.WriteLine("===============================================");
    Console.WriteLine(String.Format("{0,-11}\t|{1,-22}\t|{2,6}", $"Item {custChoice + 1}", $"{dictIndex(custChoice, catalog)}", $"${catalog[dictIndex(custChoice, catalog)]}"));

    cart.Add(dictIndex(custChoice, catalog));
    cartPrice.Add(catalog[dictIndex(custChoice, catalog)]);
}





static string dictIndex(int num, Dictionary<string, decimal> catalog)
{
    return catalog.ElementAt(num).Key;
}





static void displayCatalog(Dictionary<string, decimal> catalog)
{
    Console.WriteLine(String.Format("{0, -11}\t\t\t|{1, -25}\t\t\t|{2, 15}", "Item Number", "Item Name", "Item Price"));
    Console.WriteLine("=================================================================================================================");
    for (int i = 0; i < catalog.Count; i++)
    {
        Console.WriteLine(String.Format("{0, -11}\t\t\t|{1, -25}\t\t\t|{2, 15}", $"Item {i + 1}", $"{dictIndex(i, catalog)}", $"${catalog[dictIndex(i, catalog)]}"));
    }
}





static Dictionary<string, decimal> createDictionary()
{
    Dictionary<string, decimal> catalog = new Dictionary<string, decimal>();

    catalog.Add("Beef Cutlets", 21.96m);
    catalog.Add("Boneless Chicken Breast", 5.29m);
    catalog.Add("Mexican Rice", 2.49m);
    catalog.Add("Black Beans", 1.39m);
    catalog.Add("Dr. Pepper", 6.49m);
    catalog.Add("Pepsi", 7.99m);
    catalog.Add("Fruit Gushers", 15.91m);
    catalog.Add("Whole Milk", 1.89m);
    catalog.Add("Eggs", 1.79m);
    catalog.Add("Bread", 1.95m);
    catalog.Add("Lays Chips", 2.99m);
    catalog.Add("Doritos", 4.69m);

    return catalog;
}





static bool askShopper()
{
    Console.Write("\nWould you like to add another item to your cart?(Y/N): ");
    string answer = Console.ReadLine().ToLower().Trim();
    while (answer != "y" && answer != "n")
    {
        Console.WriteLine("\nPlease answer Y or N");
        Console.Write("\nWould you like to add another item to your cart?: ");
        answer = Console.ReadLine().ToLower().Trim();
    }

    if (answer == "y")
    {
        return true;
    }
    else
    {
        return false;
    }
}





static void displayCart(Dictionary<string, decimal> catalog, List<string> cart, List<decimal> cartPrice)
{
    decimal total = cartPrice.Sum();
    orderCart(catalog, cart, cartPrice);
    Console.Clear();
    Console.WriteLine(String.Format("{0, -26}\t|{1, 20}", "\nItem Name", "Item Price"));
    Console.WriteLine("======================================================");
    foreach (string item in cart)
    {
        Console.WriteLine(String.Format("{0,-26}\t|{1,20}", $"{item}", $"{catalog[item]}"));
    }
    Console.WriteLine("\n======================================================");
    Console.WriteLine(String.Format("{0, 26}\t{1, 20}", "Your total is:", $"{total}"));
    Console.WriteLine(String.Format("{0, 26}\t{1, 20}", "\nYour most expensive item was:", $"{displayMax(catalog, cart)}"));
    Console.WriteLine(String.Format("{0, 26}\t{1, 20}", "Your least expensive item was:", $"{displayMin(catalog, cart)}"));
}





static string displayMax(Dictionary<string, decimal> catalog, List<string> cart)
{
    decimal highPrice = decimal.MinValue;
    string maxItem = "";
    foreach(string x in cart)
    {
        if (catalog[x] > highPrice)
        {
            highPrice = catalog[x];
            maxItem = x;
        }
    }
    return maxItem;
}





static string displayMin(Dictionary<string, decimal> catalog, List<string> cart)
{
    decimal lowPrice = decimal.MaxValue;
    string minItem = "";
    foreach (string x in cart)
    {
        if (catalog[x] < lowPrice)
        {
            lowPrice = catalog[x];
            minItem = x;
        }
    }
    return minItem;
}





static void orderCart(Dictionary<string, decimal> catalog, List<string> cart, List<decimal> cartPrice)
{
    List<string> holder = new List<string>();
    List<decimal> prices = new List<decimal>();
    int cartLength = cart.Count;

    for (int i = 0; i < cartLength; i++)
    {
        int minPrice = cartPrice.IndexOf(cartPrice.Min());
        holder.Add(cart[minPrice]);
        cart.Remove(cart[minPrice]);
        cartPrice.Remove(cartPrice[minPrice]);
    }

    for (int i = 0;i < cartLength; i++)
    {
        cart.Add(holder[i]);
    }
}