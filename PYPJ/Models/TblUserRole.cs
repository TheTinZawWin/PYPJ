using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblUserRole
{
    public Guid Id { get; set; }

    public Guid? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public bool? IsActive { get; set; }
}
