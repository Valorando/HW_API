create database APIdb;

use APIdb;

create table Notes
(
id int auto_increment primary key,
noteid int not null unique,
note nvarchar(1000) not null    
);

select * from Notes;