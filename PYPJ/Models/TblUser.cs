using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblUser
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }
}
