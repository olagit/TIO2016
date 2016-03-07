using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//programy pisane są w namespace'ach
namespace Zajecia
{
    //to powstaje przy wybraniu projektu konsolowego
    public abstract class Animal
    {
        // properties
        public string Name { get; set; }
        public float Weight { get; set; }
        public bool HaveFur { get; set; }

        public abstract string Sound();
        public abstract string Trick();
        public abstract int CountLegs();
    }

    public class Circus : ICircus
    {
        public List<Animal> Animals = new List<Animal>();
        public string Name { get; set; }

        public Circus(Animal a)
        {
            Animals.Add(a);
        }
        public Circus(Circus c)
        {
            Animals = c.Animals;
        }
        public Circus(string name)
            : this()
        {
            Name = name;
        }

        public Circus()
        {
            Animals.Add(new Pony("Rosynant"));
            Animals.Add(new Pony("Sekretatiat"));
            Animals.Add(new Ant("Velvet"));
            Animals.Add(new Elephant("Watermelon"));
            Animals.Add(new Giraffe("SkyAchiever"));
            Animals.Add(new Cat("Cat in shoes"));
            Animals.Add(new Giraffe("Observer"));
            Animals.Add(new Cat("Filemon"));
        }

        public Circus SetName(string name)
        {
            this.Name = name;
            return this;
        }

        public string AnimalsIntroduction()
        {
            string soundAssembling = "";
            foreach (var A in Animals)
            {
                soundAssembling = String.Format(soundAssembling, A.Sound());
            }
            return soundAssembling;
        }

        public int Patter(int howMuch)
        {
            int result = 0;
            foreach (var A in Animals)
            {
                result += A.CountLegs() * howMuch;
            }
            return result;
        }

        public string ShowTricks()
        {
            string trickAssembling = "";
            string trickAssembling2 = "";

            foreach (var A in Animals)
            {
                //trickAssembling = String.Format("{0}{1}", trickAssembling, A.Trick());
                trickAssembling2 = String.Join("\n", new String[] { trickAssembling2, A.Trick() });
            }
            return trickAssembling2;
        }

        public void ShowPresentation()
        {
            Console.WriteLine("Names of animals in {0} circous", Name);
            foreach (var A in Animals)
            {
                Console.WriteLine(A.Name);
            }
        }
    }

    public class Zoo : IZoo
    {
        public List<Animal> Animals = new List<Animal>();
        public string Name { get; set; }

        public Zoo()
        {
            Animals.Add(new Ant("Velvet"));
            Animals.Add(new Elephant("Watermelon"));
            Animals.Add(new Pony("Rosynant"));
            Animals.Add(new Pony("Sekretatiat"));
            Animals.Add(new Giraffe("SkyAchiever"));
            Animals.Add(new Cat("Cat in shoes"));
            Animals.Add(new Giraffe("Observer"));
            Animals.Add(new Cat("Filemon"));
        }


        public string Sounds()
        {
            string soundAssembling = "";
            foreach (var A in Animals)
            {
                soundAssembling = String.Join("\n", new string[] { soundAssembling, A.Sound() });
            }

            return soundAssembling;
        }

        public Animal FirstWithFur()
        {
            Animal animal = Animals.FirstOrDefault(x => x.HaveFur == true);
            return animal;
        }
    }

    public class Cat : Animal
    {
        string Color { get; set; }


        public Cat()
        {
            Name = "cat";
            HaveFur = true;
        }

        public Cat(string name)
            : this()
        {
            this.Name = name;
        }

        public override int CountLegs()
        {
            return 2;
        }

        public override string Sound()
        {
            return "miau";
        }

        public override string Trick()
        {
            return "Standing on 2 legs";
        }
    }

    public class Pony : Animal
    {
        bool IsMagic { get; set; }

        public Pony()
        {
            Name = "pony";
            HaveFur = true;
        }
        public Pony(string name)
            : this()
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "ihhaa";
        }

        public override string Trick()
        {
            return "Jump over a car";
        }
    }

    public class Ant : Animal
    {
        bool IsQueen;

        public Ant()
        {
            Name = "ant";
            HaveFur = false;
        }

        public Ant(string name)
            : this()
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 8;
        }

        public override string Sound()
        {
            return "szsz";
        }

        public override string Trick()
        {
            return "Doing somersaults";
        }
    }

    public class Elephant : Animal
    {

        public Elephant()
        {
            Name = "elephant";
            HaveFur = false;
        }

        public Elephant(string name)
            : this()
        {
            Name = name;
        }
        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "truu";
        }

        public override string Trick()
        {
            return "Eating hundreds kilograms of watermelons";
        }
    }

    public class Giraffe : Animal
    {
        public Giraffe()
        {
            Name = "giraffe";
            HaveFur = false;
        }
        public Giraffe(string name)
            : this()
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "bzzz";
        }

        public override string Trick()
        {
            return "Playing volleyball";
        }
    }

    public interface ICircus
    {
        string AnimalsIntroduction();
        string ShowTricks();
        int Patter(int howMuch);
    }

    public interface IZoo
    {
        string Sounds();
    }

    class Program
    {

        static void Main(string[] args)
        {
            Circus Cyrk1 = new Circus("Krakowski");
            //Console.WriteLine(Cyrk1.Show());
            //Console.ReadLine();
            //Circus Cyrk2 = new Circus().SetName("Poznański");
            //Circus Cyrk3 = new Circus() { Name = "Krynicki" };

            //Console.WriteLine("Cyrk1 is called {0}", Cyrk1.Name);
            //Console.WriteLine("Cyrk2 is called {0}", Cyrk2.Name);
            //Console.WriteLine("Cyrk3 is called {0}", Cyrk3.Name);


            Zoo Z1 = new Zoo() { Name = "Wrocławskie" };
            //Zoo Z2 = new Zoo() { Name = "Krakowskie" };
            //Zoo Z3 = new Zoo() { Name = "Białostockie" };
            //Console.WriteLine("First found animal with fur {0}", Z1.FirstWithFur().Name);

            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Press 'a' to show Animals in {0} circous", Cyrk1.Name);
                Console.WriteLine("Press 'b' to show Program of  {0} circous", Cyrk1.Name);
                Console.WriteLine("Press 'c' to listen Sounds in {0} zoo", Z1.Name);
                Console.WriteLine("Press 'd' to show First Found animal with fur in {0} zoo", Z1.Name);
                Console.WriteLine("Press 'e' to show Names in {0} zoo", Z1.Name);
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.A)
                {
                    Cyrk1.ShowPresentation();
                }
                else if (key.Key == ConsoleKey.B)
                {
                    Console.WriteLine(Cyrk1.ShowTricks());
                }
                else if (key.Key == ConsoleKey.C)
                {
                    Console.WriteLine(Z1.Sounds());
                }
                else if (key.Key == ConsoleKey.D)
                {
                    Console.WriteLine(Z1.FirstWithFur().Name);
                }
                else if (key.Key == ConsoleKey.E)
                {
                    foreach (var A in Z1.Animals)
                    {
                        Console.WriteLine(A.Name);
                    }
                }
            }
        }
    }

}
