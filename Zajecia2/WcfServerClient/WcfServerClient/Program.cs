//więcej o WCF (oficjalna dokumentacja): https://msdn.microsoft.com/en-us/library/dd456779%28v=vs.110%29.aspx

using System;
using System.Collections.Generic;
//to zostało dodane poprzez PPM na References (w widoku Solution Explorer) 
//- > Add Reference -> Assemblies -> Framework -> System.ServiceModel oraz System.ServiceModel.Web
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //poprzez klase Uri definiujemy sobie adres i port na którym nasz serwis będzie wystawiony
            Uri address = new Uri("http://localhost:9009/Test");
            //ServiceHost to specjalny obiekt wcf do obsługi serwisu w stylu self-host
            //typeof() to operator (odwołanie) używany w Refleksji do uzyskania System.Type parametru
            ServiceHost selfHost = new ServiceHost(typeof(Test), address);

            //klauzula try..catch pozwala nam na łapanie i obsługę wyjątków
            try
            {
                //używając AddServiceEndpoint z typem interfejsu oraz WSHttpBinding zleca się ServiceHost
                //aby nasłuchiwał (wystawił endpoint) po protokole Http
                selfHost.AddServiceEndpoint(typeof(ITest), new WSHttpBinding(), "TestServiceEndpoint");
                //warto pozwolić innym ułatwić komunikację z serwisem, rozwiązanie poniżej
                //poprzez wbudowany mechanizm zachowań i ustawienie flagi dla HttpGetEnabled na true
                //nakazuje obiektowi ServiceHost wystawić po Http m.in plik WSDL
                ServiceMetadataBehavior smd = new ServiceMetadataBehavior();
                smd.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smd);

                //Open otwiera połączenie po czym pozwala wykonywać kolejne instrukcje (reszta działa w oddzielnym wątku)
                selfHost.Open();
                Console.WriteLine("Service is running!");
                Console.ReadLine();

                //gdy użytkownik zamknie serwis, Close pozwala na "posprzątanie" wszystkich obiektów natywnych
                //używanych przez ServiceHost wewnątrz siebie
                //więcej o wzorcu IDisposable: https://msdn.microsoft.com/en-us/library/b1yfkh5e%28v=vs.110%29.aspx
                selfHost.Close();
            }
            catch (CommunicationException ex)
            {
                //catch można ustawić w hierarchii. UWAGA gdy typ wyjątku zostanie dopasowany, wyjątek nie odwiedzi już następnych
                //klauzul catch. W szczególności użycie takiej konstrukcji:
                //catch (Exception ex) {}
                //catch (FileNotFoundException ex) {}
                //nie ma najmniejszego sensu, bo wyjątek wejdzie w pierwszą klauzulę
            }
            catch (Exception ex)
            {
                //wypiszmy błąd i poprzez metode Abort wycofajmy obiekt ServiceHost z działania
                Console.WriteLine(ex.Message);
                selfHost.Abort();
            }

        }
    }

    //Więcej o atrybutach: https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx
    //atrybut ServiceContract pozwala WCFowi na potraktowanie interfejsu jako definicji wystawianej usługi
    [ServiceContract]
    public interface ITest
    {
        //atrybut OperationContract pozwala WCFowi na potraktowanie sygnaturki metody jako sygnaturki metody dostępnej w usłudze
        [OperationContract]
        Classroom SetScore(Classroom classroom);
    }

    //interfejs bez implementacji jest pozbawiony sensu...
    public class Test : ITest
    {
        public Classroom SetScore(Classroom classroom)
        {
            foreach (var pers in classroom.People)
            {
                Console.WriteLine(pers.Score);
                pers.Score = 5;
            }
            return classroom;
        }
    }


    public class Person
    {
        //UWAGA, w takim podejściu (bez atrybutów na klasach wystawianych na zewnątrz) wystawione zostaną 
        //tylko PUBLICZNE pola. Tak samo dodanie dodatkowej logiki w set/get spowoduje w tym przypadku
        //niezamierzone wyniki
        private int _score;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Score
        {
            get { return this._score; }
            set { this._score = value + 1; }
        }
    }

    public class Classroom
    {
        public List<Person> People { get; set; }
    }
}
