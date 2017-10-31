using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.WebAdmin.Models;

namespace ECommerce.WebAdmin.Infrastructure
{
    public class ExportResult : ActionResult
    {
        private List<OrderViewModel> _model;
        public ExportResult(List<OrderViewModel> model)
        {
            _model = model;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            string filename = "report.csv";
            string filepath = ExportModel.GetCSV(_model);
            byte[] filedata = File.ReadAllBytes(filepath);

            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", "attachment; filename=myfile.csv");
            response.AddHeader("Pragma", "public");
            response.BinaryWrite(filedata);
            response.Flush();
            response.Close();
            response.End();
        }
    }
}