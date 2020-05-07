using System;
using System.Collections.Generic;
using NLog;

namespace hierarchy
{


    public class AnimalComparer : IComparer<Animal>
    {
        public int Compare(Animal a, Animal b)
        {
            return a.name.CompareTo(b.name);

        }
    }
    public class Program
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Main()
        {
            Cat cat1 = new Cat("Fluffy");
            Dog dog1 = new Dog("Tuzik");
            cat1.Speak();
            dog1.Speak();

            var TheAnimals = new List<Animal>
            {
                new Cat("Vaska"),
                new Dog("Mukhtar"),
                cat1,
                dog1
            };

            var comparer = new AnimalComparer();
            TheAnimals.Sort(comparer);
            foreach(var animal in TheAnimals)
            {
                Console.WriteLine(animal.name);
            }


            Dog anotherDog;
            try
            {
                anotherDog = new Dog("kek");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                anotherDog = new Dog("какое-нибудь другое имя");
            }
            Console.WriteLine(anotherDog.name);
        }


    }
    public class LegAttribute : Attribute
    {
        public int count;
        public LegAttribute(int count) { this.count = count; }
    }

    [LegAttribute(4)]
    abstract public class Animal
    {
        public string name;

        public int GetLegCount()
        {
            var type = this.GetType();
            var attributeValue = Attribute.GetCustomAttribute(type, typeof(LegAttribute)) as LegAttribute;
            return attributeValue.count;
        }
        public Animal(string name)
        {
            Program.Logger.Info("Создано новое животное " + name + ".");
            this.name = name;
            if (name == "kek")
            {
                throw new Exception("You can't use this name KEK");
            }
        }
        public void Speak()
        {
            Console.WriteLine("silence");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name) : base(name)
        {

        }
        public new void Speak()
        {
            Console.WriteLine("Meow, my name is " + name + ".");
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name)
        {

        }
        public new void Speak()
        {
            Console.WriteLine("Woof, my name is " + name + ".");
        }

    }

}
