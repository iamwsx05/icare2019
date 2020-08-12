using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using Common.Entity;

namespace Common.Controls.Emr
{
    public partial class ctlTreeSelectICD : ctlTreeSelect
    {
        public ctlTreeSelectICD()
        {
            InitializeComponent();

            LoadData();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            LoadData();
        }

        /// <summary>
        /// 原因: 自定义单只在InitializeComponent后调用有效,非自定义单只在OnLoaded后调用后有效,所以2个地方都调
        /// </summary>
        private void LoadData()
        {
            //字典信息处理
            if (!DesignMode && this.DataSource == null)
            {
                this.Properties.PopupFormMinSize = new Size(450, 350);
                this.KeyFieldName = "IcdCode";
                this.ParentFieldName = "ParentCode";
                this.DisplayMember = "IcdName";
                this.ValueMember = "IcdCode";
                PopTreeColumnCollection cols = new PopTreeColumnCollection();
                cols.Add(new PopupTreeColumn("IcdCode", "ICD码", 200));
                cols.Add(new PopupTreeColumn("IcdName", "中文名称", 250));
                cols.Add(new PopupTreeColumn("PyCode", "拼音码", 0));
                cols.Add(new PopupTreeColumn("WbCode", "五笔码", 0));
                this.Columns = cols;
                this.DataSource = EntityTools.ConvertToDataTable<EntityIcd>(GlobalDic.DataSourceICD);
            }
        }
    }
}
