using PlacemarkFilter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Domain.Interfaces.Services
{
    public interface IKmlService
    {

        List<Placemark> LoadPlacemarks(string filePath);
        List<Placemark> FilterPlacemarks(List<Placemark> placemarks, Dictionary<string, string> filters);
    }
}
