using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

namespace Common.Controls.Emr
{
    public class DesignPanelWrapper : BaseObjectPropertyWrapper<IDesignerPanel>
    {
        IDesignerPanel p;
        public DesignPanelWrapper(IDesignerPanel designPanel, IDesignerHost host) : base(host)
        {
            p = designPanel;
            this.WrappedObject = designPanel;
        }

        public int 宽度
        {
            get
            {
                return this.p.Width;
            }
            set
            {
                OnPropertyChanging("Width");

                object oldValue = this.p.Width;
                this.p.Width = value;
                OnPropertyChanged("Width", oldValue, value);
            }
        }

        public int 高度
        {
            get
            {
                return this.p.Height;
            }
            set
            {
                OnPropertyChanging("Height");

                object oldValue = this.p.Height;
                this.p.Height = value;
                OnPropertyChanged("Height", oldValue, value);
            }
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
