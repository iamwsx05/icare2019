using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using weCare.Core.Entity;
using Microsoft.VisualBasic;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Ԥ�ɽ��߼�������
    /// </summary>
    public class clsCtl_PrePay : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_PrePay objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmPrePay m_objViewer;
        /// <summary>
        /// Ԥ������: 0 ���� 1 �ֹ�
        /// </summary>
        internal string PreType = "0";
        /// <summary>
        /// Ԥ�����վݺ�
        /// </summary>
        internal string PrepayBillNo = "";

        /// <summary>
        /// ����Ԥ���Ƿ��ӡ�վ�
        /// </summary>
        private bool NorPrint = false;
        /// <summary>
        /// �ֹ�Ԥ���Ƿ��ӡ�վ�
        /// </summary>
        private bool HandPrint = false;
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_PrePay()
        {
            objSvc = new clsDcl_PrePay();
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
            m_objViewer = (frmPrePay)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            if (PreType == "0")
            {
                this.m_objViewer.lblpretype.Text = "����Ԥ��";

                this.m_mthGetPrePayBillNo();//������󣬲����ж�
                this.m_objViewer.lblInfo.Visible = true;
            }
            else if (PreType == "1")
            {
                this.m_objViewer.lblpretype.Text = "�ֹ�Ԥ��";

                this.m_objViewer.txtprebillno.Text = "";
                this.m_objViewer.lblInfo.Visible = false;
            }

            this.m_objViewer.txtMoney.Text = "";
            this.m_objViewer.cbopaytype.Text = "";
            this.m_objViewer.cbopaytype.SelectedIndex = -1;
            this.m_objViewer.txtnote.Text = "";

            this.m_objViewer.txtprebillno.Focus();
            this.m_objViewer.txtprebillno.SelectAll();
        }
        #endregion

        #region ��ȡ��ӡ����
        /// <summary>
        /// ��ȡ��ӡ����
        /// </summary>
        public void m_mthGetPrintParm()
        {
            NorPrint = clsPublic.m_intGetSysParm("1011") == 1 ? true : false;
            HandPrint = clsPublic.m_intGetSysParm("1012") == 1 ? true : false;
        }
        #endregion

        #region ��ȡԤ�����վݺ�
        /// <summary>
        /// ��ȡԤ�����վݺ�
        /// </summary>
        /// <returns></returns>
        public void m_mthGetPrePayBillNo()
        {
            PrepayBillNo = clsPublic.m_strGetCurrInvoiceNo(this.m_objViewer.LoginInfo.m_strEmpID, 2);
            if (PrepayBillNo == "")
            {
                return;
            }

            if (clsPublic.m_blnCheckPrepayNoExpression(PrepayBillNo))
            {
                //if (!clsPublic.m_blnCheckPrepayNoIsUsed(PrepayBillNo, int.Parse(PreType)))
                if (!clsPublic.m_blnCheckPrepayNoIsUsed(PrepayBillNo, 0))
                {
                    this.m_objViewer.txtprebillno.Text = PrepayBillNo;
                }
                else
                {
                    MessageBox.Show("��ǰԤ�����վݵı���ѱ�ʹ�ã��������º�(�뵱ǰ��ӡƱ�ݺ���ͬ)��", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("��ǰԤ�����վݵı�Ź�����ȷ������ϸ��顣", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ����Ԥ�����վݺ�
        /// <summary>
        /// ����Ԥ�����վݺ�
        /// </summary>
        /// <param name="CurrNo"></param>
        /// <returns></returns>
        public bool m_blnSavePrePayBillNo(string CurrNo)
        {
            return clsPublic.m_blnWriteXML("BeInHospital", "CurrPrepayBillNo", "AnyOne", CurrNo);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthFind()
        {
            frmCommonFind f = new frmCommonFind("������Ժ��������", this.m_objViewer.ucPatientInfo.Status);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                this.m_objViewer.ucPatientInfo.m_mthFind(f.RegisterID, 3);
                if (this.m_objViewer.ucPatientInfo.IsChanged)
                {
                    this.m_mthGetPrePayHistoryInfo();
                }
            }
            else
            {
                if (this.m_objViewer.InitFlag)
                {
                    this.m_objViewer.DirClose = true;
                }
                else
                {
                    if (this.m_objViewer != null)
                    {
                        this.m_objViewer.Close();
                    }
                }
            }
        }
        #endregion

        #region ��ʾ
        /// <summary>
        /// ��ʾ
        /// </summary>
        /// <param name="zyh"></param>
        public void m_mthShow(string zyh)
        {
            this.m_objViewer.ucPatientInfo.m_mthFind(zyh, 2);
            if (this.m_objViewer.ucPatientInfo.IsChanged)
            {
                this.m_mthGetPrePayHistoryInfo();
                this.m_mthInit();
            }
        }
        #endregion

        #region �л�Ԥ������
        /// <summary>
        /// �л�Ԥ������
        /// </summary>
        public void m_mthSwitch()
        {
            if (MessageBox.Show("��ǰΪ��" + this.m_objViewer.lblpretype.Text + "��״̬���Ƿ�����л���", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            PreType = (PreType == "0" ? "1" : "0");
            this.m_mthInit();
        }
        #endregion

        #region ��ȡ��ʷԤ������Ϣ
        /// <summary>
        /// ��ȡ��ʷԤ������Ϣ
        /// </summary>
        public void m_mthGetPrePayHistoryInfo()
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID == "")
            {
                return;
            }

            this.m_objViewer.dtgHistory.Rows.Clear();

            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;

            DataTable dt;
            long l = this.objSvc.m_lngGetPrepayByRegID(RegID, 1, out dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] s = new string[13];

                    s[0] = Convert.ToString(i + 1);
                    s[1] = dt.Rows[i]["prepayinv_vchr"].ToString();

                    string cuycate = dt.Rows[i]["cuycate_int"].ToString();
                    if (cuycate == "1")
                    {
                        cuycate = "�ֽ�";
                    }
                    else if (cuycate == "2")
                    {
                        cuycate = "֧Ʊ";
                    }
                    else if (cuycate == "3")
                    {
                        cuycate = "���п�";
                    }
                    else if (cuycate == "4")
                    {
                        cuycate = "΢��2";
                    }
                    else if (cuycate == "5")
                    {
                        cuycate = "����";
                    }
                    else if (cuycate == "6")
                    {
                        cuycate = "֧����";     // ����.֧����
                    }
                    else if (cuycate == "8")
                    {
                        cuycate = "΢��";
                    }
                    else if (cuycate == "9")
                    {
                        cuycate = "֧����";     // ����.֧����
                    }
                    else
                    {
                        cuycate = "�ֽ�";
                    }
                    s[2] = cuycate;

                    s[3] = clsPublic.ConvertObjToDecimal(dt.Rows[i]["money_dec"]).ToString("0.00");

                    Color FCR = Color.Black;
                    string status = dt.Rows[i]["paytype_int"].ToString();
                    if (status == "1")
                    {
                        status = "����";
                    }
                    else if (status == "2")
                    {
                        status = "�˿�";
                        FCR = Color.Red;
                    }
                    else if (status == "3")
                    {
                        status = "�ָ�";
                        FCR = Color.FromArgb(0, 138, 89);
                    }
                    else if (status == "4")
                    {
                        status = "�嵥";
                        FCR = Color.Firebrick;
                    }
                    s[4] = status;

                    s[5] = dt.Rows[i]["lastname_vchr"].ToString();
                    s[6] = Convert.ToDateTime(dt.Rows[i]["create_dat"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

                    string uptype = dt.Rows[i]["uptype_int"].ToString();
                    if (uptype == "0")
                    {
                        uptype = "����";
                    }
                    else if (uptype == "1")
                    {
                        uptype = "�ֹ�";
                    }
                    s[7] = uptype;

                    int isrec = Convert.ToInt32(dt.Rows[i]["balanceflag_int"].ToString());
                    s[8] = (isrec == 1 ? "��" : "��");
                    // �ش�Ʊ��
                    s[9] = dt.Rows[i]["repprnbillno_vchr"].ToString();
                    int isclear = Convert.ToInt32(dt.Rows[i]["isclear_int"].ToString());
                    s[10] = (isclear == 1 ? "����" : "δ��");

                    s[11] = dt.Rows[i]["confirmemp"].ToString();
                    s[12] = dt.Rows[i]["des_vchr"].ToString();
                    if (dt.Rows[i]["originvono_vchr"].ToString().Trim() != "")
                    {
                        s[12] = "ԭʼ����: " + dt.Rows[i]["originvono_vchr"].ToString().Trim() + "  " + s[11];
                    }

                    int row = this.m_objViewer.dtgHistory.Rows.Add(s);
                    this.m_objViewer.dtgHistory.Rows[row].Tag = dt.Rows[i];
                    this.m_objViewer.dtgHistory.Rows[row].DefaultCellStyle.ForeColor = FCR;

                    if (Math.IEEERemainder(Convert.ToDouble(i + 1), 2) == 0)
                    {
                        this.m_objViewer.dtgHistory.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }
                }

                if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 3)
                {
                    this.m_objViewer.btnNew.Enabled = false;
                    this.m_objViewer.btnCharge.Enabled = false;
                    this.m_objViewer.btnRefundment.Enabled = false;
                    this.m_objViewer.btnRepeatPrn.Enabled = false;
                    this.m_objViewer.tableLayoutPanel1.Enabled = false;
                }
                else
                {
                    this.m_objViewer.btnNew.Enabled = true;
                    this.m_objViewer.btnCharge.Enabled = true;
                    this.m_objViewer.btnRefundment.Enabled = true;
                    this.m_objViewer.btnRepeatPrn.Enabled = true;
                    this.m_objViewer.tableLayoutPanel1.Enabled = true;
                }
            }
        }
        #endregion

        #region �շ�
        /// <summary>
        /// �շ�
        /// </summary>
        public void m_mthCharge()
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID == "")
            {
                return;
            }

            //ȡֵ
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;
            string Zyh = this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh;
            string CardNo = this.m_objViewer.ucPatientInfo.BihPatient_VO.CardNO;
            string BillNo = this.m_objViewer.txtprebillno.Text.Trim();
            string Money = this.m_objViewer.txtMoney.Text.Trim();
            string PayType = this.m_objViewer.cbopaytype.SelectedIndex.ToString();
            string Note = this.m_objViewer.txtnote.Text.Trim();

            //У��
            if (BillNo == "")
            {
                MessageBox.Show("������Ԥ�����ţ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtprebillno.Focus();
                this.m_objViewer.txtprebillno.SelectAll();
                return;
            }

            if (Money == "")
            {
                MessageBox.Show("������Ԥ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtMoney.Focus();
                this.m_objViewer.txtMoney.SelectAll();
                return;
            }

            if (!Microsoft.VisualBasic.Information.IsNumeric(Money))
            {
                MessageBox.Show("�����������֣����������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtMoney.Focus();
                this.m_objViewer.txtMoney.SelectAll();
                return;
            }

            if (Convert.ToDecimal(Money) <= 0)
            {
                MessageBox.Show("�����������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtMoney.Focus();
                this.m_objViewer.txtMoney.SelectAll();
                return;
            }

            if (PayType == "-1")
            {
                MessageBox.Show("��ѡ��֧�����ͣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.cbopaytype.Focus();
                return;
            }

            if (this.PreType == "0")
            {
                if (!clsPublic.m_blnCheckPrepayNoExpression(BillNo))
                {
                    MessageBox.Show("��ǰ�����Ԥ�����Ų����ϱ���������������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.txtprebillno.Focus();
                    this.m_objViewer.txtprebillno.SelectAll();
                    return;
                }
            }

            //if (clsPublic.m_blnCheckPrepayNoIsUsed(BillNo, int.Parse(PreType)))
            if (clsPublic.m_blnCheckPrepayNoIsUsed(BillNo, 0))
            {
                MessageBox.Show("��ǰ�����Ԥ�������Ѿ���ʹ�ã����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtprebillno.Focus();
                this.m_objViewer.txtprebillno.SelectAll();
                return;
            }

            frmPrepayAlert fa = new frmPrepayAlert(this.m_objViewer.cbopaytype.Text.Substring(2), clsPublic.DoubleConvertToCurrency(double.Parse(Money)));
            if (fa.ShowDialog() == DialogResult.No)
            {
                return;
            }

            this.m_objViewer.Cursor = Cursors.WaitCursor;

            //��ֵ
            clsBihPrePay_VO PrePay_VO = new clsBihPrePay_VO();
            PrePay_VO.strPrePayInv = BillNo;
            PrePay_VO.strPatientID = this.m_objViewer.ucPatientInfo.BihPatient_VO.PatientID;
            PrePay_VO.strRegisterID = RegID;
            PrePay_VO.decMoney = clsPublic.ConvertObjToDecimal(Money);
            PrePay_VO.intPayType = 1;
            PrePay_VO.intCuyCate = int.Parse(PayType) + 1;
            PrePay_VO.strAreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
            PrePay_VO.strDes = Note;
            PrePay_VO.strCreatorID = this.m_objViewer.LoginInfo.m_strEmpID;
            PrePay_VO.intUpType = int.Parse(PreType);
            PrePay_VO.strPatientName = this.m_objViewer.ucPatientInfo.lblName.Text;
            PrePay_VO.strAreaName = this.m_objViewer.ucPatientInfo.txtArea.Text;

            //����
            string PrePayID = "";
            long l = this.objSvc.m_lngAddPrePay(PrePay_VO, out PrePayID);
            if (l > 0)
            {
                clsPublic.m_blnSaveCurrInvoiceNo(this.m_objViewer.LoginInfo.m_strEmpID, BillNo, 2);
                this.m_blnSavePrePayBillNo(BillNo);
                if (this.PreType == "0")
                {
                    //this.m_objViewer.txtprebillno.Text = Convert.ToString(int.Parse(BillNo) + 1);
                    this.m_objViewer.txtprebillno.Text = "";
                }
                else
                {
                    this.m_objViewer.txtprebillno.Text = "";
                }
                this.m_mthGetPrePayHistoryInfo();
                this.m_mthInit();

                try
                {
                    if (PreType == "0")
                    {
                        if (NorPrint)
                        {
                            clsPBNetPrint.m_mthPrintPrepayBill(PrePayID, "");
                        }
                    }
                    else if (PreType == "1")
                    {
                        if (HandPrint)
                        {
                            clsPBNetPrint.m_mthPrintPrepayBill(PrePayID, "");
                        }
                    }

                    this.m_objViewer.ucPatientInfo.m_mthFind(Zyh, 2);
                    //clsPublic.m_mthWriteParm(RegID, Zyh, CardNo);                    
                    this.m_objViewer.Cursor = Cursors.Default;

                    //MessageBox.Show("�շѳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.m_objViewer.btnFind.Focus();
                }
                catch
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                }
            }
            else
            {
                this.m_objViewer.Cursor = Cursors.Default;
                MessageBox.Show("��������ʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region �ش�
        /// <summary>
        /// �ش�
        /// </summary>
        public void m_mthRepeatPrn()
        {
            if (this.m_objViewer.dtgHistory.Rows.Count == 0)
            {
                return;
            }

            if (this.m_objViewer.dtgHistory.SelectedRows.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫ�ش�İ��𵥾ݣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dr = this.m_objViewer.dtgHistory.SelectedRows[0].Tag as DataRow;

            if (dr["paytype_int"].ToString() == "2")
            {
                if (MessageBox.Show("�ð�������Ʊ���Ƿ�ȷ���ش�", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
                //MessageBox.Show("���˿�İ��𵥾ݲ����ش�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }

            string prepayid = dr["prepayid_chr"].ToString();
            string oldno = dr["prepayinv_vchr"].ToString();

            frmPrePayRepeatPrn fpprp = new frmPrePayRepeatPrn(oldno, int.Parse(PreType), this.m_objViewer.LoginInfo.m_strEmpID);
            if (fpprp.ShowDialog() == DialogResult.OK)
            {
                string prntype = fpprp.PrnType;
                string newno = fpprp.NewNo;

                if (prntype == "1")
                {
                    clsPBNetPrint.m_mthPrintPrepayBill(prepayid, "");
                    //clsInviocePrint_GD.m_mthPrintPrepayBill(prepayid, "");
                }
                else if (prntype == "2")
                {
                    long l = this.objSvc.m_lngSaveRepeatPrn(prepayid, oldno, newno, this.m_objViewer.LoginInfo.m_strEmpID, "1");
                    if (l > 0)
                    {
                        clsPBNetPrint.m_mthPrintPrepayBill(prepayid, newno);
                        //clsInviocePrint_GD.m_mthPrintPrepayBill(prepayid, newno);
                        clsPublic.m_blnSaveCurrInvoiceNo(this.m_objViewer.LoginInfo.m_strEmpID, newno, 2);
                        this.m_blnSavePrePayBillNo(newno);

                        //this.m_objViewer.txtprebillno.Text = Convert.ToString(int.Parse(newno) + 1);
                        this.m_objViewer.txtprebillno.Text = "";//m_mthInit()�������ʼ��
                        this.m_mthInit();
                    }
                    else
                    {
                        MessageBox.Show("�����ش���Ϣʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (prntype == "3")
                {
                    clsPBNetPrint.m_mthPrintPrepayBill(prepayid, newno);
                }
            }
        }
        #endregion

        #region �˿�ָ��ͳ嵥
        /// <summary>
        /// �˿���ָ�
        /// </summary>
        /// <param name="type">���� 2 �˿� 3 �ָ� 4 �嵥</param>
        public void m_mthRefundmentAndResumeAndStrike(int type)
        {
            if (this.m_objViewer.dtgHistory.Rows.Count == 0)
            {
                return;
            }

            string msg = "";
            if (type == 2)
            {
                msg = "�˿�";
            }
            else if (type == 3)
            {
                msg = "�ָ�";
            }
            else if (type == 4)
            {
                msg = "�嵥";
            }

            if (this.m_objViewer.dtgHistory.SelectedRows.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫ" + msg + "�İ��𵥾ݣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dr = this.m_objViewer.dtgHistory.SelectedRows[0].Tag as DataRow;

            if (dr["isclear_int"].ToString() == "1")
            {
                MessageBox.Show("�ñ�Ԥ�����ѳ��ʣ�������ѡ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string prepayno = dr["prepayinv_vchr"].ToString();//Ʊ��
            string prepayid = "";//ID

            //�ñ�Ԥ����״̬ 1 ���� 2 �˿� 3 �ָ� 4 �嵥
            int status = 1;
            if (type == 2)
            {
                for (int i = 0; i < this.m_objViewer.dtgHistory.Rows.Count; i++)
                {
                    DataRow tmpdr = this.m_objViewer.dtgHistory.Rows[i].Tag as DataRow;

                    if (prepayno == tmpdr["prepayinv_vchr"].ToString() && tmpdr["paytype_int"].ToString() == "2")
                    {
                        status = 2;
                        break;
                    }
                }

                if (status == 2)
                {
                    MessageBox.Show("�ñ�Ԥ�����Ѱ����˿�������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                prepayid = dr["prepayid_chr"].ToString();
            }
            else if (type == 3)
            {
                for (int i = 0; i < this.m_objViewer.dtgHistory.Rows.Count; i++)
                {
                    DataRow tmpdr = this.m_objViewer.dtgHistory.Rows[i].Tag as DataRow;

                    if (prepayno == tmpdr["prepayinv_vchr"].ToString() && tmpdr["paytype_int"].ToString() == "2")
                    {
                        prepayid = tmpdr["prepayid_chr"].ToString();
                        status = 2;
                    }
                    else if (prepayno == tmpdr["prepayinv_vchr"].ToString() && tmpdr["paytype_int"].ToString() == "3")
                    {
                        status = 3;
                        break;
                    }
                    else if (prepayno == tmpdr["prepayinv_vchr"].ToString() && tmpdr["paytype_int"].ToString() == "4")
                    {
                        status = 4;
                        break;
                    }
                }

                if (status == 1)
                {
                    MessageBox.Show("�ñ�Ԥ����Ϊ�����շ�(δ�˿�)�����ܰ���ָ�������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (status == 3)
                {
                    MessageBox.Show("�ñ�Ԥ�����Ѱ���ָ�������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (status == 4)
                {
                    MessageBox.Show("�ñ�Ԥ�����Ѱ���嵥������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (type == 4)
            {
                for (int i = 0; i < this.m_objViewer.dtgHistory.Rows.Count; i++)
                {
                    DataRow tmpdr = this.m_objViewer.dtgHistory.Rows[i].Tag as DataRow;

                    if (prepayno == tmpdr["prepayinv_vchr"].ToString() && tmpdr["paytype_int"].ToString() == "2")
                    {
                        status = 2;
                        break;
                    }
                    else if ((prepayno == tmpdr["prepayinv_vchr"].ToString() || prepayno == tmpdr["originvono_vchr"].ToString()) && tmpdr["paytype_int"].ToString() == "4")
                    {
                        status = 4;
                        break;
                    }
                }

                if (status == 2)
                {
                    MessageBox.Show("�ñ�Ԥ�����Ѱ����˿�������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (status == 4)
                {
                    MessageBox.Show("�ñ�Ԥ�����Ѱ���嵥������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                prepayid = dr["prepayid_chr"].ToString();
            }

            //string prepayid = dr["prepayid_chr"].ToString();

            string ConfirmEmpID = "";
            DialogResult dlg = clsPublic.m_dlgConfirm(out ConfirmEmpID);
            if (dlg == DialogResult.Yes)
            {
                string BillNo = "";
                string CuyCate = "1";
                if (type == 4)
                {
                    frmPrePayNoInput fpp = new frmPrePayNoInput();
                    fpp.NewNo = PrepayBillNo;
                    if (fpp.ShowDialog() == DialogResult.OK)
                    {
                        BillNo = fpp.NewNo;
                        CuyCate = fpp.CuyCate;
                    }
                    else
                    {
                        MessageBox.Show("�嵥�������뵥�š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                // �˿�
                if (type == 2)
                {
                    frmInvoiceRefundReason frmR = new frmInvoiceRefundReason(3, prepayno, ConfirmEmpID);
                    if (frmR.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }

                string NewPrePayID = "";
                long l = this.objSvc.m_lngRefundAndResumeAndStrikePrePay(prepayid, BillNo, this.m_objViewer.LoginInfo.m_strEmpID, ConfirmEmpID, type, CuyCate, out NewPrePayID);
                if (l > 0)
                {
                    if (type == 4)
                    {
                        clsPublic.m_blnSaveCurrInvoiceNo(this.m_objViewer.LoginInfo.m_strEmpID, BillNo, 2);
                        this.m_blnSavePrePayBillNo(BillNo);
                        this.m_objViewer.txtprebillno.Text = Convert.ToString(int.Parse(BillNo) + 1);

                        this.m_mthInit();
                        clsPBNetPrint.m_mthPrintPrepayBill(NewPrePayID, "");
                    }
                    else
                    {
                        MessageBox.Show(msg + "�ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.m_mthGetPrePayHistoryInfo();
                    this.m_objViewer.ucPatientInfo.m_mthFind(this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, 2);
                }
                else
                {
                    MessageBox.Show(msg + "ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
