using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// ҽ���Ļ����Ŷ��б�
    /// </summary>
    public partial class ctlDocPatientWaitQueue : UserControl
    {
        public ctlDocPatientWaitQueue()
        {
            InitializeComponent();
        }

        #region ˽�г�Ա

        private clsDoctor doctor = null;
        private List<clsMFZPatientVO> firstQueue = new List<clsMFZPatientVO>(); //ҽ���ϵĻ��߶���
        private clsQueueManage manage;
        private string roomName;
        private int calledCount;
        private string deptId;
        private bool isYiJiArea; //�Ƿ���ҽ������
        
        #endregion

        #region �� ��

        public string DeptId
        {
            set { deptId = value; }
        }
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        /// <summary>
        /// �й����߸���
        /// </summary>
        public int CalledCount
        {
            set
            {
                calledCount=value;
                this.doctor.calledCount = calledCount;
            }
        }
        public List<clsMFZPatientVO> FirstQueue
        {
            set
            {
                if (value != null)
                {
                    firstQueue = value;
                }
            }
        }
        public clsDoctor Doctor
        {
            set
            {
                if (value != null)
                {
                    doctor = value;
                    //m_lblDoctorName.Text = doctor.strDoctName;
                }
            }
            get
            {
                return doctor;
            }
        }
        public string DoctHeaderText
        {
            set
            {
                m_lblDeptRoomDocName.Text = value;
            }
        }
        public string DoctorID
        {
            get
            {
                return doctor.strDoctID;
            }
        }
        public ListView lsvPatientQueue
        {
            get
            {
                return m_lsvPatientQueue;
            }
        }
        public clsQueueManage Manage
        {
            set
            {
                if (value != null)
                {
                    manage = value;
                }
            }
        }
        /// <summary>
        /// �����Ƿ���ҽ������
        /// </summary>
        public bool IsYiJiArea 
        {
            set 
            {
                isYiJiArea = value;
            }
        }

        #endregion

        #region ��������

       

        #endregion

        #region �趨����/���ߵ�ͼƬ

        public void SetOnline()
        {
            this.m_lblDeptRoomDocName.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.men_online;
        }

        public void SetOffline()
        {
            this.m_lblDeptRoomDocName.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.men_offline;
        } 

        #endregion

        #region Drag/Dropʵ��

        private void m_lsvPatientQueue_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem dragItem = (ListViewItem)e.Item;
            if (dragItem.Tag != null)
            {
                DragDropEffects effect = DoDragDrop(dragItem, DragDropEffects.Move);
            }
        }

        private void m_lsvPatientQueue_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void m_lsvPatientQueue_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem dragItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            string strQueueID = dragItem.ListView.Tag as string;

            clsMFZPatientVO dragPatient = (clsMFZPatientVO)dragItem.Tag;
            int dargIndex = dragItem.Index;

            Point p = m_lsvPatientQueue.PointToClient(new Point(e.X, e.Y));
            ListViewItem dropItem = m_lsvPatientQueue.GetItemAt(p.X, p.Y);
            if (dropItem == null && strQueueID != this.doctor.strDoctID)
            {
                manage.MovePatient(dragPatient, strQueueID, this.doctor.strDoctID, m_lsvPatientQueue.Items.Count);
            }
            int dropIndex = dropItem.Index;
            manage.MovePatient(dragPatient, strQueueID, doctor.strDoctID, dropIndex);
        } 

        #endregion

        #region ������ʾ��ͷ��ʽ

        /// <summary>
        /// ����ҽ��ͷ��ʽ
        /// </summary>
        private void SetTeJiHeadTitle()
        {
            m_lblRoomName.Visible = false;
            this.gradientPanel1.Dock = DockStyle.Fill;
            this.splitContainer1.Height = 145;
            this.splitContainer1.SplitterDistance = 45;

            string calledCount = this.calledCount.ToString().PadLeft(2, '0');

            string projectName = string.Empty;
            if (doctor == null) return;

            clsDoctorDiscrible doctorStyle = new clsDoctorDiscrible(doctor.strSummary);
            if (doctor.doctorType == enmMFZDoctorType.Common)
            {
                projectName = doctorStyle.GetDescrible("��ͨ");
            }
            if (doctor.doctorType == enmMFZDoctorType.Expert)
            {
                projectName = doctorStyle.GetDescrible("ר��");
            }

            m_lblDeptRoomDocName.Text = string.Format("          ����:{0}\n          ��Ŀ:{1}\n          {2}��{3}��", this.roomName, projectName, doctor.strDoctName, this.calledCount);
            m_lblRoomName.Text = this.roomName;
        }

        public void SetHeadTitle()
        {
            if (isYiJiArea)
            {
                SetTeJiHeadTitle();
            }
            else
            {
                string calledCount = this.calledCount.ToString().PadLeft(2, '0');

                string projectName = string.Empty;
                if (doctor == null) return;

                clsDoctorDiscrible doctorStyle = new clsDoctorDiscrible(doctor.strSummary);
                if (doctor.doctorType == enmMFZDoctorType.Common)
                {
                    projectName = doctorStyle.GetDescrible("��ͨ");
                }
                if (doctor.doctorType == enmMFZDoctorType.Expert)
                {
                    projectName = doctorStyle.GetDescrible("ר��");
                }

                m_lblDeptRoomDocName.Text = string.Format("                   ��Ŀ:{0}\n                   {1}��{2}��", projectName, doctor.strDoctName, this.calledCount);
                m_lblRoomName.Text = this.roomName;
            }
        } 

        #endregion
    }

    /// <summary>
    /// ���ҵ�����
    /// </summary>
    public class PatientQueueComparer : IComparer<ctlDocPatientWaitQueue> 
    {
        public int Compare(ctlDocPatientWaitQueue patientQueue1,ctlDocPatientWaitQueue patientQueue2) 
        {
            return string.Compare(patientQueue1.RoomName, patientQueue2.RoomName);
        }
    }
}
