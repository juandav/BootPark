using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Boot_Park.Model.BootPark;

namespace Boot_Park.Controller.BootPark
{
    public class ParametrizacionCOD
    {
        ParticularOAD particular = new ParticularOAD();
        VehiculoOAD vehiculo = new VehiculoOAD();
        EtiquetaOAD etiqueta = new EtiquetaOAD();

        #region GESTION DE PATICULARES
        public DataTable consultarParticulares() {
                return particular.consultarParticulares();
            }

            public bool registrarParticulares() {
                return particular.registrarParticulares();
            }

            public bool registrarParticular(string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.registrarParticular();
            }

            public bool actualizarParticular(string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.actualizarParticular();
            }

            public bool eliminalParticular(string id) {
                return particular.eliminarParticular();
            }
        #endregion

        #region GESTION DE VEHICULOS
        public DataTable consultarVehiculos()
        {
            return vehiculo.consultarVehiculos();
        }

        public bool registrarVehiculos()
        {
            return vehiculo.registrarVehiculos();
        }

        public bool registrarVehiculo()
        {
            return vehiculo.registrarVehiculo();
        }

        public bool actualizarVehiculo()
        {
            return vehiculo.actualizarVehiculo();
        }

        public bool eliminalVehiculo()
        {
            return vehiculo.eliminarVehiculo();
        }
        #endregion

        #region GESTION DE ETIQUETAS
            public DataTable consultarEtiquetas()
            {
                return etiqueta.consultarEtiquetas();
            }

            public bool registrarEtiquetas()
            {
                return etiqueta.registrarEtoquetas();
            }

            public bool registrarEtiqueta()
            {
                return etiqueta.registrarEtiqueta();
            }

            public bool actualizarEtiqueta()
            {
                return etiqueta.actualizarEtiqueta();
            }

            public bool eliminalEtiqueta()
            {
                return etiqueta.eliminarEtiqueta();
            }
        #endregion
    }
}