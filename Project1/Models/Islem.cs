using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class Islem
{
    public int Islemno { get; set; }

    public int? Ogrno { get; set; }

    public int? Kitapno { get; set; }

    public DateTime? Atarih { get; set; }

    public DateTime? Vtarih { get; set; }

    public virtual Kitap? KitapnoNavigation { get; set; }

    public virtual Ogrenci? OgrnoNavigation { get; set; }
}
