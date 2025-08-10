using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneosv2.src.Shared.utils
{
    public class Bienvenida
    {
        public void MensajeBienvenida()
        {
            string[] colores = {"Red","Green", "Yellow", "Blue", "Magenta", "Cyan" };
            ConsoleColor[] consoleColors =
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                ConsoleColor.Blue,
                ConsoleColor.Magenta,
                ConsoleColor.Cyan
            };

                string mensaje = @"
╔═════════════════════════════════════════════════════════╗
║                                                         ║
║      ✨⚽  🎉  BIENVENIDO AL SISTEMA DE TORNEOS  🎉⚽✨ ║
║                                                         ║
║    ╭──────────────────────────────────────────────╮     ║
║    │  🌟 Donde cada partido cuenta...             │     ║
║    │  💪 ¡Forma equipos, crea torneos y gana! 🏆  │     ║
║    ╰──────────────────────────────────────────────╯     ║
║                                                         ║
║                🚀 ¡Comienza tu leyenda hoy! 🚀          ║
║                                                         ║
╚═════════════════════════════════════════════════════════╝
            ";
            for (int i = 0; i < consoleColors.Length; i++)
            {
                Console.Clear();
                Console.ForegroundColor = consoleColors[i];
                Console.WriteLine(mensaje);
                Console.ResetColor();
                Thread.Sleep(1000);
            }
        }
    }
}