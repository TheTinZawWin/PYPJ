using System;
using System.Collections.Generic;

namespace PYPJ.Models;

public partial class TblRefreshToken
{
    public Guid UserId { get; set; }

    public string TokenId { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public bool IsActive { get; set; }
}
