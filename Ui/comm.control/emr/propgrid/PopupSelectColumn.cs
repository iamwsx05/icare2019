using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    public class PopupSelectColumn
    {
        private string _fieldname = string.Empty;

        [Description("列名")]
        public virtual string FieldName
        {
            get
            {
                return _fieldname;
            }
            set
            {
                _fieldname = value;
                if (HeaderCaption == null)
                {
                    HeaderCaption = _fieldname;
                }
            }
        }

        [Description("列头显示名")]
        public virtual string HeaderCaption { get; set; }

        [Description("列宽")]
        public virtual int ColumnWidth { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.HeaderCaption, this.FieldName);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        public PopupSelectColumn()
        {
            //this.PropertyName = string.Empty;
            //this._displaytext = string.Empty;
            this.ColumnWidth = 100;
        }
    }
}
