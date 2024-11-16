using PlacemarkFilter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacemarkFilter.Application.Builder
{
    public class PlacemarkBuilder
    {
        private readonly Placemark _placemark = new Placemark();

            public PlacemarkBuilder SetCliente(string cliente)
            {
                if (!string.IsNullOrWhiteSpace(cliente))
                {
                    _placemark.Cliente = cliente;
                }
                return this;
            }

            public PlacemarkBuilder SetSituacao(string situacao)
            {
                if (!string.IsNullOrWhiteSpace(situacao))
                {
                    _placemark.Situacao = situacao;
                }
                return this;
            }

            public PlacemarkBuilder SetBairro(string bairro)
            {
                if (!string.IsNullOrWhiteSpace(bairro))
                {
                    _placemark.Bairro = bairro;
                }
                return this;
            }

            public PlacemarkBuilder SetReferencia(string referencia)
            {
                if (!string.IsNullOrWhiteSpace(referencia))
                {
                    _placemark.Referencia = referencia;
                }
                return this;
            }

            public PlacemarkBuilder SetRuaCruzamento(string ruaCruzamento)
            {
                if (!string.IsNullOrWhiteSpace(ruaCruzamento))
                {
                    _placemark.RuaCruzamento = ruaCruzamento;
                }
                return this;
            }

            public Placemark Build()
            {
                return _placemark;
            }
        }

    }

