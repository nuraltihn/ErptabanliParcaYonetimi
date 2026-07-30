using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Domain.Entities;

public class Roles : Baseseyler
{
    public string RolAdi { get; set; } = string.Empty; 

    public ICollection<Users> Kullanicilar { get; set; } = new List<Users>();
}