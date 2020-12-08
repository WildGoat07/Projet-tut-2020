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
            var response = await Client.PostAsync(url, new StringContent(@"
{
    ""filters"":
    {
        ""prenom"":
        [
            ""Henri"",
            ""Kamel""
        ],
        ""HMax"":
        {
            ""min"":100,
            ""max"":200
        }
    },
    ""quantity"":1,
    ""skip"":1
}", Encoding.UTF8, "application/json"));
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion Private Methods
    }
}