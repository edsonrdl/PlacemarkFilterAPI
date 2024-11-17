using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace PlacemarkFilter.Application.FilterStrategy
{
    public class BairroFilterStrategy : IFilterStrategy
    {
        public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
        {
            return placemarks.Where(p => p.Bairro == filterValue).ToList();
        }
    }
}
