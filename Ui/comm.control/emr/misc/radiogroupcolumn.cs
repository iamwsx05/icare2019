using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Common.Controls.Emr
{
    public class RadioGroupColumn
    {
        public string 显示文字 { get; set; }
        public string 选择值 { get; set; }
        public decimal 计算值 { get; set; }

        [Browsable(false)]
        public int hashcode { get; set; }

        public RadioGroupColumn()
        {
            this.显示文字 = string.Empty;
            this.选择值 = string.Empty;
            //this.计算值 = null;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.显示文字, this.选择值);
        }
    }

    public class RadioGroupColumnItemCollection : CollectionBase
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

        public RadioGroupColumn this[int index]
        {
            get
            {
                return (RadioGroupColumn)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        public virtual void Add(RadioGroupColumn item)
        {
            this.List.Add(item);
            OnCollectionChange(CollectionChangeAction.Add, item);
        }

        public virtual void Remove(RadioGroupColumn item)
        {
            this.List.Remove(item);
            OnCollectionChange(CollectionChangeAction.Remove, item);
        }
    }
}
