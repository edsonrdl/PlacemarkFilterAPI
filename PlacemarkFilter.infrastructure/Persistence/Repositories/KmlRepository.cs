using PlacemarkFilter.Application.Builder;
using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace PlacemarkFilter.Infrastructure.Persistence.Repositories
{
    public class KmlRepository : IKmlRepository
    {
        public List<Placemark> LoadPlacemarks(string filePath)
        {
            var placemarks = new List<Placemark>();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            var placemarkNodes = xmlDoc.GetElementsByTagName("Placemark");
            foreach (XmlNode node in placemarkNodes)
            {
                string nodeText = node.InnerText;

                string cliente = ExtractValue(nodeText, @"CLIENTE:\s*(.*?)\s*<br>");
                string situacao = ExtractValue(nodeText, @"SITUAÇÃO:\s*(.*?)\s*<br>");
                string bairro = ExtractValue(nodeText, @"BAIRRO:\s*(.*?)\s*<br>");
                string referencia = ExtractValue(nodeText, @"REFERENCIA:\s*(.*?)\s*<br>");
                string ruaCruzamento = ExtractValue(nodeText, @"RUA/CRUZAMENTO:\s*(.*?)\s*<br>");

                var builder = new PlacemarkBuilder();
                var placemark = builder
                    .SetCliente(cliente)
                    .SetSituacao(situacao)
                    .SetBairro(bairro)
                    .SetReferencia(referencia)
                    .SetRuaCruzamento(ruaCruzamento)
                    .Build();

                placemarks.Add(placemark);
                Console.WriteLine(placemark);
            }

            return placemarks;
        }

        private string ExtractValue(string input, string pattern)
        {
            var match = Regex.Match(input, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
