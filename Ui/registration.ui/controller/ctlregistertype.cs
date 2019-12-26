using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;
using System.Data;

namespace Registration.Ui
{
    public class ctlRegisterType : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterType Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterType)child;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 数据源
        /// </summary>
        BindingListView<EntityCodeReg> DataSourceReg { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            this.gridView = Viewer.gvNo;
            this.gvDataBindingSource = new BindingSource();
            Viewer.gcNo.DataSource = gvDataBindingSource;

            DataSourceReg = new BindingListView<EntityCodeReg>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                DataTable dt = proxy.Service.SelectFullTable(new EntityCodeReg());
                DataSourceReg.AddRange(EntityTools.ConvertToEntityList<EntityCodeReg>(dt));
            }
            if (DataSourceReg.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    DataSourceReg.Add(new EntityCodeReg());
                }
            }
            else
            {
                foreach (EntityCodeReg item in DataSourceReg)
                {
                    if (item.status == 0) item.statusName = "停用";
                    else if (item.status == 1) item.statusName = "启用";
                    if (item.bookFlag == "2") item.bookFlagName = "不可预约";
                    else if (item.bookFlag == "1") item.bookFlagName = "可预约";
                    else item.bookFlagName = "";
                    if (item.regFlag == "3") item.regFlagName = "全部";
                    else if (item.regFlag == "2") item.regFlagName = "挂到医师";
                    else if (item.regFlag == "1") item.regFlagName = "挂到科室";
                    if (item.typeCode == "01") item.typeName = "普通";
                    else if (item.typeCode == "02") item.typeName = "急诊";
                    item.CopyOriginalValue();
                    item.CloneObject = item.Clone();
                }
            }
            this.gvDataBindingSource.DataSource = DataSourceReg;
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        public void New()
        {
            AppendRow();
        }
        #endregion

        #region Del
        /// <summary>
        /// Del
        /// </summary>
        public void Del()
        {
            DeleteCurrentRow();
        }
        #endregion

        #region ClearInvalidData
        /// <summary>
        /// 清空无效数据
        /// </summary>
        protected override void ClearInvalidData()
        {
            if (gvDataBindingSource == null || gvDataBindingSource.DataSource == null) return;
            gridView.CloseEditor();

            List<EntityCodeReg> lstNo = new List<EntityCodeReg>((gvDataBindingSource.DataSource as BindingListView<EntityCodeReg>));
            if (lstNo == null || lstNo.Count == 0) return;
            for (int i = lstNo.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(lstNo[i].regCode) && string.IsNullOrEmpty(lstNo[i].regName))
                {
                    DeleteRow(i);
                }
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            Viewer.IsSave = false;
            ClearInvalidData();
            if (!CheckNotNullColumn()) return;

            try
            {
                Viewer.Cursor = Cursors.WaitCursor;
                EntityDML<EntityCodeReg> voDML = Verify<EntityCodeReg>();
                if (voDML.IsAdd || voDML.IsDelete || voDML.IsUpdate)
                {
                    int intRet = 0;
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        intRet = proxy.Service.SaveCodeReg(voDML.AddSource, voDML.DeleteSource, voDML.UpdateSource, voDML.UpdatePreSource);
                    }
                    if (intRet > 0)
                    {
                        Viewer.ValueChanged = false;
                        DialogBox.Msg("数据保存成功！");
                    }
                    else
                    {
                        DialogBox.Msg("数据保存失败。");
                    }
                }
                else
                {
                    DialogBox.Msg("数据无变化。");
                }
            }
            finally
            {
                Viewer.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        public void Print()
        {
            Viewer.gvNo.Print();
        }
        #endregion

        #endregion

    }
}
