using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> discObjsEscu)
        {
            if (discObjsEscu == null)
                throw new ArgumentNullException(nameof(discObjsEscu));
            _diccionario = discObjsEscu;
        }

        public IEnumerable<Evaluacion> GetListaEvaluacion()
        {
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }
        }
        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluacion();
            return (from Evaluacion ev in listaEvaluaciones
                    where ev.Nota >= 3.0f
                    select ev.Asignatura.Nombre).Distinct(); ;
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetListaEvaluaAsig()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;
                dictaRta.Add(asig, evalsAsig);
            }
            return dictaRta;
        }

        public Dictionary<string, IEnumerable<object>> GetPromeAlumnPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicEvalAsig = GetListaEvaluaAsig();
            foreach (var asigConEval in dicEvalAsig)
            {
                var dummy = from eval in asigConEval.Value
                            group eval by eval.Alumno.UniqueId
                            into grupoEvalAlumno
                            select new
                            {
                                AlimnoId = grupoEvalAlumno.Key,
                                Promedio = grupoEvalAlumno.Average(evaluacion => evaluacion.Nota)
                            };
            }
            return rta;
        }
    }
}