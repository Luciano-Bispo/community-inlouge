CREATE DATABASE communityInLounge;

USE communityInLounge;

CREATE TABLE Tipo_usuario(
 Tipo_usuario_id INT IDENTITY PRIMARY KEY NOT NULL,
 Titulo VARCHAR(30) UNIQUE NOT NULL,
 Deletedo_em DATETIME
 );

 SELECT * FROM Tipo_usuario;

 CREATE TABLE Usuario(
 Usuario_id INT IDENTITY PRIMARY KEY NOT NULL,
 Nome VARCHAR(100) NOT NULL,
 Email VARCHAR(100) UNIQUE NOT NULL,
 Senha VARCHAR(255) NOT NULL, 
 Telefone VARCHAR(20) NOT NULL,
 Foto VARCHAR(255) NOT NULL,
 Genero VARCHAR(30) NOT NULL,
 Deletedo_em DATETIME,
 Tipo_usuario_id INT FOREIGN KEY REFERENCES Tipo_usuario(Tipo_usuario_id) NOT NULL
 );

 SELECT * FROM Usuario;

 CREATE TABLE Comunidade(
 Comunidade_id INT IDENTITY PRIMARY KEY NOT NULL,
 Nome VARCHAR(100) NOT NULL, 
 Descricao VARCHAR(255) NOT NULL,
 Email_contato VARCHAR(100) NOT NULL,
 Telefone_contato VARCHAR(20) NOT NULL,
 Foto VARCHAR(255) NOT NULL,
 Deletedo_em DATETIME,
 Responsavel_usuario_id INT FOREIGN KEY REFERENCES Usuario(Usuario_id) NOT NULL
 );

 SELECT * FROM Comunidade;

 CREATE TABLE Categoria(
  Categoria_id INT IDENTITY PRIMARY KEY NOT NULL, 
  Nome VARCHAR(100) NOT NULL
  );

  SELECT * FROM Categoria;

  CREATE TABLE Sala(
  Sala_id INT IDENTITY PRIMARY KEY NOT NULL,
  Nome VARCHAR(100) NOT NULL,
  Descricao VARCHAR(255) NOT NULL,
  Qntd_pessoas VARCHAR(20) NOT NULL,
  Deletedo_em DATETIME
  );

  SELECT * FROM Sala;

 

  CREATE TABLE Evento(
  Evento_id INT IDENTITY PRIMARY KEY NOT NULL,
  Nome VARCHAR(100) NOT NULL,
  Evento_data DATE NOT NULL,
  Horario TIME NOT NULL,
  Descricao VARCHAR(255) NOT NULL, 
  Email_contato VARCHAR(255) NOT NULL,
  Publico VARCHAR(255) NOT NULL,
  Status_evento VARCHAR(255) NOT NULL, 
  Diversidade VARCHAR(3) NOT NULL,
  Coffe VARCHAR(3) NOT NULL,
  Foto VARCHAR(255),
  Deletedo_em DATETIME,
  Categoria_id INT FOREIGN KEY REFERENCES Categoria(Categoria_id) NOT NULL,
  Sala_id INT FOREIGN KEY REFERENCES Sala(Sala_id) NOT NULL,
  Comunidade_id INT FOREIGN KEY REFERENCES Comunidade(Comunidade_id) NOT NULL
  );

   SELECT * FROM Evento;

  CREATE TABLE Responsavel_evento_tw(
  Responsavel_evento_tw_id INT IDENTITY PRIMARY KEY NOT NULL,
  Evento INT FOREIGN KEY REFERENCES Evento(Evento_id) NOT NULL,
  Responsavel_evento INT FOREIGN KEY REFERENCES Usuario(Usuario_id) NOT NULL,
  Deletedo_em DATETIME
  );

  SELECT * FROM Responsavel_evento_tw;

  INSERT INTO Tipo_usuario (Titulo)
 VALUES('Funcionário'),
 ('Administrador'),
 ('Comunidade');

 INSERT INTO Usuario(Nome, Email, Senha, Telefone, Foto, Genero, Tipo_usuario_id)
 VALUES ('Alexandre', 'alexandre@gmail.com','alexandre123','055011999665005','URL', 'Masculino', 2),
 ('Rafaela', 'rafaela2gmail.com', 'rafaela123', '055011988776655','URL','Feminino',3),
 ('Daniela','daniela@gmail.com', 'daniela123', '055011922337788','URL', 'Feminino', 1);

 SELECT *FROM Usuario;

 SELECT *FROM Comunidade;


  INSERT INTO Comunidade(Nome, Descricao, Email_contato, Telefone_contato, Foto, Responsavel_usuario_id)
 VALUES ('Nerdezão','O NerdZão é um grupo do conhecimento sobre softwares, plataformas, tecnologias e linguagens de programação de forma divertida, quebrando o paradigma de complexidade no aprendizado.', 'nerdezao_contato@gmail.com', '055011335544', 'URL',2),
 ('Microsoft','Atualmente a Microsoft está com treinamento e eventos para o público, com ensinamentos co gestores,educadores e diretores escolar ','reprograma_contato@gmail.com', '05501122556677', 'URL',3);


 INSERT INTO Categoria(Nome)
VALUES('Tecnologia'),
('Empregabilidade'),
('Educação');

INSERT INTO Sala(Nome, Descricao, Qntd_pessoas)
  VALUES('Lounge', 'Sala com ambiente moderno, com progetor e TV', '25 pessoas'),
  ('Sala de reunião','Sala com ambiente proprício a reuniões com progetor para apresentações', '50 pessoas');

  SELECT * FROM Evento;

   INSERT INTO Evento (Nome, Evento_data, Horario, Descricao, Email_contato, Publico, Status_evento, Diversidade, Coffe, Foto, Categoria_id, Sala_id, Comunidade_id)
   VALUES ('Microsoft Ignite The Tour', '2019/11/12', '20:00:01', 'O Microsoft Ignite The Tour traz o melhor do Microsoft Ignite para uma cidade perto de você. O tour fornece treinamentos técnicos conduzidos por especialistas da Microsoft e a sua comunidade.', 'microsoft_contato@gmail.com','Evento público','Pendente','Não', 'Sim','URL',1,2,1),
   ('Cloud Girls Recife', '2019/11/06', '18:00:01', 'O Cloud Girls nasceu para que as mulheres se sintam a vontade para falar de tecnologia', 'cloudgirls_contato@gmail.com','Evento público','Aprovado','Sim', 'Sim','URL',1,2,4),
  ('Nerdgirlz #27 - Smile with your Mobile', '2019/12/04', '19:00:01', 'Nesse encontro PRESENCIAL e GRATUITO, vamos falar sobre Desenvolvimento Mobile e conteúdos relacionados a como melhorar sua aplicação e fazer com que todos os usuários desejem seu app.', 'nerdezao_contato@gmail.com','Evento público','Pendente','Sim', 'Sim','URL',1,2,3);

  SELECT * FROM Evento;


  INSERT INTO Responsavel_evento_tw(Evento, Responsavel_evento)
  VALUES(4,2),
  (2,3),
  (3,2);




