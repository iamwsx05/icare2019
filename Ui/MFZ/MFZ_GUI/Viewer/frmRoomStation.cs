using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// 诊间维护
    /// </summary>
    public partial class frmRoomStation : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {

        #region 构造函数

        public frmRoomStation()
        {
            InitializeComponent();
        }

        public frmRoomStation(int areaId)
        {
            InitializeComponent();
            this.areaId = areaId;
        } 

        #endregion

        #region 私有成员

        private bool m_blnNewRoom = true;
        private static Hashtable hasAreas=new Hashtable(); //保存诊区列表信息
        private int areaId;

        #endregion

        #region 辅助方法

        private void m_mthLoadRoom()
        {
            GetAreas();
            ctlCboDiagnoseArea.m_intAreaID = this.areaId;

            //this.m_txtRoomName.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsMFZRoomVO[] objRoomsArr = null;
            clsTmdRoomSmp.s_object.m_lngFind(out objRoomsArr);
            if (objRoomsArr == null)
            {
                objRoomsArr = new clsMFZRoomVO[0];
            }
            m_lsvRooms.Tag = objRoomsArr;

            //填充列表
            m_mthShowRoomList(objRoomsArr);

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 将诊区信息载入
        /// </summary>
        private  void GetAreas()
        {
            if (hasAreas.Count==0)
            {
                clsMFZDiagnoseAreaVO[] p_objReadArr;
                clsTmdDiagnoseAreaSmp.s_object.m_lngFind(out p_objReadArr);
                if (p_objReadArr != null && p_objReadArr.Length > 0)
                {
                    foreach (clsMFZDiagnoseAreaVO area in p_objReadArr)
                    {
                        hasAreas.Add(area.m_intDiagnoseAreaID, area.m_strDiagnoseAreaName);
                    }
                }
            }
        }

        private void m_mthShowRoomList(clsMFZRoomVO[] objRoomsArr)
        {
            this.m_lsvRooms.BeginUpdate();//开始更新列表
            this.m_lsvRooms.Items.Clear();
            if (objRoomsArr != null)
            {
                foreach (clsMFZRoomVO room in objRoomsArr)
                {
                    ListViewItem item = m_ConstructListViewItemByVO(room);
                    this.m_lsvRooms.Items.Add(item);
                }
            }
            //重置状态标志
            //this.m_blnNewRoom = false;
            //清空明细
            m_mthCMDetailClear();

            this.m_lsvRooms.EndUpdate();//结束更新列表
        }

        private ListViewItem m_ConstructListViewItemByVO(clsMFZRoomVO Room)
        {
            ListViewItem item = new ListViewItem(hasAreas[Room.m_intAreaId].ToString());
            item.SubItems.Add(m_strGetDeptName(Room.m_strDeptID));
            item.SubItems.Add(Room.m_strRoomName);
            item.SubItems.Add(Room.m_strSummary);
            item.Tag = Room;
            return item;
        }

        /// <summary>
        /// 清空明细
        /// </summary>
        private void m_mthCMDetailClear()
        {
            this.m_txtRoomName.Clear();
            this.m_txtSummary.Clear();
        }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private string m_strGetDeptName(string deptID)
        {
            com.digitalwave.Utility.ctlDeptTextBox deptBox = new com.digitalwave.Utility.ctlDeptTextBox();
            deptBox.m_StrDeptID = deptID;
            return deptBox.m_StrDeptName;
        }

        private string m_strGetAreaName(int roomID)
        {
            clsMFZDiagnoseAreaVO diagArea;
            clsTmdDiagnoseAreaSmp.s_object.m_lngFind(roomID, out diagArea);
            if (diagArea != null)
            {
                return diagArea.m_strDiagnoseAreaName;
            }
            return string.Empty;
        }

        private string ValidMessage() 
        {
            if (this.m_txtRoomName.Text.Trim()==string.Empty)
            {
                return "诊室名称不能为空！";
            }
            if (this.ctlCboDiagnoseArea.SelectedIndex==0)
            {
                return "请选择诊室所在诊区！";
            }
            if (this.m_txtDeptName.Text.Trim()==string.Empty)
            {
                return "请设置默认科室名称！";
            }
            return string.Empty;
        }

        #endregion

        #region 事件实现

        private void frmRoomStation_Load(object sender, EventArgs e)
        {
            m_mthLoadRoom();
            m_txtRoomName.Focus();
        }

        private void m_lsvRoom_Click(object sender, EventArgs e)
        {
            if (this.m_lsvRooms.FocusedItem == null)
                return;

            //变更状态标志
            this.m_blnNewRoom = false;

            clsMFZRoomVO objRoom = (clsMFZRoomVO)this.m_lsvRooms.FocusedItem.Tag;
            this.m_txtRoomName.Text = objRoom.m_strRoomName;
            this.m_txtRoomName.Enabled = true;

            ctlCboDiagnoseArea.m_intAreaID = objRoom.m_intAreaId;
            m_txtDeptName.m_StrDeptID = objRoom.m_strDeptID;

        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            this.m_txtRoomName.Enabled = true;
            m_txtRoomName.Focus();
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvRooms.FocusedItem != null)
            {
                this.m_lsvRooms.FocusedItem.Selected = false;
                this.m_lsvRooms.FocusedItem.Focused = false;
            }
            //ctlCboDept.SelectedIndex = -1;
            //ctlCboDiagnoseArea.SelectedIndex = -1;
            //清空明细
            m_mthCMDetailClear();

            //设置新增标志
            this.m_blnNewRoom = true;
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvRooms.FocusedItem == null
              && !this.m_blnNewRoom)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            string msg=ValidMessage();
            if (msg!=string.Empty)
            {
                MessageBox.Show(msg);
                this.m_cmdSave.Enabled = true;
                return;
            }

            if (this.m_blnNewRoom)
            {//新增的保存
                clsMFZRoomVO objRoom = new clsMFZRoomVO();
                objRoom.m_intAreaId = this.ctlCboDiagnoseArea.m_intAreaID;
                objRoom.m_strRoomName = this.m_txtRoomName.Text.Trim();
                objRoom.m_strDeptID = m_txtDeptName.m_StrDeptID;
                objRoom.m_strSummary = m_txtSummary.Text;
                long lngRes = clsTmdRoomSmp.s_object.m_lngInsert(objRoom);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewRoom = false;
                    //加入到集合
                    clsMFZRoomVO[] objRooms = (clsMFZRoomVO[])this.m_lsvRooms.Tag;
                    clsMFZRoomVO[] objRoomsNewArr = new clsMFZRoomVO[objRooms.Length + 1];
                    objRooms.CopyTo(objRoomsNewArr, 0);
                    objRoomsNewArr[objRoomsNewArr.Length - 1] = objRoom;
                    this.m_lsvRooms.Tag = objRoomsNewArr;
                    //添加新项
                    ListViewItem item = m_ConstructListViewItemByVO(objRoom);
                    this.m_lsvRooms.Items.Add(item);

                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvRoom_Click(null, null);

                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }

                this.m_blnNewRoom = true;
                this.m_txtRoomName.Focus();
            }
            else
            {//修改的保存
                clsMFZRoomVO objRoom = (clsMFZRoomVO)this.m_lsvRooms.FocusedItem.Tag;

                clsMFZRoomVO objNewRoom = new clsMFZRoomVO();
                objRoom.m_mthCopyTo(objNewRoom);
                objNewRoom.m_strRoomName = this.m_txtRoomName.Text.Trim();
                objNewRoom.m_intAreaId = this.ctlCboDiagnoseArea.m_intAreaID;
                objNewRoom.m_strDeptID = this.m_txtDeptName.m_StrDeptID;
                objNewRoom.m_strSummary = m_txtSummary.Text;

                long lngRes = clsTmdRoomSmp.s_object.m_lngUpdate(objNewRoom);

                if (lngRes > 0)
                {//成功
                    objNewRoom.m_mthCopyTo(objRoom);

                    this.m_lsvRooms.FocusedItem.Text = objRoom.m_intRoomID.ToString();

                    this.m_lsvRooms.FocusedItem.SubItems[0].Text = this.ctlCboDiagnoseArea.SelectedDiagnoseAreaVO.m_strDiagnoseAreaName;
                    this.m_lsvRooms.FocusedItem.SubItems[1].Text = m_strGetDeptName(objRoom.m_strDeptID);
                    this.m_lsvRooms.FocusedItem.SubItems[2].Text = objRoom.m_strRoomName;
                    this.m_lsvRooms.FocusedItem.SubItems[3].Text = objRoom.m_strSummary;
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
            if (this.m_lsvRooms.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;
            clsMFZRoomVO objRoom = (clsMFZRoomVO)this.m_lsvRooms.FocusedItem.Tag;
            clsMFZRoomVO objCopy = new clsMFZRoomVO();
            objRoom.m_mthCopyTo(objCopy);

            //删除诊室,判断诊室下面的工作站是否为空!
            clsMFZWorkStationVO[] stations = null;
            clsTmdWorkStationSmp.s_object.m_lngFind(objCopy.m_intRoomID,out stations);
            if (stations!=null&&stations.Length>0)
            {
                MessageBox.Show("诊室删除失败,诊室下面的工作站不为空!");
                this.m_cmdDelete.Enabled = true;
                return;
            }

            long lngRes = clsTmdRoomSmp.s_object.m_lngDelete(objCopy.m_intRoomID);

            if (lngRes > 0)
            {//成功
                int intIdx = this.m_lsvRooms.FocusedItem.Index;

                this.m_lsvRooms.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvRooms.Items.Count)
                {
                    this.m_lsvRooms.Items[intIdx].Selected = true;
                    this.m_lsvRooms.Items[intIdx].Focused = true;
                    this.m_lsvRoom_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvRooms.Items[intIdx - 1].Selected = true;
                    this.m_lsvRooms.Items[intIdx - 1].Focused = true;
                    this.m_lsvRoom_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clsMFZWorkStationVO workStation = new clsMFZWorkStationVO();
            clsMFZRoomVO roomVO = m_lsvRooms.FocusedItem.Tag as clsMFZRoomVO;
            if (roomVO != null)
            {
                workStation.m_intRoomID = roomVO.m_intRoomID;
                frmStation station = new frmStation(workStation);
                station.RoomName = roomVO.m_strRoomName;
                station.Show();
            }

        }

        private void m_lsvRooms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool isAsc = false;
            ListView lstTemp = m_lsvRooms;
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

        private void frmRoomStation_KeyDown(object sender, KeyEventArgs e)
        {
            base.m_mthSetKeyTab(e);

            if (e.KeyCode == Keys.F3)
            {
                m_cmdNew_Click(this, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                m_cmdSave_Click(this, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                m_cmdDelete_Click(this, e);
            }
        }

        /// <summary>
        /// 诊区改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlCboDiagnoseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.m_txtRoomName.Enabled = true;
            //if (ctlCboDiagnoseArea.m_intAreaID != DBAssist.NullInt)
            //{
            //    ctlCboDept.m_intDiagnoseAreaID = ctlCboDiagnoseArea.m_intAreaID;
            //}
        } 

        #endregion

    }

    #region 数据库消息类
    public class clsCommonDialog
    {
        public static void m_mthShowDBError()
        {
            MessageBox.Show("数据库访问出错！", "iCare");
        }
        public static void m_mthShowNoAccordantResult()
        {
            MessageBox.Show("没有符合条件的记录！", "iCare");
        }
    } 
    #endregion
}