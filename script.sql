--1 º Passo
CREATE DATABASE RegisterAPI;

--2 º Passo
USE RegisterAPI;

--3 º Passo
CREATE TABLE Client (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [Name] NVARCHAR(255),
    [Email] NVARCHAR(255),
    [Phone] NVARCHAR(50),
    [Document] NVARCHAR(50),
    [DateBirth] DATETIME
);

CREATE TABLE Integration (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [NameIntegration] NVARCHAR(255),
    [UrlBase] NVARCHAR(255),
    [Token] NVARCHAR(50)
)

--4 º Passo
--Caso queira testar o "SyncClients", é necessário inserir de forma separada as integrações no banco, pegar o Guid gerado e passar no endpoint.
INSERT INTO Integration VALUES (NEWID(), 'FirstIntegration', 'http://localhost:7056/', 'E1ACD6A2-ABF2-4CFB-B200-180BB9A1EDB2'),(NEWID(), 'SecondIntegration', 'http://localhost:7057/', 'A8F6F570-6AB8-497C-A69E-670721B1F551')
SELECT * FROM Integration