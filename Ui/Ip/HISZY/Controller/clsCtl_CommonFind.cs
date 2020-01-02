using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ���ҿ�����
    /// </summary>
    public class clsCtl_CommonFind : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_CommonFind objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmCommonFind m_objViewer;
        /// <summary>
        /// ��Ժ�ǼǴ����ñ�־
        /// </summary>
        internal bool BlnInReg = false;

        #endregion 

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_CommonFind()
        {
            objSvc = new clsDcl_CommonFind();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmCommonFind)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthFind()
        {
            string SqlWhereMZ = "", SqlWhereZY = "";            
            this.m_mthGetsqlwhere(out SqlWhereZY, out SqlWhereMZ);

            //����
            SqlWhereMZ = SqlWhereMZ.Trim();           

            if (SqlWhereMZ.StartsWith("or"))
            {
                SqlWhereMZ = SqlWhereMZ.Substring(2);
            }
            else if (SqlWhereMZ.StartsWith("and"))
            {
                SqlWhereMZ = SqlWhereMZ.Substring(3);
            }

            bool IsIncludeMZ = this.m_objViewer.chkMZ.Checked;

            //סԺ
            SqlWhereZY = SqlWhereZY.Trim();

            if (SqlWhereZY.StartsWith("or"))
            {
                SqlWhereZY = SqlWhereZY.Substring(2);           
            }
            else if (SqlWhereZY.StartsWith("and"))
            {
                SqlWhereZY = SqlWhereZY.Substring(3);           
            }

            if (!SqlWhereZY.Trim().StartsWith("(to_char"))
            {
                SqlWhereZY = "(" + SqlWhereZY + ")";
            }

            if (SqlWhereZY.Length > 8)
            {
                SqlWhereZY = " and " + SqlWhereZY;
            }
            else
            {
                SqlWhereZY = "";
            } 

            clsCommonQueryDate_VO CommonQueryDate_VO = new clsCommonQueryDate_VO();
            if (this.m_objViewer.chkInDate.Checked == false && this.m_objViewer.chkOutDate.Checked == false)
            {
                CommonQueryDate_VO.QueryType = 0;
            }
            else
            {
                if (this.m_objViewer.chkInDate.Checked && this.m_objViewer.chkOutDate.Checked)
                {
                    CommonQueryDate_VO.QueryType = 3;
                    CommonQueryDate_VO.BeginDate_In = this.m_objViewer.dtBegin_in.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                    CommonQueryDate_VO.EndDate_In = this.m_objViewer.dtEnd_in.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                    CommonQueryDate_VO.BeginDate_Out = this.m_objViewer.dtBegin_out.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                    CommonQueryDate_VO.EndDate_Out = this.m_objViewer.dtEnd_out.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                }
                else
                {
                    if (this.m_objViewer.chkInDate.Checked)
                    {
                        CommonQueryDate_VO.QueryType = 1;
                        CommonQueryDate_VO.BeginDate_In = this.m_objViewer.dtBegin_in.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                        CommonQueryDate_VO.EndDate_In = this.m_objViewer.dtEnd_in.Value.ToString("yyyy-MM-dd") + " 23:59:59";                        
                    }
                    else if (this.m_objViewer.chkOutDate.Checked)
                    {
                        CommonQueryDate_VO.QueryType = 2;                        
                        CommonQueryDate_VO.BeginDate_Out = this.m_objViewer.dtBegin_out.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                        CommonQueryDate_VO.EndDate_Out = this.m_objViewer.dtEnd_out.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                    }
                }
            }

            clsPublic.PlayAvi("���ڲ��Ҳ������Ͽ⣬���Ժ�...");
            try
            {
                DataTable dt = new DataTable();
                long l = this.objSvc.m_lngGetPatientinfo(SqlWhereZY, this.m_objViewer.Status, IsIncludeMZ, SqlWhereMZ, CommonQueryDate_VO, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    this.m_mthSetval(dt);
                    clsPublic.CloseAvi();
                }
                else
                {
                    this.m_objViewer.lsvPatient.BeginUpdate();
                    this.m_objViewer.lsvPatient.Items.Clear();
                    this.m_objViewer.lsvPatient.EndUpdate();
                   
                    MessageBox.Show("û���ҵ������ѯ�����Ĳ�����Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.lblInfo.Text = "�ҵ����������ļ�¼���� 0��";

                    if (this.m_objViewer.txtCardNo.Text.Trim() != "")
                    {
                        this.m_objViewer.txtCardNo.Focus();
                        this.m_objViewer.txtCardNo.SelectAll();
                    }
                    else if (this.m_objViewer.txtZyh.Text.Trim() != "")
                    {
                        this.m_objViewer.txtZyh.Focus();
                        this.m_objViewer.txtZyh.SelectAll();
                    }
                    else if (this.m_objViewer.txtName.Text.Trim() != "")
                    {
                        this.m_objViewer.txtName.Focus();
                        this.m_objViewer.txtName.SelectAll();
                    }
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region �õ���������
        /// <summary>
        /// �õ���������
        /// </summary>        
        private void m_mthGetsqlwhere(out string SqlWhereZY, out string SqlWhereMZ)
        {
            SqlWhereZY = "";
            SqlWhereMZ = "1 <> 1";
            string Match = "";

            if (this.m_objViewer.chkMatch.Checked)
            {
                Match = "%";
            }

            string Unlogic = "";
            bool Union = true;

            if (this.m_objViewer.chkUnionAnd.Checked)
            {
                Unlogic = "and";
            }
            else if (this.m_objViewer.chkUnionOr.Checked)
            {
                Unlogic = "or";
            }
            else
            {
                Union = false;
            }                        

            if (this.m_objViewer.txtZyh.Text.Trim() != "")
            {
                SqlWhereZY = "a.inpatientid_chr like '" + this.m_objViewer.txtZyh.Text.Trim() + Match + "'";
                if (!Union)
                {
                    return;
                }
            }

            if (this.m_objViewer.txtCardNo.Text.Trim() != "")
            {
                SqlWhereZY += Unlogic + " f.patientcardid_chr like '" + this.m_objViewer.txtCardNo.Text.Trim() + Match + "'";
                SqlWhereMZ = " f.patientcardid_chr like '" + this.m_objViewer.txtCardNo.Text.Trim() + Match + "'";
                if (!Union)
                {
                    return;
                }
            }

            if (this.m_objViewer.txtName.Text.Trim() != "")
            {
                SqlWhereZY += Unlogic + " b.lastname_vchr like '" + this.m_objViewer.txtName.Text.Trim() + Match + "'";

                if (SqlWhereMZ.Trim() == "1 <> 1")
                {
                    SqlWhereMZ = Unlogic + " b.lastname_vchr like '" + this.m_objViewer.txtName.Text.Trim() + Match + "'";
                }
                else
                {
                    SqlWhereMZ += Unlogic + " b.lastname_vchr like '" + this.m_objViewer.txtName.Text.Trim() + Match + "'";
                }
                if (!Union)
                {
                    return;
                }
            }
        }
        #endregion

        #region ��ʽ����
        /// <summary>
        /// ��ʽ����
        /// </summary>
        /// <param name="Val">ֵ</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="type">���� 0 סԺ�� 1 ���ƿ��� 2 ����</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns>��¼��</returns>
        public int m_mthFind(string Val, bool Ismatch, int type, bool IsIncludeMZ)
        {
            string SqlWhereZY = "", SqlWhereMZ = "";
            string Match = "";
            
            if (Ismatch)
            {
                Match = "%";
            }

            switch (type)
            {
                case 0:
                    SqlWhereZY = "a.inpatientid_chr like '" + Val + Match + "'";
                    SqlWhereMZ = "1 <> 1";
                    break;
                case 1:
                    SqlWhereZY += "f.patientcardid_chr like '" + Val + Match + "'";
                    SqlWhereMZ = "a.inpatientid_chr like '" + Val + Match + "'";
                    break;
                case 2:
                    SqlWhereZY += "b.lastname_vchr like '" + Val + Match + "'";
                    SqlWhereMZ = "a.inpatientid_chr like '" + Val + Match + "'";
                    break;                
            }

            SqlWhereZY = " and " + SqlWhereZY;

            DataTable dt = new DataTable();
            long l = this.objSvc.m_lngGetPatientinfo(SqlWhereZY, this.m_objViewer.Status, IsIncludeMZ, SqlWhereMZ, null, out dt);            
            if (l > 0 && dt.Rows.Count > 0)
            {
                this.m_mthSetval(dt);                
            }
            else
            {
                return 0;
            }

            return dt.Rows.Count;
        }
        
        /// <summary>
        /// ��ʽ����
        /// </summary>
        /// <param name="Name">����</param>
        /// <param name="Sex">�Ա�</param>
        /// <param name="Type">��Ժ����(1 ��ͨ 2 ����)</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns>��¼��</returns>
        public int m_mthFind(string Name, string Sex, int Type, bool Ismatch, bool IsIncludeMZ)
        {            
            string Match = "";
            
            if (Ismatch)
            {
                Match = "%";
            }

            string SqlWhereZY = " and b.lastname_vchr like '" + Name + Match + "' and b.sex_chr = '" + Sex + "' and a.inpatientnotype_int = " + Type.ToString();
            string SqlWhereMZ = "b.lastname_vchr like '" + Name + Match + "' and b.sex_chr = '" + Sex + "'";
                        
            DataTable dt = new DataTable();
            long l = this.objSvc.m_lngGetPatientinfo(SqlWhereZY, this.m_objViewer.Status, IsIncludeMZ, SqlWhereMZ, null, out dt);            
            if (l > 0 && dt.Rows.Count > 0)
            {
                this.m_mthSetval(dt);                
            }
            else
            {
                return 0;
            }

            return dt.Rows.Count;
        }
        #endregion

        #region ��ʾ���
        /// <summary>
        /// ��ʾ���
        /// </summary>
        /// <param name="dt"></param>
        private void m_mthSetval(DataTable dt)
        {
            this.m_objViewer.Cursor = Cursors.WaitCursor;
            this.m_objViewer.lsvPatient.BeginUpdate();
            this.m_objViewer.lsvPatient.Items.Clear();

            string status = "";
            string feestatus = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["feestatus_int"].ToString())
                {
                    case "0":
                        feestatus = "��";
                        break;
                    case "1":
                        feestatus = "����";
                        break;
                    case "2":
                        feestatus = "����";
                        break;
                    case "3":
                        feestatus = "����";
                        break;
                    case "4":
                        feestatus = "��������";
                        break;
                    case "5":
                        feestatus = "����";
                        break;
                }

                switch (dt.Rows[i]["pstatus_int"].ToString())
                {
                    case "0":
                        status = "�޴�";
                        break;
                    case "1":
                        status = "�ڴ�";
                        break;
                    case "2":
                        status = "Ԥ��Ժ";
                        if (dt.Rows[i]["feestatus_int"].ToString() != "5")
                        {
                            feestatus = "����";
                        }
                        break;
                    case "3":
                        status = "��Ժ";
                        break;
                    case "4":
                        status = "���";
                        break;      
                    case "999":
                        status = "����";
                        break;
                }                

                ListViewItem lvitem = new ListViewItem((Convert.ToInt32(i + 1)).ToString());
                lvitem.SubItems.Add(status);

                if (status == "����")
                {
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add(dt.Rows[i]["lastname_vchr"].ToString().Trim());
                    lvitem.SubItems.Add(dt.Rows[i]["sex_chr"].ToString().Trim());
                    if (dt.Rows[i]["cssj"].ToString().Trim() == "")
                    {
                        lvitem.SubItems.Add("");
                        lvitem.SubItems.Add("");
                    }
                    else
                    {
                        lvitem.SubItems.Add(clsPublic.CalcAge(Convert.ToDateTime(dt.Rows[i]["birth_dat"])));
                        lvitem.SubItems.Add(dt.Rows[i]["cssj"].ToString());
                    }
                    lvitem.SubItems.Add(dt.Rows[i]["homeaddress_vchr"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["employer_vchr"].ToString());
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add(dt.Rows[i]["patientcardid_chr"].ToString());
                    lvitem.SubItems.Add("");
                    lvitem.SubItems.Add(dt.Rows[i]["patientid_chr"].ToString());

                    lvitem.BackColor = clsPublic.CustomBackColor;
                }
                else
                {
                    lvitem.SubItems.Add(feestatus);
                    lvitem.SubItems.Add(dt.Rows[i]["inpatientid_chr"].ToString().Trim());
                    lvitem.SubItems.Add(dt.Rows[i]["inpatientcount_int"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["deptname_vchr"].ToString().Trim());
                    lvitem.SubItems.Add(dt.Rows[i]["lastname_vchr"].ToString().Trim());
                    lvitem.SubItems.Add(dt.Rows[i]["sex_chr"].ToString().Trim());
                    if (dt.Rows[i]["cssj"].ToString().Trim() == "")
                    {
                        lvitem.SubItems.Add("");
                        lvitem.SubItems.Add("");
                    }
                    else
                    {
                        lvitem.SubItems.Add(clsPublic.CalcAge(Convert.ToDateTime(dt.Rows[i]["birth_dat"])));
                        lvitem.SubItems.Add(dt.Rows[i]["cssj"].ToString());
                    }
                    lvitem.SubItems.Add(dt.Rows[i]["homeaddress_vchr"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["employer_vchr"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["rysj"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["cysj"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["patientcardid_chr"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["registerid_chr"].ToString());
                    lvitem.SubItems.Add(dt.Rows[i]["patientid_chr"].ToString());

                    lvitem.BackColor = Color.FromArgb(255, 255, 255);
                }


                lvitem.ImageIndex = 0;
                lvitem.Tag = dt.Rows[i];
                this.m_objViewer.lsvPatient.Items.Add(lvitem);
            }

            this.m_objViewer.lblInfo.Text = "�ҵ����������ļ�¼���� " + dt.Rows.Count.ToString() + "��";

            this.m_objViewer.lsvPatient.EndUpdate();
            this.m_objViewer.Cursor = Cursors.Default;

            if (dt.Rows.Count > 0)
            {
                this.m_objViewer.lsvPatient.Items[0].Selected = true;
                this.m_objViewer.lsvPatient.Focus();
            }
        }
        #endregion

        #region ���������λ��Ϣ
        /// <summary>
        /// ���������λ��Ϣ
        /// </summary>
        public void m_mthFindArea()
        {
            clsDcl_Charge objCharge = new clsDcl_Charge();
            DataTable dt = new DataTable();

            long l = objCharge.m_lngGetDeptArea(out dt, 2);
            if (l == 0)
            {
                return;
            }

            frmAreaBedInfo f = new frmAreaBedInfo(dt, 0);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.RegisterID = f.regid;
                this.m_objViewer.PatientID = f.pid;
                this.m_objViewer.Zyh = f.Zyh;
                this.m_objViewer.Zycs = f.Zycs;
                this.m_objViewer.PatName = f.patname;
                this.m_objViewer.InType = 1;

                clsPublic.m_mthWriteParm(f.regid, f.Zyh, f.CardNo);                
                this.m_objViewer.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region ����ѡ��Ĳ�����Ϣ
        /// <summary>
        /// ����ѡ��Ĳ�����Ϣ
        /// </summary>
        public void m_mthGetPatientinfo()
        {
            if (this.m_objViewer.lsvPatient.Items.Count == 0 || this.m_objViewer.lsvPatient.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow dr = (DataRow)(this.m_objViewer.lsvPatient.SelectedItems[0].Tag);

            if (dr["pstatus_int"].ToString() == "999")
            {
                if (!BlnInReg)
                {
                    MessageBox.Show("��ѡ��סԺ���ˣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //else
            //{
            //    string[] statusinfo = new string[5] { "�´�", "�ڴ�", "Ԥ��Ժ", "ʵ�ʳ�Ժ", "���" };
            //    int statusno = int.Parse(dr["pstatus_int"].ToString());
            //    if (statusno != this.m_objViewer.Status)
            //    {
            //        MessageBox.Show("��ǰ����Ϊ��" + statusinfo[statusno] + "��״̬��������ѡ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
       
            this.m_objViewer.RegisterID = dr["registerid_chr"].ToString();
            this.m_objViewer.PatientID = dr["patientid_chr"].ToString();
            this.m_objViewer.Zyh = dr["inpatientid_chr"].ToString();
            this.m_objViewer.Zycs = int.Parse(dr["inpatientcount_int"].ToString());
            this.m_objViewer.CardNo = dr["patientcardid_chr"].ToString();
            this.m_objViewer.PatName = dr["lastname_vchr"].ToString();
            this.m_objViewer.OutDate = dr["cysj"].ToString();
            this.m_objViewer.InType = int.Parse(dr["inpatientnotype_int"].ToString());

            clsPublic.m_mthWriteParm(this.m_objViewer.RegisterID, this.m_objViewer.Zyh, this.m_objViewer.CardNo);
            this.m_objViewer.DialogResult = DialogResult.OK;
        }
        #endregion               
    }
}
