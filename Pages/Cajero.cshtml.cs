using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace practica3.Pages
{
    public class CajeroModel : PageModel
    {
        private const int maximoTransaccion = 10000;
        private const int minimoTransaccion = 2000;
        public int totalDisponible=28400;
        public int cantidadARetirar { get; set; } = 0;

        public bool error { get; set; } = false;
        public string mensaje { get; set; } = "";

        public readonly List<Billete> billetes;
        
        public CajeroModel()
        {
            this.billetes = new List<Billete>()
            {
                new Billete(){valor=1000,cantidad=9},
                new Billete(){valor=500,cantidad=19},
                new Billete(){valor=100,cantidad=99},
            };
        }
        public void OnPost(int cantidadARetirar)
        {
            if (cantidadARetirar > totalDisponible)
            {
                this.error = true;
                this.mensaje = "El cajero no tiene suficiente fondos para realizar la transaccion";
                return;
            }
            if (cantidadARetirar > maximoTransaccion)
            {
                this.error = true;
                this.mensaje = "El monto excede el limite de la transaccion";
                return;
            }
            if (cantidadARetirar < minimoTransaccion)
            {
                this.error = true;
                this.mensaje = "El monto esta por debajo el limite de la transaccion";
                return;
            }
            this.error = false;
            foreach (var item in this.billetes)
            {
                item.CantidadUsados = 0;
                this.obtenerCantidades(ref cantidadARetirar, item);
            }
            if (cantidadARetirar>0)
            {
                this.error = true;
                this.mensaje = "No se puede realizar esta transaccion";
                foreach (var item in billetes)
                {
                    item.cantidad += item.CantidadUsados;
                    item.CantidadUsados = 0;
                }
                return;
            }
            this.totalDisponible -= cantidadARetirar;
        }
        public void OnGet()
        {           
        }
        private int obtenerCantidades( ref int cantidadARetirar,Billete billete)
        {
            if (billete.cantidad==0)
            {
                return 0;
            }
            var residuoDivision = cantidadARetirar % billete.valor;
            var cantBilletes = cantidadARetirar / billete.valor;
            if (residuoDivision>0)
            {
                billete.cantidad = 0;
            }
            else
            {
                billete.cantidad -= cantBilletes;
            }
            var retirado = cantBilletes * billete.valor;
            cantidadARetirar -= retirado;
            billete.CantidadUsados = cantBilletes;
            return retirado;

        }
    }
    public class Billete
    {
        public int valor { get; set; }
        public int cantidad { get; set; }
        public int CantidadUsados { get; set; } = 0;
    }
}
