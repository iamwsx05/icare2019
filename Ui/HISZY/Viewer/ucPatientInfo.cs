using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    #region ί��
    /// <summary>
    /// ���ƿ��Ÿı�ί��
    /// </summary>
    public delegate void TextCardNOChanged();

    /// <summary>
    /// סԺ�Ÿı�ί��
    /// </summary>
    public delegate void TextZyhChanged();
    #endregion

    /// <summary>
    /// ������Ϣ�ؼ���
    /// </summary>
    public partial class ucPatientInfo : UserControl
    {
        #region ����������
        /// <summary>
        /// ��������VO
        /// </summary>
        private clsBihPatient_VO Patient_VO = new clsBihPatient_VO();
        /// <summary>
        /// סԺ��������VO
        /// </summary>
        public clsBihPatient_VO BihPatient_VO
        {
            get
            {
                return Patient_VO;
            }
        }
        /// <summary>
        /// ��ѯ״̬ 1 ��Ժ 2 Ԥ��Ժ 3 ��ʽ��Ժ 4 ��� 5 ���� 8 ��Ժ���� 9 Ԥ���ֱ�ա�������
        /// </summary>
        private int status = 0;
        /// <summary>
        /// ��ѯ״̬ 1 ��Ժ 2 Ԥ��Ժ 3 ��ʽ��Ժ 4 ��� 5 ���� 8 ��Ժ���� 9 Ԥ���ֱ�ա�������
        /// </summary>
        public int Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        }
        /// <summary>
        /// �����Ƿ�ı�
        /// </summary>
        private bool ischanged = false;
        /// <summary>
        /// �����Ƿ�ı�
        /// </summary>
        public bool IsChanged
        {
            set
            {
                ischanged = value;
            }
            get
            {
                return ischanged;
            }
        }
        /// <summary>
        /// ��Ժ�Ǽ���ˮ��
        /// </summary>
        private string registerid = "";
        /// <summary>
        /// ��Ժ�Ǽ���ˮ��
        /// </summary>
        public string RegisterID
        {
            get
            {
                return registerid;
            }
        }
        /// <summary>
        /// ����Ԥ����ID(�ṹ��ID;NO;MONEY)
        /// </summary>
        private ArrayList prepayidarr = new ArrayList();
        /// <summary>
        /// ����Ԥ����ID(�ṹ��ID;NO;MONEY)
        /// </summary>
        public ArrayList PrePayIDArr
        {
            get
            {
                return prepayidarr;
            }
        }
        /// <summary>
        /// ����Ԥ������
        /// </summary>
        private decimal Balanceprepaymoney = 0;
        /// <summary>
        /// ����Ԥ������
        /// </summary>
        public decimal BalancePrepayMoney
        {
            get
            {
                return Balanceprepaymoney;
            }
        }
        /// <summary>
        /// ���·���ʱ��
        /// </summary>
        private string maxfeedate = "";
        /// <summary>
        /// ���·���ʱ��
        /// </summary>
        public string MaxFeeDate
        {
            get
            {
                return maxfeedate;
            }
        }
        /// <summary>
        /// �ؼ��Ƿ�ӵ�н���
        /// </summary>
        private bool isfocus = false;
        /// <summary>
        /// �ؼ��Ƿ�ӵ�н���
        /// </summary>
        public bool IsFocus
        {
            get
            {
                return isfocus;
            }
        }
        /// <summary>
        /// DOMAIN����
        /// </summary>
        private clsDcl_Charge objSvc;
        /// <summary>
        /// ��¼��������
        /// </summary>
        private Form frmMark = null;
        /// <summary>
        /// (����)����ID
        /// </summary>
        private string parm_areaid = "";
        /// <summary>
        /// (����)����ID
        /// </summary>
        public string Parm_AreaID
        {
            set
            {
                parm_areaid = value;
            }
        }
        /// <summary>
        /// (����)��������
        /// </summary>
        public string Parm_AreaName
        {
            set
            {
                this.txtArea.Text = value;
                this.txtBedno.Focus();
            }
        }
        /// <summary>
        /// (����)����ID
        /// </summary>
        private string parm_bedid = "";
        /// <summary>
        /// (����)����ID
        /// </summary>
        public string Parm_BedID
        {
            set
            {
                parm_bedid = value;
            }
        }
        /// <summary>
        /// (����)��������
        /// </summary>
        public string Parm_BedName
        {
            set
            {
                this.txtBedno.Text = value;
            }
        }

        /// <summary>
        /// (����)סԺ��
        /// </summary>
        private string parm_zyh = "";
        /// <summary>
        /// (����)סԺ��
        /// </summary>
        public string Parm_Zyh
        {
            set
            {
                parm_zyh = value;
                this.m_mthFind(parm_zyh, 2);
                if (ZyhChanged != null)
                {
                    ZyhChanged();
                }
            }
        }

        /// <summary>
        /// ��¼Ա��������(����)ID�б�
        /// </summary>
        private ArrayList ObjDeptIDArr = new ArrayList();
        /// <summary>
        /// ��¼Ա��������(����)�б�
        /// </summary>
        private clsDepartmentVO[] deptarr;
        /// <summary>
        /// ��¼Ա��������(����)�б�(����)
        /// </summary>
        public clsDepartmentVO[] DeptArr
        {
            set
            {
                deptarr = value;

                for (int i = 0; i < deptarr.Length; i++)
                {
                    ObjDeptIDArr.Add(((clsDepartmentVO)deptarr[i]).strDeptID);
                }
            }
        }
        /// <summary>
        /// ��ǰҽ��ͳ����
        /// </summary>
        private decimal YBSum = 0;
        /// <summary>
        /// ����
        /// </summary>
        public string strBedNo = "";

        /// <summary>
        /// ���ú˶�״̬: 0 Ĭ��״̬ 1 ���ں˶� 2 ��ɺ˶� 3 ��Ժ����
        /// </summary>
        private int _FeeCheckStatus = 0;
        /// <summary>
        /// ���ú˶�״̬
        /// </summary>
        public int FeeCheckStatus
        {
            set
            {
                _FeeCheckStatus = value;
            }
            get
            {
                return _FeeCheckStatus;
            }
        }
        /// <summary>
        /// ��ʾ���ú˶�״̬��־
        /// </summary>
        private bool _ShowFeeCheckStatusFlag = false;
        /// <summary>
        /// ��ʾ���ú˶�״̬��־
        /// </summary>
        public bool ShowFeeCheckStatusFlag
        {
            set
            {
                _ShowFeeCheckStatusFlag = value;
            }
            get
            {
                return _ShowFeeCheckStatusFlag;
            }
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ���ƿ��Ÿı��¼�
        /// </summary>
        public event TextCardNOChanged CardNOChanged;

        /// <summary>
        /// סԺ�Ÿı��¼�
        /// </summary>
        public event TextZyhChanged ZyhChanged;
        #endregion

        #region ����
        /// <summary>
        /// ������Ϣ�ؼ���
        /// </summary>
        public ucPatientInfo()
        {
            InitializeComponent();

            objSvc = new clsDcl_Charge();
        }
        #endregion

        #region ���Ҳ���ֵ
        /// <summary>
        /// ���Ҳ���ֵ
        /// </summary>
        /// <param name="no">����</param>        
        /// <param name="type">1 ���ƿ��� 2 סԺ�� 3 ��Ժ�Ǽ���ˮ��</param>
        public void m_mthFind(string no, int type)
        {
            this.IsChanged = false;
            this.parm_zyh = "";

            if (Patient_VO != null)
            {
                Patient_VO = null;
            }
            Patient_VO = new clsBihPatient_VO();
            DataTable dt = new DataTable();


            long l = this.objSvc.m_lngGetPatientinfoByNO(no, out dt, status, type);

            if (l > 0 && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];

                string RegID = dr["registerid_chr"].ToString().Trim();

                Patient_VO.RegisterID = RegID;
                Patient_VO.CardNO = dr["patientcardid_chr"].ToString().Trim();
                Patient_VO.PatientID = dr["patientid_chr"].ToString().Trim();
                Patient_VO.Zyh = dr["inpatientid_chr"].ToString().Trim();
                Patient_VO.Zycs = int.Parse(dr["inpatientcount_int"].ToString());
                Patient_VO.Name = dr["lastname_vchr"].ToString().Trim();
                Patient_VO.Sex = dr["sex_chr"].ToString().Trim();

                // 1-22���޸� ��Ǽǵķ�������һ�� 
                //Patient_VO.Age = clsPublic.CalcAge(Convert.ToDateTime(dr["birth_dat"].ToString()));
                Patient_VO.Age = (new clsBrithdayToAge().m_strGetAge(Convert.ToDateTime(dr["birth_dat"]))).TrimEnd('��');
                Patient_VO.m_dtmBirthDay = Convert.ToDateTime(dr["birth_dat"]);

                Patient_VO.IdCard = dr["idcard_chr"].ToString().Trim();
                Patient_VO.PayTypeID = dr["paytypeid_chr"].ToString().Trim();
                Patient_VO.IdNo = dr["idno_vchr"].ToString();
                Patient_VO.Fee = dr["paytypename_vchr"].ToString().Trim();
                Patient_VO.InType = dr["inpatientnotype_int"].ToString().Trim();
                Patient_VO.InHospitalDate = dr["rysj"].ToString();
                Patient_VO.AreaID = dr["areaid_chr"].ToString().Trim();
                Patient_VO.AreaName = dr["deptname_vchr"].ToString().Trim();

                Patient_VO.BedNO = dr["code_chr"].ToString().Trim();
                Patient_VO.OutHospitalDate = dr["cysj"].ToString();
                Patient_VO.Days = Convert.ToInt32(dr["indays"].ToString());
                Patient_VO.Status = Convert.ToInt32(dr["pstatus_int"].ToString());
                Patient_VO.FeeStatus = Convert.ToInt32(dr["feestatus_int"].ToString());

                Patient_VO.SpecialInfo = dr["remarkname_vchr"].ToString().Trim();
                Patient_VO.Diagnose = dr["diagnose_vchr"].ToString().Trim();
                Patient_VO.Note = dr["note"].ToString().Trim();

                //��ǰҽ��ͳ����
                YBSum = clsPublic.ConvertObjToDecimal(dr["ybsum"]);

                this.m_mthGetPatientFeeInfo(RegID);
                this.m_mthSetValue();

                Patient_VO.BedID = dr["bedid_chr"].ToString().Trim();
                Patient_VO.SpecChargeCtrl = Convert.ToInt32(dr["SpecChargeCtrl"].ToString());
                this.parm_areaid = Patient_VO.AreaID;

                /***����ҽ����Ϣ***/
                Patient_VO.DoctorID = dr["doctorid"].ToString().Trim();
                Patient_VO.DoctorName = dr["doctorname"].ToString().Trim();
                DataTable dtDoct;
                l = this.objSvc.m_lngGetGroupEmp(this.BihPatient_VO.DoctorID, out dtDoct);
                if (l > 0 && dtDoct.Rows.Count > 0)
                {
                    Patient_VO.DoctorGroupID = dtDoct.Rows[0]["groupid_chr"].ToString();
                }
                /*****************/

                /***ҽ����Ϣ***/
                Patient_VO.InsuredTotalMoney = clsPublic.ConvertObjToDecimal(dr["insuredtotalmoney_mny"]);
                Patient_VO.InsuredZycs = int.Parse(clsPublic.ConvertObjToDecimal(dr["insuredpaytime_int"]).ToString()) + 1;
                /**************/

                //���ú˶�״̬
                this.FeeCheckStatus = int.Parse(dr["checkstatus_int"].ToString());
                this.m_mthShowFeeCheckStatus();

                clsPublic.m_mthWriteParm(Patient_VO.RegisterID, Patient_VO.Zyh, Patient_VO.CardNO);
                this.IsChanged = true;
            }
            else
            {
                string msg = "";

                switch (status)
                {
                    case 2:
                        msg = "û�����������Ĳ�����Ϣ(��ʾ�����˳�Ժǰ�����Ԥ��Ժ����)��";
                        break;
                    default:
                        msg = "û�����������Ĳ�����Ϣ��";
                        break;
                }

                MessageBox.Show(msg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="regid"></param>        
        public void m_mthGetPatientFeeInfo(string regid)
        {
            if (Patient_VO == null)
            {
                return;
            }

            prepayidarr = new ArrayList();
            Balanceprepaymoney = 0;
            DataTable dt;

            //��ȡԤ����            
            long l = 0;
            clsDcl_PrePay objPrepay = new clsDcl_PrePay();

            if (Patient_VO.FeeStatus == 4)
            {
                decimal d = 0;
                l = objPrepay.m_lngGetBadChargePrepayByRegID(regid, out d);
                if (l > 0)
                {
                    Patient_VO.PrepayMoney = 0;
                    Patient_VO.PrepayMoneyBadCharge = d;
                }
            }
            else
            {
                l = objPrepay.m_lngGetPrepayByRegID(regid, 2, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string id = dt.Rows[i]["prepayid_chr"].ToString();
                        string no = dt.Rows[i]["prepayinv_vchr"].ToString();
                        decimal money = clsPublic.ConvertObjToDecimal(dt.Rows[i]["balancetotal"]);
                        Balanceprepaymoney += money;

                        prepayidarr.Add(id + ";" + no + ";" + money.ToString());
                    }

                    //Ԥ�����Ϊ��ʾ��ǰ����Ԥ����
                    Patient_VO.PrepayMoney = Balanceprepaymoney;
                }
            }

            //��ȡ�ܷ��á����ᡢ���塢ֱ���շѡ����塢���ࡢ�������ڡ�δ������
            decimal TotalFee = 0;
            decimal WaitChargeFee = 0;
            decimal WaitClearFee = 0;
            decimal DirectorFee = 0;
            decimal CompleteClearFee = 0;
            string ClearFeeDate = "";
            int NoChargeDays = 0;

            //���·���ʱ��
            maxfeedate = DateTime.Now.ToString("yyyy-MM-dd");

            clsDcl_Charge objCharge = new clsDcl_Charge();

            int intParm = 0;
            //�ж��Ƿ�Ϊ��Ժ�����Ժ����Ժ����ʱҪ�ж�ĸӤ���㹦��
            if (Status == 8)
            {
                //ĸӤ�ϲ����㹦�ܿ��أ�Ϊ�ء���Ϊ��
                intParm = clsPublic.m_intGetSysParm("1119");
            }

            if (intParm == 0)
            {
                l = objCharge.m_lngGetChargeInfoByID(regid, 1, out dt);
            }
            else
            {
                l = objCharge.m_lngGetChargeInfoByIDForBaby(regid, out dt);
            }
            if (l > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //�����ܷ���
                    decimal d = clsPublic.Round(clsPublic.ConvertObjToDecimal(dt.Rows[i]["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dt.Rows[i]["amount_dec"]), 2);
                    //��������
                    decimal decDiffSum = clsPublic.Round(clsPublic.ConvertObjToDecimal(dt.Rows[i]["TOTALDIFFCOSTMONEY_DEC"]), 2);
                    //�ܷ���
                    TotalFee += d + decDiffSum;
                    //����״̬ 0 ��ȷ�� 1 ���� 2 ���� 3 ���� 4 ֱ��
                    int pstatus = Convert.ToInt32(dt.Rows[i]["pstatus_int"].ToString());
                    if (pstatus == 1)
                    {
                        WaitChargeFee += d + decDiffSum;
                    }
                    else if (pstatus == 2)
                    {
                        WaitClearFee += d + decDiffSum;
                    }
                    else if (pstatus == 3)
                    {
                        CompleteClearFee += d + decDiffSum;
                    }
                    else if (pstatus == 4)
                    {
                        DirectorFee += d + decDiffSum;
                    }
                }

                //���·���ʱ��
                DataView dv = new DataView(dt);
                dv.Sort = "chargeactive_dat asc";
                if (dv.Count > 0)
                {
                    maxfeedate = Convert.ToDateTime(dv[dv.Count - 1]["chargeactive_dat"].ToString()).ToString("yyyy-MM-dd");
                }

                //�����������                
                dv.RowFilter = "pstatus_int = 3";
                dv.Sort = "chearaccount_dat asc";
                if (dv.Count > 0)
                {
                    ClearFeeDate = Convert.ToDateTime(dv[dv.Count - 1]["chearaccount_dat"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }

                //δ��(���ᡢ����)����
                dv.RowFilter = "pstatus_int = 1 or pstatus_int = 2";
                dv.Sort = "chargeactive_dat asc";
                if (dv.Count > 0)
                {
                    Hashtable has = new Hashtable();
                    foreach (DataRowView drv in dv)
                    {
                        string s = Convert.ToDateTime(drv["chargeactive_dat"].ToString()).ToString("yyyy-MM-dd");
                        if (has.ContainsKey(s) || (s == DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            continue;
                        }
                        else
                        {
                            has.Add(s, null);
                        }
                    }

                    NoChargeDays = has.Count;
                }
            }

            //���� �� ����Ԥ���� - ���� - ����
            decimal BalanceFee = Balanceprepaymoney - WaitChargeFee - WaitClearFee;

            if (Patient_VO.FeeStatus == 4 && Patient_VO.PrepayMoneyBadCharge > 0)
            {
                if (WaitClearFee >= Patient_VO.PrepayMoneyBadCharge)
                {
                    WaitClearFee -= Patient_VO.PrepayMoneyBadCharge;
                }
                else
                {
                    WaitChargeFee = WaitChargeFee + WaitClearFee - Patient_VO.PrepayMoneyBadCharge;
                    WaitClearFee = 0;
                }

                BalanceFee = (WaitChargeFee + WaitClearFee) * -1;
                CompleteClearFee += Patient_VO.PrepayMoneyBadCharge;
            }

            Patient_VO.TotalFee = TotalFee;
            Patient_VO.WaitChargeFee = WaitChargeFee;
            Patient_VO.WaitClearFee = WaitClearFee;
            Patient_VO.DirectorFee = DirectorFee;
            Patient_VO.CompleteClearFee = CompleteClearFee;
            Patient_VO.BalanceMoney = BalanceFee;
            Patient_VO.ClearFeeDate = ClearFeeDate;
            Patient_VO.NoChargeDays = NoChargeDays;
        }
        #endregion

        #region �ؼ���ֵ
        /// <summary>
        /// �ؼ���ֵ
        /// </summary>       
        public void m_mthSetValue()
        {
            string s2 = "";
            switch (Patient_VO.FeeStatus)
            {
                case 0:
                    s2 = "��";
                    break;
                case 1:
                    s2 = "����";
                    break;
                case 2:
                    s2 = "��;����";
                    break;
                case 3:
                    s2 = "��Ժ����";
                    break;
                case 4:
                    s2 = "���ʽ���";
                    break;
                case 5:
                    s2 = "����";
                    break;
                default:
                    s2 = "δ֪";
                    break;
            }

            string s1 = "";
            switch (Patient_VO.Status)
            {
                case 0:
                    s1 = "�´�";
                    break;
                case 1:
                    s1 = "�ڴ�";
                    break;
                case 2:
                    s1 = "Ԥ��Ժ";
                    if (Patient_VO.FeeStatus != 5)
                    {
                        s2 = "����";
                    }
                    break;
                case 3:
                    s1 = "��Ժ";
                    break;
                case 4:
                    s1 = "���";
                    break;
                default:
                    s1 = "δ֪";
                    break;
            }

            this.lblStatus.Text = "[" + s1 + "." + s2 + "]";
            this.txtCardno.Text = Patient_VO.CardNO;
            this.txtZyh.Text = Patient_VO.Zyh;
            this.lblZycs.Text = "��" + Patient_VO.Zycs.ToString() + "����Ժ";
            this.lblName.Text = Patient_VO.Name;
            this.lblSex.Text = Patient_VO.Sex;
            this.lblAge.Text = (Patient_VO.Age.Trim() == "" ? "" : Patient_VO.Age.Trim()) + "��";
            this.lblFee.Text = Patient_VO.Fee;
            this.lblInType.Text = Patient_VO.InType == "1" ? "��ͨסԺ" : "����סԺ";
            this.lblInHospitalDate.Text = Patient_VO.InHospitalDate;
            this.txtArea.Text = Patient_VO.AreaName;
            this.txtBedno.Text = Patient_VO.BedNO + "��";
            this.lblOutHospitalDate.Text = Patient_VO.OutHospitalDate;
            this.lblDays.Text = (Patient_VO.Days == 0 ? "" : Patient_VO.Days.ToString() + "��");

            this.lblSpecialInfo.Text = Patient_VO.SpecialInfo;
            this.toolTip1.SetToolTip(this.lblSpecialInfo, this.lblSpecialInfo.Text);
            this.lblDiag.Text = Patient_VO.Diagnose;
            this.toolTip2.SetToolTip(this.lblDiag, this.lblDiag.Text);
            this.lblNote.Text = Patient_VO.Note;
            this.toolTip3.SetToolTip(this.lblNote, this.lblNote.Text);

            this.lblTotalFee.Text = (Patient_VO.TotalFee == 0 ? "" : Patient_VO.TotalFee.ToString("###,##0.00"));
            this.lblWaitReckoningFee.Text = "����:" + (Patient_VO.WaitChargeFee == 0 ? "" : Patient_VO.WaitChargeFee.ToString("###,##0.00"));
            this.lblWaitClearFee.Text = (Patient_VO.WaitClearFee == 0 ? "" : Patient_VO.WaitClearFee.ToString("###,##0.00"));
            this.lblDirectorFee.Text = "ֱ��:" + (Patient_VO.DirectorFee == 0 ? "" : Patient_VO.DirectorFee.ToString("###,##0.00"));
            this.lblCompleteClearFee.Text = (Patient_VO.CompleteClearFee == 0 ? "" : Patient_VO.CompleteClearFee.ToString("###,##0.00"));
            this.lblClearDate.Text = Patient_VO.ClearFeeDate;
            this.lblNoChargeDays.Text = "δ��:" + Patient_VO.NoChargeDays.ToString() + "��";

            string Info = "";
            if (YBSum > 0)
            {
                decimal d = this.BihPatient_VO.TotalFee - YBSum;
                this.lblYbFee.Text = YBSum.ToString("###,##0.00");
                this.lblSelfPay.Text = d.ToString("###,##0.00");

                Patient_VO.BalanceMoney += YBSum;
                Info = " (��ҽ��ͳ��)";
            }
            else
            {
                this.lblYbFee.Text = "";
                this.lblSelfPay.Text = "";
            }

            if (Patient_VO.FeeStatus == 4)
            {
                if (Patient_VO.PrepayMoneyBadCharge > 0)
                {
                    this.lblPrepayMoney.Text = Patient_VO.PrepayMoneyBadCharge.ToString("###,##0.00") + "(�ѳ�)";
                    this.lblPrepayMoney.ForeColor = Color.Green;
                    this.label12.ForeColor = Color.Green;
                }
            }
            else
            {
                this.lblPrepayMoney.Text = (Patient_VO.PrepayMoney == 0 ? "" : Patient_VO.PrepayMoney.ToString("###,##0.00"));
            }

            this.lblBalanceMoney.Text = (Patient_VO.BalanceMoney == 0 ? "" : Patient_VO.BalanceMoney.ToString("###,##0.00") + Info);
            if (Patient_VO.BalanceMoney > 0)
            {
                this.lblBalanceMoney.ForeColor = Color.Blue;
                this.lblSpec.ForeColor = Color.Blue;
                this.lblSpecialInfo.ForeColor = Color.Blue;
                this.lblFee.ForeColor = Color.Blue;
            }
            else if (Patient_VO.BalanceMoney < 0)
            {
                this.lblBalanceMoney.ForeColor = Color.Red;
                this.lblSpec.ForeColor = Color.Red;
                this.lblSpecialInfo.ForeColor = Color.Red;
                this.lblFee.ForeColor = Color.Red;
            }

            this.registerid = Patient_VO.RegisterID;
            if (Patient_VO.Fee == "")
            {
                MessageBox.Show("�ò��˵ķѱ�Ϊ�գ�����Ժ��������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ݲ�ѯ
        /// <summary>
        /// ��ݲ�ѯ
        /// </summary>
        public void m_mthShortCurFind()
        {
            try
            {
                string tmpzyh = clsPublic.m_strReadParmZyh();
                if (tmpzyh == "")
                {
                    MessageBox.Show("�Բ���û����ʷ��ѯ��¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.m_mthFind(tmpzyh, 2);
                    this.ZyhChanged();
                    clsPublic.m_mthWriteParm(this.RegisterID, this.BihPatient_VO.Zyh, this.BihPatient_VO.CardNO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region ����(���)
        /// <summary>
        /// ����(���)
        /// </summary>
        public void m_mthReset()
        {
            this.m_mthClear(this.tableLayoutPanel1);

            this.txtArea.Text = "<��ѡ����>";
            this.txtBedno.Text = "<����>";
        }
        /// <summary>
        /// ���
        /// </summary>
        private void m_mthClear(Control ParentCtl)
        {
            foreach (Control ctl in ParentCtl.Controls)
            {
                if (ctl.GetType().FullName == "System.Windows.Forms.TextBox" || ctl.GetType().FullName == "System.Windows.Forms.Label")
                {
                    if (ctl.Tag != null && ctl.Tag.ToString().ToLower() == "edit")
                    {
                        ctl.Text = "";
                    }
                }

                if (ctl.Controls.Count > 0)
                {
                    this.m_mthClear(ctl);
                }
            }
        }
        #endregion

        #region ��ʾ����(����)
        /// <summary>
        /// ��ʾ����(����)
        /// </summary>
        public void m_mthShowArea()
        {
            frmAidFind fAid = new frmAidFind(this, 1, null, ObjDeptIDArr);
            fAid.StartPosition = FormStartPosition.Manual;

            frmMark = fAid;

            Point p1 = new Point(this.txtArea.Location.X - 16, this.txtArea.Location.Y + this.txtArea.Height - 2);
            Point p2 = this.txtArea.PointToScreen(p1);
            fAid.Location = new Point(p2.X, p2.Y);
            fAid.Show();
        }
        #endregion

        #region ������(����)
        /// <summary>
        /// ������(����)
        /// </summary>
        public void m_mthLoadArea()
        {
            DataTable dt = new DataTable();

            long l = this.objSvc.m_lngGetDeptArea(out dt, 2);
            if (l == 0)
            {
                return;
            }

            frmAreaBedInfo f = new frmAreaBedInfo(dt, 1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_mthFind(f.Zyh, 2);
                if (ZyhChanged != null)
                {
                    ZyhChanged();
                }
            }
        }
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        public void m_mthLoadBed()
        {
            if (parm_areaid != "" && parm_zyh == "")
            {
                if (this.txtBedno.Text.Trim() != "" && this.txtBedno.Text.Trim() != "<����>")
                {
                    string s = this.objSvc.m_strGetZyhByAreaAndBedID(parm_areaid, this.txtBedno.Text.Trim());
                    if (s != "")
                    {
                        this.Parm_Zyh = s;
                    }
                }
                else
                {
                    frmAidFind fAid = new frmAidFind(this, 2, parm_areaid, ObjDeptIDArr);
                    fAid.StartPosition = FormStartPosition.Manual;

                    frmMark = fAid;

                    Point p1 = new Point(this.txtBedno.Location.X - 100, this.txtBedno.Location.Y + this.txtBedno.Height - 2);
                    Point p2 = this.txtBedno.PointToScreen(p1);
                    fAid.Location = new Point(p2.X, p2.Y);
                    fAid.Width = fAid.Width - 40;
                    fAid.Show();
                }
            }
            else
            {
                if (parm_zyh != "")
                {
                    this.m_mthFind(parm_zyh, 2);
                    if (ZyhChanged != null)
                    {
                        ZyhChanged();
                    }
                }
                else
                {
                    this.txtCardno.Focus();
                }
            }
        }
        #endregion

        #region ��ʾ���ú˶����
        /// <summary>
        /// ��ʾ���ú˶����
        /// </summary>
        public void m_mthShowFeeCheckStatus()
        {
            if (this.ShowFeeCheckStatusFlag)
            {
                if (_FeeCheckStatus == 1)
                {
                    this.lblCheckStatus.Text = "���ں˶Է���";
                }
                else if (_FeeCheckStatus == 2)
                {
                    this.lblCheckStatus.Text = "������ɺ˶�";
                }
                else if (_FeeCheckStatus == 3)
                {
                    this.lblCheckStatus.Text = "�����Ժ����";
                }
                else
                {
                    this.lblCheckStatus.Text = "����δ�˶�";
                }
            }
            else
            {
                this.lblCheckStatus.Text = "";
            }
        }
        #endregion

        private void txtCardno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = this.txtCardno.Text.Trim();
                if (s != "")
                {
                    s = s.PadLeft(10, '0');
                    this.txtCardno.Text = s;
                    if (this.BihPatient_VO != null && s == this.BihPatient_VO.CardNO)
                    {
                        return;
                    }

                    this.m_mthFind(s, 1);
                    if (CardNOChanged != null)
                    {
                        CardNOChanged();
                    }
                }
                else
                {
                    this.txtZyh.Focus();
                }
            }
        }

        private void txtZyh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = this.txtZyh.Text.Trim();
                if (s != "")
                {
                    if (this.BihPatient_VO != null && s == this.BihPatient_VO.Zyh)
                    {
                        return;
                    }

                    this.m_mthFind(s, 2);
                    if (ZyhChanged != null)
                    {
                        ZyhChanged();
                    }
                }
            }
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            this.label3.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            this.label3.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.m_mthShortCurFind();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.label1.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.m_mthShortCurFind();
        }

        #region ���ÿؼ������ػ�
        /// <summary>
        /// ���ÿؼ������ػ�
        /// </summary>        
        public void m_mthSetRedraw()
        {
            this.m_mthSuspendLayout(this.tableLayoutPanel1);
            this.timer.Enabled = true;
            this.timer.Interval = 100;
        }

        /// <summary>
        /// ���𲼾�
        /// </summary>
        /// <param name="Ctrl"></param>
        private void m_mthSuspendLayout(Control Ctrl)
        {
            //Ctrl.SuspendLayout();
            Ctrl.Visible = false;

            if (Ctrl.HasChildren)
            {
                foreach (Control SubCtrl in Ctrl.Controls)
                {
                    this.m_mthSuspendLayout(SubCtrl);
                }
            }
        }

        /// <summary>
        /// �ָ�����
        /// </summary>
        /// <param name="Ctrl"></param>
        private void m_mthResumeLayout(Control Ctrl)
        {
            //Ctrl.ResumeLayout();
            Ctrl.Visible = true;

            if (Ctrl.HasChildren)
            {
                foreach (Control SubCtrl in Ctrl.Controls)
                {
                    this.m_mthResumeLayout(SubCtrl);
                }
            }
        }
        #endregion                    

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.m_mthResumeLayout(this.tableLayoutPanel1);
            //this.Refresh();
        }

        private void ucPatientInfo_Enter(object sender, EventArgs e)
        {
            isfocus = true;
        }

        private void ucPatientInfo_Leave(object sender, EventArgs e)
        {
            isfocus = false;
        }

        private void label29_MouseEnter(object sender, EventArgs e)
        {
            this.label29.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void label29_MouseLeave(object sender, EventArgs e)
        {
            this.label29.ForeColor = Color.Black;
        }

        private void label29_Click(object sender, EventArgs e)
        {
            if (this.registerid == "")
            {
                return;
            }

            #region ҽ������
            string ybCity = clsPublic.m_strGetSysparm("1000").Trim();
            if (ybCity == "006")  //����̨ɽҽ������
            {
                string strybpayID = clsPublic.m_strGetSysparm("0008").Trim();
                System.Collections.Generic.List<string> arrPayID = clsPublic.m_ArrGettoken(strybpayID, ";");
                if (arrPayID.IndexOf(this.BihPatient_VO.PayTypeID) >= 0)
                {
                    decimal InsuredMoney = 0;
                    frmYB_TS frmYB = new frmYB_TS();
                    frmYB.objPatient_VO = this.BihPatient_VO;
                    string strPatNameWithInID = "";
                    strPatNameWithInID = this.BihPatient_VO.Name.Trim();
                    if (this.BihPatient_VO.Zyh.Trim() != "")
                    {
                        if (strPatNameWithInID != "")
                        {
                            strPatNameWithInID += "(" + this.BihPatient_VO.Zyh.Trim() + ")";
                        }
                        else
                        {
                            strPatNameWithInID = this.BihPatient_VO.Zyh.Trim();
                        }
                    }
                    frmYB.lblzyh.Text = strPatNameWithInID;
                    frmYB.lblTotal.Text = this.lblTotalFee.Text;
                    frmYB.m_mthUploadYbInfo(this.BihPatient_VO.RegisterID, this.BihPatient_VO.Zyh);
                    if (frmYB.ShowDialog() == DialogResult.OK)
                    {
                        InsuredMoney = (frmYB.txtYBpay.Text == "" ? 0 : Convert.ToDecimal(frmYB.txtYBpay.Text));
                        decimal d = this.BihPatient_VO.TotalFee - InsuredMoney;
                        this.lblYbFee.Text = InsuredMoney.ToString("###,##0.00");
                        this.lblSelfPay.Text = d.ToString("###,##0.00");

                        Patient_VO.BalanceMoney += InsuredMoney - YBSum;
                        d = Patient_VO.BalanceMoney;
                        this.lblBalanceMoney.Text = (d == 0 ? "" : d.ToString("###,##0.00")) + " (��ҽ��ͳ��)";

                        if (d > 0)
                        {
                            this.lblBalanceMoney.ForeColor = Color.Blue;
                            this.lblSpec.ForeColor = Color.Blue;
                            this.lblSpecialInfo.ForeColor = Color.Blue;
                            this.lblFee.ForeColor = Color.Blue;
                        }

                        this.objSvc.m_lngUpdateInsuredSum(this.registerid, InsuredMoney);
                    }
                }
                return;
            }

            System.Collections.Generic.List<string> PayIDArr = clsPublic.m_mthGetYBPayID();
            if (PayIDArr.IndexOf(this.BihPatient_VO.PayTypeID) >= 0)
            {
                decimal TotalMoney = 0;
                decimal InsuredMoney = 0;
                string ErrMsg = "";

                clsCtl_YBCharge objYB = new clsCtl_YBCharge();
                if (objYB.m_blnBudget(this.registerid, this.BihPatient_VO.Zyh, this.Patient_VO.InsuredZycs, out TotalMoney, out InsuredMoney, out ErrMsg))
                {
                    decimal d = this.BihPatient_VO.TotalFee - InsuredMoney;
                    this.lblYbFee.Text = InsuredMoney.ToString("###,##0.00");
                    this.lblSelfPay.Text = d.ToString("###,##0.00");

                    Patient_VO.BalanceMoney += InsuredMoney - YBSum;
                    d = Patient_VO.BalanceMoney;
                    this.lblBalanceMoney.Text = (d == 0 ? "" : d.ToString("###,##0.00")) + " (��ҽ��ͳ��)";

                    if (d > 0)
                    {
                        this.lblBalanceMoney.ForeColor = Color.Blue;
                        this.lblSpec.ForeColor = Color.Blue;
                        this.lblSpecialInfo.ForeColor = Color.Blue;
                        this.lblFee.ForeColor = Color.Blue;
                    }

                    //����
                    this.objSvc.m_lngUpdateInsuredSum(this.registerid, InsuredMoney);
                }
                else
                {
                    MessageBox.Show("����ʧ�ܡ�" + ErrMsg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("���ݵ�ǰϵͳ�趨���ò��˲�������ҽ�����ߣ�������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmAidFind fAid = new frmAidFind(this, 1, null, ObjDeptIDArr);
                fAid.StartPosition = FormStartPosition.Manual;

                frmMark = fAid;

                Point p1 = new Point(this.txtArea.Location.X - 16, this.txtArea.Location.Y + this.txtArea.Height - 2);
                Point p2 = this.txtArea.PointToScreen(p1);
                fAid.Location = new Point(p2.X, p2.Y);
                fAid.Show();
            }
        }

        private void txtBedno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthLoadBed();
            }
        }

        private void txtArea_Enter(object sender, EventArgs e)
        {
            if (this.txtArea.Text.Trim() == "<��ѡ����>")
            {
                this.txtArea.Text = "";
            }
        }

        private void txtArea_Leave(object sender, EventArgs e)
        {
            if (this.parm_areaid == "" && this.registerid == "")
            {
                this.txtArea.Text = "<��ѡ����>";
            }
            this.m_mthDispose();
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthShowArea();
        }

        private void txtBedno_Enter(object sender, EventArgs e)
        {
            if (this.txtBedno.Text.Trim() == "<����>")
            {
                this.txtBedno.Text = "";
            }
        }

        private void txtBedno_Leave(object sender, EventArgs e)
        {
            if (this.parm_bedid == "" && this.registerid == "")
            {
                this.txtBedno.Text = "<����>";
            }
            this.m_mthDispose();
        }

        private void txtBedno_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthLoadBed();
        }

        private void m_mthDispose()
        {
            if (frmMark != null)
            {
                frmMark.Close();
                frmMark = null;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            this.m_mthLoadArea();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (parm_areaid == "")
            {
                MessageBox.Show("����ѡ����!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string s = this.txtBedno.Text.Trim();
                this.txtBedno.Text = "";
                this.m_mthLoadBed();
                if (this.txtBedno.Text.Trim() == "")
                {
                    this.txtBedno.Text = s;
                }
            }
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            this.label14.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            this.label14.ForeColor = Color.Black;
        }
    }
}
