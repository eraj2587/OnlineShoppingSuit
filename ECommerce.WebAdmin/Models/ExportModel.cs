using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FileHelpers;

namespace ECommerce.WebAdmin.Models
{
    public static class ExportModel
    {
        public static string GetCSV<T>(List<T> list) where T:class 
        {
            var engine = new FileHelperEngine<T>();
            var path =  Path.GetTempFileName();
            engine.WriteFile(path,list);
            return path;
        }
    }
}