using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlacemarkFilter.Application.Services
{
    public class KmlService : IKmlService
    {
        private readonly IKmlRepository _kmlRepository;

        public KmlService(IKmlRepository kmlRepository)
        {
            _kmlRepository = kmlRepository;
        }

        public List<Placemark> LoadPlacemarks(string filePath)
        {
            return _kmlRepository.LoadPlacemarks(filePath);
        }

        public List<Placemark> FilterPlacemarks(List<Placemark> placemarks, Dictionary<string, string> filters)
        {
            if (filters == null || !filters.Any())
            {
                return placemarks;
            }

            foreach (var filter in filters)
            {
                switch (filter.Key.ToUpperInvariant())
                {
                    case "CLIENTE":
                        placemarks = placemarks.Where(p => p.Cliente == filter.Value).ToList();
                        break;
                    case "SITUACAO":
                        placemarks = placemarks.Where(p => p.Situacao == filter.Value).ToList();
                        break;
                    case "BAIRRO":
                        placemarks = placemarks.Where(p => p.Bairro == filter.Value).ToList();
                        break;
                    case "REFERENCIA":
                        if (filter.Value.Length >= 3)
                            placemarks = placemarks.Where(p => p.Referencia?.Contains(filter.Value) == true).ToList();
                        break;
                    case "RUA/CRUZAMENTO":
                        if (filter.Value.Length >= 3)
                            placemarks = placemarks.Where(p => p.RuaCruzamento?.Contains(filter.Value) == true).ToList();
                        break;
                }
            }

            return placemarks;
        }

        public byte[] ExportFilteredPlacemarks(List<Placemark> placemarks)
        {
            // Implementação básica de geração de conteúdo KML.
            StringBuilder kmlContent = new StringBuilder();
            kmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            kmlContent.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\">");
            kmlContent.AppendLine("<Document>");

            foreach (var placemark in placemarks)
            {
                kmlContent.AppendLine("<Placemark>");
                kmlContent.AppendLine($"<name>{placemark.Cliente}</name>");
                kmlContent.AppendLine($"<description>{placemark.Situacao}</description>");
                // Adicione outros elementos conforme necessário
                kmlContent.AppendLine("</Placemark>");
            }

            kmlContent.AppendLine("</Document>");
            kmlContent.AppendLine("</kml>");

            return Encoding.UTF8.GetBytes(kmlContent.ToString());
        }
    }
}
