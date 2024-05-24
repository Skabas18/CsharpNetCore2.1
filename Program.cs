using System;
using System.Collections.Generic;
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

            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.WriteTitle("Pruebas de Poliformismo");

            var alumnoTest = new Alumno{ Nombre = "Claire UnderWood"};
            ObjetoEscuelaBase ob = alumnoTest;

            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"ID: {alumnoTest.UniqueId}");
            WriteLine($"Type: {alumnoTest.GetType()}");


            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"ID: {ob.UniqueId}");
            WriteLine($"Type: {ob.GetType()}");

            var objDummy = new ObjetoEscuelaBase(){Nombre = "Frank Underwood"};
            Printer.WriteTitle("ObjetoEscuelaBase");
            WriteLine($"Alumno: {objDummy.Nombre}");
            WriteLine($"Alumno: {objDummy.UniqueId}");
            WriteLine($"Alumno: {objDummy.GetType()}");

            var evaluacion =  new Evaluacion() { Nombre = "Evaluacion de math" , Nota= 4.5f};
            Printer.WriteTitle("Evaluacion");
            WriteLine($"evaluacion: {evaluacion.Nombre}");
            WriteLine($"evaluacion: {evaluacion.UniqueId}");
            WriteLine($"evaluacion: {evaluacion.Nota}");
            WriteLine($"evaluacion: {evaluacion.GetType()}");

            ob = evaluacion;
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"ID: {ob.UniqueId}");
            WriteLine($"Type: {ob.GetType()}");

        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            
            Printer.WriteTitle("Cursos de la Escuela");
            
            
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }

        
    }
}
