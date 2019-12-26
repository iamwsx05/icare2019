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
    /// 医生-诊室工作站设置界面
    /// </summary>
    public partial class frmDoctor : Form
    {

        #region 构造函数

        public frmDoctor()
        {
            InitializeComponent();
        }
 
        #endregion

        #region 属 性

        private clsMFZRoomVO[] m_arrRooms; //显示的诊室集合
        private int m_intAreaID = -1;   //诊区Id
        private string m_strDeptID = string.Empty; //部门Id
        private int m_intSchemeID = -1; //班次Id

        #endregion

        #region 显示诊室

        /// <summary>
        /// 显示诊室列表
        /// </summary>
        /// <param name="arrRooms"></param>
        private void m_mthShowRooms(clsMFZRoomVO[] arrRooms)
        {
            clsMFZRoomVO[] arrTemp = arrRooms;
            m_flpMain.Controls.Clear();
            if (arrTemp == null)
            {
                arrTemp = new clsMFZRoomVO[0];
            }

            for (int i = 0; i < arrTemp.Length; i++)
            {
                ctlRoomElement roomElement = new ctlRoomElement();
                roomElement.SchemeID = m_intSchemeID;
                roomElement.Room = arrRooms[i];
                m_flpMain.Controls.Add(roomElement);
            }
            
        }
        
        #endregion

        #region 事件实现

        /// <summary>
        /// 导入班次安排
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdImportScheme_Click(object sender, EventArgs e)
        {
            if (m_intAreaID == -1 || m_intAreaID == int.MinValue || m_intSchemeID == -1)
            {
                MessageBox.Show("未选择诊区或班次!");
                return;
            }

            Point p = m_cmdImportScheme.Parent.PointToScreen(m_cmdImportScheme.Location);
            frmSelectScheme scheme = new frmSelectScheme(m_intAreaID, m_intSchemeID);
            scheme.Location = new Point(p.X - 100, p.Y + 35);
            if (scheme.ShowDialog() == DialogResult.Yes)
            {
                ctlCboScheme.SchemeID = m_intSchemeID;
            }
        }

        /// <summary>
        /// 添加诊室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdAddRoom_Click(object sender, EventArgs e)
        {
            if (m_intAreaID == -1 || m_intAreaID == int.MinValue || m_intSchemeID == -1)
            {
                MessageBox.Show("未选择诊区或班次!");
                return;
            }
            if (string.IsNullOrEmpty(m_strDeptID))
            {
                MessageBox.Show("未选择科室!");
                return;
            }

            Point p = m_cmdAddRoom.Parent.PointToScreen(m_cmdAddRoom.Location);
            frmAddRoom room = new frmAddRoom(ctlCboDiagnoseArea.m_intAreaID,m_txtDeptName.m_StrDeptID, m_txtDeptName.m_StrDeptName, ctlCboDiagnoseArea.SelectedDiagnoseAreaVO.m_strDiagnoseAreaName);
            room.Location = new Point(p.X - 80, p.Y + 35);
            if (room.ShowDialog() == DialogResult.Yes)
            {
                ctlCboScheme.SchemeID = m_intSchemeID;
            }
        }

        /// <summary>
        /// 删除当前班次的排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdDelScheme_Click(object sender, EventArgs e)
        {
            if (m_intAreaID == -1 || m_intAreaID == int.MinValue || m_intSchemeID == -1)
            {
                MessageBox.Show("未选择诊区或班次!");
                return;
            }

            if (MessageBox.Show("确认要删除当前班次安排吗?", "确认删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                long res = clsTmdDoctorSmp.s_object.m_lngDelete(m_intAreaID, m_intSchemeID);
                if (res != 0)
                {
                    MessageBox.Show("删除当前安排成功!");
                    ctlCboScheme.SchemeID = m_intSchemeID;
                }
                else
                {
                    MessageBox.Show("删除当前安排失败!");
                }
            }
        }

        /// <summary>
        /// 诊区选择更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlCboDiagnoseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDocotorList();

            clsTmdRoomSmp.s_object.m_lngFindByAreaID(m_intAreaID,m_intSchemeID, out m_arrRooms);
            m_mthShowRooms(m_arrRooms);
        }

        /// <summary>
        /// 部门选择更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlCboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDocotorList();
        }

        /// <summary>
        /// 班次选择更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlCboScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_intSchemeID = ctlCboScheme.SchemeID;

            if (m_txtDeptName.m_StrDeptID == string.Empty || m_txtDeptName.Tag==null)
            {
                if (ctlCboDiagnoseArea.SelectedIndex == 0 || ctlCboDiagnoseArea.SelectedIndex == -1)
                {
                    return;
                }

                RefreshDocotorList();

                clsTmdRoomSmp.s_object.m_lngFindByAreaID(m_intAreaID,m_intSchemeID, out m_arrRooms);
                m_mthShowRooms(m_arrRooms);
                return;
            }
            else
            {
                RefreshDocotorList();

                //显示所有诊室
                clsTmdRoomSmp.s_object.m_lngFindByAreaID(m_intAreaID, m_intSchemeID, out m_arrRooms);
                m_mthShowRooms(m_arrRooms);
            }
        }

        private string m_oldDeptId;
        private void m_txtDeptName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (m_txtDeptName.Text.Trim() == string.Empty || m_txtDeptName.m_StrDeptID != null)
                {
                    m_oldDeptId = m_txtDeptName.m_StrDeptID;
                    RefreshDocotorList();
                }
                else 
                {
                    m_txtDeptName.m_lsvList.Show();
                }
            }
        }

        private void m_txtDeptName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_txtDeptName.m_StrDeptID) && m_oldDeptId != m_txtDeptName.m_StrDeptID)
            {
                RefreshDocotorList();
            }
        }

        private void frmDoctor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_flpMain.Controls.Clear();
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 刷新医生列表
        /// </summary>
        private void RefreshDocotorList()
        {
            m_intAreaID = ctlCboDiagnoseArea.m_intAreaID;
            m_strDeptID = m_txtDeptName.m_StrDeptID;
            m_intSchemeID = ctlCboScheme.SchemeID;

            m_ctlDoctors.AreaID = ctlCboDiagnoseArea.m_intAreaID;
            m_ctlDoctors.DeptID = m_txtDeptName.m_StrDeptID;
            m_ctlDoctors.SchemeID =  ctlCboScheme.SchemeID;

            m_ctlDoctors.RefreshList();
        } 
        #endregion
        
    }
}