﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface IXtraRadioGroup
    {
        int Columns { get; set; }
        RadioGroupColumnItemCollection Items { get; }
    }
}
