using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.Entities;

internal class ETableHistory
{

    public DateTime AutoUpdatedAt { get; set; } = DateTime.MaxValue;

    public DateTime AutoCreatedAt { get; set; } = DateTime.MaxValue;

    public DateTime AutoEffectiveStartDate { get; set; } = DateTime.MaxValue;

    public DateTime AutoEffectiveEndDate { get; set; } = DateTime.MaxValue;

}
