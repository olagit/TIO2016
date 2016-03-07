using System;
using System.Linq;
//ten namespace będzie widoczny po dodaniu wsdl (definicji web serwisu - w tym przypadku web serwisu WCF)
//poprzez PPM na References -> Add Service Reference -> wpisanie adresu do wsdl (z końcówką ?wsdl) -> Go
//można wpisać włąsny namespace pod którym dodawany serwis ma być widoczny (na dole w Namespace)
using WcfClient.MyOwnService;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //to będzie dostępne po dodaniu Service Reference
            TestClient testClient = new TestClient();

            //tworzymy obiekt dostępny w WCF Web Service.
            Classroom classr = new Classroom();
            //Typy generyczne takie jak List<> są tłumaczone na tablicę (ten typ obsuguje większość technologii)
            classr.People = new Person[] { new Person() { Score = 2 }, new Person() { Score = 3 } };
            foreach (var pers in classr.People.ToList())
            {
                Console.WriteLine(pers.Score);
            }
            //w tym miejscu dzieje się cała magia. Obiekt Classrom jest wysyłany na serwer, tam jest przetwarzany, a następnie
            //jest zwracany (dzięki popranwej definicji! Można również nic nie zwracać, ale w tym przypadku to nie będzie miało sensu)
            var newClass = testClient.SetScore(classr);
            foreach (var pers in newClass.People.ToList())
            {
                Console.WriteLine(pers.Score);
            }
            //zobaczmy wynik, ReadLine czeka na enter
            Console.ReadLine();
        }
        
    }
}
