-- phpMyAdmin SQL Dump
-- version 4.4.9
-- http://www.phpmyadmin.net
--
-- Φιλοξενητής: localhost:3306
-- Χρόνος δημιουργίας: 29 Φεβ 2016 στις 16:04:54
-- Έκδοση διακομιστή: 5.5.42
-- Έκδοση PHP: 5.6.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Βάση δεδομένων: `unmentionable`
--

-- --------------------------------------------------------

--
-- Δομή πίνακα για τον πίνακα `account`
--

CREATE TABLE `account` (
  `id` bigint(20) NOT NULL,
  `credentialsexpired` bit(1) NOT NULL,
  `enabled` bit(1) NOT NULL,
  `expired` bit(1) NOT NULL,
  `locked` bit(1) NOT NULL,
  `password` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=latin1;

--
-- Άδειασμα δεδομένων του πίνακα `account`
--

INSERT INTO `account` (`id`, `credentialsexpired`, `enabled`, `expired`, `locked`, `password`, `username`) VALUES
(3, b'0', b'1', b'0', b'0', 'papidakos', 'cpapidas'),
(5, b'0', b'1', b'0', b'0', 'b8f57d6d6ec0a60dfe2e20182d4615b12e321cad9e2979e0b9f81e0d6eda78ad9b6dcfe53e4e22d1', 'cpapidas2'),
(10, b'0', b'1', b'0', b'0', '$2a$10$h6cO.XkIuJ8I1kS2ZuaW/uRbv67.0ib7xOLdyLhAdWtN5cXRXSatW', 'cdddddc'),
(11, b'0', b'1', b'0', b'0', '$2a$10$gTqi4RDXj5ovC.MoFHEoEOMMhPqjcvF/R49EjxwfUme6PwXhUHMwa', 'cc'),
(13, b'0', b'1', b'0', b'0', '$2a$10$nlGCcuWdRcLDVOFYzudn5.tl4bhH47mkZDB3JubzbZy6obZVxgdmy', 'cd'),
(15, b'0', b'1', b'0', b'0', '$2a$10$jT/N9/TgXnbGpdH0R1lfL.VlCihlR9jY7CBESimbtyAhOAwo9bguu', 'cs'),
(16, b'0', b'1', b'0', b'0', '$2a$10$wupaAG1Cu8rGI4.kapj7rebxjGKfwjBYjkWbq.F.ZQKyiegu0exrq', 'wesdfsdfsdfs'),
(17, b'0', b'1', b'0', b'0', '$2a$10$QufI1ku2vBT1NBCENVAKUeHsaktKTGZPzn/I66WdcTnXqEhbqAaC6', 'yy'),
(18, b'0', b'1', b'0', b'0', '$2a$10$1OOIk5weuiDjS8S9BKpP3uBohiQ/RaLJjUJfiqdn8IIumXQ6hXn3K', 'mm33'),
(19, b'0', b'1', b'0', b'0', '$2a$10$AZJQX.Zppg2.5geTAnC6V.XUJt.NGtYROpxoKpOdBCHoDCtbU/cNa', 'mm33dfgdfg'),
(20, b'0', b'1', b'0', b'0', '$2a$10$lWh.9oyG1fz7NaMrEd32m.hLm4ucEhQdanNO09fmVFZB1YxyRC3ua', 'wesdfsdllfsdfs'),
(21, b'0', b'1', b'0', b'0', '$2a$10$3ybRtrgCf1D.01aeWwheluIaOEJ8FRw0UZ7LBG.yzvY1.2/QTCghK', 'asdfafsd234'),
(22, b'0', b'1', b'0', b'0', '$2a$10$x1loX5n0.b5MHDSfLWEJduhZgNU.6aVlaxAJ57PlWs.B.FlFDhL3K', 'asdfafsd234e'),
(23, b'0', b'1', b'0', b'0', '$2a$10$qSyGWdtTl2w2ubms8P2c1.PR4.QAxFuAaFMyA133XsGXJfPNXnVzi', 'sdfsdf333'),
(24, b'0', b'1', b'0', b'0', '$2a$10$/Dtd5WqgGTmTGDaB8Sd6vOTdyCu4qc9G94Igukv3QONpuB.aUkBue', 'sdfsdf333s'),
(25, b'0', b'1', b'0', b'0', '$2a$10$MA50o0uzuyqERecKD7Rl4uqdlNDAm1x/4nE2.1cJ1DTvsf8799i.O', 'fasdf3333'),
(26, b'0', b'1', b'0', b'0', '$2a$10$KL00afcwiDbq24EBev1Lj.CG5WSgydNra3LlmRu3CWJMIlNF/TQcO', 'fasdf33332'),
(27, b'0', b'1', b'0', b'0', '$2a$10$PY9L787.w5VgoO9ZH0S62OQOz31.yARV6w.F2QfiRxbPTktZ0s9oy', 'Christost22'),
(28, b'0', b'1', b'0', b'0', '$2a$10$Q9XKh8cc4J7qy0cy9OSksuXhOwMgoghPof8ILZxsULuk3gsMr65gC', 'asdfasdf234'),
(29, b'0', b'1', b'0', b'0', '$2a$10$q4FAN8hsZKwEGWWmKUJK4uan4hBHhq2nd.NLm.tc2qDq3COjHfkz6', 'adsfsda234'),
(30, b'0', b'1', b'0', b'0', '$2a$10$oXvocLdaQVC3n4wKYEtON./1EnOn8Jk5E9XTQEYHvJEjD7uj12Pvy', 'asdfasdf1234'),
(31, b'0', b'1', b'0', b'0', '$2a$10$OFLSsg0x5A36HPq3.2wEDexxPlHQzWvQGo5rmfG5P6eeORtaQ/Ubq', 'asfdasdf3433'),
(32, b'0', b'1', b'0', b'0', '$2a$10$XlUPeD83cMNbnjnXnXHcxubm0n9SrBd.LsCB3wxwAge6tYhwhumZq', 'fasfafd324'),
(38, b'0', b'1', b'0', b'0', '$2a$10$SdZfafMVkFopgdvi/JrBn.h1KDIt.3Dfx6lbhLWB/CiGIBNiGGg/i', 'asdfasdf2344'),
(39, b'0', b'1', b'0', b'0', '$2a$10$7sibRze3x6SVstcQYXSDe.LqhE0gUHvSfDTVZKyURr9EzKykw4sry', 'dsfsdff444'),
(40, b'0', b'1', b'0', b'0', '$2a$10$EglFcHZnRyzrlmvb1ldV9e89IL1IqgdLhgeG/WGAR9UeXRP0fNYnW', 'ddsdsdfffd'),
(41, b'0', b'1', b'0', b'0', '$2a$10$5RcLNvOD.r03ZayV53MxVuUQYbJvDtMrRFTTUVL9G2X0S6/ZxEo3S', 'papidakos'),
(42, b'0', b'1', b'0', b'0', '$2a$10$eR.90pak84ipqOq9HVEMq.VgQ8O9JOFGOYROCzNzb7hdRhI/cEmem', 'papidakos2'),
(43, b'0', b'1', b'0', b'0', 'spring', 'roy'),
(44, b'0', b'1', b'0', b'0', '$2a$10$FbaEGdHDEEvpPHjSZkWNyecmsL79lH8qzJKexRo5EH4shvZfd4WhG', 'papapakiki'),
(45, b'0', b'1', b'0', b'0', '$2a$10$69qe1gDLxu9T2n6fIV6s3.bY.uvNBEG/GvLcw7enccZ4HFLY2sMHy', 'papapakiki1'),
(46, b'0', b'1', b'0', b'0', '$2a$10$qBQBPCNXVGd5l.qZd47MKuI.M88K8WiwbigPUxpUbx1WJA5b8oK8q', 'papalolif'),
(47, b'0', b'1', b'0', b'0', '$2a$10$6LZfgsqeunoY4gLc1EZbe..6q5seZrA191F6iH81EeI709Y0h.1Py', 'fafafakoko');

-- --------------------------------------------------------

--
-- Δομή πίνακα για τον πίνακα `account_role`
--

CREATE TABLE `account_role` (
  `account_id` bigint(20) NOT NULL,
  `role_id` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Άδειασμα δεδομένων του πίνακα `account_role`
--

INSERT INTO `account_role` (`account_id`, `role_id`) VALUES
(3, 1),
(5, 1),
(10, 1),
(11, 1),
(13, 1),
(15, 1),
(16, 1),
(17, 1),
(18, 1),
(19, 1),
(20, 1),
(21, 1),
(22, 1),
(23, 1),
(24, 1),
(25, 1),
(26, 1),
(27, 1),
(28, 1),
(29, 1),
(30, 1),
(31, 1),
(32, 1),
(38, 1),
(39, 1),
(40, 1),
(41, 1),
(42, 1),
(44, 1),
(45, 1),
(46, 1),
(47, 1);

-- --------------------------------------------------------

--
-- Δομή πίνακα για τον πίνακα `role`
--

CREATE TABLE `role` (
  `id` bigint(20) NOT NULL,
  `code` varchar(255) NOT NULL,
  `label` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Άδειασμα δεδομένων του πίνακα `role`
--

INSERT INTO `role` (`id`, `code`, `label`) VALUES
(1, 'ROLE_USER', 'User');

--
-- Ευρετήρια για άχρηστους πίνακες
--

--
-- Ευρετήρια για πίνακα `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `UK_gex1lmaqpg0ir5g1f5eftyaa1` (`username`);

--
-- Ευρετήρια για πίνακα `account_role`
--
ALTER TABLE `account_role`
  ADD PRIMARY KEY (`account_id`,`role_id`),
  ADD KEY `FK_p2jpuvn8yll7x96rae4hvw3sj` (`role_id`);

--
-- Ευρετήρια για πίνακα `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT για άχρηστους πίνακες
--

--
-- AUTO_INCREMENT για πίνακα `account`
--
ALTER TABLE `account`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=48;
--
-- Περιορισμοί για άχρηστους πίνακες
--

--
-- Περιορισμοί για πίνακα `account_role`
--
ALTER TABLE `account_role`
  ADD CONSTRAINT `FK_ibmw1g5w37bmuh5fc0db7wn10` FOREIGN KEY (`account_id`) REFERENCES `account` (`id`),
  ADD CONSTRAINT `FK_p2jpuvn8yll7x96rae4hvw3sj` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`);
