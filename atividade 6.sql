CREATE DATABASE estoque;

CREATE TABLE produto(
id_produto INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
codigo NVARCHAR(10) NOT NULL,
preco DECIMAL(10,4) NOT NULL,
);

SELECT * FROM produto;

CREATE TABLE categoria(
id_categoria INT IDENTITY (1,1) PRIMARY KEY,
codigo NVARCHAR (10),
nome NVARCHAR(255),

FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria)
);

SELECT * FROM categoria;