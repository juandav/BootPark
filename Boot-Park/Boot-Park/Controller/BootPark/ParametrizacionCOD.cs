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
        private TerminalOAD terminal = new TerminalOAD();
        private General general = new General();

        #region GESTION DE PARTICULARES
        public DataTable consultarParticulares()
        {
            return particular.consultarParticulares();
        }
             

        public bool registrarParticular(string identificacion, string nombre, string apellido, string registradoPor)
        {
            return particular.registrarParticular(general.nextPrimaryKey("BOOTPARK.PARTICULAR", "PART_ID"), identificacion, nombre, apellido, registradoPor);
        }

        public bool actualizarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor)
        {
            return particular.actualizarParticular(id, identificacion, nombre, apellido, registradoPor);
        }

        public bool eliminarParticular(string id)
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
            return autorizacion.desvincularVehiculoUsuario(id, usuario);
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
        public bool desvincularVehiculoUsuarioPropietario(string idvehiculo, string usuario)
        {
            return autorizacion.desvincularVehiculoUsuario(idvehiculo, usuario);
        }

        # endregion

        #region GESTION DE VEHICULOS
        public DataTable consultarVehiculos()
        {
            return vehiculo.consultarVehiculos();
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

        #region GESTION DE TERMINALES

        public DataTable consultarTerminales() {
            return terminal.consultarTerminales();
        }

        public bool registrarTerminal(string puerto, string ip, string tipo, string registradoPor)
        {
            return terminal.registrarTerminal(general.nextPrimaryKey("BOOTPARK.TERMINAL", "TERM_ID"), puerto, ip, tipo, registradoPor);
        }

        public bool actualizarTerminal(string id,string puerto, string ip, string tipo, string registradoPor)
        {
            return terminal.actualizarTerminal(id, puerto, ip, tipo, registradoPor);
        }

        public bool eliminaTerminal(string id)
        {
            return terminal.eliminarTerminal(id);
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
        public DataTable ConsultarVehiculoTagAsignado(string tag) {
            return etiquetaVehiculo.ConsultarVehiculoTagAsignado(tag);
        }

        public DataTable ConsultarVehiculoCirculacion(string tag) // TRAE EL VEHICULO VALIDADO AL MODULO DE CIRCULACION
        {
            return etiquetaVehiculo.ConsultarVehiculoValidacion(tag);

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

        /// <summary>
        ///     COMPRUEBA DE QUE EL USUARIO ESTE AUTORIZADO EN CHAIRA
        /// </summary>
        public DataTable ValidarUsuario(string user) {
            return _circulacion.ValidarUsuario(user);
        }

        /// <summary>
        ///     VALIDA LA HUELLA O LA TARJETA DE USUARIO
        /// </summary>
        public DataTable ValidarTagAndHuella(string user, string tag, string tipo) {
            return _circulacion.ValidarTagAndHuella(user, tag, tipo);
        }

        public DataTable ComprobarTipoTerminal(string ip, string puerto) {
            return _circulacion.ComprobarTipoTerminal(ip, puerto);
        }

        public DataTable ConsultarUsuarioCirculacion(string user) {
            return _circulacion.ConsultarUsuarioCirculacion(user);
        }

        public bool RegistrarCirculacion(string user, string vehiculo, string tipo) {
            string circId = general.nextPrimaryKey("BOOTPARK.CIRCULACION", "CIRC_ID");
            return _circulacion.RegistrarCirculacion(user, vehiculo, circId, tipo, "SYSTEM");
        }
        public DataTable ConsultarTipoEntradaVehiculo(string vehiculo) {
            return _circulacion.ConsultarTipoEntradaVehiculo(vehiculo);
        }
        #endregion





    }
}