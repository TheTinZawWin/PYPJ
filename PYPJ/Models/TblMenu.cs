using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblMenu
{
    public Guid Id { get; set; }

    public string MenuName { get; set; } = null!;

    public Guid ProgramId { get; set; }

    public int MenuLevel { get; set; }

    public Guid? ParentMenuId { get; set; }

    public int MenuIndex { get; set; }

    public bool IsShow { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public bool IsActive { get; set; }
}
