using PlacemarkFilter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Domain.Interfaces.UseCases
{
    public interface IKmlRepository
    {
        List<Placemark> LoadPlacemarks(string filePath);
    }
}
