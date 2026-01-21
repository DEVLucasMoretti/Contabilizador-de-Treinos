CREATE TABLE Treino (
	Id int IDENTITY(1,1),
	Quantidade_Caloria decimal(6,2),
	Treino_Do_Dia varchar(150),
	Dia_Da_Semana varchar(30),
	Data Date not null
)