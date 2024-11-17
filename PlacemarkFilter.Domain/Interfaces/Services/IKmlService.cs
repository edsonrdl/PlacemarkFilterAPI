using PlacemarkFilter.Domain.Entities;
using System.Collections.Generic;

namespace PlacemarkFilter.Domain.Interfaces.Services
{
    public interface IKmlService
    {
        List<Placemark> LoadPlacemarks(string filePath);
        List<Placemark> FilterPlacemarks(List<Placemark> placemarks, Dictionary<string, string> filters);
        byte[] ExportFilteredPlacemarks(List<Placemark> placemarks); // Certifique-se que este método está corretamente declarado.
    }
}
