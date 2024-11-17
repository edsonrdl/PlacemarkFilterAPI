using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace PlacemarkFilter.Application.FilterStrategy
{
    public class ReferenciaFilterStrategy : IFilterStrategy
    {
        public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
        {
            if (filterValue.Length < 3)
                return placemarks;

            return placemarks.Where(p => p.Referencia?.Contains(filterValue) == true).ToList();
        }
    }
}
