using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class fipeMarcas
    {
        public static async Task GetFipeMarcas()
        {
            // Solicita o tipo de veículo ao usuário
            Console.WriteLine("Digite o tipo de veículo (caminhoes, carros ou motos):");
            string tipoVeiculo = Console.ReadLine();  // Lê a entrada do usuário

            // Define a URL correta para a API da Fipe
            string url = $"https://brasilapi.com.br/api/fipe/marcas/v1/{tipoVeiculo}";

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

                        // Desserializa a string JSON em uma lista de objetos
                        var marcas = JsonSerializer.Deserialize<List<MarcaResponse>>(jsonResponse);

                        if (marcas != null)
                        {
                            // Exibe as informações das marcas
                            Console.WriteLine("\nMarcas disponíveis:");
                            foreach (var marca in marcas)
                            {
                                Console.WriteLine($"- Nome: {marca.nome}, Valor: {marca.valor}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Tipo de veículo não encontrado ou não disponível.");
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

        public class MarcaResponse
        {
            public string nome { get; set; }
            public string valor { get; set; }
        }
    }
}
