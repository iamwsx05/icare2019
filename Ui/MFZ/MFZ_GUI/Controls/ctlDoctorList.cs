using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ.Controls
{

    /// <summary>
    /// 医生列表控件
    /// </summary>
    public partial class ctlDoctorList : UserControl
    {

        #region 构造函数

        public ctlDoctorList()
        {
            InitializeComponent();
            //ctlRoomElement.DoctorAdded += new clsDoctorAddedEventHandler(ctlRoomElement_DoctorAdded);
        }
 
        #endregion

        #region 属  性

        private int m_intDiagnoseAreaID = -1;       //诊区ID
        private int m_intSchemeID = -1;           //班次ID
        private string m_strDeptID = string.Empty; //部门ID
        private clsMFZDoctorVO[] m_arrDoctors;   //医生列表 

        #endregion

        #region 初始化显示

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
            {
                try
                {
                    m_mthInitList();
                }
                catch { }
            }
        }

        private void m_mthInitList()
        {
            clsMFZDoctorVO[] arrTempDocts=new clsMFZDoctorVO[0];
            if (string.IsNullOrEmpty(m_strDeptID)&&m_intDiagnoseAreaID!=-1&&m_intDiagnoseAreaID!=int.MinValue)
            {
                clsTmdDoctorSmp.s_object.m_lngFindNoWorkStationDoctorsByAreaID(m_intDiagnoseAreaID,m_intSchemeID, out m_arrDoctors);
            }
            else
            {
                clsTmdDoctorSmp.s_object.m_lngFindDoctorsByDeptID(m_intDiagnoseAreaID,m_intSchemeID,m_strDeptID, out m_arrDoctors);
            }
            this.m_lsvDoctors.BeginUpdate();
            if (m_arrDoctors == null)
            {
                m_arrDoctors = new clsMFZDoctorVO[0];
            }
            m_lsvDoctors.Items.Clear();
            for (int i = 0; i < m_arrDoctors.Length; i++)
            {
                clsMFZDoctorVO doctor = m_arrDoctors[i];
                AddDoctor(doctor);
            }
            this.m_lsvDoctors.EndUpdate();
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public void RefreshList()
        {
            m_mthInitList();
        }

        #endregion

        #region 公开的属性

        /// <summary>
        /// 设置诊区ID,加载诊区上的医生
        /// </summary>
        public int AreaID
        {
            set
            {
                m_intDiagnoseAreaID = value;
            }
        }

        /// <summary>
        /// 设置部门ID，加载部门上的医生
        /// </summary>
        public string DeptID
        {
            get 
            {
                return m_strDeptID;
            }
            set
            {
                m_strDeptID = value;
            }
        }

        /// <summary>
        /// 设置安排Id
        /// </summary>
        public int SchemeID 
        {
            set 
            {
                m_intSchemeID = value;
            }
        }

        /// <summary>
        /// 获取医生列表ListView控件,用以
        /// 外界控制医生的删除
        /// </summary>
        public ListView lsvDoctorList
        {
            get
            {
                return m_lsvDoctors;
            }
        } 

        #endregion

        #region 医生列表的拖放操作

        private void m_lsvDoctors_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem doctorItem = (ListViewItem)e.Item;
            if (doctorItem!=null&&doctorItem.Tag!=null)
            {
                DragDropEffects effect = DoDragDrop(doctorItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void m_lsvDoctors_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem dragItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            clsMFZDoctorStationVO doctorStation = dragItem.Tag as clsMFZDoctorStationVO;
            if (doctorStation!=null)
            {
                clsMFZDoctorVO doctor = doctorStation.m_objDoctor;
                clsTmdDoctorSmp.s_object.m_lngDelete(doctor.m_strDoctorID,doctor.m_strDeptID,doctor.m_intSchemeSeq);
                doctorStation.m_objDoctor = null;

                if (dragItem.SubItems.Count==2)
                {
                    dragItem.SubItems[1].Text = string.Empty;
                }

                AddDoctor(doctor);
            }
        }

        private void m_lsvDoctors_DragEnter(object sender, DragEventArgs e)
        {
            clsMFZDoctorVO doctor = e.Data.GetData(typeof(clsMFZDoctorVO)) as clsMFZDoctorVO;
            if (doctor!=null)
            {
                e.Effect=DBAssist.IsNull(doctor.m_intRoomID)?DragDropEffects.None:DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.Copy;
        }

        private string GetDeptName(string deptID)
        {
            com.digitalwave.Utility.ctlDeptTextBox deptBox = new com.digitalwave.Utility.ctlDeptTextBox();
            deptBox.m_StrDeptID = deptID;
            return deptBox.m_StrDeptName;
        }

        #endregion

        #region 辅助方法

        private void AddDoctor(clsMFZDoctorVO doctor)
        {
            ListViewItem item = new ListViewItem(doctor.m_strDoctorName);
            //item.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            if (doctor.m_enmDoctorType == enmMFZDoctorType.Common)
            {
                item.SubItems.Add("普 通");
            }
            if (doctor.m_enmDoctorType == enmMFZDoctorType.Expert)
            {
                item.SubItems.Add("专 家");
            }
            item.SubItems.Add(doctor.m_strDeptName);
            m_lsvDoctors.Items.Add(item);

            item.Tag = doctor;
        }

        public void DeleteDoctor(clsMFZDoctorVO doctor)
        {
            foreach (ListViewItem item in m_lsvDoctors.Items)
            {
                clsMFZDoctorVO tempDoctor = item.Tag as clsMFZDoctorVO;
                bool isSame = doctor.m_strDeptID == tempDoctor.m_strDeptID && doctor.m_strDoctorID == tempDoctor.m_strDoctorID;
                if (isSame)
                {
                    this.m_lsvDoctors.Items.Remove(item);
                    return;
                }
            }
        } 

        #endregion

        #region 在工作站添加医生事件

        //private void ctlRoomElement_DoctorAdded(object sender, DoctorAddedEventArgs e)
        //{
        //    if (e.DoctorAdded!=null)
        //    {
        //        this.AddDoctor(e.DoctorAdded);
        //    }
        //    if (e.DoctorDeleted!=null)
        //    {
        //        this.DeleteDoctor(e.DoctorDeleted);
        //    }
        //}

        #endregion
    }
}
