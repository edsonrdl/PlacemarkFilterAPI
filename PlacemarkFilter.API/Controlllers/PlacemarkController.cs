using Microsoft.AspNetCore.Mvc;
using PlacemarkFilter.Domain.Interfaces.Services;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

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
            string basePath = Directory.GetCurrentDirectory();
            string relativePath = configuration.GetValue<string>("KmlFilePath");
            _filePath = Path.Combine(basePath, relativePath);

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

                var exportedKml = _kmlService.ExportFilteredPlacemarks(filteredPlacemarks);
                return File(exportedKml, "application/vnd.google-earth.kml+xml", "filteredPlacemarks.kml");
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
                var uniqueClients = placemarks.Select(p => p.Cliente?.Trim().ToUpperInvariant()).Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
                var uniqueSituations = placemarks.Select(p => p.Situacao?.Trim().ToUpperInvariant()).Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
                var uniqueNeighborhoods = placemarks.Select(p => p.Bairro?.Trim().ToUpperInvariant()).Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();

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
