using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("SoldProducts")]
    public class SoldProductsCollectionDto
    {
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("products")]
        public List<SoldProductsDto> Products { get; set; }
    }
}
