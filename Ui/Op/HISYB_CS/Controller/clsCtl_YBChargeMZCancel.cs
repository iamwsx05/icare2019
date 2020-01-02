using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBChargeMZCancel: com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_YBChargeMZCancel()
        {
            objDomain = new clsDcl_YB();
        }
        public clsDcl_YB objDomain = null;

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBChargeMZCancel m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBChargeMZCancel)frmMDI_Child_Base_in;
        }
        #endregion

        public void m_mthGetYBInfo()
        {
            string strInvoiceNo = this.m_objViewer.txtfph.Text.Trim();
            DataTable dtPaitentInfo = null;
            DataTable dtFee = null;
            long lngRes = this.objDomain.m_lngGetPatientInfoByInvoice(strInvoiceNo, out dtPaitentInfo, out dtFee);
            if (lngRes < 0)
            {
                return;
            }
            if (dtPaitentInfo.Rows.Count <= 0)
            {
                return;
            }
            if (dtPaitentInfo.Rows.Count > 0)
            {
                DataRow drPatient = dtPaitentInfo.Rows[0];
                
                this.m_objViewer.lblxm.Text = drPatient["patientname_vchr"].ToString();
                this.m_objViewer.lblPSCODE.Text = drPatient["pscode"].ToString();
                this.m_objViewer.lblHPZCD.Text = drPatient["hpzcd"].ToString();
                this.m_objViewer.lblSTTLTRXNO.Text = drPatient["sttltrxno"].ToString();
                this.m_objViewer.lblxb.Text = drPatient["sex_chr"].ToString();
                this.m_objViewer.lblzlkh.Text = drPatient["medrecnbr"].ToString();
                
            }
            if (dtFee.Rows.Count > 0)
            {
                this.m_objViewer.dtItem.DataSource = dtFee;
            }
        }

        public long m_lngYBChargeCancel()
        {
            long lngRes = 0;
            string strPSCODE = this.m_objViewer.lblPSCODE.Text.Trim();
            string strHPZCD = this.m_objViewer.lblHPZCD.Text.Trim();
            string strSTTLTRXNO = this.m_objViewer.lblSTTLTRXNO.Text.Trim();
            string strPROCFLAG = "0";
            int intPtr = clsYBPublic_cs.CreateInstace();
            lngRes = clsYBPublic_cs.m_lngUserLoin(null,null,false);
            if (lngRes > 0)
            {
                //lngRes = clsYB_ts.m_lngFunc6000(intPtr, strPSCODE, "14", strHPZCD, strSTTLTRXNO);
                if (lngRes < 0)
                {
                    MessageBox.Show("���Ͳ���������Ϣʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
                do
                {
                    //lngRes = clsYB_ts.m_lngFunc6001(intPtr, strPSCODE, "14", ref strHPZCD, ref strSTTLTRXNO, ref strPROCFLAG);
                } while (strPROCFLAG == "0");
                if (strPROCFLAG == "2")//����6002ɾ��������Ϣ
                {
                    MessageBox.Show("ҽ���ǼǴ���ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
                else if (strPROCFLAG == "1")//����ɹ����˴�����HISϵͳ��Ϣ������6002ɾ��������Ϣ
                {
                    //this.objDomain.m_lngYBChargeCancel(strPSCODE, strHPZCD, strSTTLTRXNO);
                    return 1;
                }
                if (lngRes < 0)
                {
                    MessageBox.Show("��ѯ������Ϣʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            return 1;
        }

        #region ��ɽҽ���˷�ר��
        /// <summary>
        /// ��ɽҽ���˷�ר��
        /// </summary>
        /// <returns></returns>
        public long m_lngCSYBChargeCancel(string p_strInvoice, string p_strEmpNo)
        {
            long lngRes = 0;
            clsDGExtra_VO objDgextraVo = null;
            bool blRefund = false;
            //flag:1 �����������˷ѣ�0:����������
            int flag = 1;
            //�ж��Ƿ����������˷�
            if (p_strEmpNo.Contains("711014_"))
            {
                flag = 0;
            }
            lngRes = objDomain.m_lngCSYBChargeCancel(p_strInvoice, flag, out objDgextraVo, out  blRefund);
            if (lngRes > 0 && objDgextraVo != null)
            {
                if (blRefund)
                {
                    objDgextraVo.JBR = p_strEmpNo;
                    //�ȵ�¼
                    lngRes = clsYBPublic_cs.m_lngUserLoin(null, null, true);
                    if (lngRes > 0)
                    {
                        lngRes = clsYBPublic_cs.m_lngFunSP2007(objDgextraVo);
                        if (lngRes > 0)
                        {
                            lngRes = objDomain.m_lngUpdateCSYBChargeCancel(objDgextraVo);
                            p_strInvoice = null;
                            objDgextraVo = null;
                        }
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region ����ҽ��������Ϣ�޸�ר��
        /// <summary>
        /// ����ҽ��������Ϣ�޸�ר��
        /// </summary>
        /// <returns></returns>
        public long m_lngCSYBChargeInfoModify(string p_strInvoice, string p_strNewInvoice, string p_strEmpNo)
        {
            long lngRes = 0;
            clsDGExtra_VO objDgextraVo = null;
            p_strEmpNo = this.m_objViewer.LoginInfo.m_strEmpNo;
            bool blRefund = false;
            //flag:1 �����������˷ѣ�0:����������
            int flag = 1;
            //�ж��Ƿ����������˷�
            if (p_strEmpNo.Contains("711014_"))
            {
                flag = 0;
            }
            lngRes = objDomain.m_lngCSYBChargeCancel(p_strInvoice, flag, out objDgextraVo, out  blRefund);
            if (lngRes > 0 && objDgextraVo != null)
            {
                if (blRefund)
                {
                    objDgextraVo.JBR = p_strEmpNo;
                    objDgextraVo.FPHM = p_strNewInvoice;//�·�Ʊ��
                    //�ȵ�¼
                    lngRes = clsYBPublic_cs.m_lngUserLoin(null, null, true);
                    if (lngRes > 0)
                    {
                        lngRes = clsYBPublic_cs.m_lngFunSP2006(objDgextraVo);
                        if (lngRes > 0)
                        {
                            lngRes = objDomain.m_lngUpdateCSYBChargeInfo(objDgextraVo, "1");
                            p_strInvoice = null;
                            objDgextraVo = null;
                        }
                    }
                }
            }
            return lngRes;
        }
        #endregion
    }
}
