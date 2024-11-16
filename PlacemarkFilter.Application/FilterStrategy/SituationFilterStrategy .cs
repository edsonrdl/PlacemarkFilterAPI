using PlacemarkFilter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Application.FilterStrategy
{
    public class SituationFilterStrategy
    {
        public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
        {
            return placemarks.Where(p => p.Situacao == filterValue).ToList();
        }
    }
}
