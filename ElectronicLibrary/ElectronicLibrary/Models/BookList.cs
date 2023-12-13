using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class BookList
    {
        public string Название { get; set; }
        public DateTime ГодПубликации { get; set; }
        public string КраткийСюжет { get; set; }
        public int КоличествоСтраниц { get; set; }
        public int КоличествоЭкземпляров { get; set; }
        public string ФотоОбложки { get; set; }
        public decimal? Цена { get; set; }
        public string Автор { get; set; }
        public string Жанр { get; set; }
        public string ТипЛитературы { get; set; }
        public string Издатель { get; set; }
    }
}
