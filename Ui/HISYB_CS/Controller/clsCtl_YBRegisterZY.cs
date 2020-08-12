using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Collections.Generic;
using ControlLibrary;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBRegisterZY : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_YBRegisterZY()
        {
        }
        public clsDcl_YB objDomain = new clsDcl_YB();

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBRegisterZY m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBRegisterZY)frmMDI_Child_Base_in;
        }
        #endregion

        clsDGExtra_VO objDgextraVo = null;

        Dictionary<string, string> dicRYDYZDBY = new Dictionary<string, string>();

        /// <summary>
        /// ��ǰ��Ͽؼ�: 1 ��Ҫ���; 2 ��Ҫ���(1); 3 ��Ҫ���(2)
        /// </summary>
        internal int CurrDiagTxtIndex { get; set; }

        /// <summary>
        /// ICD.DataSource
        /// </summary>
        internal DataTable IcdDataSource { get; set; }

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="findCode"></param>
        internal void FindICD(string findCode)
        {
            if (this.IcdDataSource == null || this.IcdDataSource.Rows.Count == 0) return;

            DataRow[] drr = null;
            if (findCode.Trim() == "")
            {
                drr = new DataRow[this.IcdDataSource.Rows.Count];
                for (int i = 0; i < this.IcdDataSource.Rows.Count; i++)
                {
                    drr[i] = this.IcdDataSource.Rows[i];
                }
            }
            else
            {
                string exp = "icdcode like '{0}%' or icdname like '{0}%' or pycode like '{0}%' ";
                exp = string.Format(exp, new string[3] { findCode, findCode, findCode });
                drr = this.IcdDataSource.Select(exp);
                if (drr == null || drr.Length == 0)
                {
                    MessageBox.Show("��������");
                    return;
                }
            }

            string icdCode = string.Empty;
            List<string> lstIcdCode = new List<string>();
            this.m_objViewer.lsvItemICD.BeginUpdate();
            this.m_objViewer.lsvItemICD.Items.Clear();
            foreach (DataRow dr in drr)
            {
                icdCode = dr["icdcode"].ToString();
                if (lstIcdCode.IndexOf(icdCode) < 0)
                {
                    lstIcdCode.Add(icdCode);
                    ListViewItem lv = new ListViewItem(icdCode);
                    lv.SubItems.Add(dr["icdname"].ToString());
                    lv.Tag = dr;
                    this.m_objViewer.lsvItemICD.Items.Add(lv);
                }
            }
            if (this.m_objViewer.lsvItemICD.Items.Count > 0)
            {
                if (CurrDiagTxtIndex == 1)
                {
                    this.m_objViewer.lsvItemICD.Height = 156;
                }
                else if (CurrDiagTxtIndex == 2)
                {
                    this.m_objViewer.lsvItemICD.Height = 116;
                }
                else if (CurrDiagTxtIndex == 3)
                {
                    this.m_objViewer.lsvItemICD.Height = 116;
                }
                this.m_objViewer.lsvItemICD.Items[0].Selected = true;
                this.m_objViewer.lsvItemICD.Focus();
            }

            this.m_objViewer.lsvItemICD.EndUpdate();
        }
        #endregion

        #region ѡ�����
        /// <summary>
        /// ѡ�����
        /// </summary>
        internal void SelectICD()
        {
            if (this.m_objViewer.lsvItemICD.Items.Count == 0 || this.m_objViewer.lsvItemICD.SelectedItems.Count == 0)
            {
                return;
            }
            DataRow dr = this.m_objViewer.lsvItemICD.SelectedItems[0].Tag as DataRow;
            if (CurrDiagTxtIndex == 1)
            {
                this.m_objViewer.txtMainDiag.Text = dr["icdname"].ToString();
                this.m_objViewer.txtMainDiag.Tag = dr;
                this.m_objViewer.txtMainDiag.Focus();
            }
            else if (CurrDiagTxtIndex == 2)
            {
                this.m_objViewer.txtSecDiag1.Text = dr["icdname"].ToString();
                this.m_objViewer.txtSecDiag1.Tag = dr;
                this.m_objViewer.txtSecDiag1.Focus();
            }
            else if (CurrDiagTxtIndex == 3)
            {
                this.m_objViewer.txtSecDiag2.Text = dr["icdname"].ToString();
                this.m_objViewer.txtSecDiag2.Tag = dr;
                this.m_objViewer.txtSecDiag2.Focus();
            }
        }
        #endregion

        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void m_mthInit()
        {
            try
            {
                if (string.IsNullOrEmpty(this.m_objViewer.strRegisterId))
                {
                    return;
                }
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                this.m_objViewer.lsvItemICD.Height = 0;
                // ICD10�ֵ��
                this.IcdDataSource = objDomain.GetIcd10();
                this.m_objViewer.txtCbdtcqbm.Text = "441925";  // ��ɽ

                long lngRes = -1;
                dicRYDYZDBY.Add("", "");
                dicRYDYZDBY.Add("����", "1");
                dicRYDYZDBY.Add("�������ص��µ��˺�", "2");
                dicRYDYZDBY.Add("�����ǵ��������µ��˺�", "3");
                dicRYDYZDBY.Add("ͻ���¼����µ��˺�", "4");
                dicRYDYZDBY.Add("���������µ��˺�����ͨ�¹ʳ��⣩", "5");
                dicRYDYZDBY.Add("��ͨ�¹�", "6");

                foreach (string strKey in dicRYDYZDBY.Keys)
                {
                    this.m_objViewer.cobRYDYZDBY.Items.Add(strKey);
                }
                this.m_objViewer.cobRYDYZDBY.SelectedIndex = 0;

                this.m_objViewer.cboJZLB.SelectedIndex = -1;
                DataTable dtRegisterInfo = null;
                //need modify ��ȫ����datarow�ĸ�ֵ
                lngRes = this.objDomain.m_lngGetZYYBRegister(this.m_objViewer.strRegisterId, out dtRegisterInfo);
                if (dtRegisterInfo != null && dtRegisterInfo.Rows.Count > 0)
                {
                    if (m_lngLoadDataCtl() < 0)
                    {
                        MessageBox.Show("�������ݼ������������´򿪣�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }

                    //��ʼ��������ϢclsDGExtra_VO
                    objDgextraVo = new clsDGExtra_VO();
                    objDgextraVo.JBR = this.m_objViewer.LoginInfo.m_strEmpNo;
                    objDgextraVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne"); //ҽԺ���
                    string strUser = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "USERNAMEZY", "AnyOne");
                    string strPwd = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
                    lngRes = clsYBPublic_cs.m_lngUserLoin(strUser, strPwd, false);
                    if (lngRes < 0)
                    {
                        MessageBox.Show("�������ݼ������������´򿪣�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    #region ����1201�鿴���˻�����Ϣ
                    clsDGZydj_VO m_objItem = new clsDGZydj_VO();
                    //���˻�����Ϣ
                    clsDGPaitentInfo_VO m_objPatientInfo = new clsDGPaitentInfo_VO();
                    //����������Ϣ
                    List<clsDGJxzlxx_VO> m_objJXzlxx = new List<clsDGJxzlxx_VO>();
                    //�����Ա��Ϣ
                    List<clsDGYdryxx_VO> m_objYDryxx = new List<clsDGYdryxx_VO>();
                    //תԺ��Ϣ
                    List<clsDGZyxx_VO> m_objZYxx = new List<clsDGZyxx_VO>();
                    //���סԺ��Ϣ
                    List<clsDGZjzyxx_VO> m_objZJzyxx = new List<clsDGZjzyxx_VO>();
                    m_objItem.GMSFHM = dtRegisterInfo.Rows[0]["idcard_chr"].ToString().Trim();
                    m_objItem.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne"); //ҽԺ���
                    m_objItem.CBDTCQBM = dtRegisterInfo.Rows[0]["cbdtcqbm_vchr"].ToString();
                    m_objItem.LXDH = this.m_objViewer.LoginInfo.m_strEmpNo;
                    lngRes = clsYBPublic_cs.m_lngFunSP1201(m_objItem, out m_objPatientInfo, out m_objJXzlxx, out m_objYDryxx, out m_objZYxx, out m_objZJzyxx);
                    if (lngRes > 0)
                    {
                        if (m_objPatientInfo != null)
                        {
                            if (m_objPatientInfo.JSFS == "3")
                            {
                                if (MessageBox.Show("�ò��˵Ľ��㷽ʽΪ��ȫ���Էѣ� \n�Է�ԭ���ǣ�" + m_objPatientInfo.ZFYY.ToString() + ", �Ƿ�������еǼǣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            if (m_objPatientInfo.SBBZ.ToString().Trim() == "0")
                            {
                                MessageBox.Show("�ò��˵��籣��־Ϊ0�������ԷѲ��˴��������籣�Ǽǣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    #endregion
                    DataRow drTemp = dtRegisterInfo.Rows[0];
                    if (drTemp["insdeptcode_vchr"].ToString() == null || drTemp["insdeptcode_vchr"].ToString() == "")
                    {
                        MessageBox.Show("�������Ǽǵ���Ժ����:" + drTemp["deptname_vchr"].ToString() + " ���������籣���ң�����ϵ����Ա�鿴��ά����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    this.m_objViewer.strSBBZ = m_objPatientInfo.SBBZ.ToString();
                    this.m_objViewer.txtIDCard.Text = drTemp["idcard_chr"].ToString();//���֤��
                    this.m_objViewer.txtZYH.Text = drTemp["inpatientid_chr"].ToString();//סԺ��
                    this.m_objViewer.txtName.Text = drTemp["lastname_vchr"].ToString();//��������
                    this.m_objViewer.txtDept.Text = drTemp["deptname_vchr"].ToString();//ҽԺ��������
                    this.m_objViewer.txtDept.Tag = drTemp["insdeptcode_vchr"].ToString();//t_ins_deptrel���е��籣����id
                    this.m_objViewer.txtBedno.Text = drTemp["bed_no"].ToString();//����
                    this.m_objViewer.txtDoctor.Value = drTemp["empno_chr"].ToString();//����ҽ������
                    this.m_objViewer.txtDoctor.Text = drTemp["doctorname_vchr"].ToString();//����ҽ��
                    this.m_objViewer.txtDoctor.Tag = drTemp["empno_chr"].ToString();//����ҽ������
                    //���txtBedno��txtDoctor�����ؼ�
                    this.m_objViewer.txtDept.Value = drTemp["areaid_chr"].ToString();

                    // ���ݻ������»�ȡ�α��ر��� -- ���ҽ��
                    DataTable dtTmp = null;
                    this.objDomain.m_lngGetPatientInfo(drTemp["inpatientid_chr"].ToString(), "0", out dtTmp);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        this.m_objViewer.txtCbdtcqbm.Text = dtTmp.Rows[dtTmp.Rows.Count - 1]["CBDTCQBM_VCHR"].ToString();
                    }

                    this.m_mthFilterBed();
                    this.m_mthFilterDoc();
                    if (drTemp["cybz_vchr"] != DBNull.Value)
                    {
                        //this.m_objViewer.cboZYLB.Text = drTemp["zylb_vchr"].ToString();//סԺ���
                        this.m_objViewer.cboCYBZ.SelectedIndex = 0;
                        for (int i = 0; i < this.m_objViewer.cboCYBZ.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboCYBZ.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["cybz_vchr"].ToString()))
                            {
                                this.m_objViewer.cboCYBZ.SelectedIndex = i;
                            }
                        }
                    }
                    if (drTemp["zylb_vchr"] != DBNull.Value)
                    {
                        //this.m_objViewer.cboZYLB.Text = drTemp["zylb_vchr"].ToString();//סԺ���
                        for (int i = 0; i < this.m_objViewer.cboZYLB.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboZYLB.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["zylb_vchr"].ToString()))
                            {
                                this.m_objViewer.cboZYLB.SelectedIndex = i;
                            }
                        }
                    }
                    if (drTemp["jzlb_vchr"] != DBNull.Value)
                    {
                        //this.m_objViewer.cboJZLB.SelectedValue = drTemp["jzlb_vchr"].ToString();//�������
                        for (int i = 0; i < this.m_objViewer.cboJZLB.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboJZLB.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["jzlb_vchr"].ToString()))
                            {
                                this.m_objViewer.cboJZLB.SelectedIndex = i;
                            }
                        }
                    }
                    if (drTemp["wsbz_vchr"] != DBNull.Value)
                    {
                        //this.m_objViewer.cboWSBZ.Text = drTemp["wsbz_vchr"].ToString();//���˱�־
                        for (int i = 0; i < this.m_objViewer.cboWSBZ.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboWSBZ.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["wsbz_vchr"].ToString()))
                            {
                                this.m_objViewer.cboWSBZ.SelectedIndex = i;
                            }
                        }
                    }
                    //"סԺ���" == "2-����"
                    if (drTemp["zqqrqk_vchr"] != DBNull.Value)
                    {
                        //this.m_objViewer.cboZQQRQK.Text = drTemp["zqqrqk_vchr"].ToString();//֪��ȷ����� //��סԺ���Ϊ����ʱ��Ĭ��¼��1��ͬ�⣬�Ҳ��ɸı䣩סԺ���Ϊҽ��ʱ����¼��
                        for (int i = 0; i < this.m_objViewer.cboZQQRQK.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboZQQRQK.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["zqqrqk_vchr"].ToString()))
                            {
                                this.m_objViewer.cboZQQRQK.SelectedIndex = i;
                            }
                        }
                        if (this.m_objViewer.cboZYLB.SelectedIndex == 1)
                        {
                            this.m_objViewer.cboZQQRQK.Enabled = false;
                        }
                        else
                        {
                            this.m_objViewer.cboZQQRQK.Enabled = true;
                        }
                    }

                    this.m_objViewer.txtZQQRSBH.Text = drTemp["zqqrsbh_vchr"].ToString();//֪��ȷ������
                    this.m_objViewer.txtPhone.Text = drTemp["contactphone"].ToString();//��ϵ�绰
                    this.m_objViewer.dtmInHospitalDate.Value = Convert.ToDateTime(drTemp["inpatient_dat"].ToString());//��Ժ����YYYYMMDD�����ܴ��ڵ�ǰ����

                    this.m_objViewer.txtDiagnsis.Text = drTemp["mzdiagnose_vchr"].ToString();//��Ժ���
                    if (drTemp["jzjlh_vchr"] != DBNull.Value)
                    {
                        this.m_objViewer.txtJZJLH.Text = drTemp["jzjlh_vchr"].ToString();//ҽ���ǼǷ��صľ����¼��
                    }

                    //��Ժ��һ��ϲ���
                    if (drTemp["rydyzdby_vchr"] != DBNull.Value)
                    {
                        foreach (string strKey in dicRYDYZDBY.Keys)
                        {
                            if (dicRYDYZDBY[strKey] == drTemp["rydyzdby_vchr"].ToString())
                            {
                                this.m_objViewer.cobRYDYZDBY.SelectedItem = strKey;
                                break;
                            }
                        }
                    }

                    #region ���ҽ��

                    string icd10Code = string.Empty;
                    DataRow[] drr = null;
                    // ��Ҫ���
                    if (drTemp["icd10_1"] != DBNull.Value && this.IcdDataSource != null && this.IcdDataSource.Rows.Count > 0)
                    {
                        icd10Code = drTemp["icd10_1"].ToString();
                        drr = this.IcdDataSource.Select("icdcode = '" + icd10Code + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            this.m_objViewer.txtMainDiag.Text = drr[0]["icdname"].ToString();
                            this.m_objViewer.txtMainDiag.Tag = drr[0];
                        }
                    }
                    // ��Ҫ���1
                    if (drTemp["icd10_2"] != DBNull.Value && this.IcdDataSource != null && this.IcdDataSource.Rows.Count > 0)
                    {
                        icd10Code = drTemp["icd10_2"].ToString();
                        drr = this.IcdDataSource.Select("icdcode = '" + icd10Code + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            this.m_objViewer.txtSecDiag1.Text = drr[0]["icdname"].ToString();
                            this.m_objViewer.txtSecDiag1.Tag = drr[0];
                        }
                    }
                    // ��Ҫ���2
                    if (drTemp["icd10_3"] != DBNull.Value && this.IcdDataSource != null && this.IcdDataSource.Rows.Count > 0)
                    {
                        icd10Code = drTemp["icd10_3"].ToString();
                        drr = this.IcdDataSource.Select("icdcode = '" + icd10Code + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            this.m_objViewer.txtSecDiag2.Text = drr[0]["icdname"].ToString();
                            this.m_objViewer.txtSecDiag2.Tag = drr[0];
                        }
                    }
                    // ��Ժԭ��
                    if (drTemp["inreason"] != DBNull.Value)
                    {
                        this.m_objViewer.cboZyyy.SelectedIndex = 0;
                        for (int i = 1; i < this.m_objViewer.cboZyyy.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboZyyy.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["inreason"].ToString()))
                            {
                                this.m_objViewer.cboZyyy.SelectedIndex = i;
                            }
                        }
                    }
                    // ��������
                    if (drTemp["assitype"] != DBNull.Value)
                    {
                        this.m_objViewer.cboBzlx.SelectedIndex = 0;
                        for (int i = 1; i < this.m_objViewer.cboBzlx.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboBzlx.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["assitype"].ToString()))
                            {
                                this.m_objViewer.cboBzlx.SelectedIndex = i;
                            }
                        }
                    }
                    // ��Ժԭ��
                    if (drTemp["outstatus"] != DBNull.Value)
                    {
                        this.m_objViewer.cboCyyy.SelectedIndex = 0;
                        for (int i = 1; i < this.m_objViewer.cboCyyy.Items.Count; i++)
                        {
                            if (this.m_objViewer.cboCyyy.Items[i].ToString().Trim().Split('-')[0].ToString().Equals(drTemp["outstatus"].ToString()))
                            {
                                this.m_objViewer.cboCyyy.SelectedIndex = i;
                            }
                        }
                    }
                    #endregion

                    if (this.m_objViewer.txtJZJLH.Text.Trim() == "")//�����ѯ�����ľ����¼��Ϊ�գ�˵������ҽ���Ǽ�
                    {
                        this.m_objViewer.btnYBReg.Tag = "Register";
                        this.m_objViewer.btnYBCancelReg.Enabled = false;
                        this.m_objViewer.btnYBModifyReg.Enabled = false;

                    }
                    else//�����ѯ�����ľ����¼��Ϊ�գ�˵�����޸�ҽ���Ǽ���Ϣ
                    {
                        //ҽ���ѵǼ�
                        this.m_objViewer.btnYBReg.Tag = "Modify";
                        this.m_objViewer.lblTitle.Text = "ҽ������סԺ�Ǽ�(�޸�)";
                        this.m_objViewer.btnYBReg.Enabled = false;
                        this.m_objViewer.cboCYBZ.Enabled = true;

                    }
                }
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region ���ؿؼ�����
        /// <summary>
        /// ���ؿؼ�����
        /// </summary>
        /// <returns></returns>
        private long m_lngLoadDataCtl()
        {
            try
            {
                // �����б�
                clsColumns_VO[] columArr = new clsColumns_VO[]{
                new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                new clsColumns_VO("��������","deptname_vchr",HorizontalAlignment.Left,130)
                };
                m_objViewer.txtDept.m_strSQL = @"select   deptid_chr, deptname_vchr, pycode_chr, code_vchr
                                                    from t_bse_deptdesc t1
                                                   where attributeid = '0000003' and status_int = 1
                                                order by code_vchr";
                m_objViewer.txtDept.m_mthInitListView(columArr);

                //��ȡ��λ�б�
                columArr = new clsColumns_VO[]{
                new clsColumns_VO("����ID","areaid_chr",HorizontalAlignment.Left,0),
                new clsColumns_VO("��λID","bedid_chr",HorizontalAlignment.Left,0),
                new clsColumns_VO("��λ��","code_chr",HorizontalAlignment.Left,60)
                };
                m_objViewer.txtBedno.m_strSQL = @"select areaid_chr,bedid_chr,code_chr from t_bse_bed where status_int=1 and areaid_chr='0000209' order by bedid_chr,bed_no";
                m_objViewer.txtBedno.m_mthInitListView(columArr);

                //��ȡ����ҽ���б�
                columArr = new clsColumns_VO[]{
                new clsColumns_VO("����","empno_chr",HorizontalAlignment.Left,50),
                new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                new clsColumns_VO("����","doctorname",HorizontalAlignment.Left,80),
                };
                m_objViewer.txtDoctor.m_strSQL = @"select distinct t1.empid_chr,
                                                                    t2.empno_chr,
                                                                    t2.pycode_chr,
                                                                    t2.lastname_vchr as doctorname
                                                      from t_bse_deptemp t1, t_bse_employee t2
                                                     where t2.hasprescriptionright_chr = 1
                                                       and t2.status_int = 1
                                                       and t1.empid_chr = t2.empid_chr
                                                     order by t2.empno_chr";
                m_objViewer.txtDoctor.m_mthInitListView(columArr);
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        #endregion

        #region ��֤�����ϵĿؼ�
        /// <summary>
        /// ��֤�����ϵĿؼ�
        /// </summary>
        /// <returns></returns>
        private long m_lngCheckCtl()
        {
            foreach (System.Windows.Forms.Control c in m_objViewer.patientinto.Controls)
            {
                if (c.Name == "txtIDCard" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("�������֤����Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "txtZYH" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("����סԺ�Ų���Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "txtName" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("������������Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "txtDept" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("�������Ʋ���Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                //if (c.Name == "txtBedno" && c.Text.Trim().Length == 0)
                //{
                //    MessageBox.Show("��λ�Ų���Ϊ�գ���ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //    return -1;
                //}
                //if (c.Name == "txtDoctor" && c.Text.Trim().Length == 0)
                //{
                //    MessageBox.Show("����ҽ������Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //    return -1;
                //}
                if (c.Name == "cboZYLB")
                {
                    if (c.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("סԺ�����Ϊ�գ���ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return -1;
                    }
                    else if (c.Text.Trim() == "2-����סԺ")
                    {
                        this.m_objViewer.cboZQQRQK.Text = "1-ͬ��";
                        this.m_objViewer.cboZQQRQK.Enabled = false;
                    }
                    else if (c.Text.Trim() == "1-ҽ��סԺ")
                    {
                        if (string.IsNullOrEmpty(this.m_objViewer.cboZQQRQK.Text))
                        {
                            MessageBox.Show("֪��ȷ�����Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            this.m_objViewer.cboZQQRQK.Focus();
                            return -1;
                        }
                        if (this.m_objViewer.cobRYDYZDBY.SelectedIndex == 0)
                        {
                            MessageBox.Show("ҽ��סԺ������д��Ժ��һ��ϲ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            this.m_objViewer.cobRYDYZDBY.Focus();
                            return -1;
                        }
                    }
                }
                if (c.Name == "cboJZLB" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("���������Ϊ�գ���ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "txtPhone" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("��ϵ�绰����Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "txtDiagnsis" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("�����Ϣ����Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                if (c.Name == "dtmInHospitalDate" && c.Text.Trim().Length == 0)
                {
                    MessageBox.Show("����(��Ժ)���ڲ���Ϊ�գ���ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
            }
            if (this.m_objViewer.btnYBReg.Tag == null)
            {
                return -1;
            }
            return 1;
        }
        #endregion

        #region סԺҽ���Ǽ�
        /// <summary>
        /// סԺҽ���Ǽ�
        /// </summary>
        public void m_mthYbReg()
        {
            if (m_lngCheckCtl() < 0)
            {
                return;
            }

            //need modify �Ǽ�VO��ֵ
            #region ҽ��סԺ�Ǽ�VO��ֵ

            string bedNo = this.m_objViewer.txtBedno.Text.Trim();
            if (bedNo == string.Empty) bedNo = "9999";      // ��Ժ�Ǽ�ʱû�а��Ŵ���.�ݶ�Ϊ9999

            string doctNo = this.m_objViewer.txtDoctor.Value.ToString();
            if (doctNo == string.Empty) doctNo = "0000";    // ��Ժ�Ǽ�ʱû��ָ������ҽʦ.�ݶ�Ϊ0000

            clsDGZydj_VO objDgzydjVo = new clsDGZydj_VO();
            objDgzydjVo.GMSFHM = this.m_objViewer.txtIDCard.Text.Trim();
            objDgzydjVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne"); ;//סԺҽԺ���
            objDgzydjVo.ZYH = this.m_objViewer.txtZYH.Text.Trim();
            objDgzydjVo.ZYKS = this.m_objViewer.txtDept.Tag.ToString().Trim();//t_ins_deptrel���е��籣����id
            objDgzydjVo.YYRYKS = this.m_objViewer.txtDept.Text.Trim();//ҽԺ�Լ������Ŀ�������
            objDgzydjVo.CWH = bedNo;
            objDgzydjVo.RYRQ = Convert.ToDateTime(this.m_objViewer.dtmInHospitalDate.Text.Trim()).ToString("yyyyMMdd");
            objDgzydjVo.ZYLB = this.m_objViewer.cboZYLB.Text.Trim().Split('-')[0].ToString();
            objDgzydjVo.JZLB = this.m_objViewer.cboJZLB.Text.Trim().Split('-')[0].ToString();
            objDgzydjVo.RYZD = this.m_objViewer.txtDiagnsis.Text.Trim();
            objDgzydjVo.WSBZ = string.Empty;
            objDgzydjVo.ZQQRQK = this.m_objViewer.cboZQQRQK.Text.Trim().Split('-')[0].ToString();
            objDgzydjVo.ZQQRSBH = this.m_objViewer.txtZQQRSBH.Text.Trim();
            objDgzydjVo.YSGH = doctNo;
            objDgzydjVo.LXDH = this.m_objViewer.txtPhone.Text.Trim();
            objDgzydjVo.CYBZ = this.m_objViewer.cboCYBZ.Text.Trim().Split('-')[0].ToString();
            objDgzydjVo.YYCYKS = this.m_objViewer.txtcyks.Text.Trim();
            objDgzydjVo.CYRQ = Convert.ToDateTime(this.m_objViewer.dtmOutHospitalDate.Text.Trim()).ToString("yyyyMMdd");
            objDgextraVo.JZJLH = this.m_objViewer.txtJZJLH.Text.Trim();
            objDgzydjVo.BZ = "";
            objDgzydjVo.CBDTCQBM = this.m_objViewer.txtCbdtcqbm.Text.Trim();    // ����ؾ�ҽƽ̨Ԥ���ֶΡ�����440600	��ɽ��
            objDgzydjVo.SBBZ = this.m_objViewer.strSBBZ;
            objDgzydjVo.RYDYZDBY = dicRYDYZDBY[this.m_objViewer.cobRYDYZDBY.SelectedItem.ToString()];

            #region ���ҽ��
            DataRow dr = null;
            if (this.m_objViewer.txtMainDiag.Tag != null)
            {
                dr = this.m_objViewer.txtMainDiag.Tag as DataRow;
                objDgzydjVo.Icd10_1 = dr["icdcode"].ToString();
            }
            if (this.m_objViewer.txtSecDiag1.Tag != null)
            {
                dr = this.m_objViewer.txtSecDiag1.Tag as DataRow;
                objDgzydjVo.Icd10_2 = dr["icdcode"].ToString();
            }
            if (this.m_objViewer.txtSecDiag2.Tag != null)
            {
                dr = this.m_objViewer.txtSecDiag2.Tag as DataRow;
                objDgzydjVo.Icd10_3 = dr["icdcode"].ToString();
            }
            if (this.m_objViewer.cboZyyy.SelectedIndex > 0)
            {
                objDgzydjVo.InReason = this.m_objViewer.cboZyyy.Text.Trim().Split('-')[0].ToString();
            }
            else
            {
                objDgzydjVo.InReason = string.Empty;
            }
            if (this.m_objViewer.cboBzlx.SelectedIndex > 0)
            {
                objDgzydjVo.AssiType = this.m_objViewer.cboBzlx.Text.Trim().Split('-')[0].ToString();
            }
            else
            {
                objDgzydjVo.AssiType = string.Empty;
            }
            if (this.m_objViewer.cboCyyy.SelectedIndex > 0)
            {
                objDgzydjVo.OutStatus = this.m_objViewer.cboCyyy.Text.Trim().Split('-')[0].ToString();
            }
            else
            {
                objDgzydjVo.OutStatus = string.Empty;
            }
            objDgzydjVo.FYBH = "0";
            objDgzydjVo.TXBZ = this.m_objViewer.rdoTxYes.Checked ? "1" : "0";

            #endregion
            #endregion
            if (this.m_objViewer.btnYBReg.Tag.ToString() == "Register")
            {
                //ҽ���Ǽ�
                string strJzjlhTemp = string.Empty;
                long lngRes = clsYBPublic_cs.m_lngFunSP3001(objDgzydjVo, objDgextraVo, ref strJzjlhTemp);
                if (lngRes > 0)
                {
                    //ҽ���Ǽǳɹ� ����ҽ�����صľ����¼��JZJLH
                    this.m_objViewer.txtJZJLH.Text = strJzjlhTemp;
                    lngRes = this.objDomain.m_lngSaveYBZYRegInfo(this.m_objViewer.strRegisterId, strJzjlhTemp, this.m_objViewer.LoginInfo.m_strEmpID, objDgzydjVo);
                    if (lngRes > 0)
                    {
                        MessageBox.Show("ҽ��סԺ�Ǽǳɹ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        this.m_objViewer.btnYBReg.Enabled = false;
                        this.m_objViewer.btnYBModifyReg.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("ҽ��סԺ�Ǽ�ʧ�ܣ����Ժ����ԣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            else if (this.m_objViewer.btnYBReg.Tag.ToString() == "Modify")
            {
                // ��ʿ(����)�޸�
                if (this.m_objViewer.IsNurseModify)
                {
                    string jbrNo = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "JBRNO", "AnyOne");  // �����˹���
                    objDgextraVo.JBR = string.IsNullOrEmpty(jbrNo) ? "049" : jbrNo;
                }
                //ҽ���Ǽ���Ϣ�޸�.need modify��ҽ����ѡ���ܡ��޸�סԺ�Ǽ���Ϣ[SP3_3009]�������ڿ��������Ӹù���
                long lngRes = clsYBPublic_cs.m_lngFunSP3009(objDgzydjVo, objDgextraVo);
                if (lngRes > 0)
                {
                    //ҽ���Ǽ���Ϣ�޸�
                    lngRes = this.objDomain.m_lngUpdateYBZYRegInfo(this.m_objViewer.strRegisterId, objDgzydjVo, objDgextraVo);
                    if (lngRes > 0)
                    {
                        MessageBox.Show("ҽ��סԺ�Ǽ���Ϣ�޸ĳɹ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        this.m_objViewer.btnYBModifyReg.Enabled = false;
                        if (this.m_objViewer.IsNurseModify)
                        {
                            this.m_objViewer.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ҽ��סԺ�Ǽ���Ϣ�޸�ʧ�ܣ����Ժ����ԣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }
        #endregion

        #region ���˴�λ
        /// <summary>
        /// ���˴�λ
        /// </summary>
        public void m_mthFilterBed()
        {
            string areaID = string.Empty;
            if (this.m_objViewer.txtDept.Value != null)
            {
                areaID = this.m_objViewer.txtDept.Value.ToString();
            }

            if (string.IsNullOrEmpty(areaID))
            {
                areaID = "0000330";
            }
            m_objViewer.txtBedno.m_strSQL = @"select areaid_chr,bedid_chr,code_chr from t_bse_bed where status_int=1 and areaid_chr='" + areaID + @"'order by bedid_chr,bed_no";
            m_objViewer.txtBedno.m_mthGetData();
        }
        #endregion

        #region ����ҽ������
        /// <summary>
        /// ����ҽ������
        /// </summary>
        public void m_mthFilterDoc()
        {
            string areaID = string.Empty;
            if (this.m_objViewer.txtDept.Value != null)
            {
                areaID = this.m_objViewer.txtDept.Value.ToString();
            }

            if (string.IsNullOrEmpty(areaID))
            {
                areaID = "0000330";
            }
            m_objViewer.txtDoctor.m_strSQL = @"select distinct t1.empid_chr,
                                                                    t2.empno_chr,
                                                                    t2.pycode_chr,
                                                                    t2.lastname_vchr as doctorname
                                                      from t_bse_deptemp t1, t_bse_employee t2
                                                     where t2.hasprescriptionright_chr = 1
                                                       and t2.status_int = 1
                                                       and t1.empid_chr = t2.empid_chr
                                                       and t1.deptid_chr = '" + areaID + @"'
                                                     order by t2.empno_chr";
            m_objViewer.txtDoctor.m_mthGetData();
        }
        #endregion

        #region ��ʡ��ؿ���Ȩ
        /// <summary>
        /// ��ʡ��ؿ���Ȩ
        /// </summary>
        internal void Ksydkjq()
        {
            if (m_lngCheckCtl() < 0)
            {
                return;
            }

            string uri = string.Empty;
            string JBR = this.m_objViewer.LoginInfo.m_strEmpNo;
            clsDGZydj_VO vo = new clsDGZydj_VO();
            vo.GMSFHM = this.m_objViewer.txtIDCard.Text.Trim();
            vo.JZLB = this.m_objViewer.cboJZLB.Text.Trim().Split('-')[0].ToString();
            vo.RYRQ = Convert.ToDateTime(this.m_objViewer.dtmInHospitalDate.Text.Trim()).ToString("yyyyMMdd");
            vo.CBDTCQBM = this.m_objViewer.txtCbdtcqbm.Text.Trim();
            vo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");  // סԺҽԺ���
            clsYBPublic_cs.SP3_5002(vo, JBR, out uri);
            if (!string.IsNullOrEmpty(uri))
            {
                frmUri frm = new frmUri("��ʡ��ؿ���Ȩ", uri);
                frm.ShowDialog();
            }
        }
        #endregion
    }
}
