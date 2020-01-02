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
    /// 添加医生
    /// </summary>
    public partial class frmAddDoctor : Form
    {

        #region 构造函数

        public frmAddDoctor(clsMFZDoctorStationVO stationDoctor,string roomDeptId, int schemeId)
        {
            InitializeComponent();
            this.m_station = stationDoctor.m_objStation;
            this.m_oldDoctor = stationDoctor.m_objDoctor;
            this.m_schemeId = schemeId;
            this.m_roomDeptId = roomDeptId;
        }
 
        #endregion

        #region 私有成员

        private clsMFZWorkStationVO m_station;
        private clsMFZDoctorVO m_oldDoctor;
        private clsMFZDoctorVO m_doctor;
        private string m_roomDeptId;
        private int m_schemeId;

        #endregion

        #region 属    性

        public clsMFZDoctorVO Doctor
        {
            get { return m_doctor; }
        } 

        #endregion

        #region 辅助方法

        private void LoadDoctorList(string deptId)
        {
            clsMFZDoctorVO[] m_arrDoctors = new clsMFZDoctorVO[0];

            clsTmdDoctorSmp.s_object.m_lngFindDoctorsByDeptID(deptId, out m_arrDoctors);
            //clsTmdDoctorSmp.s_object.m_lngFindDoctorsByDeptID(GetAreaId(m_station.m_intRoomID), this.m_schemeId, deptId, out m_arrDoctors);
            if (m_arrDoctors.Length > 0)
            {
                m_cboDoctorList.Items.Clear();
                m_cboDoctorList.BeginUpdate();
                m_cboDoctorList.Items.AddRange(m_arrDoctors);
                m_cboDoctorList.EndUpdate();
                m_cboDoctorList.SelectedIndex = 0;
            }
        }

        private int GetAreaId(int roomId)
        {
            clsMFZRoomVO objRoom = null;
            clsTmdRoomSmp.s_object.m_lngFind(roomId, out objRoom);
            return objRoom.m_intAreaId;
        }


        private void Display(clsMFZDoctorVO oldDoctor)
        {
            this.m_txtDeptName.m_StrDeptID = oldDoctor.m_strDeptID;
            foreach (object obj in m_cboDoctorList.Items)
            {
                clsMFZDoctorVO doctor = obj as clsMFZDoctorVO;
                if (doctor != null)
                {
                    if (doctor.m_strDoctorID == oldDoctor.m_strDoctorID)
                    {
                        m_cboDoctorList.SelectedItem = obj; break;
                    }
                }
            }

            clsDoctorDiscrible doctorStyle = new clsDoctorDiscrible(oldDoctor.m_strSummary);

            this.m_chkVisible.Checked = doctorStyle.IsVisible ? true : false;
            this.m_chkReverse.Checked = doctorStyle.IsReverse ? true : false;

            m_txtDeptDesc.Text = doctorStyle.Describle;

            if (oldDoctor.m_enmDoctorType == enmMFZDoctorType.Common)
            {
                this.m_cboDoctorExpert.SelectedIndex = 0;
            }
            if (oldDoctor.m_enmDoctorType == enmMFZDoctorType.Expert)
            {
                this.m_cboDoctorExpert.SelectedIndex = 1;
            }
        }
 
        #endregion

        #region 事件实现

        private void frmAddDoctor_Load(object sender, EventArgs e)
        {
            if (this.m_oldDoctor != null)
            {
                LoadDoctorList(m_oldDoctor.m_strDeptID);
                Display(m_oldDoctor);
                this.m_txtDeptDesc.SelectAll();
            }
            else
            {
                this.m_txtDeptName.m_StrDeptID = m_roomDeptId;
                LoadDoctorList(m_roomDeptId);
            }
        }

        private void m_txtDeptName_TextChanged(object sender, EventArgs e)
        {
            string deptId = m_txtDeptName.m_StrDeptID;
            if (!string.IsNullOrEmpty(deptId))
            {
                LoadDoctorList(deptId);
            }
        }

        private void m_cmdSubmit_Click(object sender, EventArgs e)
        {
            if (m_cboDoctorList.SelectedIndex < 0||m_cboDoctorExpert.SelectedIndex<0||m_txtDeptName.Focused)
            {
                DialogResult = DialogResult.None;
                return;
            }

            this.m_doctor = m_cboDoctorList.Items[m_cboDoctorList.SelectedIndex] as clsMFZDoctorVO;
            if (m_doctor == null) return; 

            m_doctor.m_intRoomID = m_station.m_intRoomID;
            m_doctor.m_intSchemeSeq = m_schemeId;
            m_doctor.m_intWorkStationID = m_station.m_intWorkStationID;


            clsDoctorDiscrible doctorDiscrible = new clsDoctorDiscrible(this.m_txtDeptDesc.Text);
            doctorDiscrible.IsVisible = m_chkVisible.Checked ? true : false;
            doctorDiscrible.IsReverse = m_chkReverse.Checked ? true : false;
            m_doctor.m_strSummary = doctorDiscrible.DbValue;
            
            if (m_cboDoctorExpert.SelectedIndex==0)
            {
                m_doctor.m_enmDoctorType = enmMFZDoctorType.Common;
            }
            if (m_cboDoctorExpert.SelectedIndex == 1)
            {
                m_doctor.m_enmDoctorType = enmMFZDoctorType.Expert;
            }

            if (string.IsNullOrEmpty(m_txtDeptName.m_StrDeptID))
            {
                //MessageBox.Show("科室不能为空！");
                DialogResult = DialogResult.None;
                return;
            }
            if (this.m_doctor == null)
            {
                MessageBox.Show("医生不能为空！");
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.Yes;
        }

        private void frmAddDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_cmdSubmit_Click(sender, e);
            }
        }

        private void m_cboDoctorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_doctor = m_cboDoctorList.Items[m_cboDoctorList.SelectedIndex] as clsMFZDoctorVO;
            if (this.m_doctor != null)
            {
                m_doctor.m_intRoomID = m_station.m_intRoomID;
                m_doctor.m_intSchemeSeq = m_schemeId;
                m_doctor.m_intWorkStationID = m_station.m_intWorkStationID;

                if (m_doctor.m_enmDoctorType == enmMFZDoctorType.Common)
                {
                    this.m_cboDoctorExpert.SelectedIndex = 0;
                }
                if (m_doctor.m_enmDoctorType == enmMFZDoctorType.Expert)
                {
                    this.m_cboDoctorExpert.SelectedIndex = 1;
                }
            }
        }

        #endregion

    }
}