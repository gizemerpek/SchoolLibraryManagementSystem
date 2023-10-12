﻿using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class Ogrenci
{
    public int Ogrno { get; set; }

    public string? Ad { get; set; }

    public string? Soyad { get; set; }

    public DateTime? Dtarih { get; set; }

    public string? Cinsiyet { get; set; }

    public string Sinif { get; set; } = null!;

    public int? Puan { get; set; }

    public virtual ICollection<Islem> Islems { get; set; } = new List<Islem>();
}
