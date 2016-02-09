-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.10 - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2016-02-09 18:17:15
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for bootpark
CREATE DATABASE IF NOT EXISTS `bootpark` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `bootpark`;


-- Dumping structure for table bootpark.autorizacion
CREATE TABLE IF NOT EXISTS `autorizacion` (
  `VEHI_ID` decimal(30,0) NOT NULL,
  `USUA_ID` decimal(30,0) NOT NULL,
  `AUTO_DESCRIPCION` varchar(4000) NOT NULL,
  `AUTO_FECHAAUTORIZACION` date NOT NULL,
  `AUTO_TIPO` varchar(30) NOT NULL,
  `AUTO_ESTADO` varchar(30) NOT NULL,
  `AUTO_REGISTRADOPOR` varchar(30) NOT NULL,
  `AUTO_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`,`USUA_ID`),
  KEY `AUTV` (`VEHI_ID`),
  CONSTRAINT `AUTV` FOREIGN KEY (`VEHI_ID`) REFERENCES `vehiculo` (`VEHI_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.autorizacion: ~0 rows (approximately)
/*!40000 ALTER TABLE `autorizacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `autorizacion` ENABLE KEYS */;


-- Dumping structure for table bootpark.circulacion
CREATE TABLE IF NOT EXISTS `circulacion` (
  `CIRC_ID` decimal(30,0) NOT NULL,
  `CIRC_TIPO` varchar(30) NOT NULL,
  `CIRC_OBSERVACION` varchar(4000) NOT NULL,
  `CIRC_FECHACIRCULA` date NOT NULL,
  `CIRC_REGISTRADOPOR` varchar(30) NOT NULL,
  `CIRC_FECHACAMBIO` date NOT NULL,
  `VEHI_ID` decimal(30,0) NOT NULL,
  `USUA_ID` decimal(30,0) NOT NULL,
  `TERM_ID` int(10) NOT NULL,
  PRIMARY KEY (`CIRC_ID`),
  KEY `CIRA` (`VEHI_ID`,`USUA_ID`),
  KEY `TERC` (`TERM_ID`),
  CONSTRAINT `CIRA` FOREIGN KEY (`VEHI_ID`, `USUA_ID`) REFERENCES `autorizacion` (`VEHI_ID`, `USUA_ID`),
  CONSTRAINT `TERC` FOREIGN KEY (`TERM_ID`) REFERENCES `terminal` (`TERM_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.circulacion: ~0 rows (approximately)
/*!40000 ALTER TABLE `circulacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `circulacion` ENABLE KEYS */;


-- Dumping structure for table bootpark.etiqueta
CREATE TABLE IF NOT EXISTS `etiqueta` (
  `ETIQ_ID` decimal(30,0) NOT NULL,
  `ETIQ_TIPO` varchar(30) NOT NULL,
  `ETIQ_ETIQUETA` varchar(4000) NOT NULL,
  `ETIQ_DESCRIPCION` varchar(4000) NOT NULL,
  `ETIQ_OBSERVACION` varchar(4000) NOT NULL,
  `ETIQ_ESTADO` varchar(30) NOT NULL,
  `ETIQ_REGISTRADOPOR` varchar(30) NOT NULL,
  `ETIQ_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`ETIQ_ID`,`ETIQ_TIPO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.etiqueta: ~2 rows (approximately)
/*!40000 ALTER TABLE `etiqueta` DISABLE KEYS */;
INSERT INTO `etiqueta` (`ETIQ_ID`, `ETIQ_TIPO`, `ETIQ_ETIQUETA`, `ETIQ_DESCRIPCION`, `ETIQ_OBSERVACION`, `ETIQ_ESTADO`, `ETIQ_REGISTRADOPOR`, `ETIQ_FECHACAMBIO`) VALUES
	(1, 'CARNET', '12343434', 'vfv', 'fvfdv', 'DISPONIBLE', '53233', '2016-02-09'),
	(2, 'TAG', '2435ere', 'df', 'df', 'DISPONIBLE', '53233', '2016-02-09');
/*!40000 ALTER TABLE `etiqueta` ENABLE KEYS */;


-- Dumping structure for table bootpark.etiquetausuario
CREATE TABLE IF NOT EXISTS `etiquetausuario` (
  `ETIQ_ID` decimal(30,0) NOT NULL,
  `ETIQ_TIPO` varchar(30) NOT NULL,
  `USUA_ID` decimal(30,0) NOT NULL,
  `ETUS_MOTIVO` varchar(4000) NOT NULL,
  `ETUS_FECHACADUCIDAD` date NOT NULL,
  `ETUS_REGISTRADOPOR` varchar(30) NOT NULL,
  `ETUS_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`ETIQ_ID`,`ETIQ_TIPO`,`USUA_ID`),
  KEY `ETIU` (`ETIQ_ID`,`ETIQ_TIPO`),
  CONSTRAINT `ETIU` FOREIGN KEY (`ETIQ_ID`, `ETIQ_TIPO`) REFERENCES `etiqueta` (`ETIQ_ID`, `ETIQ_TIPO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.etiquetausuario: ~0 rows (approximately)
/*!40000 ALTER TABLE `etiquetausuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `etiquetausuario` ENABLE KEYS */;


-- Dumping structure for table bootpark.etiquetavehiculo
CREATE TABLE IF NOT EXISTS `etiquetavehiculo` (
  `VEHI_ID` decimal(30,0) NOT NULL,
  `ETIQ_ID` decimal(30,0) NOT NULL,
  `ETIQ_TIPO` varchar(30) NOT NULL,
  `ETVE_OBSERVACION` varchar(4000) NOT NULL,
  `ETVE_REGISTRADOPOR` varchar(30) NOT NULL,
  `ETVE_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`,`ETIQ_ID`,`ETIQ_TIPO`),
  KEY `ETIV` (`ETIQ_ID`,`ETIQ_TIPO`),
  KEY `VEHE` (`VEHI_ID`),
  CONSTRAINT `ETIV` FOREIGN KEY (`ETIQ_ID`, `ETIQ_TIPO`) REFERENCES `etiqueta` (`ETIQ_ID`, `ETIQ_TIPO`),
  CONSTRAINT `VEHE` FOREIGN KEY (`VEHI_ID`) REFERENCES `vehiculo` (`VEHI_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.etiquetavehiculo: ~0 rows (approximately)
/*!40000 ALTER TABLE `etiquetavehiculo` DISABLE KEYS */;
/*!40000 ALTER TABLE `etiquetavehiculo` ENABLE KEYS */;


-- Dumping structure for table bootpark.huella
CREATE TABLE IF NOT EXISTS `huella` (
  `HUEL_ID` decimal(30,0) NOT NULL,
  `USUA_ID` decimal(30,0) NOT NULL,
  `HUEL_DEDO` varchar(30) NOT NULL,
  `HUEL_HUELLA` varchar(4000) NOT NULL,
  `HUEL_FLAG` varchar(4000) NOT NULL,
  `HUEL_FINGERINDEX` varchar(4000) NOT NULL,
  `HUEL_LENGTH` varchar(30) NOT NULL,
  `HUEL_REGISTRADOPOR` varchar(30) NOT NULL,
  `HUEL_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`HUEL_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.huella: ~0 rows (approximately)
/*!40000 ALTER TABLE `huella` DISABLE KEYS */;
/*!40000 ALTER TABLE `huella` ENABLE KEYS */;


-- Dumping structure for table bootpark.marcavehiculo
CREATE TABLE IF NOT EXISTS `marcavehiculo` (
  `MAVE_ID` int(10) NOT NULL AUTO_INCREMENT,
  `MAVE_MARCA` varchar(30) NOT NULL,
  PRIMARY KEY (`MAVE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.marcavehiculo: ~29 rows (approximately)
/*!40000 ALTER TABLE `marcavehiculo` DISABLE KEYS */;
INSERT INTO `marcavehiculo` (`MAVE_ID`, `MAVE_MARCA`) VALUES
	(1, 'Volkswagen'),
	(2, 'Chevrolet'),
	(3, 'Nissan'),
	(4, 'Kia'),
	(5, 'Toyota'),
	(6, 'Hyundai'),
	(7, 'Merecedez benz'),
	(8, 'Renault'),
	(9, 'Honda'),
	(10, 'Suzuki'),
	(11, 'Lifan'),
	(12, 'Bmw'),
	(13, 'Ford'),
	(14, 'Ssangyong'),
	(15, 'Mazda'),
	(16, 'Dongfeng'),
	(17, 'Mitsubishi'),
	(18, 'Chery'),
	(19, 'Fiat'),
	(20, 'Jeep'),
	(21, 'Citroen'),
	(22, 'Jac'),
	(23, 'Sabaru'),
	(24, 'Mg'),
	(25, 'Land rover'),
	(26, 'Audi'),
	(27, 'Dodge'),
	(28, 'Peugeot'),
	(29, 'Otra marca');
/*!40000 ALTER TABLE `marcavehiculo` ENABLE KEYS */;


-- Dumping structure for table bootpark.particular
CREATE TABLE IF NOT EXISTS `particular` (
  `PART_ID` int(30) NOT NULL,
  `PART_IDENTIFICACION` int(30) NOT NULL,
  `PART_NOMBRE` varchar(100) NOT NULL,
  `PART_APELLIDO` varchar(100) NOT NULL,
  `PART_REGISTRADOPOR` varchar(30) NOT NULL,
  `PART_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`PART_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Contiene Información de las personas que no se encuentran vinculadas a la institución.';

-- Dumping data for table bootpark.particular: ~1 rows (approximately)
/*!40000 ALTER TABLE `particular` DISABLE KEYS */;
INSERT INTO `particular` (`PART_ID`, `PART_IDENTIFICACION`, `PART_NOMBRE`, `PART_APELLIDO`, `PART_REGISTRADOPOR`, `PART_FECHACAMBIO`) VALUES
	(1, 1117513159, 'OSWALDO', 'PAMO LEAL', '53233', '2016-02-09');
/*!40000 ALTER TABLE `particular` ENABLE KEYS */;


-- Dumping structure for table bootpark.terminal
CREATE TABLE IF NOT EXISTS `terminal` (
  `TERM_ID` int(10) NOT NULL AUTO_INCREMENT,
  `TERM_IP` varchar(20) NOT NULL,
  `TERM_PUERTO` varchar(10) NOT NULL,
  `TERM_TIPO` varchar(30) NOT NULL,
  `TERM_REGISTRADOPOR` varchar(30) NOT NULL,
  `TERM_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`TERM_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.terminal: ~1 rows (approximately)
/*!40000 ALTER TABLE `terminal` DISABLE KEYS */;
INSERT INTO `terminal` (`TERM_ID`, `TERM_IP`, `TERM_PUERTO`, `TERM_TIPO`, `TERM_REGISTRADOPOR`, `TERM_FECHACAMBIO`) VALUES
	(1, '127.0.1.1', '2020', 'Biometrico', '53233', '2016-02-09');
/*!40000 ALTER TABLE `terminal` ENABLE KEYS */;


-- Dumping structure for view bootpark.usuario
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `usuario` (
	`PEGE_ID` DECIMAL(30,0) NULL DEFAULT NULL,
	`IDENTIFICACION` VARCHAR(30) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	`NOMBRE` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	`APELLIDO` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	`TIPOUSUARIO` VARCHAR(8) NOT NULL DEFAULT '' COLLATE 'utf8_general_ci'
) ENGINE=MyISAM;


-- Dumping structure for table bootpark.vehiculo
CREATE TABLE IF NOT EXISTS `vehiculo` (
  `VEHI_ID` decimal(30,0) NOT NULL,
  `MAVE_ID` int(10) NOT NULL,
  `VEHI_OBSERVACION` varchar(4000) NOT NULL,
  `VEHI_PLACA` varchar(30) NOT NULL,
  `VEHI_MODELO` decimal(4,0) NOT NULL,
  `VEHI_COLOR` varchar(30) NOT NULL,
  `VEHI_REGISTRADOPOR` varchar(30) NOT NULL,
  `VEHI_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`),
  KEY `MAVV` (`MAVE_ID`),
  CONSTRAINT `MAVV` FOREIGN KEY (`MAVE_ID`) REFERENCES `marcavehiculo` (`MAVE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table bootpark.vehiculo: ~2 rows (approximately)
/*!40000 ALTER TABLE `vehiculo` DISABLE KEYS */;
INSERT INTO `vehiculo` (`VEHI_ID`, `MAVE_ID`, `VEHI_OBSERVACION`, `VEHI_PLACA`, `VEHI_MODELO`, `VEHI_COLOR`, `VEHI_REGISTRADOPOR`, `VEHI_FECHACAMBIO`) VALUES
	(1, 3, '', 'DE243', 2010, 'NEGRO', '53233', '2016-02-09'),
	(2, 9, '', 'DRWIF100', 2011, 'NEGRO', '53233', '2016-02-09');
/*!40000 ALTER TABLE `vehiculo` ENABLE KEYS */;


-- Dumping structure for view bootpark.usuario
-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `usuario`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` VIEW `usuario` AS (select `P`.`PART_IDENTIFICACION` AS `PEGE_ID`,`P`.`PART_IDENTIFICACION` AS `IDENTIFICACION`,`P`.`PART_NOMBRE` AS `NOMBRE`,`P`.`PART_APELLIDO` AS `APELLIDO`,'NOCHAIRA' AS `TIPOUSUARIO` from `BOOTPARK`.`PARTICULAR` `P`) union (select `PC`.`PEGE_ID` AS `PEGE_ID`,`PC`.`IDENTIFICACION` AS `IDENTIFICACION`,`PC`.`NOMBRE` AS `NOMBRE`,`PC`.`APELLIDO` AS `APELLIDO`,'DOCENTE' AS `TIPOUSUARIO` from `GENERAL`.`PERSONACHAIRA` `PC`) ;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
