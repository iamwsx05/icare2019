using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Diagnostics;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// 诊室自定义控件(工作站,医生)
    /// </summary>
    public partial class ctlRoomElement : UserControl
    {

        #region 默认构造函数

        public ctlRoomElement()
        {
            InitializeComponent();
            DoctorMoved += new DoctorMovedEventHandler(ctlRoomElement_DoctorMoved);
        }

        #endregion

        #region 私有成员

        private clsMFZRoomVO m_objRoom = new clsMFZRoomVO();　　//　诊室信息
        private clsMFZDoctorStationVO[] m_arrDoctorStations=new clsMFZDoctorStationVO[0];　//　医生－工作站列表
        private Hashtable m_hasDepts=new Hashtable();
        private int m_intSchemeID = -1;

        internal static event DoctorMovedEventHandler DoctorMoved;

        #endregion

        #region 属  性

        /// <summary>
        /// 设置诊间实例
        /// </summary>
        public clsMFZRoomVO Room
        {
            set
            {
                if (value != null)
                {
                    m_objRoom = value;
                    com.digitalwave.Utility.ctlDeptTextBox deptBox = new com.digitalwave.Utility.ctlDeptTextBox();
                    deptBox.m_StrDeptID = m_objRoom.m_strDeptID;
                    m_lblRoomName.Text = string.Format("         诊室:{0}\n         科室:{1}", m_objRoom.m_strRoomName, deptBox.m_StrDeptName);

                    clsTmdWorkStationSmp.s_object.m_lngFindDoctorStations(m_objRoom.m_intRoomID, m_intSchemeID, out m_arrDoctorStations);
                    if (m_arrDoctorStations == null)
                    {
                        m_arrDoctorStations = new clsMFZDoctorStationVO[0];
                    }
                    m_mthInitList();
                }
            }
        }

        /// <summary>
        /// 获取诊间包含医生和工作站的ListView
        /// </summary>
        public ListView lsvRoom
        {
            get
            {
                return this.m_lsvRoom;
            }
        }

        /// <summary>
        /// 设置班次Id
        /// </summary>
        public int SchemeID
        {
            set
            {
                m_intSchemeID = value;
            }
        }

        #endregion

        #region 加载列表

        private void m_mthInitList()
        {
            if (!this.Created)
            {
                m_lsvRoom.BeginUpdate();
                for (int i = 0; i < m_arrDoctorStations.Length; i++)
                {
                    clsMFZDoctorStationVO doctorStation = m_arrDoctorStations[i];
                    clsMFZDoctorVO doctor = doctorStation.m_objDoctor;
                    clsMFZWorkStationVO station = doctorStation.m_objStation;
                    string stationName;
                    if (station.m_strWorkStationName.Length > 8)
                    {
                        stationName = station.m_strWorkStationName.Substring(8, station.m_strWorkStationName.Length - 8);
                    }
                    else 
                    {
                        stationName = station.m_strWorkStationName;
                    }
                    ListViewItem item = new ListViewItem(stationName);
                    if (doctor != null)
                    {
                        item.SubItems.Add(doctor.m_strDoctorName);
                    }
                    m_lsvRoom.Items.Add(item);
                    item.Tag = doctorStation;
                }
                m_lsvRoom.EndUpdate();
            }
        }

        #endregion

        #region 拖放操作

        private void m_lsvRoom_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void m_lsvRoom_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem doctorItem = (ListViewItem)e.Item;
            if (doctorItem.Tag != null)
            {
                DragDropEffects effect = DoDragDrop(doctorItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void m_lsvRoom_DragDrop(object sender, DragEventArgs e)
        {
            LoadDeptShorts();

            Point cp = m_lsvRoom.PointToClient(new Point(e.X, e.Y));
            ListViewItem dropItem = m_lsvRoom.GetItemAt(cp.X, cp.Y);
            if (dropItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            dropItem.Selected = true;

            ListViewItem dragItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            ListView lsvSource = dragItem.ListView;
            clsMFZDoctorStationVO doctorStationFromRoom = dragItem.Tag as clsMFZDoctorStationVO;
            clsMFZDoctorVO doctorFromList = dragItem.Tag as clsMFZDoctorVO;

            clsMFZDoctorStationVO oldRoomDoctorStation = dropItem.Tag as clsMFZDoctorStationVO;
            clsMFZDoctorVO oldRoomDoctor = oldRoomDoctorStation.m_objDoctor;

            //诊室是否分配医生
            int doctCount = GetRoomDoctorCount();

            #region 来自医生列表
            if (doctorFromList != null)
            {

                clsDoctorMovedArgEvents eMove = new clsDoctorMovedArgEvents(doctorFromList,m_objRoom.m_intAreaId,m_intSchemeID,oldRoomDoctorStation.m_objStation.m_intWorkStationID);
                if (DoctorMoved!=null)
                {
                    DoctorMoved(this, eMove);
                    if (eMove.Cancel)
                    {
                        MessageBox.Show(string.Format("该医生已添加到诊室:{0}！", eMove.RoomName));
                        return;
                    }
                } 

                // 工作站没分配医生
                if (oldRoomDoctor == null)
                {
                    if (doctCount != 0 && doctorFromList.m_strDeptID != m_objRoom.m_strDeptID)
                    {
                        MessageBox.Show("当前移动的医生和诊室中医生不属于同一科室!");
                        return;
                    }
                    else if (doctorFromList.m_strDeptID != m_objRoom.m_strDeptID)
                    {
                        if (MessageBox.Show("该医生所在科室和诊室的默认科室不一致,是否继续?", "医生设置", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        m_objRoom.m_strDeptID = doctorFromList.m_strDeptID;
                        SetRoomTitle(m_objRoom.m_strRoomName, doctorFromList.m_strDeptName);
                        
                    }

                    dragItem.Remove();
                    doctorFromList.m_intRoomID = oldRoomDoctorStation.m_objStation.m_intRoomID;
                    doctorFromList.m_intWorkStationID = oldRoomDoctorStation.m_objStation.m_intWorkStationID;
                    doctorFromList.m_intSchemeSeq = m_intSchemeID;
                    doctorFromList.m_strSummary =(string)m_hasDepts[doctorFromList.m_strDeptID];
                    clsTmdDoctorSmp.s_object.m_lngInsert(doctorFromList);
                    oldRoomDoctorStation.m_objDoctor = doctorFromList;

                    m_mthModifyDropItem(dropItem, doctorFromList);
                }
                // 工作站已分配医生
                else
                {
                    if (doctorFromList.m_strDeptID != m_objRoom.m_strDeptID)
                    {
                        MessageBox.Show("当前移动的医生和诊室中医生不属于同一科室!");
                        return;
                    }

                    dragItem.Remove();
                    doctorFromList.m_intRoomID = oldRoomDoctor.m_intRoomID;
                    doctorFromList.m_intWorkStationID = oldRoomDoctor.m_intWorkStationID;
                    doctorFromList.m_intSchemeSeq = m_intSchemeID;

                    clsTmdDoctorSmp.s_object.m_lngInsert(doctorFromList);
                    oldRoomDoctorStation.m_objDoctor = doctorFromList;
                    clsTmdDoctorSmp.s_object.m_lngDelete(oldRoomDoctor.m_strDoctorID, oldRoomDoctor.m_strDeptID, oldRoomDoctor.m_intSchemeSeq);

                    m_mthModifyDropItem(dropItem, doctorFromList);

                    // 改变医生列表信息
                    oldRoomDoctor.m_intRoomID = DBAssist.NullInt;
                    oldRoomDoctor.m_intWorkStationID = DBAssist.NullInt;
                    ListViewItem item = new ListViewItem();
                    item.Tag = oldRoomDoctor;
                    item.Text = oldRoomDoctor.m_strDoctorName;



                    if (oldRoomDoctor.m_enmDoctorType == enmMFZDoctorType.Expert)
                    {
                        item.SubItems.Add("专 家");
                    }
                    else if (oldRoomDoctor.m_enmDoctorType == enmMFZDoctorType.Common)
                    {
                        item.SubItems.Add("普 通");
                    }
                    //--
                    item.SubItems.Add(oldRoomDoctor.m_strDeptName);
                    lsvSource.Items.Add(item);
                }

                
            }
            #endregion

            #region 来自诊室
            else if (doctorStationFromRoom != null)
            {
                if (oldRoomDoctor == null)
                {
                    if (doctorStationFromRoom.m_objDoctor.m_strDeptID != m_objRoom.m_strDeptID)
                    {
                        if (GetRoomDoctorCount() > 0)
                        {
                            MessageBox.Show("当前移动的医生所在科室与诊室中的医生科室不相同！");
                            return;
                        }
                        
                        if (MessageBox.Show("该医生所在科室和诊室的默认科室不一致,是否继续?", "医生设置", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        m_objRoom.m_strDeptID = doctorStationFromRoom.m_objDoctor.m_strDeptID;
                        SetRoomTitle(m_objRoom.m_strRoomName, doctorStationFromRoom.m_objDoctor.m_strDeptName);
                    }



                    clsMFZDoctorVO newDoctor = doctorStationFromRoom.m_objDoctor;
                    newDoctor.m_intRoomID = oldRoomDoctorStation.m_objStation.m_intRoomID;
                    newDoctor.m_intWorkStationID = oldRoomDoctorStation.m_objStation.m_intWorkStationID;

                    clsTmdDoctorSmp.s_object.m_lngUpdate(newDoctor);
                    oldRoomDoctorStation.m_objDoctor = newDoctor;

                    clsMFZDoctorStationVO tempDoctorStation = new clsMFZDoctorStationVO();
                    tempDoctorStation.m_objStation = doctorStationFromRoom.m_objStation;
                    tempDoctorStation.m_objDoctor = null;
                    dragItem.Tag = tempDoctorStation;
                    dragItem.SubItems[1].Text = string.Empty;

                    m_mthModifyDropItem(dropItem, newDoctor);
                }
                else
                {
                    clsMFZDoctorVO dragDoctor = doctorStationFromRoom.m_objDoctor;
                    // 同一个人
                    if (dragDoctor.m_strDoctorID == oldRoomDoctor.m_strDoctorID && dragDoctor.m_strDeptID == oldRoomDoctor.m_strDeptID)
                    {

                        //MessageBox.Show("医生不能分配到其他科室！");
                        return;
                    }

                    if (dragDoctor.m_strDeptID!=m_objRoom.m_strDeptID&&GetRoomDoctorCount()>0)
                    {
                        MessageBox.Show("当前移动的医生所在科室与诊室中的医生科室不相同！");
                        return;
                    }

                    dragDoctor.m_intRoomID = oldRoomDoctorStation.m_objStation.m_intRoomID;
                    dragDoctor.m_intWorkStationID = oldRoomDoctorStation.m_objStation.m_intWorkStationID;
                    oldRoomDoctorStation.m_objDoctor = dragDoctor;

                    oldRoomDoctor.m_intRoomID = doctorStationFromRoom.m_objStation.m_intRoomID;
                    oldRoomDoctor.m_intWorkStationID = doctorStationFromRoom.m_objStation.m_intWorkStationID;
                    doctorStationFromRoom.m_objDoctor = oldRoomDoctor;

                    clsTmdDoctorSmp.s_object.m_lngUpdate(dragDoctor);
                    clsTmdDoctorSmp.s_object.m_lngUpdate(oldRoomDoctor);

                    dragItem.Tag = doctorStationFromRoom;
                    dropItem.Tag = oldRoomDoctorStation;

                    dragItem.SubItems[1].Text = oldRoomDoctor.m_strDoctorName;
                    dropItem.SubItems[1].Text = dragDoctor.m_strDoctorName;
                    return;
                }
            }
            #endregion

        }

        /// <summary>
        /// 加载部门Id-部门简称对应表
        /// </summary>
        private void LoadDeptShorts()
        {
            if (m_hasDepts.Count == 0)
            {
                clsMFZDeptVO[] depts;
                clsTmdDeptSmp.s_object.m_lngFind(out depts);
                if (depts == null)
                {
                    depts = new clsMFZDeptVO[0];
                }

                foreach (clsMFZDeptVO dept in depts)
                {
                    if (!m_hasDepts.Contains(dept.m_strDeptID))
                    {
                        m_hasDepts.Add(dept.m_strDeptID, dept.m_strDeptNameShort);
                    }
                }
            }
        }

        /// <summary>
        /// 获取诊室医生个数
        /// </summary>
        /// <returns></returns>
        private int GetRoomDoctorCount()
        {
            int doctCount = 0;
            foreach (ListViewItem item in lsvRoom.Items)
            {
                clsMFZDoctorStationVO doctorStation = item.Tag as clsMFZDoctorStationVO;
                if (doctorStation!=null&&doctorStation.m_objDoctor!=null)
                {
                    doctCount++;
                }
            }
            return doctCount;
        }

        /// <summary>
        /// 把数据修改到ListviewItem上
        /// </summary>
        /// <param name="dropItem"></param>
        private void m_mthModifyDropItem(ListViewItem dropItem, clsMFZDoctorVO doctor)
        {
            if (dropItem.SubItems.Count == 2)
            {
                dropItem.SubItems[1].Text = doctor.m_strDoctorName;
            }
            else
            {
                dropItem.SubItems.Add(doctor.m_strDoctorName);
            }
        }

        #endregion

        #region 设置头样式

        private void SetRoomTitle(string roomName, string deptName)
        {
            m_lblRoomName.Text = string.Format("         诊室:{0}\n         科室:{1}", roomName, deptName);
        } 

        #endregion

        #region 诊室上添加医生工作站

        private void m_lblAdd_Click(object sender, EventArgs e)
        {
            clsMFZWorkStationVO workStation = new clsMFZWorkStationVO();
            if (m_objRoom == null) return;

            workStation.m_intRoomID = m_objRoom.m_intRoomID;
            frmStation station = new frmStation(workStation);
            station.RoomName = m_objRoom.m_strRoomName;
            if (station.ShowDialog() == DialogResult.Yes)
            {
                bool isAdd = true;
                clsMFZWorkStationVO tempStation = station.WorkStation;
                if (workStation != null && tempStation != null)
                {
                    foreach (ListViewItem item in m_lsvRoom.Items)
                    {
                        clsMFZDoctorStationVO doctTemp = item.Tag as clsMFZDoctorStationVO;
                        if (doctTemp != null)
                        {
                            if (doctTemp.m_objStation.m_intWorkStationID == tempStation.m_intWorkStationID)
                            {
                                item.SubItems[0].Text = tempStation.m_strWorkStationName;
                                isAdd = false;
                                break;
                            }
                        }
                    }

                    if (isAdd)
                    {
                        clsMFZDoctorStationVO doctorStation = new clsMFZDoctorStationVO();
                        doctorStation.m_objDoctor = null;
                        doctorStation.m_objStation = tempStation;
                        ListViewItem item = new ListViewItem(tempStation.m_strWorkStationName);
                        m_lsvRoom.Items.Add(item);
                        item.Tag = doctorStation;
                    }
                }
                else
                {
                    MessageBox.Show("保存不成功!");
                }
            }
        }

        #endregion
        
        #region 添加医生

        private void m_lsvRoom_DoubleClick(object sender, EventArgs e)
        {
            clsMFZDoctorStationVO doctorStation = lsvRoom.FocusedItem.Tag as clsMFZDoctorStationVO;
            if (doctorStation == null) return;

            clsMFZWorkStationVO station = doctorStation.m_objStation;

            frmAddDoctor frmDoctor = new frmAddDoctor(doctorStation,m_objRoom.m_strDeptID, m_intSchemeID);
            Point p = lsvRoom.Parent.PointToScreen(lsvRoom.FocusedItem.Position);
            frmDoctor.Location = new Point(p.X, p.Y + 20);

            if (frmDoctor.ShowDialog() != DialogResult.Yes) return;

            clsMFZDoctorVO addDoctor = frmDoctor.Doctor;
            clsMFZDoctorVO stationDoctor = doctorStation.m_objDoctor;
            long res;

            // 医生已经被其他工作站添加
            if (IsDoctorAdded(station, addDoctor)) return;

            // 医生工作站没有分配医生
            if (stationDoctor == null)
            {
                res = clsTmdDoctorSmp.s_object.m_lngInsert(addDoctor);
                if (res > 0)
                {
                    m_objRoom.m_strDeptID = addDoctor.m_strDeptID;
                }
                else
                {
                    MessageBox.Show("添加医生失败！");
                }
            }
            else 
            {
                // 和原来是同一个医生（DeptId和DoctorId一致）
                if (addDoctor.m_strDoctorID == stationDoctor.m_strDoctorID && addDoctor.m_strDeptID == stationDoctor.m_strDeptID)
                {
                    res = clsTmdDoctorSmp.s_object.m_lngUpdate(addDoctor);
                    if (res > 0)
                    {
                        m_objRoom.m_strDeptID = addDoctor.m_strDeptID;
                    }
                    else
                    {
                        MessageBox.Show("添加医生失败！");
                    }
                }
                else 
                {
                    res = clsTmdDoctorSmp.s_object.m_lngDelete(stationDoctor.m_strDoctorID, stationDoctor.m_strDeptID, stationDoctor.m_intSchemeSeq);
                    if (res<0)
                    {
                        MessageBox.Show("添加医生失败！");
                        return;
                    }
                    res=clsTmdDoctorSmp.s_object.m_lngInsert(addDoctor);
                    if (res>0)
                    {
                        m_objRoom.m_strDeptID = addDoctor.m_strDeptID;
                    }
                    else
                    {
                        MessageBox.Show("添加医生失败！");
                    }
                }
            }

            SetRoomTitle(m_objRoom.m_strRoomName, addDoctor.m_strDeptName);
            doctorStation.m_objDoctor = addDoctor;
            m_mthModifyDropItem(lsvRoom.FocusedItem, addDoctor);

            /*

            if (addDoctor.m_strDeptID != m_objRoom.m_strDeptID && GetRoomDoctorCount() > 0)
            {
                MessageBox.Show("当前添加的医生和诊室中医生不属于同一科室!");
                return;
            }

            if (addDoctor.m_strDeptID != m_objRoom.m_strDeptID)
            {
                if (MessageBox.Show("该医生所在科室和诊室的默认科室不一致,是否继续?", "医生设置", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            long res = clsTmdDoctorSmp.s_object.m_lngInsert(addDoctor);
            if (res > 0)
            {
                m_objRoom.m_strDeptID = addDoctor.m_strDeptID;
                SetRoomTitle(m_objRoom.m_strRoomName, addDoctor.m_strDeptName);

                clsDoctorMovedArgEvents eMove = new clsDoctorMovedArgEvents(addDoctor, m_objRoom.m_intAreaId, m_intSchemeID, station.m_intWorkStationID);
                if (DoctorMoved != null)
                {
                    DoctorMoved(this, eMove);
                    if (eMove.Cancel)
                    {
                        MessageBox.Show(string.Format("该医生已添加到诊室:{0}！", eMove.RoomName));
                        return;
                    }
                }

                doctorStation.m_objDoctor = addDoctor;
                m_mthModifyDropItem(lsvRoom.FocusedItem, addDoctor);
            }
            else
            {
                MessageBox.Show("添加医生失败！");
            }
             
             */
        }

        /// <summary>
        /// 医生是否已经添加
        /// </summary>
        /// <param name="station"></param>
        /// <param name="addDoctor"></param>
        private bool IsDoctorAdded(clsMFZWorkStationVO station, clsMFZDoctorVO addDoctor)
        {
            clsDoctorMovedArgEvents eMove = new clsDoctorMovedArgEvents(addDoctor, m_objRoom.m_intAreaId, m_intSchemeID, station.m_intWorkStationID);
            if (DoctorMoved != null)
            {
                DoctorMoved(this, eMove);
                if (eMove.Cancel)
                {
                    MessageBox.Show(string.Format("该医生已添加到诊室:{0}！", eMove.RoomName));
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 自定义事件实现

        private void ctlRoomElement_DoctorMoved(object sender, clsDoctorMovedArgEvents e)
        {
            if (!e.Cancel)
            {
                foreach (ListViewItem item in this.m_lsvRoom.Items)
                {
                    clsMFZDoctorStationVO doctorStation = item.Tag as clsMFZDoctorStationVO;
                    if (doctorStation != null && doctorStation.m_objDoctor != null&&sender!=this)
                    {
                        bool isCheck = doctorStation.m_objDoctor.m_strDoctorID == e.Doctor.m_strDoctorID
                            && m_objRoom.m_intAreaId == e.AreaId
                            && this.m_intSchemeID == e.SchemeId
                            && doctorStation.m_objStation.m_intWorkStationID != e.WorkStationId;
                        if (isCheck)
                        {
                            e.RoomName = m_objRoom.m_strRoomName;
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private void ctlRoomElement_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
            {
                new com.digitalwave.Utility.clsLogText().LogError(string.Format("###### {0}", this.m_objRoom.m_intRoomID));
                DoctorMoved -= new DoctorMovedEventHandler(ctlRoomElement_DoctorMoved);
            }
        }  

        #endregion
    }

    #region 工作站添加、移动医生事件

    internal delegate void DoctorMovedEventHandler(object sender,clsDoctorMovedArgEvents e);
    internal class clsDoctorMovedArgEvents:EventArgs
    {
        private clsMFZDoctorVO m_docotr;
        private bool m_cancel=false;

        public bool Cancel
        {
            get { return m_cancel; }
            set { m_cancel = value; }
        }

        public clsMFZDoctorVO Doctor
        {
            get { return m_docotr; }
            set { m_docotr = value; }
        }

        private int m_workStationId;

        public int WorkStationId
        {
            get { return m_workStationId; }
        }

        private int m_areaId;

        public int AreaId
        {
            get { return m_areaId; }
        }

        private int m_schemeId;

        public int SchemeId
        {
            get { return m_schemeId; }
        }

        private string m_roomName;

        public string RoomName
        {
            get { return m_roomName; }
            set { m_roomName = value; }
        }

	


        public clsDoctorMovedArgEvents(clsMFZDoctorVO doctor,int areaId,int shemeId,int workStationId) 
        {
            this.m_docotr = doctor;
            this.m_workStationId = workStationId;
            this.m_schemeId = shemeId;
            this.m_areaId = areaId;
        }
    }
    #endregion
}
