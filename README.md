# BrasilApiTools

Esta pasta foi criada como parte de um desafio e para demonstrar as APIs disponíveis no site [BrasilAPI](https://brasilapi.com.br/).

## Termos de Uso

O BrasilAPI é uma iniciativa desenvolvida por brasileiros, para brasileiros. Pedimos que você utilize este serviço de forma responsável. Estamos atualmente em fase beta e ainda estamos elaborando os Termos de Uso. Até que os termos estejam finalizados, solicitamos que não utilize métodos automatizados para fazer crawling ou varreduras completas dos dados da API.

### O que NÃO fazer:

- **Requisições em loop**: Por exemplo, não faça requisições para todos os CEPs de 00000000 a 99999999. Um exemplo prático desse problema ocorreu quando um dos maiores provedores de telefonia do Brasil validou todos os CEPs, resultando em um volume de requisições que ultrapassou em cinco vezes o limite da nossa conta no servidor.

O volume de consultas deve refletir o comportamento de um usuário real solicitando dados específicos. Para consultas de alto volume automatizadas, planejamos oferecer soluções, como a possibilidade de download de toda a base de CEPs em uma única requisição.
