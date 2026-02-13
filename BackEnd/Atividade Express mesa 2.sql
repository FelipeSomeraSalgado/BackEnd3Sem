CREATE DATABASE db_express;

USE db_express;

CREATE TABLE cliente(
id_cliente INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
CPF CHAR(11) UNIQUE NOT NULL,
CNPJ CHAR(14) UNIQUE NOT NULL,
Telefone CHAR(15) NOT NULL,
Endereco NVARCHAR(255) NOT NULL,

);

SELECT * FROM cliente;

CREATE TABLE pedido(
id_pedido INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
data_pedido DATE NOT NULL,
CPF NVARCHAR(255) NOT NULL,
categoria NVARCHAR (255) NOT NULL,
descricao NVARCHAR (255) NOT NULL,
status_pedido NVARCHAR(255) NOT NULL,
id_cliente INT,
id_entregador INT,
id_rastreamento INT,

FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente),
FOREIGN KEY (id_entregador) REFERENCES entregador (id_entregador),
FOREIGN KEY (id_rastreamento) REFERENCES rastreamento (id_rastreamento)
);

SELECT * FROM pedido;

CREATE TABLE produto(
id_produto INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
descricao NVARCHAR(255) NOT NULL,
categoria NVARCHAR (255) NOT NULL,
preco DECIMAL(10,2) NOT NULL,
);

SELECT * FROM produto;

CREATE TABLE rastreamento(
id_rastreamento INT IDENTITY (1,1) Primary Key,
historico_de_status NVARCHAR (255) NOT NULL,
atualizacao NVARCHAR(255) NOT NULL,
data_rastreamento DATE NOT NULL,
hora TIME NOT NULL,
localizacao NVARCHAR(255) NOT NULL,
);

SELECT * FROM rastreamento;

CREATE TABLE entregador(
id_entregador INT IDENTITY (1,1) Primary Key,
nome NVARCHAR (255) NOT NULL,
Cpf CHAR(11) UNIQUE NOT NULL,
placa CHAR(7) UNIQUE NOT NULL,
veiculo NVARCHAR(255) NOT NULL,
id_cliente INT NOT NULL,

FOREIGN KEY (id_cliente) REFERENCES cliente (id_cliente)
);

SELECT * FROM entregador;

CREATE TABLE tb_pedido_produto(
  id_pedido INT NOT NULL
 ,id_produto INT NOT NULL

  PRIMARY KEY(id_pedido,id_produto)
);

SELECT * FROM tb_pedido_produto;