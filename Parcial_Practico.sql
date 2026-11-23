-- MySQL dump 10.13  Distrib 8.0.46, for Win64 (x86_64)
--
-- Host: localhost    Database: lab_mvp
-- ------------------------------------------------------
-- Server version	8.0.46

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

-- --------
-- Base de datos: se crea si no existe y se selecciona para todo lo que sigue
CREATE DATABASE IF NOT EXISTS lab_mvp;
USE lab_mvp;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `cantidad` int NOT NULL,
  `imagen` longblob,
  PRIMARY KEY (`id`),
  CONSTRAINT `chk_productos_cantidad` CHECK ((`cantidad` >= 0)),
  CONSTRAINT `chk_productos_precio` CHECK ((`precio` >= 0))
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'Teclado',20.00,2,NULL),(2,'Mouse',15.70,2,NULL),(3,'Portátil',700.70,2,NULL);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `correo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `salt` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password_hash` char(64) COLLATE utf8mb4_unicode_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `fecha_creacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `rol` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'lector',
  PRIMARY KEY (`id`),
  UNIQUE KEY `correo` (`correo`),
  CONSTRAINT `chk_usuarios_rol` CHECK ((`rol` in (_utf8mb4'admin',_utf8mb4'editor',_utf8mb4'lector')))
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Kankibe','admin@lab.com','LabMVP_Admin_Salt_2026','c76b531fd06d3f43aee0fd07a1f00fa65a55895a1413402c9591879354cfb4a1',1,'2026-11-08 21:04:27','admin'),(2,'Kankibe','Kankibe.@lab.com','b954b8393f60618022833771c37ddf01','97388727e5e4d588f773724eae261b8aed901f9eda661179f70edffe8b85709e',1,'2026-11-08 21:54:30','lector'),(3,'admin','admin2@lab.com','0a6c97f6cab80e914b11be5365de6406','e008174e4c42b1205a04e437da438314bdc73ac357d32b0e11d7390f4de66b9c',1,'2026-11-16 10:28:06','lector'),(4,'Usuario Normal','usuario@lab.com','LabMVP_User_Salt_2026','53465b3e0e85b8d86f170bae176f36f16755d618032ae32f34f1056d365f1025',1,'2026-11-16 11:21:19','lector'),(5,'Kanki','kanki@lab.com','LabMVP_Kanki_Salt_2026','89ac385e24fca577f0c84da8e7f06fcf156fda7419724c54821bde44efb997fe',1,'2026-11-23 00:08:24','admin'),(6,'Ibe','ibe@lab.com','LabMVP_Ibe_Salt_2026','1082cee4c1ede97036664948b38b516a017114c4b05ec7bf48aa725f6a14545b',1,'2026-11-23 00:08:24','lector');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'lab_mvp'
--

--
-- Dumping routines for database 'lab_mvp'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_actualizar_producto` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_actualizar_producto`(
    IN p_id INT,
    IN p_nombre VARCHAR(100),
    IN p_precio DECIMAL(10,2),
    IN p_cantidad INT,
    IN p_imagen LONGBLOB
)
BEGIN
    UPDATE productos
    SET nombre = p_nombre,
        precio = p_precio,
        cantidad = p_cantidad,
        imagen = p_imagen
    WHERE id = p_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_cambiar_estado_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_cambiar_estado_usuario`(
    IN p_id INT,
    IN p_activo TINYINT
)
BEGIN
    DECLARE v_rol_actual VARCHAR(20);
    DECLARE v_estado_actual TINYINT;
    DECLARE v_admins INT;

    IF p_activo NOT IN (0, 1) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Estado no valido.';
    END IF;

    SELECT rol, activo
    INTO v_rol_actual, v_estado_actual
    FROM usuarios
    WHERE id = p_id;

    IF v_rol_actual IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Usuario no encontrado.';
    END IF;

    IF v_rol_actual = 'admin'
       AND v_estado_actual = 1
       AND p_activo = 0 THEN

        SELECT COUNT(*)
        INTO v_admins
        FROM usuarios
        WHERE rol = 'admin' AND activo = 1;

        IF v_admins <= 1 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Debe existir al menos un administrador activo.';
        END IF;
    END IF;

    UPDATE usuarios
    SET activo = p_activo
    WHERE id = p_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_cambiar_rol_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_cambiar_rol_usuario`(
    IN p_id INT,
    IN p_rol VARCHAR(20)
)
BEGIN
    DECLARE v_rol_actual VARCHAR(20);
    DECLARE v_activo TINYINT;
    DECLARE v_admins INT;

    IF p_rol NOT IN ('admin', 'editor', 'lector') THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Rol no valido.';
    END IF;

    SELECT rol, activo
    INTO v_rol_actual, v_activo
    FROM usuarios
    WHERE id = p_id;

    IF v_rol_actual IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Usuario no encontrado.';
    END IF;

    IF v_rol_actual = 'admin'
       AND v_activo = 1
       AND p_rol <> 'admin' THEN

        SELECT COUNT(*)
        INTO v_admins
        FROM usuarios
        WHERE rol = 'admin' AND activo = 1;

        IF v_admins <= 1 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Debe existir al menos un administrador activo.';
        END IF;
    END IF;

    UPDATE usuarios
    SET rol = p_rol
    WHERE id = p_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_eliminar_producto` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_eliminar_producto`(IN p_id INT)
BEGIN
    DELETE FROM productos
    WHERE id = p_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_insertar_producto` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertar_producto`(
    IN p_nombre VARCHAR(100),
    IN p_precio DECIMAL(10,2),
    IN p_cantidad INT,
    IN p_imagen LONGBLOB
)
BEGIN
    INSERT INTO productos (nombre, precio, cantidad, imagen)
    VALUES (p_nombre, p_precio, p_cantidad, p_imagen);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_insertar_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertar_usuario`(
    IN p_nombre        VARCHAR(100),
    IN p_correo        VARCHAR(100),
    IN p_salt          VARCHAR(50),
    IN p_password_hash CHAR(64)
)
BEGIN
    INSERT INTO usuarios (nombre, correo, salt, password_hash)
    VALUES (p_nombre, p_correo, p_salt, p_password_hash);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_insertar_usuario_con_rol` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertar_usuario_con_rol`(
    IN p_nombre VARCHAR(255),
    IN p_correo VARCHAR(255),
    IN p_salt VARCHAR(255),
    IN p_password_hash VARCHAR(255),
    IN p_rol VARCHAR(20)
)
BEGIN
    IF p_rol NOT IN ('admin', 'editor', 'lector') THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Rol no valido.';
    END IF;

    INSERT INTO usuarios (
        nombre, correo, salt, password_hash, rol
    )
    VALUES (
        p_nombre, p_correo, p_salt, p_password_hash, p_rol
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_listar_productos` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_listar_productos`(IN p_filtro VARCHAR(100))
BEGIN
    SELECT id, nombre, precio, cantidad, imagen
    FROM productos
    WHERE p_filtro IS NULL
       OR p_filtro = ''
       OR id LIKE CONCAT('%', p_filtro, '%')
       OR nombre LIKE CONCAT('%', p_filtro, '%')
       OR precio LIKE CONCAT('%', p_filtro, '%')
       OR cantidad LIKE CONCAT('%', p_filtro, '%')
    ORDER BY id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_listar_usuarios` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_listar_usuarios`()
BEGIN
    SELECT id, nombre, correo, rol, activo
    FROM usuarios
    ORDER BY nombre;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_obtener_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_obtener_usuario`(IN p_correo VARCHAR(150))
BEGIN
    SELECT id, nombre, correo, salt, password_hash, rol, activo
    FROM usuarios
    WHERE correo = p_correo;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-11-23  1:51:41

-- --------
-- USUARIOS DE MYSQL: Kanki (superusuario) e Ibe (privilegios minimos)
-- Importa este archivo con root, porque crear usuarios y dar permisos solo lo hace un administrador
-- Los usuarios de MySQL no viven en lab_mvp sino en la base del sistema "mysql"

-- --------
-- Kanki: superusuario, todos los privilegios sobre todo el servidor
CREATE USER IF NOT EXISTS 'Kanki'@'localhost' IDENTIFIED BY 'Kanki_2026*';
GRANT ALL PRIVILEGES ON *.* TO 'Kanki'@'localhost' WITH GRANT OPTION;

-- --------
-- Ibe: privilegios minimos, solo ejecuta estos 5 procedimientos de lab_mvp
-- (login, registro de lector, listar, insertar y actualizar productos)
-- No puede borrar productos, ni listar usuarios, ni cambiar roles o estados
CREATE USER IF NOT EXISTS 'Ibe'@'localhost' IDENTIFIED BY 'Ibe_2026*';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_obtener_usuario TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_insertar_usuario TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_listar_productos TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_insertar_producto TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_actualizar_producto TO 'Ibe'@'localhost';

-- --------
-- Cuentas de la APP (ya vienen en la tabla usuarios de este archivo)
-- kanki@lab.com / Kanki_2026*  -> rol admin  -> la app se conecta a MySQL como Kanki
-- ibe@lab.com   / Ibe_2026*    -> rol lector -> la app se conecta a MySQL como Ibe

-- --------
-- Donde ver los usuarios de MySQL: en Workbench, menu Server -> Users and Privileges
-- o con esta consulta
SELECT user, host FROM mysql.user WHERE user IN ('Kanki', 'Ibe');

-- --------
-- Ver los permisos de cada uno
SHOW GRANTS FOR 'Kanki'@'localhost';
SHOW GRANTS FOR 'Ibe'@'localhost';

-- --------
-- prueba con Kanki (conexion nueva en Workbench): todo funciona
-- SELECT * FROM lab_mvp.usuarios;
-- CALL lab_mvp.sp_listar_usuarios();

-- prueba con Ibe (conexion nueva en Workbench):
-- SELECT * FROM lab_mvp.usuarios;            -- debe dar error "SELECT command denied"
-- CALL lab_mvp.sp_listar_productos('');      -- debe funcionar
-- CALL lab_mvp.sp_eliminar_producto(1);      -- debe dar error "EXECUTE command denied"
