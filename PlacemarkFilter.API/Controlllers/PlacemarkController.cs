using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlacemarkFilter.Domain.Interfaces.Services;
using System.Reflection.Metadata;

namespace PlacemarkFilter.API.Controlllers
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
            _filePath = configuration["KmlFilePath"];
        }

        [HttpPost("export")]
        public IActionResult ExportFilteredKml([FromBody] Dictionary<string, string> filters)
        {
            try
            {
                var placemarks = _kmlService.LoadPlacemarks(_filePath);
                var filteredPlacemarks = _kmlService.FilterPlacemarks(placemarks, filters);

                // Lógica para exportar o novo KML
                return Ok(filteredPlacemarks);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ListFilteredPlacemarks([FromQuery] Dictionary<string, string> filters)
        {
            try
            {
                var placemarks = _kmlService.LoadPlacemarks(_filePath);
                var filteredPlacemarks = _kmlService.FilterPlacemarks(placemarks, filters);
                return Ok(filteredPlacemarks);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
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
        }
    }
}