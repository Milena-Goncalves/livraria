DROP DATABASE IF EXISTS bd_leitura;
CREATE DATABASE bd_leitura;

USE bd_leitura;

CREATE TABLE usuarios (
    id INTEGER 						PRIMARY KEY AUTO_INCREMENT,
    nome 							VARCHAR(250) NOT NULL,
    email 							VARCHAR(50) NOT NULL UNIQUE,
    senha 							VARCHAR(255) NOT NULL,
    data_criacao 					DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE livros (
		id 							INTEGER PRIMARY KEY AUTO_INCREMENT,
		titulo 						VARCHAR(200) NOT NULL,
		autor 						VARCHAR(100) NOT NULL,
		editora	 					VARCHAR(100) NOT NULL,
		genero						VARCHAR(50) NOT NULL,
        sinopse						VARCHAR(5000) NOT NULL
        
);

CREATE TABLE status_livro_usuario (
    id 								INTEGER PRIMARY KEY AUTO_INCREMENT,
    usuario_id 						INTEGER NOT NULL,
    livro_id 						INTEGER NOT NULL,
    status 							ENUM('ja_li', 'quero_ler', 'lendo') NOT NULL,


    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (livro_id) REFERENCES livros(id) ON DELETE CASCADE
);

CREATE TABLE livros_favoritos (
    usuario_id 						INTEGER NOT NULL,
    livro_id 						INTEGER NOT NULL,


    PRIMARY KEY (usuario_id, livro_id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (livro_id) REFERENCES livros(id) ON DELETE CASCADE
);

CREATE TABLE comentario (
    id                          	INTEGER PRIMARY KEY AUTO_INCREMENT,
    usuario_id                  	INTEGER NOT NULL,
    livros_id                   	INTEGER,
    livro_titulo                	VARCHAR(50) NOT NULL,
    nome                        	VARCHAR(250) NOT NULL,
    comentarioTexto                 VARCHAR(2000) NOT NULL,
    
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (livros_id) REFERENCES livros(id) ON UPDATE CASCADE
);


CREATE TABLE post (
    id 								INTEGER PRIMARY KEY AUTO_INCREMENT,
    usuario_id 						INTEGER NOT NULL,
    conteudo 						VARCHAR(2000) NOT NULL,
    data_postagem 					DATETIME DEFAULT CURRENT_TIMESTAMP,
    editado 						BOOLEAN DEFAULT FALSE,

    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE
);

CREATE TABLE pedido (
		id 							INTEGER PRIMARY KEY AUTO_INCREMENT,
		livro 						VARCHAR(200) NOT NULL,
		genero 						VARCHAR(100) NOT NULL,
		nome	 					VARCHAR(100) NOT NULL,
		email						VARCHAR(50)
        
);

DELIMITER $$

 

    CREATE PROCEDURE Genero (IN getGenero TEXT)
BEGIN
    SELECT titulo, autor, editora, genero, sinopse
    FROM livros
    WHERE genero LIKE getGenero
    ORDER BY titulo ASC;
END$$
    

    
