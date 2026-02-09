
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
	Nome varchar(50) not null,
	Senha Nvarchar(50) not null,
	CONSTRAINT pk_Id PRIMARY KEY (Id)
);

GO

CREATE TABLE Tela_Permissao(
	Id int IDENTITY(1,1),
	Id_Usuario int not null,
	Home Nvarchar(3),
	ContabilizarTreino Nvarchar(3),
	Relatorio Nvarchar(3),
	Treino Nvarchar(3),
	CONSTRAINT pk_Id_Tela_Permissao PRIMARY KEY (Id),
	CONSTRAINT fk_Id_Usuario FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
);

INSERT INTO Tela_Permissao(Id_Usuario,Home,ContabilizarTreino,Relatorio,Treino)
VALUES(1,'Sim','Sim','Sim','Sim')

GO 

SELECT U.Nome ,U.Id, Tp.Id_Usuario, Tp.Home, Tp.ContabilizarTreino, Tp.Relatorio, Tp.Treino FROM Usuario U
INNER JOIN Tela_Permissao Tp ON Tp.Id_Usuario = U.Id
