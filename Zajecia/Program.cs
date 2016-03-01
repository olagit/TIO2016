using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//programy pisane są w namespace'ach
namespace Zajecia
{
    //to powstaje przy wybraniu projektu konsolowego
    class Program
    {
        //metoda statyczna programu
        static void Main(string[] args)
        {
            //stworzenie obiektu klasy Person używając nawiasów {}
            Person osoba1 = new Person() {Name = "Jack", Surname = "Janet"};
            //stworzenie obiektu klasy Person używając podejścia Fluent
            Person osoba2 = new Person().SetName("Marc").SetSurName("Kowalsky").SetAge(23);
            //stworzenie obiektu struktury Line
            Line newLine = new Line() {x1 = 2, x2 = 3, y1 = 1, y2 = 4};
            
            //wywołanie gettera właściwości (działa jak zwykle przypisanie przez pobranie wartości
            string nazwisko = osoba1.Surname;
            //wywołanie settera własciwości (działa jak zwykłe przypisanie)
            osoba1.Age = 20;

            //skopiowanie typu wartościowego przez operator =
            Line anotherLine = newLine;
            //zmiana wartości x1 w skopiowanym typie nie spowoduje zmiany wartości x1 w newLine
            Console.WriteLine("newLine.x1 przed zmiana wartosci x1 po skopiowaniu: " + newLine.x1);
            anotherLine.x1 = 10;
            Console.WriteLine("newLine.x1 po zmianie wartosci x1 po skopiowaniu: " + newLine.x1);
            //klucz var zostawia kompilatorowi miejsce na "wnioskowaniu" typu po typie zwracanym
            //kopiujemy referencję do obiektu typu Person
            var osoba3 = osoba1;
            //zmiana na referencji zmieni wartosc obiektu wskazywanego przez wszystkie referencje (osoba1, osoba3)
            Console.WriteLine("Przed zmiana wieku osoba3 na 12 lat, wartosc wieku osoba1: " + osoba1.Age);
            osoba3.Age = 12;
            Console.WriteLine("Po zmianie wieku osoba3 na 12 lat, wartosc wieku osoba1: " + osoba1.Age);
            //to samo co powyżej, tylko z wykorzystaniem metody
            //parametry przekazywane są przez kopiowanie wartości, skopiowana zostanie referencja i cala struktura
            Console.WriteLine("Przed wywołaniem metody Test (zmiana imienia) " + osoba2.Name);
            osoba1.Test(osoba2, newLine);
            Console.WriteLine("Wynik wywołania metody Test (zmiana imienia) " + osoba2.Name);

            //stworzenie obiektu klasy JavaWorker z referencją typu implementowanego interfejsu
            IProgrammer work = new JavaWorker();
            Console.WriteLine("Wynik Code() z interfejsu IProgrammer: " + work.Code());
            //stworzenie obiektu klasy JavaWorker z referencją typu bazowego Worker
            Worker worker = new JavaWorker();
            //wywołanie nadpisanej metody DoWork() spowoduje wywołanie metody z klasy JavaWorker
            //mimo wołania jej na obiekcie "typu bazowego"
            Console.WriteLine("Wynik metody DoWork() na bazowym typie: "+ worker.DoWork());

            //przykłąd typów generycznych - lista
            List<Person> people = new List<Person>();
            //dodawnie do listy
            people.Add(osoba1);
            people.Add(new Person() { Name = "Jack", Surname = "Janet" });
            people.Add(new Person() { Name = "Jill", Surname = "Jill" });
            people.Add(new Person() { Name = "Kate", Surname = "Kowalsky" });
            people.Add(new Person() { Name = "Kate", Surname = "Janicki" });
            people.Add(new Person() { Name = "Marc", Surname = "Kowalsky" });
            people.Add(new Person() { Name = "Leo", Surname = "DaCapri" });

            Console.WriteLine("Ilość osób na liście (iteruje po całej liście) " + people.Count);
            Console.WriteLine("Czy lista ma jakikolwiek element? " + people.Any());

            //przez rachunek lambda (x => x.Surname == "Kowalsky") co oznacza, dla każdego elementu sprawdź taki warunek
            //ToList zamienia nam wynik na Listę
            var kowalskyFamily = people.Where(x => x.Surname == "Kowalsky").ToList();
            Console.WriteLine("Ilość osób z rodziny Kowalsky: " + kowalskyFamily.Count);
            //wyszukanie pierwszej osoby o imieniu Kate (albo wartość domyślna - w większości przypadków null)
            var pers2 = people.FirstOrDefault(x => x.Name == "Kate");
            //to sprawdzanie jest bardzo ważne, bo w przypadku niewystępowania szukanego obiektu, referencja
            //będzie wskazywać na null i dostaniemy wyjątek
            if (pers2 != null)
            {
                Console.WriteLine("Pierwsza znaleziona osoba o imieniu Kate ma nazwisko: " + pers2.Surname);
            }

            //pętla foreach działa na Liście (dla ciekawskich, wszystkie obiekty implementujące IEnumerable)
            Console.WriteLine("Osoby z rodziny Kowalsky:");
            foreach (var obj in kowalskyFamily)
            {
                Console.WriteLine(obj.Name);
            }

            var pers1 = new Person().SetName("Anna").SetSurName("Kozlowsky").SetAge(23);
            //bezpieczna konkatenacja zmiennych typu string z użyciem statycznej metody String.Format
            Console.WriteLine(
                String.Format("Wiek: {0} Imie: {1} Nazwisko: {2}", pers1.Age, pers1.Name, pers1.Surname));
            //mniej bezpieczna konkatenacja z użyciem operatora +
            Console.WriteLine("Wiek: " + pers1.Age + " Imie: " + pers1.Name + " Nazwisko: " + pers1.Surname);

            Console.WriteLine("Naciśnij a lub b");
            //metoda ReadKey() będzie czekać aż użytkownik wciśnie klawisz na klawiaturze (generalnie)
            var tmp = Console.ReadKey();
            //jeśli wciśnięty klawisz nie był klawiszem Escape, wejdź do środka pętli
            while (tmp.Key != ConsoleKey.Escape)
            {
                //złamanie linii po naciśnieciu klawisza
                Console.WriteLine();
                if (tmp.Key == ConsoleKey.A)
                {
                    Console.WriteLine("Nacisnales A");
                }
                if (tmp.Key == ConsoleKey.B)
                {
                    Console.WriteLine("Nacisnales B");
                }
                Console.WriteLine("Naciśnij a lub b");
                //kolejna opcja użytkownika
                tmp = Console.ReadKey();
            }
        }
    }

    //Klasa publiczna
    public class Person
    {
        //pole prywatne (niedostępne spoza klasy)
        private string _name;
        //pole publiczne
        public int Age;

        //Właściwość z domyślnym get/set
        public string Surname { get; set; }
        //Właściwość z własną implementacją get/set
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                //klucz value trzyma wartość która jest przypisywana
                this._name = value;
                //po przypisaniu wartości możemy dalej modyfikować obiekt klasy
                if (this.Age >= 50)
                    this._name = "Sr " + this._name;
            }
        }

        //pusty konstruktor (w momencie zdefiniowania jakiegokolwiek konstruktora z parametrami, domyślny pusty nie jest dodawany do klasy)
        //gdy w klasie nie ma żadnego konstruktora zdefiniowany, dodawany jest w trakcie kompilacji domyslny pusty
        public Person() { }
        //konstruktor kopiujący
        public Person(Person person)
        {
            this.Name = person.Name;
            this.Surname = person.Surname;
        }
        //metody w stylu Fluent ustawiajaca wartosc pola na wartosc przekazana jako parametr i zwracająca siebie (obiekt) dla dalszych modyfikacji
        public Person SetName(string name)
        {
            this._name = name;
            return this;
        }
        public Person SetSurName(string surname)
        {
            this.Surname = surname;
            return this;
        }
        public Person SetAge(int age)
        {
            this.Age = age;
            return this;
        }

        //Test kopiowania referencji
        //Person to klasa (skopiowana zostanie referencja do obiektu)
        //Line to struktura (skopiowany zostanie obiekt)
        public void Test(Person person, Line line)
        {
            //takie przypisanie zmieni nam wartość obiektu na który wskazuje referencja person
            //po wywołaniu takiej metody, obiekt który przekazany był jako 1 parametr będzie miał zmienione pole Name
            person.Name = "Stuart";
            //to nie spowoduje "skasowania" obiektu, tylko od teraz person, które jest skopiowaną referencją
            //nie wskazuje na nic
            person = null;
        }
        //Przykład na drugą wersję metody o syganturze boid Test - tym razem tylko jeden parametr       
        public void Test(Person person)
        {
            //Tak często robi się metody o tej samej syganturze ale zmienionej liczbe parametrów
            this.Test(person, new Line());
        }
    }
    //struktura
    public struct Line
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
    }

    //przykłąd interfejsu, interfejsy definiują metody (ale nie tylko!) które muszą zostac zdefiniowane w klasach
    //które je "dziedziczą" - implementują
    public interface IProgrammer
    {
        string Code();
    }

    //Przykład dziedziczenia w C# (maksymalnie dziedziczenie po 1 klasie, wiele interfejsów)
    public class Worker : Person, IProgrammer
    {
        //klucz virtual pozwala nam na nadpisywanie tak zmodyfikowanych metod w klasach dziedziczących
        public virtual string DoWork()
        {
            return "I'm doing work";
        }

        public string Code()
        {
            return "Java Java Java";
        }
    }

    //kolejne dziedziczenie
    public class JavaWorker : Worker
    {
        //słówko override pozwala nam na nadpisanie metody DoWork() (ponieważ w klasie z której nasza klasa dziedziczy,
        //znajduje się metoda DoWork ze słowem kluczowych virtual
        //to pozwala nam na deklarowanie obiektu jako typu jego klasy bazowej, a wywołanie metody z ostatniej klasy nadpisującej
        public override string DoWork()
        {
            return "I'm doing Java";
        }
    }


}
