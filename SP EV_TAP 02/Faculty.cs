﻿using System;
using System.Collections.Generic;

namespace SP_EV_TAP_02;

public partial class Faculty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
