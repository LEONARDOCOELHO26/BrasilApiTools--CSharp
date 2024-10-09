using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class pix
    {
        // Método para obter informações dos participantes do PIX
        public static async Task GetPixInfo()
        {
            // Define a URL para obter os participantes
            string url = "https://brasilapi.com.br/api/pix/v1/participants";

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

                        // Desserializa a string JSON em uma lista de PixResponse
                        var pixInfoList = JsonSerializer.Deserialize<List<PixResponse>>(jsonResponse);

                        if (pixInfoList != null)
                        {
                            // Exibe as informações de cada participante
                            foreach (var pixInfo in pixInfoList)
                            {
                                Console.WriteLine($"ISPB: {pixInfo.ispb}");
                                Console.WriteLine($"Nome: {pixInfo.nome}");
                                Console.WriteLine($"Nome Reduzido: {pixInfo.nome_reduzido}");
                                Console.WriteLine($"Modalidade de Participação: {pixInfo.modalidade_participacao}");
                                Console.WriteLine($"Tipo de Participação: {pixInfo.tipo_participacao}");
                                Console.WriteLine($"Início da Operação: {pixInfo.inicio_operacao}");
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
                        Console.WriteLine("Nenhum participante encontrado ou todos os serviços retornaram erro.");
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
    public class PixResponse
    {
        [JsonPropertyName("ispb")]
        public string ispb { get; set; }

        [JsonPropertyName("nome")]
        public string nome { get; set; }

        [JsonPropertyName("nome_reduzido")]
        public string nome_reduzido { get; set; } // Corrigido para string

        [JsonPropertyName("modalidade_participacao")]
        public string modalidade_participacao { get; set; }

        [JsonPropertyName("tipo_participacao")]
        public string tipo_participacao { get; set; }

        [JsonPropertyName("inicio_operacao")]
        public string inicio_operacao { get; set; }
    }
}
