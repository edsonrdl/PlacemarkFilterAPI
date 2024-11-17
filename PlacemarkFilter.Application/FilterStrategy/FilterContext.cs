using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.Services;

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
