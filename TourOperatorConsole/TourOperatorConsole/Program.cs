using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourOperatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string numClienti, codice, nome, destinazione, cliente;
            string[] tmp;
            int ripeti;
            do
            {
                Console.Clear();
                //chiedo all'utente di inserire un codice cliente
                Console.WriteLine("Inserisci un codice cliente (formato: Lnnn):");
                codice = Console.ReadLine();
                //controllo il formato del codice cliente
                if (!ControlloCodice(codice))
                {
                    Console.WriteLine("Codice cliente errato");
                    Console.ReadLine();
                }
                //l'utente dovrà inserire il codice finchè quest'ultimo non rispetterà il formato stabilito
            } while (!ControlloCodice(codice));

            TourOperator raccolta = new TourOperator(codice);

            do
            {
                Console.Clear();
                //Chiedo all'utente quanti clienti vuole inserire all'interno della raccolta
                Console.WriteLine("Quanti clienti vuoi inserire?");
                numClienti = Console.ReadLine();
                if (!int.TryParse(numClienti, out ripeti))
                {
                    Console.WriteLine("Inserisci un numero");
                    Console.ReadLine();
                }
                //Controllo che l'utente digiti un numero
            } while (!int.TryParse(numClienti, out ripeti));

            try
            {
                //l'utente inserisce le informazioni richieste per ogni cliente
                for (int i = 0; i < ripeti; i++)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Inserisci il nome e la destinazione (formato:  nome:destinazione):");
                        cliente = Console.ReadLine();
                        if (!ControlloCliente(cliente))
                        {
                            Console.WriteLine("Formato incorretto");
                            Console.ReadLine();
                        }
                        //l'utente dovrà inserire le informazioni richieste finchè quest'ultime non rispetteranno il formato stabilito
                    } while (!ControlloCliente(cliente));

                    tmp = cliente.Split(':');
                    nome = tmp[0];
                    destinazione = tmp[1];
                    //aggiungo le informazioni nella raccolta
                    raccolta.add(nome, destinazione);
                }
            }
            catch (Exception ms)
            {
                //nel caso in cui venga raggiunto il limite massimo dei codici verrà generata un'eccezione
                Console.WriteLine("\n" + ms.Message);
                Console.ReadLine();
            }

            Console.Clear();
            //stampa a video dei clienti presenti nella raccolta
            Console.WriteLine("I clienti presenti nella raccolta sono:\n");
            Console.WriteLine(raccolta.ToString());
            Console.ReadLine();
        }

        static bool ControlloCodice(string codice)
        {
            //controllo formato codice (Lnnn)
            string numero = "";
            //controllo se la lunghezza del codice è ugaule a 4 e se la prima lettera è maiuscola
            if(codice.Length != 4 || codice[0] < 'A' || codice[0] > 'Z')
                return false;
            //controllo se le restanti 3 cifre sono dei numeri
            for (int i = 1; i < codice.Length; i++)
                numero += codice[i];
            return int.TryParse(numero, out int valore);
        }

        static bool ControlloCliente(string cliente)
        {
            //controllo formato informazioni del cliente
            string[] tmp;
            //controllo se ci sono spazi
            for (int i = 0; i < cliente.Length; i++)
            {
                if (cliente[i] == ' ')
                    return false;
            }
            tmp = cliente.Split(':');
            //controllo se l'utente ha inserito le informazioni nel formato 'nome:destinazine'
            //controllo che la lunghezza del nome e della destinazione siano almeno maggiori di due
            if (tmp.Length == 2 && tmp[0].Length > 1 && tmp[1].Length > 1)
                return true;
            return false;
        }
    }
}
