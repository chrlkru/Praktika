using System;
using System.Collections.Generic;

namespace Praktika;

public partial class DiscontCard
{
    public int DiscontCardId { get; set; }

   

    public int UsersId { get; set; }

    public double Discont { get; set; }

    public double OrderSum { get; set; }



    public virtual User Users { get; set; } = null!;
}
