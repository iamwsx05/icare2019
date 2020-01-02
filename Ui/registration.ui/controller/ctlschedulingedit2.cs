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
    public class ctlSchedulingEdit2 : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmSchedulingEdit2 Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmSchedulingEdit2)child;
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
        /// RegDate
        /// </summary>
        internal string RegDate { get; set; }

        /// <summary>
        /// DeptCode
        /// </summary>
        internal string DeptCode { get; set; }

        /// <summary>
        /// RoomCode
        /// </summary>
        internal string RoomCode { get; set; }

        /// <summary>
        /// DoctCode
        /// </summary>
        internal string DoctCode { get; set; }

        /// <summary>
        /// 号别字典
        /// </summary>
        List<EntityCodeReg> DataSourceCodeReg { get; set; }

        /// <summary>
        /// 诊室字典
        /// </summary>
        List<EntityDicDeptRoom> DataSourceRoom { get; set; }

        /// <summary>
        /// 班次字典
        /// </summary>
        List<EntityDicShift> DataSourceShift { get; set; }

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
        /// 号源.出诊时间
        /// </summary>
        //List<EntityOpRegSchedulingDatePlus> DataSourceRegDate { get; set; }
        BindingListView<EntityOpRegSchedulingDatePlus> DataSourceRegDate { get; set; }

        /// <summary>
        /// 绑定.出诊时间
        /// </summary>
        BindingSource BindRegDate { get; set; }

        /// <summary>
        /// load状态
        /// </summary>
        bool isLoad { get; set; }

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
                this.BindRegDate = new BindingSource();
                Viewer.gcDate.DataSource = this.BindRegDate;

                InitLue();
                InitRegDate();
                InitNumber();

                #region setVisible

                if (EditType == 1)
                {
                    Viewer.plTop.Visible = false;
                    Viewer.tvDate.Visible = false;
                    Viewer.splitterControl1.Visible = false;
                    Viewer.colStatusName.Visible = false;
                    Viewer.Width -= (Viewer.tvDate.Width + Viewer.splitterControl1.Width);
                    Viewer.blbiAddNo.Visibility = BarItemVisibility.Never;
                    Viewer.blbiDoctServcie.Visibility = BarItemVisibility.Never;
                }
                else if (EditType == 2)
                {
                    Viewer.colWeekIdName.Visible = false;
                    Viewer.colWeekIdName2.Visible = false;
                    Viewer.btnCopy.Visible = false;
                    CreateTree();
                }
                #endregion

                Load();
                SetEditValueChangedEvent(Viewer.pcBackGround);
                SetEditValueChangedEvent(Viewer.gvDate);
                SetEditValueChangedEvent(Viewer.gvNo);

                this.LueFilterRoom();
                this.LueFilterDoct();
                Viewer.Location = new Point(Viewer.Location.X, 0);
                Viewer.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
            finally
            {
                Viewer.ValueChanged = false;
                isInit = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region InitRegDate
        /// <summary>
        /// InitRegDate
        /// </summary>
        void InitRegDate()
        {
            DataSourceRegDate = new BindingListView<EntityOpRegSchedulingDatePlus>();
            if (this.EditType == 1)
            {
                EntityOpRegSchedulingDatePlus vo = null;
                for (int i = 0; i < 7; i++)
                {
                    vo = new EntityOpRegSchedulingDatePlus();
                    //vo.weekId = i;
                    //vo.weekIdName = Function.GetWeekName(i);
                    DataSourceRegDate.Add(vo);
                }
            }
            else if (this.EditType == 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    DataSourceRegDate.Add(new EntityOpRegSchedulingDatePlus());
                }
            }
            this.BindRegDate.DataSource = DataSourceRegDate;
            //Viewer.gvDate.FocusedRowHandle = 0;
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
            //Viewer.gvNo.FocusedRowHandle = 0;
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
                dt = proxy.Service.SelectFullTable(new EntityDicDeptRoom());
                DataSourceRoom.AddRange(EntityTools.ConvertToEntityList<EntityDicDeptRoom>(dt));

                dt = proxy.Service.SelectFullTable(new EntityDicShift());
                DataSourceShift.AddRange(EntityTools.ConvertToEntityList<EntityDicShift>(dt));
            }
            #endregion

            #region lue

            #region 号别
            //Viewer.lueRegType.Properties.PopupWidth = 130;
            //Viewer.lueRegType.Properties.PopupHeight = 200;
            //Viewer.lueRegType.Properties.ValueColumn = EntityCodeReg.Columns.regCode;
            //Viewer.lueRegType.Properties.DisplayColumn = EntityCodeReg.Columns.regName;
            //Viewer.lueRegType.Properties.Essential = false;
            //Viewer.lueRegType.Properties.IsShowColumnHeaders = true;
            //Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regCode, 45);
            //Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regName, 85);
            //Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regCode, "编码");
            //Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regName, "名称");
            //Viewer.lueRegType.Properties.ShowColumn = EntityCodeReg.Columns.regCode + "|" + EntityCodeReg.Columns.regName;
            //Viewer.lueRegType.Properties.IsUseShowColumn = true;
            //if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0) Viewer.lueRegType.Properties.DataSource = DataSourceCodeReg.ToArray();
            //Viewer.lueRegType.Properties.SetSize();
            #endregion

            #region 科室
            Viewer.lueDept.Properties.PopupWidth = 176;
            Viewer.lueDept.Properties.PopupHeight = 300;
            Viewer.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            Viewer.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            Viewer.lueDept.Properties.Essential = false;
            Viewer.lueDept.Properties.IsShowColumnHeaders = true;
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 116);
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
            Viewer.lueRoom.Properties.PopupWidth = 260;
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
            Viewer.lueDoct.Properties.PopupWidth = 176;
            Viewer.lueDoct.Properties.PopupHeight = 250;
            Viewer.lueDoct.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            Viewer.lueDoct.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.Essential = false;
            Viewer.lueDoct.Properties.IsShowColumnHeaders = true;
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 70);
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 106);
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "编码");
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "名称");
            Viewer.lueDoct.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.IsUseShowColumn = true;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #endregion

            #region grid.lue

            this.LookUpEditFieldName.Clear();
            LookUpEditContainer cell = null;
            this.MatchColFieldName = new Dictionary<string, string>();
            this.MatchColFieldName.Add(EntityDicShift.Columns.shiftCode, EntityOpRegSchedulingDatePlus.Columns.amPmName);
            this.MatchColFieldName.Add(EntityCodeReg.Columns.regCode, EntityOpRegSchedulingDatePlus.Columns.regCodeName);
            this.MatchColFieldName.Add(EntityOpRegSchedulingDatePlus.Columns.amPm, EntityOpRegSchedulingDatePlus.Columns.amPmName);
            this.TextColFieldName = new List<string>();
            this.ValueColFieldName = new List<string>();
            this.LookUpEditFieldName.Add(EntityOpRegSchedulingDatePlus.Columns.amPmName, EntityOpRegSchedulingDatePlus.Columns.amPm);
            this.LookUpEditFieldName.Add(EntityOpRegSchedulingDatePlus.Columns.regCodeName, EntityOpRegSchedulingDatePlus.Columns.regCode);
            this.ResetColFieldName = EntityOpRegSchedulingDatePlus.Columns.amPmName;

            // 1.
            cell = new LookUpEditContainer(150, 160);
            cell.FieldName = EntityOpRegSchedulingDatePlus.Columns.regCode;
            cell.ValueColumn = EntityCodeReg.Columns.regCode;
            cell.DisplayColumn = EntityCodeReg.Columns.regName;
            cell.Essential = true;
            cell.IsShowColumnHeaders = true;
            cell.ColumnWidth.Add(EntityCodeReg.Columns.regCode, 60);
            cell.ColumnWidth.Add(EntityCodeReg.Columns.regName, 90);
            cell.ColumnHeaders.Add(EntityCodeReg.Columns.regCode, "编码");
            cell.ColumnHeaders.Add(EntityCodeReg.Columns.regName, "号别");
            cell.ShowColumn = EntityCodeReg.Columns.regCode + "|" + EntityCodeReg.Columns.regName;
            cell.IsUseShowColumn = true;
            cell.ParentGridView = Viewer.gvDate;
            cell.ParentBindingSource = this.BindRegDate;
            cell.Enter += new EventHandler(cell_Enter);
            cell.KeyDown += new KeyEventHandler(cell_KeyDown);
            cell.HandleResetDBValue += new _HandleResetDBValue(cell_HandleResetDBValue);
            cell.HandleDBValueChanged += new _HandleDBValueChanged(cell_HandleDBValueChanged);
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
            {
                cell.DataSource = DataSourceCodeReg.ToArray();
            }
            Viewer.colRegCode.ColumnEdit = cell;

            // 2.
            cell = new LookUpEditContainer(150, 200);
            cell.FieldName = EntityOpRegSchedulingDatePlus.Columns.amPm;
            cell.ValueColumn = EntityDicShift.Columns.shiftCode;
            cell.DisplayColumn = EntityDicShift.Columns.shiftName;
            cell.Essential = true;
            cell.IsShowColumnHeaders = true;
            cell.ColumnWidth.Add(EntityDicShift.Columns.shiftCode, 60);
            cell.ColumnWidth.Add(EntityDicShift.Columns.shiftName, 90);
            cell.ColumnHeaders.Add(EntityDicShift.Columns.shiftCode, "编码");
            cell.ColumnHeaders.Add(EntityDicShift.Columns.shiftName, "班次");
            cell.ShowColumn = EntityDicShift.Columns.shiftCode + "|" + EntityDicShift.Columns.shiftName;
            cell.IsUseShowColumn = true;
            cell.ParentGridView = Viewer.gvDate;
            cell.ParentBindingSource = this.BindRegDate;
            cell.Enter += new EventHandler(cell_Enter);
            cell.KeyDown += new KeyEventHandler(cell_KeyDown);
            cell.HandleResetDBValue += new _HandleResetDBValue(cell_HandleResetDBValue);
            cell.HandleDBValueChanged += new _HandleDBValueChanged(cell_HandleDBValueChanged);
            if (DataSourceShift != null && DataSourceShift.Count > 0)
            {
                cell.DataSource = DataSourceShift.ToArray();
            }
            Viewer.colShift.ColumnEdit = cell;

            // 3.
            cell = new LookUpEditContainer(150, 160);
            cell.FieldName = EntityOpRegSchedulingDatePlus.Columns.regCode;
            cell.ValueColumn = EntityCodeReg.Columns.regCode;
            cell.DisplayColumn = EntityCodeReg.Columns.regName;
            cell.Essential = true;
            cell.IsShowColumnHeaders = true;
            cell.ColumnWidth.Add(EntityCodeReg.Columns.regCode, 60);
            cell.ColumnWidth.Add(EntityCodeReg.Columns.regName, 90);
            cell.ColumnHeaders.Add(EntityCodeReg.Columns.regCode, "编码");
            cell.ColumnHeaders.Add(EntityCodeReg.Columns.regName, "号别");
            cell.ShowColumn = EntityCodeReg.Columns.regCode + "|" + EntityCodeReg.Columns.regName;
            cell.IsUseShowColumn = true;
            cell.ParentGridView = Viewer.gvNo;
            cell.ParentBindingSource = this.gvDataBindingSource;
            cell.Enter += new EventHandler(cell_Enter);
            cell.KeyDown += new KeyEventHandler(cell_KeyDown);
            cell.HandleResetDBValue += new _HandleResetDBValue(cell_HandleResetDBValue);
            cell.HandleDBValueChanged += new _HandleDBValueChanged(cell_HandleDBValueChanged);
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
            {
                cell.DataSource = DataSourceCodeReg.ToArray();
            }
            Viewer.colRegCodeName2.ColumnEdit = cell;

            // 4.
            cell = new LookUpEditContainer(150, 200);
            cell.FieldName = EntityOpRegSchedulingNumber.Columns.amPm;
            cell.ValueColumn = EntityDicShift.Columns.shiftCode;
            cell.DisplayColumn = EntityDicShift.Columns.shiftName;
            cell.Essential = true;
            cell.IsShowColumnHeaders = true;
            cell.ColumnWidth.Add(EntityDicShift.Columns.shiftCode, 60);
            cell.ColumnWidth.Add(EntityDicShift.Columns.shiftName, 90);
            cell.ColumnHeaders.Add(EntityDicShift.Columns.shiftCode, "编码");
            cell.ColumnHeaders.Add(EntityDicShift.Columns.shiftName, "班次");
            cell.ShowColumn = EntityDicShift.Columns.shiftCode + "|" + EntityDicShift.Columns.shiftName;
            cell.IsUseShowColumn = true;
            cell.ParentGridView = Viewer.gvNo;
            cell.ParentBindingSource = this.gvDataBindingSource;
            cell.Enter += new EventHandler(cell_Enter);
            cell.KeyDown += new KeyEventHandler(cell_KeyDown);
            cell.HandleResetDBValue += new _HandleResetDBValue(cell_HandleResetDBValue);
            cell.HandleDBValueChanged += new _HandleDBValueChanged(cell_HandleDBValueChanged);
            if (DataSourceShift != null && DataSourceShift.Count > 0)
            {
                cell.DataSource = DataSourceShift.ToArray();
            }
            Viewer.colAmPmName.ColumnEdit = cell;

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
                this.DeptCode = mainVo.deptCode;
                this.RoomCode = mainVo.roomCode;
                this.DoctCode = mainVo.doctCode;
                this.RegDate = mainVo.regDate;
                if (!string.IsNullOrEmpty(this.DeptCode) && !string.IsNullOrEmpty(this.DoctCode) && !string.IsNullOrEmpty(this.RegDate))
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
            Viewer.lblDoct.Tag = null;
            Viewer.picPhoto.Image = null;
            Viewer.lueDept.Properties.ReadOnly = false;
            Viewer.lueRoom.Properties.ReadOnly = false;
            Viewer.lueDoct.Properties.ReadOnly = false;

            Viewer.lueDept.Properties.DBValue = string.Empty;
            Viewer.lueDept.Text = string.Empty;
            Viewer.lueRoom.Properties.DBValue = string.Empty;
            Viewer.lueRoom.Text = string.Empty;
            Viewer.lueDoct.Properties.DBValue = string.Empty;
            Viewer.lueDoct.Text = string.Empty;

            Viewer.txtIntroduce.Text = string.Empty;
            Viewer.txtSkill.Text = string.Empty;
            Viewer.txtRank.Text = string.Empty;
            Viewer.blbiSave.Enabled = true;

            InitRegDate();
            InitNumber();
            Viewer.ValueChanged = false;
        }
        #endregion

        #region AddRegDateRow
        /// <summary>
        /// AddRegDateRow
        /// </summary>
        internal void AddRegDateRow()
        {
            AppendRow(this.BindRegDate);
        }
        #endregion

        #region DelRegDateRow
        /// <summary>
        /// DelRegDateRow
        /// </summary>
        internal void DelRegDateRow()
        {
            if (Viewer.gvDate.FocusedRowHandle < 0) return;
            DeleteRow(Viewer.gvDate, this.BindRegDate, Viewer.gvDate.FocusedRowHandle);
        }
        #endregion

        #region DelRegDateRowAll
        /// <summary>
        /// DelRegDateRowAll
        /// </summary>
        internal void DelRegDateRowAll()
        {
            if (DialogBox.Msg("是否删除全部的出诊时间？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.BindRegDate.Clear();
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
            if (Viewer.gvNo.FocusedRowHandle < 0) return;
            DeleteRow(Viewer.gvNo, this.gvDataBindingSource, Viewer.gvNo.FocusedRowHandle);
        }
        #endregion

        #region DelNumberRowAll
        /// <summary>
        /// DelNumberRowAll
        /// </summary>
        internal void DelNumberRowAll()
        {
            if (DialogBox.Msg("是否删除全部的分时号源？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.gvDataBindingSource.Clear();
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            Clear();
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

            Viewer.tvDate.BeginUpdate();
            Viewer.tvDate.DataSource = dataSource;
            Viewer.tvDate.RefreshDataSource();
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
                List<EntityOpRegSchedulingDatePlus> lstPlus = null;
                EntityOpRegSchedulingDoct doctVo = null;
                if (this.EditType == 1 && !string.IsNullOrEmpty(DeptCode) && !string.IsNullOrEmpty(DoctCode))
                {
                    #region 排班计划
                    List<EntityOpRegSchedulingNumber> lstRegNumber = null;
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        proxy.Service.GetScheduling(this.DeptCode, this.RoomCode, this.DoctCode, out lstPlus, out lstRegNumber, out doctVo);
                    }
                    SetLueDept(this.DeptCode);
                    SetLueRoom(this.DeptCode, this.RoomCode);

                    // 挂到医师.医师信息
                    if (doctVo != null && !string.IsNullOrEmpty(doctVo.doctCode))
                    {
                        SetLueDoct(this.DoctCode);
                        SetDoctPlusInfo(doctVo);
                        LueFilterRank();
                    }
                    #region 时间设定
                    // 时间设定
                    DataSourceRegDate = new BindingListView<EntityOpRegSchedulingDatePlus>();
                    if (lstPlus != null && lstPlus.Count > 0) DataSourceRegDate.AddRange(lstPlus);
                    foreach (EntityOpRegSchedulingDatePlus item in DataSourceRegDate)
                    {
                        item.CopyOriginalValue();
                        item.CloneObject = item.Clone();
                    }
                    this.BindRegDate.DataSource = DataSourceRegDate;
                    #endregion

                    // 号源
                    this.gvDataBindingSource.DataSource = lstRegNumber;
                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        Viewer.gvNo.FocusedColumn = Viewer.colNormalNo;
                    }
                    #endregion
                }
                else if (this.EditType == 2 && !string.IsNullOrEmpty(DeptCode) && !string.IsNullOrEmpty(DoctCode))
                {
                    #region 日排班
                    List<EntityOpRegSchedulingDayNumber> lstRegNumber = null;
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        proxy.Service.GetSchedulingDay(this.RegDate, this.DeptCode, this.RoomCode, this.DoctCode, out lstPlus, out lstRegNumber, out doctVo);
                    }
                    Viewer.dteRegDate.Text = this.RegDate;
                    SetLueDept(this.DeptCode);
                    SetLueRoom(this.DeptCode, this.RoomCode);
                    SetLueDoct(this.DoctCode);
                    SetDoctPlusInfo(doctVo);
                    LueFilterRank();

                    // 刷新树
                    if (isInit)
                    {
                        List<EntityOpRegSchedulingDay> dataSourceTree = new List<EntityOpRegSchedulingDay>();
                        using (ProxyScheduling proxy = new ProxyScheduling())
                        {
                            List<EntityOpRegSchedulingDay> tmpData = proxy.Service.GetDayRegListByDoct(this.DeptCode, this.DoctCode);
                            foreach (EntityOpRegSchedulingDay item in tmpData)
                            {
                                if (!dataSourceTree.Exists(t => t.regDate == item.regDate && t.deptCode == item.deptCode && t.roomCode.Trim() == item.roomCode.Trim() && t.doctCode == item.doctCode))
                                {
                                    dataSourceTree.Add(item);
                                }
                            }
                        }
                        Viewer.tvDate.DataSource = null;
                        SetTreeDataSource(dataSourceTree);
                        Viewer.tvDate.SetFocusedNode(Viewer.tvDate.FindNodeByFieldValue("regDate", this.RegDate));
                    }

                    #region 时间设定
                    // 时间设定
                    DataSourceRegDate = new BindingListView<EntityOpRegSchedulingDatePlus>();
                    if (lstPlus != null && lstPlus.Count > 0) DataSourceRegDate.AddRange(lstPlus);
                    foreach (EntityOpRegSchedulingDatePlus item in DataSourceRegDate)
                    {
                        item.CopyOriginalValue();
                        item.CloneObject = item.Clone();
                    }
                    this.BindRegDate.DataSource = DataSourceRegDate;
                    #endregion

                    // 号源
                    this.gvDataBindingSource.DataSource = lstRegNumber;
                    if (lstRegNumber != null && lstRegNumber.Count > 0)
                    {
                        Viewer.gvNo.FocusedColumn = Viewer.colNormalNo;
                    }
                    #endregion
                }
            }
            finally
            {
                Viewer.ValueChanged = false;
                this.isLoad = false;
            }
        }

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
                teFreq.Text = Math.Ceiling(Convert.ToDecimal(ts.TotalMinutes / limitNum)).ToString();
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
            Viewer.gvDate.CloseEditor();
            Viewer.gvNo.CloseEditor();

            if (this.EditType == 1)
            {
                #region 排班计划

                #region 主信息
                // 主信息
                EntityOpRegScheduling mainVo = new EntityOpRegScheduling();
                mainVo.deptCode = Viewer.lueDept.Properties.DBValue;
                mainVo.roomCode = Viewer.lueRoom.Properties.DBValue;
                mainVo.doctCode = Viewer.lueDoct.Properties.DBValue;

                #region 检测

                if (string.IsNullOrEmpty(mainVo.deptCode))
                {
                    DialogBox.Msg("请选择科室.");
                    Viewer.lueDept.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.roomCode))
                {
                    DialogBox.Msg("请选择诊室.");
                    Viewer.lueRoom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.doctCode))
                {
                    DialogBox.Msg("请选择医生.");
                    Viewer.lueDoct.Focus();
                    return;
                }

                #endregion

                // 医师资料
                EntityOpRegSchedulingDoct doctVo = new EntityOpRegSchedulingDoct();
                doctVo.doctCode = mainVo.doctCode;
                doctVo.doctName = Viewer.lueDoct.Text;
                doctVo.doctSkill = Viewer.txtSkill.Text.Trim();
                doctVo.doctIntroduce = Viewer.txtIntroduce.Text.Trim();
                if (Viewer.picPhoto.Image != null)
                {
                    doctVo.doctPhoto = Function.ConvertImageToByte(Viewer.picPhoto.Image, 4);
                }
                #endregion

                // 挂号时间设置
                if (DataSourceRegDate == null || DataSourceRegDate.Count == 0)
                {
                    DialogBox.Msg("请维护医师出诊时间。");
                    return;
                }

                string startTime = string.Empty;
                string endTime = string.Empty;
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                List<decimal> lstWid = new List<decimal>();
                BindingListView<EntityOpRegSchedulingDatePlus> lstPlus = this.BindRegDate.DataSource as BindingListView<EntityOpRegSchedulingDatePlus>;
                List<EntityOpRegSchedulingDatePlus> lstPlusAll = new List<EntityOpRegSchedulingDatePlus>();
                for (int i = lstPlus.Count - 1; i >= 0; i--)
                {
                    if (lstPlus[i].regWid > 0 && lstWid.IndexOf(lstPlus[i].regWid) < 0) lstWid.Add(lstPlus[i].regWid);
                    if (!string.IsNullOrEmpty(lstPlus[i].weekIdName) && !string.IsNullOrEmpty(lstPlus[i].regCode) && !string.IsNullOrEmpty(lstPlus[i].amPmName))
                    {
                        if (lstPlus[i].limitNum <= 0)
                        {
                            DialogBox.Msg("医师出诊表格里请指定限号数量。");
                            return;
                        }
                        lstPlus[i].weekId = Function.GetWeekIndex(lstPlus[i].weekIdName);
                        lstPlus[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstPlus[i].amPmName).shiftCode;
                        startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == lstPlus[i].amPm).startTime;
                        endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == lstPlus[i].amPm).endTime;
                        TimeSpan ts = Convert.ToDateTime(date + " " + endTime).Subtract(Convert.ToDateTime(date + " " + startTime));
                        if (ts.TotalMinutes > 0)
                        {
                            lstPlus[i].freqNum = Math.Ceiling(Convert.ToDecimal((decimal)ts.TotalMinutes / lstPlus[i].limitNum));
                        }
                    }
                    else
                    {
                        lstPlus.RemoveAt(i);
                        continue;
                    }
                    if (lstPlus[i].regWid > 0)
                    {
                        lstPlusAll.Add(lstPlus[i]);
                    }
                    else
                    {
                        // 20171228重复新建判断
                        using (ProxyScheduling proxy = new ProxyScheduling())
                        {
                            if (proxy.Service.IsExistScheShiftRec(mainVo.doctCode, lstPlus[i].weekId, lstPlus[i].amPm))
                            {
                                //DialogBox.Msg(lstPlus[i].weekIdName + " " + lstPlus[i].amPmName + " 的排班记录已存在,不能再新建,请检查。");
                                //return;
                                // 2018-07-02 
                                DialogBox.Msg(lstPlus[i].weekIdName + " " + lstPlus[i].amPmName + " 的排班记录已存在,请特别注意。");
                            }
                        }
                    }
                }
                // 检测重复项
                List<EntityOpRegSchedulingDatePlus> lstTmp = new List<EntityOpRegSchedulingDatePlus>();
                foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                {
                    if (lstTmp.Any(t => t.weekId == item.weekId && t.amPm == item.amPm))
                    {
                        DialogBox.Msg(Function.GetWeekName((int)item.weekId) + " " + item.amPmName + " 重复排班,请检查。");
                        return;
                    }
                    else
                    {
                        lstTmp.Add(item);
                    }
                }
                if (lstPlus.Count == 0)
                {
                    Viewer.gcDate.RefreshDataSource();
                    DialogBox.Msg("请填写医师出诊表格。");
                    return;
                }

                // 号源设置
                List<EntityOpRegSchedulingNumber> lstRegNumber = this.gvDataBindingSource.DataSource as List<EntityOpRegSchedulingNumber>;
                for (int i = lstRegNumber.Count - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(lstRegNumber[i].startTime) && !string.IsNullOrEmpty(lstRegNumber[i].endTime) &&
                         Convert.ToDateTime(lstRegNumber[i].endTime) > Convert.ToDateTime(lstRegNumber[i].startTime))
                    {
                        if (string.IsNullOrEmpty(lstRegNumber[i].weekIdName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定时间。");
                            return;
                        }
                        if (string.IsNullOrEmpty(lstRegNumber[i].regCodeName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定号别。");
                            return;
                        }
                        if (string.IsNullOrEmpty(lstRegNumber[i].amPmName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定班次。");
                            return;
                        }
                        lstRegNumber[i].weekId = Function.GetWeekIndex(lstRegNumber[i].weekIdName);
                        lstRegNumber[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstRegNumber[i].amPmName).shiftCode;
                        lstRegNumber[i].startTime = Convert.ToDateTime(lstRegNumber[i].startTime).ToString("HH:mm");
                        lstRegNumber[i].endTime = Convert.ToDateTime(lstRegNumber[i].endTime).ToString("HH:mm");
                    }
                    else
                    {
                        lstRegNumber.RemoveAt(i);
                    }
                }
                if (lstRegNumber.Count > 0)
                {
                    foreach (EntityOpRegSchedulingNumber numVo in lstRegNumber)
                    {
                        if (!DataSourceRegDate.Any(t => t.weekId == numVo.weekId && t.regCode == numVo.regCode && t.amPm == numVo.amPm))
                        {
                            DialogBox.Msg("号源设置的时间、号别、班次 与出诊时间不一致，请检查。");
                            return;
                        }
                    }
                }
                bool isNoNumber = false;
                if (lstRegNumber == null || lstRegNumber.Count == 0)
                {
                    isNoNumber = true;
                    // 默认值
                    EntityOpRegSchedulingNumber numVo = null;
                    if (Viewer.chkTimeSpan.Checked)
                    {
                        #region 按小时拆分
                        foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                        {
                            #region 拆
                            startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).startTime;
                            endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).endTime;
                            if (endTime == "24:00") endTime = "23:59";
                            string endTime2 = endTime;
                            TimeSpan ts = Convert.ToDateTime(date + " " + endTime + ":00").Subtract(Convert.ToDateTime(date + " " + startTime + ":00"));
                            int hours = (int)ts.TotalHours;
                            List<string> lstTimes = new List<string>();
                            if (hours > 0)
                            {
                                endTime = startTime;
                                for (int i = 0; i < hours; i++)
                                {
                                    startTime = endTime;
                                    endTime = Convert.ToDateTime(endTime + ":00").AddHours(1).ToString("HH:mm");
                                    lstTimes.Add(startTime + "|" + endTime);
                                }
                            }
                            if (endTime != endTime2)
                            {
                                startTime = endTime;
                                endTime = endTime2;
                                lstTimes.Add(startTime + "|" + endTime);
                            }
                            int nums = (int)item.limitNum;
                            int x = nums / lstTimes.Count;
                            int y = nums % lstTimes.Count;
                            List<int> lstNums = new List<int>();
                            foreach (string str in lstTimes)
                            {
                                lstNums.Add(x);
                            }
                            if (y > 0) lstNums[lstNums.Count - 1] += y;
                            #endregion
                            for (int k = 0; k < lstTimes.Count; k++)
                            {
                                numVo = new EntityOpRegSchedulingNumber();
                                numVo.regWid = item.regWid;
                                numVo.amPm = (int)item.amPm;
                                numVo.startTime = lstTimes[k].Split('|')[0];
                                numVo.endTime = lstTimes[k].Split('|')[1];
                                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                    numVo.weNum = lstNums[k];
                                else
                                    numVo.normalNum = lstNums[k];
                                numVo.weekId = item.weekId;
                                numVo.regCode = item.regCode;
                                lstRegNumber.Add(numVo);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 大时段
                        foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                        {
                            numVo = new EntityOpRegSchedulingNumber();
                            numVo.regWid = item.regWid;
                            numVo.amPm = (int)item.amPm;
                            numVo.startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == numVo.amPm).startTime;
                            numVo.endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == numVo.amPm).endTime;
                            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                numVo.weNum = (int)item.limitNum;
                            else
                                numVo.normalNum = (int)item.limitNum;
                            numVo.weekId = item.weekId;
                            numVo.regCode = item.regCode;
                            lstRegNumber.Add(numVo);
                        }
                        #endregion
                    }
                }

                EntityDML<EntityOpRegSchedulingDatePlus> voDML = Verify<EntityOpRegSchedulingDatePlus>(Viewer.gvDate, this.BindRegDate);
                if (voDML.IsDelete && voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDatePlus item in voDML.DeleteSource)
                    {
                        if (item.regWid > 0) lstWid.Add(item.regWid);
                    }
                }

                int ret = 0;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    ret = proxy.Service.SaveScheduling(mainVo, voDML.AddSource, voDML.DeleteSource, voDML.UpdateSource, lstRegNumber, doctVo, lstWid, DataSourceShift, lstPlusAll);
                }
                if (ret > 0)
                {
                    if (!isExit)
                    {
                        this.DeptCode = mainVo.deptCode;
                        this.RoomCode = mainVo.roomCode;
                        this.DoctCode = mainVo.doctCode;
                        Load();
                    }
                    Viewer.IsSave = true;
                    Viewer.ValueChanged = false;
                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    if (isNoNumber) Viewer.gcNo.RefreshDataSource();
                    DialogBox.Msg("保存失败。");
                }
                #endregion
            }
            else if (this.EditType == 2)
            {
                #region 日排班

                #region 主信息
                // 主信息
                EntityOpRegSchedulingDay mainVo = new EntityOpRegSchedulingDay();
                mainVo.regDate = Viewer.dteRegDate.Text.Trim();
                mainVo.deptCode = Viewer.lueDept.Properties.DBValue;
                mainVo.roomCode = Viewer.lueRoom.Properties.DBValue;
                mainVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                mainVo.deptName = Viewer.lueDept.Text;
                mainVo.doctName = Viewer.lueDoct.Text;
                mainVo.status = 1;

                #region 检测

                if (string.IsNullOrEmpty(mainVo.regDate))
                {
                    DialogBox.Msg("请选择日期.");
                    Viewer.dteRegDate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.deptCode))
                {
                    DialogBox.Msg("请选择科室.");
                    Viewer.lueDept.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.roomCode))
                {
                    DialogBox.Msg("请选择诊室.");
                    Viewer.lueRoom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mainVo.doctCode))
                {
                    DialogBox.Msg("请选择医生.");
                    Viewer.lueDoct.Focus();
                    return;
                }

                #endregion

                // 医师资料
                EntityOpRegSchedulingDoct doctVo = new EntityOpRegSchedulingDoct();
                doctVo.doctCode = mainVo.doctCode;
                doctVo.doctName = Viewer.lueDoct.Text;
                doctVo.doctSkill = Viewer.txtSkill.Text.Trim();
                doctVo.doctIntroduce = Viewer.txtIntroduce.Text.Trim();
                if (Viewer.picPhoto.Image != null)
                {
                    doctVo.doctPhoto = Function.ConvertImageToByte(Viewer.picPhoto.Image, 4);
                }
                #endregion

                // 挂号时间设置
                if (DataSourceRegDate == null || DataSourceRegDate.Count == 0)
                {
                    DialogBox.Msg("请维护医师出诊时间。");
                    return;
                }

                string startTime = string.Empty;
                string endTime = string.Empty;
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                List<decimal> lstDid = new List<decimal>();
                BindingListView<EntityOpRegSchedulingDatePlus> lstPlus = this.BindRegDate.DataSource as BindingListView<EntityOpRegSchedulingDatePlus>;
                List<EntityOpRegSchedulingDatePlus> lstPlusAll = new List<EntityOpRegSchedulingDatePlus>();
                for (int i = lstPlus.Count - 1; i >= 0; i--)
                {
                    if (lstPlus[i].regDid > 0 && lstDid.IndexOf(lstPlus[i].regDid) < 0) lstDid.Add(lstPlus[i].regDid);
                    if (!string.IsNullOrEmpty(lstPlus[i].regCode) && !string.IsNullOrEmpty(lstPlus[i].amPmName))
                    {
                        if (lstPlus[i].limitNum <= 0)
                        {
                            DialogBox.Msg("医师出诊表格里请指定限号数量。");
                            return;
                        }
                        if (string.IsNullOrEmpty(lstPlus[i].amPmName))
                        {
                            DialogBox.Msg("医师出诊表格里请指定停诊/出诊。");
                            return;
                        }
                        if (string.IsNullOrEmpty(lstPlus[i].statusName))
                            lstPlus[i].status = 1;
                        else
                            lstPlus[i].status = lstPlus[i].statusName == "出诊" ? 1 : 0;
                        if (!string.IsNullOrEmpty(lstPlus[i].limitTypeName))
                        {
                            if (lstPlus[i].limitTypeName == "微信不可见") lstPlus[i].limitTypeId = 1;
                            else if (lstPlus[i].limitTypeName == "自助不可见") lstPlus[i].limitTypeId = 2;
                            else if (lstPlus[i].limitTypeName == "微信自助不可见") lstPlus[i].limitTypeId = 3;
                        }
                        lstPlus[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstPlus[i].amPmName).shiftCode;
                        startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == lstPlus[i].amPm).startTime;
                        endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == lstPlus[i].amPm).endTime;
                        TimeSpan ts = Convert.ToDateTime(date + " " + endTime).Subtract(Convert.ToDateTime(date + " " + startTime));
                        if (ts.TotalMinutes > 0)
                        {
                            lstPlus[i].freqNum = Math.Ceiling(Convert.ToDecimal((decimal)ts.TotalMinutes / lstPlus[i].limitNum));
                        }
                    }
                    else
                    {
                        lstPlus.RemoveAt(i);
                        continue;
                    }
                    if (lstPlus[i].regDid > 0)
                    {
                        lstPlusAll.Add(lstPlus[i]);
                    }
                    else
                    {
                        // 20171228重复新建判断
                        using (ProxyScheduling proxy = new ProxyScheduling())
                        {
                            if (proxy.Service.IsExistDayShiftRec(mainVo.doctCode, mainVo.regDate, lstPlus[i].amPm))
                            {
                                DialogBox.Msg(mainVo.regDate + " " + lstPlus[i].amPmName + " 的排班记录已存在,不能再新建,请检查。");
                                return;
                            }
                        }
                    }
                }
                // 检测重复项
                List<EntityOpRegSchedulingDatePlus> lstTmp = new List<EntityOpRegSchedulingDatePlus>();
                foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                {
                    if (lstTmp.Any(t => t.regCode == item.regCode && t.amPm == item.amPm))
                    {
                        DialogBox.Msg(item.regCodeName + " " + item.amPmName + " 重复排班,请检查。");
                        return;
                    }
                    else
                    {
                        lstTmp.Add(item);
                    }
                }
                if (lstPlus.Count == 0)
                {
                    Viewer.gcDate.RefreshDataSource();
                    DialogBox.Msg("请填写医师出诊表格。");
                    return;
                }

                // 号源设置
                List<EntityOpRegSchedulingDayNumber> lstRegNumber = this.gvDataBindingSource.DataSource as List<EntityOpRegSchedulingDayNumber>;
                for (int i = lstRegNumber.Count - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(lstRegNumber[i].startTime) && !string.IsNullOrEmpty(lstRegNumber[i].endTime) &&
                         Convert.ToDateTime(lstRegNumber[i].endTime) > Convert.ToDateTime(lstRegNumber[i].startTime))
                    {
                        if (string.IsNullOrEmpty(lstRegNumber[i].regCodeName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定号别。");
                            return;
                        }
                        if (string.IsNullOrEmpty(lstRegNumber[i].amPmName))
                        {
                            DialogBox.Msg("号源设置表格里的时间段请指定班次。");
                            return;
                        }
                        lstRegNumber[i].amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == lstRegNumber[i].amPmName).shiftCode;
                        lstRegNumber[i].startTime = Convert.ToDateTime(lstRegNumber[i].startTime).ToString("HH:mm");
                        lstRegNumber[i].endTime = Convert.ToDateTime(lstRegNumber[i].endTime).ToString("HH:mm");
                    }
                    else
                    {
                        lstRegNumber.RemoveAt(i);
                    }
                }
                if (lstRegNumber.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDayNumber numVo in lstRegNumber)
                    {
                        if (!DataSourceRegDate.Any(t => t.regCode == numVo.regCode && t.amPm == numVo.amPm))
                        {
                            DialogBox.Msg("号源设置的号别、班次 与出诊时间不一致，请检查。");
                            return;
                        }
                    }
                }
                bool isNoNumber = false;
                if (lstRegNumber == null || lstRegNumber.Count == 0)
                {
                    isNoNumber = true;
                    // 默认值
                    EntityOpRegSchedulingDayNumber numVo = null;
                    if (Viewer.chkTimeSpan.Checked)
                    {
                        #region 按小时拆分
                        foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                        {
                            #region 拆
                            startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).startTime;
                            endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).endTime;
                            if (endTime == "24:00") endTime = "23:59";
                            string endTime2 = endTime;
                            TimeSpan ts = Convert.ToDateTime(date + " " + endTime + ":00").Subtract(Convert.ToDateTime(date + " " + startTime + ":00"));
                            int hours = (int)ts.TotalHours;
                            List<string> lstTimes = new List<string>();
                            if (hours > 0)
                            {
                                endTime = startTime;
                                for (int i = 0; i < hours; i++)
                                {
                                    startTime = endTime;
                                    endTime = Convert.ToDateTime(endTime + ":00").AddHours(1).ToString("HH:mm");
                                    lstTimes.Add(startTime + "|" + endTime);
                                }
                            }
                            if (endTime != endTime2)
                            {
                                startTime = endTime;
                                endTime = endTime2;
                                lstTimes.Add(startTime + "|" + endTime);
                            }
                            int nums = (int)item.limitNum;
                            int x = nums / lstTimes.Count;
                            int y = nums % lstTimes.Count;
                            List<int> lstNums = new List<int>();
                            foreach (string str in lstTimes)
                            {
                                lstNums.Add(x);
                            }
                            if (y > 0) lstNums[lstNums.Count - 1] += y;
                            #endregion
                            for (int k = 0; k < lstTimes.Count; k++)
                            {
                                numVo = new EntityOpRegSchedulingDayNumber();
                                numVo.regDid = item.regDid;
                                numVo.amPm = (int)item.amPm;
                                numVo.startTime = lstTimes[k].Split('|')[0];
                                numVo.endTime = lstTimes[k].Split('|')[1];
                                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                    numVo.weNum = lstNums[k];
                                else
                                    numVo.normalNum = lstNums[k];
                                numVo.regCode = item.regCode;
                                lstRegNumber.Add(numVo);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 大时段
                        foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                        {
                            numVo = new EntityOpRegSchedulingDayNumber();
                            numVo.regDid = item.regDid;
                            numVo.amPm = (int)item.amPm;
                            numVo.startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == numVo.amPm).startTime;
                            numVo.endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == numVo.amPm).endTime;
                            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                numVo.weNum = (int)item.limitNum;
                            else
                                numVo.normalNum = (int)item.limitNum;
                            numVo.regCode = item.regCode;
                            lstRegNumber.Add(numVo);
                        }
                        #endregion
                    }
                }
                EntityDML<EntityOpRegSchedulingDatePlus> voDML = Verify<EntityOpRegSchedulingDatePlus>(Viewer.gvDate, this.BindRegDate);
                if (voDML.IsDelete && voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                {
                    List<decimal> lstDelRegDid = new List<decimal>();
                    foreach (EntityOpRegSchedulingDatePlus item in voDML.DeleteSource)
                    {
                        if (item.regDid > 0)
                        {
                            lstDid.Add(item.regDid);
                            lstDelRegDid.Add(item.regDid);
                        }
                    }
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        if (proxy.Service.CheckSchedulingDayIsReg(lstDelRegDid))
                        {
                            DialogBox.Msg("该日排班记录已被预约，不能删除。");
                            return;
                        }
                    }
                }
                if (voDML.AddSource != null && voDML.AddSource.Count > 0)
                {
                    if (voDML.UpdateSource == null) voDML.UpdateSource = new List<EntityOpRegSchedulingDatePlus>();
                    for (int k = voDML.AddSource.Count - 1; k >= 0; k--)
                    {
                        if (voDML.AddSource[k].regDid > 0)
                        {
                            voDML.UpdateSource.Add(voDML.AddSource[k]);
                            voDML.AddSource.RemoveAt(k);
                        }
                    }
                }
                int ret = 0;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    ret = proxy.Service.SaveSchedulingDay(mainVo, voDML.AddSource, voDML.DeleteSource, voDML.UpdateSource, lstRegNumber, doctVo, lstDid, DataSourceShift, lstPlusAll);
                }
                if (ret > 0)
                {
                    List<EntityOpRegSchedulingDatePlus> lstStop = new List<EntityOpRegSchedulingDatePlus>();
                    if (voDML.AddSource != null && voDML.AddSource.Count > 0)
                    {
                        foreach (EntityOpRegSchedulingDatePlus item in voDML.AddSource)
                        {
                            if (item.status == 0 && !lstStop.Any(t => t.regDid == item.regDid)) lstStop.Add(item);
                        }
                    }
                    if (voDML.UpdateSource != null && voDML.UpdateSource.Count > 0)
                    {
                        foreach (EntityOpRegSchedulingDatePlus item in voDML.UpdateSource)
                        {
                            if (item.status == 0 && !lstStop.Any(t => t.regDid == item.regDid)) lstStop.Add(item);
                        }
                    }
                    if (voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                    {
                        foreach (EntityOpRegSchedulingDatePlus item in voDML.DeleteSource)
                        {
                            if (item.status == 0 && !lstStop.Any(t => t.regDid == item.regDid)) lstStop.Add(item);
                        }
                    }
                    if (!isExit)
                    {
                        this.DeptCode = mainVo.deptCode;
                        this.RoomCode = mainVo.roomCode;
                        this.DoctCode = mainVo.doctCode;
                        this.RegDate = mainVo.regDate;
                        Load();
                        Viewer.tvDate.SetFocusedNode(Viewer.tvDate.FindNodeByFieldValue("regDate", mainVo.regDate));
                    }
                    if (lstStop.Count > 0)
                    {
                        // 再次校验.0611
                        lstRegNumber = this.gvDataBindingSource.DataSource as List<EntityOpRegSchedulingDayNumber>;
                        foreach (EntityOpRegSchedulingDatePlus item in lstStop)
                        {
                            if (lstRegNumber != null && (lstRegNumber.Any(t => t.regCode == item.regCode && t.regDid != item.regDid)))
                            {
                                item.regDid = lstRegNumber.FirstOrDefault(t => t.regCode == item.regCode && t.regDid != item.regDid).regDid;
                            }
                            #region Shift
                            //1	急诊-早	00:00	07:59
                            //2	早班	08:00	11:50
                            //3	晚早	08:30	11:50
                            //4	早中	08:00	13:50
                            //5	午班	14:00	16:50
                            //6	晚班	19:00	21:50
                            //7	急诊-中	11:51	13:59
                            //8	急诊-晚	16:51	23:59
                            //9	儿科急诊-晚	22:00	23:59
                            //10	妇产科急诊-晚	21:30	23:59

                            //停诊开始时间	startTime	N	格式：24hh:mm
                            //停诊结束时间	endTime	    N	格式：24hh:mm
                            //时段	timeFlag	N	1：上午
                            //                      2：下午
                            //                      3：晚上
                            //                      4：全天
                            startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).startTime;
                            endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).endTime;
                            string shiftCode2 = "4";
                            if (!string.IsNullOrEmpty(startTime))
                            {
                                if (Convert.ToDateTime(startTime) < Convert.ToDateTime("12:00") && Convert.ToDateTime(startTime) >= Convert.ToDateTime("00:00"))
                                    shiftCode2 = "1";
                                else if (Convert.ToDateTime(startTime) < Convert.ToDateTime("19:00") && Convert.ToDateTime(startTime) >= Convert.ToDateTime("12:00"))
                                    shiftCode2 = "2";
                                else if (Convert.ToDateTime(startTime) < Convert.ToDateTime("23:59") && Convert.ToDateTime(startTime) >= Convert.ToDateTime("19:00"))
                                    shiftCode2 = "3";
                                else
                                    shiftCode2 = "4";
                            }
                            #endregion

                            string amPm = item.amPm.ToString();
                            ProxyRegistration proxy = null;
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
                            // 20180319.由于201802以来停诊通知不能用，现改为班次不再转换，传HIS原始值
                            // xmlIn += "<timeFlag>" + shiftCode2 + "</timeFlag>" + Environment.NewLine;
                            xmlIn += "<timeFlag>" + item.amPm + "</timeFlag>" + Environment.NewLine;
                            xmlIn += "<startTime>" + startTime + "</startTime>" + Environment.NewLine;
                            xmlIn += "<endTime>" + endTime + "</endTime>" + Environment.NewLine;
                            xmlIn += "<stopDesc>" + "􀀓􀄆􀀿􀆬" + "</stopDesc>" + Environment.NewLine;
                            xmlIn += "<personalFirst> </personalFirst>" + Environment.NewLine;
                            xmlIn += "<personalSecond> </personalSecond>" + Environment.NewLine;
                            xmlIn += "<personalThird></personalThird>" + Environment.NewLine;
                            xmlIn += "<personalForth></personalForth>" + Environment.NewLine;
                            xmlIn += "</request>" + Environment.NewLine;

                            string ipAddr = string.Empty;
                            if (GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                            {
                                if (GlobalParm.dicSysParameter.ContainsKey(38) && GlobalParm.dicSysParameter[38].Trim() != string.Empty)
                                {
                                    ipAddr = GlobalParm.dicSysParameter[38].Trim();
                                }
                            }
                            if (ipAddr == string.Empty)
                                proxy = new ProxyRegistration();
                            else
                                proxy = new ProxyRegistration(ipAddr);
                            string xmlOut = proxy.Service.WeChatStop(item.regDid, amPm, xmlIn);
                            if (!string.IsNullOrEmpty(xmlOut))
                            {
                                DialogBox.Msg(xmlOut);
                            }
                            #endregion

                            #region 自助机停诊通知.暂不实现
                            if (GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                            {
                                //xmlOut = proxy.Service.GdaStopTreatment(mainVo, lstStop);
                                //if (!string.IsNullOrEmpty(xmlOut))
                                //{
                                //    DialogBox.Msg(xmlOut);
                                //}
                            }
                            #endregion
                            proxy = null;
                        }
                    }
                    Viewer.IsSave = true;
                    Viewer.ValueChanged = false;
                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    if (isNoNumber) Viewer.gcNo.RefreshDataSource();
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
            Viewer.gvDate.CloseEditor();
            Viewer.gvNo.CloseEditor();
            if (this.EditType == 1)
            {
                #region 排班计划
                //List<decimal> lstWid = new List<decimal>();
                //if (DataSourceRegDate != null && DataSourceRegDate.Count > 0)
                //{
                //    foreach (EntityOpRegSchedulingDatePlus item in DataSourceRegDate)
                //    {
                //        if (item.regWid > 0) lstWid.Add(item.regWid);
                //    }
                //}
                //EntityDML<EntityOpRegSchedulingDatePlus> voDML = Verify<EntityOpRegSchedulingDatePlus>(Viewer.gvDate, this.BindRegDate);
                //if (voDML.IsDelete && voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                //{
                //    foreach (EntityOpRegSchedulingDatePlus item in voDML.DeleteSource)
                //    {
                //        if (item.regWid > 0) lstWid.Add(item.regWid);
                //    }
                //}
                //if (lstWid.Count == 0)
                //{
                //    Clear();
                //    return;
                //}
                string deptCode = Viewer.lueDept.Properties.DBValue;
                string roomCode = Viewer.lueRoom.Properties.DBValue;
                string doctCode = Viewer.lueDoct.Properties.DBValue;
                if (string.IsNullOrEmpty(deptCode) || string.IsNullOrEmpty(roomCode) || string.IsNullOrEmpty(doctCode))
                {
                    DialogBox.Msg("请指定科室、诊间、医生。");
                    return;
                }
                if (DialogBox.Msg("确定是否删除？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.DelScheduling(deptCode, roomCode, doctCode);
                    proxy = null;
                    if (ret > 0)
                    {
                        Viewer.ValueChanged = false;
                        Viewer.IsSave = true;
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
                List<decimal> lstDid = new List<decimal>();
                if (DataSourceRegDate != null && DataSourceRegDate.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDatePlus item in DataSourceRegDate)
                    {
                        if (item.regDid > 0) lstDid.Add(item.regDid);
                    }
                }
                EntityDML<EntityOpRegSchedulingDatePlus> voDML = Verify<EntityOpRegSchedulingDatePlus>(Viewer.gvDate, this.BindRegDate);
                if (voDML.IsDelete && voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDatePlus item in voDML.DeleteSource)
                    {
                        if (item.regDid > 0) lstDid.Add(item.regDid);
                    }
                }
                if (lstDid.Count == 0)
                {
                    Clear();
                    return;
                }
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    if (proxy.Service.CheckSchedulingDayIsReg(lstDid))
                    {
                        DialogBox.Msg("该日排班记录已被预约，不能删除。");
                        return;
                    }
                }
                if (DialogBox.Msg("确定是否删除？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.DelSchedulingDay(lstDid);
                    proxy = null;
                    if (ret > 0)
                    {
                        Viewer.tvDate.Nodes.Remove(Viewer.tvDate.FocusedNode);
                        Viewer.ValueChanged = false;
                        Viewer.IsSave = true;
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
                //           join var2 in GlobalDic.DataSourceEmpDept on var1.operCode.Trim() equals var2.operCode.Trim()
                //           where (var2.deptCode.Trim() == deptCode.Trim())
                //           select new { var1.operCode, var1.operName, var2.pyCode, var2.wbCode };
                //var doct = GlobalDic.DataSourceDoctor.FindAll(t => t.DeptNo == deptCode);
                var doct = from var1 in GlobalDic.DataSourceDoctor
                           join var2 in GlobalDic.DataSourceDefDeptEmployee on var1.operCode.Trim() equals var2.operCode.Trim()
                           where (var2.deptCode.Trim() == deptCode.Trim())
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
                Viewer.blbiSave.Enabled = true;
            }
            else
            {
                if (doctVo.doctPhoto != null)
                    Viewer.picPhoto.Image = Function.ConvertByteToImage(doctVo.doctPhoto);
                else
                    Viewer.picPhoto.Image = Properties.Resources.unknow;
                Viewer.txtSkill.Text = doctVo.doctSkill;
                Viewer.txtIntroduce.Text = doctVo.doctIntroduce;
                Viewer.blbiSave.Enabled = (doctVo.isConfirm ? false : true);
            }
        }
        #endregion

        #endregion

        #region SetCellStyleDate
        /// <summary>
        /// SetCellStyleDate
        /// </summary>
        /// <param name="e"></param>
        internal void SetCellStyleDate(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            if (fieldName == EntityOpRegSchedulingDatePlus.Columns.limitNum || fieldName == EntityOpRegSchedulingDatePlus.Columns.freqNum)
            {
                if (Function.Dec(GetFieldValueStr(Viewer.gvDate, e.RowHandle, fieldName)) == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
        }
        #endregion

        #region SetCellStyleNumber
        /// <summary>
        /// SetCellStyleNumber
        /// </summary>
        /// <param name="e"></param>
        internal void SetCellStyleNumber(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
                }
            }
        }
        #endregion

        #region FillRowValueByDBRow
        /// <summary>
        /// FillRowValueByDBRow
        /// </summary>
        /// <param name="DBRow"></param>
        protected override void FillRowValueByDBRow(DevExpress.XtraGrid.Views.Grid.GridView gv, BaseDataContract DBRow)
        {
            if (gv != null && gv == Viewer.gvDate && DBRow is EntityDicShift && gv.FocusedColumn.FieldName == EntityOpRegSchedulingDatePlus.Columns.amPmName)
            {
                gv.SetRowCellValue(gv.FocusedRowHandle, EntityOpRegSchedulingDatePlus.Columns.dateScope, ((EntityDicShift)DBRow).startTime + "~" + ((EntityDicShift)DBRow).endTime);
            }
        }
        #endregion

        #region DayCopy
        /// <summary>
        /// DayCopy
        /// </summary>
        internal void DayCopy()
        {
            List<string> lstWeekName = new List<string>();
            BindingListView<EntityOpRegSchedulingDatePlus> lstPlus = this.BindRegDate.DataSource as BindingListView<EntityOpRegSchedulingDatePlus>;
            for (int i = 0; i < lstPlus.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(lstPlus[i].weekIdName) && !string.IsNullOrEmpty(lstPlus[i].regCode) && !string.IsNullOrEmpty(lstPlus[i].amPmName))
                {
                    if (lstWeekName.IndexOf(lstPlus[i].weekIdName) < 0) lstWeekName.Add(lstPlus[i].weekIdName);
                }
            }
            if (lstWeekName.Count > 0)
            {
                Function.SuspendLayout(Viewer.plTop2.Handle);
                frmSchedulingEditCopy frm = new frmSchedulingEditCopy(lstWeekName);
                Function.ResumeLayout(Viewer.plTop2.Handle);
                Viewer.plTop2.Refresh();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    List<int> lstWeekId = frm.lstWeekId;
                    List<EntityOpRegSchedulingDatePlus> lstSource = new List<EntityOpRegSchedulingDatePlus>();
                    for (int i = lstPlus.Count - 1; i >= 0; i--)
                    {
                        if (!string.IsNullOrEmpty(lstPlus[i].weekIdName) && !string.IsNullOrEmpty(lstPlus[i].regCode) && !string.IsNullOrEmpty(lstPlus[i].amPmName))
                        {
                            if (lstWeekId.IndexOf(Function.GetWeekIndex(lstPlus[i].weekIdName)) >= 0)
                            {
                                lstPlus.RemoveAt(i);
                            }
                            else if (Function.GetWeekIndex(lstPlus[i].weekIdName) == frm.SWeekId)
                            {
                                lstSource.Add(lstPlus[i]);
                            }
                        }
                        else
                        {
                            lstPlus.RemoveAt(i);
                        }
                    }
                    int rowHandle = 0;
                    foreach (int weekid in lstWeekId)
                    {
                        EntityOpRegSchedulingDatePlus vo1 = null;
                        EntityOpRegSchedulingDatePlus vo2 = null;
                        List<EntityOpRegSchedulingDatePlus> lstVo2 = new List<EntityOpRegSchedulingDatePlus>();
                        for (int j = lstSource.Count - 1; j >= 0; j--)
                        {
                            vo1 = lstSource[j];
                            vo2 = new EntityOpRegSchedulingDatePlus();
                            vo2.weekId = weekid;
                            vo2.amPm = vo1.amPm;
                            vo2.regCode = vo1.regCode;
                            vo2.weekIdName = Function.GetWeekName(weekid);
                            vo2.amPmName = vo1.amPmName;
                            vo2.regCodeName = vo1.regCodeName;
                            vo2.limitNum = vo1.limitNum;
                            vo2.freqNum = vo1.freqNum;
                            vo2.status = vo1.status;
                            vo2.statusName = vo1.statusName;
                            vo2.limitTypeId = vo1.limitTypeId;
                            vo2.limitTypeName = vo1.limitTypeName;
                            vo2.regWid = 0;
                            vo2.regDid = vo1.regDid;
                            vo2.regDate = vo1.regDate;
                            vo2.dateScope = vo1.dateScope;
                            vo2.check = vo1.check;
                            lstVo2.Add(vo2);
                        }
                        lstPlus.AppendRange(lstVo2);
                        //lstPlus.Sort(EntityOpRegSchedulingDatePlus.Columns.weekIdName, System.ComponentModel.ListSortDirection.Ascending);
                        Viewer.gcDate.RefreshDataSource();

                        #region 复制方式2
                        //foreach (EntityOpRegSchedulingDatePlus item in lstSource)
                        //{
                        //    AddRegDateRow();
                        //    rowHandle = Viewer.gvDate.RowCount - 1;
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.weekId, weekid);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.amPm, item.amPm);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.regCode, item.regCode);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.weekIdName, Function.GetWeekName(weekid));
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.amPmName, item.amPmName);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.regCodeName, item.regCodeName);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.limitNum, item.limitNum);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.freqNum, item.freqNum);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.status, item.status);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.statusName, item.statusName);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.regWid, item.regWid);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.regDid, item.regDid);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.regDate, item.regDate);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.dateScope, item.dateScope);
                        //    Viewer.gvDate.SetRowCellValue(rowHandle, EntityOpRegSchedulingDatePlus.Columns.check, item.check);
                        //    Viewer.gvDate.CloseEditor();
                        //}
                        //Viewer.gcDate.RefreshDataSource();
                        #endregion
                    }
                }
            }
            else
            {
                DialogBox.Msg("无有效排班");
            }
        }
        #endregion

        #region AddNo
        /// <summary>
        /// AddNo
        /// </summary>
        internal void AddNo()
        {
            List<EntityOpRegSchedulingDatePlus> lstDate = new List<EntityOpRegSchedulingDatePlus>();
            BindingListView<EntityOpRegSchedulingDatePlus> lstPlus = this.BindRegDate.DataSource as BindingListView<EntityOpRegSchedulingDatePlus>;
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                {
                    if (item.regDid > 0)
                    {
                        if (proxy.Service.IsHaveQueue(item.regDid, item.amPm))
                        {
                            item.isHaveQueue = true;
                            lstDate.Add(item);
                        }
                        else
                        {
                            if (proxy.Service.IsSchedulingDayConfirm(item.regDid))
                            {
                                item.isHaveQueue = false;
                                lstDate.Add(item);
                            }
                        }
                    }
                }
            }
            if (lstDate.Count == 0)
            {
                DialogBox.Msg("未审核排班数据不能加号。");
                return;
            }
            Function.SuspendLayout(Viewer.plTop2.Handle);
            frmSchedulingEditAddNo frm = new frmSchedulingEditAddNo(lstDate);
            Function.ResumeLayout(Viewer.plTop2.Handle);
            Viewer.plTop2.Refresh();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Load();
            }
        }
        #endregion

        #region 停出诊
        /// <summary>
        /// 停出诊
        /// </summary>
        internal void DoctService()
        {
            Viewer.gvDate.CloseEditor();

            #region 主信息
            // 主信息
            EntityOpRegSchedulingDay mainVo = new EntityOpRegSchedulingDay();
            mainVo.regDate = Viewer.dteRegDate.Text.Trim();
            mainVo.deptCode = Viewer.lueDept.Properties.DBValue;
            mainVo.roomCode = Viewer.lueRoom.Properties.DBValue;
            mainVo.doctCode = Viewer.lueDoct.Properties.DBValue;
            mainVo.deptName = Viewer.lueDept.Text;
            mainVo.doctName = Viewer.lueDoct.Text;
            mainVo.status = 1;

            #region 检测

            if (string.IsNullOrEmpty(mainVo.regDate))
            {
                DialogBox.Msg("请选择日期.");
                Viewer.dteRegDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mainVo.deptCode))
            {
                DialogBox.Msg("请选择科室.");
                Viewer.lueDept.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mainVo.roomCode))
            {
                DialogBox.Msg("请选择诊室.");
                Viewer.lueRoom.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mainVo.doctCode))
            {
                DialogBox.Msg("请选择医生.");
                Viewer.lueDoct.Focus();
                return;
            }

            #endregion

            #endregion

            // 挂号时间设置
            if (DataSourceRegDate == null || DataSourceRegDate.Count == 0)
            {
                DialogBox.Msg("请维护医师出诊时间。");
                return;
            }

            string startTime = string.Empty;
            string endTime = string.Empty;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            List<decimal> lstDid = new List<decimal>();
            BindingListView<EntityOpRegSchedulingDatePlus> lstPlus = this.BindRegDate.DataSource as BindingListView<EntityOpRegSchedulingDatePlus>;
            List<EntityOpRegSchedulingDayDate> lstRegDate = new List<EntityOpRegSchedulingDayDate>();
            EntityOpRegSchedulingDayDate vo = null;
            List<EntityOpRegSchedulingDayDate> lstStop = new List<EntityOpRegSchedulingDayDate>();

            foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
            {
                if (string.IsNullOrEmpty(item.amPmName))
                {
                    DialogBox.Msg("医师出诊表格里请指定停诊/出诊。");
                    return;
                }
                if (string.IsNullOrEmpty(item.statusName))
                    item.status = 1;
                else
                    item.status = item.statusName == "出诊" ? 1 : 0;
                item.amPm = DataSourceShift.FirstOrDefault(t => t.shiftName == item.amPmName).shiftCode;

                vo = new EntityOpRegSchedulingDayDate();
                vo.serNo = item.serNo;
                vo.regDid = item.regDid;
                vo.regCode = item.regCode;
                vo.typeId = 3;
                vo.amPm = (int)item.amPm;
                vo.startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == vo.amPm).startTime;
                vo.endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == vo.amPm).endTime; ;
                vo.limitNum = (int)item.limitNum;
                vo.freqNum = item.freqNum;
                vo.status = item.status;
                if (vo.serNo <= 0) continue;
                lstRegDate.Add(vo);
                if (vo.status == 0) lstStop.Add(vo);
            }
            int ret = 0;
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                ret = proxy.Service.DoctService(lstRegDate);
            }
            if (ret > 0)
            {
                if (lstStop.Count > 0)
                {
                    foreach (EntityOpRegSchedulingDayDate item in lstStop)
                    {
                        startTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).startTime;
                        endTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == item.amPm).endTime;

                        string amPm = item.amPm.ToString();
                        ProxyRegistration proxy = null;
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
                        // 20180319.由于201802以来停诊通知不能用，现改为班次不再转换，传HIS原始值
                        // xmlIn += "<timeFlag>" + shiftCode2 + "</timeFlag>" + Environment.NewLine;
                        xmlIn += "<timeFlag>" + item.amPm + "</timeFlag>" + Environment.NewLine;
                        xmlIn += "<startTime>" + startTime + "</startTime>" + Environment.NewLine;
                        xmlIn += "<endTime>" + endTime + "</endTime>" + Environment.NewLine;
                        xmlIn += "<stopDesc>" + "􀀓􀄆􀀿􀆬" + "</stopDesc>" + Environment.NewLine;
                        xmlIn += "<personalFirst> </personalFirst>" + Environment.NewLine;
                        xmlIn += "<personalSecond> </personalSecond>" + Environment.NewLine;
                        xmlIn += "<personalThird></personalThird>" + Environment.NewLine;
                        xmlIn += "<personalForth></personalForth>" + Environment.NewLine;
                        xmlIn += "</request>" + Environment.NewLine;

                        string ipAddr = string.Empty;
                        if (GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                        {
                            if (GlobalParm.dicSysParameter.ContainsKey(38) && GlobalParm.dicSysParameter[38].Trim() != string.Empty)
                            {
                                ipAddr = GlobalParm.dicSysParameter[38].Trim();
                            }
                        }
                        if (ipAddr == string.Empty)
                            proxy = new ProxyRegistration();
                        else
                            proxy = new ProxyRegistration(ipAddr);
                        string xmlOut = proxy.Service.WeChatStop(item.regDid, amPm, xmlIn);
                        if (!string.IsNullOrEmpty(xmlOut))
                        {
                            DialogBox.Msg(xmlOut);
                        }
                        #endregion

                        proxy = null;
                    }
                }
                Viewer.IsSave = true;
                Viewer.ValueChanged = false;
                DialogBox.Msg("出停诊成功！");
            }
            else
            {
                DialogBox.Msg("出停诊失败。");
            }
        }
        #endregion

        #endregion

    }
}
