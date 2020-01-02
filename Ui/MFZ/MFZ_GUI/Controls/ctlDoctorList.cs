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
    /// ҽ���б�ؼ�
    /// </summary>
    public partial class ctlDoctorList : UserControl
    {

        #region ���캯��

        public ctlDoctorList()
        {
            InitializeComponent();
            //ctlRoomElement.DoctorAdded += new clsDoctorAddedEventHandler(ctlRoomElement_DoctorAdded);
        }
 
        #endregion

        #region ��  ��

        private int m_intDiagnoseAreaID = -1;       //����ID
        private int m_intSchemeID = -1;           //���ID
        private string m_strDeptID = string.Empty; //����ID
        private clsMFZDoctorVO[] m_arrDoctors;   //ҽ���б� 

        #endregion

        #region ��ʼ����ʾ

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
        /// ˢ�½���
        /// </summary>
        public void RefreshList()
        {
            m_mthInitList();
        }

        #endregion

        #region ����������

        /// <summary>
        /// ��������ID,���������ϵ�ҽ��
        /// </summary>
        public int AreaID
        {
            set
            {
                m_intDiagnoseAreaID = value;
            }
        }

        /// <summary>
        /// ���ò���ID�����ز����ϵ�ҽ��
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
        /// ���ð���Id
        /// </summary>
        public int SchemeID 
        {
            set 
            {
                m_intSchemeID = value;
            }
        }

        /// <summary>
        /// ��ȡҽ���б�ListView�ؼ�,����
        /// ������ҽ����ɾ��
        /// </summary>
        public ListView lsvDoctorList
        {
            get
            {
                return m_lsvDoctors;
            }
        } 

        #endregion

        #region ҽ���б���ϷŲ���

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

        #region ��������

        private void AddDoctor(clsMFZDoctorVO doctor)
        {
            ListViewItem item = new ListViewItem(doctor.m_strDoctorName);
            //item.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            if (doctor.m_enmDoctorType == enmMFZDoctorType.Common)
            {
                item.SubItems.Add("�� ͨ");
            }
            if (doctor.m_enmDoctorType == enmMFZDoctorType.Expert)
            {
                item.SubItems.Add("ר ��");
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

        #region �ڹ���վ���ҽ���¼�

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
