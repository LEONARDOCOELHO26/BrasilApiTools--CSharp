using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    public class CPTEC
    {
        public static async Task GetCorretorasInfo()
        {
            // Solicita o CEP ao usuário
            Console.WriteLine("Digite o CNPJ:");
            string cnpj = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o CEP contém 8 dígitos
            if (cnpj.Length != 14 || !long.TryParse(cnpj, out _))
            {
                Console.WriteLine("cnpj inválido. O cnpj deve conter 14 dígitos.");
                return;
            }

            // Define a URL com o CEP inserido
            string url = $"https://brasilapi.com.br/api/cnpj/v1/{cnpj}";

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
                        Console.WriteLine("Resposta JSON recebida:");
                        Console.WriteLine(jsonResponse);

                        // Desserializa a string JSON em um objeto C#
                        var cepInfo = JsonSerializer.Deserialize<CepResponse>(jsonResponse);

                        if (cepInfo != null)
                        {
                            // Exibe as informações do CEP
                            //Console.WriteLine($"CEP: {cepInfo.Cep}");
                            //Console.WriteLine($"Estado: {cepInfo.State}");
                            //Console.WriteLine($"Cidade: {cepInfo.City}");
                            //Console.WriteLine($"Bairro: {cepInfo.Neighborhood}");
                            //Console.WriteLine($"Rua: {cepInfo.Street}");
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
}
