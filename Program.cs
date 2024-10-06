/*
Создать консольное приложение C#. Необходимо реализовать следующую логику работы программы: на вход поступает путь до XML файла справочника. Необходимо прочитать файл и заполнить поля соответствующего класса, а далее записать массив заполненных данных в файл, данные записываются в формате JSON. 

Класс для заполнения данных справочника
(название осмысленное!) должен быть наследником от DictionaryBaseType :

public class DictionaryBaseType
    {
        [Display(Name = "Код")]
        public int Code { get; set; }
        
        [Display(Name = "Начало")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Окончание")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Наименование")]
        public string Name { get; set; } = string.Empty;

        public DictionaryBaseType()
        {
        }

    }
Для чтения и парсинга XML файла использовать XDocument из пространства имен System.Xml.Linq.
Для чтения\записи в файла и преобразования в JSON использовать стандартную библиотеку. 

*Логика должна быть вынесена в отдельные классы и быть наследником от соответствующего интерфейса: 
interface DictionaryXMlReader
{
     List<DictionaryBaseType> ReadFromXml(string filePath);
}

interface  DictionaryJsonWriter
{
    bool WriteToJson(DictionaryBaseType dictionary, string outputPath);
}

*Кто знает про Generic классы, то для универсальности можно использовать их.
Создание дополнительных классов, полей, методов не запрещено.

Касательно остальных деталей реализации выполняется по личному усмотрению. 
*/

using app.Models;
using app.Services;
using app.Interfaces;

class Program {
    
    static void Main(string[] args) {
        
        string? xmlFilePath = "Files/";

        if (args.Length > 0) {
            
            xmlFilePath = args[0];
        }
        
        else {
            
            Console.Write("Введите название XML файла: ");
            string? fileName = Console.ReadLine();
            xmlFilePath += fileName; // Думал, что буду использовать, а убирать лень
        }

        if (!File.Exists(xmlFilePath)) {
            
            Console.WriteLine("Указанный XML файл не найден.");

            return;
        }

        IDictionaryXmlReader<DictionaryVar> reader = new DictionaryXmlReader<DictionaryVar>();
        List<DictionaryVar> entries = reader.ReadFromXml(xmlFilePath);

        Console.WriteLine($"Прочитано {entries.Count} записей из XML файла.");

        string jsonFilePath = Path.ChangeExtension(xmlFilePath, ".json");

        // Пихнул в словарь для удобства
        var entriesEl = new Dictionary<string, List<DictionaryVar>>() {
            { "Entries", entries }
        };

        IDictionaryJsonWriter<DictionaryVar> writer = new DictionaryJsonWriter<DictionaryVar>();
        bool success = writer.WriteToJson(entriesEl, jsonFilePath);

        if (success) {
            
            Console.WriteLine($"Данные успешно записаны в {jsonFilePath}");
        }   
        
        else {
            
            Console.WriteLine("Не удалось записать данные в JSON файл.");
        }
    }
}
