using PlacemarkFilter.Domain.Entities;
using System.Collections.Generic;

namespace PlacemarkFilter.Domain.Interfaces.UseCases
{
    public interface IKmlRepository
    {
        List<Placemark> LoadPlacemarks(string filePath);
    }
}
