using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    [ListBindable(false)]
    public class PopupSelectColumn4Designer : CollectionBase, IList, ICollection, IEnumerable
    {
        public virtual PopupSelectColumn4Designer this[int index]
        {
            get
            {
                if (this.List.Count > index - 1)
                {
                    object obj = this.InnerList[index];
                    if (obj != null)
                    {
                        return (PopupSelectColumn4Designer)obj;
                    }
                }
                return null;
            }
        }

        public virtual void Add(PopupSelectColumn4Designer col)
        {
            this.InnerList.Add(col);
        }

        public virtual void Remove(PopupSelectColumn4Designer col)
        {
            foreach (object item in this.InnerList)
            {
                if ((PopupSelectColumn4Designer)item == col)
                {
                    this.InnerList.Remove(item);
                    break;
                }
            }
        }
    }
}
