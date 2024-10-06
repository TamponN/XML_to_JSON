using app.Models;

namespace app.Interfaces {
    
    public interface IDictionaryJsonWriter<T> where T : DictionaryBaseType {// generic для разных типов
        
        bool WriteToJson(List<T> dictionaries, string outputPath);
    }
}
