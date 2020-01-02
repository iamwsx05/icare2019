using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBPatientInfo : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBPatientInfo m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBPatientInfo)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void m_mthInit()
        {
            //ҽԺ������ע����סԺ��¼
            string strUser = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
            string strPwd = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
            long lngRes = clsYBPublic_cs.m_lngUserLoin(strUser, strPwd, false);
            if (lngRes < 0)
            {
                MessageBox.Show("��ʼ��ʧ�ܣ������´򿪣�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        #endregion

        public clsCtl_YBPatientInfo()
        {
            objDomain = new clsDcl_YB();
        }
        public clsDcl_YB objDomain = null;

        #region ��ѯҽ�����˻�����Ϣ
        /// <summary>
        /// ��ѯҽ�����˻�����Ϣ
        /// </summary>
        public void m_mthGetPatientInfo()
        {
            if (this.m_objViewer.strFlag == "1")
            {
                this.m_objViewer.txtInpatientid.Text = this.m_objViewer.txtInpatientid.Text.ToString().PadLeft(10, '0');
            }
            string InpatientID = this.m_objViewer.txtInpatientid.Text.ToString().Trim();
            ClearData();
            if (string.IsNullOrEmpty(InpatientID))
            {
                MessageBox.Show("��������Ϊ�գ������룡", "iCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dtResult = new DataTable();
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
            long lngRes = this.objDomain.m_lngGetPatientInfo(InpatientID, this.m_objViewer.strFlag, out dtResult);
            if (lngRes > 0)
            {
                if (dtResult.Rows.Count > 0)
                {
                    m_objItem.GMSFHM = dtResult.Rows[0]["idcard_chr"].ToString().Trim();
                    m_objItem.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne"); //ҽԺ���
                    m_objItem.CBDTCQBM = this.m_objViewer.txtCbdtc.Text.ToString().Trim();   //need modify
                    m_objItem.LXDH = this.m_objViewer.LoginInfo.m_strEmpNo;
                    lngRes = clsYBPublic_cs.m_lngFunSP1201(m_objItem, out m_objPatientInfo, out m_objJXzlxx, out m_objYDryxx, out m_objZYxx, out m_objZJzyxx);
                    if (lngRes > 0)
                    {
                        #region ���˻�����Ϣ��ֵ
                        if (m_objPatientInfo != null)
                        {
                            this.m_objViewer.txtIDCard.Text = m_objItem.GMSFHM;
                            this.m_objViewer.txtName.Text = m_objPatientInfo.XM;
                            this.m_objViewer.txtSex.Text = dtResult.Rows[0]["sex_chr"].ToString();
                            this.m_objViewer.txtBirthDay.Text = m_objPatientInfo.CSNY;
                            this.m_objViewer.dtpInDate.Value = Convert.ToDateTime(dtResult.Rows[0]["rysj"].ToString());
                            TimeSpan t = new TimeSpan();
                            if (string.IsNullOrEmpty(dtResult.Rows[0]["cysj"].ToString()))
                            {
                                t = DateTime.Now - Convert.ToDateTime(dtResult.Rows[0]["rysj"].ToString());
                            }
                            else
                            {
                                t = Convert.ToDateTime(dtResult.Rows[0]["cysj"].ToString()) - Convert.ToDateTime(dtResult.Rows[0]["rysj"].ToString());
                            }
                            this.m_objViewer.txtRYTS.Text = t.Days.ToString();
                            this.m_objViewer.txtSBJG.Text = m_objPatientInfo.XTJGDM;
                            if (m_objPatientInfo.JSFS == "1")
                            {
                                this.m_objViewer.txtJSFS.Text = "��������";
                            }
                            else if (m_objPatientInfo.JSFS == "3")
                            {
                                this.m_objViewer.txtJSFS.Text = "ȫ���Է�";
                            }
                            else
                            {
                                this.m_objViewer.txtJSFS.Text = m_objPatientInfo.JSFS;
                            }
                            switch (m_objPatientInfo.RYLB)
                            {
                                case "1":
                                    this.m_objViewer.txtType.Text = "��ְ";
                                    break;
                                case "2":
                                    this.m_objViewer.txtType.Text = "����";
                                    break;
                                case "3":
                                    this.m_objViewer.txtType.Text = "����";
                                    break;
                                case "4":
                                    this.m_objViewer.txtType.Text = "����ǰ�Ϲ���";
                                    break;
                                case "5":
                                    this.m_objViewer.txtType.Text = "��������";
                                    break;
                                case "6":
                                    this.m_objViewer.txtType.Text = "���⹤������";
                                    break;
                                case "7":
                                    this.m_objViewer.txtType.Text = "����ǰ����";
                                    break;
                                case "8":
                                    this.m_objViewer.txtType.Text = "��Ů";
                                    break;
                                default:
                                    this.m_objViewer.txtType.Text = "";
                                    break;
                            }
                            switch (m_objPatientInfo.YLDYLX)
                            {
                                case "1":
                                    this.m_objViewer.txtYLDYLB.Text = "��ְ����";
                                    break;
                                case "2":
                                    this.m_objViewer.txtYLDYLB.Text = "���ݴ���";
                                    break;
                                case "9":
                                    this.m_objViewer.txtYLDYLB.Text = "�޴���";
                                    break;
                                default:
                                    this.m_objViewer.txtYLDYLB.Text = "";
                                    break;
                            }

                            switch (m_objPatientInfo.GWYLB)
                            {
                                case "1":
                                    this.m_objViewer.txtGWYLB.Text = "���ҹ���Ա";
                                    break;
                                case "2":
                                    this.m_objViewer.txtGWYLB.Text = "���չ���Ա";
                                    break;
                                case "3":
                                    this.m_objViewer.txtGWYLB.Text = "���չ���Ա";
                                    break;
                                case "4":
                                    this.m_objViewer.txtGWYLB.Text = "���չ���Ա";
                                    break;
                                default:
                                    this.m_objViewer.txtGWYLB.Text = "";
                                    break;
                            }

                            switch (m_objPatientInfo.JBYILCBFS)
                            {
                                case "310":
                                    this.m_objViewer.txtJBYLCBFS.Text = "�ۺϻ���ҽ�Ʊ���";
                                    break;
                                case "311":
                                    this.m_objViewer.txtJBYLCBFS.Text = "סԺ����ҽ�Ʊ��գ���ҵ��";
                                    break;
                                case "315":
                                    this.m_objViewer.txtJBYLCBFS.Text = "��ŮסԺ����ҽ�Ʊ���";
                                    break;
                                case "390":
                                    this.m_objViewer.txtJBYLCBFS.Text = "סԺ����ҽ�Ʊ��գ���������";
                                    break;
                                case "999":
                                    this.m_objViewer.txtJBYLCBFS.Text = "δ�α�";
                                    break;
                                default:
                                    this.m_objViewer.txtJBYLCBFS.Text = "";
                                    break;
                            }

                            this.m_objViewer.txtJBYILKBYE.Text = m_objPatientInfo.JBYILKBYE;
                            switch (m_objPatientInfo.BCYILCBZT)
                            {
                                case "0":
                                    this.m_objViewer.txtBCYLCBZT.Text = "δ�α�";
                                    break;
                                case "1":
                                    this.m_objViewer.txtBCYLCBZT.Text = "�����α�";
                                    break;
                                case "2":
                                    this.m_objViewer.txtBCYLCBZT.Text = "��ͣ�α�";
                                    break;
                                case "4":
                                    this.m_objViewer.txtBCYLCBZT.Text = "��ֹ�α�";
                                    break;
                                default:
                                    this.m_objViewer.txtBCYLCBZT.Text = "";
                                    break;
                            }

                            this.m_objViewer.txtBCYLKBYE.Text = m_objPatientInfo.BCYILKBYE;
                            switch (m_objPatientInfo.GSCBZT)
                            {
                                case "0":
                                    this.m_objViewer.txtGSCBZT.Text = "δ�α�";
                                    break;
                                case "1":
                                    this.m_objViewer.txtGSCBZT.Text = "�����α�";
                                    break;
                                case "2":
                                    this.m_objViewer.txtGSCBZT.Text = "��ͣ�α�";
                                    break;
                                case "4":
                                    this.m_objViewer.txtGSCBZT.Text = "��ֹ�α�";
                                    break;
                                default:
                                    this.m_objViewer.txtGSCBZT.Text = "";
                                    break;
                            }
                            switch (m_objPatientInfo.GWYBZLB)
                            {
                                case "320":
                                    this.m_objViewer.txtGWYBZLB.Text = "����Աҽ�Ʋ���";
                                    break;
                                case "321":
                                    this.m_objViewer.txtGWYBZLB.Text = "����Ա���ﲹ��";
                                    break;
                                case "340":
                                    this.m_objViewer.txtGWYBZLB.Text = "������Ա����Աҽ�Ʋ���";
                                    break;
                                case "999":
                                    this.m_objViewer.txtGWYBZLB.Text = "δ�α�";
                                    break;
                                default:
                                    this.m_objViewer.txtGWYBZLB.Text = "";
                                    break;
                            }
                            switch (m_objPatientInfo.GSBXXSBZ)
                            {
                                case "0":
                                    this.m_objViewer.txtGSBXXSBZ.Text = "��������";
                                    break;
                                case "1":
                                    this.m_objViewer.txtGWYBZLB.Text = "������";
                                    break;
                                default:
                                    this.m_objViewer.txtGWYBZLB.Text = "";
                                    break;
                            }
                            switch (m_objPatientInfo.MZYLJZDX)
                            {
                                case "0":
                                    this.m_objViewer.txtMZYLJZDX.Text = "��";
                                    break;
                                case "1":
                                    this.m_objViewer.txtGWYBZLB.Text = "�����ͱ�ҽ�ƾ�������";
                                    break;
                                default:
                                    this.m_objViewer.txtGWYBZLB.Text = "";
                                    break;
                            }
                            this.m_objViewer.txtZFYY.Text = m_objPatientInfo.ZFYY;
                            this.m_objViewer.txtRYKS.Text = dtResult.Rows[0]["deptname_vchr"].ToString();
                            this.m_objViewer.txtRYZD.Text = dtResult.Rows[0]["DIAGNOSEID_CHR"].ToString();
                            this.m_objViewer.txtZZYS.Text = dtResult.Rows[0]["lastname_vchr"].ToString();
                            this.m_objViewer.txtBCMC.Text = dtResult.Rows[0]["code_chr"].ToString();
                            this.m_objViewer.txtTel.Text = m_objPatientInfo.LXDH;
                            this.m_objViewer.txtCYKS.Text = "";//need modify
                            this.m_objViewer.txtCYZD.Text = dtResult.Rows[0]["OUTDIAGNOSE_VCHR"].ToString();
                            if (m_objPatientInfo.SBBZ.ToString() == "0")
                            {
                                this.m_objViewer.txtSBBZ.Text = "����Ǽ��籣";
                            }
                            else
                            {
                                this.m_objViewer.txtSBBZ.Text = "��Ǽ��籣";
                            }
                            this.m_objViewer.txtbcyl1zt.Text = m_objPatientInfo.BCYILCBZT1;
                            this.m_objViewer.txtbcyl2zt.Text = m_objPatientInfo.BCYILCBZT2;
                            this.m_objViewer.txtbcyl3zt.Text = m_objPatientInfo.BCYILCBZT3;
                            this.m_objViewer.txtbcyl4zt.Text = m_objPatientInfo.BCYILCBZT4;
                            this.m_objViewer.txtbcyl1je.Text = m_objPatientInfo.BCYILKBYE1;
                            this.m_objViewer.txtbcyl2je.Text = m_objPatientInfo.BCYILKBYE2;
                            this.m_objViewer.txtbcyl3je.Text = m_objPatientInfo.BCYILKBYE3;
                            this.m_objViewer.txtbcyl4je.Text = m_objPatientInfo.BCYILKBYE4;
                        }
                        #endregion

                        #region ���סԺ��Ϣ��ֵ
                        if (m_objZJzyxx != null)
                        {
                            int Index = 0;
                            foreach (clsDGZjzyxx_VO objTemp in m_objZJzyxx)
                            {
                                Index = this.m_objViewer.dgvZJZYXX.Rows.Add();
                                this.m_objViewer.dgvZJZYXX["GMSFHM", Index].Value = objTemp.GMSFHM;
                                this.m_objViewer.dgvZJZYXX["JZJLH", Index].Value = objTemp.JZJLH;
                                this.m_objViewer.dgvZJZYXX["XM", Index].Value = objTemp.XM;
                                this.m_objViewer.dgvZJZYXX["JZYYMC", Index].Value = objTemp.JZYYMC;
                                this.m_objViewer.dgvZJZYXX["ZYH", Index].Value = objTemp.ZYH;
                                this.m_objViewer.dgvZJZYXX["CYZD", Index].Value = objTemp.CYZD;
                                if (objTemp.ZYLB.ToString() == "1")
                                {
                                    this.m_objViewer.dgvZJZYXX["ZYLB", Index].Value = "ҽ��סԺ";
                                }
                                else if (objTemp.ZYLB.ToString() == "2")
                                {
                                    this.m_objViewer.dgvZJZYXX["ZYLB", Index].Value = "����סԺ";
                                }
                                else
                                {
                                    this.m_objViewer.dgvZJZYXX["ZYLB", Index].Value = "";
                                }
                                switch (objTemp.JZLB)
                                {
                                    case "11":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��ͨסԺ";
                                        break;
                                    case "12":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�ض�����";
                                        break;
                                    case "13":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��ͥ����";
                                        break;
                                    case "22":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "����סԺ";
                                        break;
                                    case "23":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "תԺסԺ";
                                        break;
                                    case "24":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�����������";
                                        break;
                                    case "31":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "סԺ����";
                                        break;
                                    case "32":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "���￵��";
                                        break;
                                    case "33":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��������";
                                        break;
                                    case "34":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�Ͷ���������";
                                        break;
                                    case "41":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�������𼲲�סԺ";
                                        break;
                                    case "51":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��ͨ����";
                                        break;
                                    case "52":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��������";
                                        break;
                                    case "53":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "ת������";
                                        break;
                                    case "54":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��������";
                                        break;
                                    case "61":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�ض�����";
                                        break;
                                    case "62":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�����ض�����ת��";
                                        break;
                                    case "63":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "ҽԺ�ض�����(�۱�)";
                                        break;
                                    case "64":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "���������ض�����";
                                        break;
                                    case "71":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "����";
                                        break;
                                    case "72":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "�����ʸ�������";
                                        break;
                                    case "81":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "����Ա���";
                                        break;
                                    case "91":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "��������";
                                        break;
                                    case "101":
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "ҽѧ���";
                                        break;
                                    default:
                                        this.m_objViewer.dgvZJZYXX["JZLB", Index].Value = "";
                                        break;
                                }
                                this.m_objViewer.dgvZJZYXX["RYRQ", Index].Value = objTemp.RYRQ;
                                this.m_objViewer.dgvZJZYXX["CYRQ", Index].Value = objTemp.CYRQ;
                                this.m_objViewer.dgvZJZYXX["ZYKS", Index].Value = objTemp.ZYKS;
                                this.m_objViewer.dgvZJZYXX["YYRYKS", Index].Value = objTemp.YYRYKS;
                                this.m_objViewer.dgvZJZYXX["CYKS", Index].Value = objTemp.CYKS;
                                this.m_objViewer.dgvZJZYXX["RYZD", Index].Value = objTemp.RYZD;
                                this.m_objViewer.dgvZJZYXX["CYYY", Index].Value = objTemp.CYYY;
                            }
                        }
                        #endregion

                        #region תԺ��Ϣ��ֵ
                        if (m_objZYxx != null)
                        {
                            int Index = 0;
                            foreach (clsDGZyxx_VO objTemp in m_objZYxx)
                            {
                                Index = this.m_objViewer.dgvZYXX.Rows.Add();
                                this.m_objViewer.dgvZYXX["ZRYYBH", Index].Value = objTemp.ZRYYBH;
                                this.m_objViewer.dgvZYXX["XM1", Index].Value = objTemp.XM; ;
                                this.m_objViewer.dgvZYXX["SPYWH", Index].Value = objTemp.SPYWH;
                                this.m_objViewer.dgvZYXX["ZRYYMC", Index].Value = objTemp.ZRYYMC;
                                this.m_objViewer.dgvZYXX["ZCYYMC", Index].Value = objTemp.ZCYYMC;
                                this.m_objViewer.dgvZYXX["ZCKS", Index].Value = objTemp.ZCKS;
                                this.m_objViewer.dgvZYXX["ZCRQ", Index].Value = objTemp.ZCRQ;
                                this.m_objViewer.dgvZYXX["ZYLB1", Index].Value = objTemp.ZYLB;
                                this.m_objViewer.dgvZYXX["ZYZD", Index].Value = objTemp.ZYZD;
                                this.m_objViewer.dgvZYXX["JZRQ", Index].Value = objTemp.JZRQ;
                                this.m_objViewer.dgvZYXX["ZZYY", Index].Value = objTemp.ZZYY;
                            }
                        }
                        #endregion

                        #region �����Ա��Ϣ
                        if (m_objYDryxx != null)
                        {
                            int Index = 0;
                            foreach (clsDGYdryxx_VO objTemp in m_objYDryxx)
                            {
                                Index = this.m_objViewer.dgvYDRYXX.Rows.Add();
                                this.m_objViewer.dgvYDRYXX["XM2", Index].Value = objTemp.XM;
                                this.m_objViewer.dgvYDRYXX["SPYWH1", Index].Value = objTemp.SPYWH;
                                this.m_objViewer.dgvYDRYXX["XDYJYYBH", Index].Value = objTemp.XDYJYYBH;
                                this.m_objViewer.dgvYDRYXX["XDYJYYMC", Index].Value = objTemp.XDYJYYMC;
                                this.m_objViewer.dgvYDRYXX["XDEJYYBH", Index].Value = objTemp.XDEJYYBH;
                                this.m_objViewer.dgvYDRYXX["XDEJYYMC", Index].Value = objTemp.XDEJYYMC;
                                this.m_objViewer.dgvYDRYXX["XDSJYYBH", Index].Value = objTemp.XDSJYYBH;
                                this.m_objViewer.dgvYDRYXX["XDSJYYMC", Index].Value = objTemp.XDSJYYMC;
                                this.m_objViewer.dgvYDRYXX["SHENXRQ", Index].Value = objTemp.SHENXRQ;
                                this.m_objViewer.dgvYDRYXX["ZZRQ", Index].Value = objTemp.ZZRQ;
                            }
                        }
                        #endregion

                        #region ����������Ϣ
                        if (m_objJXzlxx != null)
                        {
                            int Index = 0;
                            foreach (clsDGJxzlxx_VO objTemp in m_objJXzlxx)
                            {
                                Index = this.m_objViewer.dgvJXZLXX.Rows.Add();
                                this.m_objViewer.dgvJXZLXX["XM3", Index].Value = objTemp.XM;
                                this.m_objViewer.dgvJXZLXX["SPYWH2", Index].Value = objTemp.SPYWH;
                                this.m_objViewer.dgvJXZLXX["ZYZDMC", Index].Value = objTemp.ZYZDMC;
                                this.m_objViewer.dgvJXZLXX["SCJZYYMC", Index].Value = objTemp.SCJZYYMC;
                                this.m_objViewer.dgvJXZLXX["SCJZKSSJ", Index].Value = objTemp.SCJZKSSJ;
                                this.m_objViewer.dgvJXZLXX["SCJZZZSJ", Index].Value = objTemp.SCJZZZSJ;
                                this.m_objViewer.dgvJXZLXX["JXZLYYMC", Index].Value = objTemp.JXZLYYMC;
                                this.m_objViewer.dgvJXZLXX["JXZLKSSJ", Index].Value = objTemp.JXZLKSSJ;
                                this.m_objViewer.dgvJXZLXX["JXZLZZSJ", Index].Value = objTemp.JXZLZZSJ;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("û�иò�����Ϣ�����������Ƿ���ȷ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            #region ������Ϣ���
            foreach (Control c in this.m_objViewer.gpbPaitntInfo.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            #endregion

            #region ���ݼ����
            this.m_objViewer.dgvJXZLXX.Rows.Clear();
            this.m_objViewer.dgvYDRYXX.Rows.Clear();
            this.m_objViewer.dgvZJZYXX.Rows.Clear();
            this.m_objViewer.dgvZYXX.Rows.Clear();
            #endregion
        }
        #endregion
    }
}
