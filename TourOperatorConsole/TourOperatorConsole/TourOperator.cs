using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourOperatorConsole
{
    public class TourOperator : IDictionary, IContainer
    {
        string nextClientCode;
        Dictionary<IComparable, object> dizionario = new Dictionary<IComparable, object>();

        public TourOperator(string initialClientCode)
        {
            //assegnazione alla variabile 'nextClientCode' del codice digitato dall'utente
            nextClientCode = initialClientCode;
        }

        public void add(string nome, string dest)
        {
            //inizializzazione variabile di tipo cliente
            Client cliente = new Client(nome, dest);
            //aggiunta del cliente nel dizionario
            dizionario.Add(nextClientCode, cliente);
            //incremento valore codice
            NextCode(nextClientCode);
        }

        private void NextCode(string code)
        {
            char lettera = code[0]; //lettera del codice
            const int numLength = 3;
            string numeri = "", tmpNumero = "";
            int numero;

            //numero del codice
            for (int i = 1; i < code.Length; i++)
                numeri += code[i];

            numero = Convert.ToInt32(numeri);
            //incremento la parte numerica del codice che identifica il cliente
            numero++;

            //se il numero è uguale a mille incremento la lettera a quella successiva
            //la parte numerica passa quidi al valore di 000
            if(numero == 1000)
            {
                if (lettera == 'Z')//se il numero è uguale a 1000 e la lettera uguale ad A genero un'eccezione
                    throw new Exception("Limite codici raggiunto");
                lettera++;
                tmpNumero = "000";
            }
            else
            {
                for (int i = 0; i < numLength - numero.ToString().Length; i++)
                    tmpNumero += "0";
                tmpNumero += numero.ToString();
            }

            //assegno alla variabile 'nextClientCode' il codice successivo
            nextClientCode = lettera + tmpNumero;
        }

        public override string ToString()
        {
            //stampo a video i clienti presenti nel dizionario nel formato 'codice:nome:destinazione'
            string clienti = "";
            foreach (var item in dizionario)
            {
                Client c = (Client)item.Value;
                clienti += $"{item.Key}:{c.Name}:{c.Dest}\n";
            }
            return clienti;         
        }

        //implemetazione dell'interfaccia IDictionary
        public void insert(IComparable key, object attribute)
        {
            bool sameCode = false;
            string tmp = (string)attribute;
            string[] dati = tmp.Split(':');

            //creo un oggetto Client a partire dalle informazioni fornite dall'utente
            Client client = new Client(dati[0], dati[1]);
            //creo un oggetto Coppia per poter poi utilizzare il metodo CompareTo
            Coppia coppia = new Coppia((string)key, client); 

            //controllo se esiste già un cliente con lo stesso codice
            foreach (var item in dizionario)
            {
                if (coppia.CompareTo(item.Key) == 0)
                    sameCode = true;
            }
            //se non esiste aggiungo in coda il nuovo cliente
            if (!sameCode)
                dizionario.Add(key, client);
            else
                dizionario[key] = client; //se esiste, lo sostituisco a quello individuato precedentemente nel dizionario
        }

        public object find(IComparable key)
        {
            try
            {
                //cerco il cliente in base al codice fornito dall'utente
                Client c = (Client)dizionario[key];
                return $"{c.Name} : {c.Dest}";
            }
            catch (Exception)
            {
                //nel caso in cui non venga trovato il cliente genero un'eccezione
                throw new Exception("Codice cliente non trovato");
            }
        }
        public object remove(IComparable key)
        {
            try
            {
                //rimuovo il cliente in base al codice fornito dall'utente
                Client c = (Client)dizionario[key];
                dizionario.Remove(key);
                return $"{c.Name} : {c.Dest}";
            }
            catch (Exception)
            {
                //nel caso in cui il cliente non sia presente nel dizionario genero un'eccezione
                throw new Exception("Codice cliente non presente nella raccolta");
            }
        }
        //fine implemetazione dell'interfaccia IDictionary

        //implemetazione dell'interfaccia IContainer
        public bool isEmpty()
        {
            //controllo se il dizionario è vuoto verificando se il numero di elementi presenti è uguale a 0
            if (dizionario.Count == 0)
                return true;
            return false;
        }

        public void makeEmpty()
        {
            //cancello il contenuto del dizionario
            dizionario.Clear();
        }

        public int size()
        {
            //ritorno il numero di elementi presenti nel dizionario
            return dizionario.Count;
        }
        //fine implemetazione dell'interfaccia IContainer


        private class Client
        {
            /* realizzo la classe Client in modo da impostare come 
            attributo del dizionario un oggetto cliente che contenga
            nome e destinazione */

            string name;
            string dest;

            public Client(string aName, string aDest)
            {
                name = aName;
                dest = aDest;
            }

            public string Name { get => name; }
            public string Dest { get => dest; }

        }

        private class Coppia : IComparable
        {
            /* realizzo una classe Coppia che implemementi l'interfaccia IComparable
            così da poter verificare se esistono più clienti con lo stesso codice */
            string code;
            Client client;

            public Coppia(string aCode, Client aClient)
            {
                code = aCode;
                client = aClient;
            }

            public int CompareTo(object obj)
            {
                return code.CompareTo(obj);
            }
        }
    }
}
