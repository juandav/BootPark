
CREATE TABLE IF NOT EXISTS `autorizacion` (
  `USUA_ID` varchar(30) NOT NULL,
  `VEHI_ID` int(30) NOT NULL,
  `AUTO_REGISTRADOPOR` varchar(30) DEFAULT NULL,
  `AUTO_FECHACAMBIO` date DEFAULT NULL,
  `AUTO_DESCRIPCION` longtext,
  `AUTO_FECHAAUTORIZACION` date NOT NULL,
  `AUTO_TIPO` varchar(30) NOT NULL,
  `AUTO_ESTADO` enum('DISPONIBLE','INACTIVO') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla bootpark.vehiculo
CREATE TABLE IF NOT EXISTS `vehiculo` (
  `VEHI_ID` int(30) NOT NULL,
  `VEHI_OBSERVACION` longtext NOT NULL,
  `VEHI_PLACA` varchar(30) NOT NULL,
  `VEHI_MODELO` int(4) NOT NULL,
  `VEHI_MARCA` varchar(30) NOT NULL,
  `VEHI_COLOR` varchar(30) NOT NULL,
  `VEHI_REGISTRADOPOR` varchar(30) NOT NULL,
  `VEHI_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Contiene información de los vehiculos que se registraron para el ingreso y salida de la institución.';


