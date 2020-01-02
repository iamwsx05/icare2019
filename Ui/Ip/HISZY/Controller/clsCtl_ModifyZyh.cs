using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �޸�סԺ�ſ�����
    /// </summary>
    public class clsCtl_ModifyZyh : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// �޸�סԺ�ſ�����
        /// </summary>
        public clsCtl_ModifyZyh()
        {
            objSvc = new clsDcl_ModifyZyh();
        }

        #region ����
        /// <summary>
        /// סԺ��
        /// </summary>
        private string Zyh = "";
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_ModifyZyh objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmModifyZyh m_objViewer;
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmModifyZyh)frmMDI_Child_Base_in;
        }
        #endregion

        #region �ж��º��Ƿ��Ѵ���
        /// <summary>
        /// �ж��º��Ƿ��Ѵ���
        /// </summary>
        /// <param name="newno"></param>
        /// <returns></returns>
        private bool m_blnCheckNewNO(string newno)
        {
            return this.objSvc.m_blnCheckNewNO(newno);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="type">���ͣ� 1 ���ҵ�ǰ��Ժ���ˣ�2 ������ʷסԺ����</param>
        public void m_mthFind(int type)
        {
            string title = "";
            if (type == 1)
            {
                title = "������Ժ��������";
            }
            else if (type == 2)
            {
                title = "���ҳ�Ժ��������";
            }
            else
            {
                return;
            }

            int tmp = 1;
            if (type == 2)
            {
                tmp = 3;
            }

            frmCommonFind ff = new frmCommonFind(title, tmp);            
            if (ff.ShowDialog() == DialogResult.OK)
            {                
                this.m_mthSetval(ff.Zyh, type);
                if (type == 1)
                {
                    this.m_mthCheckHisinfo(ff.PatientID, ff.InType);
                }
            }
        }
        #endregion

        #region �����Ϣ
        /// <summary>
        /// �����Ϣ
        /// </summary>
        /// <param name="type">1 ��Ժ 2 ��Ժ</param>
        private void m_mthClearinfo(int type)
        {
            if (type == 1)
            {
                this.m_objViewer.lblzyh1.Text = "";                
                this.m_objViewer.lblzycs1.Text = "";                
                this.m_objViewer.lblname1.Text = "";                
                this.m_objViewer.lblsex1.Text = "";                
                this.m_objViewer.lblbirthday1.Text = "";                
                this.m_objViewer.lblidcard1.Text = "";                
                this.m_objViewer.lbladdress1.Text = "";                
                this.m_objViewer.lblintype1.Text = "";                
                this.m_objViewer.lblindate1.Text = "";                
                this.m_objViewer.lblarea1.Text = "";                
                this.m_objViewer.lblbed1.Text = "";                
                this.m_objViewer.lblstatus1.Text = "";                
            }
            else if (type == 2)
            {                
                this.m_objViewer.lblzyh2.Text = "";             
                this.m_objViewer.lblzycs2.Text = "";             
                this.m_objViewer.lblname2.Text = "";             
                this.m_objViewer.lblsex2.Text = "";             
                this.m_objViewer.lblbirthday2.Text = "";             
                this.m_objViewer.lblidcard2.Text = "";             
                this.m_objViewer.lbladdress2.Text = "";             
                this.m_objViewer.lblintype2.Text = "";             
                this.m_objViewer.lblindate2.Text = "";             
                this.m_objViewer.lblarea2.Text = "";             
                this.m_objViewer.lblbed2.Text = "";             
                this.m_objViewer.lbloutarea.Text = "";
                this.m_objViewer.lbloutdate.Text = "";
            }
        }
        #endregion               

        #region ���ݲ���ID�͵�ǰ��Ժ����(��ͨסԺ������סԺ)��ȡ��Ӧ��(���ۡ�סԺ)��ʷ��¼
        /// <summary>
        /// ���ݲ���ID�͵�ǰ��Ժ����(��ͨסԺ������סԺ)��ȡ��Ӧ��(���ۡ�סԺ)��ʷ��¼
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="type"></param>        
        /// <returns></returns>
        public void m_mthCheckHisinfo(string pid, int type)
        {            
            DataTable dt = new DataTable();

            if (type == 1)
            {
                type = 2;
            }
            else if (type == 2)
            {
                type = 1;
            }

            long l = this.objSvc.m_lngGetHistoryinfoByPID(pid, type, out dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                string s = "";
                if (type == 1)
                {
                    s = "�ò����С�" + dt.Rows.Count.ToString() + "������ͨ��Ժ��ʷ��¼���Ƿ�������һ�μ�¼��Ϣ��";
                }
                else if (type == 2)
                {
                    s = "�ò����С�" + dt.Rows.Count.ToString() + "����������Ժ��ʷ��¼���Ƿ�������һ�μ�¼��Ϣ��";
                }

                if (MessageBox.Show(s, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {                    
                    this.m_mthSetval(dt.Rows[0]["inpatientid_chr"].ToString().Trim(), 2);
                }
            }            
        }           
        #endregion

        #region ��ֵ
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="zyh">סԺ(����)��</param>
        /// <param name="type">1 ��Ժ 2 ��Ժ</param>
        public void m_mthSetval(string zyh, int type)
        {            
            if (zyh.Trim() == "")
            {
                return;
            }                      

            #region ��Ժ������Ϣ
            if (type == 1)
            {
                DataTable dt;
                long l = this.objSvc.m_lngGetPatientinfoByZyh(zyh, out dt, 1);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    this.m_mthClearinfo(1);
                    this.m_mthClearinfo(2);

                    this.m_objViewer.lblzyh1.Text = zyh;
                    //����Ժ�Ǽ���ˮ��
                    this.m_objViewer.lblzyh1.Tag = dt.Rows[0]["registerid_chr"].ToString().Trim(); 
                    this.m_objViewer.lblzycs1.Text = dt.Rows[0]["inpatientcount_int"].ToString();                    
                    this.m_objViewer.lblname1.Text = dt.Rows[0]["lastname_vchr"].ToString().Trim();
                    //�没��ID
                    this.m_objViewer.lblname1.Tag = dt.Rows[0]["patientid_chr"].ToString().Trim();
                    this.m_objViewer.lblsex1.Text = dt.Rows[0]["sex_chr"].ToString().Trim();
                    this.m_objViewer.lblbirthday1.Text = Convert.ToDateTime(dt.Rows[0]["birth_dat"]).ToString("yyyy��MM��dd��");
                    this.m_objViewer.lblidcard1.Text = dt.Rows[0]["idcard_chr"].ToString().Trim();
                    this.m_objViewer.lbladdress1.Text = dt.Rows[0]["homeaddress_vchr"].ToString().Trim();
                    if (dt.Rows[0]["inpatientnotype_int"].ToString() == "1")
                    {
                        this.m_objViewer.lblintype1.Text = "��ͨסԺ";
                        this.m_objViewer.cboType.SelectedIndex = 0;
                        this.m_objViewer.cboType.Tag = "סԺ��";
                    }
                    else if (dt.Rows[0]["inpatientnotype_int"].ToString() == "2")
                    {
                        this.m_objViewer.lblintype1.Text = "����סԺ";
                        this.m_objViewer.cboType.SelectedIndex = 2;
                        this.m_objViewer.cboType.Tag = "���ۺ�";
                    }
                    else
                    {
                        this.m_objViewer.lblintype1.Text = "δ֪";
                    }
                    this.m_objViewer.lblindate1.Text = dt.Rows[0]["rysj"].ToString().Trim();
                    this.m_objViewer.lblarea1.Text = dt.Rows[0]["deptname_vchr"].ToString().Trim();
                    if (dt.Rows[0]["code_chr"].ToString().Trim() == "")
                    {
                        this.m_objViewer.lblstatus1.Text = "δ���Ŵ�λ";
                    }
                    else
                    {
                        this.m_objViewer.lblbed1.Text = dt.Rows[0]["code_chr"].ToString().Trim();
                        this.m_objViewer.lblstatus1.Text = "�Ѱ��Ŵ�λ";
                    }

                    if (this.m_objViewer.lblzycs1.Text == "1")
                    {
                        this.m_objViewer.txtNewNO.Enabled = true;
                    }
                    else
                    {
                        this.m_objViewer.txtNewNO.Enabled = false;
                    }
                }
            }
            #endregion

            #region ��Ժ������Ϣ
            else if (type == 2)
            {
                DataTable dt;
                long l = this.objSvc.m_lngGetPatientinfoByZyh(zyh, out dt, 3);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    this.m_mthClearinfo(type);

                    this.m_objViewer.lblzyh2.Text = zyh;
                    //���Ժ���˱��
                    this.m_objViewer.lblzyh2.Tag = dt.Rows[0]["patientid_chr"].ToString().Trim();
                    this.m_objViewer.lblzycs2.Text = dt.Rows[0]["inpatientcount_int"].ToString();
                    this.m_objViewer.lblname2.Text = dt.Rows[0]["lastname_vchr"].ToString().Trim();
                    this.m_objViewer.lblsex2.Text = dt.Rows[0]["sex_chr"].ToString().Trim();
                    this.m_objViewer.lblbirthday2.Text = Convert.ToDateTime(dt.Rows[0]["birth_dat"]).ToString("yyyy��MM��dd��");
                    this.m_objViewer.lblidcard2.Text = dt.Rows[0]["idcard_chr"].ToString().Trim();
                    this.m_objViewer.lbladdress2.Text = dt.Rows[0]["homeaddress_vchr"].ToString().Trim();
                    if (dt.Rows[0]["inpatientnotype_int"].ToString() == "1")
                    {
                        this.m_objViewer.lblintype2.Text = "��ͨסԺ";
                        this.m_objViewer.lblintype2.Tag = "סԺ��";
                    }
                    else if (dt.Rows[0]["inpatientnotype_int"].ToString() == "2")
                    {
                        this.m_objViewer.lblintype2.Text = "����סԺ";
                        this.m_objViewer.lblintype2.Tag = "���ۺ�";
                    }
                    else
                    {
                        this.m_objViewer.lblintype2.Text = "δ֪";
                    }
                    this.m_objViewer.lblindate2.Text = dt.Rows[0]["rysj"].ToString().Trim();
                    this.m_objViewer.lblarea2.Text = dt.Rows[0]["deptname_vchr"].ToString().Trim();

                    this.m_objViewer.lbloutarea.Text = dt.Rows[0]["cybq"].ToString().Trim();
                    this.m_objViewer.lblbed2.Text = dt.Rows[0]["cybc"].ToString().Trim();
                    this.m_objViewer.lbloutdate.Text = dt.Rows[0]["cysj"].ToString().Trim();
                }
            }
            #endregion
        }
        #endregion

        #region �ĺ�
        /// <summary>
        /// �ĺ�
        /// </summary>
        public void m_mthModifyNO()
        {
            if (this.m_objViewer.lblzyh1.Text.Trim() == "")
            {
                MessageBox.Show("����ҵ�ǰ��Ժ���ˣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //�޸����� 0 סԺ��->סԺ�� 1 סԺ��->���ۺ� 2 ���ۺ�->���ۺ� 3 ���ۺ�->סԺ��
            int currtype = this.m_objViewer.cboType.SelectedIndex;
                        
            string orgtype = this.m_objViewer.cboType.Tag.ToString().Trim();
            if (orgtype == "סԺ��")
            {
                if (currtype == 2 || currtype == 3)
                {
                    MessageBox.Show("��ǰ����ͨסԺ���ˣ�������ѡ���޸����͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.cboType.SelectedIndex = 0;
                    return;
                }                
            }
            else if (orgtype == "���ۺ�")
            {
                if (currtype == 0 || currtype == 1)
                {
                    MessageBox.Show("��ǰ�����۲��ˣ�������ѡ���޸����͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.cboType.SelectedIndex = 2;
                    return;
                }
            }                       

            //��ǰ����ID
            string currpatientid = this.m_objViewer.lblname1.Tag.ToString();
            //������Ժ�Ǽ���ˮ��
            string currregid = this.m_objViewer.lblzyh1.Tag.ToString();
            //����סԺ��
            string currzyh = this.m_objViewer.lblzyh1.Text.Trim();
            //����סԺ����
            int currzycs = int.Parse(this.m_objViewer.lblzycs1.Text);
            //�º�
            string newno = this.m_objViewer.txtNewNO.Text.Trim();
            //�ɺŲ���ID
            string oldpatientid = "";
            //�ɺ�
            string oldzyh = this.m_objViewer.lblzyh2.Text.Trim();
            //�ɺ�סԺ����
            int oldzycs = 0;
            //ͬһ���˱�־
            bool sameflag = false;
                           
            //��ʾ��Ϣ
            string[] Hintinfo = new string[4] { "", "", "", ""};
            Hintinfo[1] = "�Ƿ�ĳɡ��¡���סԺ(����)�ţ���ȷ�ϣ�";
            Hintinfo[2] = "�Ƿ��Զ��������µ�סԺ(����)�ţ���ȷ�ϣ�";
            Hintinfo[3] = "�Ƿ񡾺ϲ������ɵ�סԺ(����)�ţ���ȷ�ϣ�";
            string[] Saveinfo = new string[2] { "", "" };
            Saveinfo[0] = "�޸�סԺ�ųɹ���";
            Saveinfo[1] = "�޸�סԺ��ʧ�ܡ�";

            //�޸����� 1 �º� 2 �Զ� 3 �ϲ�
            int type = 0;
            //���סԺ��־
            bool miflag = false;            
            if (currzycs > 1)
            {
                miflag = true;
            }
                              
            if (this.m_objViewer.chkAuto.Checked == false && newno == "" && oldzyh == "")
            {                            
                return;
            }

            if (this.m_objViewer.chkUnion.Checked)
            {
                if (oldzyh == "")
                {
                    MessageBox.Show("�ϲ�סԺ(����)��ǰ���ҳ��ɺ���Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.m_objViewer.btnFindOldNO.Focus();
                    return;
                }
                else
                {
                    if (this.m_objViewer.lblintype2.Text == "��ͨסԺ")
                    {
                        if (currtype != 0 && currtype != 3)
                        {
                            MessageBox.Show("�ϲ�סԺ(����)��ǰ���޸ĳɵ����ͱ�����ɺ�������ͬ��������ָ���޸����͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.m_objViewer.cboType.Focus();
                            return;
                        }
                    }
                    else if (this.m_objViewer.lblintype2.Text == "����סԺ")
                    {
                        if (currtype != 1 && currtype != 2)
                        {
                            MessageBox.Show("�ϲ�סԺ(����)��ǰ���޸ĳɵ����ͱ�����ɺ�������ͬ��������ָ���޸����͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.m_objViewer.cboType.Focus();
                            return;
                        }
                    }
                    type = 3;
                }
            }
            else
            {
                if (this.m_objViewer.chkAuto.Checked)
                {
                    type = 2;
                }
                else
                {
                    if (newno == "")
                    {
                        MessageBox.Show("��������סԺ(����)�š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.m_objViewer.btnFindOldNO.Focus();
                        return;
                    }
                    else
                    {
                        type = 1;
                    }
                }
            }

            string pid1 = this.m_objViewer.lblname1.Tag.ToString().Trim();
            string pid2 = this.m_objViewer.lblzyh1.Tag.ToString().Trim();

            string intype1 = this.m_objViewer.lblintype1.Text;
            string intype2 = this.m_objViewer.lblintype2.Text;
            if (intype2.Trim() != "" && intype1 != intype2 && pid1 == pid2)
            {
                if ((currtype == 1 || currtype == 3))
                {
                    if (!this.m_objViewer.chkUnion.Checked)
                    {
                        MessageBox.Show("ͬһ����ֻ��ʹ�úϲ�סԺ(����)�Ų�����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            int zycs1 = (this.m_objViewer.lblzycs1.Text.Trim() == "" ? 0 : int.Parse(this.m_objViewer.lblzycs1.Text.Trim()));
            int zycs2 = (this.m_objViewer.lblzycs2.Text.Trim() == "" ? 0 : int.Parse(this.m_objViewer.lblzycs2.Text.Trim()));
            if ((zycs1 > 1 && zycs2 == 0 && currtype != 1 && currtype != 3 ) || (zycs1 > 1 && zycs2 > 0 && intype1 == intype2 && pid1 == pid2))
            {
                MessageBox.Show("��ͬһ���סԺ�Ĳ��˲��ܽ����޸Ĳ����������˺š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }           
            //else if (zycs1 > 1 && zycs2 > 0 && !this.m_objViewer.chkUnion.Checked)
            //{
            //    MessageBox.Show("ֻ�ܺϲ�סԺ(����)�š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (MessageBox.Show(Hintinfo[type], "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (type == 1)
            {
                if (this.m_blnCheckNewNO(newno))
                {
                    MessageBox.Show("��סԺ(����)���ѱ�ʹ�ã������������ºš�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            else if (type == 3)
            {
                if (Convert.ToDateTime(this.m_objViewer.lbloutdate.Text) >= Convert.ToDateTime(this.m_objViewer.lblindate1.Text))
                {
                    MessageBox.Show("�ɺŵĳ�Ժʱ����ڵ�ǰסԺ��Ϣ����Ժʱ�䣬���Ų��ܺϲ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                newno = oldzyh;
                oldzycs = int.Parse(this.m_objViewer.lblzycs2.Text) + 1;
                oldpatientid = this.m_objViewer.lblzyh2.Tag.ToString().Trim();
                sameflag = (currpatientid == oldpatientid ? true : false);
            }

            if (this.objSvc.m_blnModifyNewNO(oldpatientid, currregid, currzyh, ref newno, oldzycs, miflag, sameflag, currtype, type, this.m_objViewer.LoginInfo.m_strEmpID))
            {
                MessageBox.Show(Saveinfo[0], "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.m_objViewer.lblzyh1.Text = newno;
                if (type == 3)
                {
                    this.m_objViewer.lblzycs1.Text = oldzycs.ToString();
                    this.m_objViewer.lblname1.Text = this.m_objViewer.lblname2.Text;
                    this.m_objViewer.lblname1.Tag = this.m_objViewer.lblzyh2.Tag;                    
                    this.m_objViewer.lblsex1.Text = this.m_objViewer.lblsex2.Text;
                    this.m_objViewer.lblbirthday1.Text = this.m_objViewer.lblbirthday2.Text;
                    this.m_objViewer.lblidcard1.Text = this.m_objViewer.lblidcard2.Text;
                    this.m_objViewer.lbladdress1.Text = this.m_objViewer.lbladdress2.Text;
                }
                if (currtype == 0 || currtype == 3)
                {
                    this.m_objViewer.lblintype1.Text = "��ͨסԺ";
                }
                else if (currtype == 1 || currtype == 2)
                {
                    this.m_objViewer.lblintype1.Text = "����סԺ";
                }

                //˳��ͬʱ�޸�ҽ������סԺ��
                if (clsPublic.m_strGetSysparm("1000") == "003")
                {
                    clsCtl_Report objRep = new clsCtl_Report();
                    objRep.m_mthModifyZyh_SDYB(currzyh, newno);
                }
            }
            else
            {
                MessageBox.Show(Saveinfo[1], "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                                                                                                       
        }
        #endregion        
    }
}
