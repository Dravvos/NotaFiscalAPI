create database Nota_Fiscal
go
use Nota_Fiscal
go
create table NotaFiscal(notaFiscalId uniqueidentifier primary key,
numeroNf int not null,
valorTotal decimal(10,2) not null,
dataNf datetime not null,
cnpjEmissorNf varchar(max) not null,
cnpjDestinatarioNf varchar(max) not null)

drop database DevPartner