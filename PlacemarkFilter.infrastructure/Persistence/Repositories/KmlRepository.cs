using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.infrastructure.Persistence.Repositories
{
    public class KmlRepository: IKmlRepository
    {
        public List<Placemark> LoadPlacemarks(string filePath)
        {
            var placemarks = new List<Placemark>();

            try
            {
                using var stream = File.OpenRead(filePath);
                var parser = KmlFile.Load(stream);

                if (parser.Root is Kml kml && kml.Feature is Document document)
                {
                    foreach (var feature in document.Features)
                    {
                        if (feature is SharpKml.Dom.Placemark placemark)
                        {
                            var placemarkData = new Placemark
                            {
                                Cliente = placemark.Name,
                                Situacao = placemark.Description?.Text
                            };

                            placemarks.Add(placemarkData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao carregar o arquivo KML: {ex.Message}");
            }

            return placemarks;
        }
    }
}
