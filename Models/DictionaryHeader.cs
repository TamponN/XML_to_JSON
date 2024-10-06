using System.ComponentModel.DataAnnotations;

namespace app.Models {
    public class DictionaryHeader {

        [Display(Name = "Тип")]
        public string Type { get; set; } = String.Empty;
        
        [Display(Name = "Версия")]
        public string Version { get; set; } = String.Empty;

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        
        public DictionaryHeader() {}
    }

}
