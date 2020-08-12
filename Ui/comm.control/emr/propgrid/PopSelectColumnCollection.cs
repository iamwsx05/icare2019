using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    [ListBindable(false)]
    public class PopSelectColumnCollection : CollectionBase, IList, ICollection, IEnumerable
    {
        public event CollectionChangeEventHandler CollectionChanged;
        protected void OnCollectionChange(CollectionChangeAction action, object element)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new CollectionChangeEventArgs(action, element));
            }
        }


        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            OnCollectionChange(CollectionChangeAction.Add, value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            OnCollectionChange(CollectionChangeAction.Remove, value);
        }

        public virtual PopupSelectColumn this[int index]
        {
            get
            {
                if (this.List.Count > index - 1)
                {
                    object obj = this.InnerList[index];
                    if (obj != null)
                    {
                        return (PopupSelectColumn)obj;
                    }
                }
                return null;
            }
        }

        public virtual void Add(PopupSelectColumn col)
        {
            this.InnerList.Add(col);
        }

        public virtual void Remove(PopupSelectColumn col)
        {
            foreach (object item in this.InnerList)
            {
                if ((PopupSelectColumn)item == col)
                {
                    this.InnerList.Remove(item);
                    break;
                }
            }
        }
    }
}
