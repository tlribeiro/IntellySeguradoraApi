----------------------------------------------------
-- Projeto: Api para Login NetCore
-- Descrição: Script para carregar os dados no Mysql
--            via Docker Compose
-- Autor: Thiago Ribeiro
-- Data: 03/08/2019
----------------------------------------------------

CREATE TABLE Users (
    UserId CHAR(36) NOT NULL,
    UserName VARCHAR(100) NOT NULL,
	UserEmail VARCHAR(100) NOT NULL,
    UserRole VARCHAR(100) NULL,
	UserLinkedin VARCHAR(100) NULL,
	UserLogin VARCHAR(100) NULL,
	UserPassword VARCHAR(300) NULL,
    PRIMARY KEY (UserId)
)  ENGINE=INNODB;


-- Realiza o insert de dados.
insert into Users values ('aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee', 'Thiago Ribeiro', 'tlribeiro@outlook.com', 'Software Architect', 'https://', 'thiago', '12345');
