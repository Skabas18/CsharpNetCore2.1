using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000, 1000, 1);
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            // Printer.Beep(10000, cantidad:1);
            // ImpimirCursosEscuela(engine.Escuela);
            Dictionary<int, string> diccionario = new Dictionary<int, string>();
            var dictmp = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dictmp, true);
            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evaList = reporteador.GetListaEvaluacion();
            var listaAsig = reporteador.GetListaAsignaturas();
            var listaEvalAsign = reporteador.GetListaEvaluaAsig();
            var listPromedioPorAsignatura = reporteador.GetPromeAlumnPorAsignatura();

            Printer.WriteTitle("Captura de una Evaluacion por consola");
            var newEval = new Evaluacion();
            string nombre, notaString;
            WriteLine("Ingrese el nombre de la evaluación");
            Printer.PressEnter();
            nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El valor del nombre no puede ser vacio");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("Nombre de la evaluación ingresado correctamente");

            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PressEnter();
            notaString = Console.ReadLine();
            if (string.IsNullOrEmpty(notaString))
            {
                throw new ArgumentException("El valor de la nota no puede ser vacio");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notaString);
                    if (newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentException("La nota debe ser entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluación ingresado correctamente");
                }
                catch (ArgumentException e)
                {
                    WriteLine(e.Message);
                    WriteLine("Saliendo del programa");
                }
                catch (Exception)
                {
                    Printer.WriteTitle("El valor de la nota no es un número válido");
                    WriteLine("Saliendo del programa");
                }
            }

        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIO");
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
