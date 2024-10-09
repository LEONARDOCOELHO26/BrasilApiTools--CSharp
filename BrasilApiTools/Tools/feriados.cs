using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace BrasilApiTools.Tools
{
    internal class feriados
    {
        public static async Task GetFeriadosInfo()
        {
            // Solicita o ano ao usuário
            Console.WriteLine("Digite o ANO:");
            string ano = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o ano contém 4 dígitos
            if (ano.Length != 4 || !long.TryParse(ano, out _))
            {
                Console.WriteLine("Ano inválido. O ano deve conter 4 dígitos.");
                return;
            }

            // Define a URL com o ano inserido
            string url = $"https://brasilapi.com.br/api/feriados/v1/{ano}";

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
                        // Console.WriteLine("Resposta JSON recebida:");
                        //Console.WriteLine(jsonResponse);

                        // Desserializa a string JSON em uma lista de feriados
                        var feriadosInfo = JsonSerializer.Deserialize<List<FeriadoResponse>>(jsonResponse);

                        if (feriadosInfo != null)
                        {
                            // Exibe as informações dos feriados
                            Console.WriteLine("\nFeriados do Ano:");
                            foreach (var feriado in feriadosInfo)
                            {
                                Console.WriteLine($"Data: {feriado.date}, Nome: {feriado.name}, Tipo: {feriado.type}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Ano não encontrado ou todos os serviços de feriados retornaram erro.");
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

        // Classe para representar a estrutura do feriado
        public class FeriadoResponse
        {
            public string date { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }
    }
}
