using Common.Controls;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;

namespace Registration.Ui
{
    /// <summary>
    /// ctlRegisterStat1
    /// </summary>
    public class ctlRegisterStat1 : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterStat1 Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterStat1)child;
        }
        #endregion

        #region 变量

        List<EntityCodeDepartment> DataSourceOpDept { get; set; }

        List<EntityCodeOperator> DataSourceOpDoct { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region dic
            DataTable dt = null;
            List<EntityCodeReg> DataSourceCodeReg = new List<EntityCodeReg>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                dt = proxy.Service.SelectFullTable(new EntityCodeReg());
                DataSourceCodeReg.AddRange(EntityTools.ConvertToEntityList<EntityCodeReg>(dt));
                if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
                {
                    for (int i = DataSourceCodeReg.Count - 1; i >= 0; i--)
                    {
                        if (DataSourceCodeReg[i].status == 0) DataSourceCodeReg.RemoveAt(i);
                    }
                }
            }

            List<EntityCodeDepartment> DgPlatformDept = null;
            List<EntityCodeOperator> DgPlatformDoct = null;
            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    DgPlatformDept = proxy.Service.GetDgPlatformDept();
                    DgPlatformDoct = proxy.Service.GetDgPlatformDoct();
                }
            }
            DataSourceOpDept = (DgPlatformDept != null && DgPlatformDept.Count > 0) ? DgPlatformDept : GlobalDic.DataSourceDepartment;
            DataSourceOpDoct = (DgPlatformDoct != null && DgPlatformDoct.Count > 0) ? DgPlatformDoct : GlobalDic.DataSourceDoctor;

            #endregion

            #region lue

            #region 号别
            Viewer.lueRegType.Properties.PopupWidth = 130;
            Viewer.lueRegType.Properties.PopupHeight = 200;
            Viewer.lueRegType.Properties.ValueColumn = EntityCodeReg.Columns.regCode;
            Viewer.lueRegType.Properties.DisplayColumn = EntityCodeReg.Columns.regName;
            Viewer.lueRegType.Properties.Essential = false;
            Viewer.lueRegType.Properties.IsShowColumnHeaders = true;
            Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regCode, 45);
            Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regName, 85);
            Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regCode, "编码");
            Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regName, "名称");
            Viewer.lueRegType.Properties.ShowColumn = EntityCodeReg.Columns.regCode + "|" + EntityCodeReg.Columns.regName;
            Viewer.lueRegType.Properties.IsUseShowColumn = true;
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0) Viewer.lueRegType.Properties.DataSource = DataSourceCodeReg.ToArray();
            Viewer.lueRegType.Properties.SetSize();
            #endregion

            #region 科室
            Viewer.lueDept.Properties.PopupWidth = 160;
            Viewer.lueDept.Properties.PopupHeight = 300;
            Viewer.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            Viewer.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            Viewer.lueDept.Properties.Essential = false;
            Viewer.lueDept.Properties.IsShowColumnHeaders = true;
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 100);
            Viewer.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptCode, "编码");
            Viewer.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptName, "名称");
            Viewer.lueDept.Properties.ShowColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName;
            Viewer.lueDept.Properties.IsUseShowColumn = true;
            Viewer.lueDept.Properties.FilterColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName + "|" + EntityCodeDepartment.Columns.pyCode + "|" + EntityCodeDepartment.Columns.wbCode;
            if (this.DataSourceOpDept != null && this.DataSourceOpDept.Count > 0) Viewer.lueDept.Properties.DataSource = this.DataSourceOpDept.ToArray();
            Viewer.lueDept.Properties.SetSize();
            #endregion

            #region 医师
            Viewer.lueDoct.Properties.PopupWidth = 155;
            Viewer.lueDoct.Properties.PopupHeight = 250;
            Viewer.lueDoct.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            Viewer.lueDoct.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.Essential = false;
            Viewer.lueDoct.Properties.IsShowColumnHeaders = true;
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 70);
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 85);
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "编码");
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "名称");
            Viewer.lueDoct.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.IsUseShowColumn = true;
            if (this.DataSourceOpDoct != null && this.DataSourceOpDoct.Count > 0) Viewer.lueDoct.Properties.DataSource = this.DataSourceOpDoct.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #endregion

            Viewer.cboType.SelectedIndex = 0;
            Viewer.lblType.Visible = (GlobalHospital.Current == EnumHospitalCode.东莞八院 ? true : false);
            Viewer.cboType.Visible = (GlobalHospital.Current == EnumHospitalCode.东莞八院 ? true : false);

            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                Viewer.lblType.Visible = true;
                Viewer.cboType.Visible = true;
                Viewer.cboType.Properties.Items.Clear();
                Viewer.cboType.Properties.Items.Add("全部");
                Viewer.cboType.Properties.Items.Add("微信");
                Viewer.cboType.Properties.Items.Add("APP");
                Viewer.cboType.Properties.Items.Add("诊间");
                Viewer.cboType.Properties.Items.Add("现场");
            }
        }
        #endregion

        #region SetRowCellStyle
        /// <summary>
        /// SetRowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int status = Function.Int(Viewer.gvNo.GetRowCellValue(e.RowHandle, EntityOpRegBooking.Columns.status));
            if (status == -2)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            else if (status == -1)
            {
                e.Appearance.ForeColor = Color.FromArgb(192, 0, 0); //Color.Blue;
            }
            else if (status == 1)
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 176, 80);  //Color.Crimson;
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        internal void Search()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                EntityRegQueryParm queryVo = new EntityRegQueryParm();
                queryVo.dateType = Viewer.cboDateType.SelectedIndex;
                queryVo.startDate = Viewer.dteStart.Text == string.Empty ? DateTime.Now.ToString("yyyy-MM-dd") : Viewer.dteStart.Text;
                queryVo.endDate = Viewer.dteEnd.Text == string.Empty ? DateTime.Now.ToString("yyyy-MM-dd") : Viewer.dteEnd.Text;
                if (Convert.ToDateTime(queryVo.startDate) > Convert.ToDateTime(queryVo.endDate))
                {
                    DialogBox.Msg("查询开始日期不能大于结束日期,请重新输入。");
                    Viewer.dteStart.Focus();
                    return;
                }
                queryVo.deptCode = Viewer.lueDept.Properties.DBValue;
                queryVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                queryVo.regCode = Viewer.lueRegType.Properties.DBValue;
                queryVo.cardNo = Viewer.txtCardNo.Text.Trim();
                if (!string.IsNullOrEmpty(queryVo.cardNo)) queryVo.cardNo += "%";
                queryVo.isVirtual = Viewer.cboType.SelectedIndex;

                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    int num1 = 0;
                    int num2 = 0;
                    string info = "有效:{0}人 取消:{1}人";
                    List<EntityOpRegBooking> data = proxy.Service.GetRegBookingInfo2(queryVo);
                    if (data != null)
                    {
                        // 防止身份证号导出变成科学计数法
                        foreach (EntityOpRegBooking item in data)
                        {
                            item.idNo = " " + item.idNo + " ";
                            if (item.status >= 0)
                                num1++;
                            else
                                num2++;
                            if (GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                            {
                                // 预约->挂号
                                if (item.status == 0 && item.regType == 0) item.status = 9;
                            }
                        }
                    }
                    Viewer.gcNo.DataSource = data;
                    Viewer.lblNums.Text = string.Format(info, num1.ToString(), num2.ToString());
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #endregion

    }
}
