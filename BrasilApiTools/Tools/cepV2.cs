using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    public class CepV2
    {
        // Método para obter informações do CEP
        public static async Task GetCepInfo()
        {
            // Solicita o CEP ao usuário
            Console.WriteLine("Digite o CEP:");
            string cep = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o CEP contém 8 dígitos
            if (cep.Length != 8 || !long.TryParse(cep, out _))
            {
                Console.WriteLine("CEP inválido. O CEP deve conter 8 dígitos.");
                return;
            }

            // Define a URL com o CEP inserido
            string url = $"https://brasilapi.com.br/api/cep/v2/{cep}";

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
                        // Console.WriteLine(jsonResponse);

                        // Desserializa a string JSON em um objeto C#
                        var cepInfo = JsonSerializer.Deserialize<CepResponse>(jsonResponse);

                        if (cepInfo != null)
                        {
                            // Exibe as informações do CEP
                            Console.WriteLine($"CEP: {cepInfo.Cep}");
                            Console.WriteLine($"Estado: {cepInfo.State}");
                            Console.WriteLine($"Cidade: {cepInfo.City}");
                            Console.WriteLine($"Bairro: {cepInfo.Neighborhood}");
                            Console.WriteLine($"Rua: {cepInfo.Street}");
                            // Console.WriteLine($"Serviço utilizado: {cepInfo.Service}");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("CEP não encontrado ou todos os serviços de CEP retornaram erro.");
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
    public class CepResponse
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }
}
