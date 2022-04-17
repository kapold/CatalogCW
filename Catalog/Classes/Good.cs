using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    public class Good
    {
        // Основное
        public int ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int Count { get; set; }
        public string? Firm { get; set; }
        public string? Description { get; set; }
        public byte[] ImageData { get; set; }

        // Параметры
        public float? Display { get; set; }
        public string? DisplayType { get; set; }
        public string? Resolution { get; set; }
        public int? Hertz { get; set; }
        public string? CPU { get; set; }
        public int? RAM { get; set; }
        public int? ROM { get; set; }
        public string? Color { get; set; }
        public string? OS { get; set; }
        public int? Battery { get; set; }
        public int? Camera { get; set; }
        public bool? NFC { get; set; }
    }
}
