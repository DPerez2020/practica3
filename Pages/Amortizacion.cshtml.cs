using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace practica3.Pages
{
    public class AmortizacionModel : PageModel
    {
        public string mensaje { get; set; } = "";

        public void OnGet(double monto,int cuotas,double interes)
        {
            var cuotaMensual= calculoCuotaMensual(monto, cuotas, interes);
            this.mensaje = "La cuota mensual es "+cuotaMensual.ToString("N2");
        }
        private double calculoCuotaMensual(double monto, int cuotas, double interes) {
            interes = interes / 100;
            return monto * (interes /(1- Math.Pow((1+interes),-cuotas)));
        }
    }
}
