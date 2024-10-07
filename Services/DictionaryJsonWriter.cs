using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using app.Interfaces;
using app.Models;

namespace app.Services {
    public class DictionaryJsonWriter<T> : IDictionaryJsonWriter<T> where T : DictionaryBaseType {
        
        public bool WriteToJson(Dictionary<string, List<T>> data, string outputPath) {
            try {
                
                var options = new JsonSerializerOptions {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Для минимального экранирования символов
                };

                string jsonString = JsonSerializer.Serialize(data, options); // Сериализуем входную ифнормацию в строку

                File.WriteAllText(outputPath, jsonString, new UTF8Encoding(false)); // Записываем в файл с кодировкой utf-8
                
                return true;
            }
            
            catch (Exception ex) {
                
                Console.WriteLine("Ошибка при записи в JSON файл: " + ex.Message);
                
                return false;
            }

        }
    }
}
