CREATE TABLE `users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sys_id` VARCHAR(36) NOT NULL,
  `email` VARCHAR(255) NULL,
  `dttm_registration` DATETIME NULL,
  `dttm_last_login` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `Id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `sys_id_UNIQUE` (`sys_id` ASC)
);