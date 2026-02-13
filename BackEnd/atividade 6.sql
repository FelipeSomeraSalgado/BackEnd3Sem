CREATE DATABASE estoque;

USE estoque;

CREATE TABLE produto(
id_produto INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
preco DECIMAL(10,2) NOT NULL,
id_categoria INT

FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria)
);

SELECT * FROM produto;

CREATE TABLE categoria(
id_categoria INT IDENTITY (1,1) PRIMARY KEY,
nome NVARCHAR(255)
);

SELECT * FROM categoria;

