using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class banksList
    {
        // Método para obter informações dos bancos
        public static async Task GetBanksInfo()
        {
            // Define a URL para os bancos
            string url = "https://brasilapi.com.br/api/banks/v1";

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

                        // Desserializa a string JSON em uma lista de bancos
                        var banksInfo = JsonSerializer.Deserialize<List<BanksResponse>>(jsonResponse);

                        if (banksInfo != null)
                        {
                            // Exibe as informações dos bancos
                            foreach (var bank in banksInfo)
                            {
                                Console.WriteLine($"ISPB: {bank.ispb}");
                                Console.WriteLine($"Nome: {bank.name}");
                                Console.WriteLine($"Código: {bank.code}");
                                Console.WriteLine($"Nome Completo: {bank.fullName}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Bancos não encontrados.");
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
    public class BanksResponse
    {
        [JsonPropertyName("ispb")]
        public string ispb { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("code")]
        public int code { get; set; }

        [JsonPropertyName("fullName")]
        public string fullName { get; set; }
    }
}
