using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class ISBN
    {
        public static async Task GetBookInfo()
        {
            // Solicita o ISBN ao usuário
            Console.WriteLine("Digite o ISBN do livro:");
            string isbn = Console.ReadLine(); // Lê a entrada do usuário

            string url = $"https://brasilapi.com.br/api/isbn/v1/{isbn}";

            // Cria uma instância de HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Faz a requisição GET e aguarda a resposta
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verifica se a resposta foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo da resposta como uma string JSON
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Desserializa a string JSON em um objeto BookResponse
                        var bookInfo = JsonSerializer.Deserialize<BookResponse>(jsonResponse);

                        if (bookInfo != null)
                        {
                            // Exibe as informações do livro
                            Console.WriteLine($"Título: {bookInfo.title}");
                            Console.WriteLine($"Autores: {string.Join(", ", bookInfo.authors)}");
                            Console.WriteLine($"Editora: {bookInfo.publisher}");
                            Console.WriteLine($"Sinopse: {bookInfo.synopsis}");
                            Console.WriteLine($"Ano: {bookInfo.year}");
                            Console.WriteLine($"Páginas: {bookInfo.page_count}");
                            Console.WriteLine($"Dimensões: {bookInfo.dimensions.width} cm x {bookInfo.dimensions.height} cm");
                            Console.WriteLine($"Localização: {bookInfo.location}");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("ISBN não encontrado ou todos os serviços retornaram erro.");
                    }
                    else
                    {
                        Console.WriteLine("Erro na requisição: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        // Classe para representar a estrutura da resposta do livro
        public class BookResponse
        {
            public string isbn { get; set; }
            public string title { get; set; }
            public string[] authors { get; set; }
            public string publisher { get; set; }
            public string synopsis { get; set; }
            public Dimensions dimensions { get; set; }
            public int year { get; set; }
            public int page_count { get; set; }
            public string location { get; set; }
        }

        public class Dimensions
        {
            public double width { get; set; }
            public double height { get; set; }
            public string unit { get; set; }
        }
    }
}
