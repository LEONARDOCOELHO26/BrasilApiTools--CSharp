using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace BrasilApiTools.Tools
{
    public class DDD
    {
        public static async Task GetDDDInfo()
        {
            // Solicita o DDD ao usuário
            Console.WriteLine("Digite o DDD:");
            string DDD = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o DDD contém 2 dígitos
            if (DDD.Length != 2 || !long.TryParse(DDD, out _))
            {
                Console.WriteLine("DDD inválido. O DDD deve conter 2 dígitos.");
                return;
            }

            // Define a URL com o DDD inserido
            string url = $"https://brasilapi.com.br/api/ddd/v1/{DDD}";

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

                        // Exibe a resposta completa no console para depuração
                        //Console.WriteLine("Resposta JSON recebida:");
                        //Console.WriteLine(jsonResponse);

                        // Desserializa a string JSON em um objeto C#
                        var dddInfo = JsonSerializer.Deserialize<DddResponse>(jsonResponse);

                        if (dddInfo != null)
                        {
                            // Exibe as informações do DDD
                            Console.WriteLine("\nInformações do DDD:");
                            Console.WriteLine($"Estado: {dddInfo.state}");
                            Console.WriteLine("Cidades:");
                            foreach (var city in dddInfo.cities)
                            {
                                Console.WriteLine($"- {city}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("DDD não encontrado ou todos os serviços de DDD retornaram erro.");
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

        public class DddResponse
        {
            public string state { get; set; }
            public List<string> cities { get; set; }
        }
    }
}
