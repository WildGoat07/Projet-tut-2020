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
            var url = new Uri("http://localhost/Projet-tut-2020/API/ue/CUe.php");
            var response = await Client.PostAsync(url, new StringContent(@"
{
    ""values"":
    [
        {
            ""code_ue"":""testCodeUe"",
            ""libelle_ue"":""testLibelleUe"",
            ""nature"":""T"",
            ""ECTS"":0000,
            ""code_ue_pere"":""tCodeUeP"",
            ""code_sem"":""testCodeSem""
        }
    ]
}", Encoding.UTF8, "application/json"));
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion Private Methods
    }
}