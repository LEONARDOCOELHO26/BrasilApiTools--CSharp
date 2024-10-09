using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class Fipe
    {
        public static async Task GetFipeInfo()
        {
            // Solicita o código FIPE ao usuário
            Console.WriteLine("Digite o código FIPE:");
            string codigoFipe = Console.ReadLine();  // Lê a entrada do usuário

            string url = $"https://brasilapi.com.br/api/fipe/preco/v1/{codigoFipe}";

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

                        // Desserializa a string JSON em uma lista de FipeResponse
                        var fipeInfoList = JsonSerializer.Deserialize<List<FipeResponse>>(jsonResponse);

                        if (fipeInfoList != null && fipeInfoList.Count > 0)
                        {
                            // Exibe as informações do veículo
                            foreach (var fipeInfo in fipeInfoList)
                            {
                                Console.WriteLine($"Valor: {fipeInfo.valor}");
                                Console.WriteLine($"Marca: {fipeInfo.marca}");
                                Console.WriteLine($"Modelo: {fipeInfo.modelo}");
                                Console.WriteLine($"Ano do Modelo: {fipeInfo.anoModelo}");
                                Console.WriteLine($"Combustível: {fipeInfo.combustivel}");
                                Console.WriteLine($"Código FIPE: {fipeInfo.codigoFipe}");
                                Console.WriteLine($"Mês de Referência: {fipeInfo.mesReferencia}");
                                Console.WriteLine($"Tipo de Veículo: {fipeInfo.tipoVeiculo}");
                                Console.WriteLine($"Sigla do Combustível: {fipeInfo.siglaCombustivel}");
                                Console.WriteLine($"Data da Consulta: {fipeInfo.dataConsulta}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Código FIPE não encontrado ou todos os serviços retornaram erro.");
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

        // Classe para representar a estrutura da resposta da tabela FIPE
        public class FipeResponse
        {
            public string valor { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public int anoModelo { get; set; }
            public string combustivel { get; set; }
            public string codigoFipe { get; set; }
            public string mesReferencia { get; set; }
            public int tipoVeiculo { get; set; }
            public string siglaCombustivel { get; set; }
            public string dataConsulta { get; set; }
        }
    }
}
