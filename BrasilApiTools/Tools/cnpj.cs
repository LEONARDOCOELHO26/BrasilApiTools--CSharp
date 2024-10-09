using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    public class cnpj
    {
        // Método para obter informações do CEP
        public static async Task GetCnpjInfo()
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
                       // Console.WriteLine("Resposta JSON recebida:");
                        //Console.WriteLine(jsonResponse);

                        // Desserializa a string JSON em um objeto CnpjResponse
                        var cnpjInfo = JsonSerializer.Deserialize<CnpjResponse>(jsonResponse);

                        if (cnpjInfo != null)
                        {
                            // Exibe as informações do CNPJ
                            Console.WriteLine($"CNPJ: {cnpjInfo.Cnpj}");
                            Console.WriteLine($"Razão Social: {cnpjInfo.RazaoSocial}");
                            Console.WriteLine($"Nome Fantasia: {cnpjInfo.NomeFantasia}");
                            Console.WriteLine($"Situação Cadastral: {cnpjInfo.DescricaoSituacaoCadastral}");
                            Console.WriteLine($"Data de Situação Cadastral: {cnpjInfo.DataSituacaoCadastral}");
                            Console.WriteLine($"Início da Atividade: {cnpjInfo.DataInicioAtividade}");
                            Console.WriteLine($"Logradouro: {cnpjInfo.Logradouro}, Número: {cnpjInfo.Numero}");
                            Console.WriteLine($"Bairro: {cnpjInfo.Bairro}");
                            Console.WriteLine($"Município: {cnpjInfo.Municipio} - UF: {cnpjInfo.Uf}");
                            Console.WriteLine($"Telefone: {cnpjInfo.Telefone1}");
                            Console.WriteLine($"Email: {cnpjInfo.Email}");

                            // Exibe os sócios
                            if (cnpjInfo.Qsa != null && cnpjInfo.Qsa.Count > 0)
                            {
                                Console.WriteLine("Sócios:");
                                foreach (var socio in cnpjInfo.Qsa)
                                {
                                    Console.WriteLine($"- Nome: {socio.NomeSocio}, Qualificação: {socio.QualificacaoSocio}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro ao desserializar o JSON.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("CNPJ não encontrado.");
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
        public class CnpjResponse
    {
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonPropertyName("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonPropertyName("situacao_cadastral")]
        public int SituacaoCadastral { get; set; }

        [JsonPropertyName("descricao_situacao_cadastral")]
        public string DescricaoSituacaoCadastral { get; set; }

        [JsonPropertyName("data_situacao_cadastral")]
        public string DataSituacaoCadastral { get; set; }

        [JsonPropertyName("data_inicio_atividade")]
        public string DataInicioAtividade { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("telefone_1")]
        public string Telefone1 { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("qsa")]
        public List<Socio> Qsa { get; set; }

        // Adicione mais propriedades conforme necessário
    }

    public class Socio
    {
        [JsonPropertyName("nome_socio")]
        public string NomeSocio { get; set; }

        [JsonPropertyName("qualificacao_socio")]
        public string QualificacaoSocio { get; set; }

        [JsonPropertyName("cnpj_cpf_do_socio")]
        public string CnpjCpfDoSocio { get; set; }

        // Adicione mais propriedades conforme necessário
    }

}

