/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `app_flags` (
  `FlagName` varchar(60) NOT NULL,
  PRIMARY KEY (`FlagName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `assessment_scores` (
  `ScoreID` int(11) NOT NULL AUTO_INCREMENT,
  `AssessmentID` int(11) NOT NULL,
  `StudentID` int(11) NOT NULL,
  `Score` decimal(6,2) DEFAULT NULL,
  `Flag` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`ScoreID`),
  UNIQUE KEY `uq_score` (`AssessmentID`,`StudentID`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `assessments` (
  `AssessmentID` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleID` int(11) NOT NULL,
  `Title` varchar(100) NOT NULL,
  `Kind` varchar(20) NOT NULL DEFAULT 'Exam',
  `Weight` decimal(5,2) NOT NULL DEFAULT 1.00,
  `MaxScore` decimal(5,2) NOT NULL DEFAULT 20.00,
  `AssessDate` date DEFAULT NULL,
  PRIMARY KEY (`AssessmentID`),
  KEY `ix_assess_module` (`ModuleID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `attendance` (
  `AttendanceID` int(11) NOT NULL AUTO_INCREMENT,
  `SessionID` int(11) NOT NULL,
  `PersonType` char(1) NOT NULL,
  `PersonID` int(11) NOT NULL,
  `Status` char(1) NOT NULL,
  `Note` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`AttendanceID`),
  UNIQUE KEY `uq_attendance` (`SessionID`,`PersonType`,`PersonID`),
  KEY `ix_attendance_person` (`PersonType`,`PersonID`)
) ENGINE=InnoDB AUTO_INCREMENT=188 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `class_lessons` (
  `LessonID` int(11) NOT NULL AUTO_INCREMENT,
  `ClassID` int(11) NOT NULL,
  `DayOfWeek` tinyint(4) NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  `ModuleID` int(11) DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `Room` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`LessonID`),
  KEY `ix_lessons_class` (`ClassID`,`DayOfWeek`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `classes` (
  `ClassID` int(11) NOT NULL AUTO_INCREMENT,
  `ClassName` varchar(100) DEFAULT NULL,
  `major` varchar(50) DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `CreatedDate` date DEFAULT NULL,
  PRIMARY KEY (`ClassID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `classschedules` (
  `ScheduleID` int(11) NOT NULL AUTO_INCREMENT,
  `ClassID` int(11) NOT NULL,
  `TimeSlot` varchar(50) DEFAULT NULL,
  `Monday` varchar(100) DEFAULT NULL,
  `Tuesday` varchar(100) DEFAULT NULL,
  `Wednesday` varchar(100) DEFAULT NULL,
  `Thursday` varchar(100) DEFAULT NULL,
  `Friday` varchar(100) DEFAULT NULL,
  `Saturday` varchar(100) DEFAULT NULL,
  `Sunday` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ScheduleID`),
  KEY `ClassID` (`ClassID`),
  CONSTRAINT `classschedules_ibfk_1` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `employee_registry` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `p_pic` varchar(255) DEFAULT NULL,
  `f_name` varchar(20) DEFAULT NULL,
  `m_name` varchar(30) DEFAULT NULL,
  `l_name` varchar(20) DEFAULT NULL,
  `gender` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `b_date` date DEFAULT NULL,
  `nationality` varchar(50) DEFAULT NULL,
  `cin` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `cin_front_copy` varchar(255) DEFAULT NULL,
  `cin_back_copy` varchar(255) DEFAULT NULL,
  `organization` varchar(50) DEFAULT NULL,
  `role` varchar(50) DEFAULT NULL,
  `j_date` date DEFAULT NULL,
  `contract_start` date DEFAULT NULL,
  `contract_end` date DEFAULT NULL,
  `contract_copy` varchar(255) DEFAULT NULL,
  `resume_copy` varchar(255) DEFAULT NULL,
  `high_degree` varchar(50) DEFAULT NULL,
  `year_graduation` int(11) DEFAULT NULL,
  `university` varchar(50) DEFAULT NULL,
  `specialization` varchar(50) DEFAULT NULL,
  `diploma_copy` varchar(255) DEFAULT NULL,
  `salary` int(20) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `lesson_sessions` (
  `SessionID` int(11) NOT NULL AUTO_INCREMENT,
  `ClassID` int(11) NOT NULL,
  `LessonID` int(11) DEFAULT NULL,
  `SessionDate` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  `ModuleID` int(11) DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `TeacherID` int(11) DEFAULT NULL,
  PRIMARY KEY (`SessionID`),
  UNIQUE KEY `uq_session` (`ClassID`,`SessionDate`,`StartTime`),
  KEY `ix_session_date` (`ClassID`,`SessionDate`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `login` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(18) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `modules` (
  `ModuleID` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleName` varchar(100) NOT NULL,
  `ClassID` int(11) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `TeacherID` int(11) DEFAULT NULL,
  `Coefficient` decimal(4,2) NOT NULL DEFAULT 1.00,
  PRIMARY KEY (`ModuleID`),
  KEY `ClassID` (`ClassID`),
  CONSTRAINT `modules_ibfk_1` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `student_module` (
  `StudentModuleID` int(11) NOT NULL AUTO_INCREMENT,
  `StudentID` int(11) NOT NULL,
  `ModuleID` int(11) NOT NULL,
  `Grade` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`StudentModuleID`),
  UNIQUE KEY `uq_student_module` (`StudentID`,`ModuleID`),
  KEY `fk_student_module_student` (`StudentID`),
  KEY `fk_student_module_module` (`ModuleID`),
  CONSTRAINT `fk_student_module_module` FOREIGN KEY (`ModuleID`) REFERENCES `modules` (`ModuleID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_student_module_student` FOREIGN KEY (`StudentID`) REFERENCES `student_registry` (`StudentID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `student_registry` (
  `StudentID` int(11) NOT NULL AUTO_INCREMENT,
  `p_pic` varchar(255) DEFAULT NULL,
  `f_name` varchar(50) DEFAULT NULL,
  `m_name` varchar(50) DEFAULT NULL,
  `l_name` varchar(50) DEFAULT NULL,
  `gender` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `b_date` date DEFAULT NULL,
  `nationality` varchar(50) DEFAULT NULL,
  `cin` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `cin_front_copy` varchar(255) DEFAULT NULL,
  `cin_back_copy` varchar(255) DEFAULT NULL,
  `school_name` varchar(70) DEFAULT NULL,
  `j_date` date DEFAULT NULL,
  `major` varchar(50) DEFAULT NULL,
  `high_degree` varchar(50) DEFAULT NULL,
  `year_graduation` int(11) DEFAULT NULL,
  `university` varchar(50) DEFAULT NULL,
  `specialization` varchar(50) DEFAULT NULL,
  `diploma_copy` varchar(255) DEFAULT NULL,
  `emerg_contact_f_name` varchar(50) DEFAULT NULL,
  `emerg_contact_m_name` varchar(50) DEFAULT NULL,
  `emerg_contact_l_name` varchar(50) DEFAULT NULL,
  `emerg_contact_relation` varchar(50) DEFAULT NULL,
  `emerg_contact_phone` varchar(20) DEFAULT NULL,
  `emerg_contact_cin` varchar(15) DEFAULT NULL,
  `emerg_contact_cin_f_copy` varchar(255) DEFAULT NULL,
  `emerg_contact_cin_b_copy` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`StudentID`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `studentclasses` (
  `StudentClassID` int(11) NOT NULL AUTO_INCREMENT,
  `StudentID` int(11) DEFAULT NULL,
  `ClassID` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentClassID`),
  UNIQUE KEY `StudentID` (`StudentID`),
  KEY `ClassID` (`ClassID`),
  CONSTRAINT `studentclasses_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `student_registry` (`StudentID`),
  CONSTRAINT `studentclasses_ibfk_2` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `teacher_registry` (
  `TeacherID` int(11) NOT NULL AUTO_INCREMENT,
  `p_pic` varchar(255) DEFAULT NULL,
  `f_name` varchar(50) DEFAULT NULL,
  `m_name` varchar(50) DEFAULT NULL,
  `l_name` varchar(50) DEFAULT NULL,
  `gender` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `b_date` date DEFAULT NULL,
  `nationality` varchar(50) DEFAULT NULL,
  `cin` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `cin_front_copy` varchar(255) DEFAULT NULL,
  `cin_back_copy` varchar(255) DEFAULT NULL,
  `organization` varchar(50) DEFAULT NULL,
  `role` varchar(50) DEFAULT NULL,
  `j_date` date DEFAULT NULL,
  `contract_start` date DEFAULT NULL,
  `contract_end` date DEFAULT NULL,
  `contract_copy` varchar(255) DEFAULT NULL,
  `resume_copy` varchar(255) DEFAULT NULL,
  `high_degree` varchar(50) DEFAULT NULL,
  `year_graduation` int(11) DEFAULT NULL,
  `university` varchar(50) DEFAULT NULL,
  `specialization` varchar(50) DEFAULT NULL,
  `diploma_copy` varchar(255) DEFAULT NULL,
  `salary` int(20) DEFAULT NULL,
  `module` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`TeacherID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE IF NOT EXISTS `teacherclasses` (
  `TeacherClassID` int(11) NOT NULL AUTO_INCREMENT,
  `TeacherID` int(11) DEFAULT NULL,
  `ClassID` int(11) DEFAULT NULL,
  PRIMARY KEY (`TeacherClassID`),
  KEY `TeacherID` (`TeacherID`),
  KEY `ClassID` (`ClassID`),
  CONSTRAINT `teacherclasses_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `teacher_registry` (`TeacherID`),
  CONSTRAINT `teacherclasses_ibfk_2` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;
