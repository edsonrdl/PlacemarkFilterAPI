using PlacemarkFilter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Domain.Interfaces.Services
{
    public interface IFilterStrategy
    {
        List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue);
    }
}
