
USE BDFIT;

GO

CREATE TABLE Treino (
	Id int IDENTITY(1,1),
	Data Date not null,
	Dia_Da_Semana varchar(30) not null, 
	Quantidade_Caloria decimal(6,2),
	Treino_Do_Dia varchar(150),

	CONSTRAINT Id_Treino PRIMARY KEY (Id)
);

GO

CREATE TABLE Usuario 
(
	Id int IDENTITY(1,1),
	Nome Nvarchar(50) UNIQUE not null,
	Senha Nvarchar(50) not null,
	CONSTRAINT pk_Id PRIMARY KEY (Id)
);

GO
INSERT INTO Usuario(Nome, Senha) VALUES ('guest','guest')
GO

CREATE TABLE Usuario_Mestre (
    Id   INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL UNIQUE,
	CONSTRAINT fk_Usuario_Mestre_Nome FOREIGN KEY (Nome) REFERENCES Usuario (Nome)
);
GO
INSERT INTO Usuario_Mestre VALUES ('guest')
GO


CREATE  TABLE Tela_Permissao(
	Id int IDENTITY(1,1),
	Id_Usuario int not null,
	Tela Nvarchar(100),
	Acessivel BIT,
	CONSTRAINT pk_Id_Tela_Permissao PRIMARY KEY (Id),
	CONSTRAINT fk_Id_Usuario FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
);

SELECT * FROM  Tela_Permissao
GO

INSERT INTO Tela_Permissao(Id_Usuario,Tela,Acessivel) VALUES(2,'Home',1)
INSERT INTO Tela_Permissao(Id_Usuario,Tela,Acessivel) VALUES(2,'ContabilizarTreino',1)
INSERT INTO Tela_Permissao(Id_Usuario,Tela,Acessivel) VALUES(2,'Relatorio',1)
INSERT INTO Tela_Permissao(Id_Usuario,Tela,Acessivel) VALUES(2,'Treino',1)

GO 

SELECT U.Nome ,U.Id, Tp.Id_Usuario, Tp.Tela FROM Usuario U
INNER JOIN Tela_Permissao Tp ON Tp.Id_Usuario = U.Id
WHERE Tp.Acessivel = 1

SELECT * FROM Usuario