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

        public IEnumerable<Escuela> GetListaEvaluacion()
        {
            IEnumerable<Escuela> response;
            if (_diccionario.TryGetValue(LlaveDiccionario.Escuela, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                response = lista.Cast<Escuela>();
            }
            else
            {
                response = null;
            }
            return response;
        }
    }
}