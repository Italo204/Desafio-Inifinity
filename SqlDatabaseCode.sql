CREATE LOGIN freteAdm WITH PASSWORD = '16D7Af8P';

USE FreteDatabase;
CREATE USER freteAdm FOR LOGIN freteAdm;

ALTER ROLE db_owner ADD MEMBER freteAdm;

SELECT * FROM sys.server_principals;

CREATE TABLE Veiculo (
    VeiculoID BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PesoVeiculo INT NOT NULL,
    Placa CHAR(7) NOT NULL,
    RENAVAM CHAR(11) UNIQUE NOT NULL
)


CREATE TABLE Usuario (
    UsuarioID BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Senha VARCHAR(50) NOT NULL,
    Telefone VARCHAR(14)
)

CREATE TABLE Frete (
    FreteID BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PesoVeiculo INT NOT NULL,
    Distancia INT NOT NULL,
    CidadeOrigem VARCHAR(255),
    CidadeDestino VARCHAR(255),
    ValorFrete DECIMAL(10,2),
    ValorTaxa DECIMAL (10,2),
    UsuarioID BIGINT NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID)
)

USE FreteDatabase;

ALTER TABLE Frete
ALTER COLUMN CidadeOrigem VARCHAR(255) NOT NULL;

ALTER TABLE Frete
ALTER COLUMN CidadeDestino VARCHAR(255) NOT NULL;

ALTER TABLE Frete
ALTER COLUMN ValorFrete DECIMAL(10,2) NOT NULL;


ALTER TABLE Frete
ALTER COLUMN ValorTaxa DECIMAL(10,2) NOT NULL;