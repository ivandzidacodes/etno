using EtnoSelo.DAL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Helpers
{
    public class Helpers
    {
        public static void ConvertRazorViewToPdfAndreturnAsResponse(string viewName, object model, ControllerContext controllerContext, 
            ViewDataDictionary viewData, TempDataDictionary tempData, HttpResponseBase response)
        {
            try
            {
                viewData.Model = model;
                string html = "";
                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(controllerContext,
                                                                             viewName);
                    var viewContext = new ViewContext(controllerContext, viewResult.View,
                                                 viewData, tempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                    html = sw.GetStringBuilder().ToString();
                }

                KreirajPdf(html, response);
            }
            catch(Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(Helpers), nameof(ConvertRazorViewToPdfAndreturnAsResponse));
            }
        }

        public static void KreirajPdf(string html, HttpResponseBase response)
        {
            try
            {
                //MemoryStream ms = new MemoryStream();
                TextReader reader = new StringReader(html);

                Document document = new Document(PageSize.A4, 30, 30, 30, 30);
                HTMLWorker worker = new HTMLWorker(document);
                PdfWriter writer = PdfWriter.GetInstance(document, response.OutputStream);

                document.Open();
                worker.Parse(reader);
                document.Close();

                response.ContentType = "application/pdf";
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.Write(document);
                response.AddHeader("Content-Disposition", "attachment;filename=\"Report" + DateTime.Now.Ticks + ".pdf\"");
                response.End();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(Helpers), nameof(KreirajPdf));
            }
        }

        public static void SnimiSlikeUFolder(string imeFoldera, IEnumerable<HttpPostedFileBase> slike)
        {
            try
            {
                if (slike == null)
                    return;

                //Nadji radni direktorij
                var path = HttpContext.Current.Server.MapPath("~/Slike");

                //Ako direktorij ne postoji kreiraj ga
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + "\\" + imeFoldera;
                //Ako direktorij ne postoji kreiraj ga
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Sacuvaj orginalnu logaciju foldera
                var pathBak = path;
                //Prodji kroz sve fjlove
                foreach (HttpPostedFileBase slika in slike)
                {
                    try
                    {
                        //Napravi lokaciju na koju ce se snimiti fajl
                        path = $"{pathBak}\\{slika.FileName.Replace(" ", "")}";
                        if (File.Exists(path))
                        {
                            //Ako fajl sa istim imenom vec postoji dodaj u ime random broj
                            path = $"{pathBak}\\{DateTime.Now.Ticks}{slika.FileName.Replace(" ", "")}";
                        }

                        //snimi fajl
                        slika.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        LoggingHelper.Greska(ex, nameof(Helpers), nameof(SnimiSlikeUFolder));
                    }

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(Helpers), nameof(SnimiSlikeUFolder));
            }
        }

        public static List<string> DobaviImenaNaziveSlikaZaPonudu(int ponudaId)
        {
            try
            {
                var radniDirektorij = HttpContext.Current.Server.MapPath("~/Slike");
                var path = radniDirektorij + "\\" + ponudaId.ToString();

                var lista = Directory.GetFiles(path).Select(x => Path.GetFileName(x)).ToList();

                return lista;
            }
            catch(Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(Helpers), nameof(SnimiSlikeUFolder));
                return new List<string>();
            }
        }
    }
}