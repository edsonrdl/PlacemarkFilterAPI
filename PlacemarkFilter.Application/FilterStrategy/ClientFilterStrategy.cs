using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Application.FilterStrategy
{
    public class ClientFilterStrategy : IFilterStrategy
    {
        public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
        {
            return placemarks.Where(p => p.Cliente == filterValue).ToList();
        }
    }
}
