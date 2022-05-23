using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catalog.Classes
{
    public static class XmlSerializator
    {
        public static string savedUserPath = 
            Path.GetFullPath(@"D:\Универ 2 курс\Университет 4 семестр\Курсовая\Catalog\Catalog\savedLastUser.xml");
        public static void SerializeUser(User user)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));

            using (StreamWriter sw = new StreamWriter(savedUserPath, false, System.Text.Encoding.Default))
            {
                serializer.Serialize(sw, user);
            }
        }

        public static User DeserializeUser()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));

            User? user = null;
            using (StreamReader sr = new StreamReader(savedUserPath))
            {
                user = serializer.Deserialize(sr) as User;
            }

            return user;
        }
    }
}
