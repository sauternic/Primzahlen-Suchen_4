using System;
using System.Diagnostics;

//Program_Dezimal_nach_ulong_Array_Vorselektion.cs
namespace Primzahlen
{
    class CPrimzahlen
    {
        //Für mehr als 20 Stellen aber nur For

        //Reine Info:
        //27 Stellen :  100000000000000000000000000
        //25 Stellen :  1000000000000000000000000
        //21 Stellen :  100000000000000000000
        //20 Stellen:   10000000000000000000
        //17 Stellen:   10000000000000000
        //16 Stellen:   1000000000000000
        //Max ulong =  18446744073709551615;
        //Max decimal = 79228162514264337593543950335M;

        //Fields
        static ulong zwischenRest;
        static int i;


        static void Main()
        {
            Decimal anfang = 0;
            Decimal ende = 0;

            Console.Write("\n   Program_Dezimal_nach_ulong_Array_Vorselektion\n\n");
            Console.Write("\n\n   Primzahlenauflisten!\n\n");
            Console.Write("\n   Untere Grenze Eingeben? ");
            anfang = Convert.ToDecimal(Console.ReadLine());

            Console.Write("   Obere  Grenze Eingeben? ");
            ende = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();//Leerzeile

            SuchePrimzahlen(anfang, ende);
        }

        //Noch ohne Parallel.For!
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static void SuchePrimzahlen(Decimal anfang, Decimal ende)
        {
            Stopwatch s = new Stopwatch();

            label1:
            //Aeussere Schleife Ersetzt Hand Eingabe.
            for (; anfang <= ende; anfang++)
            {
                s.Start();
                //Orientierung wo gerade ist?
                //Console.WriteLine("Bin hier am Rechnen: {0:#,#}", anfang);


                //Kein Flaschenhals!! :O
                UInt64 Wurzel_anfang = (UInt64)Math.Pow(Convert.ToDouble(anfang), 0.5);



                // Hier wird bei Bedarf in UInt64 Stücke aufgeteilt
                UInt64[] anfang2 = Zerteilen(anfang);



                //Primzahlen Engine! :)
                for (ulong teiler = 2; teiler <= Wurzel_anfang; teiler++)
                {
                    //Vorselektion
                    //if (teiler % 2 != 0 && teiler % 3 != 0 || teiler == 2 || teiler == 3)

                    //2 Einzig Gerade Primzahl darum (... || teiler == 2) gibt true bei 2!!!!
                    //Das ist am Schnellsten nur gerade Zahlen weglassen
                    if (teiler % 2 != 0 || teiler == 2)
                    {
                        //Wenn anfang2 mod teiler == 0 ist keine Primzahl
                        if (Prüfung(anfang2, teiler))
                        {
                            //Schleife erhöht über label1 anfang nicht:
                            ++anfang;
                            goto label1;
                        }
                    }
                }

                //1 und 0 rausputzen! ;)
                if (anfang == 1 || anfang == 0)
                    continue;

                s.Stop();
                TimeSpan timeSpan = s.Elapsed;

                //Ausgabe
                Console.WriteLine("\nPrimzahl! :)  {0:#,#}", anfang);
                Console.WriteLine("TimeVor: {0}h {1}m {2}s {3}ms\n", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            Console.WriteLine("Fertig! :)");
            Console.WriteLine("\n\tCopyright © Nicolas Sauter");
            Console.ReadLine();
        }

        //Kann Modulo auf den zusammengesetzten Wert 
        //der Array Werte berechnen und wenn 0 dann return true
        static bool Prüfung(UInt64[] wert, UInt64 teiler)
        {
            zwischenRest = 0;

            for (i = 0; i < wert.Length; i++)
            {
                zwischenRest = (zwischenRest + wert[i]) % teiler;
            }
            return (zwischenRest == 0);
        }

        //Macht aus decimal ein ulong Array um mit ulong zu Rechnen 
        //z.B. im Primzahlenprogramm(weil viel Schneller!!!)
        static UInt64[] Zerteilen(decimal wert)
        {
            if (wert <= UInt64.MaxValue)
            {
                //Array mit einem Wert
                return new UInt64[] { (ulong)wert };
            }
            else
            {
                decimal i = 2;
                ulong rest;
                decimal teilmenge = 0;

                //Anzahl des Teiler bestimmen bis im ulong bereich nach Teilung von wert
                for (; i < 1000000000; i++)
                {
                    teilmenge = wert / i;
                    if (teilmenge <= UInt64.MaxValue)
                    {
                        break;
                    }
                }
                //Rest
                rest = (ulong)(wert % i);

                ulong[] arr_erg_sum = new ulong[(int)i];

                //Initialisieren Array
                for (int y = 0; y < i; y++)
                {
                    arr_erg_sum[y] = (ulong)teilmenge;
                }
                //Rest am letzten Eintrag dazuzählen
                arr_erg_sum[(arr_erg_sum.Length - 1)] += rest;

                return arr_erg_sum;
            }
        }
    }
}
