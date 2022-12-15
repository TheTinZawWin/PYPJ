using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblRoleMenuPermission
{
    public Guid Id { get; set; }

    public Guid? MenuId { get; set; }

    public Guid? UserRoleId { get; set; }
}
