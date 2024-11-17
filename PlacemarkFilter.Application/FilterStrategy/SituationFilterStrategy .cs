using PlacemarkFilter.Domain.Entities;


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
