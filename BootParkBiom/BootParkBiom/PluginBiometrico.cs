using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BootParkBiom
{
    [ComVisibleAttribute(true)]
    [Guid("37CEBA33-0279-4C01-B201-3CB52D3C778F")]
    [ProgId("BootParkBiom.PluginBiometrico")]
    public class PluginBiometrico
    {
        /// <summary>
        ///     Función de prueba para verificar la conexión del plugin
        /// </summary>
        /// <returns>Texto</returns>
        public string TextoPrueba()
        {
            return "Reponde desde el Plugin";
        }

        /// <summary>
        ///   Probando la Funcionalidad del Plugin en el envio por parametro.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Texto</returns>
        public string ParametroPrueba(string texto)
        {
            return texto;
        }

    }
}
