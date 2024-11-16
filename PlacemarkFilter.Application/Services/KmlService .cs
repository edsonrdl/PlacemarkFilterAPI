using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

            // Aplicação básica de filtros conforme os requisitos
            foreach (var filter in filters)
            {
                if (filter.Key == "CLIENTE")
                {
                    placemarks = placemarks.Where(p => p.Cliente == filter.Value).ToList();
                }
                else if (filter.Key == "SITUACAO")
                {
                    placemarks = placemarks.Where(p => p.Situacao == filter.Value).ToList();
                }
                else if (filter.Key == "BAIRRO")
                {
                    placemarks = placemarks.Where(p => p.Bairro == filter.Value).ToList();
                }
                else if ((filter.Key == "REFERENCIA" || filter.Key == "RUA/CRUZAMENTO") && filter.Value.Length >= 3)
                {
                    placemarks = placemarks.Where(p => (filter.Key == "REFERENCIA" && p.Referencia?.Contains(filter.Value) == true) ||
                                                       (filter.Key == "RUA/CRUZAMENTO" && p.RuaCruzamento?.Contains(filter.Value) == true)).ToList();
                }
            }

            return placemarks;
        }
    }
}
