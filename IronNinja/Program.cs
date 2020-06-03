using System;
using System.Collections.Generic;

namespace IronNinja
{
    // Create a the following *IConsumable* interface (code provided), that contains properties for Name, Calories, IsSweet, and IsSpicy, and a method for GetInfo()
    public interface IConsumable
    {
        string Name { get; set; }
        int Calories { get; set; }
        bool IsSpicy { get; set; }
        bool IsSweet { get; set; }
        string GetInfo();
    }

    // Refactor the former Food class to implement the IConsumable interface (code provided)


    public class Food : IConsumable
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public bool IsSpicy { get; set; }
        public bool IsSweet { get; set; }
        public string GetInfo()
        {
            return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
        }
        public Food(string name, int calories, bool spicy, bool sweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = spicy;
            IsSweet = sweet;
        }
    }

// Create a Drink class that implements the IConsumable interface. Make sure Drink objects are always sweet.

 public class Drink : IConsumable
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public bool IsSpicy { get; set; }
        public bool IsSweet { get; set; }

        // Implement a GetInfo Method
        public string GetInfo()
        {
            return $"{Name} (Drink).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
        }
        // Add a constructor method
        public Drink(string name, int calories)
        {
            Name = name;
            Calories = calories;
            IsSpicy = false;
            IsSweet = true;
        }
    }

public class Buffet
{
    public List<IConsumable> Menu;

        // Revisit the Buffet class to contain a Menu of IConsumables, and add a few Drinks to the Menu
        public Buffet()
    {
            Menu = new List<IConsumable>()
        {
            new Food("Donut", 800, false, true),
            new Food("Bagel", 600, false, false),
            new Food("Apple", 300, false, true),
            new Food("Carrot", 70, false, false),
            new Food("Pizza", 1200, true, false),
            new Food("Hamburger", 800, false, false),
            new Food("Taco", 350, true, false),
            new Drink("Pepsi", 300),
            new Drink("Coke", 300),
            new Drink("Chocolate Milkshake", 500),

        };
    }

    // build out a Serve method that randomly selects a IConsumable object from the Menu list and returns the IConsumable object
    public IConsumable Serve()
    {
        Random rand = new Random();
        int idx = rand.Next(0, this.Menu.Count);
        return this.Menu[idx];
    }
}

    // Convert Ninja to an abstract class. Child classes of Ninja should determine when they are full, and how they eat - or rather *consume*, as we now have both Food and Drink. (code provided)

    abstract class Ninja
    {
        protected int calorieIntake;
        public List<IConsumable> ConsumptionHistory;
        public Ninja()
        {
            calorieIntake = 0;
            ConsumptionHistory = new List<IConsumable>();
        }
        public abstract bool IsFull { get; }
        public abstract void Consume(IConsumable item);
    }

    // Make a child class of Ninja, for a SweetTooth. A SweetTooth should be "full" at 1500 calories. When a SweetTooth "Consumes":

    class SweetTooth : Ninja
    {
        // provide override for IsFull (Full at 1500 Calories)
        public override bool IsFull
        {
            get { return this.calorieIntake >= 1500; }
        }

        // provide override for Consume
        public override void Consume(IConsumable item)
        {

            // If NOT Full
            if (!this.IsFull)
            {
                // a. adds calorie value to SweetTooth's total calorieIntake (+10 additional calories if the consumable item is "Sweet")
                this.calorieIntake += item.Calories;
                if (item.IsSweet) { this.calorieIntake += 10; }

                // b. adds the randomly selected IConsumable object to SweetTooth's ConsumptionHistory list
                this.ConsumptionHistory.Add(item);

                // c. calls the IConsumable object's GetInfo() method
                Console.WriteLine($"Ninja consumed: {item.GetInfo()}");
            }
            // If Full
            else
            {
                // issues a warning to the console that the SweetTooth is full and cannot eat anymore
                Console.WriteLine("SweetTooth is full and cannot eat any more!");            }
            
        }
    }

    // Make a child class of Ninja, for a SpiceHound. A SpiceHound should be "full" at 1200 calories. When a SpiceHound "Consumes":

    class SpiceHound : Ninja
    {
        // provide override for IsFull (Full at 1200 Calories)
        public override bool IsFull
        {
            get { return this.calorieIntake >= 1200; }
        }

        // provide override for Consume
        public override void Consume(IConsumable item)
        {

            // If NOT Full
            if (!this.IsFull)
            {
                // a. adds calorie value to SpiceHounds's total calorieIntake (-5 additional calories if the consumable item is "Spicy")
                this.calorieIntake += item.Calories;
                if (item.IsSpicy) { this.calorieIntake -= 5; }

                // b. adds the randomly selected IConsumable object to SpiceHound's ConsumptionHistory list
                this.ConsumptionHistory.Add(item);

                // c. calls the IConsumable object's GetInfo() method
                Console.WriteLine($"Ninja consumed: {item.GetInfo()}");
            }
            // If Full
            else
            {
                // issues a warning to the console that the SpiceHound is full and cannot eat anymore
                Console.WriteLine("SpiceHound is full and cannot eat any more!");
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // In your Program's Main method: instantiate a Buffet, a SweetTooth, and a SpiceHound
            Buffet GoldenCorral = new Buffet();
            SweetTooth LeeAnn = new SweetTooth();
            SpiceHound John = new SpiceHound();

            // In your Program's Main method: have both the SweetTooth and Spice hound "Consume" from the Buffet until Full.
            while (!LeeAnn.IsFull)
            {
                LeeAnn.Consume(GoldenCorral.Serve());
            }
            Console.WriteLine("LeeAnn is full and cannot eat another bite!");
            while (!John.IsFull)
            {
                John.Consume(GoldenCorral.Serve());
            }
            Console.WriteLine("John is full and cannot eat another bite!");

            // In your Program's Main method: write to the console which of the two consumed the most items and the number of items consumed.
            int ItemsJohnConsumed = John.ConsumptionHistory.Count;
            int ItemsLeeAnnConsumed = LeeAnn.ConsumptionHistory.Count;

            if (ItemsJohnConsumed > ItemsLeeAnnConsumed)
            {
                Console.WriteLine($"John consumed more items than LeeAnn. He ate {ItemsJohnConsumed} items");
            }
            else if (ItemsJohnConsumed < ItemsLeeAnnConsumed)
            {
                Console.WriteLine($"LeeAnn consumed more items than John. She ate {ItemsLeeAnnConsumed} items");
            }
            else
            {
                Console.WriteLine($"John and LeeAnn both ate {ItemsLeeAnnConsumed} items each.");
            }

        }
    }
}
