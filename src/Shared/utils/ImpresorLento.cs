using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneosv2.src.Shared.utils
{
    public class ImpresorLento
    {
        public static void Imprimir(String texto, int velocidad = 2)
        {
            foreach (char letra in texto)
            {
                Console.Write(letra);
                Thread.Sleep(velocidad);
            }
            Console.WriteLine();
        }
    }
}