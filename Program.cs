using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            // Printer.Beep(10000, cantidad:1);
            ImpimirCursosEscuela(engine.Escuela);
            Dictionary<int, string> diccionario = new Dictionary<int, string>();
            diccionario.Add(10, "Sebas");
            diccionario.Add(23, "Lorem Ipsom");
            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }
           var dictmp = engine.GetDiccionarioObjetos();
           engine.ImprimirDiccionario(dictmp);
           
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");

            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre}, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
