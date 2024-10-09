using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrasilApiTools.Tools
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Dictionary<string, Func<Task>> apiOptions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayTermsOfUse },
                { "2", Banks.GetBankInfo },
                { "3", cepV1.GetCepInfo },
                { "4", CepV2.GetCepInfo },
                { "5", cnpj.GetCnpjInfo },
                { "6", corretoras.GetCorretorasInfo },
                { "7", DDD.GetDDDInfo },
                { "8", feriados.GetFeriadosInfo },
                { "9", DisplayNotImplemented },
                { "10", DisplayNotImplemented },
                { "11", DisplayNotImplemented },
                { "12", DisplayNotImplemented },
                { "13", DisplayNotImplemented },
                { "14", DisplayNotImplemented },
                { "15", DisplayNotImplemented },
            };

            while (true)
            {
                DisplayWelcomeMessage();
                string opcao = Console.ReadLine();

                if (apiOptions.ContainsKey(opcao))
                {
                    await apiOptions[opcao]();
                }
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Saindo do programa...");
                    break; // Encerra o loop e sai do programa
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente ou digite 's' para sair.");
                }
            }
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Seja Bem-vindo");
            Console.WriteLine("Esse é uma demonstração das ferramentas do site: https://brasilapi.com.br");
            Console.WriteLine("Use com responsabilidade");
            Console.WriteLine("Digite qual API você gostaria de testar:");
            Console.WriteLine("1 - Termos de Uso");
            Console.WriteLine("2 - Bancos");
            Console.WriteLine("3 - CEP");
            Console.WriteLine("4 - CEP v2");
            Console.WriteLine("5 - CNPJ");
            Console.WriteLine("6 - CPTEC");
            Console.WriteLine("7 - DDD");
            Console.WriteLine("8 - Feriados Nacionais");
            Console.WriteLine("9 - FIPE");
            Console.WriteLine("10 - IBGE");
            Console.WriteLine("11 - ISBN");
            Console.WriteLine("12 - NCM");
            Console.WriteLine("13 - PIX");
            Console.WriteLine("14 - Registro BR");
            Console.WriteLine("15 - Taxas");
            Console.WriteLine("Digite 's' para sair.");
        }

        private static async Task DisplayTermsOfUse()
        {
            Console.WriteLine("-------------Termos de Uso-------------");
            Console.WriteLine("O BrasilAPI é uma iniciativa feita de brasileiros para brasileiros, por favor, não abuse deste serviço.");
            Console.WriteLine("Estamos em beta e ainda elaborando os Termos de Uso, mas por enquanto por favor não utilize formas automatizadas para fazer crawling ou full scan dos dados da API.");
            Console.WriteLine("Nunca Faça-----------------------------");
            Console.WriteLine("-- Requisições em loop, por exemplo, ceps de 00000000 a 99999999");
            // Adicione o restante do texto conforme necessário
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static Task DisplayNotImplemented()
        {
            Console.WriteLine("Serviço ainda não implementado.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}
