# PlacemarkFilterAPI

**PlacemarkFilterAPI** é uma Web API desenvolvida em .NET para filtrar, listar e exportar dados de arquivos KML. A API permite que usuários apliquem diferentes critérios de filtragem sobre elementos do tipo `Placemark` e fornece resultados em formatos JSON ou em um novo arquivo KML gerado.

## Funcionalidades

- **Filtragem Personalizada**: Filtra elementos com base nos campos `CLIENTE`, `SITUAÇÃO`, `BAIRRO`, `REFERENCIA` e `RUA/CRUZAMENTO`.
- **Exportação de Dados**: Cria e exporta um novo arquivo KML com base nos critérios de filtragem fornecidos.
- **Listagem em JSON**: Retorna elementos filtrados no formato JSON.
- **Endpoints REST**:
  - `/api/placemarks/export` (POST): Exporta um novo arquivo KML com base nos filtros aplicados.
  - `/api/placemarks` (GET): Lista elementos filtrados no formato JSON.
  - `/api/placemarks/filters` (GET): Retorna valores únicos dos campos `CLIENTE`, `SITUAÇÃO` e `BAIRRO`.

## Requisitos Técnicos

- .NET 6.0 (ou versão compatível)
- Leitura de arquivos KML
- Princípios de programação orientada a objetos (POO) e aderência aos princípios SOLID

## Como Usar

1. **Clone o Repositório**:

    ```bash
    git clone https://github.com/seu-usuario/PlacemarkFilterAPI.git
    cd PlacemarkFilterAPI
    ```

2. **Configuração**:
    - Certifique-se de que o arquivo `DIRECIONADORES1.kml` está disponível no diretório apropriado.

3. **Execute o Projeto**:

    ```bash
    dotnet run
    ```

4. **Endpoints Disponíveis**:

    - **Filtragem e Exportação (POST)**:
      - **Rota**: `/api/placemarks/export`
      - **Parâmetros**: `CLIENTE`, `SITUAÇÃO`, `BAIRRO`, `REFERENCIA` e `RUA/CRUZAMENTO`
      - **Retorno**: Novo arquivo KML com base nos filtros aplicados.

    - **Listar Elementos (GET)**:
      - **Rota**: `/api/placemarks`
      - **Parâmetros**: `CLIENTE`, `SITUAÇÃO`, `BAIRRO`, `REFERENCIA` e `RUA/CRUZAMENTO`
      - **Retorno**: Lista de elementos filtrados no formato JSON.

    - **Obter Elementos Disponíveis para Filtragem (GET)**:
      - **Rota**: `/api/placemarks/filters`
      - **Retorno**: Valores únicos dos campos `CLIENTE`, `SITUAÇÃO` e `BAIRRO`.

## Contribuindo

Sinta-se à vontade para contribuir com este projeto enviando pull requests, relatando problemas ou sugerindo melhorias!

## Licença

[Especifique a licença aqui, ex.: MIT License]
