using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class Banks
    {
        // Método para obter informações do banco
        public static async Task GetBankInfo()
        {
            // Solicita o código ao usuário
            Console.WriteLine("Digite o código do banco:");
            string code = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o código contém 3 dígitos
            if (code.Length != 3 || !int.TryParse(code, out _))
            {
                Console.WriteLine("Código inválido. O código do banco deve conter 3 dígitos.");
                return;
            }

            // Define a URL com o código inserido
            string url = $"https://brasilapi.com.br/api/banks/v1/{code}";

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
                        var banksInfo = JsonSerializer.Deserialize<BankResponse>(jsonResponse);

                        if (banksInfo != null)
                        {
                            // Exibe as informações do banco
                            Console.WriteLine($"Código: {banksInfo.Code}");
                            Console.WriteLine($"ISPB: {banksInfo.Ispb}");
                            Console.WriteLine($"Nome: {banksInfo.Name}");
                            Console.WriteLine($"Nome Completo: {banksInfo.FullName}");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Código não encontrado ou todos os serviços de código retornaram erro.");
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
    // Classe para mapear a resposta JSON
    public class BankResponse
    {
        [JsonPropertyName("ispb")]
        public string Ispb { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }  

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
    }

}
