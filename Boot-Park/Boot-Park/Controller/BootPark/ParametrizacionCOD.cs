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
        private EtiquetaOAD etiquetas = new EtiquetaOAD();
        private AutorizacionOAD autorizacion = new AutorizacionOAD();
        private UsuarioOAD usuario = new UsuarioOAD();
        private HuellaOAD huella = new HuellaOAD();
        private EtiquetaUsuarioOAD etiquetausuario = new EtiquetaUsuarioOAD();
        private EtiquetaVehiculoOAD etiquetaVehiculo = new EtiquetaVehiculoOAD();
        private CirculacionOAD _circulacion = new CirculacionOAD();
        private General general = new General();

        #region GESTION DE PATICULARES
        public DataTable consultarParticulares()
        {
            return particular.consultarParticulares();
        }

        public bool registrarParticulares()
        {
            return particular.registrarParticulares();
        }

        public bool registrarParticular(string identificacion, string nombre, string apellido, string registradoPor)
        {
            return particular.registrarParticular(general.nextPrimaryKey("BOOTPARK.PARTICULAR", "PART_ID"), identificacion, nombre, apellido, registradoPor);
        }

        public bool actualizarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor)
        {
            return particular.actualizarParticular(id, identificacion, nombre, apellido, registradoPor);
        }

        public bool eliminalParticular(string id)
        {
            return particular.eliminarParticular(id);
        }
        #endregion

        #region AUTORIZACIONADMIN
        public DataTable consultarVehiculosEnUso(string usuario)
        {
            return autorizacion.consultarVehiculosEnUso(usuario);
        }

        public DataTable consultarVehiculosDisponibles()
        {
            return autorizacion.consultarVehiculosStock();
        }

        public bool registrarVehiculoUsuario(string id, string usuario, string pegeId, string descripcion)
        {
            return autorizacion.registrarVehiculoUsuario(id, usuario, pegeId, descripcion, "PROPIETARIO", "DISPONIBLE");
        }
        public bool desvincularVehiculoUsuario(string id, string usuario)
        {
            return autorizacion.desvincularVehiculoUsurio(id, usuario);
        }

        #endregion

        #region AUTORIZACIONPROPIETARIO
        public DataTable consultarVehiculosEnUsoPropietario(string usuario,string particular)
        {
            return autorizacion.consultarVehiculosEnUsoPropietario(usuario,particular);
        }

        public DataTable consultarVehiculosDisponiblesPropietario(string usuario, string particular)
        {
            return autorizacion.consultarVehiculosStockPropietario(usuario,particular);
        }

        public bool registrarVehiculoUsuarioPropietario(string id, string usuario, string pegeId, string descripcion)
        {
            return autorizacion.registrarVehiculoUsuarioPropietario(id, usuario, pegeId, descripcion, "PARTICULAR", "DISPONIBLE");
        }
        public bool desvincularVehiculoUsuarioPropietario(string id, string usuario)
        {
            return autorizacion.desvincularVehiculoUsurio(id, usuario);
        }

        # endregion

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

        /// <summary>
        ///   Consulta todas las etiquetas de la Base de Datos sin importar el estado en que se encuentre
        /// </summary>
        /// <returns></returns>
        public DataTable consultarEtiquetas()
        {
            return etiquetas.consultarEtiquetas();
        }

        /// <summary>
        /// Consulta la existencia del TAG o Tarjeta en la Base de Datos si existe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public DataTable validarEtiqueta(string id, string tipo) {
            return etiquetas.validarEtiqueta(id,tipo);
        }


        /// <summary>
        ///   Permite traer las etiqueras actualmente disponibles en STOCK
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataTable consultarCarnetsDisponibles()
        {
            return etiquetausuario.consultarCarnetsStock();
        }

        /// <summary>
        ///     Permite conocer las etiquetas que actualmente se encuentran en uso.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public DataTable consultarCarnetsEnUso(string usuario)
        {
            return etiquetausuario.consultarCarnetEnUso(usuario);
        }

        public bool registrarEtiquetas()
        {
            return etiquetas.registrarEtoquetas();
        }

        public bool registrarEtiqueta(string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
        {
            return etiquetas.registrarEtiqueta(general.nextPrimaryKey("BOOTPARK.ETIQUETA", "ETIQ_ID"), tipo, etiqueta, descripcion, observacion, estado, registradoPor);
        }

        public bool actualizarEtiqueta(string id, string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
        {
            return etiquetas.actualizarEtiqueta(id, tipo, etiqueta, descripcion, observacion, estado, registradoPor);
        }

        public bool eliminalEtiqueta(string id, string tipo)
        {
            return etiquetas.eliminarEtiqueta(id, tipo);
        }
        #endregion

        #region ETIQUETAUSUARIO

        public DataTable consultarEtiquetaUsuario()
        {
            return null;
        }

        public bool registrarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor)
        {
            return etiquetausuario.registrarEtiquetaUsuario(id, tipo, usuario, motivo, caducidad, registradoPor);
        }

        public bool actualizarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor)
        {
            return etiquetausuario.actualizarEtiquetaUsuario(id, tipo, usuario, motivo, caducidad, registradoPor);
        }

        public bool eliminarEtiquetaUsuario(string id, string tipo, string usuario)
        {
            return etiquetausuario.eliminarEtiquetaUsuario(id, tipo, usuario);
        }

        #endregion
        #region ETIQUETAVEHICULO
        public DataTable consultarEtiquetaDisponible()
        {
            return etiquetaVehiculo.consultarEtiquetaDisponible();
        }
        public DataTable consultarEtiquetaVehiculoEnUso(string vehiculo)
        {
            return etiquetaVehiculo.consultarEtiquetaVehiculoEnUso(vehiculo);
        }
        public bool registrarEtiquetaVehiculo(string etiqueta, string vehiculo, string observacion, string registradoPor) {
            return etiquetaVehiculo.registrarEtiquetaVehiculo(etiqueta,"TAG",vehiculo,observacion,registradoPor);
        }
        public bool eliminarEtiquetaVehiculo(string etiqueta,string vehiculo) {
            return etiquetaVehiculo.eliminarEtiquetaVehiculo(etiqueta,"TAG",vehiculo);
        }
        public bool modificarEtiquetaVehiculo(string etiqueta, string vehiculo, string observacion, string registradoPor) {
            return etiquetaVehiculo.actualizarEtiquetaVehiculo(etiqueta,"TAG",vehiculo,observacion,registradoPor);
        }
        #endregion
        #region USUARIO
        public DataTable consultarUsuarios()
        {
            return usuario.consultarUsuarios();
        }

        public DataTable consultarUsuariosHuellas()
        {
            return usuario.consultarUsuariosHuellas();
        }
        public DataTable consultarUsuariosChaira() {
            return usuario.consultarUsuariosChaira();
        }
        public DataTable consultarUsuarios(string id)
        {
            return usuario.consultarUsuarios(id);
        }
        #endregion
        #region GESTIONAR HUELLA
        public bool registrarHuella(string IdUsuario, int FingerIndex, string Huella, int Flag, int Length, string registradoPor)
        {
            return huella.registraHuella(IdUsuario, FingerIndex, Huella, Flag, Length, registradoPor);
        }
        #endregion



        #region INTEGRACION RFID + BIOMETRICO

        public string ValidarTagAndHuella(string user, string tag, string tipo) {
            return _circulacion.ValidarUsuario(user, tag, tipo);
        }

        #endregion





    }
}