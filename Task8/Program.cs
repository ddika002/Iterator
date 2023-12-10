using System;
using System.Collections.Generic;

// MenuItem class
public class MenuItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Vegetarian { get; set; }
    public double Price { get; set; }

    public MenuItem(string name, string description, bool vegetarian, double price)
    {
        Name = name;
        Description = description;
        Vegetarian = vegetarian;
        Price = price;
    }
}

// Iterator interface
public interface IIterator
{
    bool HasNext();
    object Next();
}

// DinerMenuIterator class
public class DinerMenuIterator : IIterator
{
    private List<MenuItem> menuItems;
    private int position = 0;

    public DinerMenuIterator(List<MenuItem> items)
    {
        menuItems = items;
    }

    public bool HasNext()
    {
        return position < menuItems.Count;
    }

    public object Next()
    {
        object menuItem = menuItems[position];
        position++;
        return menuItem;
    }
}

// Menu interface
public interface IMenu
{
    IIterator CreateIterator();
}

// PancakeHouseMenu class
public class PancakeHouseMenu : IMenu
{
    private List<MenuItem> menuItems;

    public PancakeHouseMenu()
    {
        menuItems = new List<MenuItem>();

        AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", true, 2.99);
        AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99);
        AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries, and blueberry syrup", true, 3.49);
        AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", true, 3.59);
    }

    public void AddItem(string name, string description, bool vegetarian, double price)
    {
        menuItems.Add(new MenuItem(name, description, vegetarian, price));
    }

    public IIterator CreateIterator()
    {
        return new DinerMenuIterator(menuItems);
    }
}

// DinerMenu class
public class DinerMenu : IMenu
{
    private List<MenuItem> menuItems;

    public DinerMenu()
    {
        menuItems = new List<MenuItem>();

        AddItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99);
        AddItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99);
        AddItem("Soup of the day", "Soup of the day, with a side of potato salad", false, 3.29);
        AddItem("Hotdog", "A hot dog, with saurkraut, relish, onions, topped with cheese", false, 3.05);
        AddItem("Steamed Veggies and Brown Rice", "Steamed vegetables over brown rice", true, 3.99);
        AddItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89);
    }

    public void AddItem(string name, string description, bool vegetarian, double price)
    {
        menuItems.Add(new MenuItem(name, description, vegetarian, price));
    }

    public IIterator CreateIterator()
    {
        return new DinerMenuIterator(menuItems);
    }
}

// Waitress class
public class Waitress
{
    private IMenu pancakeMenu;
    private IMenu dinerMenu;

    public Waitress(IMenu pancakeMenu, IMenu dinerMenu)
    {
        this.pancakeMenu = pancakeMenu;
        this.dinerMenu = dinerMenu;
    }

    public void PrintMenu()
    {
        Console.WriteLine("MENU");
        Console.WriteLine("----");

        Console.WriteLine("BREAKFAST");
        PrintMenu(pancakeMenu.CreateIterator());

        Console.WriteLine("\nLUNCH");
        PrintMenu(dinerMenu.CreateIterator());
    }

    private void PrintMenu(IIterator iterator)
    {
        while (iterator.HasNext())
        {
            MenuItem menuItem = (MenuItem)iterator.Next();
            Console.WriteLine($"{menuItem.Name}, {menuItem.Price} -- {menuItem.Description}");
        }
    }
}

class Program
{
    static void Main()
    {
        PancakeHouseMenu pancakeMenu = new PancakeHouseMenu();
        DinerMenu dinerMenu = new DinerMenu();

        Waitress waitress = new Waitress(pancakeMenu, dinerMenu);
        waitress.PrintMenu();
    }
}
