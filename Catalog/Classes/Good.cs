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
        public double? Price { get; set; }
        public int Count { get; set; }
        public string? Firm { get; set; }
        public string? Description { get; set; }
        public string? ImageSrc { get; set; }
        public string? Type { get; set; }

        // Параметры
        public double? Display { get; set; }
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
        public Good GoodLink { get; set; }


        public override string ToString()
        {
            return $"\t\t\tGood\n\n" +
                $"ID: {ID}\n" +
                $"Name: {Name}\n" +
                $"Price: {Price}\n" +
                $"Count: {Count}\n" +
                $"Firm: {Firm}\n" +
                $"Description: {Description}\n" +
                $"ImageSrc: {ImageSrc}\n" +
                $"Type: {Type}\n\n" +
                $"Display: {Display}\n" +
                $"Display Type: {DisplayType}\n" +
                $"Resolution: {Resolution}\n" +
                $"Hertz: {Hertz}\n" +
                $"CPU: {CPU}\n" +
                $"RAM: {RAM}\n" +
                $"ROM: {ROM}\n" +
                $"Color: {Color}\n" +
                $"OS: {OS}\n" +
                $"Battery: {Battery}\n" +
                $"Camera: {Camera}\n" +
                $"NFC: {NFC}";
        }

        public string FullDecsription()
        {
            StringBuilder sb = new StringBuilder();
            if (Firm != null)
                sb.Append($"Firm: {Firm}\n");
            if(Display != null)
                sb.Append($"Display: {Display}\n");
            if (DisplayType != null)
                sb.Append($"Display Type: {DisplayType}\n");
            if (Resolution != null)
                sb.Append($"Resolution: {Resolution}\n");
            if (Hertz != null)
                sb.Append($"Hertz: {Hertz}\n");
            if (CPU != null)
                sb.Append($"CPU: {CPU}\n");
            if (RAM != null)
                sb.Append($"RAM: {RAM}\n");
            if (ROM != null)
                sb.Append($"ROM: {ROM}\n");
            if (Color != null)
                sb.Append($"Color: {Color}\n");
            if (OS != null)
                sb.Append($"OS: {OS}\n");
            if (Battery != null)
                sb.Append($"Battery: {Battery}\n");
            if (Camera != null)
                sb.Append($"Camera: {Camera}\n");
            if (NFC != null)
                sb.Append($"NFC: {NFC}\n");

            return sb.ToString();
        }
    }
}
