using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Boot_Park.Model.BootPark;
using Boot_Park.Model;

namespace Boot_Park.Controller.BootPark
{
    public class ParametrizacionCOD
    {
        private ParticularOAD particular = new ParticularOAD();
        private VehiculoOAD vehiculo = new VehiculoOAD();
        private EtiquetaOAD etiqueta = new EtiquetaOAD();

        private General general = new General();

        #region GESTION DE PATICULARES
        public DataTable consultarParticulares() {
                return particular.consultarParticulares();
            }

            public bool registrarParticulares() {
                return particular.registrarParticulares();
            }

            public bool registrarParticular(string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.registrarParticular(general.nextPrimaryKey("BOOTPARK.PARTICULAR","PART_ID"), identificacion, nombre, apellido, registradoPor);
            }

            public bool actualizarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.actualizarParticular(id, identificacion, nombre, apellido, registradoPor);
            }

            public bool eliminalParticular(string id) {
                return particular.eliminarParticular(id);
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

        public bool registrarVehiculo(string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            return vehiculo.registrarVehiculo(general.nextPrimaryKey("BOOTPARK.VEHICULO", "VEHI_ID"), observacion, placa, modelo, marca, color, registradoPor);
        }

        public bool actualizarVehiculo(string id, string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            return vehiculo.actualizarVehiculo(id, observacion, placa, modelo, marca, color, registradoPor);
        }

        public bool eliminalVehiculo(string id)
        {
            return vehiculo.eliminarVehiculo(id);
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