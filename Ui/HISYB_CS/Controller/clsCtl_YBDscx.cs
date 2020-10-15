using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    class clsCtl_YBDscx : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_YBDscx()
        {

        }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBDscx m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBDscx)frmMDI_Child_Base_in;
        }
        #endregion

        /// <summary>
        /// ҽ��������ѯVO
        /// </summary>
        clsDGYBDscx_VO objDgybdscxVo = null;
        /// <summary>
        /// ��¼����
        /// </summary>
        string strYBPass = string.Empty;

        public void m_mthInit()
        {
            objDgybdscxVo = new clsDGYBDscx_VO();
            objDgybdscxVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");//Ĭ�ϲ�����
            strYBPass = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "PASSWORDMZ", "AnyOne");
            if (this.m_objViewer.cboYWLB.SelectedIndex == 0)
            {
                objDgybdscxVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                strYBPass = clsYBPublic_cs.m_strReadXML("DGCSMZYB", "PASSWORDMZ", "AnyOne");
            }
            else if (this.m_objViewer.cboYWLB.SelectedIndex == 1)
            {
                objDgybdscxVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                strYBPass = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
            }
            //�շ�Ա����
            objDgybdscxVo.SFYBH = this.m_objViewer.txtSFYBH.Text.Trim();//need modify,�˽����ܷ�ȡ���շ�Ա���룬�Ƿ������ѯȫ���շ�Ա����ϸ���Ƿ���Ҫ�ڽ�����¼��
            objDgybdscxVo.YWLB = this.m_objViewer.cboYWLB.Text.Trim().Split('-')[0].ToString();
            objDgybdscxVo.KSRQ = this.m_objViewer.dtmStart.Value.ToString("yyyyMMdd");
            objDgybdscxVo.ZZRQ = this.m_objViewer.dtmEnd.Value.ToString("yyyyMMdd");
        }

        public void m_mthQuery()
        {
            //��ʼ������ֵ
            m_mthInit();
            long lngRes = clsYBPublic_cs.m_lngUserLoin(objDgybdscxVo.YYBH, strYBPass, false);
            if (lngRes < 0)
            {
                MessageBox.Show("��¼�籣ϵͳʧ�ܣ���رս�������ԣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            //��Ϊ�����������ϴ󣬡���ʼ���ڡ�������ֹ���ڡ������ڼ�����ɳ���һ����
            if (this.m_objViewer.dtmStart.Value.AddMonths(1) < this.m_objViewer.dtmEnd.Value)
            {
                MessageBox.Show("��ѯ���ڷ�Χ���ܳ���1���£�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (this.m_objViewer.dtmStart.Value > this.m_objViewer.dtmEnd.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (this.m_objViewer.cboYWLB.SelectedIndex < 0 || string.IsNullOrEmpty(this.m_objViewer.cboYWLB.Text.Trim()))
            {
                MessageBox.Show("��ѡ����ȷҵ�����ͣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (objDgybdscxVo == null)
            {
                return;
            }
            clsDGYBDscxfh_VO objDgybdscxfhVo = null;
            List<clsDGYBDscxYYDSXX_VO> lstDgybdscxYYDSXXVo = null;
            lngRes = clsYBPublic_cs.m_lngFunSP1208(objDgybdscxVo, out objDgybdscxfhVo, out lstDgybdscxYYDSXXVo);
            if (lngRes > 0)
            {
                if (objDgybdscxfhVo == null || lstDgybdscxYYDSXXVo == null || lstDgybdscxYYDSXXVo.Count == 0)
                {
                    MessageBox.Show("��ʱ���û�ж������ݣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                //���渳ֵ
                this.m_objViewer.txtZYZJE.Text = objDgybdscxfhVo.ZYZJE_HJ;
                this.m_objViewer.txtZRS.Text = objDgybdscxfhVo.ZRS;
                this.m_objViewer.txtSBZFJE_HJ.Text = objDgybdscxfhVo.SBZFJE_HJ;
                this.m_objViewer.txtYLBZ_HJ.Text = objDgybdscxfhVo.YLBZ_HJ;
                this.m_objViewer.txtJZJE_HJ.Text = objDgybdscxfhVo.JZJE_HJ;
                this.m_objViewer.txtDBYLJZJ_HJ.Text = objDgybdscxfhVo.DBYLJZJ_HJ;
                this.m_objViewer.txtGRZFEIJE_HJ.Text = objDgybdscxfhVo.GRZFEIJE_HJ;
                try
                {
                    this.m_objViewer.txtSBBXJE_HJ.Text = (decimal.Parse(objDgybdscxfhVo.SBZFJE_HJ) + decimal.Parse(objDgybdscxfhVo.YLBZ_HJ) + decimal.Parse(objDgybdscxfhVo.JZJE_HJ) + decimal.Parse(objDgybdscxfhVo.DBYLJZJ_HJ)).ToString();
                }
                catch (Exception)
                {
                    throw;
                }
                //datagridview��ֵ
                int intCount = lstDgybdscxYYDSXXVo.Count;
                clsDGYBDscxYYDSXX_VO objDgybdscxYYDSXXTmpVo = null;
                string strJZLB = string.Empty;
                this.m_objViewer.dgvDetail.Rows.Clear();
                int intRow = 0;
                for (int i = 0; i < intCount; i++)
                {
                    objDgybdscxYYDSXXTmpVo = lstDgybdscxYYDSXXVo[i];
                    intRow = this.m_objViewer.dgvDetail.Rows.Add();
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells[0].Value = (i+1).ToString(); //"colXH"
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells[1].Value = objDgybdscxYYDSXXTmpVo.GMSFHM; //"colGMSFHM"
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colXM"].Value = objDgybdscxYYDSXXTmpVo.XM;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJSRQ"].Value = objDgybdscxYYDSXXTmpVo.JSRQ;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJZJLH"].Value = objDgybdscxYYDSXXTmpVo.JZJLH;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJSYWH"].Value = objDgybdscxYYDSXXTmpVo.JSYWH;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colYWLB"].Value = objDgybdscxYYDSXXTmpVo.YWLB == "1" ? "����" : "סԺ";
                    #region �������
                    switch (objDgybdscxYYDSXXTmpVo.JZLB)
                    {
                        case "11":
                            strJZLB = "��ͨסԺ";
                            break;
                        case "12":
                            strJZLB = "�ض�����";
                            break;
                        case "13":
                            strJZLB = "��ͥ����";
                            break;
                        case "22":
                            strJZLB = "����סԺ";
                            break;
                        case "23":
                            strJZLB = "תԺסԺ";
                            break;
                        case "24":
                            strJZLB = "�����������";
                            break;
                        case "31":
                            strJZLB = "סԺ����";
                            break;
                        case "32":
                            strJZLB = "���￵��";
                            break;
                        case "33":
                            strJZLB = "��������";
                            break;
                        case "34":
                            strJZLB = "�Ͷ���������";
                            break;
                        case "41":
                            strJZLB = "�������𼲲�סԺ";
                            break;
                        case "51":
                            strJZLB = "��ͨ����";
                            break;
                        case "52":
                            strJZLB = "��������";
                            break;
                        case "53":
                            strJZLB = "ת������";
                            break;
                        case "54":
                            strJZLB = "��������";
                            break;
                        case "57":
                            strJZLB = "��������";
                            break;
                        case "61":
                            strJZLB = "�ض�����";
                            break;
                        case "62":
                            strJZLB = "�����ض�����ת��";
                            break;
                        case "63":
                            strJZLB = "ҽԺ�ض�����(�۱�)";
                            break;
                        case "64":
                            strJZLB = "���������ض�����";
                            break;
                        case "71":
                            strJZLB = "����";
                            break;
                        case "72":
                            strJZLB = "�����ʸ�������";
                            break;
                        case "78":
                            strJZLB = "�ƻ�����סԺ";
                            break;
                        case "79":
                            strJZLB = "�ƻ���������";
                            break;
                        case "81":
                            strJZLB = "����Ա���";
                            break;
                        case "91":
                            strJZLB = "��������";
                            break;
                        case "101":
                            strJZLB = "ҽѧ���";
                            break;
                    }
                    #endregion
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJZLB"].Value = strJZLB;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colZYZJE"].Value = objDgybdscxYYDSXXTmpVo.ZYZJE;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colSBZFJE"].Value = objDgybdscxYYDSXXTmpVo.SBZFJE;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colYLBZ"].Value = objDgybdscxYYDSXXTmpVo.YLBZ;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJZJE"].Value = objDgybdscxYYDSXXTmpVo.JZJE;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colDBYLJZJ"].Value = objDgybdscxYYDSXXTmpVo.DBYLJZJ;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colGRZFEIJE"].Value = objDgybdscxYYDSXXTmpVo.GRZFEIJE;

                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colZFEIYY"].Value = objDgybdscxYYDSXXTmpVo.ZFEIYY;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colFPHM"].Value = objDgybdscxYYDSXXTmpVo.FPHM;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colJSJKLX"].Value = objDgybdscxYYDSXXTmpVo.JSJKLX;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colBCYLTCZF1"].Value = objDgybdscxYYDSXXTmpVo.BCYLTCZF1;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colBCYLTCZF2"].Value = objDgybdscxYYDSXXTmpVo.BCYLTCZF2;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colBCYLTCZF3"].Value = objDgybdscxYYDSXXTmpVo.BCYLTCZF3;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colBCYLTCZF4"].Value = objDgybdscxYYDSXXTmpVo.BCYLTCZF4;
                    this.m_objViewer.dgvDetail.Rows[intRow].Cells["colQTZHIFU"].Value = objDgybdscxYYDSXXTmpVo.QTZHIFU;
                    if (Math.IEEERemainder(Convert.ToDouble(i), 2) == 0)
                    {
                        this.m_objViewer.dgvDetail.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(235, 240, 235);
                    }
                }
            }
        }
    }
}
