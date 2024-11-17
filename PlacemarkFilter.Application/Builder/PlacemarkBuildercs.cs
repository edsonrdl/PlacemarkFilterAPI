using PlacemarkFilter.Domain.Entities;

namespace PlacemarkFilter.Application.Builder
{
    public class PlacemarkBuilder
    {
        private readonly Placemark _placemark = new Placemark();

        public PlacemarkBuilder SetCliente(string cliente)
        {
            _placemark.Cliente = string.IsNullOrWhiteSpace(cliente) ? null : cliente.Trim();
            return this;
        }

        public PlacemarkBuilder SetSituacao(string situacao)
        {
            _placemark.Situacao = string.IsNullOrWhiteSpace(situacao) ? null : situacao.Trim();
            return this;
        }

        public PlacemarkBuilder SetBairro(string bairro)
        {
            _placemark.Bairro = string.IsNullOrWhiteSpace(bairro) ? null : bairro.Trim();
            return this;
        }

        public PlacemarkBuilder SetReferencia(string referencia)
        {
            _placemark.Referencia = string.IsNullOrWhiteSpace(referencia) ? null : referencia.Trim();
            return this;
        }

        public PlacemarkBuilder SetRuaCruzamento(string ruaCruzamento)
        {
            _placemark.RuaCruzamento = string.IsNullOrWhiteSpace(ruaCruzamento) ? null : ruaCruzamento.Trim();
            return this;
        }

        public Placemark Build()
        {
            return _placemark;
        }
    }
}
