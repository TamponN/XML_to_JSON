using app.Models;

namespace app.Interfaces {
    
    public interface IDictionaryXmlReader<T> where T : DictionaryBaseType { // generic для разных типов
        
        List<T> ReadFromXml(string filePath);
    }
}
