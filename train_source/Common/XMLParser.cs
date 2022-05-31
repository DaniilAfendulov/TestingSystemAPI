using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// Класс для работы с XML файлами
    /// </summary>
    public static class XMLHelper
    {
        /// <summary>
        /// Ключ шифрования
        /// </summary>
        private const string CryptoKey = "s4G8nb5#";

        /// <summary>
        /// Записать объект в XML файл
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="_object">Объект</param>
        /// <param name="_fileName">Путь к файлу</param>
        public static void WriteXML<T>(T _object, string _fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(T));
            using (StreamWriter file = new StreamWriter(_fileName))
            {
                writer.Serialize(file, _object);
            }
        }

        /// <summary>
        /// Вычитать объект из XML файла
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="_fileName">Путь к файлу</param>
        /// <returns>Объект</returns>
        public static T ReadXML<T>(string _fileName)
        {
            XmlSerializer reader = new XmlSerializer(typeof(T));
            using (StreamReader file = new StreamReader(_fileName))
            {
                return (T)reader.Deserialize(file);
            }
        }

        /// <summary>
        /// Вычитать объект из зашифрованого XML файла
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="_fileName">Путь к файлу</param>
        /// <returns>Объект</returns>
        public static T ReadXMLEncrypt<T>(string _fileName)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(CryptoKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(CryptoKey);
            ICryptoTransform desdecrypt = DES.CreateDecryptor();

            using (FileStream fsread = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read))
                {
                    XmlSerializer reader = new XmlSerializer(typeof(T));
                    return (T)reader.Deserialize(cryptostreamDecr);
                }
            }
        }

        /// <summary>
        /// Записать объект в зашифрованый XML файл
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="_object">Объект</param>
        /// <param name="_fileName">Путь к файлу</param>
        public static void WriteXMLEncrypt<T>(T _object, string _fileName)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(CryptoKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(CryptoKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();

            using (FileStream fsEncrypted = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                {
                    XmlSerializer writer = new XmlSerializer(typeof(T));
                    writer.Serialize(cryptostream, _object);
                }
            }
        }
    }
}
