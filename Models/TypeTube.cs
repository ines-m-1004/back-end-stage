using System;
using System.Collections.Generic;

namespace stage.Models;

public partial class TypeTube
{
    public int CodeType { get; set; }

    public string LiblletType { get; set; } = null!;

    public byte[]? Couleur { get; set; }

    public string NomImage { get; set; } = null!;

    public string? UserCreate { get; set; }

    public DateTime? DateCreate { get; set; }
}