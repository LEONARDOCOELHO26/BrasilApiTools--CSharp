using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class taxasList
    {
        // Método para obter informações de taxas
        public static async Task GetTaxasInfo()
        {
            // Define a URL para as taxas
            string url = "https://brasilapi.com.br/api/taxas/v1";

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

                        // Desserializa a string JSON em uma lista de taxas
                        var taxasInfo = JsonSerializer.Deserialize<List<TaxasResponse>>(jsonResponse);

                        if (taxasInfo != null)
                        {
                            // Exibe as informações das taxas
                            foreach (var taxas in taxasInfo)
                            {
                                Console.WriteLine($"Nome: {taxas.nome}");
                                Console.WriteLine($"Valor: {taxas.valor}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Taxas não encontradas.");
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
    }

    // Classe para mapear a resposta JSON
    public class TaxasResponse
    {
        [JsonPropertyName("nome")]
        public string nome { get; set; }

        [JsonPropertyName("valor")]
        public double valor { get; set; }
    }
}

