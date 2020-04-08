
CREATE DATABASE PROJETOACADEMIA

USE PROJETOACADEMIA

create table dbo.TipoPlano(
id int primary key identity(1,1) not null,
descricao varchar(100) not null,
valor money not null
)

create table dbo.Aluno(
id int primary key identity(1,1) not null,
identidade varchar(20) not null,
cpf varchar(20) not null,
nome varchar(200) not null,
endereco varchar(1000) not null,
tipoPlano int not null,
CONSTRAINT fk_tipoPlanoAluno FOREIGN KEY (tipoPlano) REFERENCES TipoPlano(id)
)

create table dbo.Ferias(
id int primary key identity(1,1) not null,
idAluno int not null,
dataInicio smalldatetime not null,
dataFim smalldatetime not null,
dataBaseFimPagamento smalldatetime not null,
CONSTRAINT fk_idAlunoFerias FOREIGN KEY (idAluno) REFERENCES Aluno(id)
)

create table dbo.Pagamento(
id int primary key identity(1,1) not null,
idAluno int not null,
valor money not null,
dataProxima smalldatetime not null,
dataBaseInicio smalldatetime not null,
dataBaseFim smalldatetime not null,
CONSTRAINT fk_idAlunoPagamento FOREIGN KEY (idAluno) REFERENCES Aluno(id)
)

create table dbo.TipoPerfil(
id int primary key identity(1,1) not null,
descricao varchar(100) not null
)

create table dbo.Empregado(
id int primary key identity(1,1) not null,
identidade varchar(20) not null,
cpf varchar(20) not null,
nome varchar(200) not null,
aulaGrupo bit not null,
aulaMusculacao bit not null,
senha varchar(20) not null,
idPerfil int not null,
CONSTRAINT fk_idPerfilEmpregado FOREIGN KEY (idPerfil) REFERENCES TipoPerfil(id)
)

create table dbo.Aula(
id int primary key identity(1,1) not null,
nome varchar(100) not null,
horaInicio varchar(5) not null,
horaFim varchar(5) not null,
diaSemana int not null,
sala varchar(100) not null,
idInstrutor int not null,
CONSTRAINT fk_idInstrutor FOREIGN KEY (idInstrutor) REFERENCES Empregado(id)
)

create table dbo.RegistraPresenca(
id int primary key identity(1,1) not null,
dataHora datetime not null,
idAula int not null,
idAluno int not null,
CONSTRAINT fk_idAulaRegistro FOREIGN KEY (idAula) REFERENCES Aula(id),
CONSTRAINT fk_idAlunoRegistro FOREIGN KEY (idAluno) REFERENCES Aluno(id)
)

create table dbo.AvaliacaoFisica(
id int primary key identity(1,1) not null,
dataRealizacao datetime not null,
anamnese varchar(1000) not null,
dobrasCutaneas varchar(1000) not null,
exameErgonometrico varchar(1000) not null,
idFisioterapeuta int not null,
idAluno int not null,
CONSTRAINT fk_idFisioterapeuta FOREIGN KEY (idFisioterapeuta) REFERENCES Empregado(id),
CONSTRAINT fk_idAlunoAvaliacao FOREIGN KEY (idAluno) REFERENCES Aluno(id)
)

INSERT INTO TipoPlano VALUES ('MENSAL',69.90)
INSERT INTO TipoPlano VALUES ('ANUAL',600.00)
 
INSERT INTO TipoPerfil VALUES('PROFESSOR')
INSERT INTO TipoPerfil VALUES('RECEPCIONISTA')

INSERT INTO Empregado VALUES (12345678,12332112345,'JOSE LACERDA', 0,1,'joselacerda',1)
INSERT INTO Empregado VALUES (12345677,12332112344,'MARIA NEUZA', 0,0,'marianeuza',2)















