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
    /// 班次计划
    /// </summary>
    public partial class frmScheme : Form
    {
        public frmScheme()
        {
            InitializeComponent();
        }

        #region 私有成员

        bool m_blnNewScheme = false; 
        
        #endregion

        #region 辅助方法

        // 填充列表
        private void m_mthShowSchemeList(clsMFZSchemeVO[] p_arrSchemes)
        {
            m_lsvScheme.BeginUpdate();
            m_lsvScheme.Tag = p_arrSchemes;
            for (int i = 0; i < p_arrSchemes.Length; i++)
            {
                ListViewItem item = new ListViewItem(p_arrSchemes[i].m_strSchemeDesc);

                item.SubItems.Add(m_strWeekDay(p_arrSchemes[i].m_intWeekDay));
                item.SubItems.Add(p_arrSchemes[i].m_dtBegin.ToString("HH:mm"));
                item.SubItems.Add(p_arrSchemes[i].m_dtEnd.ToString("HH:mm"));
                item.Tag = p_arrSchemes[i];
                m_lsvScheme.Items.Add(item);
            }
            m_lsvScheme.EndUpdate();
        }

        private string m_strWeekDay(int weekDay)
        {
            string msg = string.Empty;
            switch (weekDay.ToString())
            {

                case "1":
                    msg = "星期一";
                    break;
                case "2":
                    msg = "星期二";
                    break;
                case "3":
                    msg = "星期三";
                    break;
                case "4":
                    msg = "星期四";
                    break;
                case "5":
                    msg = "星期五";
                    break;
                case "6":
                    msg = "星期六";
                    break;
                case "7":
                    msg = "星期日";
                    break;
                default:
                    break;
            }
            return msg;
        }

        // 控件显示VO
        private void m_mthControlsShowVO(clsMFZSchemeVO scheme)
        {
            this.m_txtSchemeName.Text = scheme.m_strSchemeDesc;
            this.m_cboWeekDay.SelectedIndex = scheme.m_intWeekDay - 1;
            this.m_dtpStartTime.Text = scheme.m_dtBegin.ToString("HH:mm");
            this.m_dtpEndTime.Text = scheme.m_dtEnd.ToString("HH:mm");
        }

        private void m_mthDetailClear()
        {
            this.m_txtSchemeName.Text = string.Empty;
            this.m_cboWeekDay.SelectedIndex = 0;
            this.m_dtpStartTime.Text = DateTime.Now.ToString("HH:mm");
            this.m_dtpEndTime.Text = DateTime.Now.ToString("HH:mm");
        } 

        #endregion
        
        #region 事件实现

        private void frmScheme_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.m_txtSchemeName.Enabled = false;

            //加载数据
            clsMFZSchemeVO[] objSchemesArr = null;
            clsTmdSchemeSmp.s_object.m_lngFind(out objSchemesArr);
            if (objSchemesArr == null)
            {
                objSchemesArr = new clsMFZSchemeVO[0];
            }
            m_lsvScheme.Tag = objSchemesArr;

            //填充列表
            m_mthShowSchemeList(objSchemesArr);

            Cursor.Current = Cursors.Default;
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            this.m_txtSchemeName.Enabled = true;
            m_txtSchemeName.Focus();
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvScheme.FocusedItem != null)
            {
                this.m_lsvScheme.FocusedItem.Selected = false;
                this.m_lsvScheme.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthDetailClear();

            //设置光标焦点
            //this.m_cmdSave.Focus();

            //设置新增标志
            this.m_blnNewScheme = true;
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvScheme.FocusedItem == null
             && !this.m_blnNewScheme)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            if (this.m_blnNewScheme)
            {//新增的保存
                clsMFZSchemeVO objScheme = new clsMFZSchemeVO();
                objScheme.m_strSchemeDesc = this.m_txtSchemeName.Text.Trim();
                objScheme.m_intWeekDay = m_cboWeekDay.SelectedIndex+1;
                objScheme.m_dtBegin = m_dtpStartTime.Value;
                objScheme.m_dtEnd=m_dtpEndTime.Value;
                long lngRes = clsTmdSchemeSmp.s_object.m_lngInsert(objScheme);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewScheme = false;
                    //加入到集合
                    clsMFZSchemeVO[] objSchemes = (clsMFZSchemeVO[])this.m_lsvScheme.Tag;
                    clsMFZSchemeVO[] objSchemesNewArr = new clsMFZSchemeVO[objSchemes.Length + 1];
                    objSchemes.CopyTo(objSchemesNewArr, 0);
                    objSchemesNewArr[objSchemesNewArr.Length - 1] = objScheme;
                    this.m_lsvScheme.Tag = objSchemesNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objScheme.m_strSchemeDesc);

                    item.SubItems.Add(m_strWeekDay(objScheme.m_intWeekDay));
                    item.SubItems.Add(objScheme.m_dtBegin.ToString("HH:mm"));
                    item.SubItems.Add(objScheme.m_dtEnd.ToString("HH:mm"));
                    item.Tag = objScheme;
                    m_lsvScheme.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvScheme_SelectedIndexChanged(null, null);
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//修改的保存
                clsMFZSchemeVO objScheme = (clsMFZSchemeVO)this.m_lsvScheme.FocusedItem.Tag;

                clsMFZSchemeVO objNewScheme = new clsMFZSchemeVO();
                objScheme.m_mthCopyTo(objNewScheme);
                objNewScheme.m_strSchemeDesc = this.m_txtSchemeName.Text.Trim();
                objNewScheme.m_intWeekDay = m_cboWeekDay.SelectedIndex+1;
                objNewScheme.m_dtBegin = m_dtpStartTime.Value;
                objNewScheme.m_dtEnd = m_dtpEndTime.Value;
                long lngRes = clsTmdSchemeSmp.s_object.m_lngUpdate(objNewScheme);

                if (lngRes > 0)
                {//成功
                    objNewScheme.m_mthCopyTo(objScheme);

                    this.m_lsvScheme.FocusedItem.Text = objScheme.m_strSchemeDesc;
                    this.m_lsvScheme.FocusedItem.SubItems[1].Text = m_strWeekDay(objScheme.m_intWeekDay);
                    this.m_lsvScheme.FocusedItem.SubItems[2].Text = objScheme.m_dtBegin.ToString("HH:mm");
                    this.m_lsvScheme.FocusedItem.SubItems[3].Text = objScheme.m_dtEnd.ToString("HH:mm");
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
            if (this.m_lsvScheme.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;



            clsMFZSchemeVO objScheme = (clsMFZSchemeVO)this.m_lsvScheme.FocusedItem.Tag;
            clsMFZSchemeVO objCopy = new clsMFZSchemeVO();
            objScheme.m_mthCopyTo(objCopy);

            //删除班次安排,判断安排的医生是否为空
            clsMFZDoctorVO[] doctors=null;
            clsTmdDoctorSmp.s_object.m_lngFindDoctorsBySchemeId(objCopy.m_intSchemeSeq,out doctors);
            if (doctors!=null&&doctors.Length>0)
            {
                MessageBox.Show("班次计划删除失败,该班次安排的医生不为空!");
                return;
            }

            long lngRes = clsTmdSchemeSmp.s_object.m_lngDelete(objCopy.m_intSchemeSeq);

            if (lngRes > 0)
            {//成功
                int intIdx = this.m_lsvScheme.FocusedItem.Index;

                this.m_lsvScheme.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvScheme.Items.Count)
                {
                    this.m_lsvScheme.Items[intIdx].Selected = true;
                    this.m_lsvScheme.Items[intIdx].Focused = true;
                    this.m_lsvScheme_SelectedIndexChanged(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvScheme.Items[intIdx - 1].Selected = true;
                    this.m_lsvScheme.Items[intIdx - 1].Focused = true;
                    this.m_lsvScheme_SelectedIndexChanged(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_txtSchemeName.Enabled = true;
            if (m_lsvScheme.FocusedItem == null)
            {
                return;
            }

            m_blnNewScheme = false;
            clsMFZSchemeVO Scheme = m_lsvScheme.FocusedItem.Tag as clsMFZSchemeVO;
            if (Scheme != null)
            {
                m_mthControlsShowVO(Scheme);
            }
        }

        private void m_cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_cboTime.SelectedIndex)
            {
                case 0:
                    this.m_dtpStartTime.Text = "8:00";
                    this.m_dtpEndTime.Text = "11:59";
                    break;
                case 1:
                    this.m_dtpStartTime.Text = "12:00";
                    this.m_dtpEndTime.Text = "17:59";
                    break;
                case 2:
                    this.m_dtpStartTime.Text = "18:00";
                    this.m_dtpEndTime.Text = "23:59";
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}