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

        public IEnumerable<Asignatura> GetListaAsignaturas()
        {
            var listaEvaluaciones = GetListaEvaluacion();
            return  (from Evaluacion ev in listaEvaluaciones
                    where ev.Nota >= 3.0f
                    select ev.Asignatura).Distinct(); ; 
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetListaEvaluaAsig(){
            Dictionary<string, IEnumerable<Evaluacion>> dictaRta = new Dictionary<string, IEnumerable<Evaluacion>>();
            return dictaRta;
        }
    }
}