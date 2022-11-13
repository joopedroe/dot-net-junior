create database db_clients;
use db_clients;
create table address (
id int  primary key identity,
road varchar(100) not null,
number int not null,
district varchar(100) not null,
city varchar(100) not null,
cep varchar(20) not null,
type_address varchar(20),
complement varchar(200))

create table client (
id int  primary key identity,
name varchar(200) not null,
cpf_cnpj varchar(20) not null,
type_client varchar(20) not null,
address_id int foreign key references address ON DELETE CASCADE)

create table phones (
id int  primary key identity,
identification varchar(50) not null,
ddd varchar(5) not null,
number varchar(10) not null,
type_phone varchar(20) not null,
client_id int foreign key references client)