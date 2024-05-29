using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine() { }

        public void Inicializar()
        {
            Escuela = new Escuela(
                "Platzi Academy",
                2012,
                TiposEscuela.Primaria,
                ciudad: "Bogotá",
                pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();
            diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());
            var listaTmp = new List<Evaluacion>();
            var listaTmpAsignatura = new List<Asignatura>();
            var listaTmpAlumno = new List<Alumno>();

            foreach (var curso in Escuela.Cursos)
            {
                listaTmpAsignatura.AddRange(curso.Asignaturas);
                listaTmpAlumno.AddRange(curso.Alumnos);
                foreach (var alumno in curso.Alumnos)
                {
                    listaTmp.AddRange(alumno.Evaluaciones);
                }
            }
            diccionario.Add(
                LlaveDiccionario.Asignatura,
                listaTmpAsignatura.Cast<ObjetoEscuelaBase>()
            );
            diccionario.Add(LlaveDiccionario.Alumno, listaTmpAlumno.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Evaluacion, listaTmp.Cast<ObjetoEscuelaBase>());

            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool treaCursos = true
        )
        {
            return GetObjetoEscuelas(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluacioines,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool treaCursos = true
        )
        {
            return GetObjetoEscuelas(out conteoEvaluacioines, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluacioines,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool treaCursos = true
        )
        {
            return GetObjetoEscuelas(
                out conteoEvaluacioines,
                out conteoCursos,
                out int dummy,
                out dummy
            );
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluacioines,
            out int conteoCursos,
            out int conteoAsignaturas,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool treaCursos = true
        )
        {
            return GetObjetoEscuelas(
                out conteoEvaluacioines,
                out conteoCursos,
                out conteoAsignaturas,
                out int dummy
            );
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluacioines,
            out int conteoAlumnos,
            out int conteoAsignaturas,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool treaCursos = true
        )
        {
            conteoEvaluacioines = conteoAsignaturas = conteoAlumnos = 0;
            var listaObje = new List<ObjetoEscuelaBase>();
            listaObje.Add(Escuela);
            if (treaCursos)
                listaObje.AddRange(Escuela.Cursos);
            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;
                if (traeAsignaturas)
                    listaObje.AddRange(curso.Asignaturas);
                if (traeAlumnos)
                    listaObje.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObje.AddRange(alumno.Evaluaciones);
                        conteoEvaluacioines += alumno.Evaluaciones.Count;
                    }
                }
            }
            return listaObje.AsReadOnly();
        }

        #region Metodos de carga
        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>()
                {
                    new Asignatura { Nombre = "Matemáticas" },
                    new Asignatura { Nombre = "Educación Física" },
                    new Asignatura { Nombre = "Castellano" },
                    new Asignatura { Nombre = "Ciencias Naturales" }
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 =
            {
                "Alba",
                "Felipa",
                "Eusebio",
                "Farid",
                "Donald",
                "Alvaro",
                "Nicolás"
            };
            string[] apellido1 =
            {
                "Ruiz",
                "Sarmiento",
                "Uribe",
                "Maduro",
                "Trump",
                "Toledo",
                "Herrera"
            };
            string[] nombre2 =
            {
                "Freddy",
                "Anabel",
                "Rick",
                "Murty",
                "Silvana",
                "Diomedes",
                "Nicomedes",
                "Teodoro"
            };

            var listaAlumnos =
                from n1 in nombre1
                from n2 in nombre2
                from a1 in apellido1
                select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>()
            {
                new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso { Nombre = "301", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "401", Jornada = TiposJornada.Tarde },
                new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde },
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion
    }
}
