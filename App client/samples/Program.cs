using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace samples
{
    internal class Program
    {
        private static HttpClient Client;

        #region Private Methods

        private static async Task Main(string[] args)
        {
            Client = new HttpClient();
            await TestRequest();
        }

        private static async Task TestRequest()
        {
            var url = new Uri("http://localhost/Projet-tut-2020/API/enseignant.php");
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(
@"{
   ""filters"":{
      ""id_ens"":""cg462"",
      ""prenom"":""Jean""
   }
}", Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion Private Methods
    }
}