﻿using Microsoft.AspNetCore.Mvc;
using PlacemarkFilter.Domain.Interfaces.Services;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace PlacemarkFilter.API.Controllers
{
    [ApiController]
    [Route("api/placemarks")]
    public class PlacemarkController : ControllerBase
    {
        private readonly IKmlService _kmlService;
        private readonly string _filePath;

        public PlacemarkController(IKmlService kmlService, IConfiguration configuration)
        {
            _kmlService = kmlService;

            // Obtém o caminho do arquivo KML a partir da configuração
            _filePath = configuration.GetValue<string>("KmlFilePath");

            // Verifica se o arquivo existe durante a inicialização
            if (string.IsNullOrEmpty(_filePath) || !System.IO.File.Exists(_filePath))
            {
                throw new FileNotFoundException($"O arquivo KML especificado em 'KmlFilePath' não foi encontrado: {_filePath}");
            }
        }

        [HttpPost("export")]
        public IActionResult ExportFilteredKml([FromBody] Dictionary<string, string> filters)
        {
            try
            {
                var placemarks = _kmlService.LoadPlacemarks(_filePath);
                var filteredPlacemarks = _kmlService.FilterPlacemarks(placemarks, filters);

                if (!filteredPlacemarks.Any())
                {
                    return NotFound("Nenhum placemark encontrado com os filtros fornecidos.");
                }

                // TODO: Lógica para exportar o novo KML (pode ser implementada conforme necessário)
                // Exemplo de retorno de dados filtrados (substitua conforme necessário)
                return Ok(filteredPlacemarks);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Erro de E/S ao acessar o arquivo: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ListFilteredPlacemarks([FromQuery] Dictionary<string, string> filters)
        {
            try
            {
                var placemarks = _kmlService.LoadPlacemarks(_filePath);
                var filteredPlacemarks = _kmlService.FilterPlacemarks(placemarks, filters);

                if (!filteredPlacemarks.Any())
                {
                    return NotFound("Nenhum placemark encontrado com os filtros fornecidos.");
                }

                return Ok(filteredPlacemarks);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Erro de E/S ao acessar o arquivo: {ex.Message}");
            }
        }

        [HttpGet("filters")]
        public IActionResult GetAvailableFilters()
        {
            try
            {
                var placemarks = _kmlService.LoadPlacemarks(_filePath);
                var uniqueClients = placemarks.Select(p => p.Cliente).Distinct().ToList();
                var uniqueSituations = placemarks.Select(p => p.Situacao).Distinct().ToList();
                var uniqueNeighborhoods = placemarks.Select(p => p.Bairro).Distinct().ToList();

                return Ok(new
                {
                    Clientes = uniqueClients,
                    Situacoes = uniqueSituations,
                    Bairros = uniqueNeighborhoods
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Erro de E/S ao acessar o arquivo: {ex.Message}");
            }
        }
    }
}
