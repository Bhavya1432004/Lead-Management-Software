-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: lms
-- ------------------------------------------------------
-- Server version	9.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activity_log`
--

DROP TABLE IF EXISTS `activity_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `activity_log` (
  `activity_id` int NOT NULL AUTO_INCREMENT,
  `u_id` int NOT NULL,
  `lead_id` int NOT NULL,
  `action_type` enum('Add','Update','Delete') DEFAULT NULL,
  `action_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`activity_id`),
  KEY `u_id` (`u_id`),
  KEY `lead_id` (`lead_id`),
  CONSTRAINT `activity_log_ibfk_1` FOREIGN KEY (`u_id`) REFERENCES `users` (`u_id`),
  CONSTRAINT `activity_log_ibfk_2` FOREIGN KEY (`lead_id`) REFERENCES `leads` (`lead_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activity_log`
--

LOCK TABLES `activity_log` WRITE;
/*!40000 ALTER TABLE `activity_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `activity_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imp_exp_log`
--

DROP TABLE IF EXISTS `imp_exp_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imp_exp_log` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `u_id` int NOT NULL,
  `activity_type` enum('Import','Export') NOT NULL,
  `activity_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`log_id`),
  KEY `u_id` (`u_id`),
  CONSTRAINT `imp_exp_log_ibfk_1` FOREIGN KEY (`u_id`) REFERENCES `users` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imp_exp_log`
--

LOCK TABLES `imp_exp_log` WRITE;
/*!40000 ALTER TABLE `imp_exp_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `imp_exp_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lead_assignment`
--

DROP TABLE IF EXISTS `lead_assignment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lead_assignment` (
  `assignmet_id` int NOT NULL AUTO_INCREMENT,
  `lead_id` int NOT NULL,
  `u_id` int NOT NULL,
  `assingment_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`assignmet_id`),
  KEY `lead_id` (`lead_id`),
  KEY `u_id` (`u_id`),
  CONSTRAINT `lead_assignment_ibfk_1` FOREIGN KEY (`lead_id`) REFERENCES `leads` (`lead_id`),
  CONSTRAINT `lead_assignment_ibfk_2` FOREIGN KEY (`u_id`) REFERENCES `users` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lead_assignment`
--

LOCK TABLES `lead_assignment` WRITE;
/*!40000 ALTER TABLE `lead_assignment` DISABLE KEYS */;
/*!40000 ALTER TABLE `lead_assignment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lead_log`
--

DROP TABLE IF EXISTS `lead_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lead_log` (
  `status_id` int NOT NULL AUTO_INCREMENT,
  `lead_id` int NOT NULL,
  `old_status` enum('NEW','CONTACTED','FOLLOW-UP','CONVERTED','LOST') NOT NULL,
  `new_status` enum('NEW','CONTACTED','FOLLOW-UP','CONVERTED','LOST') NOT NULL,
  `update_by` int NOT NULL,
  `update_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`status_id`),
  KEY `lead_id` (`lead_id`),
  KEY `update_by` (`update_by`),
  CONSTRAINT `lead_log_ibfk_1` FOREIGN KEY (`lead_id`) REFERENCES `leads` (`lead_id`),
  CONSTRAINT `lead_log_ibfk_2` FOREIGN KEY (`update_by`) REFERENCES `users` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lead_log`
--

LOCK TABLES `lead_log` WRITE;
/*!40000 ALTER TABLE `lead_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `lead_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `leads`
--

DROP TABLE IF EXISTS `leads`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `leads` (
  `lead_id` int NOT NULL AUTO_INCREMENT,
  `lead_name` varchar(50) NOT NULL,
  `lead_email` varchar(50) NOT NULL,
  `lead_contact` varchar(15) DEFAULT NULL,
  `lead_source` varchar(50) NOT NULL,
  `assigned_to` int NOT NULL,
  `lead_status` enum('NEW','CONTACTED','FOLLOW-UP','CONVERTED','LOST') NOT NULL DEFAULT 'NEW',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`lead_id`),
  KEY `assigned_to` (`assigned_to`),
  CONSTRAINT `leads_ibfk_1` FOREIGN KEY (`assigned_to`) REFERENCES `users` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `leads`
--

LOCK TABLES `leads` WRITE;
/*!40000 ALTER TABLE `leads` DISABLE KEYS */;
/*!40000 ALTER TABLE `leads` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `u_id` int NOT NULL AUTO_INCREMENT,
  `u_name` varchar(50) NOT NULL,
  `u_password` varchar(255) NOT NULL,
  `role` enum('Admin','Manager','Sales Representative','Superuser') NOT NULL,
  `u_email` varchar(50) NOT NULL,
  `contact_no` varchar(15) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`u_id`),
  UNIQUE KEY `u_name` (`u_name`),
  UNIQUE KEY `u_email` (`u_email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-02-12 15:02:14
