using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Data;
using System.Web.Helpers;

namespace QuasarERPBanking.Controllers
{
    public class CentroCostoGraficoController : ParentController
    {
        // GET: CentroCostoGrafico
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dashboard(FormCollection _pagina)
        {
            return RedirectToAction("Create","CentroCostoGrafico",_pagina );
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            var hasta = DateTime.Now.ToString();
            var desde = DateTime.Today.AddDays(-180).ToString();
            DateTime fechad = Convert.ToDateTime(desde);
            DateTime fechah = Convert.ToDateTime(hasta);
            int anoinicio = Convert.ToInt32(fechad.Month.ToString());
            int anofin = Convert.ToInt32(fechah.Month.ToString());
            int intervalo = Convert.ToInt32(anofin) - Convert.ToInt32(anoinicio);
            intervalo++;
            string[] titulo = new string[intervalo];
            decimal[] resultado = new decimal[intervalo];
            int linea = 0;
            Graficos grf = new Models.Graficos();
            string query = "";
            //cc_transacciones
            query = "Select distinct(month(cc_fetrans)),sum(cc_monto) from " + ParametrosGlobales.bd + "cc_transacciones where cc_fetrans between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "'  group by month(cc_fetrans) order by month(cc_fetrans)";
            //input
            query = "select distinct(inpvdm),sum(inpdra) from " + ParametrosGlobales.bd + "input where inpvdm between '" + Convert.ToDateTime(fechad).Month.ToString() + "' and '" + Convert.ToDateTime(fechah).Month.ToString() + "' and inpvdy=" + Convert.ToInt32(fechah.ToString("yy")) + " and inpgln like '4%' group by inpvdm order by inpvdm"  ;
            grf.query = query;
            DataSet ds = grf.dts();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado[linea] = Convert.ToDecimal(dr[1].ToString());
                string fecha = "01-" + dr[0].ToString() + "-" + DateTime.Now.Year.ToString();
                DateTime fechat = Convert.ToDateTime(fecha);
                titulo[linea] = fechat.ToString("MMMM");
                
                linea++;
            }
            string[] CadenaTitulo = new string[linea];
            decimal[] CadenaMonto = new decimal[linea];
            List<DashBoard> lista1 = new List<DashBoard>();
            for (int x = 0; x < linea; x++)
            {
                DashBoard lis = new DashBoard();
                CadenaTitulo[x] = titulo[x].ToString();
                CadenaMonto[x] = resultado[x];
                lis.mes = titulo[x].ToString();
                lis.monto = resultado[x].ToString("###,##0.00");
                lista1.Add(lis);

            }
            Chart gr1 = new Chart(300,300,ChartTheme.Blue); 
            gr1.AddTitle("Gastos Mensuales Generales");
            gr1.AddSeries("GR1", chartType: "range", xValue: CadenaTitulo, yValues: CadenaMonto);
            //gr2 = new Chart(200, 200, theme: ChartTheme.Vanilla);
            string[] titulo2 = new string[intervalo];
            decimal[] resultado2 = new decimal[intervalo];
            linea = 0;
            query = "Select distinct(month(fetrans)),sum(total) from " + ParametrosGlobales.bd + "cxp_transacciones where fetrans between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' and tpodoc='9'  group by month(fetrans) order by month(fetrans)";
            grf.query = query;
            ds = grf.dts();
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado2[linea] = Convert.ToDecimal(dr[1].ToString());
                string fecha = "01-" + dr[0].ToString() + "-" + DateTime.Now.Year.ToString();
                DateTime fechat = Convert.ToDateTime(fecha);
                titulo2[linea] = fechat.ToString("MMMM");
                linea++;
            }
            string[] CadenaTitulo2 = new string[linea];
            decimal[] CadenaMonto2 = new decimal[linea];
            
            for (int x = 0; x < linea; x++)
            {
                
                CadenaTitulo2[x] = titulo2[x].ToString();
                CadenaMonto2[x] = resultado2[x];
           }
            Chart gr2 = new Chart(500, 300, ChartTheme.Yellow);
            
            
            gr2.AddTitle("Pagos Realizados Mensuales");
            gr2.AddSeries("Pagos", chartType: "Doughnut",  xValue: CadenaTitulo2, yValues: CadenaMonto2);
            gr2.AddLegend("Meses");
            
            //gr3 = new Chart(300, 300, theme: ChartTheme.Vanilla3D);
            string[] titulo3 = new string[intervalo];
            decimal[] resultado3 = new decimal[intervalo];
            linea = 0;
            query = "Select distinct(month(invtransfecha)),sum(invtranscosto*invtranscant) from " + ParametrosGlobales.bd + "inv_transacciones where invtransfecha between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' and invtranstipo='9'  group by month(invtransfecha) order by month(invtransfecha)";
            grf.query = query;
            ds = grf.dts();
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado3[linea] = Convert.ToDecimal(dr[1].ToString());
                string fecha = "01-" + dr[0].ToString() + "-" + DateTime.Now.Year.ToString();
                DateTime fechat = Convert.ToDateTime(fecha);
                titulo3[linea] = fechat.ToString("MMMM");
                linea++;
            }
            string[] CadenaTitulo3 = new string[linea];
            decimal[] CadenaMonto3 = new decimal[linea];
            List<DashBoard> lista3 = new List<DashBoard>();
            for (int x = 0; x < linea; x++)
            {
                DashBoard a = new DashBoard();
                CadenaTitulo3[x] = titulo3[x].ToString();
                CadenaMonto3[x] = resultado3[x];
                a.mes = titulo3[x].ToString();
                a.monto = resultado3[x].ToString("###,##0.00");
                lista3.Add(a);

            }
            Chart gr3 = new Chart(500, 300, ChartTheme.Green);

            string[] titulo4 = new string[intervalo];
            decimal[] resultado4 = new decimal[intervalo];
            linea = 0;
            query = "Select distinct(month(invtransfecha)),sum(invtranscosto*invtranscant) from " + ParametrosGlobales.bd + "inv_transacciones where invtransfecha between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' and invtranstipo='11'  group by month(invtransfecha) order by month(invtransfecha)";
            grf.query = query;
            ds = grf.dts();
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado4[linea] = Convert.ToDecimal(dr[1].ToString());
                string fecha = "01-" + dr[0].ToString() + "-" + DateTime.Now.Year.ToString();
                DateTime fechat = Convert.ToDateTime(fecha);
                titulo4[linea] = fechat.ToString("MMMM");
                linea++;
            }
            string[] CadenaTitulo4 = new string[linea];
            decimal[] CadenaMonto4 = new decimal[linea];
            List<DashBoard> lista4 = new List<DashBoard>();
            for (int x = 0; x < linea; x++)
            {
                DashBoard a = new DashBoard();
                CadenaTitulo4[x] = titulo4[x].ToString();
                CadenaMonto4[x] = resultado4[x];
                a.mes = titulo4[x].ToString();
                a.monto = resultado4[x].ToString("###,##0.00");
                lista4.Add(a);
            }
            gr3.AddTitle("Movimientos de Inventario");
            gr3.AddSeries("Entradas", chartType: "SplineArea", xValue: CadenaTitulo4, yValues: CadenaMonto4);
            gr3.AddSeries("Salidas", chartType: "SplineRange", xValue: CadenaTitulo3, yValues: CadenaMonto3);
            gr3.AddLegend("tipos de Movimientos");
            
            
            byte[] gra1 = gr1.GetBytes(format: "jpg");
            byte[] gra2 = gr2.GetBytes(format: "jpg");
            byte[] gra3 = gr3.GetBytes(format: "jpg");
            ViewBag.grafo1 = "data:image/png;base64," + Convert.ToBase64String(gra1, 0, gra1.Length);
            ViewBag.grafo2 = "data:image/png;base64," + Convert.ToBase64String(gra2, 0, gra2.Length);
            ViewBag.grafo3 = "data:image/png;base64," + Convert.ToBase64String(gra3, 0, gra3.Length);
            ViewBag.lista1 = lista1;
            ViewBag.lista3 = lista3;
            ViewBag.lista4 = lista4;
            return View();
        }

        [HttpPost]
        public ActionResult Create_old(FormCollection  pagina)
        {
            var desde = pagina["fechacombo"].ToString();
            var hasta = pagina["fechacomboh"];
            var tipo = pagina["tipo"];
            var tema = pagina["tema"];
            var cuenta = pagina["cuenta"];
            string cuenta_ = "";
            if (cuenta!="")
            {
                cuenta_ = " and cc_tpgastos like '" + cuenta + "' ";
            }
            DateTime fechad = Convert.ToDateTime(desde);
            DateTime fechah = Convert.ToDateTime(hasta);
            int anoinicio = Convert.ToInt32( fechad.Year.ToString());
            int anofin = Convert.ToInt32(fechah.Year.ToString());
            int intervalo = Convert.ToInt32( anofin)-Convert.ToInt32(anoinicio);
            intervalo++;
            string[] titulo = new string[intervalo];
            decimal[] resultado=new decimal[intervalo];
            int linea = 0;
            Graficos grf = new Models.Graficos();
            string query = "Select distinct(year(cc_fetrans)),sum(cc_monto) from " + ParametrosGlobales.bd + "cc_transacciones where cc_fetrans between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' " + cuenta_ + " group by year(cc_fetrans)";
            grf.query = query;
            DataSet ds= grf.dts();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado[linea] = Convert.ToDecimal( dr[1].ToString());
                titulo[linea] = dr[0].ToString();
                linea++;
            }
            string[] CadenaTitulo = new string[linea];
            decimal[] CadenaMonto = new decimal[linea];
            for (int x=0;x<linea;x++)
            {
                CadenaTitulo[x] = titulo[x].ToString();
                CadenaMonto[x] = resultado[x];
            }

            ViewBag.titulo = CadenaTitulo;
            ViewBag.resultado = CadenaMonto;
            ViewBag.tipo = tipo.ToString();
            ViewBag.tema = "ChartTheme." + tema.ToString();
            ViewBag.Cantidad = linea;
            return View();
            

        }
        [HttpPost]
        //public ActionResult  Index(FormCollection pagina)
        //{
        //    var desde = pagina["fechacombo"].ToString();
        //    var hasta = pagina["fechacomboh"];
        //    var tipo = pagina["tipo"];
        //    var tema = pagina["tema"];
        //    var cuenta = pagina["cuenta"];
        //    string cuenta_ = "";
        //    if (cuenta != "")
        //    {
        //        cuenta_ = " and cc_tpgastos like '" + cuenta + "' ";
        //    }
        //    DateTime fechad = Convert.ToDateTime(desde);
        //    DateTime fechah = Convert.ToDateTime(hasta);
        //    int anoinicio = Convert.ToInt32(fechad.Year.ToString());
        //    int anofin = Convert.ToInt32(fechah.Year.ToString());
        //    int intervalo = Convert.ToInt32(anofin) - Convert.ToInt32(anoinicio);
        //    intervalo++;
        //    string[] titulo = new string[intervalo];
        //    decimal[] resultado = new decimal[intervalo];
        //    int linea = 0;
        //    Graficos grf = new Models.Graficos();
        //    string query = "Select distinct(year(cc_fetrans)),sum(cc_monto) from " + ParametrosGlobales.bd + "cc_transacciones where cc_fetrans between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' " + cuenta_ + " group by year(cc_fetrans)";
        //    grf.query = query;
        //    DataSet ds = grf.dts();
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        resultado[linea] = Convert.ToDecimal(dr[1].ToString());
        //        titulo[linea] = dr[0].ToString();
        //        linea++;
        //    }
        //    string[] CadenaTitulo = new string[linea];
        //    decimal[] CadenaMonto = new decimal[linea];
        //    for (int x = 0; x < linea; x++)
        //    {
        //        CadenaTitulo[x] = titulo[x].ToString();
        //        CadenaMonto[x] = resultado[x];
        //    }
        //    Chart gr1 = new Chart(400, 300, ChartTheme.Blue);
        //    switch (tema)
        //    {
        //        case "Blue":
        //            {
        //                gr1 = new Chart(400, 300, theme: ChartTheme.Vanilla);
        //                break;
        //            }
        //        case "Green":
        //            {
        //                gr1 = new Chart(400, 300, theme: ChartTheme.Green);
        //                break;
        //            }
        //        case "Yellow":
        //            {
        //                gr1 = new Chart(400, 300, theme: ChartTheme.Yellow);
        //                break;
        //            }
        //        case "Vanilla":
        //            {
        //                gr1 = new Chart(400, 300, theme: ChartTheme.Vanilla);
        //                break;
        //            }
        //        case "Vanilla3D":
        //            {
        //                gr1 = new Chart(400, 300, theme: ChartTheme.Vanilla3D);
        //                break;
        //            }

        //    }
        //    //gr1 = new Chart(300, 300, theme: ChartTheme.Vanilla);
        //    gr1.AddTitle("GR1");
        //    gr1.AddSeries("GR1", chartType: tipo, xValue: CadenaTitulo, yValues: CadenaMonto);
        //    byte[] gra1 = gr1.GetBytes(format: "jpg");
        //    ViewBag.grafo1 = "data:image/png;base64," + Convert.ToBase64String(gra1, 0, gra1.Length);
        //    return View();
        //}
        
        public JsonResult  Index(string termino, string termino2, string termino3, string termino4, string termino5)
        {
            //var desde = pagina["fechacombo"].ToString();
            //var hasta = pagina["fechacomboh"];
            //var tipo = pagina["tipo"];
            //var tema = pagina["tema"];
            //var cuenta = pagina["cuenta"];
            var desde=termino;
            var hasta=termino2;
            var tipo =termino4;
            var tema =termino5;
            var cuenta =termino3;
            string cuenta_ = "";
            if (cuenta != "")
            {
                cuenta_ = " and cc_tpgastos like '" + cuenta + "' ";
            }
            DateTime fechad = Convert.ToDateTime(desde);
            DateTime fechah = Convert.ToDateTime(hasta);
            int anoinicio = Convert.ToInt32(fechad.Year.ToString());
            int anofin = Convert.ToInt32(fechah.Year.ToString());
            int intervalo = Convert.ToInt32(anofin) - Convert.ToInt32(anoinicio);
            intervalo++;
            string[] titulo = new string[intervalo];
            decimal[] resultado = new decimal[intervalo];
            int linea = 0;
            Graficos grf = new Models.Graficos();
            string query = "Select distinct(year(cc_fetrans)),sum(cc_monto) from " + ParametrosGlobales.bd + "cc_transacciones where cc_fetrans between '" + fechad.ToString(ParametrosGlobales.dfmt) + "' and '" + fechah.ToString(ParametrosGlobales.dfmt) + "' " + cuenta_ + " group by year(cc_fetrans)";
            grf.query = query;
            DataSet ds = grf.dts();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                resultado[linea] = Convert.ToDecimal(dr[1].ToString());
                titulo[linea] = dr[0].ToString();
                linea++;
            }
            string[] CadenaTitulo = new string[linea];
            decimal[] CadenaMonto = new decimal[linea];
            for (int x = 0; x < linea; x++)
            {
                CadenaTitulo[x] = titulo[x].ToString();
                CadenaMonto[x] = resultado[x];
            }
            Chart gr1 = new Chart(400, 300, ChartTheme.Blue);
            switch (tema)
            {
                case "Blue":
                    {
                        gr1 = new Chart(400, 300, theme: ChartTheme.Blue);
                        break;
                    }
                case "Green":
                    {
                        gr1 = new Chart(400, 300, theme: ChartTheme.Green);
                        break;
                    }
                case "Yellow":
                    {
                        gr1 = new Chart(400, 300, theme: ChartTheme.Yellow);
                        break;
                    }
                case "Vanilla":
                    {
                        gr1 = new Chart(400, 300, theme: ChartTheme.Vanilla);
                        break;
                    }
                case "Vanilla3D":
                    {
                        gr1 = new Chart(400, 300, theme: ChartTheme.Vanilla3D);
                        break;
                    }

            }
            //gr1 = new Chart(300, 300, theme: ChartTheme.Vanilla);
            gr1.AddTitle("GR1");
            gr1.AddSeries("GR1", chartType: tipo, xValue: CadenaTitulo, yValues: CadenaMonto);
            byte[] gra1 = gr1.GetBytes(format: "jpg");
            ViewBag.grafo1 = "data:image/png;base64," + Convert.ToBase64String(gra1, 0, gra1.Length);
            
            return Json(new { base64imgage = Convert.ToBase64String(gra1) }, JsonRequestBehavior.AllowGet);
        }
    }
}