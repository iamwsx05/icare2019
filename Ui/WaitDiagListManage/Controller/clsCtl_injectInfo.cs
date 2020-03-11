using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using weCare.Core.Entity;
using iCare.CustomForm;
using com.digitalwave.controls;
using System.Data;
using com.digitalwave.iCare.middletier.HI;
using com.digitalwave.iCare.middletier.HIS;
using System.IO;
using System.Xml;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsCtl_injectInfo ��ժҪ˵����
    /// </summary>
    public class clsCtl_injectInfo : com.digitalwave.GUI_Base.clsController_Base
    {
        private clsDcl_injectInfo objSvc = null;
        public clsCtl_injectInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            objSvc = new clsDcl_injectInfo();
        }


        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frminjectInfo m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frminjectInfo)frmMDI_Child_Base_in;
        }
        #endregion
        /// <summary>
        /// ���没����Ϣ
        /// </summary>
        DataTable dtPatient = null;
        /// <summary>
        /// ������Ŀ��Ϣ����ӡ��
        /// </summary>
        DataTable printdt = null;
        #region ���տ��Ų鲡����Ϣ
        public void m_mthGetPaientInfoByCardID()
        {
            if (this.m_objViewer.txtCardID.Text.Trim() == "")
            {
                MessageBox.Show("�����뿨��!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtCardID.Focus();
                return;
            }
            long ret = this.m_mthGetPatientInfo("a.patientcardid_chr", this.m_objViewer.txtCardID.Text.Trim(), out dtPatient);
            m_mthFillDataToTextBox(dtPatient.Rows[0]);
        }
        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        public void m_mthFillDeptdesc()
        {
            DataTable dtDeptdesc = new DataTable();
            objSvc.m_mthGetAllDeptdescByEmpId(out dtDeptdesc, this.m_objViewer.LoginInfo.m_strEmpID);
            dtDeptdesc.Columns[0].ColumnName = "���Ŵ���";
            dtDeptdesc.Columns[1].ColumnName = "��  ��  ��  ��";
            dtDeptdesc.Columns[2].ColumnName = "ƴ����";
            dtDeptdesc.Columns[3].ColumnName = "�����";
            dtDeptdesc.Columns[4].ColumnName = "ID";

            this.m_objViewer.txtEmp.m_GetDataTable = dtDeptdesc;
        }
        #endregion

        #region �����Ϣ��TextBox
        public void m_mthFillDataToTextBox(DataRow dr)
        {
            this.m_objViewer.txtCardID.Text = dr["PATIENTCARDID_CHR"].ToString();
            this.m_objViewer.txtCardID.Tag = dr["PATIENTID_CHR"].ToString();
            this.m_objViewer.txtName.Text = dr["NAME_VCHR"].ToString();
            this.m_objViewer.txtSex.Text = dr["SEX_CHR"].ToString();
            this.m_objViewer.txtDeptName.Text = dr["DEPTNAME_VCHR"].ToString();
            this.m_objViewer.txtDeptName.Tag = dr;

            try
            {
                this.m_objViewer.txtAge.Tag = dr["BIRTH_DAT"].ToString();
                //this.m_objViewer.txtAge.Text =clsArithmetic.CalcAge(DateTime.Parse(dr["BIRTH_DAT"].ToString()));
                this.m_objViewer.txtAge.Text = new clsBrithdayToAge().m_strGetAge(DateTime.Parse(dr["BIRTH_DAT"].ToString()));
            }
            catch
            {
                this.m_objViewer.txtAge.Text = "";
            }
            this.m_objViewer.btFind.Focus();

        }
        #endregion
        #region ��ȡtextBox��ֵ
        /// <summary>
        /// ��ȡtextBox��ֵ
        /// </summary>
        /// <returns></returns>
        private System.Data.DataRow m_getDataRow()
        {
            System.Data.DataRow CurrRow = dtPatient.NewRow();
            CurrRow["PATIENTCARDID_CHR"] = this.m_objViewer.txtCardID.Text;
            CurrRow["PATIENTID_CHR"] = (string)this.m_objViewer.txtCardID.Tag;
            CurrRow["NAME_VCHR"] = this.m_objViewer.txtName.Text;
            CurrRow["SEX_CHR"] = this.m_objViewer.txtSex.Text;
            CurrRow["BIRTH_DAT"] = DateTime.Parse((string)this.m_objViewer.txtAge.Tag);
            return CurrRow;
        }

        #endregion
        #region ���������鲡����Ϣ
        public void m_mthGetPaientInfoByName()
        {
            if (this.m_objViewer.txtName.Text.Trim() == "")
            {
                MessageBox.Show("����������!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtName.Focus();
                return;
            }
            DataTable printdt;
            long ret = this.m_mthGetPatientInfo("b.NAME_VCHR", this.m_objViewer.txtName.Text.Trim(), out printdt);
        }
        #endregion
        #region ��ȡ������Ϣ
        private long m_mthGetPatientInfo(string strType, string strValue, out DataTable printdt)
        {
            printdt = null;
            return objSvc.m_mthGetPatientInfo(strType, strValue, out printdt);

        }
        #endregion
        #region ��ȡ�÷�
        public void m_mthFindUsage()
        {
            DataTable printdt;
            long ret = objSvc.m_mthFindUsage(this.m_objViewer.cmbCat.SelectedIndex + 1, out printdt);
            //			this.m_objViewer.listView_Usage.Items.Clear();
            if (ret > 0 && printdt.Rows.Count > 0)
            {
                ListViewItem lv;
                for (int i = 0; i < printdt.Rows.Count; i++)
                {
                    lv = new ListViewItem(printdt.Rows[i]["usageid_chr"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["usagename_vchr"].ToString().Trim());
                    //					this.m_objViewer.listView_Usage.Items.Add(lv);
                }
                //				this.m_objViewer.listView_Usage.Items[0].Selected=true;
                //				this.m_objViewer.listView_Usage.Tag =printdt.Rows[0]["usageid_chr"].ToString();
            }
            else
            {
                //			this.m_objViewer.listView_Usage.Tag=null;
            }
        }
        #endregion
        #region ���ݲ���ID,��ʾ��Ŀ
        /// <summary>
        /// ���ݲ���ID,��ʾ��Ŀ
        /// </summary>
        public void m_mthGetItemInfo(DataTable printdt)
        {
            this.m_objViewer.listView1.Items.Clear();
            if (printdt.Rows.Count > 0)
            {
                ListViewItem lv;
                for (int i = 0; i < printdt.Rows.Count; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(printdt.Rows[i]["ROWNO_CHR"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["itemspec_vchr"].ToString().Trim());
                    //if(this.m_objViewer.cmbCat.SelectedIndex==3)
                    //{
                    //double tolQty=double.Parse(printdt.Rows[i]["QTY_DEC"].ToString())*double.Parse(printdt.Rows[i]["DOSAGE_DEC"].ToString());
                    //lv.SubItems.Add(tolQty.ToString().Trim());
                    //}
                    //else
                    //{
                    lv.SubItems.Add(printdt.Rows[i]["QTY_DEC"].ToString().Trim() + printdt.Rows[i]["UNITID_CHR"].ToString().Trim());
                    //}
                    //lv.SubItems.Add(printdt.Rows[i]["UNITID_CHR"].ToString().Trim());
                    //new add
                    lv.SubItems.Add(printdt.Rows[i]["freqname_chr"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["usagename_vchr"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["ITEMID_CHR"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["days_int"].ToString().Trim() + "��");
                    lv.SubItems.Add(printdt.Rows[i]["tolqty_dec"].ToString().Trim() + printdt.Rows[i]["itemunit"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["itemusagedetail_vchr"].ToString().Trim());
                    lv.SubItems.Add(printdt.Rows[i]["diag_vchr"].ToString().Trim());
                    lv.Tag = printdt.Rows[i];
                    this.m_objViewer.m_txtDoc.Text = printdt.Rows[i]["LASTNAME_VCHR"].ToString().Trim();

                    this.m_objViewer.listView1.Items.Add(lv);
                }
            }

        }
        #endregion

        #region ���������÷����˴�ӡ���ݱ�
        /// <summary>
        /// ���������÷����˴�ӡ���ݱ�
        /// </summary>
        public void m_mthFilterTableCanPrint(DataTable p_dt, string p_strtypeid, string p_strorderid)
        {
            DataTable dtResult = new DataTable();
            long lngRes = 0;
            string strusageid = "";
            System.Collections.ArrayList AList = new ArrayList();
            lngRes = this.objSvc.m_lngGetData(p_strtypeid, p_strorderid, out dtResult);
            //string strSql="";
            //strSql = "select USAGEID_CHR from t_opr_setusage where TYPE_INT="+p_strtypeid+" and ORDERID_VCHR='"+p_strorderid.Trim()+"'";
            //clsChargeItemSvc svc = new clsChargeItemSvc();
            //lngRes = svc.m_lngGetData(this.objPrincipal,strSql,out dtResult);
            if (lngRes > 0)
            {
                for (int i = 0; i < p_dt.Rows.Count; i++)
                {
                    try
                    {
                        strusageid = p_dt.Rows[i]["usageid"].ToString().Trim();
                        DataRow[] objRow = dtResult.Select("USAGEID_CHR =" + strusageid);
                        if (objRow.Length == 0)
                        {
                            p_dt.Rows[i].Delete();
                            p_dt.AcceptChanges();
                            i--;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion

        #region ��ȡ��ʿ����վ���ಡ�˼��շ���Ŀ��Ϣ
        public void m_mthGetPatinetInfo(DateTime begiontime, DateTime endtime)
        {

            int index = this.m_objViewer.cmbCat.SelectedIndex + 1;
            string strDepArr = "";
            clsDepartmentVO[] depArr = null;
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out depArr);
            for (int j = 0; j < depArr.Length; j++)
            {
                strDepArr += depArr[j].strDeptID.Trim() + ",";
            }
            string strDepID = "";
            if (this.m_objViewer.txtEmp.txtValuse.Trim() != "")
            {
                if (this.m_objViewer.txtEmp.Tag == null)
                {
                    MessageBox.Show("����б���ѡ����ȷ�Ŀ���");
                    return;
                }
                else
                {
                    strDepID = this.m_objViewer.txtEmp.Tag.ToString().Trim();
                }
            }
            long ret = objSvc.m_mthGetAllData(index.ToString(), begiontime, endtime, out dtPatient, this.m_objComInfo.m_lonGetModuleInfo("0041"), this.m_objViewer.m_txtCarNo.Text.Trim(), this.m_objViewer.m_patentName.Text.Trim(), strDepID, this.m_objViewer.m_strUseMode);
            this.m_objViewer.listView1.Items.Clear();
            this.m_objViewer.listView_Patient.Items.Clear();
            m_mthClearTxt();
            if (ret >= 0 && dtPatient.Rows.Count > 0)
            {
                ListViewItem lv;
                string m_strHasOrNot = "0";
                long lngRes = -1;
                for (int i = 0; i < dtPatient.Rows.Count; i++)
                {
                    if (strDepArr.IndexOf(dtPatient.Rows[i]["deptid_chr"].ToString().Trim()) >= 0)
                    {
                        if (dtPatient.Rows[i]["PSTAUTS_INT"].ToString().Trim() == "-2")
                        {
                            if (this.m_objViewer.m_chkTui.Checked == false)
                                continue;
                        }
                        if (dtPatient.Rows[i]["printed_int"].ToString().Trim() == "1")
                        {
                            if (this.m_objViewer.m_chkPrint.Checked == false)
                                continue;
                        }
                        lv = new ListViewItem(dtPatient.Rows[i]["patientcardid_chr"].ToString().Trim());
                        lv.SubItems.Add(dtPatient.Rows[i]["NAME_VCHR"].ToString().Trim());
                        lv.SubItems.Add(dtPatient.Rows[i]["RECORDDATE_DAT"].ToString().Trim());
                        lv.Tag = dtPatient.Rows[i];
                        this.m_objViewer.listView_Patient.Items.Add(lv);
                        if (dtPatient.Rows[i]["PSTAUTS_INT"].ToString().Trim() != "2")
                        {
                            if (dtPatient.Rows[i]["PSTAUTS_INT"].ToString().Trim() == "-2")
                            {
                                this.m_objViewer.listView_Patient.Items[this.m_objViewer.listView_Patient.Items.Count - 1].ForeColor = Color.Red;
                            }
                            else
                            {
                                this.m_objViewer.listView_Patient.Items[this.m_objViewer.listView_Patient.Items.Count - 1].ForeColor = Color.CadetBlue;
                            }
                        }
                        if (!this.m_objViewer.m_strHospitalTitle.Contains("��ɽ"))
                        {
                            lngRes = objSvc.m_lngGetSignInfoByRecipeID(dtPatient.Rows[i]["outpatrecipeid_chr"].ToString().Trim(), out m_strHasOrNot);
                            if (lngRes > 0 && m_strHasOrNot == "1")
                            {
                                this.m_objViewer.listView_Patient.Items[this.m_objViewer.listView_Patient.Items.Count - 1].ForeColor = Color.Black;
                            }
                        }


                    }
                }
                this.m_objViewer.m_lblRecordCount.Text = "������:(" + this.m_objViewer.listView_Patient.Items.Count.ToString() + ")λ";
            }
            else
                this.m_objViewer.m_lblRecordCount.Text = "������:(0)λ";

        }
        #endregion
        #region ����ı���
        /// <summary>
        /// ����ı���
        /// </summary>
        public void m_mthClearTxt()
        {
            this.m_objViewer.txtCardID.Text = "";
            this.m_objViewer.txtName.Text = "";
            this.m_objViewer.txtSex.Text = "";
            this.m_objViewer.txtAge.Text = "";
            this.m_objViewer.m_txtDoc.Text = "";

            this.m_objViewer.txtDeptName.Text = "";
            this.m_objViewer.textBox2.Text = "";
        }

        #endregion
        #region ͨ�����Ż������ƴ��б��в��Ҳ�����Ϣ
        /// <summary>
        /// ͨ�����Ż������ƴ��б��в��Ҳ�����Ϣ
        /// </summary>
        /// <param name="IntCount"></param>
        /// <param name="FindText"></param>
        public void m_mthFindFromList(int IntCount, string FindText)
        {
            if (this.m_objViewer.listView_Patient.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.listView_Patient.Items.Count; i1++)
                {
                    if (this.m_objViewer.listView_Patient.Items[i1].SubItems[IntCount].Text == FindText)
                    {
                        this.m_objViewer.listView_Patient.Items[i1].Checked = true;
                        this.m_objViewer.listView_Patient.Items[i1].Selected = true;

                        break;
                    }
                }
            }
        }
        #endregion
        #region ��ʾ���˶�Ӧ����Ŀ��Ϣ
        /// <summary>
        /// ��ʾ���˶�Ӧ����Ŀ��Ϣ
        /// </summary>
        /// <param name="index"></param>
        public void m_mthFindData(ListViewItem lsvi)
        {
            if (this.m_objViewer.listView_Patient.SelectedItems.Count > 0)
            {
                int index = this.m_objViewer.cmbCat.SelectedIndex + 1;
                DataRow seleRow = (DataRow)lsvi.Tag;
                this.m_objViewer.m_lblOUTPATRECIPEID_CHR.Text = seleRow["OUTPATRECIPEID_CHR"].ToString();
                long ret = objSvc.m_mthGetInputWet(seleRow["OUTPATRECIPEID_CHR"].ToString(), index.ToString(), out printdt);
                m_mthFilterTableCanPrint(printdt, index.ToString(), m_strGetOrderIdFromForm());
                m_mthGetItemInfo(printdt);
            }
        }
        #endregion



        private string m_strGetOrderIdFromForm()
        {
            int id = 0;
            // -1:����: 0-ע�䵥 1-��ҺѲ�ӵ� 2-��ƿ�� 3-���Ƶ� 4-������ 5-��Ѫ�� 6-��ҩ 7-��ҩ
            if (this.m_objViewer.m_rdbinject.Checked)
                id = 0;
            if (this.m_objViewer.m_rdbscout.Checked)
                id = 1;
            if (this.m_objViewer.m_rdbbottle.Checked)
                id = 2;
            if (this.m_objViewer.m_rdbcure.Checked)
                id = 3;
            if (this.m_objViewer.m_rdboperation.Checked)
                id = 4;
            if (this.m_objViewer.m_rdbblood.Checked)
                id = 5;

            return id.ToString();
        }
        #region ȡ����������Ϣ
        /// <summary>
        /// ȡ����������Ϣ
        /// </summary>
        /// <param name="index"></param>
        public void m_mthGetAllergic()
        {
            if (this.m_objViewer.listView_Patient.SelectedItems.Count > 0)
            {
                DataRow seleRow = (DataRow)this.m_objViewer.listView_Patient.SelectedItems[0].Tag;
                string strOUTPATRECIPEID_CHR = seleRow["OUTPATRECIPEID_CHR"].ToString();
                string strPATIENTID_CHR = seleRow["PATIENTID_CHR"].ToString();
                clsT_opr_allergic objRecord = null;
                int intCount = 0;
                long lngres = this.objSvc.m_lngGetAllergicByPidOutPid(strPATIENTID_CHR, strOUTPATRECIPEID_CHR, out objRecord, out intCount);
                if (lngres > 0)
                {
                    if (intCount == 0)//�������޼�¼,�ӻ������ù�����Ϣ
                    {
                        string p_strIFALLERGIC = "";
                        string p_strALLERGICDESC = "";
                        lngres = this.objSvc.m_lngGetAllergicByPidFromTBSEPatient(strPATIENTID_CHR, out p_strIFALLERGIC, out p_strALLERGICDESC);
                        if (lngres > 0)
                        {
                            m_mthShowfrmAllergichint(p_strALLERGICDESC);
                        }
                    }
                    else
                    {
                        if (objRecord.m_strALLERGICMED_VCHR == null)
                            objRecord.m_strALLERGICMED_VCHR = "";
                        m_mthShowfrmAllergichint(objRecord.m_strALLERGICMED_VCHR);
                    }
                }
            }
        }
        #endregion
        #region ������Ϣ����
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="index"></param>
        public void m_mthAllergicManage(ListView p_listView_Patient)
        {

            if (this.m_objViewer.listView_Patient.SelectedItems.Count > 0)
            {
                DataRow seleRow = (DataRow)this.m_objViewer.listView_Patient.SelectedItems[0].Tag;
                string strOUTPATRECIPEID_CHR = seleRow["OUTPATRECIPEID_CHR"].ToString();
                string strPATIENTID_CHR = seleRow["PATIENTID_CHR"].ToString();
                clsT_opr_allergic objRecord = null;
                int intCount = 0;
                long lngres = this.objSvc.m_lngGetAllergicByPidOutPid(strPATIENTID_CHR, strOUTPATRECIPEID_CHR, out objRecord, out intCount);
                if (lngres > 0)
                {
                    frmAllergichintManage frmAllergic = null;
                    if (intCount == 0)//�������޼�¼,�ӻ������ù�����Ϣ
                    {
                        string p_strIFALLERGIC = "";
                        string p_strALLERGICDESC = "";
                        lngres = this.objSvc.m_lngGetAllergicByPidFromTBSEPatient(strPATIENTID_CHR, out p_strIFALLERGIC, out p_strALLERGICDESC);
                        if (lngres > 0)
                        {
                            if (objRecord == null)
                                objRecord = new clsT_opr_allergic();

                            objRecord.m_intSTATUS_INT = 0;
                            objRecord.m_strPATIENTID_CHR = strPATIENTID_CHR;
                            objRecord.m_strOUTPATRECIPEID_CHR = strOUTPATRECIPEID_CHR;
                            objRecord.m_strALLERGICMED_VCHR = p_strALLERGICDESC;
                            objRecord.m_strALLERGICDESC_VCHR = "";
                            frmAllergic = new frmAllergichintManage(objRecord, intCount);
                            frmAllergic.m_listView_Patient = p_listView_Patient;
                            frmAllergic.ShowDialog();
                        }
                    }
                    else
                    {
                        frmAllergic = new frmAllergichintManage(objRecord, intCount);
                        frmAllergic.m_listView_Patient = p_listView_Patient;
                        frmAllergic.ShowDialog();
                    }
                }
            }
        }
        #endregion
        #region ��ʾ����
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_strALLERGICDESC"></param>
        public void m_mthShowfrmAllergichint(string p_strALLERGICDESC)
        {
            if (p_strALLERGICDESC == null)
                p_strALLERGICDESC = "";
            if (p_strALLERGICDESC != "")
            {
                if (this.m_objViewer.m_frmAllergichintShow == null)
                {
                    this.m_objViewer.m_frmAllergichintShow = new frmAllergichint();
                }
                this.m_objViewer.m_frmAllergichintShow.DesktopLocation = new Point(570, 600);
                this.m_objViewer.m_frmAllergichintShow.CONTENTTEXT = p_strALLERGICDESC;
                this.m_objViewer.m_frmAllergichintShow.Visible = true;

            }
            else
            {
                if (this.m_objViewer.m_frmAllergichintShow != null)
                {
                    this.m_objViewer.m_frmAllergichintShow.Visible = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        public DataRow[] m_GetPatrecipedeid()
        {
            if (printdt != null)
            {
                //				DataRow[] objRow=printdt.Compute("DISTINCT (outpatrecipedeid_chr)");
                //				return objRow;
            }
            return null;
        }
        #region ��ӡ����(��ƿ��)
        public void m_mthPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            printShow.DrawObject = e;
            printShow.m_mthBegionPrint("2");
        }
        #endregion

        #region ��ӡ(ע�䵥)
        public void m_mthPrint()
        {
            if (this.m_objViewer.txtCardID.Tag == null)
            {
                return;
            }
            clsCustom_SubmitValue[] objCSVOArr = null;
            //clsCustomFormServ obj = new clsCustomFormServ();
            clsDepartmentVO[] dpvo = new clsDepartmentVO[0];
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out dpvo);
            string[] strDpArr = new string[1];
            if (dpvo != null)
            {
                strDpArr = new string[dpvo.Length];
                for (int ii = 0; ii < dpvo.Length; ii++)
                {
                    strDpArr[ii] = dpvo[ii].strDeptID;
                }
            }
            else
            {
                strDpArr[0] = "";

            }
            long l = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetSubmitForms(this.m_objViewer.LoginInfo.m_strEmpID, strDpArr, out objCSVOArr);
            if (objCSVOArr != null && objCSVOArr.Length > 0)
            {
                for (int i = 0; i < objCSVOArr.Length; i++)
                {
                    if (objCSVOArr[i].m_strFormName == "ע�����Ƶ�")
                    {
                        frmCustomFormBase objfrm = new frmCustomFormBase(this.m_objViewer.txtCardID.Tag.ToString(), this.m_objViewer.txtCardID.Text, objCSVOArr[i], this.m_objViewer);
                        objfrm.ShowDialog();
                        break;
                    }

                }
            }
        }
        #endregion

        string mthGetRecedeId()
        {
            string str = "";
            if (this.m_objViewer.listView_Patient.Items.Count > 0)
            {
                str = this.m_objViewer.m_lblOUTPATRECIPEID_CHR.Text;
            }
            return str;
        }
        #region ��ȡ����
        clsFoShan2RecipeB printShow = new clsFoShan2RecipeB();
        public void m_mthGetData(bool IsPriview)
        {
            string OUTPATRECIPEID_CHR = mthGetRecedeId();
            if (OUTPATRECIPEID_CHR == "")
                return;
            clsRecipeType_VO[] PrintVO = new clsRecipeType_VO[printdt.Rows.Count];
            clsOutpatientPrintRecipe_VO printTit = new clsOutpatientPrintRecipe_VO();
            printTit.m_strPatientName = this.m_objViewer.txtName.Text;
            printTit.m_strSex = this.m_objViewer.txtSex.Text;
            printTit.m_strAge = this.m_objViewer.txtAge.Text;
            printTit.m_strCardID = this.m_objViewer.txtCardID.Text;
            printTit.strbedNO = "";
            printTit.m_strPrintDate = DateTime.Now.ToShortDateString();
            printTit.objinjectArr3 = new List<clsOutpatientPrintRecipeDetail_VO>();

            if (printdt.Rows.Count > 0)
            {
                string str = "";
                for (int i1 = 0; i1 < printdt.Rows.Count; i1++)
                {
                    if (printdt.Rows[i1]["MEDICINEID_CHR"].ToString() != "")
                    {
                        clsOutpatientPrintRecipeDetail_VO PrintDe = new clsOutpatientPrintRecipeDetail_VO();

                        PrintDe.m_strDosage = printdt.Rows[i1]["QTY_DEC"].ToString();

                        PrintDe.m_strDosageUnit = printdt.Rows[i1]["DOSAGEUNIT_CHR"].ToString();
                        PrintDe.m_strDays = printdt.Rows[i1]["DAYS_INT"].ToString();
                        PrintDe.m_strFrequency = printdt.Rows[i1]["FREQNAME_CHR"].ToString().Trim();
                        PrintDe.m_strRowNo = printdt.Rows[i1]["ROWNO_CHR"].ToString();
                        PrintDe.m_strUsage = printdt.Rows[i1]["USAGENAME_VCHR"].ToString().Trim();

                        str = printdt.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim();
                        if (str != "")
                        {
                            PrintDe.m_strUsageDetail = "����ϸ�÷���:" + str;
                        }
                        else
                            PrintDe.m_strUsageDetail = "";
                        PrintDe.m_strChargeName = printdt.Rows[i1]["itemname_vchr"].ToString();

                        DataTable dt = new DataTable();
                        clst_opr_nurseexecute cls = new clst_opr_nurseexecute();
                        cls.m_intBUSINESS_INT = 2;
                        cls.m_strOUTPATRECIPEID_CHR = OUTPATRECIPEID_CHR;
                        cls.m_strROWNO_CHR = PrintDe.m_strRowNo.Trim() == "" ? "-1" : PrintDe.m_strRowNo.Trim();
                        this.objSvc.m_lngQueryOPERATORID_CHRAndNameByType(cls, out dt);
                        if (dt.Rows.Count > 0)
                            PrintDe.m_strSinature = dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                        else
                            PrintDe.m_strSinature = "";

                        printTit.objinjectArr3.Add(PrintDe);
                    }
                }
            }
            if (printTit.objinjectArr3.Count > 0)
            {
                Sybase.DataWindow.DataStore m_objDS = new Sybase.DataWindow.DataStore();
                m_objDS.LibraryList = Application.StartupPath + "\\pb_op.pbl";
                m_objDS.DataWindowObject = "d_optiepenbill";
                string m_strTitle = this.m_objComInfo.m_strGetHospitalTitle() + "��ƿ��";
                m_objDS.Modify("t_title.text='" + m_strTitle + "'");
                m_objDS.Modify("t_name.text='" + printTit.m_strPatientName + "'");
                m_objDS.Modify("t_sex.text='" + printTit.m_strSex + "'");
                m_objDS.Modify("t_age.text='" + printTit.m_strAge + "'");

                char[] chrZero = { '0' };
                string strCardID = printTit.m_strCardID.TrimStart(chrZero);
                m_objDS.Modify("t_card.text='" + strCardID + "'");

                //clsOutpatientPrintRecipeDetail_VO objTemp = printTit.objinjectArr3[0] as clsOutpatientPrintRecipeDetail_VO;
                //string m_strTemp = objTemp.m_strUsage + "," + objTemp.m_strFrequency + "," + objTemp.m_strDays + "��";
                //m_objDS.Modify("t_usage.text='" + m_strTemp + "'");
                for (int i = 0; i < printTit.objinjectArr3.Count; i++)
                {
                    clsOutpatientPrintRecipeDetail_VO objTemp1 = printTit.objinjectArr3[i] as clsOutpatientPrintRecipeDetail_VO;
                    int row = m_objDS.InsertRow(0);
                    m_objDS.SetItemString(row, "usage", objTemp1.m_strUsage + "," + objTemp1.m_strFrequency + "," + objTemp1.m_strDays + "��");
                    m_objDS.SetItemString(row, "fh", objTemp1.m_strRowNo.Trim());
                    m_objDS.SetItemString(row, "ypmc", objTemp1.m_strChargeName);
                    m_objDS.SetItemString(row, "yl", objTemp1.m_strDosage + objTemp1.m_strDosageUnit);
                }
                //m_objDS.CalculateGroups();

                this.m_objViewer.printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = m_GetPrintName();

                m_objDS.PrintProperties.PrinterName = this.m_objViewer.printDocument1.DefaultPageSettings.PrinterSettings.PrinterName.ToString();

                if (IsPriview)
                {
                    clsPublic.PrintDialog(m_objDS);
                }
                else
                {
                    m_objDS.CalculateGroups();
                    m_objDS.Print(true);
                }
            }
            else
            {
                return;
            }
            //printShow.PrintRecipeVOInfo=printTit;
            //System.Drawing.Printing.PaperSize ps=new System.Drawing.Printing.PaperSize("12",413,1100);
            //this.m_objViewer.printDocument1.DefaultPageSettings.PaperSize=ps;
        }
        #endregion
        #region ��ȡ��ӡ��
        /// <summary>
        /// ���ش�ӡ������
        /// </summary>
        /// <returns></returns>
        public string m_GetPrintName()
        {
            string strPrintName = "";
            string patXML = "LoginFile.xml";
            if (File.Exists(patXML))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(patXML);
                XmlNode xn = doc.DocumentElement.SelectNodes("//Printer")[0];
                XmlNode xnCurr = xn.SelectSingleNode("//tiepenPatientPrinter");
                if (xnCurr != null)
                {
                    strPrintName = xnCurr.Attributes["PrinterName"].Value.ToString();
                }
            }
            return strPrintName;
        }
        #endregion
        #region ��ӡ����
        frmShowPrintNew ShowPrint = null;
        public void m_mthPrintData(int intType)
        {
            if (printdt != null && printdt.Rows.Count > 0 && this.m_objViewer.listView1.Items.Count > 0)
            {
                ShowPrint = new frmShowPrintNew(intType, printdt, m_getDataRow(), this.m_objComInfo.m_strGetHospitalTitle());
                ShowPrint.OUTPATRECIPEID_CHR = mthGetRecedeId();
                ShowPrint.m_strDoctor = this.m_objViewer.m_txtDoc.Text.Trim();

                ShowPrint.Show();
            }
            else
            {
                MessageBox.Show("û�пɴ�ӡ������", "Icare");
            }
        }

        #endregion
        #region  ���ѡ��ť�Ƿ�ѡ��
        /// <summary>
        /// ���ѡ��ť�Ƿ�ѡ��
        /// </summary>
        /// <param name="p_blnChecked"></param>
        public void m_mthSetRiadioButtonCheck(bool p_blnChecked)
        {
            this.m_objViewer.m_rdbinject.Checked = p_blnChecked;
            this.m_objViewer.m_rdbscout.Checked = p_blnChecked;
            this.m_objViewer.m_rdbbottle.Checked = p_blnChecked;
            this.m_objViewer.m_rdbcure.Checked = p_blnChecked;
            this.m_objViewer.m_rdboperation.Checked = p_blnChecked;
            this.m_objViewer.m_rdbblood.Checked = p_blnChecked;
        }
        #endregion

        #region  �����ŷ���ѡ��
        /// <summary>
        /// �����ŷ���ѡ��
        /// </summary>
        /// <param name="p_Lsv">LV</param>
        /// <param name="p_index">����</param>
        /// <param name="p_blnCheck">���</param>
        public void m_mthSelectListViewCheckBox(ListView p_Lsv, int p_index, bool p_blnCheck)
        {
            //������
            string strROWNO_CHR = p_Lsv.Items[p_index].SubItems[1].Text.Trim();
            if (strROWNO_CHR != "")
            {
                for (int i = 0; i < p_Lsv.Items.Count; ++i)
                {
                    if (p_Lsv.Items[i].SubItems[1].Text.Trim() == strROWNO_CHR)
                        p_Lsv.Items[i].Checked = p_blnCheck;
                }
            }
        }
        #endregion
        public void m_mthUpdatePrintFlag(ListViewItem lsvi)
        {
            DataRow seleRow = (DataRow)lsvi.Tag;
            string m_strRecipeid = seleRow["OUTPATRECIPEID_CHR"].ToString();
            long lngRes = objSvc.m_lngUpdatePrintFlag(m_strRecipeid);
            if (lngRes > 0)
            {
                this.m_objViewer.listView_Patient.Items.Remove(lsvi);
            }
        }
        #region  ��ӡԤ��
        /// <summary>
        /// ��ӡԤ�� 
        /// </summary>
        /// <param name="IsPriview">true Ԥ�� false ֱ�Ӵ�ӡ</param>
        public void m_mthPrintPrivew(bool IsPriview, ListViewItem lsvi)
        {
            if (this.m_objViewer.m_rdbinject.Checked)
            {
                #region ע�䵥��ӡ
                if (m_objViewer.listView_Patient.Items.Count > 0 && m_objViewer.listView_Patient.SelectedItems.Count > 0)
                {
                    DataRow seleRow = (DataRow)lsvi.Tag;
                    string m_strsid = com.digitalwave.iCare.middletier.HI.clsInjectPrint.m_mthGetsid_int(seleRow["OUTPATRECIPEID_CHR"].ToString());
                    Sybase.DataWindow.DataWindowChild m_objDwc1;

                    Sybase.DataWindow.DataStore m_objDs = new Sybase.DataWindow.DataStore();
                    m_objDs.LibraryList = Application.StartupPath + "\\PB_OP.pbl";
                    m_objDs.DataWindowObject = "t_opinjectionandxunshika";
                    m_objDwc1 = m_objDs.GetChild("dw_1");
                    m_objDwc1.Modify("t_title.text='" + this.m_objComInfo.m_strGetHospitalTitle() + "'");
                    m_objDwc1.Modify("jzsj.text='" + Convert.ToDateTime(lsvi.SubItems[2].Text).ToString("yyyy-MM-dd") + "'");
                    m_objDwc1.Modify("brkh.text='" + lsvi.SubItems[0].Text + "'");
                    m_objDwc1.Modify("xm.text='" + lsvi.SubItems[1].Text + "'");
                    m_objDwc1.Modify("xb.text='" + this.m_objViewer.txtSex.Text + "'");
                    m_objDwc1.Modify("nl.text='" + this.m_objViewer.txtAge.Text + "'");
                    m_objDwc1.Modify("zzys.text='" + this.m_objViewer.m_txtDoc.Text + "'");
                    m_objDwc1.Modify("t_deptname.text = '" + this.m_objViewer.txtDeptName.Text + "'");
                    if (this.m_objViewer.txtDeptName.Tag != null)
                    {
                        DataRow dr = this.m_objViewer.txtDeptName.Tag as DataRow;
                        m_objDwc1.Modify("t_addr.text = '" + dr["homeaddress_vchr"].ToString() + "'");
                        //m_objDwc1.Modify("diag.text = '" + this.m_objViewer.txtDeptName.Text + "'");
                    }
                    for (int i = 0; i < this.m_objViewer.listView1.Items.Count; i++)
                    {
                        int row = m_objDwc1.InsertRow(0);
                        m_objDwc1.SetItemString(row, "fh", this.m_objViewer.listView1.Items[i].SubItems[1].Text.ToString());
                        m_objDwc1.SetItemString(row, "xmbm", this.m_objViewer.listView1.Items[i].SubItems[2].Text.ToString());
                        m_objDwc1.SetItemString(row, "gg", this.m_objViewer.listView1.Items[i].SubItems[3].Text.ToString());
                        m_objDwc1.SetItemString(row, "ul", this.m_objViewer.listView1.Items[i].SubItems[4].Text.ToString());
                        m_objDwc1.SetItemString(row, "uf", this.m_objViewer.listView1.Items[i].SubItems[6].Text.ToString());
                        m_objDwc1.SetItemString(row, "pl", this.m_objViewer.listView1.Items[i].SubItems[5].Text.ToString());
                        m_objDwc1.SetItemString(row, "ts", this.m_objViewer.listView1.Items[i].SubItems[8].Text.ToString());
                        m_objDwc1.SetItemString(row, "zsl", this.m_objViewer.listView1.Items[i].SubItems[9].Text.ToString());
                        m_objDwc1.SetItemString(row, "sm", this.m_objViewer.listView1.Items[i].SubItems[10].Text.ToString());
                    }

                    if (IsPriview)
                    {
                        clsPublic.PrintDialog(m_objDs);
                    }
                    else
                    {
                        m_objDs.Print(true);
                    }

                }
                #endregion
            }
            if (this.m_objViewer.m_rdbscout.Checked)
            {

                if (m_objViewer.listView_Patient.Items.Count > 0 && m_objViewer.listView_Patient.SelectedItems.Count > 0)
                {
                    DataRow seleRow = (DataRow)lsvi.Tag;
                    string m_strsid = com.digitalwave.iCare.middletier.HI.clsInjectPrint.m_mthGetsid_int(seleRow["OUTPATRECIPEID_CHR"].ToString());
                    Sybase.DataWindow.DataWindowChild m_objDwc2;
                    Sybase.DataWindow.DataStore m_objDs = new Sybase.DataWindow.DataStore();
                    m_objDs.LibraryList = Application.StartupPath + "\\PB_OP.pbl";
                    m_objDs.DataWindowObject = "t_opinjectionandxunshika";
                    List<List<string>> m_objList1 = new List<List<string>>();
                    List<List<List<string>>> m_objList2 = new List<List<List<string>>>();
                    List<string> m_objListGroup = new List<string>();
                    clsOutpatientPrintRecipe_VO m_objVo = new clsOutpatientPrintRecipe_VO();
                    long lngRes = -1;
                    m_objDwc2 = m_objDs.GetChild("dw_2");
                    //clsDomainControlMedStore m_objDomain = new clsDomainControlMedStore();
                    lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetPrintData(m_strsid, out m_objList1, out m_objList2, out m_objListGroup, out m_objVo);
                    if (lngRes > 0)
                    {
                        m_objDwc2.Modify("t_title.text='" + this.m_objComInfo.m_strGetHospitalTitle() + "'");
                        m_objDwc2.Modify("jzsj.text='" + m_objVo.m_strPrintDate + "'");
                        m_objDwc2.Modify("xm.text='" + m_objVo.m_strPatientName + "'");
                        m_objDwc2.Modify("xb.text='" + m_objVo.m_strSex + "'");
                        m_objDwc2.Modify("nl.text='" + m_objVo.m_strAge + "'");
                        m_objDwc2.Modify("zzys.text='" + m_objVo.m_strDiagDrName + "'");

                        if (this.m_objViewer.m_strXskStyle.Trim() == "0")
                        {
                            for (int j = 0; j < m_objList2.Count; j++)
                            {
                                int row1 = m_objDwc2.InsertRow(0);
                                m_objDwc2.SetItemString(row1, "xmmc", m_objListGroup[j].ToString());
                            }
                        }
                        else
                        {
                            m_objDwc2.Modify("t_kh.text='" + m_objVo.m_strCardID + "'");
                            m_objDwc2.Modify("t_zd.text='" + m_objVo.m_strdiagnose + "'");
                            for (int j = 0; j < this.m_objViewer.listView1.Items.Count; j++)
                            {

                                int row1 = m_objDwc2.InsertRow(0);
                                if (this.m_objViewer.listView1.Items[j].SubItems[1].Text.Trim() == string.Empty)
                                {
                                    m_objDwc2.SetItemString(row1, "xmmc", this.m_objViewer.listView1.Items[j].SubItems[2].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "pl", this.m_objViewer.listView1.Items[j].SubItems[5].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "yl", this.m_objViewer.listView1.Items[j].SubItems[4].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "ts", this.m_objViewer.listView1.Items[j].SubItems[8].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "yf", this.m_objViewer.listView1.Items[j].SubItems[6].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "sm", this.m_objViewer.listView1.Items[j].SubItems[10].Text.ToString());

                                }
                                else
                                {
                                    m_objDwc2.SetItemString(row1, "xmmc", this.m_objViewer.listView1.Items[j].SubItems[1].Text + ":" + this.m_objViewer.listView1.Items[j].SubItems[2].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "pl", this.m_objViewer.listView1.Items[j].SubItems[5].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "yl", this.m_objViewer.listView1.Items[j].SubItems[4].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "ts", this.m_objViewer.listView1.Items[j].SubItems[8].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "yf", this.m_objViewer.listView1.Items[j].SubItems[6].Text.ToString());
                                    m_objDwc2.SetItemString(row1, "sm", this.m_objViewer.listView1.Items[j].SubItems[10].Text.ToString());
                                }
                            }
                        }

                        if (IsPriview)
                        {
                            clsPublic.PrintDialog(m_objDs);
                        }
                        else
                        {
                            m_objDs.Print(true);
                        }

                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (this.m_objViewer.m_rdbbottle.Checked)
            {
                #region ��ƿ����ӡ
                if (this.m_objViewer.listView_Patient.SelectedItems.Count > 0)
                {
                    m_mthGetData(IsPriview);
                }
                else
                    m_mthShowWarning(this.m_objViewer.listView_Patient, "����ѡ����");

                #endregion
            }
            if (this.m_objViewer.m_rdbcure.Checked)
            {
                #region ���Ƶ���ӡ
                if (m_objViewer.cmbCat.SelectedIndex == 1)
                {
                    //m_mthPrintData(2);
                    int m_intRow = 0;
                    string str = "";
                    Sybase.DataWindow.DataStore m_objDs = new Sybase.DataWindow.DataStore();
                    m_objDs.LibraryList = Application.StartupPath + "\\PB_OP.pbl";
                    m_objDs.DataWindowObject = "d_op_treatbill";
                    m_objDs.Modify("t_hostitle.text='" + this.m_objComInfo.m_strGetHospitalTitle() + "'");
                    m_objDs.Modify("t_name.text='" + lsvi.SubItems[1].Text + "'");
                    m_objDs.Modify("t_age.text='" + this.m_objViewer.txtAge.Text + "'");
                    m_objDs.Modify("t_cardno.text='" + lsvi.SubItems[0].Text + "'");
                    m_objDs.Modify("t_sex.text='" + this.m_objViewer.txtSex.Text + "'");
                    m_objDs.Modify("t_doctor.text='" + this.m_objViewer.m_txtDoc.Text + "'");


                    for (int i1 = 0; i1 < this.printdt.Rows.Count; i1++)
                    {
                        if (printdt.Rows[i1]["MEDICINETYPEID_CHR"].ToString() != "5")
                        {
                            str = printdt.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim();
                            if (str != "")
                            {
                                str = "\n����ϸ�÷���:" + str;
                            }
                            m_intRow = m_objDs.InsertRow(0);
                            m_objDs.SetItemString(m_intRow, "xmmc", printdt.Rows[i1]["itemname_vchr"].ToString() + str);
                            m_objDs.SetItemString(m_intRow, "gg", printdt.Rows[i1]["ITEMSPEC_VCHR"].ToString());
                            m_objDs.SetItemString(m_intRow, "sl", printdt.Rows[i1]["QTY_DEC"].ToString() + printdt.Rows[i1]["UNITID_CHR"].ToString());
                        }
                    }
                    if (IsPriview)
                    {
                        if (m_objDs.RowCount > 0)
                        {
                            clsPublic.PrintDialog(m_objDs);
                        }
                    }
                    else
                    {
                        if (m_objDs.RowCount > 0)
                        {
                            m_objDs.Print(true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("����ѡ����Ӧ�ķ���!", "Icare");
                }
                #endregion
            }
            if (this.m_objViewer.m_rdboperation.Checked)
            {
                #region ��������ӡ
                if (m_objViewer.cmbCat.SelectedIndex == 2)
                {
                    m_mthPrintData(3);
                }
                else
                {
                    MessageBox.Show("����ѡ����Ӧ�ķ���!", "Icare");
                }
                #endregion
            }
            if (this.m_objViewer.m_rdbblood.Checked)
            {
                #region ��Ѫ��ӡ
                if (m_objViewer.cmbCat.SelectedIndex == 3)
                {
                    m_mthPrintData(4);
                }
                else
                {
                    MessageBox.Show("����ѡ����Ӧ�ķ���!", "Icare");
                }
                #endregion
            }

        }
        #endregion
        private string XMLFile = System.Windows.Forms.Application.StartupPath + "\\" + "LoginFile.xml";
        #region LoginFile.XML��д����
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        public string m_strReadXML(string parentnode, string childnode, string key)
        {
            string strRet = "";

            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }
        /// <summary>
        /// д����
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public bool m_blnWriteXML(string parentnode, string childnode, string key, string val, string XMLFile)
        {
            bool blnRet = false;
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null && xndC != null)
                    {
                        xndC.Attributes["value"].Value = val;
                        xdoc.Save(XMLFile);
                        blnRet = true;
                    }
                }
            }
            catch
            {
                blnRet = false;
            }
            return blnRet;
        }
        #endregion
        #region  0-ע�䵥 1-��ҺѲ�ӵ� 2-��ƿ�� 3-���Ƶ� 4-������ 5-��Ѫ�� 6-��ҩ 7-��ҩ
        /// <summary>
        /// -1:����: 0-ע�䵥 1-��ҺѲ�ӵ� 2-��ƿ�� 3-���Ƶ� 4-������ 5-��Ѫ�� 6-��ҩ 7-��ҩ
        /// </summary>
        public int m_intGetType()
        {
            int intType = -1;
            if (this.m_objViewer.m_rdbinject.Checked)
            {
                intType = 0;
            }
            if (this.m_objViewer.m_rdbscout.Checked)
            {
                intType = 1;
            }
            if (this.m_objViewer.m_rdbbottle.Checked)
            {
                intType = 2;
            }
            if (this.m_objViewer.m_rdbcure.Checked)
            {
                intType = 3;
            }
            if (this.m_objViewer.m_rdboperation.Checked)
            {
                intType = 4;
            }
            if (this.m_objViewer.m_rdbblood.Checked)
            {
                intType = 5;
            }
            return intType;
        }
        #endregion

        #region  ����ǩ��
        /// <summary>
        /// ����ǩ��
        /// </summary>
        public void m_mthAlterName(ListView p_Lsv)
        {
            if (p_Lsv.SelectedItems.Count > 0)
            {
                DataRow dr = (DataRow)p_Lsv.SelectedItems[0].Tag;
                if (dr != null)
                {
                    clst_opr_nurseexecute clsData = new clst_opr_nurseexecute();
                    int intID = Convert.ToInt32(dr["SEQ_INT"].ToString().Trim());
                    long lng = this.objSvc.m_lngQueryByID(out clsData, intID);
                    if (lng > 0)
                    {

                        frmSinature frm = new frmSinature(clsData.m_intBUSINESS_INT);
                        frm.m_Type = p_Lsv.SelectedItems[0].SubItems[1].Text.Trim();
                        frm.m_txtDripping.Text = clsData.m_strREMARK1_VCHR.Trim();
                        if (frm.m_Type == "ִ����")
                        {
                            frm.m_txtDripping.Enabled = true;
                            frm.m_txtDripping.Focus();
                        }
                        else
                        {
                            frm.txtID.Focus();
                        }
                        for (int i = 0; i < frm.m_cboType.Items.Count; i++)
                        {
                            if (frm.m_cboType.Items[i].ToString() == frm.m_Type)
                                frm.m_cboType.SelectedIndex = i;
                        }
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            clsData.m_strEXECTIME_DAT = frm.m_dtmTime.Value.ToString();
                            clsData.m_strREMARK1_VCHR = frm.m_txtDripping.Text.Trim();
                            clsData.m_intOPERATORTYPE_INT = m_intGetDataBaseIndexFromText(frm.m_cboType.Text);//����������
                            clsData.m_strOPERATORID_CHR = frm.txtID.Tag.ToString().Trim().PadLeft(7, '0');
                            long lngres = this.objSvc.m_lngUpdateMoreT_opr_nurseexecute(intID, clsData);

                            if (lngres > 0)
                            {
                                m_mthQueryOPERATORID_CHRAndName(this.m_objViewer.listView1, this.m_objViewer.m_lsvSinature, false);
                                if (this.m_objViewer.m_lsvSinature.Items.Count > 0)
                                    m_objViewer.m_lsvSinature.Items[0].Selected = true;
                                m_mthShowWarning(this.m_objViewer.m_lsvSinature, "�޸�ǩ���ɹ�");
                            }
                        }
                    }
                    else
                    {
                        m_mthShowWarning(this.m_objViewer.m_lsvSinature, "��ȡ����ʧ��");
                    }
                }
            }
            else
            {
                m_mthShowWarning(p_Lsv, "����ѡ��ĳǩ����¼");
            }
        }
        #endregion

        #region  Deleteǩ��
        /// <summary>
        /// Deleteǩ��
        /// </summary>
        public void m_mthDeleteName(ListView p_Lsv)
        {
            if (p_Lsv.SelectedItems.Count > 0)
            {
                DataRow dr = (DataRow)p_Lsv.SelectedItems[0].Tag;
                if (dr != null)
                {
                    clst_opr_nurseexecute clsData = new clst_opr_nurseexecute();
                    int intID = Convert.ToInt32(dr["SEQ_INT"].ToString().Trim());
                    long lng = this.objSvc.m_lngQueryByID(out clsData, intID);
                    if (lng > 0)
                    {
                        frmDeleteNameComfirm objDelFrm = new frmDeleteNameComfirm(clsData.m_strOPERATORID_CHR.Trim());

                        if (objDelFrm.ShowDialog() == DialogResult.OK)
                        {
                            lng = this.objSvc.m_lngUpdateStateT_opr_nurseexecute(clsData);
                            if (lng > 0)
                            {
                                m_mthShowWarning(this.m_objViewer.m_lsvSinature, "ɾ���ɹ�");
                                m_mthQueryOPERATORID_CHRAndName(this.m_objViewer.listView1, this.m_objViewer.m_lsvSinature, false);
                                if (this.m_objViewer.m_lsvSinature.Items.Count > 0)
                                {
                                    this.m_objViewer.m_lsvSinature.Items[0].Selected = true;
                                }
                            }
                            else
                            {
                                m_mthShowWarning(this.m_objViewer.m_lsvSinature, "ɾ��ʧ��");
                            }
                        }
                    }
                    else
                    {
                        m_mthShowWarning(this.m_objViewer.m_lsvSinature, "���ݿ����ʧ��");
                    }
                }
            }
            else
            {
                m_mthShowWarning(p_Lsv, "����ѡ��ĳǩ����¼");
            }
        }
        #endregion

        #region ȡ����ID
        /// <summary>
        /// ȡ����ID
        /// </summary>
        /// <returns></returns>
        private string m_strGetOUTPATRECIPEID_CHR()
        {
            string strRes = "";
            if (this.m_objViewer.listView_Patient.Items.Count > 0)
                strRes = this.m_objViewer.m_lblOUTPATRECIPEID_CHR.Text;

            return strRes;
        }
        #endregion

        #region  ǩ��
        /// <summary>
        /// ǩ��
        /// </summary>
        public void m_mthSinature(ListView p_Lsv)
        {
            int intCount = m_intListViewHasSelected(p_Lsv);
            if (intCount > 0)
            {
                int intType = m_intGetType();
                //				if(intType==2)//����Ҫ��ȫѡ.
                //				{
                //					if(p_Lsv.Items.Count !=intCount)
                //					{
                //						m_mthShowWarning(p_Lsv, "��ƿ��Ҫ��ȫѡ");
                //						return;
                //					}
                //				}
                int intSmallType = -1;
                string strDripping = null;
                string strOPId = null;
                string strTime = null;

                DataRow dr = null;

                frmSinature frmSt = new frmSinature(intType);
                if (frmSt.ShowDialog() == DialogResult.OK)
                {
                    int intIndex = 0;
                    clst_opr_nurseexecute[] clsDataArr = new clst_opr_nurseexecute[intCount];
                    clst_opr_nurseexecute clsData = null;
                    strTime = frmSt.m_dtmTime.Value.ToString();
                    strDripping = frmSt.m_txtDripping.Text.Trim();
                    intSmallType = m_intGetDataBaseIndexFromText(frmSt.m_cboType.Text);//����������
                    strOPId = frmSt.txtID.Tag.ToString().Trim();
                    for (int i = 0; i < p_Lsv.Items.Count; ++i)
                    {
                        if (p_Lsv.Items[i].Checked == true)
                        {
                            dr = (DataRow)p_Lsv.Items[i].Tag;
                            if (dr != null)
                            {
                                clsData = new clst_opr_nurseexecute();
                                clsData.m_intBUSINESS_INT = intType;
                                clsData.m_intEXECTIMES_INT = 1;//����Ϊ1
                                clsData.m_intOPERATORTYPE_INT = intSmallType;
                                clsData.m_intSEQ_INT = -1;
                                clsData.m_intSTATUS_INT = 1;
                                clsData.m_strEXECTIME_DAT = strTime;
                                clsData.m_strITEMID_CHR = dr["ITEMID_CHR"].ToString();
                                clsData.m_strOPERATORID_CHR = strOPId.PadLeft(7, '0');
                                clsData.m_strOUTPATRECIPEID_CHR = m_strGetOUTPATRECIPEID_CHR();
                                clsData.m_strREMARK1_VCHR = strDripping;//����
                                clsData.m_strREMARK2_VCHR = "";
                                clsData.m_strROWNO_CHR = dr["ROWNO_CHR"].ToString().Trim() != "" ? dr["ROWNO_CHR"].ToString().Trim() : "-1";
                                clsData.m_strTABLENAME_VCHR = dr["fromtable"].ToString();
                                clsDataArr[intIndex++] = clsData;
                            }
                        }
                    }
                    string[] strRecordIDArr = null;
                    long lngres = objSvc.m_lngAddNewToT_opr_nurseexecute(clsDataArr, out strRecordIDArr);
                    if (lngres > 0)
                    {
                        m_mthQueryOPERATORID_CHRAndName(this.m_objViewer.listView1, this.m_objViewer.m_lsvSinature, false);
                        if (this.m_objViewer.m_lsvSinature.Items.Count > 0)
                            m_objViewer.m_lsvSinature.Items[0].Selected = true;
                        m_mthShowWarning(this.m_objViewer.m_lsvSinature, "ǩ���ɹ�");
                        for (int i = 0; i < this.m_objViewer.listView_Patient.SelectedItems.Count; i++)
                        {
                            this.m_objViewer.listView_Patient.SelectedItems[i].ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        m_mthShowWarning(this.m_objViewer.m_lsvSinature, "ǩ��ʧ��");
                    }
                }
            }
            else
            {
                m_mthShowWarning(this.m_objViewer.listView1, "��ѡ����Ŀ");
            }
        }
        #endregion

        #region  �ж���ѡ�������
        /// <summary>
        /// �ж���ѡ�������
        /// </summary>
        public int m_intListViewHasSelected(ListView p_Lsv)
        {
            int blnSelectedCount = 0;
            for (int i = 0; i < p_Lsv.Items.Count; ++i)
            {
                if (p_Lsv.Items[i].Checked == true)
                {
                    ++blnSelectedCount;
                }
            }
            return blnSelectedCount;
        }
        #endregion

        #region  ��ѯǩ���б�
        /// <summary>
        /// ��ѯǩ���б�
        /// </summary>
        /// <param name="p_LsvSource">����Դ</param>
        /// <param name="p_LsvDestinct">Ŀ����ʾ��</param>
        public void m_mthQueryOPERATORID_CHRAndName(ListView p_LsvSource, ListView p_LsvDestinct, bool p_blnAll)
        {
            DataTable dt = new DataTable();
            ListViewItem lv;
            if (p_LsvSource.SelectedItems.Count > 0)
            {
                p_LsvDestinct.Items.Clear();
                DataRow dr = (DataRow)p_LsvSource.SelectedItems[0].Tag;
                if (dr != null)
                {
                    clst_opr_nurseexecute clsData = new clst_opr_nurseexecute();
                    clsData.m_intBUSINESS_INT = m_intGetType(); ;
                    clsData.m_intSTATUS_INT = 1;
                    clsData.m_strITEMID_CHR = dr["ITEMID_CHR"].ToString();
                    clsData.m_strOUTPATRECIPEID_CHR = m_strGetOUTPATRECIPEID_CHR();
                    clsData.m_strROWNO_CHR = dr["ROWNO_CHR"].ToString().Trim() != "" ? dr["ROWNO_CHR"].ToString().Trim() : "-1";

                    long lngres = this.objSvc.m_lngQueryOPERATORID_CHRAndName(clsData, out dt, p_blnAll);
                    if (lngres > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            lv = new ListViewItem(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
                            string type = m_intGetDataBaseTextFromIndex(Convert.ToInt32(dt.Rows[i]["OPERATORTYPE_INT"].ToString().Trim()));
                            lv.SubItems.Add(type);
                            lv.SubItems.Add(dt.Rows[i]["EXECTIME_DAT"].ToString().Trim());
                            lv.Tag = dt.Rows[i];
                            p_LsvDestinct.Items.Add(lv);
                        }
                    }
                }
            }

            //			if(p_blnAll)
            //			{
            //				long lngres2 = this.objSvc.m_lngQueryOPERATORID_CHRAndName(null,out dt,p_blnAll);
            //				if(lngres2 >0)
            //				{
            //					for(int i=0;i<dt.Rows.Count;++i)
            //					{
            //						lv=new ListViewItem(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
            //						string type = m_intGetDataBaseTextFromIndex(Convert.ToInt32(dt.Rows[i]["OPERATORTYPE_INT"].ToString().Trim()));
            //						lv.SubItems.Add(type);
            //						lv.SubItems.Add(dt.Rows[i]["EXECTIME_DAT"].ToString().Trim());
            //						lv.Tag = dt.Rows[i];
            //						p_LsvDestinct.Items.Add(lv);
            //					}
            //				}
            //			}
        }
        #endregion

        #region  ��ʾ��Ϣ
        /// <summary>
        /// ����,��ʾ��Ϣ
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="strWaring"></param>
        public void m_mthShowWarning(System.Windows.Forms.ListView p_lsv, string strWaring)
        {
            //float fontSize = float.Parse(Convert.ToString(14.25));
            //System.Drawing.Font ft = new Font("����",fontSize);
            //int intRowHeight = ft.Height;
            //frmShowWarning ShowWarning=new frmShowWarning();
            //ShowWarning.m_GetWaring=strWaring;
            //Point p= p_lsv.Parent.PointToScreen(p_lsv.Location);
            ////p.X+=(p_lsv.Width/2);
            //p.X+=80;

            //if(p_lsv.SelectedItems.Count>0)
            //{
            //    int index = p_lsv.SelectedItems[0].Index;
            //    int intHeightOffset = intRowHeight * index + intRowHeight/2 - intRowHeight*3;
            //    p.Y+=intHeightOffset;
            //}
            //else
            //{
            //    p.Y+=(p_lsv.Height/2);
            //}
            //ShowWarning.Location=p;
            //ShowWarning.Show();

        }
        #endregion

        #region ȡ�����Ӧ�����ͺ�
        /// <summary>
        /// ȡ�����Ӧ�����ͺ�
        /// </summary>
        /// <param name="p_strText">������</param>
        /// <returns></returns>
        private int m_intGetDataBaseIndexFromText(string p_strText)
        {
            int intIndex = -1;
            switch (p_strText.Trim())
            {
                case "��ҩ��":
                    intIndex = 1;
                    break;
                case "������":
                    intIndex = 2;
                    break;
                case "ִ����":
                    intIndex = 3;
                    break;
                case "Ѳ��":
                    intIndex = 4;
                    break;
                case "Ѳ��1":
                    intIndex = 4;
                    break;
                case "Ѳ��2":
                    intIndex = 5;
                    break;
                case "Ѳ��3":
                    intIndex = 6;
                    break;
                case "Ѳ��4":
                    intIndex = 7;
                    break;
                case "Ѳ��5":
                    intIndex = 8;
                    break;
                case "��ƿ��":
                    intIndex = 9;
                    break;
                case "��ҩ��ʿ":
                    intIndex = 10;
                    break;
                case "��Ѫ��":
                    intIndex = 11;
                    break;
                default:
                    break;
            }
            return intIndex;
        }
        /// <summary>
        /// ȡ�����Ӧ������
        /// </summary>
        /// <param name="p_strText">����</param>
        /// <returns></returns>
        private string m_intGetDataBaseTextFromIndex(int p_int)
        {
            string str = "";
            switch (p_int)
            {
                case 1:
                    str = "��ҩ��";
                    break;
                case 2:
                    str = "������";
                    break;
                case 3:
                    str = "ִ����";
                    break;
                case 4:
                    str = "Ѳ��";
                    break;
                case 5:
                    str = "Ѳ��2";
                    break;
                case 6:
                    str = "Ѳ��3";
                    break;
                case 7:
                    str = "Ѳ��4";
                    break;
                case 8:
                    str = "Ѳ��5";
                    break;
                case 9:
                    str = "��ƿ��";
                    break;
                case 10:
                    str = "��ҩ��ʿ";
                    break;

                case 11:
                    str = "��Ѫ��";
                    break;
                default:
                    break;
            }
            return str;
        }
        #endregion

        #region  ȫѡ
        /// <summary>
        /// ȫѡ
        /// </summary>
        public void m_intListViewAllSelected(ListView p_Lsv, bool p_blnSelected)
        {
            for (int i = 0; i < p_Lsv.Items.Count; ++i)
            {
                p_Lsv.Items[i].Checked = p_blnSelected;
            }
        }
        #endregion
    }
}
