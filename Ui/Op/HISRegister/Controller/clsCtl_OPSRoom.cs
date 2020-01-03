using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsCtl_OPSRoom ��ժҪ˵����
    /// </summary>
    public class clsCtl_OPSRoom : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// 0 ���뵥 1 ���浥
        /// </summary>
        private int billflag = 0;
        /// <summary>
        /// ����
        /// </summary>
        private string applyid = "";
        /// <summary>
        /// ����,���ڶ϶��Ƿ������޸Ĺ�.
        /// </summary>
        private string m_strApplyId = "";

        private string[] anamode;

        private clsDcl_DoctorWorkstation objSvc;
        public clsCtl_OPSRoom()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            objSvc = new clsDcl_DoctorWorkstation();
        }
        /// <summary>
        /// �Ƿ������ֵ.
        /// </summary>
        public DialogResult DialogResultCancel = DialogResult.OK;
        /// <summary>
        /// ��¼��һ��ѡ����
        /// </summary>
        public int m_lsvReportPrvSelectIndex = -1;
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmOPSRoom m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOPSRoom)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            for (int i = 0; i < m_objViewer.outlookBar.Bands.Count; i++)
            {
                if (m_objViewer.outlookBar.Bands[i].ChildControl != null)
                    m_objViewer.outlookBar.Bands[i].ChildControl.Parent = m_objViewer.outlookBar;
            }

            for (int i = m_objViewer.outlookBar.Bands.Count - 1; i >= 0; i--)
            {
                m_objViewer.outlookBar.CurrentBand = i;
            }

            string Hospitalname = new com.digitalwave.iCare.common.clsCommmonInfo().m_strGetHospitalTitle();
            this.m_objViewer.lblAppTitle.Text = Hospitalname + "�����������뵥";
            this.m_objViewer.lblRepTitle.Text = Hospitalname + "����������¼��";

            m_mthSelectBill(0);

            clsCtl_OPSApply objCls = new clsCtl_OPSApply();
            objCls.m_mthSetTextBoxControlsBackColor(new Control[] {
                        this.m_objViewer.txtRepOPSName,
                        this.m_objViewer.txtRepDiagbegin,
                        this.m_objViewer.txtRepDiagend,
                        this.m_objViewer.txtRepMaindoctor,
                        this.m_objViewer.txtRepAssidoctor,
                        this.m_objViewer.txtRepMedtool,
                        this.m_objViewer.txtRepAnadoctor,
                        this.m_objViewer.txtRepOPSStepandResult,
                        this.m_objViewer.txtRepDoctor,
                        this.m_objViewer.txtRepSigndate
            });
        }
        #endregion

        #region ѡ���
        /// <summary>
        /// ѡ���
        /// </summary>
        /// <param name="index">0 ���뵥 1 ���浥</param>
        public void m_mthSelectBill(int index)
        {
            if (index == 0)
            {
                this.m_objViewer.panelReport.Visible = false;
                this.m_objViewer.panelApply.Visible = true;
                this.m_objViewer.panelApply.BringToFront();
            }
            else if (index == 1)
            {
                this.m_objViewer.panelApply.Visible = false;
                this.m_objViewer.panelReport.Visible = true;
                this.m_objViewer.panelReport.BringToFront();
            }
        }
        #endregion

        #region ��ձ�
        /// <summary>
        /// ��ձ�
        /// </summary>
        /// <param name="Control"></param>
        public void m_mthClear(Control Ctl)
        {
            string Type = Ctl.GetType().FullName;

            if (Ctl.AccessibleName.Trim().ToUpper().StartsWith("T"))
            {
                switch (Type)
                {
                    case "System.Windows.Forms.TextBox":
                        Ctl.Text = "";
                        break;
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        Ctl.Text = "";
                        break;
                }
            }

            foreach (Control Ctlchild in Ctl.Controls)
            {
                m_mthClear(Ctlchild);
            }
        }
        #endregion

        #region �ж��Ƿ���ʾδ�շ��������뵥
        /// <summary>
        /// �ж��Ƿ���ʾδ�շ��������뵥
        /// </summary>
        /// <returns></returns>
        private int m_intIsshowchrgops()
        {
            int ischrg = 0;
            DataTable dt = new DataTable();
            long ret = objSvc.m_lngGetWSParm("0048", out dt);		//0048 �ж��Ƿ���ʾδ�շ��������뵥
            if (ret > 0 && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SETSTATUS_INT"].ToString().Trim() == "1")
                {
                    ischrg = 1;
                }
            }

            return ischrg;
        }
        #endregion

        #region ����������Ϣ
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public void m_mthGetappinfo(int flag)
        {
            int ischrg = this.m_intIsshowchrgops();
            DataTable dtRecord = new DataTable();

            long ret = objSvc.m_lngGetOPSApply(out dtRecord, "", "", flag, ischrg);

            if (flag == 1)
            {
                this.m_objViewer.lvApply.BeginUpdate();
                this.m_objViewer.lvApply.Items.Clear();
            }
            else if (flag == 2)
            {
                this.m_objViewer.lvReport.BeginUpdate();
                this.m_objViewer.lvReport.Items.Clear();
            }

            if (ret > 0 && dtRecord.Rows.Count > 0)
            {
                DateTime dteBirth;
                string age = "";
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    if (dtRecord.Rows[i]["pstauts_int"].ToString().Trim() == "-2")
                    {
                        if (this.m_objViewer.m_chkTui.Checked == false)
                            continue;
                    }

                    //���뵥��
                    ListViewItem lv = new ListViewItem(dtRecord.Rows[i]["applyid_vchr"].ToString());
                    //�������
                    lv.SubItems.Add(dtRecord.Rows[i]["deptname_vchr"].ToString());
                    //����
                    lv.SubItems.Add(dtRecord.Rows[i]["name_vchr"].ToString());
                    //�Ա�
                    lv.SubItems.Add(dtRecord.Rows[i]["sex_chr"].ToString());
                    //����
                    dteBirth = Convert.ToDateTime(dtRecord.Rows[i]["birth_dat"].ToString());
                    age = com.digitalwave.controls.clsArithmetic.CalcAge(dteBirth);
                    lv.SubItems.Add(age);

                    if (flag == 1)
                    {
                        //����ʱ��
                        lv.SubItems.Add(((DateTime)dtRecord.Rows[i]["opsbookingdate_dat"]).ToString("yyyy/MM/dd HH:mm"));
                    }
                    else if (flag == 2)
                    {
                        //ȷ��ʱ��
                        lv.SubItems.Add(((DateTime)dtRecord.Rows[i]["confirmdate_dat"]).ToString("yyyy/MM/dd HH:mm"));

                        if (dtRecord.Rows[i]["repflag"] != null && dtRecord.Rows[i]["repflag"].ToString().Trim() != "")
                        {
                            lv.BackColor = Color.FromArgb(222, 239, 165);
                        }
                    }

                    lv.Tag = dtRecord.Rows[i];

                    if (flag == 1)
                    {
                        if (ischrg != 1)
                        {
                            if (dtRecord.Rows[i]["pstauts_int"].ToString().Trim() == "2" || dtRecord.Rows[i]["pstauts_int"].ToString().Trim() == "-2")
                            {
                                this.m_objViewer.lvApply.Items.Add(lv);
                            }
                        }
                        else
                        {
                            this.m_objViewer.lvApply.Items.Add(lv);
                        }
                        if (dtRecord.Rows[i]["pstauts_int"].ToString().Trim() == "-2")
                        {
                            lv.ForeColor = Color.Red;
                        }
                    }
                    else if (flag == 2)
                    {
                        this.m_objViewer.lvReport.Items.Add(lv);
                    }
                }
            }

            if (flag == 1)
            {
                this.m_objViewer.lvApply.EndUpdate();
            }
            else if (flag == 2)
            {
                this.m_objViewer.lvReport.EndUpdate();
            }
        }
        #endregion

        #region ���뵥��ֵ
        /// <summary>
        /// ���뵥��ֵ
        /// </summary>
        public void m_mthSetappvalue()
        {
            if (this.m_objViewer.lvApply.SelectedItems.Count > 0)
            {
                DataRow dr = (DataRow)(this.m_objViewer.lvApply.SelectedItems[0].Tag);

                //����
                DateTime dteBirth = Convert.ToDateTime(dr["birth_dat"].ToString());
                string age = com.digitalwave.controls.clsArithmetic.CalcAge(dteBirth);

                //ԤԼʱ��
                string bookingdate = ((DateTime)dr["opsbookingdate_dat"]).ToString("yyyy-MM-dd HH:mm");

                applyid = dr["applyid_vchr"].ToString();
                this.m_objViewer.lblAppNO.Text = "NO: " + applyid;
                this.m_objViewer.lblAppName.Text = dr["name_vchr"].ToString();
                this.m_objViewer.lblAppSex.Text = dr["sex_chr"].ToString();
                this.m_objViewer.lblAppAge.Text = age;
                this.m_objViewer.lblAppCardNo.Text = dr["patientcardid_chr"].ToString();
                this.m_objViewer.lblAppDept.Text = dr["deptname_vchr"].ToString();
                this.m_objViewer.lblAppOPSName.Text = dr["itemname_vchr"].ToString();
                this.m_objViewer.lblAppYear.Text = bookingdate.Substring(0, 4);
                this.m_objViewer.lblAppMonth.Text = bookingdate.Substring(5, 2);
                this.m_objViewer.lblAppDay.Text = bookingdate.Substring(8, 2);
                this.m_objViewer.lblAppHour.Text = bookingdate.Substring(11, 2);
                this.m_objViewer.lblAppMinute.Text = bookingdate.Substring(14, 2);
                this.m_objViewer.lblAppHint.Text = dr["note_vchr"].ToString();
                this.m_objViewer.lblAppDoctor.Text = dr["lastname_vchr"].ToString();
                this.m_objViewer.lblAppDate.Text = ((DateTime)dr["recorddate_dat"]).ToString("yyyy��MM��dd��");

                billflag = 0;
                //this.m_objViewer.btnPrint.Enabled = false;
                //this.m_objViewer.btnSave.Enabled = false;                
            }
        }
        #endregion

        #region ����Ա����Ϣ�������Ϣ
        /// <summary>
        /// ����Ա������,��ID
        /// </summary>
        /// <param name="p_strSQLWhere"></param>
        /// <param name="p_strEmployeeArr"></param>
        /// <returns></returns>
        private long m_intGetEmployeeInfo(string p_strSQLWhere, out DataTable p_strEmployeeArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                               (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long ret = (new weCare.Proxy.ProxyOP()).Service.m_lngGetEmployeeNameByID(p_strSQLWhere, out p_strEmployeeArr);
            return ret;
        }
        /// <summary>
        /// ����ID,��Ա������
        /// </summary>
        /// <param name="p_strSQLWhere"></param>
        /// <returns></returns>
        private string m_intGetEmployeeNameInfoByID(string p_strSQLWhere)
        {
            string strRet = "";
            DataTable strEmployeeArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                               (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long ret = (new weCare.Proxy.ProxyOP()).Service.m_lngGetEmployeeNameByID(p_strSQLWhere, out strEmployeeArr);
            if (ret >= 0)
            {
                if (strEmployeeArr != null && strEmployeeArr.Rows.Count > 0)
                {
                    strRet = strEmployeeArr.Rows[0][1].ToString();
                }
            }
            return strRet;
        }
        #endregion 
        /// <summary>
        /// �ݴ���������
        /// </summary>
        private string strOPSName = "";
        #region ���浥��ֵ
        /// <summary>
        /// ���浥��ֵ
        /// </summary>
        /// <param name="dr"></param>
        public void m_mthSetreportvalue(DataRow dr)
        {
            //����
            DateTime dteBirth = Convert.ToDateTime(dr["birth_dat"].ToString());
            string age = com.digitalwave.controls.clsArithmetic.CalcAge(dteBirth);

            applyid = dr["applyid_vchr"].ToString();
            m_strApplyId = applyid;
            this.m_objViewer.lblRepNO.Text = "NO: " + applyid;
            this.m_objViewer.lblRepName.Text = dr["name_vchr"].ToString();
            this.m_objViewer.lblRepSex.Text = dr["sex_chr"].ToString();
            this.m_objViewer.lblRepAge.Text = age;
            this.m_objViewer.lblRepCardNo.Text = dr["patientcardid_chr"].ToString();
            this.m_objViewer.lblRepDept.Text = dr["deptname_vchr"].ToString();

            DataTable dt = new DataTable();
            long ret = objSvc.m_lngGetopsrecord(applyid, out dt);
            if (dt.Rows.Count == 1)
            {
                //����ʱ��
                string opsdate = ((DateTime)dt.Rows[0]["opsdate_dat"]).ToString("yyyy-MM-dd");
                //����ʽ
                int index = 0;
                string anamodecode = dt.Rows[0]["opsanamode_chr"].ToString();
                for (int i = 0; i < anamode.Length; i++)
                {
                    if (anamodecode == anamode[i])
                    {
                        index = i;
                        break;
                    }
                }

                this.m_objViewer.lblRepYear.Text = opsdate.Substring(0, 4);
                this.m_objViewer.lblRepMonth.Text = opsdate.Substring(5, 2);
                this.m_objViewer.lblRepDay.Text = opsdate.Substring(8, 2);
                this.m_objViewer.txtRepOPSName.Text = dt.Rows[0]["opsname_vchr"].ToString();
                this.m_objViewer.txtRepDiagbegin.Text = dt.Rows[0]["prediagnoses_vchr"].ToString();
                this.m_objViewer.txtRepDiagend.Text = dt.Rows[0]["enddiagnoses_vchr"].ToString();
                this.m_objViewer.txtRepMaindoctor.Tag = dt.Rows[0]["opsdoctor_chr"].ToString();
                this.m_objViewer.txtRepMaindoctor.Text = m_intGetEmployeeNameInfoByID(" empid_chr ='" + dt.Rows[0]["opsdoctor_chr"].ToString() + "'");
                this.m_objViewer.txtRepAssidoctor.Tag = dt.Rows[0]["opsassistant1_chr"].ToString();
                this.m_objViewer.txtRepAssidoctor.Text = m_intGetEmployeeNameInfoByID(" empid_chr ='" + dt.Rows[0]["opsassistant1_chr"].ToString() + "'");
                this.m_objViewer.txtRepMedtool.Tag = dt.Rows[0]["opsappliance_chr"].ToString();
                this.m_objViewer.txtRepMedtool.Text = m_intGetEmployeeNameInfoByID(" empid_chr ='" + dt.Rows[0]["opsappliance_chr"].ToString() + "'");
                this.m_objViewer.cboAnamode.SelectedIndex = index;
                this.m_objViewer.txtRepAnadoctor.Tag = dt.Rows[0]["anaemp1_chr"].ToString();
                this.m_objViewer.txtRepAnadoctor.Text = m_intGetEmployeeNameInfoByID(" empid_chr ='" + dt.Rows[0]["anaemp1_chr"].ToString() + "'");
                this.m_objViewer.txtRepOPSStepandResult.Text = dt.Rows[0]["opsresult_vchr"].ToString();
                this.m_objViewer.txtRepDoctor.Tag = dt.Rows[0]["signdoctor_chr"].ToString();
                this.m_objViewer.txtRepDoctor.Text = m_intGetEmployeeNameInfoByID(" empid_chr ='" + dt.Rows[0]["signdoctor_chr"].ToString() + "'");
                this.m_objViewer.txtRepSigndate.Text = ((DateTime)dt.Rows[0]["signdate_dat"]).ToString("yyyy-MM-dd");
                if (dt.Rows[0]["status_int"].ToString().Trim() == "1")
                {
                    this.m_objViewer.lblRepsave.Text = "�����";
                }
                else
                {
                    this.m_objViewer.lblRepsave.Text = "�ѱ���";
                }
            }
            else
            {
                this.m_objViewer.lblRepYear.Text = "";
                this.m_objViewer.lblRepMonth.Text = "";
                this.m_objViewer.lblRepDay.Text = "";

                this.m_objViewer.txtRepOPSName.Text = dr["itemname_vchr"].ToString();
                strOPSName = this.m_objViewer.txtRepOPSName.Text;
                this.m_objViewer.txtRepDiagbegin.Text = "";
                this.m_objViewer.txtRepDiagend.Text = "";
                this.m_objViewer.txtRepMaindoctor.Text = "";
                this.m_objViewer.txtRepAssidoctor.Text = "";
                this.m_objViewer.txtRepMedtool.Text = "";
                this.m_objViewer.cboAnamode.SelectedIndex = 0;
                this.m_objViewer.txtRepAnadoctor.Text = "";
                this.m_objViewer.txtRepOPSStepandResult.Text = "";
                this.m_objViewer.txtRepDoctor.Tag = this.m_objViewer.LoginInfo.m_strEmpID;
                this.m_objViewer.txtRepDoctor.Text = this.m_objViewer.LoginInfo.m_strEmpName;
                this.m_objViewer.txtRepSigndate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.m_objViewer.lblRepsave.Text = "";
            }

            this.m_mthSelectBill(1);
            billflag = 1;
        }
        #endregion

        #region ȷ�ϸ��ĺ��Ƿ񱣴�
        /// <summary>
        /// ȷ�ϸ��ĺ��Ƿ񱣴�
        /// </summary>
        /// <returns>�Ƿ�ͨ��,ת����һ����.</returns>
        public bool m_blnCheckAlterToSave()
        {
            if (m_strApplyId == "")
            {
                return true;
            }
            bool blnAlter = false;
            bool blnSuccess = false;
            string strWainIng = "���������Ѿ��޸ģ��Ƿ񱣴棿";
            DataTable dt = new DataTable();

            long ret = objSvc.m_lngGetopsrecord(m_strApplyId, out dt);
            if (dt.Rows.Count == 1)
            {
                //����ʽ
                int index = 0;
                string anamodecode = dt.Rows[0]["opsanamode_chr"].ToString();
                for (int i = 0; i < anamode.Length; i++)
                {
                    if (anamodecode == anamode[i])
                    {
                        index = i;
                        break;
                    }
                }
                if (this.m_objViewer.txtRepOPSName.Text != dt.Rows[0]["opsname_vchr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepDiagbegin.Text != dt.Rows[0]["prediagnoses_vchr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepDiagend.Text != dt.Rows[0]["enddiagnoses_vchr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepMaindoctor.Tag.ToString() != dt.Rows[0]["opsdoctor_chr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepAssidoctor.Tag.ToString() != dt.Rows[0]["opsassistant1_chr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepMedtool.Tag.ToString() != dt.Rows[0]["opsappliance_chr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.cboAnamode.SelectedIndex != index)
                    blnAlter = true;
                if (this.m_objViewer.txtRepAnadoctor.Tag.ToString() != dt.Rows[0]["anaemp1_chr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepOPSStepandResult.Text != dt.Rows[0]["opsresult_vchr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepDoctor.Tag.ToString() != dt.Rows[0]["signdoctor_chr"].ToString())
                    blnAlter = true;
                if (this.m_objViewer.txtRepSigndate.Text != ((DateTime)dt.Rows[0]["signdate_dat"]).ToString("yyyy-MM-dd"))
                    blnAlter = true;
            }
            else
            {
                //����ʽ
                if (this.m_objViewer.cboAnamode.Items.Count > 0)
                {
                    if (this.m_objViewer.cboAnamode.SelectedIndex != 0)
                    {
                        blnAlter = true;
                    }
                }
                if (this.m_objViewer.txtRepOPSName.Text != strOPSName)
                    blnAlter = true;
                if (this.m_objViewer.txtRepDiagbegin.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepDiagend.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepMaindoctor.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepAssidoctor.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepMedtool.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepAnadoctor.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepOPSStepandResult.Text != "")
                    blnAlter = true;
                if (this.m_objViewer.txtRepDoctor.Text != this.m_objViewer.LoginInfo.m_strEmpName)
                    blnAlter = true;
                if (this.m_objViewer.txtRepSigndate.Text != DateTime.Now.ToString("yyyy-MM-dd"))
                    blnAlter = true;
            }
            if (blnAlter)
            {
                DialogResult dret = MessageBox.Show(strWainIng, "ϵͳ��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dret == DialogResult.Yes)
                {
                    blnSuccess = m_mthSave();
                }
                else if (dret == DialogResult.No)
                {
                    blnSuccess = true;
                }
                else if (dret == DialogResult.Cancel)
                {
                    DialogResultCancel = DialogResult.Cancel;
                    blnSuccess = false;
                }
            }
            else
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }
        #endregion 
        #region ���(ȷ��)
        /// <summary>
        /// ���(ȷ��)
        /// </summary>
        public void m_mthConfirm()
        {
            if (billflag != 0)
            {
                return;
            }

            if (applyid == "")
            {
                return;
            }

            frmOPSConfirm fc = new frmOPSConfirm();
            if (fc.ShowDialog() == DialogResult.OK)
            {
                string empid = fc.Empid;
                long ret = objSvc.m_lngConfrimOPS(applyid, empid);
                if (ret > 0)
                {
                    MessageBox.Show("���뵥ȷ�ϳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.m_objViewer.Cursor = Cursors.WaitCursor;
                    m_objViewer.outlookBar.CurrentBand = 1;
                    this.m_mthGetappinfo(2);


                    for (int i = 0; i < this.m_objViewer.lvApply.Items.Count; i++)
                    {
                        if (this.m_objViewer.lvApply.Items[i].SubItems[0].Text == applyid)
                        {
                            this.m_objViewer.lvApply.Items[i].Remove();
                            break;
                        }
                    }

                    for (int j = 0; j < this.m_objViewer.lvReport.Items.Count; j++)
                    {
                        if (this.m_objViewer.lvReport.Items[j].SubItems[0].Text == applyid)
                        {
                            DataRow dr = (DataRow)(this.m_objViewer.lvReport.Items[j].Tag);
                            this.m_mthSetreportvalue(dr);
                            break;
                        }
                    }

                    this.m_mthSelectBill(1);
                    this.m_objViewer.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("ȷ�����뵥ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        /// <summary>
        /// ���浥���(ȷ��)
        /// </summary>
        public void m_mthConfirmReport()
        {
            if (billflag != 1)
            {
                return;
            }

            if (applyid == "")
            {
                return;
            }
            if (this.m_objViewer.lblRepsave.Text.Trim() == "�����")
            {
                MessageBox.Show("���������,�����ظ���ˡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            frmOPSConfirm fc = new frmOPSConfirm();
            if (fc.ShowDialog() == DialogResult.OK)
            {
                string empid = fc.Empid;
                long ret = objSvc.m_lngConfrimOPSReport(applyid, empid);
                if (ret > 0)
                {
                    MessageBox.Show("������˳ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_objViewer.outlookBar.CurrentBand = 1;
                    this.m_objViewer.lblRepsave.Text = "�����";
                }
                else
                {
                    MessageBox.Show("ȷ�����뵥ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        #endregion

        #region ����������¼���Ա���ֵ��������Ա
        /// <summary>
        ///  ����������¼���Ա���ֵ��������Ա
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckEmployeeIn(string p_strWhere, Control p_ctl)
        {
            bool blnRes = false;
            DataTable strEmployeeArr = null;
            long res = m_intGetEmployeeInfo(p_strWhere, out strEmployeeArr);
            if (res < 0)
            {
                MessageBox.Show("���ݿ����ʧ��,����ϵϵͳ����Ա��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                p_ctl.Focus();
                p_ctl.Text = "";
                blnRes = false;
            }
            else if (res == 0)
            {
                MessageBox.Show("����������¼���Ա���ֵ��������Ա,������ѡ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                p_ctl.Focus();
                p_ctl.Text = "";
                blnRes = false;
            }
            else if (res == 1)
            {
                if (p_ctl.Tag != null)
                {
                    if (p_ctl.Tag.ToString() != "")
                    {
                        blnRes = true;
                        return blnRes;
                    }
                }
                if (strEmployeeArr != null)
                {
                    if (strEmployeeArr.Rows.Count > 2)
                    {
                        MessageBox.Show("Ա���ֵ�����Ĵ��ڶ����ͬ����ѡ��,��ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        p_ctl.Focus();
                        clsAssistantInput objAInput = new clsAssistantInput(p_ctl, this.m_objViewer.PointToClient(this.m_objViewer.panelReport.PointToScreen(new Point(((Control)p_ctl).Left, ((Control)p_ctl).Bottom))));
                        objAInput.m_mthSetEmployeeName();
                        blnRes = false;
                    }
                    else
                    {
                        p_ctl.Tag = strEmployeeArr.Rows[0][0].ToString();
                        blnRes = true;
                    }
                }
            }
            return blnRes;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public bool m_mthSave()
        {
            bool blnSuccess = false;

            if (billflag != 1)
            {
                return false;
            }

            if (applyid == "")
            {
                return false;
            }
            if (this.m_objViewer.lblRepsave.Text == "�����")
            {
                MessageBox.Show("���������,�������޸ġ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (this.m_objViewer.txtRepOPSName.Text.Trim() == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ�ա�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (this.m_objViewer.txtRepMaindoctor.Text.Trim() == "")
            {
                MessageBox.Show("������(����ҽ��)����Ϊ�ա�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            try
            {
                Convert.ToDateTime(this.m_objViewer.txtRepSigndate.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("ǩ�����ڸ�ʽ����ȷ��(��ȷ����:2006-03-29)", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.m_objViewer.txtRepSigndate.Focus();
                return false;
            }
            bool blnCheck;
            blnCheck = m_blnCheckEmployeeIn(" lastname_vchr ='" + m_objViewer.txtRepMaindoctor.Text.Trim() + "'", m_objViewer.txtRepMaindoctor);
            if (!blnCheck)
            { return false; }
            blnCheck = m_blnCheckEmployeeIn(" lastname_vchr ='" + m_objViewer.txtRepAssidoctor.Text.Trim() + "'", m_objViewer.txtRepAssidoctor);
            if (!blnCheck)
            { return false; }
            blnCheck = m_blnCheckEmployeeIn(" lastname_vchr ='" + m_objViewer.txtRepMedtool.Text.Trim() + "'", m_objViewer.txtRepMedtool);
            if (!blnCheck)
            { return false; }
            blnCheck = m_blnCheckEmployeeIn(" lastname_vchr ='" + m_objViewer.txtRepAnadoctor.Text.Trim() + "'", m_objViewer.txtRepAnadoctor);
            if (!blnCheck)
            { return false; }
            blnCheck = m_blnCheckEmployeeIn(" lastname_vchr ='" + m_objViewer.txtRepDoctor.Text.Trim() + "'", m_objViewer.txtRepDoctor);
            if (!blnCheck)
            { return false; }
            int index = this.m_objViewer.cboAnamode.SelectedIndex;

            clsOutops_VO objops_vo = new clsOutops_VO();
            objops_vo.applyid = applyid;
            objops_vo.opsname = this.m_objViewer.txtRepOPSName.Text.Trim();
            objops_vo.opsdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objops_vo.prediagnoses = this.m_objViewer.txtRepDiagbegin.Text.Trim();
            objops_vo.enddiagnoses = this.m_objViewer.txtRepDiagend.Text.Trim();
            //objops_vo.opsdoctor = this.m_objViewer.txtRepMaindoctor.Text.Trim();
            objops_vo.opsdoctor = this.m_objViewer.txtRepMaindoctor.Tag.ToString().Trim();
            objops_vo.opsassistant1 = this.m_objViewer.txtRepAssidoctor.Tag.ToString().Trim();
            objops_vo.opsappliance = this.m_objViewer.txtRepMedtool.Tag.ToString().Trim();
            objops_vo.opsanamode = anamode[index];
            objops_vo.anaempid1 = this.m_objViewer.txtRepAnadoctor.Tag.ToString().Trim();
            objops_vo.opsresult = this.m_objViewer.txtRepOPSStepandResult.Text.Trim();
            objops_vo.signdoctor = this.m_objViewer.txtRepDoctor.Tag.ToString().Trim();
            objops_vo.signdate = this.m_objViewer.txtRepSigndate.Text.Trim();

            long ret = objSvc.m_lngSaveOPS(applyid, objops_vo);
            if (ret > 0)
            {
                this.m_objViewer.lblRepsave.Text = "�ѱ���";
                blnSuccess = true;
                MessageBox.Show("�������浥��Ϣ����ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                blnSuccess = false;
                MessageBox.Show("�������浥��Ϣ����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return blnSuccess;
        }
        #endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void m_mthPrint()
        {
            if (billflag == 0)
            {
                return;
            }
            if (applyid == "")
                return;
            if (this.m_objViewer.lblRepsave.Text.Trim() != "�����")
            {
                MessageBox.Show("������δ���,���ܴ�ӡ���档", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            frmOPCrytalReport fp = new frmOPCrytalReport(2);
            fp.TextYearRoom = this.m_objViewer.lblRepYear.Text;
            fp.TextMonthRoom = this.m_objViewer.lblRepMonth.Text;
            fp.TextdayRoom = this.m_objViewer.lblRepDay.Text;
            fp.TextNameRoom = this.m_objViewer.lblRepName.Text;
            fp.Text1SexRoom = this.m_objViewer.lblRepSex.Text;
            fp.Text1AgeRoom = this.m_objViewer.lblRepAge.Text;
            fp.Text1CardRoom = this.m_objViewer.lblRepCardNo.Text;
            fp.TextDeptRoom = this.m_objViewer.lblRepDept.Text;
            fp.TextDiagoseRoom = this.m_objViewer.txtRepDiagbegin.Text;
            fp.TextDialogseAfterRoom = this.m_objViewer.txtRepDiagend.Text;
            fp.TextOPERRoom = this.m_objViewer.txtRepMaindoctor.Text;
            fp.TextHelperRoom = this.m_objViewer.txtRepAnadoctor.Text;
            fp.TextCarRoom = this.m_objViewer.txtRepMedtool.Text;
            fp.TextWayRoom = this.m_objViewer.cboAnamode.Text;
            fp.TextWayERRoom = this.m_objViewer.txtRepAnadoctor.Text;
            fp.TextStepRoom = this.m_objViewer.txtRepOPSStepandResult.Text;
            fp.TextDocNameRoom = this.m_objViewer.txtRepDoctor.Text;
            fp.TextTimeRoom = this.m_objViewer.txtRepSigndate.Text;
            fp.TextOPNameRoom = this.m_objViewer.txtRepOPSName.Text;

            fp.ShowDialog();
        }
        #endregion

        #region ���ý���
        /// <summary>
        /// ���ý���
        /// </summary>
        /// <param name="kea"></param>
        /// <param name="ctl"></param>
        public void m_mthSetfoucs(KeyEventArgs kea, string ctl)
        {
            if (kea.KeyCode == Keys.Enter)
            {
                switch (ctl)
                {
                    case "txtRepOPSName":
                        this.m_objViewer.txtRepDiagbegin.Focus();
                        break;
                    case "txtRepDiagbegin":
                        this.m_objViewer.txtRepDiagend.Focus();
                        break;
                    case "txtRepDiagend":
                        this.m_objViewer.txtRepMaindoctor.Focus();
                        break;
                    case "txtRepMaindoctor":
                        this.m_objViewer.txtRepAssidoctor.Focus();
                        break;
                    case "txtRepAssidoctor":
                        this.m_objViewer.txtRepMedtool.Focus();
                        break;
                    case "txtRepAnamode":
                        this.m_objViewer.txtRepAnadoctor.Focus();
                        break;
                    case "txtRepAnadoctor":
                        this.m_objViewer.txtRepOPSStepandResult.Focus();
                        break;
                    case "txtRepDoctor":
                        this.m_objViewer.txtRepSigndate.Focus();
                        break;
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ���� type 0 ���� 1 ����
        /// </summary>
        public void m_mthFind(int type)
        {
            if (type == 0)
            {
                frmOPSFindapply fa = new frmOPSFindapply(0);
                fa.ShowDialog();
            }
            else if (type == 1)
            {
                frmOPSFindreport ff = new frmOPSFindreport();
                if (ff.ShowDialog() == DialogResult.OK)
                {
                    applyid = ff.Applyid;
                    m_strApplyId = ff.Applyid;
                    this.m_objViewer.Cursor = Cursors.WaitCursor;

                    DataTable dt = new DataTable();
                    long ret = objSvc.m_lngGetOPSApply(out dt, applyid);
                    if (dt.Rows.Count == 1)
                    {
                        this.m_mthSetreportvalue(dt.Rows[0]);
                        this.m_mthSelectBill(1);
                    }
                    this.m_objViewer.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region ��������ʽ
        /// <summary>
        /// ��������ʽ
        /// </summary>
        public void m_mthSetanamode()
        {
            DataTable dt = new DataTable();
            long ret = objSvc.m_lngGetanaesthesiamode(out dt);
            if (dt.Rows.Count > 0)
            {
                anamode = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.m_objViewer.cboAnamode.Items.Add(dt.Rows[i]["ANAESTHESIAMODENAME"].ToString());
                    anamode[i] = dt.Rows[i]["ANAESTHESIAMODEID"].ToString();
                }
                this.m_objViewer.cboAnamode.SelectedIndex = 0;
            }
            else
            {
                anamode = new string[1];
                anamode[0] = "0001";
            }
        }
        #endregion
    }

    public class clsAssistantInput
    {
        private System.Drawing.Point m_potLocation;
        private Control m_ctlMain;
        public clsAssistantInput(Control p_ctlSender, System.Drawing.Point p_potLocation)
        {
            m_ctlMain = p_ctlSender;
            m_potLocation = p_potLocation;
        }

        /// <summary>
		/// ģ������ҽʦ��ְ����
		/// </summary>
		public void m_mthSetEmployeeName()
        {
            if (m_ctlMain == null)
            {
                return;
            }

            DataTable strEmployeeArr = null;

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = (new weCare.Proxy.ProxyOP()).Service.m_lngGetEmployeeNameByLikeNew(m_ctlMain.Text.Trim(), out strEmployeeArr);
            if (strEmployeeArr != null)
            {
                new clsAssisantInputList(m_ctlMain, strEmployeeArr, 2, m_potLocation);
            }
            else
            {
                SendKeys.Send("{tab}");
            }
        }
    }

    public class clsAssisantInputList
    {
        private ListView m_lstItemList;
        private int m_intType;

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="p_ctlSender">����ؼ�</param>
        /// <param name="p_strValueArr">�����</param>
        /// <param name="p_intType">����</param>
        /// <param name="p_potLocation">λ��</param>
        public clsAssisantInputList(Control p_ctlSender, DataTable p_strValueArr, int p_intType, System.Drawing.Point p_potLocation)
        {
            if (p_ctlSender == null || p_strValueArr == null || p_strValueArr.Rows.Count == 0)
                return;
            m_intType = p_intType;

            m_lstItemList = null;
            m_lstItemList = new ListView();
            m_lstItemList.Size = new System.Drawing.Size(150, 200);
            m_lstItemList.View = View.Details;
            m_lstItemList.FullRowSelect = true;
            m_lstItemList.MultiSelect = false;
            m_lstItemList.GridLines = true;
            m_lstItemList.HeaderStyle = ColumnHeaderStyle.None;
            m_lstItemList.Columns.Add("EMPID_CHR", 0, HorizontalAlignment.Left);
            m_lstItemList.Columns.Add("ID", 50, HorizontalAlignment.Left);
            m_lstItemList.Columns.Add("NAME", 80, HorizontalAlignment.Left);
            m_lstItemList.DoubleClick += new EventHandler(m_mth_DoubleClick_1);
            m_lstItemList.LostFocus += new EventHandler(m_lstItemList_Leave);
            m_lstItemList.KeyDown += new KeyEventHandler(m_mth_KeyDown_1);
            m_lstItemList.Tag = p_ctlSender;

            for (int i = 0; i < p_strValueArr.Rows.Count - 1; i++)
            {
                Application.DoEvents();
                ListViewItem item = m_lstItemList.Items.Add(p_strValueArr.Rows[i][2].ToString());
                item.SubItems.Add(p_strValueArr.Rows[i][0].ToString());
                item.SubItems.Add(p_strValueArr.Rows[i][1].ToString());
            }
            //p_ctlSender.Tag = m_lstItemList;
            p_ctlSender.FindForm().Controls.Add(m_lstItemList);
            if (p_ctlSender.FindForm().Width >= (p_potLocation.X + m_lstItemList.Width))
                m_lstItemList.Location = p_potLocation;
            else
            {
                System.Drawing.Point pot = p_potLocation;
                pot.X -= p_potLocation.X + m_lstItemList.Width - p_ctlSender.FindForm().Width;
                m_lstItemList.Location = pot;
            }
            m_lstItemList.BringToFront();
            m_lstItemList.Visible = true;
            m_lstItemList.Show();
            if (m_lstItemList.Items.Count > 0)
            {
                m_lstItemList.Focus();
                m_lstItemList.TopItem.Selected = true;
            }
        }

        private void m_mth_DoubleClick_1(object sender, System.EventArgs e)
        {
            ListView lstItem = sender as ListView;
            if (lstItem.SelectedItems.Count > 0)
            {
                Control ctlBase = lstItem.Tag as Control;
                if (ctlBase == null)
                {
                    return;
                }
                ctlBase.Text = lstItem.SelectedItems[0].SubItems[m_intType].Text;
                ctlBase.Tag = lstItem.SelectedItems[0].SubItems[0].Text;
                SendKeys.Send("{tab}");
                lstItem.Visible = false;
                ctlBase.Focus();
                lstItem.Parent.Controls.Remove(lstItem);
            }
        }

        private void m_mth_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ListView lsvSender = sender as ListView;
            switch (e.KeyValue)
            {
                case 13:// enter
                    m_mth_DoubleClick_1(sender, null);
                    break;
                case 38://Arrow Up
                    if (lsvSender.Items.Count > 0 && lsvSender.TopItem.Selected)
                    {
                        Control ctlBase = lsvSender.Tag as Control;
                        if (ctlBase != null)
                            ctlBase.Focus();
                    }
                    break;
                case 40://Arrow Down
                    break;
            }
        }
        private void m_mth_KeyDown_2(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)40://Arrow Down
                    if (m_lstItemList.CanFocus && m_lstItemList != null)
                    {
                        m_lstItemList.Focus();
                        m_lstItemList.Items[0].Selected = true;
                    }
                    break;
            }
        }

        private void m_lstItemList_Leave(object sender, EventArgs e)
        {
            ((Control)sender).Visible = false;
        }
    }
}
