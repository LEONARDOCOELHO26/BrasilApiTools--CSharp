using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    public class corretoras
    {
        // Método para obter informações da corretora
        public static async Task GetCorretorasInfo()
        {
            // Solicita o CNPJ ao usuário
            Console.WriteLine("Digite o CNPJ:");
            string cnpj = Console.ReadLine();  // Lê a entrada do usuário

            // Verifica se o CNPJ contém 14 dígitos
            if (cnpj.Length != 14 || !long.TryParse(cnpj, out _))
            {
                Console.WriteLine("CNPJ inválido. O CNPJ deve conter 14 dígitos.");
                return;
            }

            // Define a URL com o CNPJ inserido
            string url = $"https://brasilapi.com.br/api/cvm/corretoras/v1/{cnpj}";

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

                        // Desserializa a string JSON em um objeto CorretoraInfo
                        var corretoraInfo = JsonSerializer.Deserialize<CorretoraInfo>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (corretoraInfo != null)
                        {
                            // Exibe as informações da corretora
                            Console.WriteLine($"CNPJ: {corretoraInfo.Cnpj}");
                            Console.WriteLine($"Nome Social: {corretoraInfo.NomeSocial}");
                            Console.WriteLine($"Nome Comercial: {corretoraInfo.NomeComercial}");
                            Console.WriteLine($"Status: {corretoraInfo.Status}");
                            Console.WriteLine($"Email: {corretoraInfo.Email}");
                            Console.WriteLine($"Telefone: {corretoraInfo.Telefone}");
                            Console.WriteLine($"CEP: {corretoraInfo.Cep}");
                            Console.WriteLine($"UF: {corretoraInfo.Uf}");
                            Console.WriteLine($"Município: {corretoraInfo.Municipio}");
                            Console.WriteLine($"Bairro: {corretoraInfo.Bairro}");
                            Console.WriteLine($"Complemento: {corretoraInfo.Complemento}");
                            Console.WriteLine($"Logradouro: {corretoraInfo.Logradouro}");
                            Console.WriteLine($"Data do Patrimônio Líquido: {corretoraInfo.DataPatrimonioLiquido}");
                            Console.WriteLine($"Valor do Patrimônio Líquido: {corretoraInfo.ValorPatrimonioLiquido:C}"); // Formatação de moeda
                            Console.WriteLine($"Código CVM: {corretoraInfo.CodigoCvm}");
                            Console.WriteLine($"Data de Início da Situação: {corretoraInfo.DataInicioSituacao}");
                            Console.WriteLine($"Data de Registro: {corretoraInfo.DataRegistro}");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("CNPJ não encontrado ou todos os serviços de CNPJ retornaram erro.");
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
    public class CorretoraInfo
    {
        public string Cnpj { get; set; }
        public string Type { get; set; }

        [JsonPropertyName("nome_social")]
        public string NomeSocial { get; set; }

        [JsonPropertyName("nome_comercial")]
        public string NomeComercial { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public string DataPatrimonioLiquido { get; set; }
        public decimal ValorPatrimonioLiquido { get; set; }
        public string CodigoCvm { get; set; }
        public string DataInicioSituacao { get; set; }
        public string DataRegistro { get; set; }
    }
}
