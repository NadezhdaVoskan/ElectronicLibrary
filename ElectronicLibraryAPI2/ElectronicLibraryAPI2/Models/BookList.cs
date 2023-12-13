using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class BookList
    {
        public string Название { get; set; } = null!;
        public DateTime ГодПубликации { get; set; }
        public string КраткийСюжет { get; set; } = null!;
        public int КоличествоСтраниц { get; set; }
        public string ФотоОбложки { get; set; } = null!;
        public decimal? Цена { get; set; }
        public string? Автор { get; set; }
        public string Жанр { get; set; } = null!;
        public string ТипЛитературы { get; set; } = null!;
        public string Издатель { get; set; } = null!;
    }
}
