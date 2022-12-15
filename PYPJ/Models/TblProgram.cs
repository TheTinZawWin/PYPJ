using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblProgram
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ProgramUrl { get; set; } = null!;

    public bool IsParentProgram { get; set; }

    public Guid ParentProgramId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public bool IsActive { get; set; }
}
