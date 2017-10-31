using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebClient
{
    class Program
    {
        private static void Main(string[] args)
        {
           
            //GET
            //GetPing();

            //POST
            PostImage();


        }
        //Get Request
        static async void GetPing()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52796/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                var response = client.GetAsync("/image/ping");
                Console.WriteLine(await response.Result.Content.ReadAsStringAsync());
                Console.ReadKey();
            }

        }

        //Get Request
        static async void PostImage()
        {
            var file = @"../../Files/Hydrangeas.jpg";
            var content = new MultipartFormDataContent();

            var filestream = new FileStream(file, FileMode.Open);
            var fileName = Path.GetFileName(file);
            content.Add(new StreamContent(filestream), "file", fileName);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52796/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                var response = client.PostAsync("/image/upload",content);
                Console.WriteLine(await response.Result.Content.ReadAsStringAsync());
                Console.ReadKey();
            }


        }
    }
}
