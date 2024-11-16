using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Application.FilterStrategy
{
    public class FilterContext
    {
        private readonly IFilterStrategy _strategy;

        public FilterContext(IFilterStrategy strategy)
        {
            _strategy = strategy;
        }

        public List<Placemark> ApplyFilter(List<Placemark> placemarks, string filterValue)
        {
            return _strategy.ApplyFilter(placemarks, filterValue);
        }
    }
}
