using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class Anonimkitaplar
{
    public int Id { get; set; }

    public string? Ad { get; set; }

    public int? Yazarno { get; set; }

    public int? Sayi { get; set; }
}
