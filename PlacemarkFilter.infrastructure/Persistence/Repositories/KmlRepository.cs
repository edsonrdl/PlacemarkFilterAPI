﻿using PlacemarkFilter.Domain.Entities;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using System.Collections.Generic;
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
                var placemark = new Placemark
                {
                    Cliente = node["name"]?.InnerText,
                    Situacao = node["description"]?.InnerText,
                    Bairro = node["address"]?.InnerText 
                };
                placemarks.Add(placemark);
            }

            return placemarks;
        }
    }
}
