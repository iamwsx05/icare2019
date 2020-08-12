using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Controls.Emr.Misc
{
    public class PopupSelectColumn4Designer : PopupSelectColumn
    {
        [Browsable(false)]
        public override int ColumnWidth
        {
            get
            {
                return base.ColumnWidth;
            }
            set
            {
                base.ColumnWidth = value;
            }
        }

        [Browsable(false)]
        public override string FieldName
        {
            get
            {
                return base.FieldName;
            }
            set
            {
                base.FieldName = value;
            }
        }

        [Browsable(false)]
        public override string HeaderCaption
        {
            get
            {
                return base.HeaderCaption;
            }
            set
            {
                base.HeaderCaption = value;
            }
        }

        public int 列宽
        {
            get
            {
                return this.ColumnWidth;
            }
            set
            {
                this.ColumnWidth = value;
            }
        }

        public string 列名
        {
            get
            {
                return this.FieldName;
            }
            set
            {
                this.FieldName = value;
            }
        }

        public string 列头显示名
        {
            get
            {
                return this.HeaderCaption;
            }
            set
            {
                this.HeaderCaption = value;
            }
        }
    }
}
