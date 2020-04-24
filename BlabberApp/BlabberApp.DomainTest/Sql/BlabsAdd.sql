CREATE TABLE `blabs` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sys_id` VARCHAR(36) NOT NULL,
  `message` VARCHAR(255) NULL,
  `dttm_created` DATETIME NOT NULL,
  `user_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `sys_id_UNIQUE` (`sys_id` ASC)
);