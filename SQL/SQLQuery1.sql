
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
