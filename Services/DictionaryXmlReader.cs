/*
Вид входного файла
<packet>
  <zglv>
    <type>VidMp</type>
    <version>2.2</version>
    <date>06.05.2020</date>
  </zglv>
  <zap>
    <IDVMP>1</IDVMP>
    <VMPNAME>первичная медико-санитарная помощь</VMPNAME>
    <DATEBEG>01.01.2010</DATEBEG>
    <DATEEND/>
  </zap>
*/

using System.Text;
using System.Xml.Linq;
using app.Interfaces;
using app.Models;

namespace app.Services {
    public class DictionaryXmlReader<T> : IDictionaryXmlReader<T> where T : DictionaryBaseType, new() {
        public List<T> ReadFromXml(string filePath) { 
            
            // DictionaryHeader[] header; 

            List<T> entries = new List<T>();

            try {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Регистрация поставщика кодировок для windows-1251

                Encoding encoding = Encoding.GetEncoding("windows-1251"); // Ставим кодировку

                using (StreamReader reader = new StreamReader(filePath, encoding)) { // Открываем файл с указанной кодировкой
                    
                    XDocument xdoc = XDocument.Load(reader);

                    // var headerElements = xdoc.Root.Element("zglv");

                    // if (headerElements != null) {
                        
                    //     Type = zglvElement.Element("type")?.Value,
                    //     Version = zglvElement.Element("version")?.Value,
                    //     Date = DateTime.Parse(zglvElement.Element("date")?.Value ?? "01.01.0001")
                    // }

                    var entryElements = xdoc.Descendants("zap");

                    foreach (var elem in entryElements) {
                        
                        T entry = new T();

                        // Console.WriteLine($"Code: {elem.Element("IDVMP")}, Name: {elem.Element("VMPNAME")}");

                        var codeElement = elem.Element("IDVMP");
                        if (codeElement != null && int.TryParse(codeElement.Value, out int code)) {
                            entry.Code = code;
                        }

                        var beginDateElement = elem.Element("DATEBEG");
                        if (beginDateElement != null && DateTime.TryParse(beginDateElement.Value, out DateTime beginDate)) {
                            entry.BeginDate = beginDate;
                        }

                        var endDateElement = elem.Element("DATEEND");
                        if (endDateElement != null && DateTime.TryParse(endDateElement.Value, out DateTime endDate)) {
                            entry.EndDate = endDate;
                        }
                        else {
                            entry.EndDate = null; // Поставил null, если во входной xml нет значения
                        }

                        var nameElement = elem.Element("VMPNAME");
                        if (nameElement != null) {
                            entry.Name = nameElement.Value;
                        }

                        entries.Add(entry);
                    }
                }
            }
            catch (Exception ex) {
                
                Console.WriteLine($"Ошибка при чтении XML файла: {ex.Message}\n{ex.StackTrace}");
            }

            return entries;
        }
    }
}
