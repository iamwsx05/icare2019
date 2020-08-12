using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class ucDoct : UserControl
    {
        public ucDoct()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public EntityCodeOperator DoctVo { get; set; }

        private void ucDoct_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.lueDoct.Properties.PopupWidth = 200;
            this.lueDoct.Properties.PopupHeight = 400;
            this.lueDoct.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueDoct.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueDoct.Properties.Essential = false;
            this.lueDoct.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueDoct.Properties.IsUseShowColumn = true;
            this.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 140);
            this.lueDoct.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (GlobalDic.DataSourceDoctor != null) this.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            this.lueDoct.Properties.SetSize();    
        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            if (this.lueDoct.Properties.DBRow != null)
            {
                DoctVo = this.lueDoct.Properties.DBRow as EntityCodeOperator;
            }
            else
            {
                DoctVo = null;
            }
        }


    }
}
