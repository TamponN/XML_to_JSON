using app.Models;

namespace app.Interfaces {
    
    public interface IDictionaryJsonWriter<T> where T : DictionaryBaseType {// generic для разных типов
        
        bool WriteToJson(Dictionary<string, List<T>> data, string outputPath);
    }
}
