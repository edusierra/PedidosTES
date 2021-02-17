CREATE PROCEDURE AcumuladoDepto
@inicio datetime = '20170901',
@fin datetime = '20170930'
as
select 
	Codigo = dep.CODDEP,
	Departamento = dep.NOMBRE,
	Total = sum(ite.PRECIO) 
from PEDIDO ped
inner join ITEMS ite on ite.NUMPEDIDO = ped.NUMPEDIDO
inner join cliente cli on cli.CODCLI = ped.CLIENTE
inner join CIUDAD ciu on ciu.CODCIU = cli.CIUDAD
inner join DEPARTAMENTO dep on dep.CODDEP = ciu.DEPARTAMENTO
where ped.FECHA between @inicio and @fin
group by
	dep.CODDEP,
	dep.NOMBRE
go

alter PROCEDURE ComisionVendedor
@inicio datetime = '20170901'
as
declare @fin datetime
SET @inicio = DATEADD(mm, DATEDIFF(mm,0,@inicio), 0)
SET @fin = EOMONTH(@inicio)
select
	*,
	comision = Total * 0.1
from (
select 
	Codigo = ven.CODVEND,
	Vendedor = ven.NOMBRE,
	Total = SUM(ite.PRECIO)
from PEDIDO ped
inner join ITEMS ite on ite.NUMPEDIDO = ped.NUMPEDIDO
inner join VENDEDOR ven on ven.CODVEND = ped.VENDEDOR
where ped.FECHA between @inicio and @fin
group by
	VEN.CODVEND,
	VEN.NOMBRE
) as acumulado
go