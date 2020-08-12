using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public class ColumnCollection : List<DrawingDataColumn>
    {
        public new void Add(DrawingDataColumn item)
        {
            base.Add(item);
        }
    }
}
