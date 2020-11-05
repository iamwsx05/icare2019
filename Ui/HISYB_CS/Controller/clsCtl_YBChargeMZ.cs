using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Collections.Generic;
using System.Threading;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBChargeMZ : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_YBChargeMZ()
        {
            objDomain = new clsDcl_YB();
        }
        public clsDcl_YB objDomain = null;

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBChargeMZ m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBChargeMZ)frmMDI_Child_Base_in;
        }
        #endregion
        /// <summary>
        /// �������VO
        /// </summary>
        clsDGMzjs_VO objDgmzjsVo = new clsDGMzjs_VO();
        clsDGExtra_VO objDgextraVo = new clsDGExtra_VO();
        /// <summary>
        /// �����������
        /// </summary>
        clsDGMzdyxs_VO objDGMzdyxsVo = new clsDGMzdyxs_VO();
        /// <summary>
        /// ������㷵�ؽ��vo
        /// </summary>
        clsDGMzjsfh_VO objDgmzjsfhVo = null;
        /// <summary>
        /// ������ϸ
        /// </summary>
        internal List<clsDGMzxmcs_VO> lstDGMzxmcsVo = new List<clsDGMzxmcs_VO>();

        /// <summary>
        /// �����籣��.��ά��
        /// </summary>
        string QRCode { get; set; }

        #region ��ʼ������,vo
        /// <summary>
        /// ��ʼ������,vo
        /// </summary>
        public void m_mthInit()
        {
            long lngRes = clsYBPublic_cs.m_lngUserLoin(null, null, true);//need modify ����ʱȷ���Ƿ�ÿ��ҵ�������Ҫ��¼
            if (lngRes > 0)
            {
                //��ʼ������
                this.m_objViewer.lblName.Text = this.m_objViewer.strPatientName;
                this.m_objViewer.lblZyh.Text = this.m_objViewer.strPatientCardNo;// strRecipeID;
                this.m_objViewer.lblJsxh.Text = string.Empty;
                lstDGMzxmcsVo = this.m_objViewer.lstDGMzxmcsVo;

                //��ʼ��������Ϣvo��objDgextraVo
                objDgextraVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                objDgextraVo.JBR = this.m_objViewer.strEmpNo;// ����Ա����
                //��ʼ���������vo
                objDGMzdyxsVo.GMSFHM = this.m_objViewer.strIDCardNo.ToUpper();
                objDGMzdyxsVo.JZLB = "";//Ĭ��Ϊ�գ��������ȫ����ѯ
                objDGMzdyxsVo.JZRQ = DateTime.Now.ToString("yyyyMMdd");
                objDGMzdyxsVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                objDGMzdyxsVo.JZYYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                //��ѯ�Ƿ�����ҽ������
                m_mthMzdyxs();
                //��ʼ������vo��objDgmzjsVo
                DataTable dtResult = null;
                lngRes = this.objDomain.m_lngGetDgmzjsdata(this.m_objViewer.strRecipeID, out dtResult);
                if (lngRes > 0)
                {
                    decimal decTotalEmp = this.m_objViewer.decTotal;
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        //need modify ��Ҫ��voֵ��������
                        objDgmzjsVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                        objDgmzjsVo.JZYYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                        objDgmzjsVo.ZYH = dtResult.Rows[0]["zyh"].ToString();//�Һű�ţ�û�йҺű���Ļ���ͳһ��������
                        objDgmzjsVo.CFH = this.m_objViewer.strRecipeID;
                        objDgmzjsVo.GMSFHM = dtResult.Rows[0]["gmsfhm"].ToString();//���֤��
                        objDgmzjsVo.JZRQ = (Convert.ToDateTime(dtResult.Rows[0]["jzrq"].ToString())).ToString("yyyyMMdd");//�������ڣ���ȡ��������
                        objDgmzjsVo.CYZD = dtResult.Rows[0]["cyzd"].ToString().Length == 0 ? "*" : dtResult.Rows[0]["cyzd"].ToString();//�������

                        objDgmzjsVo.CYBQDM = dtResult.Rows[0]["cybqdm"].ToString();//������ң���Ӷ�Ӧ����ȡt_ins_deptrel.insdeptcode_vchr�ֶ�
                        objDgmzjsVo.YYRYKS = dtResult.Rows[0]["yyryks"].ToString();//ҽԺ�Լ������Ŀ�������
                        objDgmzjsVo.YLFYZE = decTotalEmp.ToString("0.00"); ;
                        objDgmzjsVo.FPHM = this.m_objViewer.strInvNO;//��Ʊ����
                        objDgmzjsVo.LXDH = dtResult.Rows[0]["lxdh"].ToString();//��ϵ�绰
                        objDgmzjsVo.BZ = "";
                        objDgmzjsVo.JBR = this.m_objViewer.strEmpNo;//��ǰhis��¼Ա����
                        this.m_objViewer.strJZJLH = dtResult.Rows[0]["jzjlh"].ToString();
                        this.m_objViewer.strSDYWH = dtResult.Rows[0]["sdywh"].ToString();//ҽ�������
                        this.m_objViewer.decTotal = decTotalEmp;
                        this.m_objViewer.decYBTotal = dtResult.Rows[0]["ylfyze"].ToString().Length == 0 ? 0 : Convert.ToDecimal(dtResult.Rows[0]["ylfyze"].ToString());//ҽ���ܽ��
                        this.m_objViewer.decAcc = dtResult.Rows[0]["tczf"].ToString().Length == 0 ? 0 : Convert.ToDecimal(dtResult.Rows[0]["tczf"].ToString());//ͳ���
                        this.m_objViewer.decSub = dtResult.Rows[0]["grzfze"].ToString().Length == 0 ? 0 : Convert.ToDecimal(dtResult.Rows[0]["grzfze"].ToString());//�Ը�
                        this.m_objViewer.lblTotal.Text = this.m_objViewer.decYBTotal.ToString("0.00"); ;
                        this.m_objViewer.lblSub.Text = this.m_objViewer.decSub.ToString("0.00");
                        this.m_objViewer.lblAcc.Text = this.m_objViewer.decAcc.ToString("0.00");
                        this.m_objViewer.lblJsxh.Text = this.m_objViewer.strSDYWH;
                    }
                }
            }
        }
        #endregion

        #region �������ܴ����ж�

        Dictionary<int, string> dicJzlb = new Dictionary<int, string>();

        /// <summary>
        /// �������ܴ����ж�
        /// </summary>
        public void m_mthMzdyxs()
        {
            int intPtr = clsYBPublic_cs.CreateInstace();
            if (intPtr > 0)
            {
                string strJZLB = "";
                string strYY = "";
                string strDYXSBZ = "";
                List<string> lstJzlb = new List<string>();
                long lngRes = clsYBPublic_cs.m_lngFunSP1002(intPtr, objDGMzdyxsVo, objDgextraVo, ref strJZLB, ref strYY, ref strDYXSBZ, ref lstJzlb, this.m_objViewer.IsBirthInsurance, this.m_objViewer.IsCovi19,this.m_objViewer.IsFp);
                if (lngRes > 0)
                {
                    // �������-------------------------------------
                    //��������Դ 
                    DataTable OutDatable = new DataTable();
                    DataColumn ADC1 = new DataColumn("Name", typeof(string));
                    DataColumn ADC2 = new DataColumn("ID", typeof(string));
                    OutDatable.Columns.Add(ADC1);
                    OutDatable.Columns.Add(ADC2);
                    for (int i = 0; i < lstJzlb.Count; i++)
                    {
                        DataRow ADR = OutDatable.NewRow();
                        ADR[0] = clsYBPublic_cs.m_mthYBJzlbConvert(lstJzlb[i].ToString());
                        ADR[1] = lstJzlb[i].ToString();
                        OutDatable.Rows.Add(ADR);
                        this.m_objViewer.cmbJzlb.Properties.Items.Add(ADR[0].ToString());
                        this.dicJzlb.Add(i, ADR[1].ToString());
                    }
                    //���а�  
                    //this.m_objViewer.cmbJzlb.DisplayMember = "Name";//�ؼ���ʾ������  
                    //this.m_objViewer.cmbJzlb.ValueMember = "ID";//�ؼ�ֵ������  
                    //this.m_objViewer.cmbJzlb.DataSource = OutDatable;
                    //-------------------------------------------
                    // this.m_objViewer.lbljzlb.Tag = strJZLB;
                    this.m_objViewer.lbldyxsdz.Tag = strDYXSBZ;
                    this.m_objViewer.txtReason.Text = strYY;
                    if (strDYXSBZ == "0")
                    {
                        if (this.m_objViewer.IsBirthInsurance)
                        {
                            this.m_objViewer.lbldyxsdz.Text = "���������������ձ���";
                            MessageBox.Show("δ�����������յǼ�ȷ����Ϣ���籣���ɱ�����", "ϵͳ��ʾ");
                        }
                        else if (this.m_objViewer.IsCovi19)
                        {
                            this.m_objViewer.lbldyxsdz.Text = "���������������ﱣ�ձ���";
                            MessageBox.Show("�籣ϵͳ��ʾ���ò���Ŀǰ�����������������������ע�⣡", "ϵͳ��ʾ");
                        }
                        else if(this.m_objViewer.IsFp)
                        {
                            this.m_objViewer.lbldyxsdz.Text = "�������ܼƻ��������ﱣ�ձ���";
                            MessageBox.Show("�籣ϵͳ��ʾ���ò���Ŀǰ�������ܼƻ����������������ע�⣡", "ϵͳ��ʾ");
                        }
                        else
                        {
                            this.m_objViewer.lbldyxsdz.Text = "��������ҽ������";
                            MessageBox.Show("�籣ϵͳ��ʾ���ò���Ŀǰ��������ҽ����������ע�⣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        this.m_objViewer.btnUpload.Enabled = false;
                        this.m_objViewer.btnCharge.Enabled = false;
                        this.m_objViewer.btnOk.Enabled = false;
                        return;
                    }
                    else if (strDYXSBZ == "1")
                    {
                        this.m_objViewer.lbldyxsdz.Text = "����ҽ������";
                        this.m_objViewer.btnUpload.Enabled = true;
                        this.m_objViewer.btnCharge.Enabled = true;
                        this.m_objViewer.btnOk.Enabled = true;
                    }
                    #region �������

                    //if (strJZLB == "12")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "�ض�����";
                    //}
                    //else if (strJZLB == "32")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "���￵��";
                    //}
                    //else if (strJZLB == "51")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "��ͨ����";
                    //}
                    //else if (strJZLB == "52")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "��������";
                    //}
                    //else if (strJZLB == "53")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "ת������";
                    //}
                    //else if (strJZLB == "54")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "��������";
                    //}
                    //else if (strJZLB == "61")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "�ض�����";
                    //}
                    //else if (strJZLB == "62")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "�����ض�����ת��";
                    //}
                    //else if (strJZLB == "63")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "ҽԺ�ض�����(�۱�)";
                    //}
                    //else if (strJZLB == "64")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "���������ض�����";
                    //}
                    //else if (strJZLB == "81")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "����Ա���";
                    //}
                    //else if (strJZLB == "101")
                    //{
                    //    this.m_objViewer.lbljzlb.Text = "ҽѧ���";
                    //}
                    #endregion
                }
            }
        }
        #endregion

        #region ������ϸ�ϴ�
        /// <summary>
        /// ������ϸ�ϴ�
        /// </summary>
        public void m_mthRecipeUpload()
        {
            //need modify his��ȡ������ϸ
            //if (this.m_objViewer.lbljzlb.Tag == null || this.m_objViewer.lbljzlb.Tag.ToString() =="")
            //{
            //    MessageBox.Show("�ò���û�о��������˶��Ƿ���������ҽ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //} 
            int idx = this.m_objViewer.cmbJzlb.SelectedIndex;
            if (dicJzlb.ContainsKey(idx) == false)
            {
                MessageBox.Show("��ѡ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.m_objViewer.cmbJzlb.Focus();
                return;
            }
            string jzlb = dicJzlb[idx];

            //if (this.m_objViewer.cmbJzlb.SelectedValue.ToString() == null || this.m_objViewer.cmbJzlb.SelectedValue.ToString() == "")
            if (string.IsNullOrEmpty(jzlb))
            {
                MessageBox.Show("�ò���û�о��������˶��Ƿ���������ҽ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lstDGMzxmcsVo.Count > 0)
            {
                for (int i = 0; i < lstDGMzxmcsVo.Count; i++)
                {
                    if (this.m_objViewer.IsBirthInsurance && jzlb != "73") // ��������-��ǰ���
                        lstDGMzxmcsVo[i].JZLB = "73";
                    else if (this.m_objViewer.IsCovi19 && jzlb != "57") // 57 ��������
                        lstDGMzxmcsVo[i].JZLB = "57";
                    else if(this.m_objViewer.IsFp && jzlb != "79")  //�ƻ���������
                        lstDGMzxmcsVo[i].JZLB = "79";
                    else
                        lstDGMzxmcsVo[i].JZLB = jzlb; //this.m_objViewer.lbljzlb.Tag.ToString();
                }
                long lngRes = clsYBPublic_cs.m_lngFunSP2002(lstDGMzxmcsVo, objDgextraVo);
                if (lngRes < 0)
                {
                    MessageBox.Show("������ϸ�����ϴ����籣ϵͳʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("������ϸ�����ϴ��ɹ�����һ��������籣��������֤����ť����ҽ�����㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_objViewer.btnUpload.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("û�д�����ϸ�򴦷���ϸ����ʧ�ܣ���ر�����ҽ�����洰�������´򿪽��㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        public void m_mthMzybjs()
        {
            this.m_objViewer.lblJsxh.Text = string.Empty;
            long lngRes = clsYBPublic_cs.m_lngFunSP2004(objDgmzjsVo, out objDgmzjsfhVo);
            if (lngRes < 0)
            {
                MessageBox.Show("ҽ�����㴦��ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (objDgmzjsfhVo != null)
            {
                //���ϴ�����  -- ��Ϊ�ϴ���ʱ��û���ϴ������ѣ���������Ҫ���ϴ�����
                //objDgmzjsfhVo.YLFYZE = objDgmzjsfhVo.YLFYZE + this.m_objViewer.m_decCZF;
                //objDgmzjsfhVo.GRZFZE = objDgmzjsfhVo.GRZFZE + this.m_objViewer.m_decCZF;
                //decimal decTatolMny = this.m_objViewer.decTotal + this.m_objViewer.m_decCZF;
                //������ʾ
                decimal decYbTotal = 0;
                decimal.TryParse(objDgmzjsfhVo.YLFYZE.ToString(), out decYbTotal);
                this.m_objViewer.lblTotal.Text = decYbTotal.ToString("0.00");
                this.m_objViewer.decYBTotal = decYbTotal;
                this.m_objViewer.decAcc = clsYBPublic_cs.ConvertObjToDecimal(objDgmzjsfhVo.TCZF);
                this.m_objViewer.decSub = clsYBPublic_cs.ConvertObjToDecimal(objDgmzjsfhVo.GRZFZE);
                this.m_objViewer.lblSub.Text = clsYBPublic_cs.ConvertObjToDecimal(objDgmzjsfhVo.GRZFZE).ToString("0.00");//�Ը�
                this.m_objViewer.lblAcc.Text = clsYBPublic_cs.ConvertObjToDecimal(objDgmzjsfhVo.TCZF).ToString("0.00");//����
                this.m_objViewer.strSDYWH = objDgmzjsfhVo.SDYWH.ToString();//�������
                this.m_objViewer.strJZJLH = objDgmzjsfhVo.JZJLH.ToString();//�����¼��
                this.m_objViewer.m_decBCYLTCZF1 = objDgmzjsfhVo.BCYLTCZF1;//����1֧��
                this.m_objViewer.m_decBCYLTCZF2 = objDgmzjsfhVo.BCYLTCZF2;//����2֧��
                this.m_objViewer.m_decBCYLTCZF3 = objDgmzjsfhVo.BCYLTCZF3;//����3֧��
                this.m_objViewer.m_decBCYLTCZF4 = objDgmzjsfhVo.BCYLTCZF4;//����4֧��
                this.m_objViewer.m_decQTZHIFU = objDgmzjsfhVo.QTZHIFU;//����֧��
                this.m_objViewer.m_decYBJZFPJE = objDgmzjsfhVo.YBJZFPJE;//ҽ�����˽��
                this.m_objViewer.lblJsxh.Text = this.m_objViewer.strSDYWH;
                if (this.m_objViewer.decTotal != decYbTotal)
                {
                    MessageBox.Show("�籣�ܽ����HIS�ܽ�һ�£����飡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                //need modify ����ҽ����������his (�������ﴦ���Ŵ���)
                string strRegID = objDgmzjsfhVo.CFH;//need modify registerid.�������û�и��ֶΣ�����Ψһ��ʾ�ôξ����¼�ֶδ��棬����Ҳ��Ϊ�ա�
                lngRes = this.objDomain.m_lngSaveYBChargeReturn(strRegID, objDgmzjsfhVo);
                if (lngRes > 0)
                {
                    MessageBox.Show("ҽ������ɹ�����һ���������ɡ���ť���˳��ٽ���HIS���㣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }
        #endregion

        #region ����������ϸ�ϴ�
        /// <summary>
        /// ����������ϸ�ϴ�
        /// </summary>
        public void m_mthUndoRecipeUpload()
        {
            if (MessageBox.Show("�˲�����Ѵ˲����������ϴ��ķ���ȫ�������Ƿ�ȷ�ϴ˲�����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (objDgextraVo != null)
            {
                objDgextraVo.SDYWH = this.m_objViewer.strRecipeID;
                objDgextraVo.ZYH = this.m_objViewer.strPatientCardNo;
                long lngRes = clsYBPublic_cs.m_lngFunSP2003(objDgextraVo);
                if (lngRes < 0)
                {
                    MessageBox.Show("�������ϴ�������ϸ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("�������ϴ�������ϸ���ݳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_objViewer.btnUpload.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("û�д�����ϸ�򴦷���ϸ����ʧ�ܣ���ر�����ҽ�����洰�������´򿪽��㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ȡ���������
        /// <summary>
        /// ȡ���������
        /// </summary>
        public void m_mthUndoMzybjs()
        {
            if (MessageBox.Show("�˲�����Ѵ˲����������ϴ��ķ���ȫ�������Ƿ�ȷ�ϴ˲�����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            string strValue = null;
            if (objDgextraVo != null)
            {
                objDgextraVo.SDYWH = this.m_objViewer.strSDYWH;
                objDgextraVo.JZJLH = this.m_objViewer.strJZJLH;
                long lngRes = clsYBPublic_cs.m_lngFunSP2005(objDgextraVo);
                if (lngRes > 0)
                {
                    MessageBox.Show("ȡ��ҽ������ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            else
            {
                if (strValue != null)
                {
                    MessageBox.Show("ȡ��ҽ�����㴫ʧ�ܣ�������Ϣ��" + strValue.ToString(), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("ȡ��ҽ�����㴫ʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }
        #endregion

        #region �������ܴ����ж�
        /// <summary>
        /// �������ܴ����ж�
        /// </summary>
        public long m_lngPulicMzdyxs(string p_strIdcard, out clsPatient_VO clsPatient)
        {
            clsPatient = new clsPatient_VO();
            long lngRes = clsYBPublic_cs.m_lngUserLoin(null, null, true);//�����¼
            int intPtr = clsYBPublic_cs.CreateInstace();
            if (intPtr > 0)
            {
                string strJZLB = "";
                string strYY = "";
                string strDYXSBZ = "";
                List<string> lstJzlb = new List<string>();
                objDGMzdyxsVo.GMSFHM = p_strIdcard;
                objDGMzdyxsVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                objDGMzdyxsVo.JZYYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                objDgextraVo.JBR = "001";
                lngRes = clsYBPublic_cs.m_lngFunSP1002(intPtr, objDGMzdyxsVo, objDgextraVo, ref strJZLB, ref strYY, ref strDYXSBZ, ref lstJzlb, this.m_objViewer.IsBirthInsurance, this.m_objViewer.IsCovi19, this.m_objViewer.IsFp);
                if (lngRes > 0)
                {
                    if (strDYXSBZ == "0")
                    {
                        //this.m_objViewer.lbldyxsdz.Text = "��������ҽ������";
                        //MessageBox.Show("�籣ϵͳ��ʾ���ò���Ŀǰ��������ҽ����������ע�⣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        //this.m_objViewer.btnUpload.Enabled = false;
                        //this.m_objViewer.btnCharge.Enabled = false;
                        //this.m_objViewer.btnOk.Enabled = false;
                        //return;
                    }
                    else if (strDYXSBZ == "1")
                    {
                        //this.m_objViewer.lbldyxsdz.Text = "����ҽ������";
                        //this.m_objViewer.btnUpload.Enabled = true;
                        //this.m_objViewer.btnCharge.Enabled = true;
                        //this.m_objViewer.btnOk.Enabled = true;
                    }
                    #region �������
                    if (strJZLB == "0")
                    {
                        clsPatient.strPatType = "�Է�";
                    }
                    else if (strJZLB == "12")
                    {
                        clsPatient.strPatType = "�ض�����";
                    }
                    else if (strJZLB == "32")
                    {
                        clsPatient.strPatType = "���￵��";
                    }
                    else if (strJZLB == "51")
                    {
                        clsPatient.strPatType = "��ͨ����";
                    }
                    else if (strJZLB == "52")
                    {
                        clsPatient.strPatType = "��������";
                    }
                    else if (strJZLB == "53")
                    {
                        clsPatient.strPatType = "ת������";
                    }
                    else if (strJZLB == "54")
                    {
                        clsPatient.strPatType = "��������";
                    }
                    else if(strJZLB == "57")
                    {
                        clsPatient.strPatType = "��������";
                    }
                    else if (strJZLB == "61")
                    {
                        clsPatient.strPatType = "�ض�����";
                    }
                    else if (strJZLB == "62")
                    {
                        clsPatient.strPatType = "�����ض�����ת��";
                    }
                    else if (strJZLB == "63")
                    {
                        clsPatient.strPatType = "ҽԺ�ض�����(�۱�)";
                    }
                    else if (strJZLB == "64")
                    {
                        clsPatient.strPatType = "���������ض�����";
                    }
                    else if(strJZLB == "79")
                    {
                        clsPatient.strPatType = "�ƻ���������";
                    }
                    else if (strJZLB == "81")
                    {
                        clsPatient.strPatType = "����Ա���";
                    }
                    else if (strJZLB == "101")
                    {
                        clsPatient.strPatType = "ҽѧ���";
                    }
                    #endregion
                }
            }
            return lngRes;
        }
        #endregion

        #region �籣��������֤
        /// <summary>
        /// �籣��������֤
        /// </summary>
        public void m_mthCheckPsw()
        {
            try
            {
                string strRetMsg = string.Empty;
                objDgextraVo.GMSFHM = this.m_objViewer.strIDCardNo;
                objDgextraVo.ZYLB = "1";

                int idx = this.m_objViewer.cmbJzlb.SelectedIndex;
                if (dicJzlb.ContainsKey(idx) == false)
                {
                    MessageBox.Show("��ѡ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_objViewer.cmbJzlb.Focus();
                    return;
                }
                string jzlb = dicJzlb[idx];
                //if (this.m_objViewer.cmbJzlb.SelectedValue != null)
                if (!string.IsNullOrEmpty(jzlb))
                {
                    if (this.m_objViewer.IsBirthInsurance && jzlb != "73") // ��������-��ǰ���
                        objDgextraVo.JZLB = "73";
                    else if (this.m_objViewer.IsCovi19 && jzlb != "57") // 57 ��������
                        objDgextraVo.JZLB = "57";
                    else if(this.m_objViewer.IsFp && jzlb != "79")     // 79 �ƻ���������
                    {
                        objDgextraVo.JZLB = "79";
                    }
                    else
                        objDgextraVo.JZLB = jzlb;
                }
                if(objDgextraVo.JZLB == "79")
                    objDgextraVo.ZYLB = "4"; 
                long lngRes = clsYBPublic_cs.m_lngFunSP5001(1, objDgextraVo, DateTime.Now, (this.m_objViewer.rdoEk.Checked ? this.QRCode : ""));
            }
            catch (Exception objEx)
            {
                new com.digitalwave.Utility.clsLogText().LogError(objEx);
            }
        }
        #endregion

        #region ReadQRcode
        /// <summary>
        /// ReadQRcode
        /// </summary>
        internal void ReadQRcode()
        {
            // 010105 �շ�
            frmReadQRcode frmQR = new frmReadQRcode(this.m_objViewer.strIDCardNo.Trim().ToUpper(), this.m_objViewer.strEmpNo,
                                                      clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne"), "010105");
            if (frmQR.ShowDialog() == DialogResult.OK)
            {
                this.QRCode = frmQR.QRCode;       // �����籣����ά��
                this.m_objViewer.rdoEk.Checked = true;
            }
        }
        #endregion
    }
}
