create database MasterPiece;

create table cart (id int primary key identity(1,1) ,product_id varchar(50) , quantity int ,user_id int);

create table user_ (id int primary key identity(1,1) ,product_id varchar(50) , pass varchar(50) ,name varchar(50), email varchar(50), adress varchar(max),phoneNo int );

create table admin (id int primary key identity(1,1) , category varchar(50) ,product_id varchar(50) , pass varchar(50) ,name varchar(50), email varchar(50), adress varchar(max),phoneNo int );

create table category (id int primary key identity (1,1), name varchar(50));

create table product_list (id int primary key identity(1,1) ,name varchar(50), price int, description varchar(max), img varchar(max));
