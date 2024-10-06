using System.ComponentModel.DataAnnotations;

namespace app.Models {
    public class DictionaryBaseType {

        [Display(Name = "Код")]
        public int Code { get; set; }
        
        [Display(Name = "Начало")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Окончание")]
        public DateTime? EndDate { get; set; } // Поставил сюда неопределенное значение, т.к. есть пустые даты в xml
        
        [Display(Name = "Наименование")]
        public string Name { get; set; } = string.Empty;

        public DictionaryBaseType() {}
    }

}
