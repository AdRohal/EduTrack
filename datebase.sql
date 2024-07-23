-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 23, 2024 at 05:35 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `datebase`
--

-- --------------------------------------------------------

--
-- Table structure for table `classes`
--

CREATE TABLE `classes` (
  `ClassID` int(11) NOT NULL,
  `ClassName` varchar(100) DEFAULT NULL,
  `major` varchar(50) DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `CreatedDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `classschedules`
--

CREATE TABLE `classschedules` (
  `ScheduleID` int(11) NOT NULL,
  `ClassID` int(11) NOT NULL,
  `TimeSlot` varchar(50) DEFAULT NULL,
  `Monday` varchar(100) DEFAULT NULL,
  `Tuesday` varchar(100) DEFAULT NULL,
  `Wednesday` varchar(100) DEFAULT NULL,
  `Thursday` varchar(100) DEFAULT NULL,
  `Friday` varchar(100) DEFAULT NULL,
  `Saturday` varchar(100) DEFAULT NULL,
  `Sunday` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `employee_registry`
--

CREATE TABLE `employee_registry` (
  `id` int(11) NOT NULL,
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
  `salary` int(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `id` int(11) NOT NULL,
  `UserName` varchar(18) DEFAULT NULL,
  `Password` varchar(18) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `modules`
--

CREATE TABLE `modules` (
  `ModuleID` int(11) NOT NULL,
  `ModuleName` varchar(100) NOT NULL,
  `ClassID` int(11) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `studentclasses`
--

CREATE TABLE `studentclasses` (
  `StudentClassID` int(11) NOT NULL,
  `StudentID` int(11) DEFAULT NULL,
  `ClassID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `student_module`
--

CREATE TABLE `student_module` (
  `StudentModuleID` int(11) NOT NULL,
  `StudentID` int(11) NOT NULL,
  `ModuleID` int(11) NOT NULL,
  `Grade` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `student_registry`
--

CREATE TABLE `student_registry` (
  `StudentID` int(11) NOT NULL,
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
  `emerg_contact_cin_b_copy` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `teacherclasses`
--

CREATE TABLE `teacherclasses` (
  `TeacherClassID` int(11) NOT NULL,
  `TeacherID` int(11) DEFAULT NULL,
  `ClassID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `teacher_registry`
--

CREATE TABLE `teacher_registry` (
  `TeacherID` int(11) NOT NULL,
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
  `module` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `classes`
--
ALTER TABLE `classes`
  ADD PRIMARY KEY (`ClassID`);

--
-- Indexes for table `classschedules`
--
ALTER TABLE `classschedules`
  ADD PRIMARY KEY (`ScheduleID`),
  ADD KEY `ClassID` (`ClassID`);

--
-- Indexes for table `employee_registry`
--
ALTER TABLE `employee_registry`
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `modules`
--
ALTER TABLE `modules`
  ADD PRIMARY KEY (`ModuleID`),
  ADD KEY `ClassID` (`ClassID`);

--
-- Indexes for table `studentclasses`
--
ALTER TABLE `studentclasses`
  ADD PRIMARY KEY (`StudentClassID`),
  ADD UNIQUE KEY `StudentID` (`StudentID`),
  ADD KEY `ClassID` (`ClassID`);

--
-- Indexes for table `student_module`
--
ALTER TABLE `student_module`
  ADD PRIMARY KEY (`StudentModuleID`),
  ADD KEY `fk_student_module_student` (`StudentID`),
  ADD KEY `fk_student_module_module` (`ModuleID`);

--
-- Indexes for table `student_registry`
--
ALTER TABLE `student_registry`
  ADD PRIMARY KEY (`StudentID`);

--
-- Indexes for table `teacherclasses`
--
ALTER TABLE `teacherclasses`
  ADD PRIMARY KEY (`TeacherClassID`),
  ADD KEY `TeacherID` (`TeacherID`),
  ADD KEY `ClassID` (`ClassID`);

--
-- Indexes for table `teacher_registry`
--
ALTER TABLE `teacher_registry`
  ADD PRIMARY KEY (`TeacherID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `classes`
--
ALTER TABLE `classes`
  MODIFY `ClassID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `classschedules`
--
ALTER TABLE `classschedules`
  MODIFY `ScheduleID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `employee_registry`
--
ALTER TABLE `employee_registry`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `login`
--
ALTER TABLE `login`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `modules`
--
ALTER TABLE `modules`
  MODIFY `ModuleID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `studentclasses`
--
ALTER TABLE `studentclasses`
  MODIFY `StudentClassID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `student_module`
--
ALTER TABLE `student_module`
  MODIFY `StudentModuleID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `student_registry`
--
ALTER TABLE `student_registry`
  MODIFY `StudentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `teacherclasses`
--
ALTER TABLE `teacherclasses`
  MODIFY `TeacherClassID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `teacher_registry`
--
ALTER TABLE `teacher_registry`
  MODIFY `TeacherID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `classschedules`
--
ALTER TABLE `classschedules`
  ADD CONSTRAINT `classschedules_ibfk_1` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`);

--
-- Constraints for table `modules`
--
ALTER TABLE `modules`
  ADD CONSTRAINT `modules_ibfk_1` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `studentclasses`
--
ALTER TABLE `studentclasses`
  ADD CONSTRAINT `studentclasses_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `student_registry` (`StudentID`),
  ADD CONSTRAINT `studentclasses_ibfk_2` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`);

--
-- Constraints for table `student_module`
--
ALTER TABLE `student_module`
  ADD CONSTRAINT `fk_student_module_module` FOREIGN KEY (`ModuleID`) REFERENCES `modules` (`ModuleID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_student_module_student` FOREIGN KEY (`StudentID`) REFERENCES `student_registry` (`StudentID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `teacherclasses`
--
ALTER TABLE `teacherclasses`
  ADD CONSTRAINT `teacherclasses_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `teacher_registry` (`TeacherID`),
  ADD CONSTRAINT `teacherclasses_ibfk_2` FOREIGN KEY (`ClassID`) REFERENCES `classes` (`ClassID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
