using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class domain
    {
        // Método para obter informações de registro de domínio
        public static async Task GetDomainInfo()
        {
            // Solicita o domínio ao usuário
            Console.WriteLine("Digite o domínio (exemplo: brasilapi.com.br):");
            string domain = Console.ReadLine(); // Lê a entrada do usuário

            // Define a URL com o domínio inserido
            string url = $"https://brasilapi.com.br/api/registrobr/v1/{domain}";

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

                        // Desserializa a string JSON em um objeto C#
                        var domainInfo = JsonSerializer.Deserialize<DomainResponse>(jsonResponse);

                        if (domainInfo != null)
                        {
                            // Exibe as informações do domínio
                            Console.WriteLine($"FQDN: {domainInfo.fqdn}");
                            Console.WriteLine($"Status: {domainInfo.status}");
                            Console.WriteLine($"Status Code: {domainInfo.status_code}");
                            Console.WriteLine($"Publicação: {domainInfo.publication_status}");
                            Console.WriteLine($"Data de Expiração: {domainInfo.expires_at}");

                            Console.WriteLine("Servidores DNS:");
                            foreach (var host in domainInfo.hosts)
                            {
                                Console.WriteLine($"- {host}");
                            }

                            Console.WriteLine("Sugestões de domínios:");
                            foreach (var suggestion in domainInfo.suggestions)
                            {
                                Console.WriteLine($"- {suggestion}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Domínio não encontrado.");
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
    public class DomainResponse
    {
        [JsonPropertyName("status_code")]
        public int status_code { get; set; }

        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("fqdn")]
        public string fqdn { get; set; }

        [JsonPropertyName("hosts")]
        public string[] hosts { get; set; }

        [JsonPropertyName("publication-status")]
        public string publication_status { get; set; }

        [JsonPropertyName("expires-at")]
        public string expires_at { get; set; }

        [JsonPropertyName("suggestions")]
        public string[] suggestions { get; set; }
    }
}

