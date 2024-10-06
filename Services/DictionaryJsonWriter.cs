using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using app.Interfaces;
using app.Models;

namespace app.Services {
    public class DictionaryJsonWriter<T> : IDictionaryJsonWriter<T> where T : DictionaryBaseType {
        
        public bool WriteToJson(List<T> dictionaries, string outputPath) {
            try {
                
                var options = new JsonSerializerOptions {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Для избежания меток BOM
                };

                string jsonString = JsonSerializer.Serialize(dictionaries, options); // Сериализуем входную ифнормацию в строку

                File.WriteAllText(outputPath, jsonString, new UTF8Encoding(true)); // Записываем в файл с кодировкой utf-8
                
                return true;
            }
            
            catch (Exception ex) {
                
                Console.WriteLine("Ошибка при записи в JSON файл: " + ex.Message);
                
                return false;
            }

        }
    }
}
