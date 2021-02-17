using System;
using System.Collections.Generic;
using System.Text;
using Ventas.Data;
using Ventas.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ventas.Code
{
    public class Business
    {
        public  IEnumerable<Departamento> ListaDepartamentos()
        {
            using (var db = new TesDbContext())
            {
                return db.Departamentos.OrderBy(x => x.Nombre).ToList();
            }
        }

        public IEnumerable<Ciudad> ListarCiudades(string CodDep)
        {
            using (var db = new TesDbContext())
            {
                return db.Ciudads.Where(x => x.Departamento == CodDep).OrderBy(x => x.Nombre).ToList();
            }
        }

        public IEnumerable<Cliente> ListarClientes(string CodCiu)
        {
            using (var db = new TesDbContext())
            {
                return db.Clientes.Where(x => x.Ciudad == CodCiu).OrderBy(x => x.Nombre).ToList();
            }
        }

        public List<Vendedor> ListarVendedores()
        {
            using (var db = new TesDbContext())
            {
                return db.Vendedors.ToList();
            }
        }

        public IEnumerable<Producto> ListarProductos()
        {
            using (var db = new TesDbContext())
            {
                return db.Productos.ToList();
            }
        }

        public IEnumerable<TotalDepto> TotalDepartamento(DateTime From, DateTime To)
        {
            var list = new List<TotalDepto>();
            try
            {
                using(var db = new TesDbContext())
                {
                    var data = db.TotalDepartamento.FromSqlRaw("execute dbo.AcumuladoDepto {0}, {1}", From.ToString("yyyyMMdd"), To.ToString("yyyyMMdd") );
                    foreach(var itm in data)
                    {
                        list.Add(itm);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return list;
        }

        public IEnumerable<ComisionVendedor> CalcularComision(DateTime From)
        {
            var list = new List<ComisionVendedor>();
            try
            {
                using (var db = new TesDbContext())
                {
                    var data = db.TotalComision.FromSqlRaw("execute dbo.ComisionVendedor {0}", From.ToString("yyyyMMdd"));
                    foreach (var itm in data)
                    {
                        list.Add(itm);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return list;
        }

        internal bool GuardarPedido(ModeloPedido model)
        {
            using(var db = new TesDbContext())
            {
                var tran = db.Database.BeginTransaction();
                try
                {
                    var next = db.SiguientePedido.FromSqlRaw("select Id=max(convert(bigint,numpedido)) from PEDIDO");
                    string id = (next.First().Id+1).ToString();
                    db.Pedidos.Add(new Pedido()
                    {
                        Cliente = model.Cliente,
                        Fecha = model.Fecha,
                        Vendedor = model.Vendedor,
                        Numpedido = id
                    }) ;
                    foreach(var itm in model.productos)
                    {
                        db.Items.Add(new Item()
                        {
                            Cantidad = itm.Cantidad,
                            Numpedido = id,
                            Precio = itm.Precio,
                            Producto = itm.CodProd,
                            Subtotal = itm.Subtotal
                        }) ;
                    }
                    db.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
    }
}
