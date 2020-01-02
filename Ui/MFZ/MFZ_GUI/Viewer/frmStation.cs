using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// 医生工作站维护
    /// </summary>
    public partial class frmStation : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {

        public frmStation(clsMFZWorkStationVO workStation)
        {
            InitializeComponent();
            this.m_objworkStation = workStation;
        }

        #region 私有成员

        private clsMFZWorkStationVO m_objworkStation;
        private bool m_blnNewWorkStation = true;
        private string m_strRoomName = string.Empty;
        private clsMFZWorkStationVO m_objWorkStation;
        
        #endregion

        #region 属 性

        public clsMFZWorkStationVO WorkStation
        {
            get { return m_objWorkStation; }
        }

        public string RoomName
        {
            set
            {
                m_strRoomName = value;
            }
        }
        
        #endregion

        #region 一般设置

        #region 快捷键设置

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            //if (p_eumKeyCode == Keys.F4)
            //{
            //    if (this.m_btnQuery.Enabled == true
            //        && this.m_btnQuery.Visible == true)
            //    {
            //        this.m_btnQuery_Click(this.m_btnQuery, null);
            //    }
            //}
            //else if (p_eumKeyCode == Keys.F8)
            //{
            //    if (this.btnPrint.Enabled == true
            //        && this.btnPrint.Visible == true)
            //    {
            //        this.btnPrint_Click(this.btnPrint, null);
            //    }
            //}
            //else if (p_eumKeyCode == Keys.F3 && this.m_btnPreference.Enabled && m_btnPreference.Visible)//保存
            //{
            //    this.m_btnPreference_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F5 && this.m_btnPrintReport.Enabled && m_btnPrintReport.Visible)//读卡
            //{
            //    this.m_btnPrintReport_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F6 && this.m_btnPreviewReport.Enabled && m_btnPreviewReport.Visible)		//退出
            //{
            //    this.m_btnPreviewReport_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F9 && this.m_btnConfirmReport.Enabled && m_btnConfirmReport.Visible)		//清除
            //{
            //    this.m_btnConfirmReport_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F10 && this.m_btnSaveReport.Enabled && m_btnSaveReport.Visible)//手输和读卡机切换
            //{
            //    this.m_btnSaveReport_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F12 && this.m_btnDelete.Enabled && m_btnDelete.Visible)//手输和读卡机切换
            //{
            //    this.m_btnDelete_Click(null, null);
            //}
            //else if (p_eumKeyCode == Keys.F8 && this.m_btnNewApp.Enabled && m_btnNewApp.Visible)//手输和读卡机切换
            //{
            //    this.m_btnNewApp_Click(null, null);
            //}
            //			else if(p_eumKeyCode==Keys.F12 && this.m_btnInputSwitch.Enabled && m_btnInputSwitch.Visible)//手输和读卡机切换
            //			{
            //				this.m_btnInputSwitch_Click(null,null);
            //			}
        }

        #endregion

        private void frmStation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_mthShortCutKey(e.KeyCode);

            base.m_mthSetKeyTab(e);

            if (e.KeyCode == Keys.F3)
            {
                m_cmdNew_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                m_cmdSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                m_cmdDelete_Click(sender, e);
            }
        }

        #endregion

        #region 辅助方法

        //清空明细
        private void m_mthDetailClear()
        {
            this.m_txtWorkStationName.Clear();
            this.m_strWorkStationDesc.Clear();
            this.m_strSammary.Clear();
        }

        private void m_mthLoadWorkStation()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsMFZWorkStationVO[] objWorkStationsArr = null;
            clsTmdWorkStationSmp.s_object.m_lngFind(this.m_objworkStation.m_intRoomID, out objWorkStationsArr);
            if (objWorkStationsArr == null)
            {
                objWorkStationsArr = new clsMFZWorkStationVO[0];
            }
            m_lsvWorkStations.Tag = objWorkStationsArr;

            //填充列表
            m_mthShowWorkStationList(objWorkStationsArr);

            Cursor.Current = Cursors.Default;
        }

        private void m_mthShowWorkStationList(clsMFZWorkStationVO[] objWorkStationsArr)
        {
            this.m_lsvWorkStations.BeginUpdate();//开始更新列表
            this.m_lsvWorkStations.Items.Clear();
            if (objWorkStationsArr != null)
            {
                foreach (clsMFZWorkStationVO WorkStation in objWorkStationsArr)
                {
                    ListViewItem item = new ListViewItem(WorkStation.m_intWorkStationID.ToString());
                    item.SubItems.Add(WorkStation.m_strWorkStationName);
                    item.SubItems.Add(WorkStation.m_strWorkStationDesc);
                    item.Tag = WorkStation;
                    this.m_lsvWorkStations.Items.Add(item);
                }
            }
            //重置状态标志
            //this.m_blnNewWorkStation = false;
            //清空明细
            m_mthDetailClear();

            this.m_lsvWorkStations.EndUpdate();//结束更新列表
        }

        private string ValidMessage() 
        {
            if (this.m_txtWorkStationName.Text==string.Empty)
            {
                return "计算机名称不能为空！";
            }
            return string.Empty;
        }
        
        #endregion

        #region 事件实现

        private void m_lsvWorkStation_Click(object sender, EventArgs e)
        {
            m_txtWorkStationName.Enabled = true;
            if (this.m_lsvWorkStations.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewWorkStation = false;

            clsMFZWorkStationVO objWorkStation = (clsMFZWorkStationVO)this.m_lsvWorkStations.FocusedItem.Tag;
            this.m_txtWorkStationName.Text = objWorkStation.m_strWorkStationName;
            this.m_strWorkStationDesc.Text = objWorkStation.m_strWorkStationDesc;
            this.m_strSammary.Text = objWorkStation.m_strSummary;
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            m_txtWorkStationName.Enabled = true;
            m_txtWorkStationName.Focus();
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvWorkStations.FocusedItem != null)
            {
                this.m_lsvWorkStations.FocusedItem.Selected = false;
                this.m_lsvWorkStations.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthDetailClear();

            //设置光标焦点
            //this.m_cmdSave.Focus();

            //设置新增标志
            this.m_blnNewWorkStation = true;
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkStations.FocusedItem == null
              && !this.m_blnNewWorkStation)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            string msg = ValidMessage();
            if (msg!=string.Empty)
            {
                this.m_cmdSave.Enabled = true;
                MessageBox.Show(msg);
                return;
            }


            if (this.m_blnNewWorkStation)
            {//新增的保存
                clsMFZWorkStationVO objWorkStation = new clsMFZWorkStationVO();
                objWorkStation.m_strWorkStationName = this.m_txtWorkStationName.Text.Trim();
                objWorkStation.m_strWorkStationDesc = this.m_strWorkStationDesc.Text;
                objWorkStation.m_intRoomID = m_objworkStation.m_intRoomID;
                objWorkStation.m_strSummary = this.m_strSammary.Text;

                long lngRes = clsTmdWorkStationSmp.s_object.m_lngInsert(objWorkStation);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewWorkStation = false;
                    m_objWorkStation = objWorkStation;//诊室添加工作站用到
                    //加入到集合
                    clsMFZWorkStationVO[] objWorkStations = (clsMFZWorkStationVO[])this.m_lsvWorkStations.Tag;
                    clsMFZWorkStationVO[] objWorkStationsNewArr = new clsMFZWorkStationVO[objWorkStations.Length + 1];
                    objWorkStations.CopyTo(objWorkStationsNewArr, 0);
                    objWorkStationsNewArr[objWorkStationsNewArr.Length - 1] = objWorkStation;
                    this.m_lsvWorkStations.Tag = objWorkStationsNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objWorkStation.m_intWorkStationID.ToString());
                    item.SubItems.Add(objWorkStation.m_strWorkStationName);
                    item.SubItems.Add(objWorkStation.m_strWorkStationDesc);


                    item.Tag = objWorkStation;
                    this.m_lsvWorkStations.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvWorkStation_Click(null, null);

                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
                this.m_blnNewWorkStation = true;
                this.m_txtWorkStationName.Focus();
            }
            else
            {//修改的保存
                clsMFZWorkStationVO objWorkStation = (clsMFZWorkStationVO)this.m_lsvWorkStations.FocusedItem.Tag;

                clsMFZWorkStationVO objNewWorkStation = new clsMFZWorkStationVO();
                objWorkStation.m_mthCopyTo(objNewWorkStation);
                objNewWorkStation.m_strWorkStationName = this.m_txtWorkStationName.Text.Trim();
                objNewWorkStation.m_strWorkStationDesc = this.m_strWorkStationDesc.Text.Trim();
                objNewWorkStation.m_strSummary = this.m_strWorkStationDesc.Text.Trim();

                long lngRes = clsTmdWorkStationSmp.s_object.m_lngUpdate(objNewWorkStation);

                if (lngRes > 0)
                {//成功
                    objNewWorkStation.m_mthCopyTo(objWorkStation);
                    m_objWorkStation = objWorkStation;//诊室添加工作站用到

                    this.m_lsvWorkStations.FocusedItem.Text = objWorkStation.m_intWorkStationID.ToString();
                    this.m_lsvWorkStations.FocusedItem.SubItems[1].Text = objWorkStation.m_strWorkStationName;
                    this.m_lsvWorkStations.FocusedItem.SubItems[2].Text = objWorkStation.m_strWorkStationDesc;
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkStations.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;
            clsMFZWorkStationVO objWorkStation = (clsMFZWorkStationVO)this.m_lsvWorkStations.FocusedItem.Tag;
            clsMFZWorkStationVO objCopy = new clsMFZWorkStationVO();
            objWorkStation.m_mthCopyTo(objCopy);

            // 删除工作站,判断关联工作站的医生是否为空!
            clsMFZDoctorVO[] doctors = null;
            clsTmdDoctorSmp.s_object.m_lngFindDoctorsByStationId(objCopy.m_intWorkStationID, out doctors);
            if (doctors != null && doctors.Length > 0)
            {
                this.m_cmdDelete.Enabled = true;
                MessageBox.Show("工作站删除失败,工作站下的医生不为空!");
                return;
            }

            long lngRes = clsTmdWorkStationSmp.s_object.m_lngDelete(objCopy.m_intWorkStationID);

            if (lngRes > 0)
            {//成功
                int intIdx = this.m_lsvWorkStations.FocusedItem.Index;

                this.m_lsvWorkStations.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvWorkStations.Items.Count)
                {
                    this.m_lsvWorkStations.Items[intIdx].Selected = true;
                    this.m_lsvWorkStations.Items[intIdx].Focused = true;
                    this.m_lsvWorkStation_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvWorkStations.Items[intIdx - 1].Selected = true;
                    this.m_lsvWorkStations.Items[intIdx - 1].Focused = true;
                    this.m_lsvWorkStation_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvWorkStations_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool isAsc = false;
            ListView lstTemp = m_lsvWorkStations;
            if (lstTemp.Sorting == SortOrder.Ascending)
            {
                lstTemp.Sorting = SortOrder.Descending;
            }
            else
            {
                lstTemp.Sorting = SortOrder.Ascending;
                isAsc = true;
            }
            lstTemp.ListViewItemSorter = new ListViewItemComparer(e.Column, isAsc, lstTemp);
            lstTemp.Sort();
        }

        private void frmStation_Load(object sender, EventArgs e)
        {
            this.m_lblRoomName.Text = m_strRoomName;
            m_mthLoadWorkStation();

            this.m_mthSetEnter2Tab(new Control[] { });
        }

        private void m_lsvWorkStations_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        
        #endregion
    }
}