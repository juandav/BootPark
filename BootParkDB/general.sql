/*
Navicat MySQL Data Transfer

Source Server         : LOCAL
Source Server Version : 50542
Source Host           : localhost:3306
Source Database       : general

Target Server Type    : MYSQL
Target Server Version : 50542
File Encoding         : 65001

Date: 2015-12-22 01:41:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for personanaturalgeneral
-- ----------------------------
DROP TABLE IF EXISTS `personanaturalgeneral`;
CREATE TABLE `personanaturalgeneral` (
  `PEGE_ID` int(10) NOT NULL AUTO_INCREMENT,
  `PENG_IDENTIFICACION` int(10) NOT NULL,
  `PENG_NOMBRE` varchar(30) NOT NULL,
  `PENG_APELLIDO` varchar(30) NOT NULL,
  `PENG_REGISTRADOPOR` varchar(30) NOT NULL,
  `PENG_FECHACAMBIO` date NOT NULL,
  PRIMARY KEY (`PEGE_ID`),
  UNIQUE KEY `PENG_IDENTIFICACION` (`PENG_IDENTIFICACION`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of personanaturalgeneral
-- ----------------------------
