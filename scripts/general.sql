-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.10 - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2016-02-09 18:17:42
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for general
CREATE DATABASE IF NOT EXISTS `general` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `general`;


-- Dumping structure for table general.personachaira
CREATE TABLE IF NOT EXISTS `personachaira` (
  `PEGE_ID` decimal(30,0) DEFAULT NULL,
  `IDENTIFICACION` varchar(30) DEFAULT NULL,
  `NOMBRE` varchar(30) DEFAULT NULL,
  `APELLIDO` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table general.personachaira: ~0 rows (approximately)
/*!40000 ALTER TABLE `personachaira` DISABLE KEYS */;
/*!40000 ALTER TABLE `personachaira` ENABLE KEYS */;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
