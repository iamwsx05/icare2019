using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 预约计划控制类
    /// </summary>
    public class ctlSchedulingEdit : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmSchedulingEdit Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmSchedulingEdit)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// isInit
        /// </summary>
        bool isInit { get; set; }

        /// <summary>
        /// 编辑类型: 1 排班计划(周排班); 2 日排班
        /// </summary>
        internal int EditType { get; set; }

        /// <summary>
        /// RegId
        /// </summary>
        internal decimal RegId { get; set; }

        /// <summary>
        /// 号别字典
        /// </summary>
        List<EntityCodeReg> DataSourceCodeReg { get; set; }

        /// <summary>
        /// 诊室字典
        /// </summary>
        List<EntityDicDeptRoom> DataSourceRoom { get; set; }

        /// <summary>
        /// 专科号别
        /// </summary>
        List<EntityDicDeptReg> DataSourceDeptReg { get; set; }

        /// <summary>
        /// 职称字典
        /// </summary>
        List<EntityCodeRank> DataSourceRank { get; set; }

        /// <summary>
        /// 号源.数据源(周排班)
        /// </summary>
        List<EntityOpRegSchedulingNumber> DataSourceRegNum { get; set; }

        /// <summary>
        /// 号源.数据源(周排班)
        /// </summary>
        List<EntityOpRegSchedulingDayNumber> DataSourceDayRegNum { get; set; }

        /// <summary>
        /// 班次字典
        /// </summary>
        List<EntityDicShift> DataSourceShift { get; set; }

        /// <summary>
        /// 前一日排班记录.预约日期
        /// </summary>
        string preRegDate { get; set; }
        /// <summary>
        /// 前一日排班记录.挂号类型
        /// </summary>
        string preRegCode { get; set; }
        /// <summary>
        /// 前一日排班记录.科室编码
        /// </summary>
        string preDeptCode { get; set; }
        /// <summary>
        /// 前一日排班记录.医师工号
        /// </summary>
        string preDoctCode { get; set; }
        /// <summary>
        /// load状态
        /// </summary>
        bool isLoad { get; set; }

        /// <summary>
        /// 日排班.编辑
        /// </summary>
        bool isDayEdit { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                isInit = true;
                this.gvDataBindingSource = new BindingSource();
                Viewer.gcNo.DataSource = gvDataBindingSource;
                Viewer.lueDeptReg.Visible = false;

                InitLue();
                InitNumber();

                #region setVisible

                if (EditType == 1)
                {
                    Viewer.cboWeek.Visible = true;
                    Viewer.dteRegDate.Visible = false;
                    Viewer.lblLtFlag.Visible = true;
                    Viewer.rdoLtFlag.Visible = true;
                    //Viewer.rdoStatus.Visible = false;
                    Viewer.lblStatus.Visible = false;
                    Viewer.cboStatusAm.Visible = false;
                    Viewer.cboStatusPm.Visible = false;
                    Viewer.cboStatusNm.Visible = false;
                    Viewer.cboStatusMm.Visible = false;
                    Viewer.tvDate.Visible = false;

                    Viewer.Width -= Viewer.tvDate.Width;
                }
                else if (EditType == 2)
                {
                    Viewer.lblWeek.Text = "日期：";
                    Viewer.cboWeek.Visible = false;
                    //Viewer.blbiDel.Visibility = BarItemVisibility.Never;
                    Viewer.lblLtFlag.Visible = false;
                    Viewer.rdoLtFlag.Visible = false;
                    //Viewer.rdoStatus.Visible = true;
                    Viewer.lblStatus.Visible = true;
                    Viewer.cboStatusAm.Visible = true;
                    Viewer.cboStatusPm.Visible = true;
                    Viewer.cboStatusNm.Visible = true;
                    Viewer.cboStatusMm.Visible = true;
                    Viewer.dteRegDate.Visible = true;
                    Viewer.dteRegDate.Focus();
                    Viewer.blbiContiue.Visibility = BarItemVisibility.Never;
                    CreateTree();

                }
                #endregion

                New();
                Load();

                SetEditValueChangedEvent(Viewer.pcBackGround);
                SetEditValueChangedEvent(Viewer.gvNo);

                if (EditType == 2 && RegId > 0)
                {
                    //Viewer.dteRegDate.Properties.ReadOnly = true;
                    //Viewer.lueRegType.Properties.ReadOnly = true;
                    //Viewer.lueDept.Properties.ReadOnly = true;
                    //Viewer.lueRoom.Properties.ReadOnly = true;
                    //Viewer.lueDoct.Properties.ReadOnly = true;
                    //Viewer.lueDeptReg.Properties.ReadOnly = true;
                    this.isDayEdit = true;
                }
                this.LueFilterRoom();
                this.LueFilterDoct();
                this.LueFilterExpert();
            }
            finally
            {
                Viewer.ValueChanged = false;
                isInit = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region InitNumber
        /// <summary>
        /// InitNumber
        /// </summary>
        void InitNumber()
        {
            if (EditType == 1)
            {
                DataSourceRegNum = new List<EntityOpRegSchedulingNumber>();
                if (DataSourceRegNum.Count == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        DataSourceRegNum.Add(new EntityOpRegSchedulingNumber());
                    }
                }
                this.gvDataBindingSource.DataSource = DataSourceRegNum;
            }
            else if (EditType == 2)
            {
                DataSourceDayRegNum = new List<EntityOpRegSchedulingDayNumber>();
                if (DataSourceDayRegNum.Count == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        DataSourceDayRegNum.Add(new EntityOpRegSchedulingDayNumber());
                    }
                }
                this.gvDataBindingSource.DataSource = DataSourceDayRegNum;
            }
            Viewer.gvNo.FocusedRowHandle = 0;
        }
        #endregion

        #region InitLue
        /// <summary>
        /// InitLue
        /// </summary>
        void InitLue()
        {
            #region dic
            DataTable dt = null;
            DataSourceCodeReg = new List<EntityCodeReg>();
            DataSourceDeptReg = new List<EntityDicDeptReg>();
            DataSourceRoom = new List<EntityDicDeptRoom>();
            DataSourceShift = new List<EntityDicShift>();
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
                dt = proxy.Service.SelectFullTable(new EntityDicDeptReg());
                DataSourceDeptReg.AddRange(EntityTools.ConvertToEntityList<EntityDicDeptReg>(dt));

                dt = proxy.Service.SelectFullTable(new EntityDicDeptRoom());
                DataSourceRoom.AddRange(EntityTools.ConvertToEntityList<EntityDicDeptRoom>(dt));

                dt = proxy.Service.SelectFullTable(new EntityDicShift());
                DataSourceShift.AddRange(EntityTools.ConvertToEntityList<EntityDicShift>(dt));
            }
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
            if (GlobalDic.DataSourceDepartment != null && GlobalDic.DataSourceDepartment.Count > 0)
            {
                List<EntityCodeDepartment> dataDept = GlobalDic.DataSourceDepartment.FindAll(t => t.type == "1");
                if (dataDept != null && dataDept.Count > 0) Viewer.lueDept.Properties.DataSource = dataDept.ToArray();
            }
            Viewer.lueDept.Properties.SetSize();
            #endregion

            #region 诊间
            Viewer.lueRoom.Properties.PopupWidth = 250;
            Viewer.lueRoom.Properties.PopupHeight = 380;
            Viewer.lueRoom.Properties.ValueColumn = EntityDicDeptRoom.Columns.roomCode;
            Viewer.lueRoom.Properties.DisplayColumn = EntityDicDeptRoom.Columns.roomName;
            Viewer.lueRoom.Properties.Essential = false;
            Viewer.lueRoom.Properties.IsShowColumnHeaders = true;
            Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomCode, 45);
            Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomName, 110);
            Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomDesc, 110);
            Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomCode, "编码");
            Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomName, "名称");
            Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomDesc, "地点");
            Viewer.lueRoom.Properties.ShowColumn = EntityDicDeptRoom.Columns.roomCode + "|" + EntityDicDeptRoom.Columns.roomName + "|" + EntityDicDeptRoom.Columns.roomDesc;
            Viewer.lueRoom.Properties.IsUseShowColumn = true;
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
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #region 专科
            Viewer.lueDeptReg.Properties.PopupWidth = 130;
            Viewer.lueDeptReg.Properties.PopupHeight = 160;
            Viewer.lueDeptReg.Properties.ValueColumn = EntityDicDeptReg.Columns.regCode;
            Viewer.lueDeptReg.Properties.DisplayColumn = EntityDicDeptReg.Columns.regName;
            Viewer.lueDeptReg.Properties.Essential = false;
            Viewer.lueDeptReg.Properties.IsShowColumnHeaders = true;
            Viewer.lueDeptReg.Properties.ColumnWidth.Add(EntityDicDeptReg.Columns.regCode, 45);
            Viewer.lueDeptReg.Properties.ColumnWidth.Add(EntityDicDeptReg.Columns.regName, 85);
            Viewer.lueDeptReg.Properties.ColumnHeaders.Add(EntityDicDeptReg.Columns.regCode, "编码");
            Viewer.lueDeptReg.Properties.ColumnHeaders.Add(EntityDicDeptReg.Columns.regName, "名称");
            Viewer.lueDeptReg.Properties.ShowColumn = EntityDicDeptReg.Columns.regCode + "|" + EntityDicDeptReg.Columns.regName;
            Viewer.lueDeptReg.Properties.IsUseShowColumn = true;
            #endregion

            #endregion

        }
        #endregion

        #region CreateTree
        /// <summary>
        /// CreateTree
        /// </summary>
        void CreateTree()
        {
            // 树结构
            Viewer.tvDate.Columns.Clear();
            uiHelper.SetGridCol(Viewer.tvDate, new string[] { "regDate" }, new string[] { "排班日期" }, new int[] { 200 });
            Viewer.tvDate.Columns["regDate"].AppearanceCell.Font = new Font("宋体", 9);
            Viewer.tvDate.KeyFieldName = "regDid";
            Viewer.tvDate.ParentFieldName = "regParent";
            Viewer.tvDate.ImageIndexFieldName = "imageIndex";

            Viewer.tvDate.OptionsView.ShowFocusedFrame = false;
            Viewer.tvDate.Appearance.FocusedRow.Options.UseBackColor = true;
            Viewer.tvDate.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            Viewer.tvDate.Appearance.FocusedRow.BackColor2 = Color.White;
            Viewer.tvDate.Appearance.HideSelectionRow.Options.UseBackColor = true;
            Viewer.tvDate.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            Viewer.tvDate.Appearance.HideSelectionRow.BackColor2 = Color.White;

            Viewer.tvDate.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvDate_FocusedNodeChanged);
        }
        #endregion

        #region tvDate_FocusedNodeChanged
        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }
        /// <summary>
        /// tvDept_FocusedNodeChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDate_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (isInit) return;
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                if (Viewer.ValueChanged)
                {
                    if (DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Save(false);
                        return;
                    }
                }
                #region Load
                if (e.Node == null) return;
                EntityOpRegSchedulingDay mainVo = (EntityOpRegSchedulingDay)Viewer.tvDate.GetDataRecordByNode(e.Node);
                if (mainVo.regParent == -99) return;
                this.RegId = mainVo.regDid;
                if (this.RegId > 0)
                {
                    Load();
                }
                #endregion
            }
            finally
            {
                isTreeDoing = false;
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clear
        /// </summary>
        internal void Clear()
        {
            this.preRegDate = string.Empty;
            this.preRegCode = string.Empty;
            this.preDeptCode = string.Empty;
            this.preDoctCode = string.Empty;
            this.isDayEdit = false;

            Viewer.picPhoto.Image = null;
            Viewer.dteRegDate.Properties.ReadOnly = false;
            Viewer.lueRegType.Properties.ReadOnly = false;
            Viewer.lueDept.Properties.ReadOnly = false;
            Viewer.lueRoom.Properties.ReadOnly = false;
            Viewer.lueDoct.Properties.ReadOnly = false;
            Viewer.lueDeptReg.Properties.ReadOnly = false;

            Viewer.cboWeek.Text = string.Empty;
            Viewer.cboWeek.SelectedIndex = -1;
            Viewer.dteRegDate.Text = string.Empty;
            Viewer.lueRegType.Properties.DBValue = string.Empty;
            Viewer.lueRegType.Text = string.Empty;
            Viewer.lueDept.Properties.DBValue = string.Empty;
            Viewer.lueDept.Text = string.Empty;
            Viewer.lueRoom.Properties.DBValue = string.Empty;
            Viewer.lueRoom.Text = string.Empty;
            Viewer.lueDoct.Properties.DBValue = string.Empty;
            Viewer.lueDoct.Text = string.Empty;
            Viewer.lueDeptReg.Properties.DBValue = string.Empty;
            Viewer.lueDeptReg.Text = string.Empty;

            Viewer.teGam1.EditValue = null;
            Viewer.teGam2.EditValue = null;
            Viewer.teGpm1.EditValue = null;
            Viewer.teGpm2.EditValue = null;
            Viewer.teGnm1.EditValue = null;
            Viewer.teGnm2.EditValue = null;

            Viewer.teBam1.EditValue = null;
            Viewer.teBam2.EditValue = null;
            Viewer.teBpm1.EditValue = null;
            Viewer.teBpm2.EditValue = null;
            Viewer.teBnm1.EditValue = null;
            Viewer.teBnm2.EditValue = null;

            Viewer.teVam1.EditValue = null;
            Viewer.teVam2.EditValue = null;
            Viewer.teVpm1.EditValue = null;
            Viewer.teVpm2.EditValue = null;
            Viewer.teVnm1.EditValue = null;
            Viewer.teVnm2.EditValue = null;
            Viewer.teVmm1.EditValue = null;
            Viewer.teVmm2.EditValue = null;

            Viewer.txtVamLimit.Text = string.Empty;
            Viewer.txtVpmLimit.Text = string.Empty;
            Viewer.txtVnmLimit.Text = string.Empty;
            Viewer.txtVmmLimit.Text = string.Empty;
            Viewer.txtFreqNumAm.Text = string.Empty;
            Viewer.txtFreqNumPm.Text = string.Empty;
            Viewer.txtFreqNumNm.Text = string.Empty;
            Viewer.txtFreqNumMm.Text = string.Empty;
            Viewer.cboStatusAm.Text = string.Empty;
            Viewer.cboStatusPm.Text = string.Empty;
            Viewer.cboStatusNm.Text = string.Empty;
            Viewer.cboStatusMm.Text = string.Empty;
            Viewer.txtIntroduce.Text = string.Empty;
            Viewer.txtSkill.Text = string.Empty;
            Viewer.txtComment.Text = string.Empty;
            Viewer.txtRank.Text = string.Empty;

            InitNumber();
            Viewer.ValueChanged = false;
        }
        #endregion

        #region ClearTime
        /// <summary>
        /// ClearTime
        /// </summary>
        /// <param name="type"></param>
        internal void ClearTime(int type)
        {
            switch (type)
            {
                case 1:
                    Viewer.teGam1.EditValue = null;
                    Viewer.teGam2.EditValue = null;
                    break;
                case 2:
                    Viewer.teGpm1.EditValue = null;
                    Viewer.teGpm2.EditValue = null;
                    break;
                case 3:
                    Viewer.teGnm1.EditValue = null;
                    Viewer.teGnm2.EditValue = null;
                    break;
                case 4:
                    Viewer.teBam1.EditValue = null;
                    Viewer.teBam2.EditValue = null;
                    break;
                case 5:
                    Viewer.teBpm1.EditValue = null;
                    Viewer.teBpm2.EditValue = null;
                    break;
                case 6:
                    Viewer.teBnm1.EditValue = null;
                    Viewer.teBnm2.EditValue = null;
                    break;
                case 7:
                    Viewer.teVam1.EditValue = null;
                    Viewer.teVam2.EditValue = null;
                    Viewer.txtVamLimit.Text = string.Empty;
                    Viewer.txtFreqNumAm.Text = string.Empty;
                    Viewer.cboStatusAm.Text = string.Empty;
                    break;
                case 8:
                    Viewer.teVpm1.EditValue = null;
                    Viewer.teVpm2.EditValue = null;
                    Viewer.txtVpmLimit.Text = string.Empty;
                    Viewer.txtFreqNumPm.Text = string.Empty;
                    Viewer.cboStatusPm.Text = string.Empty;
                    break;
                case 9:
                    Viewer.teVnm1.EditValue = null;
                    Viewer.teVnm2.EditValue = null;
                    Viewer.txtVnmLimit.Text = string.Empty;
                    Viewer.txtFreqNumNm.Text = string.Empty;
                    Viewer.cboStatusNm.Text = string.Empty;
                    break;
                case 10:
                    Viewer.teVmm1.EditValue = null;
                    Viewer.teVmm2.EditValue = null;
                    Viewer.txtVmmLimit.Text = string.Empty;
                    Viewer.txtFreqNumMm.Text = string.Empty;
                    Viewer.cboStatusMm.Text = string.Empty;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region AddNumberRow
        /// <summary>
        /// AddNumberRow
        /// </summary>
        internal void AddNumberRow()
        {
            AppendRow(this.gvDataBindingSource);
        }
        #endregion

        #region DelNumberRow
        /// <summary>
        /// DelNumberRow
        /// </summary>
        internal void DelNumberRow()
        {
            DeleteRow(Viewer.gvNo, this.gvDataBindingSource, Viewer.gvNo.FocusedRowHandle);
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            Clear();
            Viewer.cboWeek.Tag = null;
            Viewer.dteRegDate.Tag = null;
            if (this.EditType == 1)
            {
                Viewer.cboWeek.SelectedIndex = uiHelper.NumOfWeek();
            }
            else if (this.EditType == 2)
            {
                Viewer.dteRegDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
            {
                Viewer.lueRegType.Properties.ForbidPoput = true;
                Viewer.lueRegType.Properties.DBValue = DataSourceCodeReg[0].regCode;
                Viewer.lueRegType.Text = DataSourceCodeReg[0].regName;
                Viewer.lueRegType.Properties.DisplayValue = Viewer.lueRegType.Text;
                Viewer.lueRegType.Properties.ForbidPoput = false;
            }
        }
        #endregion

        #region 续排
        /// <summary>
        /// 续排
        /// </summary>
        internal void Continue()
        {
            Viewer.cboWeek.Text = string.Empty;
            Viewer.cboWeek.SelectedIndex = -1;
            Viewer.cboWeek.Tag = null;

            Viewer.dteRegDate.Properties.ReadOnly = false;
            Viewer.lueRegType.Properties.ReadOnly = true;
            Viewer.lueDept.Properties.ReadOnly = true;
            Viewer.lueRoom.Properties.ReadOnly = true;
            Viewer.lueDoct.Properties.ReadOnly = true;
            Viewer.lueDeptReg.Properties.ReadOnly = true;

            Viewer.ValueChanged = false;
        }
        #endregion

        #region SetTreeDataSource
        /// <summary>
        /// SetTreeDataSource
        /// </summary>
        /// <param name="_vo"></param>
        void SetTreeDataSource(EntityOpRegSchedulingDay _vo)
        {
            if (_vo == null) return;
            List<EntityOpRegSchedulingDay> dataSource = Viewer.tvDate.DataSource as List<EntityOpRegSchedulingDay>;
            if (dataSource != null && dataSource.Count > 0)
            {
                if (dataSource.Exists(t => t.deptCode != _vo.deptCode || t.doctCode != _vo.doctCode))
                {
                    DialogBox.Msg("当前新增日排班记录与左侧列表不一致，左侧列表不刷新。");
                    return;
                }
            }
            SetTreeDataSource(new List<EntityOpRegSchedulingDay>() { _vo });
        }
        /// <summary>
        /// SetTreeDataSource
        /// </summary>
        /// <param name="_dataSource"></param>
        void SetTreeDataSource(List<EntityOpRegSchedulingDay> _dataSource)
        {
            if (_dataSource == null || _dataSource.Count == 0) return;
            List<EntityOpRegSchedulingDay> dataSource = Viewer.tvDate.DataSource as List<EntityOpRegSchedulingDay>;
            if (dataSource == null)
            {
                dataSource = new List<EntityOpRegSchedulingDay>();
            }
            else
            {
                for (int i = dataSource.Count - 1; i >= 0; i--)
                {
                    if (dataSource[i].regParent == -99) dataSource.RemoveAt(i);
                }
            }

            List<string> lstMonth = new List<string>();
            dataSource.AddRange(_dataSource);
            dataSource.Sort();
            string regMonth = string.Empty;
            for (int i = dataSource.Count - 1; i >= 0; i--)
            {
                regMonth = dataSource[i].regDate.Substring(0, 7);
                dataSource[i].regParent = Convert.ToDecimal(regMonth.Replace("-", "").Replace(".", "")) * (-1);
                dataSource[i].imageIndex = 1;
                if (lstMonth.IndexOf(regMonth) < 0) lstMonth.Add(regMonth);
            }
            EntityOpRegSchedulingDay monthVo = null;
            foreach (string str in lstMonth)
            {
                monthVo = new EntityOpRegSchedulingDay();
                monthVo.regDid = Convert.ToDecimal(str.Replace("-", "").Replace(".", "")) * (-1);
                monthVo.regDate = str + " 月";
                monthVo.regParent = -99;
                monthVo.imageIndex = 0;
                dataSource.Add(monthVo);
            }

            if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                foreach (EntityOpRegSchedulingDay item in dataSource)
                {
                    if (!string.IsNullOrEmpty(item.regCode))
                    {
                        if (DataSourceCodeReg.Any(t => t.regCode.Trim() == item.regCode.Trim()))
                        {
                            item.regDate += " " + DataSourceCodeReg.FirstOrDefault(t => t.regCode.Trim() == item.regCode.Trim()).regName.Replace("号", "").Trim();
                        }
                    }
                }
            }
            Viewer.tvDate.BeginUpdate();
            Viewer.tvDate.DataSource = dataSource;
            Viewer.tvDate.RefreshDataSource();
            //Viewer.tvDate.ExpandAll();
            Viewer.tvDate.CollapseAll();
            Viewer.tvDate.EndUpdate();
        }
        #endregion

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        void Load()
        {
            try
            {
                this.isLoad = true;
                Viewer.ValueChanged = false;
                this.Clear();
                if (this.EditType == 1 && this.RegId > 0)
                {
                    #region 排班计划
                    EntityOpRegScheduling mainVo = null;
                    List<EntityOpRegSchedulingDate> lstRegDate = null;
                    List<EntityOpRegSchedulingNumber> lstRegNumber = null;
                    EntityOpRegSchedulingDoct doctVo = null;
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        proxy.Service.GetScheduling(this.RegId, out mainVo, out lstRegDate, out lstRegNumber, DataSourceCodeReg.FindAll(t => t.regFlag == "2"), out doctVo);
                    }
                    Viewer.cboWeek.Tag = mainVo;
                    Viewer.cboWeek.SelectedIndex = Function.Int(mainVo.weekId);
                    Viewer.rdoLtFlag.SelectedIndex = Function.Int(mainVo.ltFlag);
                    SetLueRegType(mainVo.regCode);
                    SetLueDept(mainVo.deptCode);
                    SetLueRoom(mainVo.deptCode, mainVo.roomCode);
                    Viewer.txtComment.Text = mainVo.comment;

                    // 挂到医师.医师信息
                    bool isExpert = false;
                    if (doctVo != null && !string.IsNullOrEmpty(doctVo.doctCode))
                    {
                        SetLueDoct(mainVo.doctCode);
                        SetDoctPlusInfo(doctVo);
                        LueFilterRank();
                    }
                    else
                    {
                        SetLueExpert(mainVo.deptCode, mainVo.doctCode);
                        isExpert = true;
                    }
                    SetExpert(isExpert);

                    #region 时间设定
                    // 时间设定
                    if (lstRegDate != null)
                    {
                        foreach (EntityOpRegSchedulingDate item in lstRegDate)
                        {
                            if (item.typeId == 1)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teGam1.EditValue = item.startTime;
                                    Viewer.teGam2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teGpm1.EditValue = item.startTime;
                                    Viewer.teGpm2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teGnm1.EditValue = item.startTime;
                                    Viewer.teGnm2.EditValue = item.endTime;
                                }
                            }
                            else if (item.typeId == 2)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teBam1.EditValue = item.startTime;
                                    Viewer.teBam2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teBpm1.EditValue = item.startTime;
                                    Viewer.teBpm2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teBnm1.EditValue = item.startTime;
                                    Viewer.teBnm2.EditValue = item.endTime;
                                }
                            }
                            else if (item.typeId == 3)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teVam1.EditValue = item.startTime;
                                    Viewer.teVam2.EditValue = item.endTime;
                                    Viewer.txtVamLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumAm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teVpm1.EditValue = item.startTime;
                                    Viewer.teVpm2.EditValue = item.endTime;
                                    Viewer.txtVpmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumPm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teVnm1.EditValue = item.startTime;
                                    Viewer.teVnm2.EditValue = item.endTime;
                                    Viewer.txtVnmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumNm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                }
                                else if (item.amPm == 4)
                                {
                                    Viewer.teVmm1.EditValue = item.startTime;
                                    Viewer.teVmm2.EditValue = item.endTime;
                                    Viewer.txtVmmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumMm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                }
                            }
                        }
                    }
                    #endregion

                    // 号源
                    this.gvDataBindingSource.DataSource = lstRegNumber;
                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        Viewer.gvNo.FocusedColumn = Viewer.colNormalNo;
                        foreach (EntityOpRegSchedulingNumber item in lstRegNumber)
                        {
                            if (DataSourceShift.Any(t => t.shiftCode == item.amPm))
                            {
                                item.amPmName = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).shiftName;
                            }
                        }
                    }
                    #endregion
                }
                else if (this.EditType == 2 && this.RegId > 0)
                {
                    #region 日排班
                    EntityOpRegSchedulingDay mainVo = null;
                    List<EntityOpRegSchedulingDayDate> lstRegDate = null;
                    List<EntityOpRegSchedulingDayNumber> lstRegNumber = null;
                    EntityOpRegSchedulingDoct doctVo = null;
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        proxy.Service.GetSchedulingDay(this.RegId, out mainVo, out lstRegDate, out lstRegNumber, DataSourceCodeReg.FindAll(t => t.regFlag == "2"), out doctVo);
                    }
                    Viewer.dteRegDate.Tag = mainVo;
                    Viewer.dteRegDate.Text = mainVo.regDate;
                    SetLueRegType(mainVo.regCode);
                    SetLueDept(mainVo.deptCode);
                    SetLueRoom(mainVo.deptCode, mainVo.roomCode);
                    Viewer.txtComment.Text = mainVo.comment;
                    Viewer.rdoStatus.SelectedIndex = Function.Int(mainVo.status);
                    // 挂到医师.医师信息
                    bool isExpert = false;
                    if (doctVo != null && !string.IsNullOrEmpty(doctVo.doctCode))
                    {
                        SetLueDoct(mainVo.doctCode);
                        SetDoctPlusInfo(doctVo);
                        LueFilterRank();
                    }
                    else
                    {
                        SetLueExpert(mainVo.deptCode, mainVo.doctCode);
                        isExpert = true;
                    }
                    SetExpert(isExpert);
                    // 刷新树
                    if (isInit)
                    {
                        List<EntityOpRegSchedulingDay> dataSourceTree = null;
                        using (ProxyScheduling proxy = new ProxyScheduling())
                        {
                            dataSourceTree = proxy.Service.GetDayRegListByDoct(mainVo.deptCode, mainVo.doctCode);
                        }
                        Viewer.tvDate.DataSource = null;
                        SetTreeDataSource(dataSourceTree);
                        Viewer.tvDate.SetFocusedNode(Viewer.tvDate.FindNodeByKeyID(this.RegId));
                    }

                    #region 时间设定
                    // 时间设定
                    if (lstRegDate != null)
                    {
                        foreach (EntityOpRegSchedulingDayDate item in lstRegDate)
                        {
                            if (item.typeId == 1)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teGam1.EditValue = item.startTime;
                                    Viewer.teGam2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teGpm1.EditValue = item.startTime;
                                    Viewer.teGpm2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teGnm1.EditValue = item.startTime;
                                    Viewer.teGnm2.EditValue = item.endTime;
                                }
                            }
                            else if (item.typeId == 2)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teBam1.EditValue = item.startTime;
                                    Viewer.teBam2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teBpm1.EditValue = item.startTime;
                                    Viewer.teBpm2.EditValue = item.endTime;
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teBnm1.EditValue = item.startTime;
                                    Viewer.teBnm2.EditValue = item.endTime;
                                }
                            }
                            else if (item.typeId == 3)
                            {
                                if (item.amPm == 1)
                                {
                                    Viewer.teVam1.EditValue = item.startTime;
                                    Viewer.teVam2.EditValue = item.endTime;
                                    Viewer.txtVamLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumAm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                    Viewer.cboStatusAm.SelectedIndex = item.status + 1;
                                }
                                else if (item.amPm == 2)
                                {
                                    Viewer.teVpm1.EditValue = item.startTime;
                                    Viewer.teVpm2.EditValue = item.endTime;
                                    Viewer.txtVpmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumPm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                    Viewer.cboStatusPm.SelectedIndex = item.status + 1;
                                }
                                else if (item.amPm == 3)
                                {
                                    Viewer.teVnm1.EditValue = item.startTime;
                                    Viewer.teVnm2.EditValue = item.endTime;
                                    Viewer.txtVnmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumNm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                    Viewer.cboStatusNm.SelectedIndex = item.status + 1;
                                }
                                else if (item.amPm == 4)
                                {
                                    Viewer.teVmm1.EditValue = item.startTime;
                                    Viewer.teVmm2.EditValue = item.endTime;
                                    Viewer.txtVmmLimit.Text = (item.limitNum == 0 ? "" : item.limitNum.ToString());
                                    Viewer.txtFreqNumMm.Text = (item.freqNum == 0 ? "" : item.freqNum.ToString());
                                    Viewer.cboStatusMm.SelectedIndex = item.status + 1;
                                }
                            }
                        }
                    }
                    #endregion

                    // 号源
                    this.gvDataBindingSource.DataSource = lstRegNumber;
                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        Viewer.gvNo.FocusedColumn = Viewer.colNormalNo;
                        foreach (EntityOpRegSchedulingDayNumber item in lstRegNumber)
                        {
                            if (DataSourceShift.Any(t => t.shiftCode == item.amPm))
                            {
                                item.amPmName = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).shiftName;
                            }
                        }
                    }

                    this.preRegDate = mainVo.regDate;
                    this.preRegCode = Viewer.lueRegType.Properties.DBValue;
                    this.preDeptCode = Viewer.lueDept.Properties.DBValue;
                    this.preDoctCode = Viewer.lueDoct.Visible ? Viewer.lueDoct.Properties.DBValue : Viewer.lueDeptReg.Properties.DBValue;

                    #endregion
                }
            }
            finally
            {
                Viewer.ValueChanged = false;
                this.isLoad = false;
            }
        }

        #region SetLueRegType
        /// <summary>
        /// SetLueRegType
        /// </summary>
        /// <param name="regCode"></param>
        void SetLueRegType(string regCode)
        {
            Viewer.lueRegType.Properties.DBValue = regCode;
            Viewer.lueRegType.Properties.ForbidPoput = true;
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
            {
                if (DataSourceCodeReg.Any(t => t.regCode == regCode))
                    Viewer.lueRegType.Text = (DataSourceCodeReg.FirstOrDefault(t => t.regCode == regCode)).regName;
                else
                    Viewer.lueRegType.Text = string.Empty;
            }
            Viewer.lueRegType.Properties.DisplayValue = Viewer.lueRegType.Text;
            Viewer.lueRegType.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueDept
        /// <summary>
        /// SetLueDept
        /// </summary>
        /// <param name="deptCode"></param>
        void SetLueDept(string deptCode)
        {
            Viewer.lueDept.Properties.DBValue = deptCode;
            Viewer.lueDept.Properties.ForbidPoput = true;
            if (GlobalDic.DataSourceDepartment != null && GlobalDic.DataSourceDepartment.Count > 0)
            {
                if (GlobalDic.DataSourceDepartment.Any(t => t.deptCode == deptCode))
                    Viewer.lueDept.Text = (GlobalDic.DataSourceDepartment.FirstOrDefault(t => t.deptCode == deptCode)).deptName;
                else
                    Viewer.lueDept.Text = string.Empty;
            }
            Viewer.lueDept.Properties.DisplayValue = Viewer.lueDept.Text;
            Viewer.lueDept.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueRoom
        /// <summary>
        /// SetLueRoom
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="roomCode"></param>
        void SetLueRoom(string deptCode, string roomCode)
        {
            Viewer.lueRoom.Properties.DBValue = roomCode;
            Viewer.lueRoom.Properties.ForbidPoput = true;
            if (DataSourceRoom != null && DataSourceRoom.Count > 0)
            {
                if (DataSourceRoom.Any(t => t.deptCode.Trim() == deptCode.Trim() && t.roomCode.Trim() == roomCode.Trim()))
                    Viewer.lueRoom.Text = (DataSourceRoom.FirstOrDefault(t => t.deptCode.Trim() == deptCode.Trim() && t.roomCode.Trim() == roomCode.Trim())).roomName;
                else
                    Viewer.lueRoom.Text = string.Empty;
            }
            Viewer.lueRoom.Properties.DisplayValue = Viewer.lueRoom.Text;
            Viewer.lueRoom.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueDoct
        /// <summary>
        /// SetLueDoct
        /// </summary>
        /// <param name="doctCode"></param>
        void SetLueDoct(string doctCode)
        {
            Viewer.lueDoct.Properties.DBValue = doctCode;
            Viewer.lueDoct.Properties.ForbidPoput = true;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0)
            {
                if (GlobalDic.DataSourceDoctor.Any(t => t.operCode.ToUpper() == doctCode.ToUpper()))
                    Viewer.lueDoct.Text = (GlobalDic.DataSourceDoctor.FirstOrDefault(t => t.operCode.ToUpper() == doctCode.ToUpper())).operName;
                else
                    Viewer.lueDoct.Text = string.Empty;
            }
            Viewer.lueDoct.Properties.DisplayValue = Viewer.lueDoct.Text;
            Viewer.lueDoct.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueExpert
        /// <summary>
        /// SetLueExpert
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regCode"></param>
        void SetLueExpert(string deptCode, string regCode)
        {
            Viewer.lueDeptReg.Properties.DBValue = regCode;
            Viewer.lueDeptReg.Properties.ForbidPoput = true;
            if (DataSourceDeptReg != null && DataSourceDeptReg.Count > 0)
            {
                if (DataSourceDeptReg.Any(t => t.deptCode == deptCode && t.regCode == regCode))
                    Viewer.lueDeptReg.Text = (DataSourceDeptReg.FirstOrDefault(t => t.deptCode == deptCode && t.regCode == regCode)).regName;
                else
                    Viewer.lueDeptReg.Text = string.Empty;
            }
            Viewer.lueDeptReg.Properties.DisplayValue = Viewer.lueDeptReg.Text;
            Viewer.lueDeptReg.Properties.ForbidPoput = false;
        }
        #endregion

        #endregion

        #region GetRegDateList
        /// <summary>
        /// GetRegDateList
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="typeId"></param>
        /// <param name="amPm"></param>
        /// <param name="limitNum"></param>
        /// <param name="lstRegDate"></param>
        void GetRegDateList(string startTime, string endTime, int typeId, int amPm, int limitNum, decimal freqNum, ref List<EntityOpRegSchedulingDate> lstRegDate)
        {
            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime) &&
                endTime != "00:00" && Convert.ToDateTime(endTime) > Convert.ToDateTime(startTime))
            {
                EntityOpRegSchedulingDate dateVo = new EntityOpRegSchedulingDate();
                dateVo.typeId = typeId;
                dateVo.amPm = amPm;
                dateVo.startTime = startTime;
                dateVo.endTime = endTime;
                dateVo.limitNum = limitNum;
                dateVo.freqNum = freqNum;
                lstRegDate.Add(dateVo);
            }
        }
        #endregion

        #region GetRegDateListDay
        /// <summary>
        /// GetRegDateListDay
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="typeId"></param>
        /// <param name="amPm"></param>
        /// <param name="limitNum"></param>
        /// <param name="lstRegDate"></param>
        void GetRegDateListDay(string startTime, string endTime, int typeId, int amPm, int limitNum, decimal freqNum, int status, ref List<EntityOpRegSchedulingDayDate> lstRegDate)
        {
            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime) &&
                endTime != "00:00" && Convert.ToDateTime(endTime) > Convert.ToDateTime(startTime))
            {
                EntityOpRegSchedulingDayDate dateVo = new EntityOpRegSchedulingDayDate();
                dateVo.typeId = typeId;
                dateVo.amPm = amPm;
                dateVo.startTime = startTime;
                dateVo.endTime = endTime;
                dateVo.limitNum = limitNum;
                dateVo.freqNum = freqNum;
                dateVo.status = status - 1;
                if (typeId == 3 && (string.IsNullOrEmpty(dateVo.startTime) || string.IsNullOrEmpty(dateVo.startTime) || dateVo.limitNum == 0)) return;
                lstRegDate.Add(dateVo);
            }
        }
        #endregion

        #region ResetFreqNums
        /// <summary>
        /// ResetFreqNums
        /// </summary>
        /// <param name="teStart"></param>
        /// <param name="teEnd"></param>
        /// <param name="teNum"></param>
        /// <param name="teFreq"></param>
        void ResetFreqNums(DevExpress.XtraEditors.TimeEdit teStart, DevExpress.XtraEditors.TimeEdit teEnd, DevExpress.XtraEditors.TextEdit teNum, DevExpress.XtraEditors.TextEdit teFreq)
        {
            string startTime = teStart.Text;
            string endTime = teEnd.Text;
            int limitNum = Function.Int(teNum.Text);
            if (string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime) || limitNum == 0) return;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan ts = Convert.ToDateTime(date + " " + endTime).Subtract(Convert.ToDateTime(date + " " + startTime));
            if (ts.TotalMinutes > 0)
            {
                teFreq.Text = Math.Floor(Convert.ToDecimal(ts.TotalMinutes / limitNum)).ToString();
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="isExit"></param>
        internal void Save(bool isExit)
        {
            Viewer.gvNo.CloseEditor();

            if (this.EditType == 1)
            {
                #region 排班计划

                #region 主信息
                // 主信息
                EntityOpRegScheduling mainOrig = Viewer.cboWeek.Tag as EntityOpRegScheduling;
                EntityOpRegScheduling mainVo = new EntityOpRegScheduling();
                mainVo.regWid = (mainOrig == null ? 0 : mainOrig.regWid);
                mainVo.weekId = Viewer.cboWeek.SelectedIndex;
                mainVo.regCode = Viewer.lueRegType.Properties.DBValue;
                mainVo.deptCode = Viewer.lueDept.Properties.DBValue;
                mainVo.roomCode = Viewer.lueRoom.Properties.DBValue;
                mainVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                mainVo.comment = Viewer.txtComment.Text.Trim();
                mainVo.ltFlag = Viewer.rdoLtFlag.SelectedIndex;

                #region 检测

                if (mainVo.weekId < 0)
                {
                    DialogBox.Msg("请选择周几.");
                    Viewer.cboWeek.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.regCode))
                {
                    DialogBox.Msg("请选择号别.");
                    Viewer.lueRegType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.deptCode))
                {
                    DialogBox.Msg("请选择科室.");
                    Viewer.lueDoct.Focus();
                    return;
                }

                #endregion

                // 医师资料
                EntityOpRegSchedulingDoct doctVo = null;
                if (DataSourceCodeReg != null && DataSourceCodeReg.Exists(t => t.regCode == mainVo.regCode))
                {
                    if (DataSourceCodeReg.FirstOrDefault(t => t.regCode == mainVo.regCode).regFlag == "1")
                    {
                        mainVo.doctCode = Viewer.lueDeptReg.Properties.DBValue;
                        if (string.IsNullOrEmpty(mainVo.doctCode))
                        {
                            DialogBox.Msg("请选择专科.");
                            Viewer.lueDoct.Focus();
                            return;
                        }
                    }
                    else if (DataSourceCodeReg.FirstOrDefault(t => t.regCode == mainVo.regCode).regFlag == "2")
                    {
                        if (string.IsNullOrEmpty(mainVo.doctCode))
                        {
                            DialogBox.Msg("请选择医师.");
                            Viewer.lueDoct.Focus();
                            return;
                        }

                        doctVo = new EntityOpRegSchedulingDoct();
                        doctVo.doctCode = mainVo.doctCode;
                        doctVo.doctName = Viewer.lueDoct.Text;
                        doctVo.doctSkill = Viewer.txtSkill.Text.Trim();
                        doctVo.doctIntroduce = Viewer.txtIntroduce.Text.Trim();
                        if (Viewer.picPhoto.Image != null)
                            doctVo.doctPhoto = Function.ConvertImageToByte(Viewer.picPhoto.Image, 4);
                    }
                    if (string.IsNullOrEmpty(mainVo.doctCode))
                    {
                        DialogBox.Msg("请选择:医师/专科");
                        return;
                    }
                }
                else
                {
                    DialogBox.Msg("号别字典错误，请检测。");
                    return;
                }
                #endregion

                // 挂号时间设置
                List<EntityOpRegSchedulingDate> lstRegDate = new List<EntityOpRegSchedulingDate>();
                #region 挂号开放时间
                // 挂号开放时间            
                // am
                GetRegDateList(Viewer.teGam1.Text, Viewer.teGam2.Text, 1, 1, 0, 0, ref lstRegDate);
                // pm
                GetRegDateList(Viewer.teGpm1.Text, Viewer.teGpm2.Text, 1, 2, 0, 0, ref lstRegDate);
                // nm
                GetRegDateList(Viewer.teGnm1.Text, Viewer.teGnm2.Text, 1, 3, 0, 0, ref lstRegDate);
                #endregion
                #region 预约开放时间
                // 预约开放时间
                // am
                GetRegDateList(Viewer.teBam1.Text, Viewer.teBam2.Text, 2, 1, 0, 0, ref lstRegDate);
                // pm
                GetRegDateList(Viewer.teBpm1.Text, Viewer.teBpm2.Text, 2, 2, 0, 0, ref lstRegDate);
                // nm
                GetRegDateList(Viewer.teBnm1.Text, Viewer.teBnm2.Text, 2, 3, 0, 0, ref lstRegDate);
                #endregion
                #region 坐诊时间
                // 坐诊时间
                // am
                ResetFreqNums(Viewer.teVam1, Viewer.teVam2, Viewer.txtVamLimit, Viewer.txtFreqNumAm);
                GetRegDateList(Viewer.teVam1.Text, Viewer.teVam2.Text, 3, 1, Function.Int(Viewer.txtVamLimit.Text), Function.Dec(Viewer.txtFreqNumAm.Text), ref lstRegDate);
                // pm
                ResetFreqNums(Viewer.teVpm1, Viewer.teVpm2, Viewer.txtVpmLimit, Viewer.txtFreqNumPm);
                GetRegDateList(Viewer.teVpm1.Text, Viewer.teVpm2.Text, 3, 2, Function.Int(Viewer.txtVpmLimit.Text), Function.Dec(Viewer.txtFreqNumPm.Text), ref lstRegDate);
                // nm
                ResetFreqNums(Viewer.teVnm1, Viewer.teVnm2, Viewer.txtVnmLimit, Viewer.txtFreqNumNm);
                GetRegDateList(Viewer.teVnm1.Text, Viewer.teVnm2.Text, 3, 3, Function.Int(Viewer.txtVnmLimit.Text), Function.Dec(Viewer.txtFreqNumNm.Text), ref lstRegDate);
                // mm
                ResetFreqNums(Viewer.teVmm1, Viewer.teVmm2, Viewer.txtVmmLimit, Viewer.txtFreqNumMm);
                GetRegDateList(Viewer.teVmm1.Text, Viewer.teVmm2.Text, 3, 4, Function.Int(Viewer.txtVmmLimit.Text), Function.Dec(Viewer.txtFreqNumMm.Text), ref lstRegDate);
                #endregion

                if (lstRegDate == null || lstRegDate.Count == 0 || lstRegDate.Any(t => t.typeId == 3) == false)
                {
                    DialogBox.Msg("请维护医师出诊时间。");
                    return;
                }
                if (lstRegDate != null && lstRegDate.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDate item in lstRegDate)
                    {
                        item.regWid = mainVo.regWid;
                    }
                }

                // 号源设置
                List<decimal> lstAmPm = new List<decimal>();
                List<EntityOpRegSchedulingNumber> lstRegNumber = this.gvDataBindingSource.DataSource as List<EntityOpRegSchedulingNumber>;
                for (int i = lstRegNumber.Count - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(lstRegNumber[i].startTime) && !string.IsNullOrEmpty(lstRegNumber[i].endTime) &&
                         Convert.ToDateTime(lstRegNumber[i].endTime) > Convert.ToDateTime(lstRegNumber[i].startTime))
                    {
                        if (string.IsNullOrEmpty(lstRegNumber[i].amPmName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定上/下午。");
                            return;
                        }
                        lstRegNumber[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstRegNumber[i].amPmName).shiftCode;
                        lstRegNumber[i].startTime = Convert.ToDateTime(lstRegNumber[i].startTime).ToString("HH:mm");
                        lstRegNumber[i].endTime = Convert.ToDateTime(lstRegNumber[i].endTime).ToString("HH:mm");
                        lstAmPm.Add(lstRegNumber[i].amPm);
                    }
                    else
                    {
                        lstRegNumber.RemoveAt(i);
                    }
                }
                if (lstAmPm.Count > 0)
                {
                    foreach (decimal amPm in lstAmPm)
                    {
                        if (!lstRegDate.Exists(t => t.typeId == 3 && t.amPm == amPm))
                        {
                            DialogBox.Msg("号源设置的时间段上/下午 与出诊时间上/下午不一致，请检查。");
                            return;
                        }
                    }
                }
                if (lstRegNumber == null || lstRegNumber.Count == 0)
                {
                    // 默认值
                    EntityOpRegSchedulingNumber numVo = null;
                    List<EntityOpRegSchedulingDate> lstRegDate3 = lstRegDate.FindAll(t => t.typeId == 3);
                    foreach (EntityOpRegSchedulingDate item in lstRegDate3)
                    {
                        numVo = new EntityOpRegSchedulingNumber();
                        numVo.regWid = item.regWid;
                        numVo.amPm = item.amPm;
                        numVo.startTime = item.startTime;
                        numVo.endTime = item.endTime;
                        numVo.normalNum = item.limitNum;
                        lstRegNumber.Add(numVo);
                    }
                }

                int ret = 0;
                decimal regWid = 0;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    ret = proxy.Service.SaveScheduling(mainVo, lstRegDate, lstRegNumber, doctVo, out regWid);
                }
                if (ret > 0 && regWid > 0)
                {
                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        foreach (EntityOpRegSchedulingNumber item in lstRegNumber)
                        { item.regWid = regWid; }
                        this.gvDataBindingSource.DataSource = lstRegNumber;
                        Viewer.gcNo.RefreshDataSource();
                    }
                    mainVo.regWid = regWid;
                    Viewer.cboWeek.Tag = mainVo;
                    Viewer.IsSave = true;
                    Viewer.dteRegDate.Properties.ReadOnly = false;
                    Viewer.lueRegType.Properties.ReadOnly = false;
                    Viewer.lueDept.Properties.ReadOnly = false;
                    Viewer.lueRoom.Properties.ReadOnly = false;
                    Viewer.lueDoct.Properties.ReadOnly = false;
                    Viewer.lueDeptReg.Properties.ReadOnly = false;

                    Viewer.ValueChanged = false;
                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    DialogBox.Msg("保存失败。");
                }
                #endregion
            }
            else if (this.EditType == 2)
            {
                #region 日排班

                #region 主信息
                // 主信息
                EntityOpRegSchedulingDay mainOrig = Viewer.dteRegDate.Tag as EntityOpRegSchedulingDay;
                EntityOpRegSchedulingDay mainVo = new EntityOpRegSchedulingDay();
                mainVo.regDid = (mainOrig == null ? 0 : mainOrig.regDid);
                mainVo.regDate = Viewer.dteRegDate.Text;
                mainVo.regCode = Viewer.lueRegType.Properties.DBValue;
                mainVo.deptCode = Viewer.lueDept.Properties.DBValue;
                mainVo.roomCode = Viewer.lueRoom.Properties.DBValue;
                mainVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                mainVo.comment = Viewer.txtComment.Text.Trim();
                mainVo.status = 1; // 暂停使用，默认=1 Viewer.rdoStatus.SelectedIndex;
                if (mainOrig != null) mainVo.auditorCode = mainOrig.auditorCode;
                if (mainOrig != null) mainVo.auditDate = mainOrig.auditDate;

                #region 检测

                if (string.IsNullOrEmpty(mainVo.regCode))
                {
                    DialogBox.Msg("请选择号别.");
                    Viewer.lueRegType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.deptCode))
                {
                    DialogBox.Msg("请选择科室.");
                    Viewer.lueRegType.Focus();
                    return;
                }

                #endregion

                // 医师资料
                EntityOpRegSchedulingDoct doctVo = null;
                if (DataSourceCodeReg != null && DataSourceCodeReg.Exists(t => t.regCode == mainVo.regCode))
                {
                    if (DataSourceCodeReg.FirstOrDefault(t => t.regCode == mainVo.regCode).regFlag == "1")
                    {
                        mainVo.doctCode = Viewer.lueDeptReg.Properties.DBValue;
                        if (string.IsNullOrEmpty(mainVo.doctCode))
                        {
                            DialogBox.Msg("请选择专科.");
                            Viewer.lueDoct.Focus();
                            return;
                        }
                    }
                    else if (DataSourceCodeReg.FirstOrDefault(t => t.regCode == mainVo.regCode).regFlag == "2")
                    {
                        if (string.IsNullOrEmpty(mainVo.doctCode))
                        {
                            DialogBox.Msg("请选择医师.");
                            Viewer.lueDoct.Focus();
                            return;
                        }

                        doctVo = new EntityOpRegSchedulingDoct();
                        doctVo.doctCode = mainVo.doctCode;
                        doctVo.doctName = Viewer.lueDoct.Text;
                        doctVo.doctSkill = Viewer.txtSkill.Text.Trim();
                        doctVo.doctIntroduce = Viewer.txtIntroduce.Text.Trim();
                        if (Viewer.picPhoto.Image != null)
                            doctVo.doctPhoto = Function.ConvertImageToByte(Viewer.picPhoto.Image, 4);
                    }
                    if (string.IsNullOrEmpty(mainVo.doctCode))
                    {
                        DialogBox.Msg("请选择:医师/专科");
                        return;
                    }
                }
                else
                {
                    DialogBox.Msg("号别字典错误，请检测。");
                    return;
                }
                #endregion

                // 挂号时间设置
                List<EntityOpRegSchedulingDayDate> lstRegDate = new List<EntityOpRegSchedulingDayDate>();
                #region 挂号开放时间
                // 挂号开放时间            
                // am
                GetRegDateListDay(Viewer.teGam1.Text, Viewer.teGam2.Text, 1, 1, 0, 0, 0, ref lstRegDate);
                // pm
                GetRegDateListDay(Viewer.teGpm1.Text, Viewer.teGpm2.Text, 1, 2, 0, 0, 0, ref lstRegDate);
                // nm
                GetRegDateListDay(Viewer.teGnm1.Text, Viewer.teGnm2.Text, 1, 3, 0, 0, 0, ref lstRegDate);
                #endregion
                #region 预约开放时间
                // 预约开放时间
                // am
                GetRegDateListDay(Viewer.teBam1.Text, Viewer.teBam2.Text, 2, 1, 0, 0, 0, ref lstRegDate);
                // pm
                GetRegDateListDay(Viewer.teBpm1.Text, Viewer.teBpm2.Text, 2, 2, 0, 0, 0, ref lstRegDate);
                // nm
                GetRegDateListDay(Viewer.teBnm1.Text, Viewer.teBnm2.Text, 2, 3, 0, 0, 0, ref lstRegDate);
                #endregion
                #region 坐诊时间
                // 坐诊时间
                // am
                ResetFreqNums(Viewer.teVam1, Viewer.teVam2, Viewer.txtVamLimit, Viewer.txtFreqNumAm);
                GetRegDateListDay(Viewer.teVam1.Text, Viewer.teVam2.Text, 3, 1, Function.Int(Viewer.txtVamLimit.Text), Function.Dec(Viewer.txtFreqNumAm.Text), Viewer.cboStatusAm.SelectedIndex, ref lstRegDate);
                // pm
                ResetFreqNums(Viewer.teVpm1, Viewer.teVpm2, Viewer.txtVpmLimit, Viewer.txtFreqNumPm);
                GetRegDateListDay(Viewer.teVpm1.Text, Viewer.teVpm2.Text, 3, 2, Function.Int(Viewer.txtVpmLimit.Text), Function.Dec(Viewer.txtFreqNumPm.Text), Viewer.cboStatusPm.SelectedIndex, ref lstRegDate);
                // nm
                ResetFreqNums(Viewer.teVnm1, Viewer.teVnm2, Viewer.txtVnmLimit, Viewer.txtFreqNumNm);
                GetRegDateListDay(Viewer.teVnm1.Text, Viewer.teVnm2.Text, 3, 3, Function.Int(Viewer.txtVnmLimit.Text), Function.Dec(Viewer.txtFreqNumNm.Text), Viewer.cboStatusNm.SelectedIndex, ref lstRegDate);
                // mm
                ResetFreqNums(Viewer.teVmm1, Viewer.teVmm2, Viewer.txtVmmLimit, Viewer.txtFreqNumMm);
                GetRegDateListDay(Viewer.teVmm1.Text, Viewer.teVmm2.Text, 3, 4, Function.Int(Viewer.txtVmmLimit.Text), Function.Dec(Viewer.txtFreqNumMm.Text), Viewer.cboStatusMm.SelectedIndex, ref lstRegDate);

                #endregion

                if (lstRegDate == null || lstRegDate.Count == 0 || lstRegDate.Any(t => t.typeId == 3) == false)
                {
                    DialogBox.Msg("请维护医师出诊时间。");
                    return;
                }
                if (lstRegDate != null && lstRegDate.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDayDate item in lstRegDate)
                    {
                        item.regDid = mainVo.regDid;
                    }
                }

                // 号源设置
                List<decimal> lstAmPm = new List<decimal>();
                List<EntityOpRegSchedulingDayNumber> lstRegNumber = this.gvDataBindingSource.DataSource as List<EntityOpRegSchedulingDayNumber>;
                for (int i = lstRegNumber.Count - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(lstRegNumber[i].startTime) && !string.IsNullOrEmpty(lstRegNumber[i].endTime) &&
                         Convert.ToDateTime(lstRegNumber[i].endTime) > Convert.ToDateTime(lstRegNumber[i].startTime))
                    {
                        if (string.IsNullOrEmpty(lstRegNumber[i].amPmName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定上/下午。");
                            return;
                        }
                        lstRegNumber[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstRegNumber[i].amPmName).shiftCode;
                        lstRegNumber[i].startTime = Convert.ToDateTime(lstRegNumber[i].startTime).ToString("HH:mm");
                        lstRegNumber[i].endTime = Convert.ToDateTime(lstRegNumber[i].endTime).ToString("HH:mm");
                        lstAmPm.Add(lstRegNumber[i].amPm);
                    }
                    else
                    {
                        lstRegNumber.RemoveAt(i);
                    }
                }
                if (lstAmPm.Count > 0)
                {
                    foreach (decimal amPm in lstAmPm)
                    {
                        if (!lstRegDate.Exists(t => t.typeId == 3 && t.amPm == amPm))
                        {
                            DialogBox.Msg("号源设置的时间段上/下午 与出诊时间上/下午不一致，请检查。");
                            return;
                        }
                    }
                }
                if (lstRegNumber == null || lstRegNumber.Count == 0)
                {
                    // 默认值
                    EntityOpRegSchedulingDayNumber numVo = null;
                    List<EntityOpRegSchedulingDayDate> lstRegDate3 = lstRegDate.FindAll(t => t.typeId == 3);
                    foreach (EntityOpRegSchedulingDayDate item in lstRegDate3)
                    {
                        numVo = new EntityOpRegSchedulingDayNumber();
                        numVo.amPm = item.amPm;
                        numVo.startTime = item.startTime;
                        numVo.endTime = item.endTime;
                        numVo.normalNum = item.limitNum;
                        lstRegNumber.Add(numVo);
                    }
                }

                int ret = 0;
                decimal regDid = 0;
                bool isNew = (mainVo.regDid == 0 ? true : false);
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    ret = proxy.Service.SaveSchedulingDay(mainVo, lstRegDate, lstRegNumber, doctVo, out regDid);
                }
                if (ret > 0 && regDid > 0)
                {
                    if (this.isDayEdit && lstRegDate.Any(t => t.status == 0))
                    {
                        string amPm = string.Empty;
                        List<EntityOpRegSchedulingDayDate> lstTmp = lstRegDate.FindAll(t => t.status == 0);
                        foreach (EntityOpRegSchedulingDayDate item in lstTmp)
                        {
                            amPm += item.amPm + ",";
                        }
                        amPm = amPm.TrimEnd(',');
                        using (ProxyRegistration proxy = new ProxyRegistration())
                        {
                            #region 微信停诊通知
                            EntityHospitalPwd current = GlobalHospital.GetPassword(GlobalHospital.Current);
                            string userId = current.UserId;
                            string hcKey = current.Password;
                            string dateNow = Utils.ServerTime().ToString("yyyy-MM-dd HH:mm:ss");
                            string xmlIn = string.Empty;
                            xmlIn += "<request>" + Environment.NewLine;
                            xmlIn += "<serviceCode>" + "StopRegNotice" + "</serviceCode>" + Environment.NewLine;
                            xmlIn += "<partnerId>" + userId + "</partnerId>" + Environment.NewLine;
                            xmlIn += "<timeStamp>" + dateNow + "</timeStamp>" + Environment.NewLine;
                            xmlIn += "<password>" + ESCryptography.EncryptMD5(dateNow + hcKey).ToUpper() + "</password>" + Environment.NewLine;
                            xmlIn += "<deptId>" + mainVo.deptCode + "</deptId>" + Environment.NewLine;
                            xmlIn += "<doctorId>" + mainVo.doctCode + "</doctorId>" + Environment.NewLine;
                            xmlIn += "<startDate>" + mainVo.regDate + "</startDate>" + Environment.NewLine;
                            xmlIn += "<endDate>" + mainVo.regDate + "</endDate>" + Environment.NewLine;
                            xmlIn += "<timeFlag>" + "4" + "</timeFlag>" + Environment.NewLine;
                            xmlIn += "<startTime>" + "" + "</startTime>" + Environment.NewLine;
                            xmlIn += "<endTime>" + "" + "</endTime>" + Environment.NewLine;
                            xmlIn += "<stopDesc>" + "􀀓􀄆􀀿􀆬" + "</stopDesc>" + Environment.NewLine;
                            xmlIn += "<personalFirst> </personalFirst>" + Environment.NewLine;
                            xmlIn += "<personalSecond> </personalSecond>" + Environment.NewLine;
                            xmlIn += "<personalThird></personalThird>" + Environment.NewLine;
                            xmlIn += "<personalForth></personalForth>" + Environment.NewLine;
                            xmlIn += "</request>" + Environment.NewLine;

                            string xmlOut = proxy.Service.WeChatStop(regDid, amPm, xmlIn);
                            if (!string.IsNullOrEmpty(xmlOut))
                            {
                                DialogBox.Msg(xmlOut);
                            }
                            #endregion

                            #region 91160停诊通知

                            xmlIn = string.Empty;
                            xmlIn += "<Request>" + Environment.NewLine;
                            xmlIn += "<deptCode>" + mainVo.deptCode + "</deptCode>" + Environment.NewLine;
                            xmlIn += "<doctorCode>" + mainVo.doctCode + "</doctorCode>" + Environment.NewLine;
                            xmlIn += "<scheduleDate>" + mainVo.regDate + "</scheduleDate>" + Environment.NewLine;
                            xmlIn += "<time_type>" + "3" + "</time_type>" + Environment.NewLine;
                            xmlIn += "</Request>" + Environment.NewLine;
                            xmlOut = proxy.Service.WebStop(regDid, amPm, xmlIn);
                            if (!string.IsNullOrEmpty(xmlOut))
                            {
                                DialogBox.Msg(xmlOut);
                            }

                            #endregion
                        }
                    }

                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        foreach (EntityOpRegSchedulingDayNumber item in lstRegNumber)
                        { item.regDid = regDid; }
                        this.gvDataBindingSource.DataSource = lstRegNumber;
                        Viewer.gcNo.RefreshDataSource();
                    }
                    mainVo.regDid = regDid;
                    Viewer.dteRegDate.Tag = mainVo;
                    Viewer.IsSave = true;
                    if (isNew)
                    {
                        SetTreeDataSource(mainVo);   // 刷新树
                        Viewer.tvDate.SetFocusedNode(Viewer.tvDate.FindNodeByKeyID(mainVo.regDid));
                    }
                    else
                    {
                        if (Viewer.tvDate.FocusedNode != null)
                        {
                            EntityOpRegSchedulingDay tempVo = (EntityOpRegSchedulingDay)Viewer.tvDate.GetDataRecordByNode(Viewer.tvDate.FocusedNode);
                            if (tempVo.regDid == mainVo.regDid)
                            {
                                if (tempVo.regDate != mainVo.regDate)
                                {
                                    tempVo.regDate = mainVo.regDate;
                                    if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                    {
                                        if (!string.IsNullOrEmpty(tempVo.regCode))
                                        {
                                            if (DataSourceCodeReg.Any(t => t.regCode.Trim() == tempVo.regCode.Trim()))
                                            {
                                                tempVo.regDate += " " + DataSourceCodeReg.FirstOrDefault(t => t.regCode.Trim() == tempVo.regCode.Trim()).regName.Replace("号", "").Trim();
                                            }
                                        }
                                    }
                                }
                                if (tempVo.status != mainVo.status)
                                {
                                    tempVo.status = mainVo.status;
                                }
                            }
                        }
                    }
                    Viewer.ValueChanged = false;
                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    DialogBox.Msg("保存失败。");
                }
                #endregion
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal void Delete()
        {
            if (this.EditType == 1)
            {
                #region 排班计划
                EntityOpRegScheduling mainOrig = Viewer.cboWeek.Tag as EntityOpRegScheduling;
                if (mainOrig == null)
                {
                    Clear();
                    return;
                }
                if (DialogBox.Msg("确定是否删除？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.DelScheduling(mainOrig.regWid);
                    proxy = null;
                    if (ret > 0)
                    {
                        Viewer.ValueChanged = false;
                        Clear();
                        DialogBox.Msg("删除排班计划成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除排班计划失败。");
                    }
                }
                #endregion
            }
            else if (this.EditType == 2)
            {
                #region 日排班
                if (Viewer.tvDate.FocusedNode == null)
                {
                    DialogBox.Msg("请选择左侧日排班记录。");
                    return;
                }
                EntityOpRegSchedulingDay mainOrig = Viewer.dteRegDate.Tag as EntityOpRegSchedulingDay;
                if (mainOrig == null)
                {
                    Clear();
                    return;
                }
                EntityOpRegSchedulingDay tempVo = (EntityOpRegSchedulingDay)Viewer.tvDate.GetDataRecordByNode(Viewer.tvDate.FocusedNode);
                if (tempVo.regDid != mainOrig.regDid)
                {
                    DialogBox.Msg("请选择左侧日排班记录。");
                    return;
                }
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    if (proxy.Service.CheckSchedulingDayIsReg(new List<decimal>() { mainOrig.regDid }))
                    {
                        DialogBox.Msg("该日排班记录已被预约，不能删除。");
                        return;
                    }
                }
                if (DialogBox.Msg("确定是否删除？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.DelSchedulingDay(mainOrig.regDid);
                    proxy = null;
                    if (ret > 0)
                    {
                        Viewer.tvDate.Nodes.Remove(Viewer.tvDate.FocusedNode);
                        Viewer.ValueChanged = false;
                        Clear();
                        DialogBox.Msg("删除日排班记录成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除日排班记录失败。");
                    }
                }
                #endregion
            }
        }
        #endregion

        #region LueFilter

        #region LueFilterRegType
        /// <summary>
        /// LueFilterRegType
        /// </summary>
        internal void LueFilterRegType()
        {
            bool isExpert = false;
            string regCode = Viewer.lueRegType.Properties.DBValue;
            if (!string.IsNullOrEmpty(regCode))
            {
                if (DataSourceCodeReg != null && DataSourceCodeReg.Exists(t => t.regCode == regCode))
                {
                    if (DataSourceCodeReg.FirstOrDefault(t => t.regCode == regCode).regFlag == "1")
                    {
                        isExpert = true;
                    }
                }
            }
            Viewer.txtRank.Text = string.Empty;
            SetExpert(isExpert);
        }
        #endregion

        #region SetExpert
        /// <summary>
        /// SetExpert
        /// </summary>
        /// <param name="isExpert"></param>
        void SetExpert(bool isExpert)
        {
            if (isExpert)
            {
                Viewer.lblDoct.Text = "专科:";
                Viewer.lueDeptReg.Visible = true;
                Viewer.lueDoct.Visible = false;
            }
            else
            {
                Viewer.lblDoct.Text = "医师:";
                Viewer.lueDeptReg.Visible = false;
                Viewer.lueDoct.Visible = true;
            }
        }
        #endregion

        #region LueFilterRoom
        /// <summary>
        /// LueFilterRoom
        /// </summary>
        internal void LueFilterRoom()
        {
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                List<EntityDicDeptRoom> data = DataSourceRoom.FindAll(t => t.deptCode == deptCode);
                if (data != null)
                {
                    Viewer.lueRoom.Properties.DataSource = data.ToArray();
                    Viewer.lueRoom.Properties.SetSize();
                }
            }
        }
        #endregion

        #region LueFilterDoct
        /// <summary>
        /// LueFilterDoct
        /// </summary>
        internal void LueFilterDoct()
        {
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                //var doct = from var1 in GlobalDic.DataSourceDoctor
                //           join var2 in GlobalDic.DataSourceEmpDept on var1.operCode equals var2.operCode
                //           where (var2.deptCode == deptCode)
                //           select new { var1.operCode, var1.operName, var2.pyCode, var2.wbCode };
                //var doct = GlobalDic.DataSourceDoctor.FindAll(t => t.DeptNo == deptCode);
                var doct = from var1 in GlobalDic.DataSourceDoctor
                           join var2 in GlobalDic.DataSourceDefDeptEmployee on var1.operCode equals var2.operCode
                           where (var2.deptCode == deptCode)
                           select new { var1.operCode, var1.operName, var2.pyCode, var2.wbCode };
                if (doct != null)
                {
                    EntityCodeOperator vo = null;
                    List<EntityCodeOperator> tmpData = new List<EntityCodeOperator>();
                    foreach (var item in doct)
                    {
                        vo = new EntityCodeOperator();
                        vo.operCode = item.operCode;
                        vo.operName = item.operName;
                        vo.pyCode = item.pyCode;
                        vo.wbCode = item.wbCode;
                        tmpData.Add(vo);
                    }
                    Viewer.lueDoct.Properties.DataSource = tmpData.ToArray();
                    Viewer.lueDoct.Properties.SetSize();
                }
            }
        }
        #endregion

        #region LueFilterExpert
        /// <summary>
        /// LueFilterExpert
        /// </summary>
        internal void LueFilterExpert()
        {
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                List<EntityDicDeptReg> data = DataSourceDeptReg.FindAll(t => t.deptCode == deptCode);
                if (data != null)
                {
                    Viewer.lueDeptReg.Properties.DataSource = data.ToArray();
                    Viewer.lueDeptReg.Properties.SetSize();
                }
            }
        }
        #endregion

        #region LueFilterRank
        /// <summary>
        /// LueFilterRank
        /// </summary>
        internal void LueFilterRank()
        {
            string doctCode = Viewer.lueDoct.Properties.DBValue;
            if (!string.IsNullOrEmpty(doctCode))
            {
                string rankCode = GlobalDic.DataSourceEmpDept.FirstOrDefault(t => t.operCode.ToUpper() == doctCode.ToUpper()).rankCode;
                if (GlobalDic.DataSourceRank.Any(t => t.rankCode == rankCode))
                {
                    Viewer.txtRank.Text = GlobalDic.DataSourceRank.FirstOrDefault(t => t.rankCode == rankCode).rankName;
                }
            }
        }
        #endregion

        #region LueFilterDoctInfo
        /// <summary>
        /// LueFilterDoctInfo
        /// </summary>
        internal void LueFilterDoctInfo()
        {
            string doctCode = Viewer.lueDoct.Properties.DBValue;
            if (!string.IsNullOrEmpty(doctCode))
            {
                EntityOpRegSchedulingDoct doctVo = null;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    doctVo = proxy.Service.GetDoctPlusInfo(doctCode);
                }
                SetDoctPlusInfo(doctVo);
            }
        }
        #endregion

        #region SetDoctPlusInfo
        /// <summary>
        /// SetDoctPlusInfo
        /// </summary>
        /// <param name="doctVo"></param>
        internal void SetDoctPlusInfo(EntityOpRegSchedulingDoct doctVo)
        {
            if (doctVo == null)
            {
                Viewer.picPhoto.Image = Properties.Resources.unknow;
                Viewer.txtSkill.Text = string.Empty;
                Viewer.txtIntroduce.Text = string.Empty;
            }
            else
            {
                if (doctVo.doctPhoto != null)
                    Viewer.picPhoto.Image = Function.ConvertByteToImage(doctVo.doctPhoto);
                else
                    Viewer.picPhoto.Image = Properties.Resources.unknow;
                Viewer.txtSkill.Text = doctVo.doctSkill;
                Viewer.txtIntroduce.Text = doctVo.doctIntroduce;
            }
        }
        #endregion

        #endregion

        #region SetCellStyle
        /// <summary>
        /// SetCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            if (fieldName == EntityOpRegSchedulingNumber.Columns.normalNum || fieldName == EntityOpRegSchedulingNumber.Columns.webNum ||
                fieldName == EntityOpRegSchedulingNumber.Columns.telNum || fieldName == EntityOpRegSchedulingNumber.Columns.localNum ||
                fieldName == EntityOpRegSchedulingNumber.Columns.othNum || fieldName == EntityOpRegSchedulingNumber.Columns.weNum)
            {
                if (Function.Dec(GetFieldValueStr(Viewer.gvNo, e.RowHandle, fieldName)) == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
        }
        #endregion

        #region ReLoadRegDay
        /// <summary>
        /// 重新加载日排班记录
        /// </summary>
        internal void ReLoadRegDay()
        {
            if (this.EditType == 2 && this.RegId > 0 && !string.IsNullOrEmpty(this.preDeptCode) && !this.isLoad)
            {
                string currRegDate = Viewer.dteRegDate.Text;
                if (!currRegDate.Equals(this.preRegDate))
                {
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        this.RegId = proxy.Service.GetSchedulingDayId(currRegDate, this.preRegCode, this.preDeptCode, this.preDoctCode);
                        if (this.RegId > 0)
                        {
                            Load();
                        }
                        else
                        {
                            Clear();
                            DialogBox.Msg("该日期没有排班记录。");
                        }
                    }
                }
            }
        }
        #endregion

        #region CustomDrawNodeCell
        /// <summary>
        /// CustomDrawNodeCell
        /// </summary>
        /// <param name="e"></param>
        internal void CustomDrawNodeCell(DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node != null)
            {
                EntityOpRegSchedulingDay vo = (EntityOpRegSchedulingDay)Viewer.tvDate.GetDataRecordByNode(e.Node);
                if (vo.regParent != -99)
                {
                    if (vo.status == 0)
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.BackColor2 = Color.White;
                    }
                    //else
                    //{
                    //    e.Appearance.BackColor = Color.White;
                    //    e.Appearance.BackColor2 = Color.White;
                    //}
                }
            }
        }
        #endregion

        #endregion

    }
}
