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
        private readonly Dictionary<string, IFilterStrategy> _filterStrategies;

        public KmlService(IKmlRepository kmlRepository, Dictionary<string, IFilterStrategy> filterStrategies)
        {
            _kmlRepository = kmlRepository;
            _filterStrategies = filterStrategies;
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

                if (_filterStrategies.TryGetValue(filter.Key.ToUpperInvariant(), out var strategy))
                {
                    placemarks = strategy.ApplyFilter(placemarks, filter.Value);
                }
                else
                {
                    throw new ApplicationException($"Filtro não reconhecido: {filter.Key}");
                }
            }

            return placemarks;
        }

        public byte[] ExportFilteredPlacemarks(List<Placemark> placemarks)
        {
            StringBuilder kmlContent = new StringBuilder();
            kmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            kmlContent.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\">");
            kmlContent.AppendLine("<Document>");

            foreach (var placemark in placemarks)
            {
                kmlContent.AppendLine("<Placemark>");
                kmlContent.AppendLine($"<name>{placemark.Cliente}</name>");
                kmlContent.AppendLine($"<description>{placemark.Situacao}</description>");
                kmlContent.AppendLine("</Placemark>");
            }

            kmlContent.AppendLine("</Document>");
            kmlContent.AppendLine("</kml>");

            return Encoding.UTF8.GetBytes(kmlContent.ToString());
        }
    }
}
