using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TolBarad;

namespace Test
{
    class Program
    {
        static TolBarad.TolBarad _TB = new TolBarad.TolBarad("192.168.1.250", "27011");


        static void Main(string[] args)
        {
            var data = _TB.RunRFID();

            string[] etiqueta = data.Split(',');

            string tag = etiqueta[0];
            string antena = etiqueta[1];
           
            Console.WriteLine("Tag: " + tag + " Antena: " + antena);
            Console.Read();
        }
    }
}
