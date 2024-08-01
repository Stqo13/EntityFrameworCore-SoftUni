﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Car")]
    public class CarImportDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("traveledDistance")]
        public long TraveledDistance { get; set; }
        [XmlArray("parts")]
        public PartsCarsImportDto[] PartIds { get; set; }
    }

    [XmlType("partId")]
    public class PartsCarsImportDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
