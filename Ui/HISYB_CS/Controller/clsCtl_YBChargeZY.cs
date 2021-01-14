using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Collections.Generic;
using com.digitalwave.Utility;
using System.IO;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBChargeZY : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_YBChargeZY()
        {
            objDomain = new clsDcl_YB();
        }
        public clsDcl_YB objDomain = null;

        /// <summary>
        /// ��Ժ����Ƿ�ʹ��Ĭ��ֵ
        /// </summary>
        public bool blnIfDefaultCYZD = false;
        public string strCYZD = "";
        #region ��ȡҩƷ�������� 2014-12-25
        public bool m_blnDiffCostOn = false;
        #endregion
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmYBChargeZY m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmYBChargeZY)frmMDI_Child_Base_in;
        }
        #endregion

        clsDGExtra_VO objDgextraVo = null;

        Dictionary<string, string> dicJSDYZDBY = new Dictionary<string, string>();
        string strRYDYZDBY = "";
        string strJSDYZDBY = "";

        /// <summary>
        /// �����籣��.��ά��
        /// </summary>
        string QRCode { get; set; }

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            dicJSDYZDBY.Add("", "");
            dicJSDYZDBY.Add("����", "11");
            dicJSDYZDBY.Add("�������ص��µ��˺�", "12");
            dicJSDYZDBY.Add("�����ǵ��������µ��˺�", "13");
            dicJSDYZDBY.Add("ͻ���¼����µ��˺�", "14");
            dicJSDYZDBY.Add("���������µ��˺�����ͨ�¹ʳ��⣩", "15");
            dicJSDYZDBY.Add("��ͨ�¹�", "16");
            foreach (string strKey in dicJSDYZDBY.Keys)
            {
                this.m_objViewer.cobJSDYZDBY.Items.Add(strKey);
            }
            this.m_objViewer.cobJSDYZDBY.SelectedIndex = 0;

            if (this.m_objViewer.strJslx == "1")//��Ժ�������������Ժ�Ǽ�
            {
                this.m_objViewer.btnCYDJ.Visible = true;
                this.m_objViewer.btnCharge.Enabled = false;
            }
            else
            {
                this.m_objViewer.btnCYDJ.Visible = false;
                this.m_objViewer.btnCharge.Enabled = true;
            }
            objDgextraVo = new clsDGExtra_VO();
            objDgextraVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
            objDgextraVo.JBR = this.m_objViewer.strEmpNo;// ����Ա����
            string strPwd = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
            long lngRes = clsYBPublic_cs.m_lngUserLoin(objDgextraVo.YYBH, strPwd, false);
            if (lngRes > 0)
            {
                m_mthZydyxs();
            }


        }
        #endregion

        #region סԺ�������������жϡ���ʼ������
        /// <summary>
        /// סԺ�������������жϡ���ʼ������
        /// </summary>
        public void m_mthZydyxs()
        {
            clsDGZydyxs_VO objDgzydyxsVo = null;
            clsDGZyjsfh_VO objDgzyjsfhVo = null;
            string strName = string.Empty;
            string strZyh = string.Empty;
            string strStatus = string.Empty;
            decimal decZyfyze = 0;
            decimal decGrzfeije = 0;
            long lngRes = this.objDomain.m_lngGetZYYBDyxs(this.m_objViewer.strJslx, this.m_objViewer.strRegisterId, ref strName, ref strZyh, ref strStatus, out objDgzydyxsVo, out objDgzyjsfhVo, out decZyfyze, out decGrzfeije);
            if (objDgzydyxsVo == null)
            {
                MessageBox.Show("�ò���û��ҽ���Ǽ���Ϣ����ע�⣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            this.strRYDYZDBY = objDgzydyxsVo.RYDYZDBY;
            this.strJSDYZDBY = objDgzydyxsVo.JSDYZDBY;

            #region ��ȫȫ�ֲ���VO
            objDgextraVo.JZJLH = objDgzydyxsVo.JZJLH;
            objDgextraVo.ZYH = strZyh;

            DataTable dtTmp = null;
            this.objDomain.m_lngGetPatientInfo(objDgextraVo.ZYH, "0", out dtTmp);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                objDgextraVo.CBDTCQBM = dtTmp.Rows[dtTmp.Rows.Count - 1]["CBDTCQBM_VCHR"].ToString();
            }
            if (string.IsNullOrEmpty(objDgextraVo.CBDTCQBM)) objDgextraVo.CBDTCQBM = "441925";  // ��ɽ

            #endregion
            if (objDgzyjsfhVo != null)
            {
                if (objDgzyjsfhVo.SDYWH != "0")
                {
                    this.m_objViewer.lblSdywh.Text = objDgzyjsfhVo.SDYWH;
                }
            }
            this.m_objViewer.lblName.Text = strName;
            this.m_objViewer.lblZyh.Text = strZyh;
            if (objDgzydyxsVo.ZYLB == "1" || objDgzydyxsVo.ZYLB == "4")
            {
                if (objDgzydyxsVo.ZYLB == "1")
                    this.m_objViewer.lblzylb.Text = "ҽ��סԺ";
                else if (objDgzydyxsVo.ZYLB == "4")
                    this.m_objViewer.lblzylb.Text = "����סԺ";

                if (this.strJSDYZDBY != "")
                {
                    foreach (string key in dicJSDYZDBY.Keys)
                    {
                        if (dicJSDYZDBY[key] == this.strJSDYZDBY)
                        {
                            this.m_objViewer.cobJSDYZDBY.Enabled = false;
                            this.m_objViewer.cobJSDYZDBY.SelectedItem = key;
                        }
                    }
                }
                else if (this.strRYDYZDBY != "5" && this.strRYDYZDBY != "6" && !string.IsNullOrEmpty(this.strRYDYZDBY))
                {
                    this.m_objViewer.cobJSDYZDBY.Enabled = false;
                    string strDefault = Convert.ToString(Convert.ToInt32(this.strRYDYZDBY) + 10);
                    foreach (string key in dicJSDYZDBY.Keys)
                    {
                        if (dicJSDYZDBY[key] == strDefault)
                        {
                            this.m_objViewer.cobJSDYZDBY.SelectedItem = key;
                        }
                    }
                }
                else
                {
                    this.m_objViewer.cobJSDYZDBY.SelectedIndex = 0;
                }
            }
            else if (objDgzydyxsVo.ZYLB == "2")
            {
                this.m_objViewer.lblzylb.Text = "����סԺ";

                if (this.strJSDYZDBY != "")
                {
                    foreach (string key in dicJSDYZDBY.Keys)
                    {
                        if (dicJSDYZDBY[key] == this.strJSDYZDBY)
                        {
                            this.m_objViewer.cobJSDYZDBY.Enabled = false;
                            this.m_objViewer.cobJSDYZDBY.SelectedItem = key;
                        }
                    }
                }
                else
                {
                    this.m_objViewer.cobJSDYZDBY.Enabled = false;
                    if (this.strRYDYZDBY != "")
                    {
                        string strDefault = Convert.ToString(Convert.ToInt32(this.strRYDYZDBY) + 10);
                        foreach (string key in dicJSDYZDBY.Keys)
                        {
                            if (dicJSDYZDBY[key] == strDefault)
                            {
                                this.m_objViewer.cobJSDYZDBY.SelectedItem = key;
                            }
                        }
                    }
                }
            }

            this.m_objViewer.lbljzjlh.Text = objDgzydyxsVo.JZJLH;
            this.m_objViewer.lblTotal.Text = decZyfyze.ToString();
            this.m_objViewer.lblSub.Text = decGrzfeije.ToString();
            this.m_objViewer.lblAcc.Text = Convert.ToString(decZyfyze - decGrzfeije);
            string strYYTemp = string.Empty;
            string strDyxsbzTemp = string.Empty;

            lngRes = clsYBPublic_cs.m_lngFunSP1003(objDgzydyxsVo, objDgextraVo, ref strYYTemp, ref strDyxsbzTemp);
            if (lngRes > 0)
            {
                this.m_objViewer.txtReason.Text = strYYTemp;
                if (strDyxsbzTemp == "0")
                {

                    this.m_objViewer.lbldyxsdz.Text = "��������ҽ������";
                    MessageBox.Show("�籣ϵͳ��ʾ���ò���Ŀǰ��������ҽ����������ע�⣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    this.m_objViewer.btnUpload.Enabled = false;
                    this.m_objViewer.btnCharge.Enabled = false;
                    this.m_objViewer.btnOk.Enabled = false;
                    return;
                }
                else if (strDyxsbzTemp == "1")
                {
                    this.m_objViewer.lbldyxsdz.Text = "����ҽ������";
                    if (strStatus != null)
                    {
                        switch (strStatus)
                        {
                            case "0":
                                this.m_objViewer.btnUpload.Enabled = true;
                                break;
                            case "1":
                                this.m_objViewer.btnCharge.Enabled = true;
                                break;
                        }
                        this.m_objViewer.btnOk.Enabled = true;
                    }
                }
            }

        }
        #endregion


        #region �����ϴ�
        /// <summary>
        /// �����ϴ�
        /// </summary>
        public void m_mthUpload()
        {
            //�Д��Ƿ����籣�Y��
            if ((this.strRYDYZDBY != "5" || this.strRYDYZDBY != "6") && string.IsNullOrEmpty(this.m_objViewer.cobJSDYZDBY.SelectedItem.ToString()))
            {
                MessageBox.Show("��סԺ���Ϊ��ҽ��סԺ���ҡ���Ժ��һ��ϲ���Ϊ��5���������µ��˺�����ͨ�¹ʳ��⣩,6��ͨ�¹ʡ�ʱ���������һ��ϲ��򡱱�������¼��");
                return;
            }
            List<clsDGZyxmcs_VO> lstDgzyxmcsVo = null;
            long lngRes = this.objDomain.m_lngGetDgzyxmcs(this.m_objViewer.strRegisterId, out lstDgzyxmcsVo, this.m_blnDiffCostOn, this.m_objViewer.decTotal, this.m_objViewer.lstPChargeId);
            if (lngRes < 0 || lstDgzyxmcsVo == null || lstDgzyxmcsVo.Count == 0)
            {
                return;
            }

            // 20151207
            if (this.m_objViewer.decTotal > 0)
            {
                decimal tmpMoney = 0;
                foreach (clsDGZyxmcs_VO item in lstDgzyxmcsVo)
                {
                    tmpMoney += item.JE;
                }
                decimal diffMoney = this.m_objViewer.decTotal - tmpMoney;
                if (diffMoney != 0) //&& (this.m_objViewer.lstPChargeId == null || this.m_objViewer.lstPChargeId.Count ==0))  // 20191223 ��ʱ���ų���;���� --> 20191227�ָ�
                {
                    for (int k = lstDgzyxmcsVo.Count - 1; k >= 0; k--)
                    {
                        if (lstDgzyxmcsVo[k].JE + diffMoney > 0)
                        {
                            lstDgzyxmcsVo[k].JE += diffMoney;
                            lstDgzyxmcsVo[k].JG = lstDgzyxmcsVo[k].JE / lstDgzyxmcsVo[k].MCYL;
                            break;
                        }
                    }
                }
            }

            #region bak.20151207
            //decimal TempTotal = 0;
            //for (int k = 0; k < lstDgzyxmcsVo.Count; k++)
            //{
            //    TempTotal += lstDgzyxmcsVo[k].MCYL * lstDgzyxmcsVo[k].JG;
            //}
            //decimal diff = this.m_objViewer.decTotal - this.Round(TempTotal, 2);

            //if (diff != 0)
            //{
            //    if (diff > 0)//˵����������� ��������֮�������� ��Ҫ����
            //    {
            //        lstDgzyxmcsVo[0].JE += diff;
            //        lstDgzyxmcsVo[0].JG = lstDgzyxmcsVo[0].JE / lstDgzyxmcsVo[0].MCYL;//���/����

            //    }
            //    else
            //    {
            //        for (int k = 0; k < lstDgzyxmcsVo.Count; k++)
            //        {
            //            if (lstDgzyxmcsVo[k].JE + diff > 0)
            //            {
            //                lstDgzyxmcsVo[k].JE += diff;
            //                lstDgzyxmcsVo[k].JG = lstDgzyxmcsVo[k].JE / lstDgzyxmcsVo[k].MCYL;//���/����
            //                break;
            //            }
            //        }
            //    }
            //}
            //System.Text.StringBuilder strValue = null;

            //try
            //{
            //    string strHeader = null;
            //    string LogName = @"d:\code\" + this.m_objViewer.strRegisterId + ".csv";
            //    if (File.Exists(LogName))
            //    {
            //        File.Delete(LogName);
            //    }
            //    TempTotal = 0;
            //    decimal total2 = 0;
            //    for (int k = 0; k < lstDgzyxmcsVo.Count; k++)
            //    {
            //        TempTotal = lstDgzyxmcsVo[k].MCYL * lstDgzyxmcsVo[k].JG;
            //        total2 += TempTotal;
            //    }
            //    TempTotal = this.Round(total2, 2);//˵Ҫ��ͱ�����λС��.
            //    if (TempTotal != this.m_objViewer.decTotal)//�ٴ�У��
            //    {
            //        diff = this.m_objViewer.decTotal - TempTotal;
            //        if (diff < 0)
            //        {
            //            for (int k = 0; k < lstDgzyxmcsVo.Count; k++)
            //            {
            //                TempTotal = lstDgzyxmcsVo[k].JG * lstDgzyxmcsVo[k].JE;
            //                if (TempTotal + diff > 0)
            //                {
            //                    lstDgzyxmcsVo[k].JE = TempTotal + diff;
            //                    lstDgzyxmcsVo[k].JG = this.Round(lstDgzyxmcsVo[k].JE / lstDgzyxmcsVo[k].MCYL, 4);//���/����
            //                    break;
            //                }
            //            }
            //        }
            //        else if (diff > 0)
            //        {
            //            TempTotal = lstDgzyxmcsVo[0].JG * lstDgzyxmcsVo[0].JE;
            //            lstDgzyxmcsVo[0].JE = TempTotal + diff;
            //            lstDgzyxmcsVo[0].JG = this.Round(lstDgzyxmcsVo[0].JE / lstDgzyxmcsVo[0].MCYL, 4);//���/����
            //        }
            //    }
            //    //��־��¼
            //    for (int k = 0; k < lstDgzyxmcsVo.Count; k++)
            //    {
            //        TempTotal = lstDgzyxmcsVo[k].MCYL * lstDgzyxmcsVo[k].JG;
            //        strHeader = lstDgzyxmcsVo[k].JE + "," + TempTotal + "," + lstDgzyxmcsVo[k].MCYL + "," + lstDgzyxmcsVo[k].JG + "\r\n";
            //        File.AppendAllText(LogName, strHeader);
            //    }

            //}
            //catch
            //{
            //}
            #endregion

            System.Text.StringBuilder strValue = new System.Text.StringBuilder(5000);
            lngRes = clsYBPublic_cs.m_lngFunSP3002(lstDgzyxmcsVo, objDgextraVo, ref strValue);
            if (lngRes > 0)
            {
                lngRes = this.objDomain.m_lngUpdateDgzyxmcs(lstDgzyxmcsVo);
                MessageBox.Show("��ϸ�ϴ��ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.m_objViewer.btnUpload.Enabled = false;
            }
            else
            {
                if (strValue != null)
                {
                    MessageBox.Show("��ϸ�ϴ�ʧ�ܣ�������Ϣ��" + strValue.ToString(), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("��ϸ�ϴ�ʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }
        #endregion

        #region ���������ϴ�
        /// <summary>
        /// ���������ϴ�
        /// </summary>
        public void m_mthCancelUpload()
        {
            if (MessageBox.Show("�˲�����Ѵ˲����������ϴ��ķ���ȫ�������Ƿ�ȷ�ϴ˲�����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            List<clsDGZyxmcs_VO> lstDgzyxmcsVo = null;
            //long lngRes = this.objDomain.m_lngGetDgzyxmcs(this.m_objViewer.strRegisterId, out lstDgzyxmcsVo);
            //if (lngRes < 0 || lstDgzyxmcsVo == null || lstDgzyxmcsVo.Count == 0)
            //{
            //    return;
            //}

            DataTable dtTmp = null;
            this.objDomain.m_lngGetPatientInfo(objDgextraVo.ZYH, "0", out dtTmp);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                objDgextraVo.CBDTCQBM = dtTmp.Rows[dtTmp.Rows.Count - 1]["CBDTCQBM_VCHR"].ToString();
            }
            if (string.IsNullOrEmpty(objDgextraVo.CBDTCQBM)) objDgextraVo.CBDTCQBM = "441925";  // ��ɽ

            System.Text.StringBuilder strValue = null;
            long lngRes = clsYBPublic_cs.m_lngFunSP3003(lstDgzyxmcsVo, objDgextraVo, ref strValue);
            if (lngRes > 0)
            {
                lngRes = this.objDomain.m_lngUpdateDgzyxmcs(this.m_objViewer.strRegisterId);
                MessageBox.Show("���������ϴ��ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.m_objViewer.btnUpload.Enabled = true;
            }
            else
            {
                if (strValue != null)
                {
                    MessageBox.Show("���������ϴ�ʧ�ܣ�������Ϣ��" + strValue.ToString(), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("���������ϴ�ʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }
        #endregion

        #region ҽ������
        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="blYBss">�Ƿ�ҽ������</param>
        public void m_mthYBCharge(bool blYBss)
        {
            if (string.IsNullOrEmpty(this.m_objViewer.txtZDZMHM.Text))
            {
                MessageBox.Show("���֤�����벻��Ϊ�գ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.m_objViewer.txtZDZMHM.Focus();
                return;
            }

            clsDGZyjs_VO objDgzyjsVo = null;
            long lngRes = this.objDomain.m_lngGetZYYBjs(this.m_objViewer.strJslx, this.m_objViewer.strInvNo, this.m_objViewer.txtZDZMHM.Text.Trim(), this.m_objViewer.decTotal, this.m_objViewer.strRegisterId, out objDgzyjsVo, this.m_blnDiffCostOn);
            if (lngRes < 0 || objDgzyjsVo == null)
            {
                return;
            }
            if (this.m_objViewer.strJslx.Equals("2"))//��;����
            {
                objDgzyjsVo.JSQSRQ = this.m_objViewer.strJSQSRQ; //������ʼ����
                objDgzyjsVo.JSZZRQ = this.m_objViewer.strJSZZRQ; //������ֹ����
                int intJSTS = 0;
                try
                {
                    intJSTS = Convert.ToInt32(objDgzyjsVo.JSZZRQ) - Convert.ToInt32(objDgzyjsVo.JSQSRQ);
                    intJSTS += 1;
                    if (intJSTS <= 0)
                    {
                        MessageBox.Show("������ֹ���ڲ���С�ڽ�����ʼ���ڣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("�빴ѡ��ȷ�����ڽ�����;���㣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                objDgzyjsVo.JSTS = intJSTS;

            }
            if (!blYBss)//�Ƿ�ҽ������
            {
                objDgzyjsVo.JSLX = "3";
            }
            string strZDZMHM = this.m_objViewer.txtZDZMHM.Text.Trim();
            //need modify ��ȫҽ������Vo
            objDgzyjsVo.JBR = this.m_objViewer.strEmpNo;//������
            objDgzyjsVo.ZDZMHM = strZDZMHM.Length > 30 ? strZDZMHM.Substring(0, 30) : strZDZMHM;
            objDgzyjsVo.JDZFBL = decimal.Parse(this.m_objViewer.txtJdzfbl.Text);//����֧������
            //objDgzyjsVo.ZYFYZE = this.m_objViewer.decTotal;//�ĳ��ɺ�̨�ϼ��ܶ�
            objDgzyjsVo.FPHM = this.m_objViewer.strInvNo;//��Ʊ��
            objDgzyjsVo.JSDYZDBY = dicJSDYZDBY[this.m_objViewer.cobJSDYZDBY.SelectedItem.ToString()];
            clsDGZyjsfh_VO objDgzyjsfhVo = null;
            if (!string.IsNullOrEmpty(strCYZD))
            {
                objDgzyjsVo.CYZD = strCYZD;
            }
            else
            {
                objDgzyjsVo.CYZD = "��Ժ���";
            }
            if (blnIfDefaultCYZD)
            {
                if (this.m_objViewer.strJslx == "1")
                {
                    objDgzyjsVo.CYZD = "��Ժ����";
                }
                else if (this.m_objViewer.strJslx == "2")
                {
                    objDgzyjsVo.CYZD = "��;����";
                }
                blnIfDefaultCYZD = false;
            }

            lngRes = clsYBPublic_cs.m_lngFunSP3004(objDgzyjsVo, objDgextraVo, out objDgzyjsfhVo);
            if (lngRes > 0)
            {
                if (objDgzyjsfhVo != null)
                {
                    if (blYBss)
                    {
                        //need add ����objDgzyjsfhVo��HIS�� //need add �˴���update t_ins_cszyreg.status=2�����Ʋ������޸�ҽ���Ǽ���Ϣ
                        lngRes = this.objDomain.m_lngSaveYBChargeReturnZY(this.m_objViewer.strRegisterId, this.m_objViewer.strInvNo, objDgzyjsfhVo, dicJSDYZDBY[this.m_objViewer.cobJSDYZDBY.SelectedItem.ToString()]);
                        //�Ƶ�����ķ�������
                        //lngRes = this.objDomain.m_lngUpdateYBRegisterStatusZY(this.m_objViewer.strRegisterId, "2");
                        if (this.m_objViewer.decTotal != objDgzyjsfhVo.ZYFYZE)
                        {
                            MessageBox.Show("ҽԺ�ϴ��ܽ�����籣�����ܽ�һ�£�����ʧ�ܣ������ԭ��������ԡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        this.m_objViewer.lblTotal.Text = objDgzyjsfhVo.ZYFYZE.ToString();
                        this.m_objViewer.decTotal = objDgzyjsfhVo.ZYFYZE;
                        this.m_objViewer.lblSub.Text = objDgzyjsfhVo.GRZFEIJE.ToString();
                        this.m_objViewer.lblAcc.Text = Convert.ToString(objDgzyjsfhVo.ZYFYZE - objDgzyjsfhVo.GRZFEIJE);
                        this.m_objViewer.decYBSub = Convert.ToDecimal(objDgzyjsfhVo.GRZFEIJE.ToString().Trim());

                        this.m_objViewer.btnUpload.Enabled = false;
                        this.m_objViewer.btnCYDJ.Enabled = false;
                        this.m_objViewer.btnCharge.Enabled = false;
                        this.m_objViewer.btnOk.Enabled = true;
                        this.m_objViewer.btnClose.Enabled = true;
                        this.m_objViewer.lblSdywh.Text = objDgzyjsfhVo.SDYWH;
                        MessageBox.Show("ҽ������ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {

                        this.m_objViewer.lblTotal.Text = objDgzyjsfhVo.ZYFYZE.ToString();
                        this.m_objViewer.decTotal = objDgzyjsfhVo.ZYFYZE;
                        this.m_objViewer.lblSub.Text = objDgzyjsfhVo.GRZFEIJE.ToString();
                        this.m_objViewer.lblAcc.Text = Convert.ToString(objDgzyjsfhVo.ZYFYZE - objDgzyjsfhVo.GRZFEIJE);
                        this.m_objViewer.decYBSub = Convert.ToDecimal(objDgzyjsfhVo.GRZFEIJE.ToString().Trim());

                        this.m_objViewer.btnUpload.Enabled = false;
                        //this.m_objViewer.btnCYDJ.Enabled = false;
                        this.m_objViewer.btnCharge.Enabled = true;
                        this.m_objViewer.btnOk.Enabled = true;
                        MessageBox.Show("ҽ������ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
            }
            else
            {
                this.m_objViewer.btnOk.Enabled = false;
            }
        }
        #endregion

        #region ȡ��ҽ������
        /// <summary>
        /// ȡ��ҽ������
        /// </summary>
        public void m_mthCancelYBCharge()
        {
            string strJsxh = this.m_objViewer.lblSdywh.Text.Trim();
            if (string.IsNullOrEmpty(strJsxh))
            {
                MessageBox.Show("�˲���δ��ҽ�����㣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (MessageBox.Show("�˲�����Ѵ˲����������ϴ��ķ���ȫ�������Ƿ�ȷ�ϴ˲�����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataTable dtTmp = null;
            this.objDomain.m_lngGetPatientInfo(objDgextraVo.ZYH, "0", out dtTmp);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                objDgextraVo.CBDTCQBM = dtTmp.Rows[dtTmp.Rows.Count - 1]["CBDTCQBM_VCHR"].ToString();
            }
            if (string.IsNullOrEmpty(objDgextraVo.CBDTCQBM)) objDgextraVo.CBDTCQBM = "441925";  // ��ɽ

            System.Text.StringBuilder strValue = null;
            objDgextraVo.SDYWH = this.m_objViewer.lblSdywh.Text;
            long lngRes = clsYBPublic_cs.m_lngFunSP3008(objDgextraVo, ref strValue);
            if (lngRes > 0)
            {
                lngRes = this.objDomain.m_lngUpdateYBChargeStatusZY(objDgextraVo.SDYWH, this.m_objViewer.strRegisterId, "-1");
                this.m_objViewer.lblSdywh.Text = "";
                this.m_objViewer.lblTotal.Text = "";
                this.m_objViewer.lblAcc.Text = "";
                this.m_objViewer.lblSub.Text = "";
                MessageBox.Show("ȡ��ҽ������ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
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

        #region �޸�ҽ���Ǽ�
        /// <summary>
        /// �޸�ҽ���Ǽ�
        /// </summary>
        public void m_mthModifyReg()
        {
            frmYBRegisterZY objYBReg = new frmYBRegisterZY();
            objYBReg.strRegisterId = this.m_objViewer.strRegisterId;
            objYBReg.ShowDialog();
        }
        #endregion

        #region ��Ժ�Ǽ�
        /// <summary>
        /// ҽ������
        /// </summary>
        public void m_mthYBCydj()
        {
            clsDGZycydj_VO objdgzycydjVo = null;
            string strJZJLH = this.m_objViewer.lbljzjlh.Text;
            if (string.IsNullOrEmpty(strJZJLH))
            {
                MessageBox.Show("�˲��˻�δ����ҽ���Ǽǣ����ȵǼǣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            long lngRes = this.objDomain.m_lngGetZYYBCydj(this.m_objViewer.strRegisterId, strJZJLH, out objdgzycydjVo);
            if (lngRes < 0 || objdgzycydjVo == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(strCYZD))
            {
                objdgzycydjVo.CYZD = strCYZD;
            }
            else
            {
                objdgzycydjVo.CYZD = "��Ժ���";
            }
            lngRes = clsYBPublic_cs.m_lngFunSP3005(objdgzycydjVo, objDgextraVo);
            if (lngRes > 0)
            {
                this.m_objViewer.btnChargeTest.Enabled = true;
                this.m_objViewer.btnCharge.Enabled = true;
                //need add �˴���update t_ins_cszyreg.status=1�����Ʋ������޸�ҽ���Ǽ���Ϣ
                lngRes = this.objDomain.m_lngUpdateYBRegisterStatusZY(this.m_objViewer.strRegisterId, "1");
                MessageBox.Show("��Ժ�Ǽǳɹ������ڿ��Խ���ҽ�������ˣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        #endregion

        #region ҽ�����㵥��ӡ
        /// <summary>
        /// ҽ�����㵥��ӡ
        /// </summary>
        /// <param name="strRegisterId">�Ǽ�ID</param>
        /// <param name="strEmpNO">����Ա����</param>
        public void m_mthYBChang(string strRegisterId, string strEmpNO)
        {
            clsDGExtra_VO objDgextraVo;
            clsDGZYjsxxxx objDgzyjsxxxxVo;
            List<clsDGZYjsGRZFXMMX_VO> lstDgzyjsGRZFXMMXVo;
            this.objDomain.m_lngGetYBChargeZY(strRegisterId, out objDgextraVo);
            if (objDgextraVo != null)
            {
                if (objDgextraVo.JZJLH != null && objDgextraVo.SDYWH != null)
                {
                    objDgextraVo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                    objDgextraVo.JBR = strEmpNO.ToString().Trim();
                    string strPwd = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
                    long lngRes = clsYBPublic_cs.m_lngUserLoin(objDgextraVo.YYBH, strPwd, false);
                    if (lngRes > 0)
                    {
                        clsYBPublic_cs.m_lngFunSP3007(objDgextraVo, out objDgzyjsxxxxVo, out lstDgzyjsGRZFXMMXVo);
                        clsYBPublic_cs.m_mthRptChargeDet(objDgextraVo, objDgzyjsxxxxVo, lstDgzyjsGRZFXMMXVo, 1);
                    }
                }
            }
        }
        #endregion

        #region ����Ƿ�����סԺ���
        /// <summary>
        /// ����Ƿ�����סԺ���
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>
        public long m_lngCheckDiagnose()
        {
            long lngRes = 1;
            DataTable dtResult = new DataTable();
            clsDcl_YB objDcl = new clsDcl_YB();
            objDcl.m_lngCheckDiagnose2(this.m_objViewer.strJslx, this.m_objViewer.strRegisterId, out dtResult);
            objDcl = null;
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                strCYZD = dtResult.Rows[0]["outhospitaldiagnose_right"].ToString();
                if (!string.IsNullOrEmpty(strCYZD))
                {
                    if (strCYZD.Length > 100) strCYZD = strCYZD.Substring(0, 100);
                }
                if (dtResult.Rows[0][0].ToString().Length == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                clsLogText log = new clsLogText();
                log.LogError("û�м�⵽��Ժ���");
                return 0;
            }
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
                DateTime dtmFyrq = DateTime.Now;
                long lngRes = this.objDomain.m_lngGetYBPswCheckInfo(objDgextraVo.JZJLH, ref objDgextraVo, out dtmFyrq);
                if (lngRes > 0)
                {
                    lngRes = clsYBPublic_cs.m_lngFunSP5001(2, objDgextraVo, dtmFyrq, (this.m_objViewer.rdoEk.Checked ? this.QRCode : ""));
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
        }
        #endregion

        #region ��ʡ��ؿ�����
        /// <summary>
        /// ��ʡ��ؿ�����
        /// </summary>
        internal void Ksydkjy()
        {
            #region get
            clsDGZydyxs_VO objDgzydyxsVo = null;
            clsDGZyjsfh_VO objDgzyjsfhVo = null;
            string strName = string.Empty;
            string strZyh = string.Empty;
            string strStatus = string.Empty;
            decimal decZyfyze = 0;
            decimal decGrzfeije = 0;
            long lngRes = this.objDomain.m_lngGetZYYBDyxs(this.m_objViewer.strJslx, this.m_objViewer.strRegisterId, ref strName, ref strZyh, ref strStatus, out objDgzydyxsVo, out objDgzyjsfhVo, out decZyfyze, out decGrzfeije);
            if (objDgzydyxsVo == null)
            {
                MessageBox.Show("�ò���û��ҽ���Ǽ���Ϣ����ע�⣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            #endregion

            string uri = string.Empty;
            string JBR = this.m_objViewer.LoginInfo.m_strEmpNo;
            clsDGZydj_VO vo = new clsDGZydj_VO();
            vo.GMSFHM = objDgzydyxsVo.GMSFHM;
            vo.JZLB = objDgzydyxsVo.JZLB;
            vo.RYRQ = this.objDomain.GetFeeMaxDate(this.m_objViewer.strRegisterId); // ��������: ������ֹ����
            vo.CBDTCQBM = objDgzydyxsVo.CBDTCQBM;
            vo.YYBH = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");  // סԺҽԺ���
            clsYBPublic_cs.SP3_5003(vo, JBR, objDgzydyxsVo.JZJLH, out uri);
            if (!string.IsNullOrEmpty(uri))
            {
                frmUri frm = new frmUri("��ʡ��ؿ�����", uri);
                frm.ShowDialog();
            }

        }
        #endregion

        #region ReadQRcode
        /// <summary>
        /// ReadQRcode
        /// </summary>
        internal void ReadQRcode()
        {
            DateTime dtmFyrq = DateTime.Now;
            this.objDomain.m_lngGetYBPswCheckInfo(objDgextraVo.JZJLH, ref objDgextraVo, out dtmFyrq);

            // 010105 �շ�
            frmReadQRcode frmQR = new frmReadQRcode(objDgextraVo.GMSFHM, objDgextraVo.JBR, objDgextraVo.YYBH, "010105");
            if (frmQR.ShowDialog() == DialogResult.OK)
            {
                this.QRCode = frmQR.QRCode;       // �����籣����ά��
                this.m_objViewer.rdoEk.Checked = true;
            }
        }
        #endregion

        #region ����ֵ��������
        /// <summary>
        /// ����ֵ��������
        /// </summary>
        /// <param name="d">��ֵ</param>
        /// <param name="decimals">С��λ��</param>
        /// <returns></returns>
        public decimal Round(decimal d, int decimals)
        {
            try
            {
                if (decimals < 1)
                {
                    return Convert.ToDecimal(Convert.ToInt32(d));
                }
                else
                {
                    string s = "0.";
                    for (int i = 0; i < decimals; i++)
                    {
                        s += "0";
                    }
                    return Convert.ToDecimal(d.ToString(s));
                }
            }
            catch
            {
                return d;
            }
        }
        #endregion
    }
}
