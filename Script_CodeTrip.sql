create database Codetrip;

use Codetrip;

CREATE TABLE Usuario (
Id_Usuario INT AUTO_INCREMENT,
Nome_Usuario VARCHAR(100),
Email_Usuario VARCHAR(255) UNIQUE,
Senha_Usuario VARCHAR(255),
role enum ("Colaborador","Comum", "Admin"),
ativo tinyint(1) default 1 ,
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Usuario));

CREATE TABLE Estado (
UF_Estado CHAR(2),
Nome_Estado VARCHAR(50),
criado_Em datetime default current_timestamp,
PRIMARY KEY (UF_Estado));

CREATE TABLE Cidade (
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Cidade_Nome, UF_Estado),
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Cliente (
Id_Cli INT auto_increment,
Nome_Cli VARCHAR(100),
Email_Cli VARCHAR(255),
Data_Nasc_Cli DATE,
CPF_Cli CHAR(11) UNIQUE,
Telefone_Cli VARCHAR(20),
Logradouro_Cli VARCHAR(100),
Numero_Cli VARCHAR(10),
Bairro_Cli VARCHAR(60),
Complemento_Cli VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
ativo tinyint(1) default 1 ,
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Cli),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome));

CREATE TABLE Transporte (
Id_Transp INT AUTO_INCREMENT,
Tipo_Transp VARCHAR(100),
UF_Estado CHAR(2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Transp),
ativo tinyint(1) default 1 ,
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Tipo_Hospedagem (
Id_Tipo_Hospedagem INT AUTO_INCREMENT,
Desc_Hospedagem VARCHAR(50),
Capacidade_Hospedagem int,
Valor_Hospedagem DECIMAL(10, 2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Tipo_Hospedagem));

CREATE TABLE Tipo_Pensao (
Id_Pensao INT AUTO_INCREMENT,
Desc_Pensao VARCHAR(50),
Valor_Pensao DECIMAL(10, 2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Pensao));

CREATE TABLE Origem_Viagem (
Id_Origem INT AUTO_INCREMENT,
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Origem),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade(Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Destino_Viagem (
Id_Destino INT AUTO_INCREMENT,
Desc_Destino VARCHAR(50),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
Valor_Destino DECIMAL(10, 2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Destino),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado));

CREATE TABLE Pagamento (
Id_Pagamento INT AUTO_INCREMENT,
Desc_Pagamento VARCHAR(30),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Pagamento));

CREATE TABLE End_Transporte (
Id_End_Transporte INT AUTO_INCREMENT,
Id_Transp INT,
Logradouro_End_Transporte VARCHAR(100),
Numero_End_Transporte VARCHAR(10),
Bairro_End_Transporte VARCHAR(60),
Complemento_End_Transporte VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_End_Transporte),
FOREIGN KEY (Id_Transp) REFERENCES Transporte(Id_Transp));

CREATE TABLE Hospedagem (
Id_Hospedagem INT AUTO_INCREMENT,
Nome_Hospedagem VARCHAR(50),
Id_Tipo_Hospedagem int,
Id_Pensao int,
Logradouro_Endereco_Hospedagem VARCHAR(100),
Numero_Endereco_Hospedagem VARCHAR(10),
Bairro_Endereco_Hospedagem VARCHAR(60),
Complemento_Endereco_Hospedagem VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
ativo tinyint(1) default 1 ,
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Hospedagem),
FOREIGN KEY (Id_Tipo_Hospedagem) REFERENCES Tipo_Hospedagem (Id_Tipo_Hospedagem),
FOREIGN KEY (Id_Pensao) REFERENCES Tipo_Pensao (Id_Pensao),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado));

CREATE TABLE Passeio (
Id_Passeio INT AUTO_INCREMENT,
Nome_Passeio VARCHAR(100) NOT NULL,
Desc_Passeio VARCHAR(300),
Valor_Passeio DECIMAL(10,2) NOT NULL,
Duracao_Passeio VARCHAR(50),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
ativo tinyint(1) default 1 ,
criado_Em DATETIME DEFAULT CURRENT_TIMESTAMP,
PRIMARY KEY (Id_Passeio),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado));

CREATE TABLE Pedido (
Id_Pedido INT AUTO_INCREMENT,
Id_Usuario INT NOT NULL,
CPF_Cli CHAR(11) NOT NULL,
Id_Origem INT NOT NULL,
Id_Destino INT NOT NULL,
Data_Inicio DATE NOT NULL,
Data_Fim DATE NOT NULL,
Id_Transp INT NOT NULL,
Id_End_Transporte INT NOT NULL,
Id_Hospedagem INT NOT NULL,
Id_Pagamento INT NOT NULL,
Id_Passeio INT NOT NULL,
ativo tinyint(1) default 1 ,
criado_Em datetime default current_timestamp,
PRIMARY KEY (Id_Pedido),
FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario),
FOREIGN KEY (CPF_Cli) REFERENCES Cliente(CPF_Cli),
FOREIGN KEY (Id_Origem) REFERENCES Origem_Viagem (Id_Origem),
FOREIGN KEY (Id_Destino) REFERENCES Destino_Viagem (Id_Destino),
FOREIGN KEY (Id_End_Transporte) REFERENCES End_Transporte(Id_End_Transporte),
FOREIGN KEY (Id_Transp) REFERENCES Transporte(Id_Transp),
FOREIGN KEY (Id_Hospedagem) REFERENCES Hospedagem(Id_Hospedagem),
FOREIGN KEY (Id_Pagamento) REFERENCES Pagamento(Id_Pagamento),
FOREIGN KEY (Id_Passeio) REFERENCES Passeio (Id_Passeio));


-- INSERTS

-- USUARIO
INSERT INTO Usuario (Nome_Usuario, Email_Usuario, Senha_Usuario, role) VALUES 
('Administrador do Sistema', 'admin@codetrip.com', 'admin123', 'Admin');

-- ESTADOS
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('AC', 'Acre');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('AL', 'Alagoas');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('AP', 'Amapá');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('AM', 'Amazonas');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('BA', 'Bahia');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('CE', 'Ceará');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('DF', 'Distrito Federal');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('ES', 'Espírito Santo');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('GO', 'Goiás');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('MA', 'Maranhão');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('MT', 'Mato Grosso');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('MS', 'Mato Grosso do Sul');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('MG', 'Minas Gerais');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('PA', 'Pará');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('PB', 'Paraíba');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('PR', 'Paraná');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('PE', 'Pernambuco');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('PI', 'Piauí');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('RJ', 'Rio de Janeiro');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('RN', 'Rio Grande do Norte');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('RS', 'Rio Grande do Sul');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('RO', 'Rondônia');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('RR', 'Roraima');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('SC', 'Santa Catarina');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('SP', 'São Paulo');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('SE', 'Sergipe');
INSERT INTO Estado (UF_Estado, Nome_Estado) VALUES ('TO', 'Tocantins');

-- CIDADES
-- Acre (AC)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Rio Branco', 'AC');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Cruzeiro do Sul', 'AC');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Sena Madureira', 'AC');
-- Alagoas (AL)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Maceió', 'AL');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Arapiraca', 'AL');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Rio Largo', 'AL');
-- Amapá (AP)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Macapá', 'AP');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Santana', 'AP');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Laranjal do Jari', 'AP');
-- Amazonas (AM)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Manaus', 'AM');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Parintins', 'AM');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Itacoatiara', 'AM');
-- Bahia (BA)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Salvador', 'BA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Feira de Santana', 'BA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Vitória da Conquista', 'BA');
-- Ceará (CE)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Fortaleza', 'CE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Caucaia', 'CE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Juazeiro do Norte', 'CE');
-- Distrito Federal (DF)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Brasília', 'DF');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Ceilândia', 'DF');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Taguatinga', 'DF');
-- Espírito Santo (ES)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Vitória', 'ES');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Vila Velha', 'ES');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Serra', 'ES');
-- Goiás (GO)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Goiânia', 'GO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Aparecida de Goiânia', 'GO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Anápolis', 'GO');
-- Maranhão (MA)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('São Luís', 'MA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Imperatriz', 'MA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Timon', 'MA');
-- Mato Grosso (MT)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Cuiabá', 'MT');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Várzea Grande', 'MT');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Rondonópolis', 'MT');
-- Mato Grosso do Sul (MS)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Campo Grande', 'MS');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Dourados', 'MS');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Três Lagoas', 'MS');
-- Minas Gerais (MG)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Belo Horizonte', 'MG');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Uberlândia', 'MG');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Contagem', 'MG');
-- Pará (PA)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Belém', 'PA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Ananindeua', 'PA');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Santarém', 'PA');
-- Paraíba (PB)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('João Pessoa', 'PB');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Campina Grande', 'PB');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Santa Rita', 'PB');
-- Paraná (PR)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Curitiba', 'PR');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Londrina', 'PR');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Maringá', 'PR');
-- Pernambuco (PE)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Recife', 'PE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Jaboatão dos Guararapes', 'PE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Olinda', 'PE');
-- Piauí (PI)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Teresina', 'PI');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Parnaíba', 'PI');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Picos', 'PI');
-- Rio de Janeiro (RJ)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Rio de Janeiro', 'RJ');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('São Gonçalo', 'RJ');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Duque de Caxias', 'RJ');
-- Rio Grande do Norte (RN)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Natal', 'RN');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Mossoró', 'RN');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Parnamirim', 'RN');
-- Rio Grande do Sul (RS)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Porto Alegre', 'RS');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Caxias do Sul', 'RS');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Pelotas', 'RS');
-- Rondônia (RO)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Porto Velho', 'RO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Ji-Paraná', 'RO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Ariquemes', 'RO');
-- Roraima (RR)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Boa Vista', 'RR');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Rorainópolis', 'RR');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Caracaraí', 'RR');
-- Santa Catarina (SC)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Joinville', 'SC');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Florianópolis', 'SC');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Blumenau', 'SC');
-- São Paulo (SP)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('São Paulo', 'SP');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Guarulhos', 'SP');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Campinas', 'SP');
-- Sergipe (SE)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Aracaju', 'SE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Nossa Senhora do Socorro', 'SE');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Lagarto', 'SE');
-- Tocantins (TO)
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Palmas', 'TO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Araguaína', 'TO');
INSERT INTO Cidade (Cidade_Nome, UF_Estado) VALUES ('Gurupi', 'TO');


-- CLIENTES
INSERT INTO Cliente (Nome_Cli, Email_Cli, Data_Nasc_Cli, CPF_Cli, Telefone_Cli, Logradouro_Cli, Numero_Cli, Bairro_Cli, Complemento_Cli, Cidade_Nome, UF_Estado) VALUES 
('João Silva', 'joao.silva@email.com', '1985-03-15', '12345678901', '(11) 99999-1234', 'Rua das Flores', '123', 'Centro', 'Apto 101', 'São Paulo', 'SP'),
('Maria Santos', 'maria.santos@email.com', '1990-07-22', '23456789012', '(21) 98888-5678', 'Avenida Brasil', '456', 'Copacabana', 'Casa 2', 'Rio de Janeiro', 'RJ'),
('Pedro Oliveira', 'pedro.oliveira@email.com', '1988-11-30', '34567890123', '(31) 97777-9012', 'Rua da Paz', '789', 'Savassi', 'Sala 305', 'Belo Horizonte', 'MG'),
('Ana Costa', 'ana.costa@email.com', '1995-05-10', '45678901234', '(41) 96666-3456', 'Rua XV de Novembro', '321', 'Batel', 'Bloco B', 'Curitiba', 'PR'),
('Carlos Souza', 'carlos.souza@email.com', '1982-12-05', '56789012345', '(51) 95555-7890', 'Avenida Protásio Alves', '654', 'Moinhos de Vento', 'Conjunto 202', 'Porto Alegre', 'RS');
select * from usuario;

-- TRANSPORTE
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'AC');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'AL');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'AP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'AM');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'BA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'CE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'DF');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'ES');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'GO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'MA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'MT');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'MS');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'MG');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'PA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'PB');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'PR');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'PE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'PI');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'RJ');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'RN');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'RS');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'RO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'RR');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'SC');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'SE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Táxi', 'TO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'AC');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'AL');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'AP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'AM');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'BA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'CE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'DF');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'ES');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'GO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'MA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'MT');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'MS');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'MG');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'PA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'PB');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'PR');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'PE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'PI');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'RJ');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'RN');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'RS');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'RO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'RR');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'SC');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'SE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Ônibus', 'TO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'DF');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'RJ');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'MG');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'PE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'BA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'RJ');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'PR');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'CE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'SC');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'PA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'GO');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'RS');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'ES');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'AM');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'MT');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'AL');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Avião', 'RN');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'SP');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'RJ'); 
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'BA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'AL');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'PE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'CE');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'RN');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'PA');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'AM');
INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES ('Navio', 'SC');
-- Obs. Taxi - Todos os estados
-- Obs. Onibus - Todos os estados
-- Obs. Avião - Principais aeroportos do Brasil
-- Obs. Navio - Principais portos de passageiros do Brasil
-- Aeroporto Internacional de Natal (NAT), Aeroporto Internacional de Maceió (MCZ), Aeroporto Internacional de Cuiabá (CGB), Aeroporto Internacional de Manaus (MAO), Aeroporto Internacional de Vitória (VIX), Aeroporto Internacional de Porto Alegre (POA), Aeroporto Internacional de Goiânia (GYN), Aeroporto Internacional de Belém (BEL), Aeroporto Internacional de Florianópolis (FLN), Aeroporto Internacional de Fortaleza (FOR), Aeroporto Internacional de Curitiba (CWB), Aeroporto Santos Dumont (SDU), Aeroporto Internacional de Salvador (SSA), Aeroporto Internacional do Recife (REC), Aeroporto Internacional de Confins (CNF), Aeroporto Internacional de Viracopos (VCP), Aeroporto Internacional do Galeão (GIG),  Aeroporto Internacional de Brasília (BSB), Aeroporto de Congonhas (CGH), Aeroporto Internacional de Guarulhos (GRU)
-- Porto de Itajaí, Porto de Manaus, Porto de Belém, Porto de Natal, Porto de Fortaleza, Porto de Recife, Porto de Maceió, Porto de Salvador, Pier Mauá (Rio de Janeiro), Porto de Santos

-- TIPOS DE HOSPEDAGENS
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Single (Solteiro)', 1, 100.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Double (Solteiro)', 2, 150.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Double (Casal)', 2, 150.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Triple (Solteiro)', 3, 250.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Quadruplo (Solteiro)', 4, 300.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Família', 6, 400.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Corporativo Single', 1, 130.00);
INSERT INTO Tipo_Hospedagem (Desc_Hospedagem, Capacidade_Hospedagem, Valor_Hospedagem) VALUES ('Corporativo Double', 2, 150.00);

-- TIPO DE PENSAO
INSERT INTO Tipo_Pensao (Desc_Pensao, Valor_Pensao) VALUES ('Nenhuma', 0.00);
INSERT INTO Tipo_Pensao (Desc_Pensao, Valor_Pensao) VALUES ('Meia pensão (Café da manhã e Almoço)', 100.00);
INSERT INTO Tipo_Pensao (Desc_Pensao, Valor_Pensao) VALUES ('Pensão completa (Café, Almoço e Jantar)', 180.00);

-- ORIGEM
-- Acre (AC)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Rio Branco', 'AC');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Cruzeiro do Sul', 'AC');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Sena Madureira', 'AC');
-- Alagoas (AL)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Maceió', 'AL');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Arapiraca', 'AL');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Rio Largo', 'AL');
-- Amapá (AP)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Macapá', 'AP');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Santana', 'AP');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Laranjal do Jari', 'AP');
-- Amazonas (AM)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Manaus', 'AM');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Parintins', 'AM');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Itacoatiara', 'AM');
-- Bahia (BA)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Salvador', 'BA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Feira de Santana', 'BA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Vitória da Conquista', 'BA');
-- Ceará (CE)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Fortaleza', 'CE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Caucaia', 'CE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Juazeiro do Norte', 'CE');
-- Distrito Federal (DF)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Brasília', 'DF');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Ceilândia', 'DF');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Taguatinga', 'DF');
-- Espírito Santo (ES)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Vitória', 'ES');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Vila Velha', 'ES');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Serra', 'ES');
-- Goiás (GO)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Goiânia', 'GO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Aparecida de Goiânia', 'GO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Anápolis', 'GO');
-- Maranhão (MA)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('São Luís', 'MA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Imperatriz', 'MA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Timon', 'MA');
-- Mato Grosso (MT)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Cuiabá', 'MT');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Várzea Grande', 'MT');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Rondonópolis', 'MT');
-- Mato Grosso do Sul (MS)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Campo Grande', 'MS');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Dourados', 'MS');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Três Lagoas', 'MS');
-- Minas Gerais (MG)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Belo Horizonte', 'MG');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Uberlândia', 'MG');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Contagem', 'MG');
-- Pará (PA)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Belém', 'PA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Ananindeua', 'PA');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Santarém', 'PA');
-- Paraíba (PB)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('João Pessoa', 'PB');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Campina Grande', 'PB');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Santa Rita', 'PB');
-- Paraná (PR)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Curitiba', 'PR');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Londrina', 'PR');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Maringá', 'PR');
-- Pernambuco (PE)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Recife', 'PE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Jaboatão dos Guararapes', 'PE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Olinda', 'PE');
-- Piauí (PI)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Teresina', 'PI');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Parnaíba', 'PI');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Picos', 'PI');
-- Rio de Janeiro (RJ)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Rio de Janeiro', 'RJ');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('São Gonçalo', 'RJ');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Duque de Caxias', 'RJ');
-- Rio Grande do Norte (RN)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Natal', 'RN');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Mossoró', 'RN');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Parnamirim', 'RN');
-- Rio Grande do Sul (RS)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Porto Alegre', 'RS');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Caxias do Sul', 'RS');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Pelotas', 'RS');
-- Rondônia (RO)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Porto Velho', 'RO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Ji-Paraná', 'RO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Ariquemes', 'RO');
-- Roraima (RR)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Boa Vista', 'RR');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Rorainópolis', 'RR');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Caracaraí', 'RR');
-- Santa Catarina (SC)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Joinville', 'SC');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Florianópolis', 'SC');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Blumenau', 'SC');
-- São Paulo (SP)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('São Paulo', 'SP');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Guarulhos', 'SP');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Campinas', 'SP');
-- Sergipe (SE)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Aracaju', 'SE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Nossa Senhora do Socorro', 'SE');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Lagarto', 'SE');
-- Tocantins (TO)
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Palmas', 'TO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Araguaína', 'TO');
INSERT INTO Origem_Viagem (Cidade_Nome, UF_Estado) VALUES ('Gurupi', 'TO');

-- DESTINO
-- Acre (AC)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour pela Floresta Amazônica', 'Rio Branco', 'AC', 1250.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Expedição Cruzeiro do Sul', 'Cruzeiro do Sul', 'AC', 980.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Ecoturismo Sena Madureira', 'Sena Madureira', 'AC', 750.00);
-- Alagoas (AL)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Praias de Maceió', 'Maceió', 'AL', 1650.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Cultural Arapiraca', 'Arapiraca', 'AL', 890.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Rio Largo Natureza', 'Rio Largo', 'AL', 720.00);
-- Amapá (AP)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Macapá Histórico', 'Macapá', 'AP', 1100.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Santana Aventura', 'Santana', 'AP', 850.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Expedição Laranjal do Jari', 'Laranjal do Jari', 'AP', 680.00);
-- Amazonas (AM)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Manaus Completo', 'Manaus', 'AM', 2200.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Festival de Parintins', 'Parintins', 'AM', 1500.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Itacoatiara Fluvial', 'Itacoatiara', 'AM', 950.00);
-- Bahia (BA)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Salvador Histórico', 'Salvador', 'BA', 1850.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Feira de Santana', 'Feira de Santana', 'BA', 920.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Vitória da Conquista', 'Vitória da Conquista', 'BA', 780.00);
-- Ceará (CE)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Fortaleza Beaches', 'Fortaleza', 'CE', 1750.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Caucaia Cultural', 'Caucaia', 'CE', 880.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Romaria Juazeiro do Norte', 'Juazeiro do Norte', 'CE', 650.00);
-- Distrito Federal (DF)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Brasília Monumental', 'Brasília', 'DF', 1600.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Ceilândia Cultural', 'Ceilândia', 'DF', 820.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Taguatinga', 'Taguatinga', 'DF', 710.00);
-- Espírito Santo (ES)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Vitória Capital', 'Vitória', 'ES', 1420.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Vila Velha Praias', 'Vila Velha', 'ES', 950.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Serra Montanha', 'Serra', 'ES', 830.00);
-- Goiás (GO)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Goiânia Moderna', 'Goiânia', 'GO', 1350.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Aparecida de Goiânia', 'Aparecida de Goiânia', 'GO', 790.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Anápolis Histórico', 'Anápolis', 'GO', 680.00);
-- Maranhão (MA)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour São Luís Patrimônio', 'São Luís', 'MA', 1550.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Imperatriz', 'Imperatriz', 'MA', 870.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Timon', 'Timon', 'MA', 690.00);
-- Mato Grosso (MT)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Cuiabá Pantanal', 'Cuiabá', 'MT', 1980.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Várzea Grande', 'Várzea Grande', 'MT', 850.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Rondonópolis', 'Rondonópolis', 'MT', 760.00);
-- Mato Grosso do Sul (MS)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Campo Grande', 'Campo Grande', 'MS', 1280.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Dourados Fronteira', 'Dourados', 'MS', 920.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Três Lagoas', 'Três Lagoas', 'MS', 810.00);
-- Minas Gerais (MG)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Belo Horizonte Cultural', 'Belo Horizonte', 'MG', 1650.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Uberlândia', 'Uberlândia', 'MG', 1100.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Contagem', 'Contagem', 'MG', 880.00);
-- Pará (PA)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Belém Ver-o-Peso', 'Belém', 'PA', 1450.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Ananindeua', 'Ananindeua', 'PA', 820.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Santarém', 'Santarém', 'PA', 1250.00);
-- Paraíba (PB)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote João Pessoa', 'João Pessoa', 'PB', 1380.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Campina Grande', 'Campina Grande', 'PB', 950.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Santa Rita', 'Santa Rita', 'PB', 720.00);
-- Paraná (PR)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Curitiba Europa', 'Curitiba', 'PR', 1550.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Londrina', 'Londrina', 'PR', 1120.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Maringá', 'Maringá', 'PR', 980.00);
-- Pernambuco (PE)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Recife Antigo', 'Recife', 'PE', 1680.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Jaboatão', 'Jaboatão dos Guararapes', 'PE', 890.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Olinda Histórica', 'Olinda', 'PE', 1050.00);
-- Piauí (PI)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Teresina', 'Teresina', 'PI', 1150.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Parnaíba Delta', 'Parnaíba', 'PI', 1320.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Picos', 'Picos', 'PI', 680.00);
-- Rio de Janeiro (RJ)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Rio de Janeiro', 'Rio de Janeiro', 'RJ', 2450.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour São Gonçalo', 'São Gonçalo', 'RJ', 980.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Duque de Caxias', 'Duque de Caxias', 'RJ', 870.00);
-- Rio Grande do Norte (RN)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Natal Dunas', 'Natal', 'RN', 1720.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Mossoró', 'Mossoró', 'RN', 920.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Parnamirim', 'Parnamirim', 'RN', 780.00);
-- Rio Grande do Sul (RS)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Porto Alegre', 'Porto Alegre', 'RS', 1480.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Caxias do Sul', 'Caxias do Sul', 'RS', 1120.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Pelotas', 'Pelotas', 'RS', 950.00);
-- Rondônia (RO)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Porto Velho', 'Porto Velho', 'RO', 1280.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Ji-Paraná', 'Ji-Paraná', 'RO', 850.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Ariquemes', 'Ariquemes', 'RO', 720.00);
-- Roraima (RR)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Boa Vista', 'Boa Vista', 'RR', 1350.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Rorainópolis', 'Rorainópolis', 'RR', 880.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Caracaraí', 'Caracaraí', 'RR', 690.00);
-- Santa Catarina (SC)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Joinville', 'Joinville', 'SC', 1580.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Florianópolis', 'Florianópolis', 'SC', 1820.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Blumenau', 'Blumenau', 'SC', 1250.00);
-- São Paulo (SP)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour São Paulo', 'São Paulo', 'SP', 1950.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Guarulhos', 'Guarulhos', 'SP', 1050.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Campinas', 'Campinas', 'SP', 1280.00);
-- Sergipe (SE)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Aracaju', 'Aracaju', 'SE', 1420.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Nossa Senhora do Socorro', 'Nossa Senhora do Socorro', 'SE', 780.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Lagarto', 'Lagarto', 'SE', 650.00);
-- Tocantins (TO)
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Palmas', 'Palmas', 'TO', 1220.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Pacote Araguaína', 'Araguaína', 'TO', 890.00);
INSERT INTO Destino_Viagem (Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino) VALUES ('Tour Gurupi', 'Gurupi', 'TO', 740.00);

-- PAGAMENTO
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Dinheiro');
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Cartão de Débito');
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Cartão de Crédito');
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Transferência Bancária');
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Pix');
INSERT INTO Pagamento (Desc_Pagamento) VALUES ('Boleto Bancário');

-- ENDEREÇO DE TRANSPORTE
-- Táxis (endereços genéricos nas capitais)
INSERT INTO End_Transporte (Id_Transp, Logradouro_End_Transporte, Numero_End_Transporte, Bairro_End_Transporte, Cidade_Nome, UF_Estado)
VALUES
(1, 'Av. Getúlio Vargas', '100', 'Centro', 'Rio Branco', 'AC'),
(2, 'Av. Fernandes Lima', '1200', 'Farol', 'Maceió', 'AL'),
(3, 'Av. FAB', '500', 'Centro', 'Macapá', 'AP'),
(4, 'Av. Djalma Batista', '2200', 'Chapada', 'Manaus', 'AM'),
(5, 'Av. Paralela', '8000', 'Pituaçu', 'Salvador', 'BA'),
(6, 'Av. Bezerra de Menezes', '900', 'São Gerardo', 'Fortaleza', 'CE'),
(7, 'Eixo Monumental', '0', 'Zona Cívico-Administrativa', 'Brasília', 'DF'),
(8, 'Av. Nossa Senhora da Penha', '1600', 'Santa Luíza', 'Vitória', 'ES'),
(9, 'Av. Goiás', '500', 'Setor Central', 'Goiânia', 'GO'),
(10, 'Av. dos Holandeses', '200', 'Calhau', 'São Luís', 'MA'),
(11, 'Av. Historiador Rubens de Mendonça', '1500', 'Bosque da Saúde', 'Cuiabá', 'MT'),
(12, 'Av. Afonso Pena', '3000', 'Centro', 'Campo Grande', 'MS'),
(13, 'Av. Afonso Pena', '1200', 'Centro', 'Belo Horizonte', 'MG'),
(14, 'Av. Nazaré', '900', 'Nazaré', 'Belém', 'PA'),
(15, 'Av. Epitácio Pessoa', '1800', 'Tambauzinho', 'João Pessoa', 'PB'),
(16, 'Av. Cândido de Abreu', '300', 'Centro Cívico', 'Curitiba', 'PR'),
(17, 'Av. Boa Viagem', '1000', 'Boa Viagem', 'Recife', 'PE'),
(18, 'Av. Frei Serafim', '800', 'Centro', 'Teresina', 'PI'),
(19, 'Av. Brasil', '100', 'Centro', 'Rio de Janeiro', 'RJ'),
(20, 'Av. Hermes da Fonseca', '1000', 'Tirol', 'Natal', 'RN'),
(21, 'Av. Borges de Medeiros', '1200', 'Centro', 'Porto Alegre', 'RS'),
(22, 'Av. Jorge Teixeira', '900', 'Centro', 'Porto Velho', 'RO'),
(23, 'Av. Ville Roy', '1100', 'Centro', 'Boa Vista', 'RR'),
(24, 'Av. Mauro Ramos', '1600', 'Centro', 'Florianópolis', 'SC'),
(25, 'Av. Paulista', '1500', 'Bela Vista', 'São Paulo', 'SP'),
(26, 'Av. Augusto Franco', '800', 'Salgado Filho', 'Aracaju', 'SE'),
(27, 'Av. Teotônio Segurado', '500', 'Plano Diretor', 'Palmas', 'TO');
-- Rodoviárias principais de cada capital
INSERT INTO End_Transporte (Id_Transp, Logradouro_End_Transporte, Numero_End_Transporte, Bairro_End_Transporte, Cidade_Nome, UF_Estado)
VALUES
(28, 'Rodoviária de Rio Branco', 'S/N', 'Centro', 'Rio Branco', 'AC'),
(29, 'Rodoviária de Maceió', 'S/N', 'Feitosa', 'Maceió', 'AL'),
(30, 'Rodoviária de Macapá', 'S/N', 'Buritizal', 'Macapá', 'AP'),
(31, 'Rodoviária de Manaus', 'S/N', 'Flores', 'Manaus', 'AM'),
(32, 'Rodoviária de Salvador', 'S/N', 'Pernambués', 'Salvador', 'BA'),
(33, 'Rodoviária de Fortaleza', 'S/N', 'Fátima', 'Fortaleza', 'CE'),
(34, 'Rodoviária de Brasília', 'S/N', 'Asa Sul', 'Brasília', 'DF'),
(35, 'Rodoviária de Vitória', 'S/N', 'Ilha do Príncipe', 'Vitória', 'ES'),
(36, 'Rodoviária de Goiânia', 'S/N', 'Setor Norte Ferroviário', 'Goiânia', 'GO'),
(37, 'Rodoviária de São Luís', 'S/N', 'Areinha', 'São Luís', 'MA'),
(38, 'Rodoviária de Cuiabá', 'S/N', 'Centro Político', 'Cuiabá', 'MT'),
(39, 'Rodoviária de Campo Grande', 'S/N', 'Vila Nova', 'Campo Grande', 'MS'),
(40, 'Rodoviária de Belo Horizonte', 'S/N', 'Centro', 'Belo Horizonte', 'MG'),
(41, 'Rodoviária de Belém', 'S/N', 'São Brás', 'Belém', 'PA'),
(42, 'Rodoviária de João Pessoa', 'S/N', 'Varadouro', 'João Pessoa', 'PB'),
(43, 'Rodoviária de Curitiba', 'S/N', 'Centro', 'Curitiba', 'PR'),
(44, 'Rodoviária de Recife', 'S/N', 'Imbiribeira', 'Recife', 'PE'),
(45, 'Rodoviária de Teresina', 'S/N', 'Centro', 'Teresina', 'PI'),
(46, 'Rodoviária do Rio de Janeiro', 'S/N', 'Santo Cristo', 'Rio de Janeiro', 'RJ'),
(47, 'Rodoviária de Natal', 'S/N', 'Cidade da Esperança', 'Natal', 'RN'),
(48, 'Rodoviária de Porto Alegre', 'S/N', 'Centro', 'Porto Alegre', 'RS'),
(49, 'Rodoviária de Porto Velho', 'S/N', 'Nova Porto Velho', 'Porto Velho', 'RO'),
(50, 'Rodoviária de Boa Vista', 'S/N', 'Centro', 'Boa Vista', 'RR'),
(51, 'Rodoviária de Florianópolis', 'S/N', 'Centro', 'Florianópolis', 'SC'),
(52, 'Rodoviária de São Paulo', 'S/N', 'Tatuapé', 'São Paulo', 'SP'),
(53, 'Rodoviária de Aracaju', 'S/N', 'Capucho', 'Aracaju', 'SE'),
(54, 'Rodoviária de Palmas', 'S/N', 'Plano Diretor', 'Palmas', 'TO');
-- Portos e Píeres
INSERT INTO End_Transporte (Id_Transp, Logradouro_End_Transporte, Numero_End_Transporte, Bairro_End_Transporte, Cidade_Nome, UF_Estado)
VALUES
(75, 'Av. Conselheiro Rodrigues Alves', 'S/N', 'Estuário', 'Santos', 'SP'),
(76, 'Av. Rodrigues Alves', 'S/N', 'Centro', 'Rio de Janeiro', 'RJ'),
(77, 'Av. da França', 'S/N', 'Comércio', 'Salvador', 'BA'),
(78, 'Av. Assis Chateaubriand', 'S/N', 'Jaraguá', 'Maceió', 'AL'),
(79, 'Av. Alfredo Lisboa', 'S/N', 'Recife Antigo', 'Recife', 'PE'),
(80, 'Av. Pessoa Anta', 'S/N', 'Mucuripe', 'Fortaleza', 'CE'),
(81, 'Av. Café Filho', 'S/N', 'Praia do Meio', 'Natal', 'RN'),
(82, 'Av. Bernardo Sayão', 'S/N', 'Comércio', 'Belém', 'PA'),
(83, 'Av. Lourenço Braga', 'S/N', 'Centro', 'Manaus', 'AM'),
(84, 'Av. Pref. Paulo Bauer', 'S/N', 'Centro', 'Itajaí', 'SC');

-- HOSPEDAGEM
-- Acre (AC)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Hotel Rio Branco Palace', 2, 2, 'Rua Floriano Peixoto', '123', 'Centro', 'Rio Branco', 'AC'),
('Pousada Cruzeiro do Sol', 3, 1, 'Av. Coronel Mâncio Lima', '456', 'Centro', 'Cruzeiro do Sul', 'AC'),
('Hotel Sena Natureza', 1, 3, 'Rua 7 de Setembro', '789', 'Centro', 'Sena Madureira', 'AC');
-- Alagoas (AL)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Maceió Beach Resort', 4, 3, 'Av. Álvaro Otacílio', '2000', 'Pajuçara', 'Maceió', 'AL'),
('Arapiraca Business Hotel', 5, 2, 'Rua Esperidião Rodrigues', '321', 'Centro', 'Arapiraca', 'AL'),
('Pousada Rio Largo', 2, 1, 'Rua João Pessoa', '654', 'Centro', 'Rio Largo', 'AL');
-- Amapá (AP)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Macapá Amazon Hotel', 3, 2, 'Av. FAB', '987', 'Central', 'Macapá', 'AP'),
('Pousada Santana', 1, 1, 'Rua São José', '321', 'Centro', 'Santana', 'AP'),
('Hotel Laranjal', 2, 3, 'Av. Principal', '159', 'Centro', 'Laranjal do Jari', 'AP');
-- Amazonas (AM)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Manaus Eco Resort', 4, 3, 'Av. Eduardo Ribeiro', '456', 'Centro', 'Manaus', 'AM'),
('Parintins Folk Hotel', 2, 2, 'Rua João Catão', '789', 'Centro', 'Parintins', 'AM'),
('Pousada Itacoatiara', 3, 1, 'Av. Paraná do Ramos', '321', 'Centro', 'Itacoatiara', 'AM');
-- Bahia (BA)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Salvador Praia Hotel', 6, 3, 'Av. Oceânica', '100', 'Barra', 'Salvador', 'BA'),
('Feira Palace Hotel', 5, 2, 'Rua Marechal Deodoro', '200', 'Kalilândia', 'Feira de Santana', 'BA'),
('Pousada Conquista', 2, 1, 'Av. Brumado', '300', 'Bateias', 'Vitória da Conquista', 'BA');
-- Ceará (CE)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Fortaleza Beach Hotel', 4, 3, 'Av. Beira Mar', '800', 'Meireles', 'Fortaleza', 'CE'),
('Caucaia Resort', 3, 2, 'Av. Central', '600', 'Iparana', 'Caucaia', 'CE'),
('Juazeiro Pilgrim Hotel', 2, 1, 'Rua São José', '400', 'Centro', 'Juazeiro do Norte', 'CE');
-- Distrito Federal (DF)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Brasília Capital Hotel', 5, 3, 'SHS Quadra 2', 'Bloco A', 'Asa Sul', 'Brasília', 'DF'),
('Ceilândia Business', 6, 2, 'QNM 10', 'Módulo B', 'Centro', 'Ceilândia', 'DF'),
('Taguatinga Executive', 5, 1, 'CNB 1', 'Bloco C', 'Taguatinga Centro', 'Taguatinga', 'DF');
-- Espírito Santo (ES)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Vitória Harbor Hotel', 3, 2, 'Rua José Marcelino', '157', 'Centro', 'Vitória', 'ES'),
('Vila Velha Beach Resort', 4, 3, 'Av. Champagnat', '321', 'Praia da Costa', 'Vila Velha', 'ES'),
('Serra Mountain Hotel', 2, 1, 'Av. Central', '654', 'Centro', 'Serra', 'ES');
-- Goiás (GO)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Goiânia Palace Hotel', 5, 2, 'Av. Goiás', '1000', 'Setor Central', 'Goiânia', 'GO'),
('Aparecida Comfort', 3, 1, 'Rua 10', '234', 'Vila São José', 'Aparecida de Goiânia', 'GO'),
('Anápolis Business', 6, 3, 'Av. Brasil', '567', 'Maracanã', 'Anápolis', 'GO');
-- Maranhão (MA)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('São Luís Historic Hotel', 2, 2, 'Rua do Giz', '129', 'Centro', 'São Luís', 'MA'),
('Imperatriz Palace', 3, 1, 'Av. Getúlio Vargas', '432', 'Centro', 'Imperatriz', 'MA'),
('Timon Riverside Hotel', 1, 3, 'Rua Principal', '765', 'Centro', 'Timon', 'MA');
-- Mato Grosso (MT)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Cuiabá Pantanal Hotel', 4, 3, 'Rua Barão de Melgaço', '258', 'Porto', 'Cuiabá', 'MT'),
('Várzea Grande Executive', 5, 2, 'Av. Castelo Branco', '753', 'Centro', 'Várzea Grande', 'MT'),
('Rondonópolis Farm Hotel', 2, 1, 'Av. Rondonópolis', '159', 'Centro', 'Rondonópolis', 'MT');
-- Mato Grosso do Sul (MS)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Campo Grande Nature Hotel', 3, 2, 'Av. Afonso Pena', '2222', 'Centro', 'Campo Grande', 'MS'),
('Dourados Frontier Hotel', 2, 1, 'Rua Hayel Bon Faker', '333', 'Jardim América', 'Dourados', 'MS'),
('Três Lagoas Lake Resort', 4, 3, 'Av. Rosário Congro', '444', 'Centro', 'Três Lagoas', 'MS');
-- Minas Gerais (MG)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Belo Horizonte Palace', 5, 3, 'Av. Afonso Pena', '1234', 'Centro', 'Belo Horizonte', 'MG'),
('Uberlândia Business Hotel', 6, 2, 'Av. João Naves', '1000', 'Santa Mônica', 'Uberlândia', 'MG'),
('Contagem Industrial Hotel', 5, 1, 'Av. Amazonas', '2000', 'Eldorado', 'Contagem', 'MG');
-- Pará (PA)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Belém Amazon Hotel', 3, 2, 'Av. Presidente Vargas', '456', 'Campina', 'Belém', 'PA'),
('Ananindeua City Hotel', 2, 1, 'Av. Independência', '789', 'Centro', 'Ananindeua', 'PA'),
('Santarém Riverside', 4, 3, 'Av. Tapajós', '321', 'Centro', 'Santarém', 'PA');
-- Paraíba (PB)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('João Pessoa Beach Hotel', 4, 3, 'Av. Cabo Branco', '123', 'Cabo Branco', 'João Pessoa', 'PB'),
('Campina Grande University Hotel', 5, 2, 'Rua Maciel Pinheiro', '456', 'Centro', 'Campina Grande', 'PB'),
('Santa Rita Historic Inn', 2, 1, 'Rua Principal', '789', 'Centro', 'Santa Rita', 'PB');
-- Paraná (PR)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Curitiba Eco Hotel', 6, 3, 'Rua XV de Novembro', '100', 'Centro', 'Curitiba', 'PR'),
('Londrina Business Hotel', 5, 2, 'Av. Paraná', '200', 'Centro', 'Londrina', 'PR'),
('Maringá Garden Hotel', 4, 1, 'Av. Colombo', '300', 'Zona 7', 'Maringá', 'PR');
-- Pernambuco (PE)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Recife Old Town Hotel', 3, 2, 'Rua do Apolo', '123', 'Recife Antigo', 'Recife', 'PE'),
('Jaboatão Beach Resort', 4, 3, 'Av. Bernardo Vieira de Melo', '456', 'Piedade', 'Jaboatão dos Guararapes', 'PE'),
('Olinda Historic Inn', 2, 1, 'Rua do Amparo', '789', 'Amparo', 'Olinda', 'PE');
-- Piauí (PI)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Teresina River Hotel', 3, 2, 'Av. Maranhão', '123', 'Centro', 'Teresina', 'PI'),
('Parnaíba Delta Lodge', 4, 3, 'Rua São José', '456', 'Centro', 'Parnaíba', 'PI'),
('Picos Mountain Hotel', 2, 1, 'Av. Principal', '789', 'Centro', 'Picos', 'PI');
-- Rio de Janeiro (RJ)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Rio de Janeiro Copacabana Palace', 6, 3, 'Av. Atlântica', '1702', 'Copacabana', 'Rio de Janeiro', 'RJ'),
('São Gonçalo Business Hotel', 5, 2, 'Rua Dr. Francisco Portela', '1000', 'Alcântara', 'São Gonçalo', 'RJ'),
('Duque de Caxias Executive', 5, 1, 'Av. Brigadeiro Lima e Silva', '200', 'Centro', 'Duque de Caxias', 'RJ');

-- Rio Grande do Norte (RN)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Natal Dunas Resort', 4, 3, 'Av. Senador Dinarte Medeiros Mariz', '1000', 'Ponta Negra', 'Natal', 'RN'),
('Mossoró City Hotel', 3, 2, 'Rua Dr. Almeida Castro', '500', 'Centro', 'Mossoró', 'RN'),
('Parnamirim Airport Hotel', 5, 1, 'Av. Maria Lacerda Montenegro', '300', 'Centro', 'Parnamirim', 'RN');
-- Rio Grande do Sul (RS)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Porto Alegre Central Hotel', 5, 2, 'Rua dos Andradas', '500', 'Centro', 'Porto Alegre', 'RS'),
('Caxias do Sul Wine Hotel', 4, 3, 'Rua Sinimbu', '1000', 'Centro', 'Caxias do Sul', 'RS'),
('Pelotas Historic Inn', 3, 1, 'Rua Andrade Neves', '200', 'Centro', 'Pelotas', 'RS');
-- Rondônia (RO)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Porto Velho Amazon Hotel', 3, 2, 'Av. Campos Sales', '123', 'Centro', 'Porto Velho', 'RO'),
('Ji-Paraná Executive', 2, 1, 'Rua Marechal Rondon', '456', 'Centro', 'Ji-Paraná', 'RO'),
('Ariquemes Eco Lodge', 4, 3, 'Av. Jamari', '789', 'Setor 1', 'Ariquemes', 'RO');
-- Roraima (RR)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Boa Vista Capital Hotel', 5, 2, 'Av. Capitão Júlio Bezerra', '123', 'Centro', 'Boa Vista', 'RR'),
('Rorainópolis Frontier Inn', 2, 1, 'Rua Principal', '456', 'Centro', 'Rorainópolis', 'RR'),
('Caracaraí Riverside Hotel', 3, 3, 'Av. Beira Rio', '789', 'Centro', 'Caracaraí', 'RR');
-- Santa Catarina (SC)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Joinville German Hotel', 4, 2, 'Rua do Príncipe', '100', 'Centro', 'Joinville', 'SC'),
('Florianópolis Island Resort', 6, 3, 'Av. Beira Mar Norte', '200', 'Centro', 'Florianópolis', 'SC'),
('Blumenau Beer Hotel', 3, 1, 'Rua 7 de Setembro', '300', 'Centro', 'Blumenau', 'SC');
-- São Paulo (SP)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('São Paulo Business Hotel', 6, 3, 'Av. Paulista', '1000', 'Bela Vista', 'São Paulo', 'SP'),
('Guarulhos Airport Hotel', 5, 2, 'Av. Monteiro Lobato', '2000', 'Centro', 'Guarulhos', 'SP'),
('Campinas Tech Hotel', 5, 1, 'Av. John Boyd Dunlop', '300', 'Swiss Park', 'Campinas', 'SP');
-- Sergipe (SE)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Aracaju Beach Resort', 4, 3, 'Av. Santos Dumont', '123', 'Atalaia', 'Aracaju', 'SE'),
('Socorro City Hotel', 3, 2, 'Rua Principal', '456', 'Centro', 'Nossa Senhora do Socorro', 'SE'),
('Lagarto Thermal Hotel', 2, 1, 'Av. das Termas', '789', 'Centro', 'Lagarto', 'SE');
-- Tocantins (TO)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Palmas Capital Hotel', 5, 2, 'Av. Teotônio Segurado', '100', 'Plano Diretor', 'Palmas', 'TO'),
('Araguaína Farm Hotel', 3, 1, 'Av. Filadélfia', '200', 'Centro', 'Araguaína', 'TO'),
('Gurupi University Hotel', 4, 3, 'Av. Pará', '300', 'Centro', 'Gurupi', 'TO');
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Recife Old Town Hotel', 3, 2, 'Rua do Apolo', '123', 'Recife Antigo', 'Recife', 'PE'),
('Jaboatão Beach Resort', 4, 3, 'Av. Bernardo Vieira de Melo', '456', 'Piedade', 'Jaboatão dos Guararapes', 'PE'),
('Olinda Historic Inn', 2, 1, 'Rua do Amparo', '789', 'Amparo', 'Olinda', 'PE');
-- Piauí (PI)
INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Cidade_Nome, UF_Estado) VALUES
('Teresina River Hotel', 3, 2, 'Av. Maranhão', '123', 'Centro', 'Teresina', 'PI'),
('Parnaíba Delta Lodge', 4, 3, 'Rua São José', '456', 'Centro', 'Parnaíba', 'PI'),
('Picos Mountain Hotel', 2, 1, 'Av. Principal', '789', 'Centro', 'Picos', 'PI');

-- PASSEIOS
-- Acre (AC) - Rio Branco
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour pelo Parque da Maternidade', 'Passeio pelo principal parque urbano de Rio Branco, com visita ao memorial dos Autonomistas', 75.00, '3 horas', 'Rio Branco', 'AC'),
('City Tour Histórico Centro', 'Conheça o Palácio Rio Branco, a Catedral Nossa Senhora de Nazaré e o Mercado Velho', 60.00, '2 horas', 'Rio Branco', 'AC');
-- Acre (AC) - Cruzeiro do Sul
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Trilha da Serra do Divisor', 'Caminhada ecológica pela serra com vistas panorâmicas e contato com natureza preservada', 120.00, '6 horas', 'Cruzeiro do Sul', 'AC'),
('Passeio de Barco pelo Rio Juruá', 'Navegação pelo rio Juruá com observação da fauna e flora amazônica', 95.00, '4 horas', 'Cruzeiro do Sul', 'AC');
-- Acre (AC) - Sena Madureira
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour Rural Seringal', 'Conheça um antigo seringal e aprenda sobre o ciclo da borracha na região', 85.00, '4 horas', 'Sena Madureira', 'AC'),
('Passeio ao Igarapé Preto', 'Banho refrescante e passeio pelas margens do igarapé mais famoso da cidade', 55.00, '3 horas', 'Sena Madureira', 'AC');
-- Alagoas (AL) - Maceió
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour pelas Piscinas Naturais', 'Passeio de jangada às piscinas naturais de Pajuçara com mergulho e observação marinha', 90.00, '4 horas', 'Maceió', 'AL'),
('City Tour Praias Urbanas', 'Conheça as praias de Pontal da Barra, Pratagi e Cruz das Almas com paradas para fotos', 70.00, '3 horas', 'Maceió', 'AL');
-- Alagoas (AL) - Arapiraca
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour do Fumo e Cultura', 'Conheça o processo de produção do fumo e a cultura local no maior produtor de tabaco', 65.00, '3 horas', 'Arapiraca', 'AL'),
('Parque da Cidade e Mirante', 'Passeio pelo principal parque urbano com visita ao mirante da cidade', 45.00, '2 horas', 'Arapiraca', 'AL');
-- Alagoas (AL) - Rio Largo
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Usina Caeté e História', 'Tour pela antiga usina de açúcar com explicação histórica do ciclo canavieiro', 60.00, '3 horas', 'Rio Largo', 'AL'),
('Balneário Municipal', 'Passeio de relaxamento no balneário municipal com áreas de lazer', 40.00, '2 horas', 'Rio Largo', 'AL');
-- Amapá (AP) - Macapá
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour pela Fortaleza de São José', 'Visita guiada à fortaleza do século XVIII com vista para o Rio Amazonas', 80.00, '3 horas', 'Macapá', 'AP'),
('Complexo do Marco Zero', 'Passeio pelo marco do equador e parque com vista para o encontro dos rios', 55.00, '2 horas', 'Macapá', 'AP');
-- Amapá (AP) - Santana
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Porto de Santana e Comércio', 'Tour pelo principal porto da região e centro comercial da cidade', 50.00, '2 horas', 'Santana', 'AP'),
('Praia do Caju e Balneário', 'Passeio à praia fluvial do Caju com opção de banho e descanso', 45.00, '3 horas', 'Santana', 'AP');
-- Amapá (AP) - Laranjal do Jari
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Trilha Ecológica do Jari', 'Caminhada pela floresta preservada às margens do Rio Jari', 75.00, '4 horas', 'Laranjal do Jari', 'AP'),
('Tour pela Vila do Monte Dourado', 'Conheça a vila empresarial e a história da Jari Florestal', 60.00, '3 horas', 'Laranjal do Jari', 'AP');
-- Amazonas (AM) - Manaus
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Teatro Amazonas e Centro Histórico', 'Visita ao famoso teatro e passeio pelo centro histórico de Manaus', 85.00, '3 horas', 'Manaus', 'AM'),
('Encontro das Águas e Floresta', 'Passeio de barco para ver o encontro dos rios Negro e Solimões', 150.00, '6 horas', 'Manaus', 'AM');
-- Amazonas (AM) - Parintins
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour Cultural do Boi-Bumbá', 'Conheça a história e os bastidores do famoso festival folclórico', 95.00, '3 horas', 'Parintins', 'AM'),
('Passeio de Barco pelo Rio Amazonas', 'Navegação pelo rio com visita a comunidades ribeirinhas', 110.00, '5 horas', 'Parintins', 'AM');
-- Amazonas (AM) - Itacoatiara
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('City Tour pelas Pedras Pintadas', 'Visita aos sítios arqueológicos com inscrições rupestres', 70.00, '3 horas', 'Itacoatiara', 'AM'),
('Passeio ao Lago do Silvério', 'Dia de lazer no lago com direito a banho e pescaria esportiva', 65.00, '4 horas', 'Itacoatiara', 'AM');
-- Bahia (BA) - Salvador
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Pelourinho Histórico e Cultural', 'Tour pelo centro histórico com igrejas, museus e cultura afro-baiana', 120.00, '4 horas', 'Salvador', 'BA'),
('Elevador Lacerda e Mercado Modelo', 'Passeio pelos cartões postais da cidade com vista para a Baía de Todos-os-Santos', 80.00, '3 horas', 'Salvador', 'BA');
-- Bahia (BA) - Feira de Santana
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour pelo Centro de Abastecimento', 'Conheça um dos maiores centros de comércio do interior baiano', 60.00, '3 horas', 'Feira de Santana', 'BA'),
('Parque do Saber e Observatório', 'Visita ao parque científico com observatório astronômico', 45.00, '2 horas', 'Feira de Santana', 'BA');
-- Bahia (BA) - Vitória da Conquista
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Serra do Periperi e Vista Panorâmica', 'Passeio pela serra com mirante e vista completa da cidade', 55.00, '3 horas', 'Vitória da Conquista', 'BA'),
('Museu Regional e História da Cidade', 'Tour pelo museu que conta a história da conquista do sertão baiano', 40.00, '2 horas', 'Vitória da Conquista', 'BA');
-- Ceará (CE) - Fortaleza
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Beira-Mar e Feirinha de Artesanato', 'Passeio pela orla com visita à feira de artesanato da Praça dos Estressados', 70.00, '3 horas', 'Fortaleza', 'CE'),
('Mercado Central e Cultura Local', 'Tour pelo mercado central com degustação de comidas típicas', 65.00, '3 horas', 'Fortaleza', 'CE');
-- Ceará (CE) - Caucaia
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Praia do Cumbuco e Dunas', 'Passeio à praia famosa pelas dunas e opções de esportes aquáticos', 85.00, '4 horas', 'Caucaia', 'CE'),
('Lagoa do Banana e Pescadores', 'Tour pela lagoa com observação da pesca artesanal tradicional', 50.00, '3 horas', 'Caucaia', 'CE');
-- Ceará (CE) - Juazeiro do Norte
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour Religioso Padre Cícero', 'Visita ao museu, estátua e locais sagrados do Padre Cícero', 60.00, '3 horas', 'Juazeiro do Norte', 'CE'),
('Feira do Cariri e Artesanato', 'Passeio pela maior feira ao ar livre do Nordeste', 45.00, '2 horas', 'Juazeiro do Norte', 'CE');
-- Distrito Federal (DF) - Brasília
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour Arquitetônico Eixo Monumental', 'Visita aos principais monumentos de Oscar Niemeyer', 95.00, '4 horas', 'Brasília', 'DF'),
('Palácios do Planalto e Itamaraty', 'Tour externo pelos palácios do governo com explicação histórica', 75.00, '3 horas', 'Brasília', 'DF');
-- Distrito Federal (DF) - Ceilândia
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Cultural e Feira Permanente', 'Conheça a cultura popular e o comércio vibrante da maior cidade satélite', 50.00, '3 horas', 'Ceilândia', 'DF'),
('Parque da Vaquejada e Esportes', 'Tour pelo complexo esportivo e cultural da região', 40.00, '2 horas', 'Ceilândia', 'DF');
-- Distrito Federal (DF) - Taguatinga
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Ponte do Bragueto e Comércio', 'Tour pelo centro comercial mais movimentado do DF', 45.00, '2 horas', 'Taguatinga', 'DF'),
('Parque Ecológico de Taguatinga', 'Passeio pelo parque com trilhas e área de preservação', 35.00, '2 horas', 'Taguatinga', 'DF');
-- Espírito Santo (ES) - Vitória
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Tour pela Ilha do Boi e Praias', 'Passeio pelas praias urbanas e mirantes da capital', 80.00, '3 horas', 'Vitória', 'ES'),
('Centro Histórico e Catedral', 'Visita ao centro histórico com arquitetura colonial e catedral metropolitana', 65.00, '3 horas', 'Vitória', 'ES');
-- Espírito Santo (ES) - Vila Velha
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Convento da Penha e Vista', 'Tour pelo convento histórico com vista panorâmica da cidade', 70.00, '3 horas', 'Vila Velha', 'ES'),
('Praia da Costa e Molhe', 'Passeio pela orla com visita ao molhe e feira de artesanato', 55.00, '2 horas', 'Vila Velha', 'ES');
-- Espírito Santo (ES) - Serra
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Manguinhos e Praias do Norte', 'Tour pelas praias da região norte da grande Vitória', 60.00, '3 horas', 'Serra', 'ES'),
('Parque da Cidade e Lagoa', 'Passeio pelo parque municipal com área de lazer e lagoa', 45.00, '2 horas', 'Serra', 'ES');
-- Goiás (GO) - Goiânia
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque Vaca Brava e Setor Bueno', 'Passeio pelo famoso parque e região nobre da cidade', 65.00, '3 horas', 'Goiânia', 'GO'),
('Feira da Lua e Cultura Popular', 'Tour pela tradicional feira noturna de artesanato e comidas típicas', 50.00, '2 horas', 'Goiânia', 'GO');
-- Goiás (GO) - Aparecida de Goiânia
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque da Criança e Lazer', 'Passeio familiar pelo maior parque de diversões da cidade', 55.00, '3 horas', 'Aparecida de Goiânia', 'GO'),
('Mercado Municipal e Comércio', 'Tour pelo centro comercial e mercado municipal', 40.00, '2 horas', 'Aparecida de Goiânia', 'GO');
-- Goiás (GO) - Anápolis
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Distrito Industrial e Desenvolvimento', 'Tour pelo polo industrial e tecnológico da região', 60.00, '3 horas', 'Anápolis', 'GO'),
('Parque Ipiranga e Centro Histórico', 'Passeio pelo principal parque e centro histórico da cidade', 45.00, '2 horas', 'Anápolis', 'GO');
-- Maranhão (MA) - São Luís
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Histórico e Azulejos', 'Tour pelo patrimônio mundial da UNESCO com arquitetura portuguesa', 90.00, '4 horas', 'São Luís', 'MA'),
('Revoada dos Guarás e Delta', 'Passeio de barco para observar os guarás vermelhos no mangue', 120.00, '5 horas', 'São Luís', 'MA');
-- Maranhão (MA) - Imperatriz
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque de Exposições e Agropecuária', 'Tour pelo centro de eventos da maior feira agro da região', 55.00, '3 horas', 'Imperatriz', 'MA'),
('Balneário do Cacau e Rio Tocantins', 'Passeio de lazer às margens do Rio Tocantins', 50.00, '3 horas', 'Imperatriz', 'MA');
-- Maranhão (MA) - Timon
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Ponte Metálica e Vista para Teresina', 'Tour pela ponte histórica com vista para a capital do Piauí', 45.00, '2 horas', 'Timon', 'MA'),
('Praia do Rio Parnaíba', 'Passeio à praia fluvial com áreas de camping e lazer', 40.00, '3 horas', 'Timon', 'MA');
-- Mato Grosso (MT) - Cuiabá
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Histórico e Igrejas', 'Tour pelo centro histórico com arquitetura colonial cuiabana', 70.00, '3 horas', 'Cuiabá', 'MT'),
('Orla do Porto e Rio Cuiabá', 'Passeio pela orla revitalizada com vista para o rio', 55.00, '2 horas', 'Cuiabá', 'MT');
-- Mato Grosso (MT) - Várzea Grande
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Aeroporto e Complexo Industrial', 'Tour pela região do aeroporto e polo industrial da cidade', 50.00, '3 horas', 'Várzea Grande', 'MT'),
('Parque Bernardo Berneck', 'Passeio pelo parque municipal com área de lazer familiar', 40.00, '2 horas', 'Várzea Grande', 'MT');
-- Mato Grosso (MT) - Rondonópolis
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque Ecológico e Cachoeiras', 'Tour pelo parque com cachoeiras e trilhas ecológicas', 65.00, '4 horas', 'Rondonópolis', 'MT'),
('Museu Municipal e História', 'Visita ao museu que conta a história da colonização da região', 45.00, '2 horas', 'Rondonópolis', 'MT');
-- Mato Grosso do Sul (MS) - Campo Grande
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Morada dos Baís e História', 'Tour pelo complexo cultural e histórico da cidade', 60.00, '3 horas', 'Campo Grande', 'MS'),
('Parque das Nações Indígenas', 'Passeio pelo maior parque urbano de MS com museu', 50.00, '2 horas', 'Campo Grande', 'MS');
-- Mato Grosso do Sul (MS) - Dourados
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Universidade e Pesquisa Agrícola', 'Tour pela UFGD e centros de pesquisa agropecuária', 55.00, '3 horas', 'Dourados', 'MS'),
('Parque Antenor Martins e Lago', 'Passeio pelo parque com lago e área de esportes', 45.00, '2 horas', 'Dourados', 'MS');
-- Mato Grosso do Sul (MS) - Três Lagoas
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Lagoas Urbanas e Orla', 'Tour pelas três lagoas que dão nome à cidade', 50.00, '3 horas', 'Três Lagoas', 'MS'),
('Complexo Industrial da Celulose', 'Visita às áreas externas do polo de celulose da região', 60.00, '3 horas', 'Três Lagoas', 'MS');
-- Minas Gerais (MG) - Belo Horizonte
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Praça da Liberdade e Circuito Cultural', 'Tour pelos museus e espaços culturais da praça', 85.00, '4 horas', 'Belo Horizonte', 'MG'),
('Mercado Central e Sabores Mineiros', 'Passeio pelo mercado com degustação de comidas típicas', 70.00, '3 horas', 'Belo Horizonte', 'MG');
-- Minas Gerais (MG) - Uberlândia
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque do Sabiá e Complexo Esportivo', 'Tour pelo maior parque da cidade com estádio e áreas de lazer', 60.00, '3 horas', 'Uberlândia', 'MG'),
('Museu Municipal e História Regional', 'Visita ao museu que conta a história do triângulo mineiro', 45.00, '2 horas', 'Uberlândia', 'MG');
-- Minas Gerais (MG) - Contagem
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque Ecológico Eldorado', 'Passeio pelo principal parque ecológico da cidade', 50.00, '3 horas', 'Contagem', 'MG'),
('Centro Industrial e Desenvolvimento', 'Tour pela região industrial que impulsiona a economia', 55.00, '3 horas', 'Contagem', 'MG');
-- Pará (PA) - Belém
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Complexo Feliz Lusitânia e Ver-o-Peso', 'Tour pelo mercado histórico e complexo cultural', 80.00, '4 horas', 'Belém', 'PA'),
('Estação das Docas e Gastronomia', 'Passeio pelo complexo gastronômico às margens da baía', 75.00, '3 horas', 'Belém', 'PA');
-- Pará (PA) - Ananindeua
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque Shopping e Comércio', 'Tour pelo maior shopping center do norte do Brasil', 55.00, '3 horas', 'Ananindeua', 'PA'),
('Igarapé do Una e Comunidades', 'Passeio por áreas de preservação e comunidades locais', 45.00, '3 horas', 'Ananindeua', 'PA');
-- Pará (PA) - Santarém
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Encontro das Águas Tapajós-Amazonas', 'Passeio de barco para ver o encontro dos rios', 100.00, '5 horas', 'Santarém', 'PA'),
('Centro Histórico e Catedral', 'Tour pelo centro histórico com arquitetura tradicional', 60.00, '3 horas', 'Santarém', 'PA');
-- Paraíba (PB) - João Pessoa
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Histórico e São Francisco', 'Tour pelas igrejas e construções do período colonial', 70.00, '3 horas', 'João Pessoa', 'PB'),
('Ponta do Seixas e Praias', 'Passeio ao ponto mais oriental das Américas com lindas praias', 65.00, '4 horas', 'João Pessoa', 'PB');
-- Paraíba (PB) - Campina Grande
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque do Povo e São João', 'Tour pelo local do maior São João do mundo', 60.00, '3 horas', 'Campina Grande', 'PB'),
('Museu do Algodão e História', 'Visita ao museu que conta a história da riqueza algodoeira', 45.00, '2 horas', 'Campina Grande', 'PB');
-- Paraíba (PB) - Santa Rita
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Ruínas do Engenho e História', 'Tour pelas ruínas de engenhos de açúcar do período colonial', 50.00, '3 horas', 'Santa Rita', 'PB'),
('Manguezais e Costa Norte', 'Passeio pelos manguezais e praias da região metropolitana', 55.00, '4 horas', 'Santa Rita', 'PB');
-- Paraná (PR) - Curitiba
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Jardim Botânico e Ópera de Arame', 'Tour pelos principais cartões postais da cidade', 75.00, '3 horas', 'Curitiba', 'PR'),
('Centro Histórico e Rua das Flores', 'Passeio pelo centro histórico com comércio tradicional', 60.00, '3 horas', 'Curitiba', 'PR');
-- Paraná (PR) - Londrina
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Catedral e Marco Zero', 'Tour pela catedral metropolitana e marco inicial da cidade', 55.00, '2 horas', 'Londrina', 'PR'),
('Lago Igapó e Parque Municipal', 'Passeio pelo complexo de lagos e parques urbanos', 50.00, '3 horas', 'Londrina', 'PR');
-- Paraná (PR) - Maringá
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Catedral Basílica e Centro', 'Tour pela catedral símbolo da cidade e região central', 60.00, '2 horas', 'Maringá', 'PR'),
('Parque do Ingá e Zoobotânico', 'Passeio pelo principal parque com zoológico e trilhas', 45.00, '3 horas', 'Maringá', 'PR');
-- Pernambuco (PE) - Recife
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Recife Antigo e Marco Zero', 'Tour pelo centro histórico com visita ao marco zero', 80.00, '4 horas', 'Recife', 'PE'),
('Praia de Boa Viagem e Piscinas', 'Passeio pela orla mais famosa com observação das piscinas naturais', 65.00, '3 horas', 'Recife', 'PE');
-- Pernambuco (PE) - Jaboatão dos Guararapes
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Praia de Piedade e Orla', 'Passeio pela orla revitalizada com feirinha noturna', 55.00, '3 horas', 'Jaboatão dos Guararapes', 'PE'),
('Colina dos Guararapes e História', 'Tour pelo parque histórico da batalha dos Guararapes', 50.00, '3 horas', 'Jaboatão dos Guararapes', 'PE');
-- Pernambuco (PE) - Olinda
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Histórico Patrimônio Mundial', 'Tour pelas ladeiras, igrejas e casarios coloniais', 70.00, '3 horas', 'Olinda', 'PE'),
('Alto da Sé e Artesanato', 'Passeio pelo ponto mais alto com vista panorâmica e feira', 60.00, '2 horas', 'Olinda', 'PE');
-- Piauí (PI) - Teresina
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Encontro dos Rios Parnaíba-Potí', 'Passeio de barco para ver o encontro dos rios', 90.00, '4 horas', 'Teresina', 'PI'),
('Centro Cultural e Museus', 'Tour pelos principais museus e centros culturais', 55.00, '3 horas', 'Teresina', 'PI');
-- Piauí (PI) - Parnaíba
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Porto das Barcas e História', 'Tour pelo porto histórico e centro comercial', 65.00, '3 horas', 'Parnaíba', 'PI'),
('Delta do Parnaíba e Ilhas', 'Passeio pelo único delta em mar aberto das Américas', 120.00, '6 horas', 'Parnaíba', 'PI');
-- Piauí (PI) - Picos
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Serra e Mirante dos Picos', 'Passeio pela serra com vista panorâmica da cidade', 50.00, '3 horas', 'Picos', 'PI'),
('Feira Livre e Comércio Regional', 'Tour pela maior feira livre do sul do Piauí', 40.00, '2 horas', 'Picos', 'PI');
-- Rio de Janeiro (RJ) - Rio de Janeiro
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Cristo Redentor e Paineiras', 'Tour pelo Cristo Redentor com vista panorâmica da cidade', 150.00, '4 horas', 'Rio de Janeiro', 'RJ'),
('Pão de Açúcar e Bondinho', 'Passeio pelo bondinho e mirante do Pão de Açúcar', 130.00, '3 horas', 'Rio de Janeiro', 'RJ');
-- Rio de Janeiro (RJ) - São Gonçalo
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Alcântara e Centro Comercial', 'Tour pelo distrito comercial mais movimentado', 50.00, '3 horas', 'São Gonçalo', 'RJ'),
('Parque da Cidade e Lazer', 'Passeio pelo principal parque de lazer da cidade', 45.00, '2 horas', 'São Gonçalo', 'RJ');
-- Rio de Janeiro (RJ) - Duque de Caxias
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Refinaria e Polo Industrial', 'Tour pela região da refinaria e complexo industrial', 60.00, '3 horas', 'Duque de Caxias', 'RJ'),
('Parque Natural Municipal', 'Passeio pelo parque de preservação ambiental', 40.00, '2 horas', 'Duque de Caxias', 'RJ');
-- Rio Grande do Norte (RN) - Natal
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Fortaleza dos Reis Magos', 'Tour pela fortaleza histórica com vista para o mar', 70.00, '3 horas', 'Natal', 'RN'),
('Dunas de Genipabu e Buggy', 'Passeio de buggy pelas dunas e lagoa de Genipabu', 110.00, '4 horas', 'Natal', 'RN');
-- Rio Grande do Norte (RN) - Mossoró
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Centro Histórico e Resistência', 'Tour pelos locais de resistência ao bando de Lampião', 55.00, '3 horas', 'Mossoró', 'RN'),
('Termas e Balneário Municipal', 'Passeio pelas termas e área de lazer da cidade', 50.00, '3 horas', 'Mossoró', 'RN');
-- Rio Grande do Norte (RN) - Parnamirim
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Base Aérea e História', 'Tour pela base aérea e museu da aviação', 65.00, '3 horas', 'Parnamirim', 'RN'),
('Cotovelo e Praias do Sul', 'Passeio pelas praias da região metropolitana', 55.00, '4 horas', 'Parnamirim', 'RN');
-- Rio Grande do Sul (RS) - Porto Alegre
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque da Redenção e Feira', 'Tour pelo parque mais famoso com feira aos domingos', 65.00, '3 horas', 'Porto Alegre', 'RS'),
('Mercado Público e Centro Histórico', 'Passeio pelo mercado centenário e centro histórico', 70.00, '3 horas', 'Porto Alegre', 'RS');
-- Rio Grande do Sul (RS) - Caxias do Sul
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Catedral e Monumento ao Imigrante', 'Tour pelos símbolos da colonização italiana', 60.00, '3 horas', 'Caxias do Sul', 'RS'),
('Parque de Eventos e Festa da Uva', 'Visita ao local da famosa Festa da Uva', 55.00, '2 horas', 'Caxias do Sul', 'RS');
-- Rio Grande do Sul (RS) - Pelotas
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Charqueadas e História', 'Tour pelos antigos locais de produção de charque', 65.00, '3 horas', 'Pelotas', 'RS'),
('Praia do Laranjal e Orla', 'Passeio pela orla do Guaíba com áreas de lazer', 50.00, '3 horas', 'Pelotas', 'RS');
-- Rondônia (RO) - Porto Velho
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Estrada de Ferro Madeira-Mamoré', 'Tour pela ferrovia histórica e museu', 75.00, '3 horas', 'Porto Velho', 'RO'),
('Usina Hidrelétrica de Santo Antônio', 'Visita às áreas externas da usina hidrelétrica', 85.00, '4 horas', 'Porto Velho', 'RO');
-- Rondônia (RO) - Ji-Paraná
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Parque de Exposições e Agro', 'Tour pelo centro de eventos agropecuários', 55.00, '3 horas', 'Ji-Paraná', 'RO'),
('Rio Machado e Orla Urbana', 'Passeio pela orla do rio com áreas de lazer', 45.00, '2 horas', 'Ji-Paraná', 'RO');
-- Rondônia (RO) - Ariquemes
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Garimpo e História do Ouro', 'Tour pelos locais históricos da exploração de ouro', 60.00, '3 horas', 'Ariquemes', 'RO'),
('Parque Botânico Municipal', 'Passeio pelo parque com espécies da flora local', 40.00, '2 horas', 'Ariquemes', 'RO');
-- Roraima (RR) - Boa Vista
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Praça do Centro Cívico', 'Tour pela praça com arquitetura radial de Boa Vista', 60.00, '2 horas', 'Boa Vista', 'RR'),
('Orla Taumanan e Rio Branco', 'Passeio pela orla do rio com vista para o Monte Roraima', 55.00, '3 horas', 'Boa Vista', 'RR');
-- Roraima (RR) - Rorainópolis
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Serra do Tepequém e Cachoeiras', 'Tour pela serra com cachoeiras e garimpo desativado', 95.00, '5 horas', 'Rorainópolis', 'RR'),
('Comunidade Agrícola e Produção', 'Visita a comunidades rurais e produção local', 65.00, '4 horas', 'Rorainópolis', 'RR');
-- Roraima (RR) - Caracaraí
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Ponte sobre o Rio Branco', 'Tour pela ponte e área de transição de ecossistemas', 50.00, '2 horas', 'Caracaraí', 'RR'),
('Reserva Ecológica e Pesca', 'Passeio por áreas de preservação com opção de pesca', 75.00, '4 horas', 'Caracaraí', 'RR');
-- Santa Catarina (SC) - Joinville
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Rua das Palmeiras e Centro Histórico', 'Tour pela rua símbolo com arquitetura alemã', 65.00, '2 horas', 'Joinville', 'SC'),
('Festival de Dança e Teatro', 'Visita aos locais do maior festival de dança do mundo', 70.00, '3 horas', 'Joinville', 'SC');
-- Santa Catarina (SC) - Florianópolis
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Ponte Hercílio Luz e Centro', 'Tour pela ponte cartão postal e centro histórico', 75.00, '3 horas', 'Florianópolis', 'SC'),
('Lagoa da Conceição e Dunas', 'Passeio pela lagoa com visita às dunas e mirante', 80.00, '4 horas', 'Florianópolis', 'SC');
-- Santa Catarina (SC) - Blumenau
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Vila Germânica e Oktoberfest', 'Tour pelo complexo da oktoberfest e cultura alemã', 70.00, '3 horas', 'Blumenau', 'SC'),
('Rua XV de Novembro e Museus', 'Passeio pela rua principal com arquitetura europeia', 55.00, '2 horas', 'Blumenau', 'SC');
-- São Paulo (SP) - São Paulo
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Paulista e MASP', 'Tour pela avenida Paulista com visita ao MASP', 90.00, '3 horas', 'São Paulo', 'SP'),
('Mercado Municipal e Centro', 'Passeio pelo Mercadão com degustação de frutas', 75.00, '3 horas', 'São Paulo', 'SP');
-- São Paulo (SP) - Guarulhos
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Aeroporto e Logística', 'Tour pela região do aeroporto internacional', 60.00, '3 horas', 'Guarulhos', 'SP'),
('Parque da Cidade e Lazer', 'Passeio pelo principal parque urbano', 45.00, '2 horas', 'Guarulhos', 'SP');
-- São Paulo (SP) - Campinas
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Lagoa do Taquaral e Parque', 'Tour pelo parque da lagoa com monumentos', 65.00, '3 horas', 'Campinas', 'SP'),
('Centro de Convívio e Comércio', 'Passeio pelo centro comercial e cultural', 55.00, '2 horas', 'Campinas', 'SP');
-- Sergipe (SE) - Aracaju
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Orla de Atalaia e Passarela', 'Passeio pela orla mais famosa com centro de artesanato', 70.00, '3 horas', 'Aracaju', 'SE'),
('Centro Histórico e Catedral', 'Tour pelo centro com arquitetura antiga', 55.00, '2 horas', 'Aracaju', 'SE');
-- Sergipe (SE) - Nossa Senhora do Socorro
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Mirante e Vista para Aracaju', 'Tour pelo mirante com vista panorâmica da região metropolitana', 45.00, '2 horas', 'Nossa Senhora do Socorro', 'SE'),
('Parque Municipal e Lazer', 'Passeio pelo principal parque da cidade', 40.00, '2 horas', 'Nossa Senhora do Socorro', 'SE');
-- Sergipe (SE) - Lagarto
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Termas e Balneário', 'Passeio pelas termas medicinais da cidade', 50.00, '3 horas', 'Lagarto', 'SE'),
('Feira Livre e Comércio', 'Tour pela feira e centro comercial regional', 35.00, '2 horas', 'Lagarto', 'SE');
-- Tocantins (TO) - Palmas
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Praia da Graciosa e Orla', 'Passeio pela praia fluvial e orla da capital', 60.00, '3 horas', 'Palmas', 'TO'),
('Parque Cesamar e Trilhas', 'Tour pelo parque municipal com trilhas ecológicas', 45.00, '2 horas', 'Palmas', 'TO');
-- Tocantins (TO) - Araguaína
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Polo Universitário e Pesquisa', 'Tour pelas universidades e centros de pesquisa', 55.00, '3 horas', 'Araguaína', 'TO'),
('Parque de Exposições e Agro', 'Visita ao centro de eventos agropecuários', 50.00, '2 horas', 'Araguaína', 'TO');
-- Tocantins (TO) - Gurupi
INSERT INTO Passeio (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado) VALUES 
('Universidade Federal e Desenvolvimento', 'Tour pela UFT e centros de desenvolvimento tecnológico', 60.00, '3 horas', 'Gurupi', 'TO'),
('Parque Mutuca e Lazer', 'Passeio pelo parque municipal com áreas de esporte', 45.00, '2 horas', 'Gurupi', 'TO');



