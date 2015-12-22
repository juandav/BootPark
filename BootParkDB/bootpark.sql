/*
Navicat MySQL Data Transfer

Source Server         : LOCAL
Source Server Version : 50542
Source Host           : localhost:3306
Source Database       : bootpark

Target Server Type    : MYSQL
Target Server Version : 50542
File Encoding         : 65001

Date: 2015-12-22 01:41:10
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for autorizacion
-- ----------------------------
DROP TABLE IF EXISTS `autorizacion`;
CREATE TABLE `autorizacion` (
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

-- ----------------------------
-- Records of autorizacion
-- ----------------------------

-- ----------------------------
-- Table structure for circulacion
-- ----------------------------
DROP TABLE IF EXISTS `circulacion`;
CREATE TABLE `circulacion` (
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
  CONSTRAINT `TERC` FOREIGN KEY (`TERM_ID`) REFERENCES `terminal` (`TERM_ID`),
  CONSTRAINT `CIRA` FOREIGN KEY (`VEHI_ID`, `USUA_ID`) REFERENCES `autorizacion` (`VEHI_ID`, `USUA_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of circulacion
-- ----------------------------

-- ----------------------------
-- Table structure for etiqueta
-- ----------------------------
DROP TABLE IF EXISTS `etiqueta`;
CREATE TABLE `etiqueta` (
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

-- ----------------------------
-- Records of etiqueta
-- ----------------------------

-- ----------------------------
-- Table structure for etiquetausuario
-- ----------------------------
DROP TABLE IF EXISTS `etiquetausuario`;
CREATE TABLE `etiquetausuario` (
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

-- ----------------------------
-- Records of etiquetausuario
-- ----------------------------

-- ----------------------------
-- Table structure for etiquetavehiculo
-- ----------------------------
DROP TABLE IF EXISTS `etiquetavehiculo`;
CREATE TABLE `etiquetavehiculo` (
  `VEHI_ID` decimal(30,0) NOT NULL,
  `ETIQ_ID` decimal(30,0) NOT NULL,
  `ETIQ_TIPO` varchar(30) NOT NULL,
  `ETVE_OBSERVACION` varchar(4000) NOT NULL,
  `ETVE_REGISTRADOPOR` varchar(30) NOT NULL,
  `ETVE_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`,`ETIQ_ID`,`ETIQ_TIPO`),
  KEY `ETIV` (`ETIQ_ID`,`ETIQ_TIPO`),
  KEY `VEHE` (`VEHI_ID`),
  CONSTRAINT `VEHE` FOREIGN KEY (`VEHI_ID`) REFERENCES `vehiculo` (`VEHI_ID`),
  CONSTRAINT `ETIV` FOREIGN KEY (`ETIQ_ID`, `ETIQ_TIPO`) REFERENCES `etiqueta` (`ETIQ_ID`, `ETIQ_TIPO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of etiquetavehiculo
-- ----------------------------

-- ----------------------------
-- Table structure for huella
-- ----------------------------
DROP TABLE IF EXISTS `huella`;
CREATE TABLE `huella` (
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

-- ----------------------------
-- Records of huella
-- ----------------------------

-- ----------------------------
-- Table structure for marcavehiculo
-- ----------------------------
DROP TABLE IF EXISTS `marcavehiculo`;
CREATE TABLE `marcavehiculo` (
  `MAVE_ID` int(10) NOT NULL AUTO_INCREMENT,
  `MAVE_MARCA` varchar(30) NOT NULL,
  PRIMARY KEY (`MAVE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of marcavehiculo
-- ----------------------------

-- ----------------------------
-- Table structure for particular
-- ----------------------------
DROP TABLE IF EXISTS `particular`;
CREATE TABLE `particular` (
  `PART_ID` decimal(30,0) NOT NULL,
  `PART_IDENTIFICACION` decimal(30,0) NOT NULL,
  `PART_NOMBRE` varchar(200) NOT NULL,
  `PART_APELLIDO` varchar(200) NOT NULL,
  `PART_REGISTRADOPOR` varchar(30) NOT NULL,
  `PART_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`PART_ID`),
  UNIQUE KEY `PART_IDENTIFICACION` (`PART_IDENTIFICACION`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of particular
-- ----------------------------

-- ----------------------------
-- Table structure for terminal
-- ----------------------------
DROP TABLE IF EXISTS `terminal`;
CREATE TABLE `terminal` (
  `TERM_ID` int(10) NOT NULL AUTO_INCREMENT,
  `TERM_IP` varchar(20) NOT NULL,
  `TERM_PUERTO` varchar(10) NOT NULL,
  `TERM_TIPO` varchar(30) NOT NULL,
  `TERM_REGISTRADOPOR` varchar(30) NOT NULL,
  `TERM_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`TERM_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of terminal
-- ----------------------------

-- ----------------------------
-- Table structure for vehiculo
-- ----------------------------
DROP TABLE IF EXISTS `vehiculo`;
CREATE TABLE `vehiculo` (
  `VEHI_ID` decimal(30,0) NOT NULL,
  `MAVE_ID` int(10) NOT NULL,
  `VEHI_OBSERVACION` varchar(4000) NOT NULL,
  `VEHI_PLACA` varchar(30) NOT NULL,
  `VEHI_MODELO` decimal(4,0) NOT NULL,
  `VEHI_MARCA` varchar(30) NOT NULL,
  `VEHI_COLOR` varchar(30) NOT NULL,
  `VEHI_REGISTRADOPOR` varchar(30) NOT NULL,
  `VEHI_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`VEHI_ID`),
  KEY `MAVV` (`MAVE_ID`),
  CONSTRAINT `MAVV` FOREIGN KEY (`MAVE_ID`) REFERENCES `marcavehiculo` (`MAVE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vehiculo
-- ----------------------------

-- ----------------------------
-- View structure for usuario
-- ----------------------------
DROP VIEW IF EXISTS `usuario`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost`  VIEW `usuario` AS (SELECT
  P.PART_IDENTIFICACION AS ID,
	P.PART_IDENTIFICACION AS IDENT,
	P.PART_NOMBRE AS NOMBRE,
  P.PART_APELLIDO AS APELLIDO,
  'NOCHAIRA' AS TIPO
FROM
	bootpark.PARTICULAR P
)
UNION
(SELECT
  PNG.PEGE_ID AS ID,
	PNG.PENG_IDENTIFICACION AS IDENT,
	PNG.PENG_NOMBRE AS NOMBRE,
  PNG.PENG_APELLIDO AS APELLIDO,
  'CHAIRA' AS TIPO
FROM
	general.PERSONANATURALGENERAL PNG
) ;
