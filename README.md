
# PlacemarkFilter API

**PlacemarkFilter API** é uma Web API desenvolvida em .NET para filtrar e manipular elementos do tipo Placemark de arquivos KML. A API oferece funcionalidades de filtragem, listagem e exportação com base em critérios personalizados.

---

## Funcionalidades Principais

### 1. Filtragem de Elementos
- **CLIENTE**: Pré-seleção de valores disponíveis.
- **SITUAÇÃO**: Pré-seleção de valores disponíveis.
- **BAIRRO**: Pré-seleção de valores disponíveis.
- **REFERÊNCIA**: Texto parcial com no mínimo 3 caracteres.
- **RUA/CRUZAMENTO**: Texto parcial com no mínimo 3 caracteres.

### 2. Exportação de Dados
- Cria um novo arquivo KML com os elementos filtrados.

### 3. Listagem em Formato JSON
- Retorna os elementos filtrados em formato JSON.

### 4. Obtenção de Filtros Disponíveis
- Retorna valores únicos disponíveis para **CLIENTE**, **SITUAÇÃO** e **BAIRRO**.

---

## Endpoints Disponíveis

### 1. Exportar Novo Arquivo KML
**Rota**: `/api/placemarks/export`  
**Método**: `POST`  
**Parâmetros**:  
- `CLIENTE` (opcional)
- `SITUAÇÃO` (opcional)
- `BAIRRO` (opcional)
- `REFERÊNCIA` (opcional, mínimo 3 caracteres)
- `RUA/CRUZAMENTO` (opcional, mínimo 3 caracteres)

**Retorno**: Um novo arquivo KML contendo os elementos filtrados.  

**Exemplo**:
```json
{
  "BAIRRO": "PORTO DANTAS"
}
```
![Exportar Exemplo](https://github.com/user-attachments/assets/87addf22-8e98-45e2-9d8f-50451a3b849f)

---

### 2. Listar Elementos em Formato JSON
**Rota**: `/api/placemarks`  
**Método**: `GET`  
**Parâmetros (como query parameters)**:  
- `CLIENTE` (opcional)
- `SITUAÇÃO` (opcional)
- `BAIRRO` (opcional)
- `REFERÊNCIA` (opcional, mínimo 3 caracteres)
- `RUA/CRUZAMENTO` (opcional, mínimo 3 caracteres)

**Retorno**: Lista de elementos filtrados em formato JSON.  

**Exemplo**:
```json
{
  "rua/cruzamento": "SAN"
}
```
![Listar Exemplo](https://github.com/user-attachments/assets/35c83a22-d5af-4208-a028-83d20c329fa2)

---

### 3. Obter Valores Disponíveis para Filtragem
**Rota**: `/api/placemarks/filters`  
**Método**: `GET`  
**Retorno**: Valores únicos disponíveis para os campos **CLIENTE**, **SITUAÇÃO** e **BAIRRO**.  

![Obter Valores Exemplo](https://github.com/user-attachments/assets/14e413ac-24d7-495d-9a27-a0cfbaa8f04f)

---

## Requisitos de Filtragem

- **CLIENTE**, **SITUAÇÃO** e **BAIRRO**: Apenas valores previamente lidos e disponíveis são aceitos.
- **REFERÊNCIA** e **RUA/CRUZAMENTO**: Texto parcial com no mínimo 3 caracteres.
- **Validação**: Se qualquer filtro for passado incorretamente ou com valores não válidos, a API retorna um erro 400 com uma mensagem apropriada.

---

## Requisitos Técnicos

- **Plataforma**: .NET 6.0 ou superior

### Princípios Seguidos:
- Programação orientada a objetos (POO)
- Princípios SOLID para organização e manutenção do código
- Foco em performance e boas práticas de desenvolvimento

---

## Como Utilizar

### 1. Clone o Repositório
```bash
git clone https://github.com/seu-usuario/PlacemarkFilterAPI.git
cd PlacemarkFilterAPI
```

### 2. Configuração
Siga as instruções de configuração para rodar a API em seu ambiente de desenvolvimento.

---

## Estrutura do Projeto PlacemarkFilter API

Este projeto adota o padrão de **Clean Architecture**, organizando o código em camadas bem definidas para manter a separação de responsabilidades, escalabilidade, fácil manutenção e testes independentes.

### Camadas da Clean Architecture

- **Core (Domínio)**: Contém as entidades e interfaces principais, definindo as regras de negócios de forma pura e isolada de implementações específicas.
- **Application**: Contém casos de uso e serviços para manipular as regras de negócios, aplicando padrões como *Builder* e *Strategy* para manter a lógica desacoplada e clara.
- **Infrastructure**: Responsável pela implementação de repositórios, persistência de dados e acesso a sistemas externos. Inclui a leitura de arquivos KML por meio do `KmlRepository`.
- **Presentation**: Contém a lógica para a exposição de APIs e interação com o mundo externo, incluindo os controllers que definem os endpoints REST.

---

## Padrões de Projeto Utilizados

### 1. Padrão Strategy para Filtragem
Utilizado para gerenciar diferentes estratégias de filtragem dos elementos Placemark com base em campos como **CLIENTE**, **SITUAÇÃO**, **BAIRRO**, **REFERÊNCIA** e **RUA/CRUZAMENTO**.

**Exemplo**:
```csharp
public class ClientFilterStrategy : IFilterStrategy
{
    public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
    {
        return placemarks.Where(p => p.Cliente == filterValue).ToList();
    }
}
```

### 2. Padrão Builder para Construção de Objetos
Facilita a criação de objetos Placemark complexos, oferecendo uma interface fluente para definir valores e garantir que a instância final seja válida.

**Exemplo**:
```csharp
public class PlacemarkBuilder
{
    private readonly Placemark _placemark = new Placemark();

    public PlacemarkBuilder SetCliente(string cliente)
    {
        _placemark.Cliente = string.IsNullOrWhiteSpace(cliente) ? null : cliente.Trim();
        return this;
    }

    public PlacemarkBuilder SetSituacao(string situacao)
    {
        _placemark.Situacao = string.IsNullOrWhiteSpace(situacao) ? null : situacao.Trim();
        return this;
    }

    public Placemark Build()
    {
        return _placemark;
    }
}
```

---

## Arquitetura em Camadas

A arquitetura segue os princípios de **Clean Architecture** para manter responsabilidades separadas e garantir fácil manutenção:

- **Domínio/Core**: Define as entidades (`Placemark`) e interfaces (`IKmlRepository`).
- **Aplicação**: Contém serviços (`KmlService`) e builders (`PlacemarkBuilder`), encapsulando a lógica de negócios.
- **Infraestrutura**: Implementa a persistência de dados, como o `KmlRepository`.
- **Apresentação**: Expõe a API para interação com os clientes.

---

## Considerações Finais

- **Princípios SOLID**: Garante baixo acoplamento e alta coesão.
- **Flexibilidade e Testabilidade**: Facilita a adição de novos filtros, estratégias de manipulação de dados ou modificações no comportamento, mantendo a lógica de negócios isolada e facilmente testável.

A abordagem garante que o projeto seja robusto, extensível e fácil de manter, com flexibilidade para futuras mudanças e melhorias.
