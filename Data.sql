-- --------------------------------------------------------
-- 호스트:                          127.0.0.1
-- 서버 버전:                        11.4.2-MariaDB - mariadb.org binary distribution
-- 서버 OS:                        Win64
-- HeidiSQL 버전:                  12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- game 데이터베이스 구조 내보내기
CREATE DATABASE IF NOT EXISTS `game` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `game`;

-- 테이블 game.users 구조 내보내기
CREATE TABLE IF NOT EXISTS `users` (
  `uid` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(50) NOT NULL,
  `pw` varchar(100) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `LEVEL` int(11) DEFAULT NULL,
  `class` int(11) unsigned DEFAULT NULL,
  `profile_text` varchar(255) DEFAULT NULL,
  `pwHash` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`uid`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- 테이블 데이터 game.users:~13 rows (대략적) 내보내기
INSERT INTO `users` (`uid`, `email`, `pw`, `name`, `LEVEL`, `class`, `profile_text`, `pwHash`) VALUES
	(1, 'aa', '1234', 'zZ타락파워Zz', 9, 1, '이젠 맞습니다.', NULL),
	(2, 'aac', '1234', NULL, 1, 3, '사실아닙니다.', NULL),
	(3, 'abc@abc.com', '1234', NULL, 1, 2, NULL, NULL),
	(4, 'abd@abc.com', '1234', NULL, 2, 1, NULL, NULL),
	(5, 'koj887174@gmail.com', '1234', NULL, 1, 0, 'text', NULL),
	(6, 'koj8871@naver.com', '1234', NULL, 2, 1, 'text', NULL),
	(7, 'dudwlsl8@hanmail.net', '1234', NULL, 1, 2, 'taxi', NULL),
	(8, 'koj887174@gamil.com', '1234', NULL, 1, 1, 'sudo', NULL),
	(11, 'SignUpEmail@Email', '********', '지니지니영진이', 5, 1, '히히히히 성공', NULL),
	(13, '3rdTry', 'asdf', '영진쓰', 444, 3, '이번엔 되겠지', NULL),
	(14, 'koj8871', 'dudwlsl8', 'Youngjean', 3, 1, 'hello', '8145ab35f9d4389101baebff1a257da5be7ce88b42249e4589a16a84ce11a665'),
	(15, 'koj887174', 'dudwlsl8', 'Youngejan', 1, 1, '11', '8145ab35f9d4389101baebff1a257da5be7ce88b42249e4589a16a84ce11a665'),
	(16, '111', '123', 'Aaa', 111, 2, 'aaa', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
