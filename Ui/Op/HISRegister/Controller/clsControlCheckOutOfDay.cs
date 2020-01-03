using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Drawing.Printing;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlCheckOutOfDay ��ժҪ˵����
    /// </summary>
    public class clsControlCheckOutOfDay : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlCheckOutOfDay()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// �ش�Ʊ������
        /// </summary>
        public string[] m_strInvoArr = null;
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmCheckOutOfDay m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmCheckOutOfDay)frmMDI_Child_Base_in;
        }
        #endregion

        DataTable dtCheckOut = new DataTable();
        DataTable dtPayType = new DataTable();
        clsDomainControl_Register Domain = new clsDomainControl_Register();
        private DataTable dtStatistics = new DataTable();
        private DataRow StatisticsRow;
        private ArrayList SaveINVOICENO = new ArrayList();
        /// <summary>
        /// 0-δ����1-����
        /// </summary>
        int intcomand = 0;
        string strCheckDate = "";
        private ArrayList arrList;
        private ArrayList arrReList = new ArrayList();
        string strCheckManID = "";
        string strName = "";

        public void getData()
        {
            if (this.m_objViewer.isDoctorDean == true)
            {
                intcomand = 1;
                strCheckManID = this.m_objViewer.m_cboCheckMan.SelectItemValue.ToString();
            }
            else
            {
                strCheckManID = this.m_objViewer.LoginInfo.m_strEmpID;
            }
            arrReList.Clear();
            SaveINVOICENO.Clear();
            if (intcomand == 0)
            {
                //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = new com.digitalwave.iCare.middletier.HIS.clsHisBase();
                //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)
                //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
                string checkDate = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString();
                Domain.m_lngGetPayTypeAndCheckOutData(strCheckManID, checkDate, out dtPayType, out dtCheckOut);
                Domain.m_mthGetbalancerepeatinvoinfo(strCheckManID, checkDate, out this.m_strInvoArr, intcomand);
                strCheckDate = this.m_objViewer.DateCheckOut.Value.ToShortDateString();
                this.m_objViewer.btnPrint.Enabled = false;
                this.m_objViewer.buttonXP3.Enabled = false;
                this.m_objViewer.buttonXP2.Enabled = false;
                this.m_objViewer.buttonXP4.Enabled = false;
            }
            else
            {
                strCheckDate = this.m_objViewer.ctlDgFind[this.m_objViewer.ctlDgFind.CurrentCell.RowNumber, 0].ToString();
                string BALANCEEMP = this.m_objViewer.LoginInfo.m_strEmpID;
                string CheckDate = this.m_objViewer.DateCheckOut.Value.ToShortDateString();
                Domain.m_lngGetPayTypeAndCheckOutDatahistory(strCheckDate, strCheckManID, out dtPayType, out dtCheckOut);
                Domain.m_mthGetbalancerepeatinvoinfo(strCheckManID, strCheckDate, out this.m_strInvoArr, intcomand);
                this.m_objViewer.btnCheck.Enabled = false;
                this.m_objViewer.btnPrint.Enabled = true;
                this.m_objViewer.buttonXP3.Enabled = true;
                this.m_objViewer.buttonXP2.Enabled = true;
                this.m_objViewer.buttonXP4.Enabled = true;
            }

            arrList = new ArrayList();
            DataTable dt = new DataTable();
            dt.Columns.Add("OPREMP_CHR");
            dt.Columns.Add("STATUS_INT");
            dt.Columns.Add("invoiceno_vchr");
            for (int k1 = 0; k1 < dtCheckOut.Rows.Count; k1++)
            {
                DataRow newRow = dt.NewRow();
                newRow["OPREMP_CHR"] = dtCheckOut.Rows[k1]["OPREMP_CHR"];
                newRow["STATUS_INT"] = dtCheckOut.Rows[k1]["STATUS_INT"];
                newRow["invoiceno_vchr"] = dtCheckOut.Rows[k1]["invoiceno_vchr"];
                dt.Rows.Add(newRow);
            }
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                if (dt.Rows[i1]["STATUS_INT"].ToString() == "2" || dt.Rows[i1]["STATUS_INT"].ToString() == "3")
                {
                    dt.Rows[i1].Delete();
                    i1--;
                    dt.AcceptChanges();
                }
            }
            dt.AcceptChanges();
            clsMain.m_Detach(dt, "INVOICENO_VCHR", out arrList);
            #region ����һ��ͳ�Ʊ�
            //			DataTable dtStatistics=new DataTable();
            dtStatistics = new DataTable();
            dtStatistics.Columns.Add("��Ʊ��");
            dtStatistics.Columns.Add("��Ʊ���");
            dtStatistics.Columns.Add("��Ʊ��");
            dtStatistics.Columns.Add("��Ʊ���ϼ�");
            dtStatistics.Columns.Add("�ָ�Ʊ��");
            dtStatistics.Columns.Add("�ָ����ϼ�");
            dtStatistics.Columns.Add("��ЧƱ��");
            dtStatistics.Columns.Add("ʵ�ս��ϼ�");
            dtStatistics.Columns.Add("ʵ���ֽ�ϼ�");
            dtStatistics.Columns.Add("ˢ�����ϼ�");
            dtStatistics.Columns.Add("֧Ʊ���ϼ�");
            dtStatistics.Columns.Add("ҽ�����˽��");
            dtStatistics.Columns.Add("���Ѽ��˽��");
            dtStatistics.Columns.Add("�Է��Ͻɽ��");
            dtStatistics.Columns.Add("ҽ���˴�");
            dtStatistics.Columns.Add("�����˴�");
            dtStatistics.Columns.Add("�ԷѼ������˴�");
            dtStatistics.Columns.Add("�Է��˴�");
            dtStatistics.Columns.Add("�������ʽ��");
            dtStatistics.Columns.Add("��ʼ��Ʊ��");
            dtStatistics.Columns.Add("������Ʊ��");
            dtStatistics.Columns.Add("��һ�ŷ�Ʊʱ��");
            dtStatistics.Columns.Add("���һ�ŷ�Ʊʱ��");

            dtStatistics.Columns.Add("�������ϼ�");
            dtStatistics.Columns.Add("IC�����ϼ�");
            dtStatistics.Columns.Add("��������");
            dtStatistics.Columns.Add("���ݼ���");
            dtStatistics.Columns.Add("��Ժ����");
            dtStatistics.Columns.Add("��������");
            #endregion

            #region ͳ�Ƹ����շ����͵Ľ��
            dtPayType.Columns.Add("tolMoney");
            if (dtCheckOut.Rows.Count >= 0)
            {
                if (intcomand == 0)
                {
                    this.m_objViewer.btnCheck.Enabled = true;
                }
                //				this.m_objViewer.btnPrint.Enabled=true;
                for (int i1 = 0; i1 < dtPayType.Rows.Count; i1++)
                {
                    Double tolMoney = 0;
                    for (int f2 = 0; f2 < dtCheckOut.Rows.Count; f2++)
                    {
                        if (dtCheckOut.Rows[f2]["ITEMCATID_CHR"].ToString().Trim() == dtPayType.Rows[i1]["TYPEID_CHR"].ToString().Trim())
                        {
                            tolMoney += Convert.ToDouble(dtCheckOut.Rows[f2]["TOLFEE_MNY"].ToString().Trim());
                        }
                    }
                    dtPayType.Rows[i1]["tolMoney"] = tolMoney.ToString("0.00");
                }
            }
            else
            {
                this.m_objViewer.btnCheck.Enabled = false;
                this.m_objViewer.btnPrint.Enabled = false;
                this.m_objViewer.buttonXP3.Enabled = false;
                this.m_objViewer.buttonXP2.Enabled = false;
            }

            #endregion

            #region ͳ������
            StatisticsRow = dtStatistics.NewRow();
            StatisticsRow["��Ʊ��"] = 0;
            StatisticsRow["��Ʊ���"] = 0.00;
            StatisticsRow["��Ʊ��"] = 0;
            StatisticsRow["��Ʊ���ϼ�"] = 0.00;
            StatisticsRow["�ָ�Ʊ��"] = 0;
            StatisticsRow["�ָ����ϼ�"] = 0.00;

            StatisticsRow["ʵ���ֽ�ϼ�"] = 0.00;
            StatisticsRow["ˢ�����ϼ�"] = 0.00;

            StatisticsRow["֧Ʊ���ϼ�"] = 0.00;
            StatisticsRow["ҽ�����˽��"] = 0.00;
            StatisticsRow["���Ѽ��˽��"] = 0.00;
            StatisticsRow["�Է��Ͻɽ��"] = 0.00;

            StatisticsRow["ʵ�ս��ϼ�"] = 0.00;
            StatisticsRow["�������ʽ��"] = 0.00;
            StatisticsRow["ҽ���˴�"] = 0;
            StatisticsRow["�����˴�"] = 0;
            StatisticsRow["�ԷѼ������˴�"] = 0;
            StatisticsRow["�Է��˴�"] = 0;
            StatisticsRow["�������ϼ�"] = 0;
            StatisticsRow["IC�����ϼ�"] = 0;
            StatisticsRow["��������"] = 0;
            StatisticsRow["���ݼ���"] = 0;
            StatisticsRow["��Ժ����"] = 0;
            StatisticsRow["��������"] = 0;
            StatisticsRow["��һ�ŷ�Ʊʱ��"] = "";
            StatisticsRow["���һ�ŷ�Ʊʱ��"] = "";
            DateTime startDateTime = new DateTime();
            DateTime endDateTime = new DateTime();
            if (dtCheckOut.Rows.Count > 0)
            {
                StatisticsRow["��ʼ��Ʊ��"] = dtCheckOut.Rows[0]["INVOICENO_VCHR"].ToString();
                StatisticsRow["������Ʊ��"] = dtCheckOut.Rows[dtCheckOut.Rows.Count - 1]["INVOICENO_VCHR"].ToString();
                for (int i1 = 0; i1 < dtCheckOut.Rows.Count; i1++)
                {
                    //���㷢Ʊ�ĵ�һ�ź����һ��
                    if (i1 == 0)
                    {
                        StatisticsRow["��һ�ŷ�Ʊʱ��"] = Convert.ToDateTime(dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString());
                        StatisticsRow["���һ�ŷ�Ʊʱ��"] = Convert.ToDateTime(dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString());
                    }
                    else
                    {
                        if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) < Convert.ToDateTime(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString()))
                        {
                            StatisticsRow["��һ�ŷ�Ʊʱ��"] = Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString());
                        }
                        if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) > Convert.ToDateTime(StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString()))
                        {
                            StatisticsRow["���һ�ŷ�Ʊʱ��"] = Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString());
                        }
                    }

                    //-----------------------

                    //ͳ�ƿ�Ʊ��,��Ʊ���

                    if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "1")//ͳ�ƿ�Ʊ��,��Ʊ���
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                            StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                        }

                    }

                    //-------------------------


                    //��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                    if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")//��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                            StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            SaveINVOICENO.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                SaveINVOICENO.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }

                        }

                    }

                    //--------------------------

                    //�ָ�Ʊ��,�ָ����ϼ�
                    if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "3")//�ָ�Ʊ��,�ָ����ϼ�
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                            StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            arrReList.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                arrReList.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                        }
                    }

                    //----------------------



                    //ͳ���ֽ�ϼ�

                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "0")//ͳ���ֽ�ϼ�
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["ʵ���ֽ�ϼ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["ʵ���ֽ�ϼ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }

                    }

                    //-----------------------


                    //ˢ���ϼ�
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "1")//ˢ���ϼ�
                    {
                        if (i1 == 0)
                        {

                            //							if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="2")
                            //							    StatisticsRow["ˢ�����ϼ�"]=Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString().Trim())-Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            //							else
                            StatisticsRow["ˢ�����ϼ�"] = Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["ˢ�����ϼ�"] = Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }

                    }

                    //---------------


                    //֧Ʊ
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "2")//֧Ʊ
                    {
                        if (i1 == 0)
                        {
                            //							if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="2")
                            //							{
                            //								StatisticsRow["֧Ʊ���ϼ�"]=Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim())-Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            //							}
                            //							else
                            //							{
                            StatisticsRow["֧Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            //							}
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                //								if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="2")
                                //								{
                                //									StatisticsRow["֧Ʊ���ϼ�"]=Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                //								}
                                //								else
                                //								{
                                StatisticsRow["֧Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                //								}
                            }
                        }

                    }
                    //------------------

                    //ҽ�����˽��˴�

                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "2")//ҽ�����˽��˴�
                    {
                        if (i1 == 0)
                        {
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                            {
                                StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) - 1;
                                StatisticsRow["ҽ�����˽��"] = Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                StatisticsRow["ҽ�����˽��"] = Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                                {
                                    StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) - 1;
                                    StatisticsRow["ҽ�����˽��"] = Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                    StatisticsRow["ҽ�����˽��"] = Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }
                        }
                    }
                    //-------------

                    //�������ʽ��˴�
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "1" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "2" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "3" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "4" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "5")
                    {
                        if (i1 == 0)
                        {
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                            {
                                StatisticsRow["�ԷѼ������˴�"] = Convert.ToInt32(StatisticsRow["�ԷѼ������˴�"].ToString()) - 1;
                                StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["�ԷѼ������˴�"] = Convert.ToInt32(StatisticsRow["�ԷѼ������˴�"].ToString()) + 1;
                                StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                                {
                                    StatisticsRow["�ԷѼ������˴�"] = Convert.ToInt32(StatisticsRow["�ԷѼ������˴�"].ToString()) - 1;
                                    StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["�ԷѼ������˴�"] = Convert.ToInt32(StatisticsRow["�ԷѼ������˴�"].ToString()) - 1;
                                    StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }
                        }
                    }

                    //���Ѽ��˽��˴�
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "1")//���Ѽ��˽��˴�
                    {
                        if (i1 == 0)
                        {
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                            {
                                StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) - 1;
                                StatisticsRow["���Ѽ��˽��"] = Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                StatisticsRow["���Ѽ��˽��"] = Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                                {
                                    StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) - 1;
                                    StatisticsRow["���Ѽ��˽��"] = Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                    StatisticsRow["���Ѽ��˽��"] = Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }
                        }
                    }

                    //-------------------

                    //�Է��Ͻɽ��˴�
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "0")//�Է��Ͻɽ��˴�
                    {
                        if (i1 == 0)
                        {
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                            {
                                StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) - 1;
                                StatisticsRow["�Է��Ͻɽ��"] = Convert.ToDouble(StatisticsRow["�Է��Ͻɽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
                                StatisticsRow["�Է��Ͻɽ��"] = Convert.ToDouble(StatisticsRow["�Է��Ͻɽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                                {
                                    StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) - 1;
                                    StatisticsRow["�Է��Ͻɽ��"] = Convert.ToDouble(StatisticsRow["�Է��Ͻɽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
                                    StatisticsRow["�Է��Ͻɽ��"] = Convert.ToDouble(StatisticsRow["�Է��Ͻɽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                            }
                        }

                    }
                    #region �������˽��
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "3")
                    {
                        if (i1 == 0)
                        {

                            StatisticsRow["��������"] = Convert.ToDouble(StatisticsRow["��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {

                                StatisticsRow["��������"] = Convert.ToDouble(StatisticsRow["��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                    }

                    #endregion

                    #region ���ݼ��˽��
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "4")
                    {
                        if (i1 == 0)
                        {

                            StatisticsRow["���ݼ���"] = Convert.ToDouble(StatisticsRow["���ݼ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {

                                StatisticsRow["���ݼ���"] = Convert.ToDouble(StatisticsRow["���ݼ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                    }

                    #endregion

                    #region ��Ժ���˽��
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "5")
                    {
                        if (i1 == 0)
                        {

                            StatisticsRow["��Ժ����"] = Convert.ToDouble(StatisticsRow["��Ժ����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {

                                StatisticsRow["��Ժ����"] = Convert.ToDouble(StatisticsRow["��Ժ����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                    }

                    #endregion

                    #region IC��֧��

                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "3")
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["IC�����ϼ�"] = Convert.ToDouble(StatisticsRow["IC�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["IC�����ϼ�"] = Convert.ToDouble(StatisticsRow["IC�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }

                    }

                    #endregion

                    #region �������ϼ�

                    ////��ɽ������֧����>��������
                    if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "0")
                    {
                        if (i1 == 0)
                        {

                            StatisticsRow["�������ϼ�"] = Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["�������ϼ�"] = Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                    }

                    //if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="0"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="1"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="2"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="3"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="4"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="5")
                    //{
                    //    if(i1==0)
                    //    {
                    //        StatisticsRow["�������ϼ�"]=Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                    //    }
                    //    else
                    //    {
                    //        if(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim()==dtCheckOut.Rows[i1-1]["INVOICENO_VCHR"].ToString().Trim()&&dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim()==dtCheckOut.Rows[i1-1]["SEQID_CHR"].ToString().Trim())
                    //        {

                    //        }
                    //        else
                    //        {
                    //            StatisticsRow["�������ϼ�"]=Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                    //        }
                    //    }

                    //}

                    #endregion
                }
            }

            //---------------------

            //������ЧƱ��
            int intAvailability = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) - Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) + Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString().Trim());

            //������Ч���
            Double AvailabilityMoney = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString().Trim()) - Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString().Trim());

            StatisticsRow["��ЧƱ��"] = intAvailability.ToString();
            StatisticsRow["ʵ�ս��ϼ�"] = AvailabilityMoney.ToString();
            #endregion

            #region ���ô�ӡ
            this.m_objViewer.printDocument1.DefaultPageSettings.Landscape = false;

            PaperSize ps = new PaperSize("�ս��㱨��", 920, 700);
            this.m_objViewer.printDocument1.DefaultPageSettings.PaperSize = ps;

            #endregion
        }

        #region ���෢Ʊ��ӡ
        /// <summary>
        /// ͳ�Ʒ�Ʊ
        /// </summary>
        /// <param name="intsta"></param>
        /// <param name="arrCheck"></param>
        /// <param name="arrReturn"></param>
        /// <param name="arrBreck"></param>
        /// <param name="totailMoney"></param>
        private void m_mthCheckNO(int intsta, out ArrayList arrCheck, out ArrayList arrReturn, out ArrayList arrBreck, out string totailMoney, out string minCheckDate, out string maxCheckDate, out DataTable dtCheckNo)
        {

            int Rowint = 0;
            arrReturn = new ArrayList();
            arrBreck = new ArrayList();

            arrCheck = new ArrayList();
            minCheckDate = "";
            maxCheckDate = "";
            double momey = 0;
            totailMoney = "0";
            dtCheckNo = new DataTable();
            dtCheckNo.Columns.Add("CheckNo");
            if (dtCheckOut.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtCheckOut.Rows.Count; i1++)
                {
                    #region ͳ���ֽ�Ʊ
                    if (intsta == 1 && dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString() == "0")
                    {
                        //���㷢Ʊ�ĵ�һ�ź����һ��
                        if (Rowint == 0)
                        {
                            minCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                            maxCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) < Convert.ToDateTime(minCheckDate))
                            {
                                minCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) > Convert.ToDateTime(maxCheckDate))
                            {
                                maxCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                        }
                        Rowint++;
                        DataRow newRow = dtCheckNo.NewRow();
                        newRow["CheckNo"] = dtCheckOut.Rows[i1]["INVOICENO_VCHR"];
                        dtCheckNo.Rows.Add(newRow);
                        int INTERNALFLAG = int.Parse(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim());
                        if (i1 == 0)
                        {

                            arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                            {
                                arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                            {
                                arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }

                            if (INTERNALFLAG == 1 || INTERNALFLAG == 2 || (INTERNALFLAG < 0 || INTERNALFLAG > 2))
                            {
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                            else
                            {
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                                {
                                    arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                                {
                                    arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (INTERNALFLAG == 1 || INTERNALFLAG == 2 || (INTERNALFLAG < 0 || INTERNALFLAG > 2))
                                {
                                    momey += Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                                else
                                {
                                    momey += Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                            }

                        }
                    }
                    #endregion

                    #region ͳ��ҽ����ˢ����Ʊ
                    if (intsta == 2 && (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString() == "2" || dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString() == "1"))
                    {
                        DataRow[] objRow = dtCheckOut.Select("INTERNALFLAG_INT=2 or PAYTYPE_INT=1");

                        //���㷢Ʊ�ĵ�һ�ź����һ��
                        if (Rowint == 0)
                        {
                            minCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                            maxCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) < Convert.ToDateTime(minCheckDate))
                            {
                                minCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) > Convert.ToDateTime(maxCheckDate))
                            {
                                maxCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                        }
                        Rowint++;
                        if (i1 == 0)
                        {
                            arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                            {
                                arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                            {
                                arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                            {
                                arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "2")
                            {
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }

                            if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "1")
                            {
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                                {
                                    arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                                {
                                    arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                                {
                                    arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "2")
                                {
                                    momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "1")
                                {
                                    momey += Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                            }

                        }
                    }
                    #endregion

                    #region ͳ�ƹ��ѷ�Ʊ
                    if (intsta == 3 && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString() == "1")
                    {
                        //���㷢Ʊ�ĵ�һ�ź����һ��
                        if (Rowint == 0)
                        {
                            minCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                            maxCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) < Convert.ToDateTime(minCheckDate))
                            {
                                minCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) > Convert.ToDateTime(maxCheckDate))
                            {
                                maxCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                        }
                        Rowint++;
                        if (i1 == 0)
                        {
                            arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                            {
                                arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                            {
                                arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                                {
                                    arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                                {
                                    arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }

                        }
                    }
                    #endregion

                    #region ͳ��������Ʊ
                    if (intsta == 4 && dtCheckOut.Rows[i1]["internalflag_int"].ToString() != "1" && dtCheckOut.Rows[i1]["internalflag_int"].ToString() != "2" && dtCheckOut.Rows[i1]["internalflag_int"].ToString() != "0")
                    {

                        //���㷢Ʊ�ĵ�һ�ź����һ��
                        if (Rowint == 0)
                        {
                            minCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                            maxCheckDate = dtCheckOut.Rows[0]["RECORDDATE_DAT"].ToString();
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) < Convert.ToDateTime(minCheckDate))
                            {
                                minCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                            if (Convert.ToDateTime(dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString()) > Convert.ToDateTime(maxCheckDate))
                            {
                                maxCheckDate = dtCheckOut.Rows[i1]["RECORDDATE_DAT"].ToString();
                            }
                        }
                        Rowint++;
                        if (i1 == 0)
                        {
                            arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                            {
                                arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                            {
                                arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            }
                            momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                arrCheck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "3")
                                {
                                    arrBreck.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString() == "2")
                                {
                                    arrReturn.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                }
                                momey += Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }

                        }
                    }
                    #endregion

                }
            }
            else
            {
                return;
            }
            totailMoney = momey.ToString();
        }
        #region ��ӡ����Ʊ
        /// <summary>
        /// ��ӡ����Ʊ
        /// </summary>
        /// <param name="e"></param>
        public void printPageCheckNoMB(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 14);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
            const float RowHight = 23F;//��ĸ߶�
            const float LeftWith = 10F;//���޽��ĳ���
            const float RightWith = 50F;//���޽��ĳ���
            ArrayList arrCheck = null;//��Ʊ��
            ArrayList arrReturn = null;//��Ʊ��Ʊ��
            string totailMoney = "0";//�ܽ��
            ArrayList arrBreck = null;//��ԭ��Ʊ��
            string minCheckDate = "";//��ʼ��Ʊ��
            string maxCheckDate = "";//������Ʊ��

            DataTable dtCheckNo = null;//�����ֽ�Ʊ�ű�
            //��Ʊ����
            ArrayList arr1 = new ArrayList();
            #endregion
            for (int f1 = 1; f1 < 5; f1++)
            {
                #region ͷ��
                Pen penLine = new Pen(Brushes.Black, 1);
                curRowY += 15;
                curRowX = LeftWith;
                SizeF tilWith = e.Graphics.MeasureString("�շ�Աҽ����ˢ���սᱨ��", m_fntTitle);
                string strTiteName = "";
                switch (f1)
                {
                    case 1:
                        strTiteName = "�շ�Ա�ֽ�Ʊ�սᱨ��";
                        m_mthCheckNO(1, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        clsMain.m_Detach(dtCheckNo, "CheckNo", out arr1);
                        break;
                    case 2:
                        strTiteName = "�շ�Աҽ����ˢ����Ʊ�սᱨ��";
                        m_mthCheckNO(2, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                    case 3:
                        strTiteName = "�շ�Ա���ѷ�Ʊ�սᱨ��";
                        m_mthCheckNO(3, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                    case 4:
                        strTiteName = "�շ�Ա������Ʊ�սᱨ��";
                        m_mthCheckNO(4, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                }
                e.Graphics.DrawString(strTiteName, m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, curRowY);
                curRowY += 25;
                e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
                curRowX += tilWith.Width;
                e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
                curRowX += 150;
                e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
                curRowX += tilWith.Width;

                e.Graphics.DrawString(minCheckDate + " ~ " + maxCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
                curRowX += 300;
                e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
                tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
                string NowDate = DateTime.Now.ToShortDateString();
                e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
                #endregion
                curRowX = LeftWith;
                curRowY += 15;
                float strLine = curRowY;

                #region �����
                for (int i1 = 0; i1 < 4; i1++)
                {
                    float tmpWith = 20;//X��

                    switch (i1)
                    {
                        case 0:
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            curRowY += 5;
                            e.Graphics.DrawString("�ϼƽ���д", TextFont, Brushes.Black, curRowX + 5, curRowY);
                            e.Graphics.DrawLine(penLine, curRowX + 100, strLine, curRowX + 100, curRowY + 20);
                            e.Graphics.DrawString(clsMain.CurrencyToString(Math.Abs(float.Parse(totailMoney))), TextFont, Brushes.Black, curRowX + 100, curRowY);

                            e.Graphics.DrawLine(penLine, PageWidth - 250, strLine, PageWidth - 250, curRowY + 20);
                            e.Graphics.DrawString("�ϼƽ��Сд", TextFont, Brushes.Black, PageWidth - 245, curRowY);

                            e.Graphics.DrawLine(penLine, PageWidth - 145, strLine, PageWidth - 145, curRowY + 20);
                            e.Graphics.DrawString("��" + totailMoney, TextFont, Brushes.Black, PageWidth - 140, curRowY);
                            curRowY += 20;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 1:
                            curRowY += 50;
                            float temY = 0;
                            if (f1 == 1)
                            {

                                if (arr1.Count > 0)
                                {
                                    curRowY -= 50;
                                    int intRow = 0;
                                    int tolRow = 0;
                                    for (int f2 = 0; f2 < arr1.Count; f2++)
                                    {
                                        if (arr1[f2].ToString() == ",")
                                            tolRow++;
                                    }
                                    if (tolRow >= 4)
                                    {
                                        temY = 5;
                                    }
                                    else
                                    {
                                        temY = 10;
                                    }

                                    e.Graphics.DrawString(arr1[0].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                    int intCount = 0;
                                    for (int j2 = 0; j2 < arr1.Count; j2++)
                                    {

                                        if (intRow == 4 && intCount == 0)
                                        {
                                            intCount++;
                                            curRowY = temY + 10 + curRowY;
                                            tmpWith = 20;
                                        }
                                        try
                                        {
                                            if (arr1[j2].ToString() == ",")
                                            {
                                                intRow++;
                                                tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                                tmpWith += tilWith.Width;

                                                e.Graphics.DrawString("- " + arr1[j2 - 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                                if (j2 != arr1.Count - 1)
                                                {
                                                    tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                                    tmpWith += tilWith.Width;
                                                    e.Graphics.DrawString("," + arr1[j2 + 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                    tmpWith += tilWith.Width;
                                    e.Graphics.DrawString("- " + arr1[arr1.Count - 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                    curRowY += (intCount + 3) * temY;
                                }
                            }
                            else
                            {

                                float tempX;
                                float tempY;
                                if (arrCheck.Count > 0)
                                {
                                    curRowY -= 50;
                                    tempX = tmpWith;
                                    tempY = curRowY + 10;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrCheck.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrCheck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrCheck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString("," + arrCheck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrCheck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrCheck.Count && tempX + tilWith.Width >= PageWidth - RightWith)
                                        {
                                            RowCount++;
                                            tempX = tmpWith;
                                            tempY = curRowY + 15 * RowCount;

                                        }
                                    }
                                    curRowY = tempY + 20;
                                }

                            }
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 2:
                            try
                            {
                                float tempX = 0;
                                float tempY = 0;
                                e.Graphics.DrawString("��Ʊ��Ʊ��", TextFont, Brushes.Black, curRowX + 5, curRowY + 5);
                                e.Graphics.DrawLine(penLine, curRowX + 100, curRowY, curRowX + 100, curRowY + 20);

                                if (arrReturn.Count > 0)
                                {
                                    tempX = curRowX + 105;
                                    tempY = curRowY + 5;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrReturn.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrReturn[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrReturn[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {

                                            e.Graphics.DrawString("," + arrReturn[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrReturn[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrReturn.Count && tempX + tilWith.Width >= PageWidth - 300)
                                        {
                                            RowCount++;
                                            tempX = curRowX + 105;
                                            tempY = curRowY + 10 * RowCount;
                                        }
                                    }
                                }
                                e.Graphics.DrawLine(penLine, PageWidth - 350, curRowY, PageWidth - 350, curRowY + 20);
                                e.Graphics.DrawString("�ָ���Ʊ��", TextFont, Brushes.Black, PageWidth - 345, curRowY + 5);
                                e.Graphics.DrawLine(penLine, PageWidth - 250, curRowY, PageWidth - 250, curRowY + 20);

                                if (arrBreck.Count > 0)
                                {
                                    tempX = PageWidth - 255;
                                    tempY = curRowY + 5;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrBreck.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrBreck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrBreck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString("," + arrBreck[k1].ToString(), TextFont, Brushes.Black, PageWidth - 200, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrBreck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrBreck.Count && tempX + tilWith.Width >= PageWidth - RightWith)
                                        {
                                            RowCount++;
                                            tempX = PageWidth - 200;
                                            tempY = curRowY + 10 * RowCount;
                                        }
                                    }
                                    curRowY = tempY + 5;

                                }
                            }
                            catch
                            {
                                MessageBox.Show("2");
                            }

                            curRowY += 20;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 3:
                            try
                            {
                                tmpWith = LeftWith + 4;
                                e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                                tmpWith += tilWith.Width;


                                if (this.m_objViewer.isDoctorDean == true)
                                {
                                    e.Graphics.DrawString(this.m_objViewer.m_cboCheckMan.SelectItemText.ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                }
                                else
                                {
                                    e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                }

                                tmpWith += 200;
                                e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tmpWith += 200;
                                e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                curRowY += RowHight;
                                e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            }
                            catch
                            {
                                MessageBox.Show("3");
                            }
                            break;
                    }
                }
                e.Graphics.DrawLine(penLine, curRowX, strLine, curRowX, curRowY);
                e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine, PageWidth - RightWith, curRowY);
                #endregion
            }

        }
        #endregion

        #endregion

        public void printPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 14);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
            const float RowHight = 23F;//��ĸ߶�
            const float LeftWith = 45F;//���޽��ĳ���
            const float RightWith = 130F;//���޽��ĳ���
            const float Uphight = 10F;//�����޽��ĳ���
            #endregion

            #region ͷ��
            Pen penLine = new Pen(Brushes.Black, 1);
            curRowY = RowHight + Uphight + 10;
            curRowX += LeftWith;
            SizeF tilWith = e.Graphics.MeasureString(strName + "�շ�Ա�սᱨ��", m_fntTitle);
            e.Graphics.DrawString(strName + "�շ�Ա�սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(strCheckDate, TextFont);
            curRowX += tilWith.Width + 20;
            e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
            #endregion
            curRowX = LeftWith;
            float strLine = curRowY;

            #region �����
            for (int i1 = 0; i1 < 12; i1++)
            {
                float tmpWith;//X��
                const int with = 4;
                switch (i1)
                {
                    case 10:
                        #region ����շ���������
                        if (dtPayType.Rows.Count > 0)
                        {
                            tmpWith = LeftWith + 2;
                            curRowY += 10;
                            for (int f2 = 0; f2 < dtPayType.Rows.Count; f2++)
                            {
                                if (f2 % 5 == 0 && f2 != 0)
                                {
                                    tmpWith = LeftWith + 2;
                                    curRowY += RowHight + 5;
                                }
                                tilWith = e.Graphics.MeasureString("��ӡ����", TextFont);
                                if (dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length > 4)
                                {
                                    string star = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0, 4);
                                    string end = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
                                    e.Graphics.DrawString(star, TextFont, Brushes.Black, tmpWith, curRowY - 5);
                                    e.Graphics.DrawString(end + "��", TextFont, Brushes.Black, tmpWith, curRowY + 10);
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                else
                                {
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                tmpWith += 146;
                            }
                        }
                        curRowY += RowHight;
                        #endregion
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 0:
                        #region ��һ��
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tmpWith = LeftWith + with;
                        e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("ʵ�ս��ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//ʵ�ս��ϼ�
                        e.Graphics.DrawString("ʵ���ֽ�ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("ҽ�����˽��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//ҽ�����˽��
                        e.Graphics.DrawString("�� �� ͳ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 5);//�˴�ͳ��
                        e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 10 + RowHight * 6);//��Ʊ��
                        e.Graphics.DrawString("��Ʊ��Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 7);//��Ʊ��Ʊ��
                        e.Graphics.DrawString("�ָ���Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 8);//��Ʊ��Ʊ��
                        tilWith = e.Graphics.MeasureString("��  Ʊ  ����", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 9 + 10);

                        #endregion


                        #region �ڶ���
                        tmpWith += with;
                        e.Graphics.DrawString("�� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�� Ʊ �� �� �� ��:", TextFont);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���
                        float tolMoney = float.Parse(StatisticsRow["ʵ�ս��ϼ�"].ToString());

                        string strMoney = clsMain.CurrencyToString(Math.Abs(tolMoney));
                        e.Graphics.DrawString(strMoney, m_fntTitle, Brushes.Black, tmpWith + 10, curRowY + 3 + RowHight * 2);//ʵ�ս��

                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 3 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 3 + RowHight * 4);//ʵ���ֽ�ϼ�
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);

                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);

                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("��   Ʊ   ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("ˢ�����ϼ�", TextFont, Brushes.Black, tmpWith + 5, curRowY + 7 + RowHight * 3);//ˢ�����ϼ�
                        e.Graphics.DrawString("���Ѽ��ʽ��", TextFont, Brushes.Black, tmpWith + 5, curRowY + 7 + RowHight * 4);//��Ʊ��
                        tilWith = e.Graphics.MeasureString("��   Ʊ   ��:", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);
                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("�� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ˢ�����ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//ˢ�����ϼ�
                        tilWith = e.Graphics.MeasureString("�� Ʊ �� �� �� ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);

                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("�ָ�Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["�ָ�Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ�Ʊ��
                        e.Graphics.DrawString("֧ Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith + 10, curRowY + 7 + RowHight * 3);//֧Ʊ���ϼ�
                        tilWith = e.Graphics.MeasureString("�ָ�Ʊ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        #endregion


                        #region ������

                        tmpWith += with;
                        e.Graphics.DrawString("�ָ����ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ����ϼ�
                        tilWith = e.Graphics.MeasureString("�ָ����ϼ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 4);

                        tmpWith += with;
                        e.Graphics.DrawString("��  Ч  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        e.Graphics.DrawString(StatisticsRow["��ЧƱ��"].ToString() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��ЧƱ��
                        e.Graphics.DrawString("��" + Convert.ToDouble(tolMoney.ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//��Ч���
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//֧Ʊ���ϼ�
                        #endregion

                        break;
                    case 1:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 9:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 2:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 3:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 4:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 5:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        e.Graphics.DrawString("ҽ���˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("ҽ���˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["ҽ���˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        tmpWith += 50;
                        e.Graphics.DrawString("�����˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�����˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["�����˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 50;
                        e.Graphics.DrawString("�ԷѼ������˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        tilWith = e.Graphics.MeasureString("�ԷѼ������˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["�Է��˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        break;
                    case 6:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);

                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrList.Count > 0)
                        {
                            float temY = 0;
                            float newWith = tmpWith;
                            float temcurRowY = curRowY;
                            int intRow = 0;
                            int tolRow = 0;
                            for (int f2 = 0; f2 < arrList.Count; f2++)
                            {
                                if (arrList[f2].ToString() == ",")
                                    tolRow++;
                            }
                            if (tolRow >= 3)
                            {
                                temY = 3;
                            }
                            else
                            {
                                temY = 10;
                            }
                            e.Graphics.DrawString(arrList[0].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                            int intCount = 0;
                            for (int j2 = 0; j2 < arrList.Count; j2++)
                            {

                                if (intRow == 3 && intCount == 0)
                                {
                                    intCount++;
                                    temcurRowY = temY + 10 + temcurRowY;
                                    tmpWith = newWith - 90;
                                }
                                if (arrList[j2].ToString() == ",")
                                {
                                    intRow++;
                                    tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                    tmpWith += tilWith.Width;
                                    e.Graphics.DrawString("- " + arrList[j2 - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    if (j2 != arrList.Count - 1)
                                    {
                                        tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                        tmpWith += tilWith.Width;
                                        e.Graphics.DrawString("," + arrList[j2 + 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    }
                                }
                            }
                            tilWith = e.Graphics.MeasureString("- " + arrList[0].ToString(), TextFont);
                            tmpWith += tilWith.Width;
                            e.Graphics.DrawString("- " + arrList[arrList.Count - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                        }
                        break;
                    case 7:
                        curRowY += RowHight + 10;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (SaveINVOICENO.Count > 0)
                        {
                            for (int k2 = 0; k2 < SaveINVOICENO.Count; k2++)
                            {
                                e.Graphics.DrawString(SaveINVOICENO[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(SaveINVOICENO[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;

                            }

                        }

                        break;
                    case 8:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrReList.Count > 0)
                        {
                            for (int k2 = 0; k2 < arrReList.Count; k2++)
                            {
                                e.Graphics.DrawString(arrReList[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(arrReList[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;
                            }

                        }
                        break;
                    case 11:

                        tmpWith = LeftWith + 4;
                        e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);


                        tmpWith += 200;
                        e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 200;
                        e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);


                        break;
                    case 12:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                }
            }
            e.Graphics.DrawLine(penLine, curRowX, strLine + RowHight, curRowX, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine + RowHight, PageWidth - RightWith, curRowY);
            #endregion

        }


        public void printPageMB(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 14);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
            const float RowHight = 23F;//��ĸ߶�
            const float LeftWith = 45F;//���޽��ĳ���
            const float RightWith = 130F;//���޽��ĳ���
            const float Uphight = 10F;//�����޽��ĳ���
            #endregion

            #region ͷ��
            Pen penLine = new Pen(Brushes.Black, 1);
            curRowY = RowHight + Uphight + 10;
            curRowX += LeftWith;
            SizeF tilWith = e.Graphics.MeasureString(strName + "�շ�Ա�սᱨ��", m_fntTitle);
            e.Graphics.DrawString(strName + "�շ�Ա�սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(strCheckDate, TextFont);
            curRowX += tilWith.Width + 20;
            e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
            #endregion
            curRowX = LeftWith;
            float strLine = curRowY;

            #region �����
            for (int i1 = 0; i1 < 12; i1++)
            {
                float tmpWith;//X��
                const int with = 4;
                switch (i1)
                {
                    case 10:
                        #region ����շ���������
                        if (dtPayType.Rows.Count > 0)
                        {
                            tmpWith = LeftWith + 2;
                            curRowY += 10;
                            for (int f2 = 0; f2 < dtPayType.Rows.Count; f2++)
                            {
                                if (f2 % 5 == 0 && f2 != 0)
                                {
                                    tmpWith = LeftWith + 2;
                                    curRowY += RowHight + 5;
                                }
                                tilWith = e.Graphics.MeasureString("��ӡ����", TextFont);
                                if (dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length > 4)
                                {
                                    string star = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0, 4);
                                    string end = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
                                    e.Graphics.DrawString(star, TextFont, Brushes.Black, tmpWith, curRowY - 5);
                                    e.Graphics.DrawString(end + "��", TextFont, Brushes.Black, tmpWith, curRowY + 10);
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                else
                                {
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                tmpWith += 146;
                            }
                        }
                        curRowY += RowHight;
                        #endregion
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 0:
                        #region ��һ��
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tmpWith = LeftWith + with;
                        e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("ʵ�ս��ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//ʵ�ս��ϼ�
                        e.Graphics.DrawString("ʵ���ֽ�ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("ҽ�����˽��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//ҽ�����˽��
                        e.Graphics.DrawString("�� �� ͳ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 5);//�˴�ͳ��
                        e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 10 + RowHight * 6);//��Ʊ��
                        e.Graphics.DrawString("��Ʊ��Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 7);//��Ʊ��Ʊ��
                        e.Graphics.DrawString("�ָ���Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 8);//��Ʊ��Ʊ��
                        tilWith = e.Graphics.MeasureString("��  Ʊ  ����", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 9 + 10);

                        #endregion


                        #region �ڶ���
                        tmpWith += with;
                        e.Graphics.DrawString("�� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�� Ʊ �� �� �� ��:", TextFont);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���
                        float tolMoney = float.Parse(StatisticsRow["ʵ�ս��ϼ�"].ToString());
                        string strMoney = clsMain.CurrencyToString(Math.Abs(tolMoney));
                        e.Graphics.DrawString(strMoney, m_fntTitle, Brushes.Black, tmpWith + 10, curRowY + 3 + RowHight * 2);//ʵ�ս��

                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 3 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 3 + RowHight * 4);//ʵ���ֽ�ϼ�
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);

                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);

                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("��   Ʊ   ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("ˢ�����ϼ�", TextFont, Brushes.Black, tmpWith + 5, curRowY + 7 + RowHight * 3);//ˢ�����ϼ�
                        e.Graphics.DrawString("���Ѽ��ʽ��", TextFont, Brushes.Black, tmpWith + 5, curRowY + 7 + RowHight * 4);//���Ѽ��ʽ��

                        tilWith = e.Graphics.MeasureString("��   Ʊ   ��:", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);
                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("�� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ˢ�����ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//ˢ�����ϼ�
                        tilWith = e.Graphics.MeasureString("�� Ʊ �� �� �� ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);

                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("�ָ�Ʊ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["�ָ�Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ�Ʊ��
                        e.Graphics.DrawString("֧ Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith + 10, curRowY + 7 + RowHight * 3);//֧Ʊ���ϼ�
                        e.Graphics.DrawString("�� �� �� �� �� ��", TextFont, Brushes.Black, tmpWith + 10, curRowY + 7 + RowHight * 4);//�������ʽ��
                        tilWith = e.Graphics.MeasureString("�ָ�Ʊ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        #endregion


                        #region ������

                        tmpWith += with;
                        e.Graphics.DrawString("�ָ����ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ����ϼ�
                        tilWith = e.Graphics.MeasureString("�ָ����ϼ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 5);

                        tmpWith += with;
                        e.Graphics.DrawString("��  Ч  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        e.Graphics.DrawString(StatisticsRow["��ЧƱ��"].ToString() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��ЧƱ��
                        e.Graphics.DrawString("��" + Convert.ToDouble(tolMoney.ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//��Ч���
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//֧Ʊ���ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//֧Ʊ���ϼ�
                        #endregion

                        break;
                    case 1:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 9:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 2:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 3:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 4:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 5:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        e.Graphics.DrawString("ҽ���˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("ҽ���˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["ҽ���˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        tmpWith += 50;
                        e.Graphics.DrawString("�����˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�����˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["�����˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 50;
                        e.Graphics.DrawString("�ԷѼ������˴Σ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        tilWith = e.Graphics.MeasureString("�ԷѼ������˴Σ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(StatisticsRow["�Է��˴�"].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        break;
                    case 6:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);

                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrList.Count > 0)
                        {
                            float temY = 0;

                            float newWith = tmpWith;
                            float temcurRowY = curRowY;
                            int intRow = 0;
                            int tolRow = 0;
                            for (int f2 = 0; f2 < arrList.Count; f2++)
                            {
                                if (arrList[f2].ToString() == ",")
                                    tolRow++;
                            }
                            if (tolRow >= 3)
                            {
                                temY = 3;
                            }
                            else
                            {
                                temY = 10;
                            }
                            e.Graphics.DrawString(arrList[0].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                            int intCount = 0;
                            for (int j2 = 0; j2 < arrList.Count; j2++)
                            {

                                if (intRow == 3 && intCount == 0)
                                {
                                    intCount++;
                                    temcurRowY = temY + 10 + temcurRowY;
                                    tmpWith = newWith - 90;
                                }
                                if (arrList[j2].ToString() == ",")
                                {
                                    intRow++;
                                    tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                    tmpWith += tilWith.Width;
                                    e.Graphics.DrawString("- " + arrList[j2 - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    if (j2 != arrList.Count - 1)
                                    {
                                        tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                        tmpWith += tilWith.Width;
                                        e.Graphics.DrawString("," + arrList[j2 + 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    }
                                }
                            }
                            tilWith = e.Graphics.MeasureString("- " + arrList[0].ToString(), TextFont);
                            tmpWith += tilWith.Width;
                            e.Graphics.DrawString("- " + arrList[arrList.Count - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                        }
                        break;
                    case 7:
                        curRowY += RowHight + 10;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (SaveINVOICENO.Count > 0)
                        {
                            for (int k2 = 0; k2 < SaveINVOICENO.Count; k2++)
                            {
                                e.Graphics.DrawString(SaveINVOICENO[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(SaveINVOICENO[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;

                            }

                        }

                        break;
                    case 8:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��  Ʊ  ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrReList.Count > 0)
                        {
                            for (int k2 = 0; k2 < arrReList.Count; k2++)
                            {
                                e.Graphics.DrawString(arrReList[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(arrReList[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;

                            }

                        }
                        break;
                    case 11:

                        tmpWith = LeftWith + 4;
                        e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                        tmpWith += tilWith.Width;

                        if (this.m_objViewer.isDoctorDean == true)
                        {
                            e.Graphics.DrawString(this.m_objViewer.m_cboCheckMan.SelectItemText.ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        }
                        else
                        {
                            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        }

                        tmpWith += 200;
                        e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 200;
                        e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);


                        break;
                    case 12:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                }
            }
            e.Graphics.DrawLine(penLine, curRowX, strLine + RowHight, curRowX, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine + RowHight, PageWidth - RightWith, curRowY);
            #endregion

        }


        public void printPageFS(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 14);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
            const float RowHight = 23F;//��ĸ߶�
            const float LeftWith = 45F;//���޽��ĳ���
            const float RightWith = 130F;//���޽��ĳ���
            const float Uphight = 10F;//�����޽��ĳ���
            #endregion

            #region ͷ��
            Pen penLine = new Pen(Brushes.Black, 1);
            curRowY = RowHight + Uphight + 10;
            curRowX += LeftWith;
            SizeF tilWith = e.Graphics.MeasureString(strName + "�շ�Ա�սᱨ��", m_fntTitle);
            e.Graphics.DrawString(strName + "�շ�Ա�սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(strCheckDate, TextFont);
            curRowX += tilWith.Width + 20;
            e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString(StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString() + " ~ " + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString(), TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
            #endregion
            curRowX = LeftWith;
            float strLine = curRowY;

            #region �����
            for (int i1 = 0; i1 < 12; i1++)
            {
                float tmpWith;//X��
                const int with = 4;
                switch (i1)
                {
                    case 10:
                        #region ����շ���������
                        if (dtPayType.Rows.Count > 0)
                        {
                            tmpWith = LeftWith + 2;
                            curRowY += 10;
                            for (int f2 = 0; f2 < dtPayType.Rows.Count; f2++)
                            {
                                if (f2 % 5 == 0 && f2 != 0)
                                {
                                    tmpWith = LeftWith + 2;
                                    curRowY += RowHight + 5;
                                }
                                tilWith = e.Graphics.MeasureString("��ӡ����", TextFont);
                                if (dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length > 4)
                                {
                                    string star = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0, 4);
                                    string end = dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
                                    e.Graphics.DrawString(star, TextFont, Brushes.Black, tmpWith, curRowY - 5);
                                    e.Graphics.DrawString(end + "��", TextFont, Brushes.Black, tmpWith, curRowY + 10);
                                    if (dtPayType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                else
                                {
                                    e.Graphics.DrawString(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);
                                    if (dtPayType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        e.Graphics.DrawString(dtPayType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                }
                                tmpWith += 146;
                            }
                        }
                        curRowY += RowHight;
                        #endregion
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 0:
                        #region ��һ��
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tmpWith = LeftWith + with;
                        e.Graphics.DrawString("�� Ʊ ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("ʵ�պϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//ʵ�ս��ϼ�
                        e.Graphics.DrawString("�ֽ�֧��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("���Ѽ���", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//ҽ�����˽��
                        //						e.Graphics.DrawString("�� �� ͳ  ��",TextFont,Brushes.Black,tmpWith,curRowY+7+RowHight*5);//�˴�ͳ��
                        e.Graphics.DrawString("�� Ʊ ��", TextFont, Brushes.Black, tmpWith, curRowY + 10 + RowHight * 5);//��Ʊ��
                        e.Graphics.DrawString("��Ʊ��Ʊ", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 6);//��Ʊ��Ʊ��
                        e.Graphics.DrawString("�ָ���Ʊ", TextFont, Brushes.Black, tmpWith, curRowY + 15 + RowHight * 7);//��Ʊ��Ʊ��
                        tilWith = e.Graphics.MeasureString("�� Ʊ ��", TextFont);

                        e.Graphics.DrawString("�ش�Ʊ", TextFont, Brushes.Black, tmpWith, curRowY + 20 + RowHight * 8);//�ش�Ʊ��Ʊ��
                        e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 9 + 20, PageWidth - RightWith, curRowY + RowHight * 9 + 20);

                        #region �ش�Ʊ������
                        //   m_strInvoArr = new string[] { "DW9038844", "DW9038844", "DW9038844", "DW9038844", "DW9038844", "DW9038844", "DW9038844" };
                        string strInvoPrintstrUp = "";
                        string strInvoPrintstrDown = "";
                        SizeF sizeFtemp;
                        float fltWith = PageWidth - RightWith - 100;// RightWith - curRowX - tmpWith - tilWith.Width - 10;
                        if (m_strInvoArr != null)
                        {
                            for (int j = 0; j < m_strInvoArr.Length; j++)
                            {
                                sizeFtemp = e.Graphics.MeasureString(strInvoPrintstrUp + m_strInvoArr[j].Trim() + " ", TextFont);
                                if (fltWith > sizeFtemp.Width)
                                {
                                    strInvoPrintstrUp += m_strInvoArr[j].Trim() + " ";
                                }
                                else
                                {
                                    strInvoPrintstrDown += m_strInvoArr[j].Trim() + " ";
                                }
                            }
                            if (strInvoPrintstrDown != "")
                            {
                                e.Graphics.DrawString(strInvoPrintstrUp + strInvoPrintstrDown, TextFont, Brushes.Black, new RectangleF(tmpWith + tilWith.Width + 10, curRowY + 12 + RowHight * 8, PageWidth - RightWith - 100, curRowY + 12 + RowHight * 8));
                            }
                            else
                            {
                                e.Graphics.DrawString(strInvoPrintstrUp + strInvoPrintstrDown, TextFont, Brushes.Black, new RectangleF(tmpWith + tilWith.Width + 10, curRowY + 20 + RowHight * 8, PageWidth - RightWith - 100, curRowY + 20 + RowHight * 8));
                            }
                        }
                        #endregion

                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 9 + 20);

                        #endregion


                        #region �ڶ���
                        tmpWith += with;
                        e.Graphics.DrawString("�� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�� Ʊ �� �� �� ��", TextFont);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���
                        float tolMoney = float.Parse(StatisticsRow["ʵ�ս��ϼ�"].ToString());
                        string strMoney = clsMain.CurrencyToString(Math.Abs(tolMoney));
                        e.Graphics.DrawString(strMoney, m_fntTitle, Brushes.Black, tmpWith + 2, curRowY + 3 + RowHight * 2);//ʵ�ս��

                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//ʵ���ֽ�ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//���Ѽ��˽��
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);

                        e.Graphics.DrawLine(penLine, tmpWith - 50, curRowY + RowHight * 3, tmpWith - 50, curRowY + RowHight * 5);
                        e.Graphics.DrawString("IC ��", TextFont, Brushes.Black, tmpWith - 50, curRowY + 7 + RowHight * 3);
                        e.Graphics.DrawString("�� ��", TextFont, Brushes.Black, tmpWith - 50, curRowY + 7 + RowHight * 4);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);

                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ��
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["IC�����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//IC�����ϼ�
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��������"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//�������ʽ��

                        tilWith = e.Graphics.MeasureString("��  Ʊ  ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);
                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString("  �� Ʊ �� �� �� ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + "-" + Math.Abs(Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString())).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��Ʊ���ϼ�
                        //						
                        e.Graphics.DrawString("���п�", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);
                        e.Graphics.DrawString("��  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);

                        tilWith = e.Graphics.MeasureString("���п�", TextFont);
                        e.Graphics.DrawLine(penLine, tmpWith + tilWith.Width, curRowY + RowHight * 3, tmpWith + tilWith.Width, curRowY + RowHight * 5);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY + 7 + RowHight * 3);//ˢ�����ϼ�


                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["���ݼ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY + 7 + RowHight * 4);//��������

                        tilWith = e.Graphics.MeasureString(" ��  Ʊ  ��  ��  ��  ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);
                        e.Graphics.DrawLine(penLine, tmpWith - 30, curRowY + RowHight * 3, tmpWith - 30, curRowY + RowHight * 5);
                        e.Graphics.DrawString("֧Ʊ", TextFont, Brushes.Black, tmpWith - 30, curRowY + 7 + RowHight * 3);
                        e.Graphics.DrawString("��Ժ", TextFont, Brushes.Black, tmpWith - 30, curRowY + 7 + RowHight * 4);
                        #endregion

                        #region ������
                        tmpWith += with;
                        e.Graphics.DrawString(" �ָ�Ʊ�� ", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString(StatisticsRow["�ָ�Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ�Ʊ��

                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);//֧Ʊ���ϼ�

                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["��Ժ����"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//��Ժ����
                        tilWith = e.Graphics.MeasureString("�� �� Ʊ��", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 2);
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY + RowHight * 3, tmpWith, curRowY + RowHight * 5);
                        #endregion


                        #region ������

                        tmpWith += with;
                        e.Graphics.DrawString("�ָ����ϼ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//�ָ����ϼ�
                        //��ɽ������֧����>��������
                        e.Graphics.DrawString("��������", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);
                        //e.Graphics.DrawString("�� �� ֧ ��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);
                        e.Graphics.DrawString("�ض�ҽ������", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);
                        tilWith = e.Graphics.MeasureString("�ָ����ϼ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawLine(penLine, tmpWith, curRowY, tmpWith, curRowY + RowHight * 5);
                        tmpWith += with;
                        e.Graphics.DrawString("��  Ч  Ʊ  ��", TextFont, Brushes.Black, tmpWith, curRowY + 7);

                        e.Graphics.DrawString(StatisticsRow["��ЧƱ��"].ToString() + "��", TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight);//��ЧƱ��
                        e.Graphics.DrawString("��" + Convert.ToDouble(tolMoney.ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 2);//��Ч���
                        e.Graphics.DrawString("��" + Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 3);

                        ////�����ҽ���Ѵ�ӡ����������
                        //double hj = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString()) + Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString());
                        //������������ʸ�Ϊ�ض�ҽ������
                        double hj = Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString());
                        e.Graphics.DrawString("��" + hj.ToString("0.00"), TextFont, Brushes.Black, tmpWith, curRowY + 7 + RowHight * 4);//�������ʽ��

                        #endregion


                        break;
                    case 1:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 9:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        curRowY += RowHight + 15;

                        break;
                    case 2:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 3:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 4:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                    case 6:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);

                        tilWith = e.Graphics.MeasureString("��  ��Ʊ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrList.Count > 0)
                        {
                            float temY = 0;

                            float newWith = tmpWith;
                            float temcurRowY = curRowY;
                            int intRow = 0;
                            int tolRow = 0;
                            for (int f2 = 0; f2 < arrList.Count; f2++)
                            {
                                if (arrList[f2].ToString() == ",")
                                    tolRow++;
                            }
                            if (tolRow >= 3)
                            {
                                temY = 3;
                            }
                            else
                            {
                                temY = 10;
                            }
                            e.Graphics.DrawString(arrList[0].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                            int intCount = 0;
                            for (int j2 = 0; j2 < arrList.Count; j2++)
                            {

                                if (intRow == 3 && intCount == 0)
                                {
                                    intCount++;
                                    temcurRowY = temY + 10 + temcurRowY;
                                    tmpWith = newWith - 90;
                                }
                                if (arrList[j2].ToString() == ",")
                                {
                                    intRow++;
                                    tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                    tmpWith += tilWith.Width;
                                    e.Graphics.DrawString("- " + arrList[j2 - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    if (j2 != arrList.Count - 1)
                                    {
                                        tilWith = e.Graphics.MeasureString("- " + arrList[j2 - 1].ToString(), TextFont);
                                        tmpWith += tilWith.Width;
                                        e.Graphics.DrawString("," + arrList[j2 + 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                                    }
                                }
                            }
                            tilWith = e.Graphics.MeasureString("- " + arrList[0].ToString(), TextFont);
                            tmpWith += tilWith.Width;
                            e.Graphics.DrawString("- " + arrList[arrList.Count - 1].ToString(), TextFont, Brushes.Black, tmpWith, temcurRowY + temY);
                        }

                        break;
                    case 7:
                        curRowY += RowHight + 10;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("��  ��Ʊ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (SaveINVOICENO.Count > 0)
                        {
                            for (int k2 = 0; k2 < SaveINVOICENO.Count; k2++)
                            {
                                e.Graphics.DrawString(SaveINVOICENO[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(SaveINVOICENO[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;
                            }
                        }

                        break;
                    case 8:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        tilWith = e.Graphics.MeasureString("�� �� Ʊ��", TextFont);
                        tmpWith = LeftWith + tilWith.Width;
                        if (arrReList.Count > 0)
                        {
                            for (int k2 = 0; k2 < arrReList.Count; k2++)
                            {
                                e.Graphics.DrawString(arrReList[k2].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + 7);
                                tilWith = e.Graphics.MeasureString(arrReList[k2].ToString() + ",", TextFont);
                                tmpWith += tilWith.Width;
                            }
                        }

                        break;
                    case 11:
                        tmpWith = LeftWith + 4;
                        e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                        tmpWith += tilWith.Width;
                        e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 200;
                        e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        tmpWith += 200;
                        e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);


                        break;
                    case 12:
                        curRowY += RowHight;
                        e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                        break;
                }
            }
            e.Graphics.DrawLine(penLine, curRowX, strLine + RowHight, curRowX, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine + RowHight, PageWidth - RightWith, curRowY);
            #endregion

        }

        #region ͳ�Ƹ����շ����͵Ľ��
        private void m_mthDe(ref DataTable dtType, DataTable dtCheckOut)
        {

            if (dtCheckOut.Rows.Count >= 0)
            {
                for (int i1 = 0; i1 < dtType.Rows.Count; i1++)
                {
                    Double tolMoney = 0;
                    for (int f2 = 0; f2 < dtCheckOut.Rows.Count; f2++)
                    {
                        if (dtCheckOut.Rows[f2]["ITEMCATID_CHR"].ToString().Trim() == dtType.Rows[i1]["TYPEID_CHR"].ToString().Trim())
                        {
                            try
                            {
                                tolMoney += Convert.ToDouble(dtCheckOut.Rows[f2]["SBSUM_MNY"].ToString().Trim());
                            }
                            catch
                            {
                                MessageBox.Show(dtCheckOut.Rows[f2]["SBSUM_MNY"].ToString());
                            }
                        }
                    }
                    dtType.Rows[i1]["tolMoney"] = tolMoney.ToString("0.00");
                }
            }
        }
        #endregion
        #region ��ȡ����
        /// <summary>
        /// �ֽ�֧��
        /// </summary>
        DataTable dt1 = null;
        /// <summary>
        /// �ֽ�֧����ϸ
        /// </summary>
        DataTable dtDe1 = null;
        /// <summary>
        /// IC��
        /// </summary>
        DataTable dt2 = null;
        /// <summary>
        /// IC����ϸ
        /// </summary>
        DataTable dtDe2 = null;
        /// <summary>
        /// ���п�
        /// </summary>
        DataTable dt3 = null;
        /// <summary>
        /// ���п���ϸ
        /// </summary>
        DataTable dtDe3 = null;
        /// <summary>
        /// ֧Ʊ
        /// </summary>
        DataTable dt4 = null;
        /// <summary>
        /// ֧Ʊ��ϸ
        /// </summary>
        DataTable dtDe4 = null;
        /// <summary>
        /// ��������
        /// </summary>
        DataTable dt5 = null;
        /// <summary>
        /// ����������ϸ
        /// </summary>
        DataTable dtDe5 = null;
        /// <summary>
        /// ���Ѽ���
        /// </summary>
        DataTable dt6 = null;
        /// <summary>
        /// ���Ѽ�����ϸ
        /// </summary>
        DataTable dtDe6 = null;
        /// <summary>
        /// ����
        /// </summary>
        DataTable dt7 = null;
        /// <summary>
        /// ������ϸ
        /// </summary>
        DataTable dtDe7 = null;
        /// <summary>
        /// ����
        /// </summary>
        DataTable dt8 = null;
        /// <summary>
        /// ������ϸ
        /// </summary>
        DataTable dtDe8 = null;
        /// <summary>
        /// ��Ժ
        /// </summary>
        DataTable dt9 = null;
        /// <summary>
        /// ��Ժ��ϸ
        /// </summary>
        DataTable dtDe9 = null;
        /// <summary>
        /// �ض�ҽ������
        /// </summary>
        DataTable dt10 = null;
        /// <summary>
        /// �ض�ҽ��������ϸ
        /// </summary>
        DataTable dtDe10 = null;
        /// <summary>
        /// �շ�����
        /// </summary>
        DataTable dtCheckType = null;
        public void m_mthGetData()
        {
            if (this.m_objViewer.isDoctorDean == true)
            {
                intcomand = 1;
                strCheckManID = this.m_objViewer.m_cboCheckMan.SelectItemValue.ToString();
            }
            else
            {
                strCheckManID = this.m_objViewer.LoginInfo.m_strEmpID;
            }
            if (intcomand == 1)
            {
                strCheckDate = this.m_objViewer.ctlDgFind[this.m_objViewer.ctlDgFind.CurrentCell.RowNumber, 0].ToString();
                string BALANCEEMP = this.m_objViewer.LoginInfo.m_strEmpID;
                string CheckDate = this.m_objViewer.DateCheckOut.Value.ToShortDateString();
                Domain.m_lngGetPayTypeAndCheckOutDatahistory(strCheckDate, strCheckManID, out dtPayType, out dtCheckOut);
                this.m_objViewer.btnCheck.Enabled = false;
                this.m_objViewer.btnPrint.Enabled = true;
                this.m_objViewer.buttonXP3.Enabled = true;
                this.m_objViewer.buttonXP2.Enabled = true;

            }
            //Domain.m_lngGetDataAllOfStat(strCheckDate, strCheckManID, out dt1, out dtDe1, out dt2, out dtDe2, out dt3, out dtDe3, out dt4, out dtDe4, out dtCheckType);
            Domain.m_lngGetCheckOutOfClassification(strCheckDate, strCheckManID, out dt1, out dtDe1, out dt2, out dtDe2, out dt3, out dtDe3, out dt4, out dtDe4, out dt5, out dtDe5, out dt6, out dtDe6, out dt7, out dtDe7, out dt8, out dtDe8, out dt9, out dtDe9, out dt10, out dtDe10, out dtCheckType);
            #region ���ô�ӡ
            this.m_objViewer.printDocument2.DefaultPageSettings.Landscape = false;
            foreach (PaperSize ps in this.m_objViewer.printDocument2.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName == "A4")
                {
                    this.m_objViewer.printDocument2.DefaultPageSettings.PaperSize = ps;
                    break;
                }
            }
            #endregion
        }

        #endregion
        #region ����
        int m_intCurrentPage = 0;
        float PageWidth ;//���ҳ��Ŀ��
        float PageHight ;//���ҳ��ĸ߶�
        float curRowY = 0;//��ǰ�е�Y����
        float curRowX = 0;//��ǰ�е�X����
        System.Drawing.Font m_fntTitle = new Font("����", 15,FontStyle.Bold);//����ʹ�õ�����
        System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
        const float RowHight = 23F;//��ĸ߶�
        const float LeftWith = 25F;//���޽��ĳ���
        const float RightWith = 40F;//���޽��ĳ���
        #endregion
        #region �����շ�
        /// <summary>
        /// �����շ�
        /// </summary>
        /// <param name="e"></param>
        public void printPageCheckMB(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            curRowY = 0;//��ǰ�е�Y����
            curRowX = 0;//��ǰ�е�X����
            #endregion

            DataTable dtCheck =new DataTable ();
            for (int i = 0; i < 3; i++)
            {
                #region ͷ��
                Pen penLine = new Pen(Brushes.Black, 1);
                curRowY += 35;
                curRowX = LeftWith;
                try
                {

                    dtCheckType.Columns.Add("tolMoney");
                }
                catch
                {
                    dtCheckType.Columns.RemoveAt(dtCheckType.Columns.Count - 1);
                    dtCheckType.Columns.Add("tolMoney");

                }
                SizeF tilWith = e.Graphics.MeasureString("�շ�Ա�ֽ��սᱨ��", m_fntTitle);
                string strTiteName = "";
                switch (m_intCurrentPage)
                {
                    case 0:
                        strTiteName = "�շ�Ա�ֽ�֧���սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա�ֽ�֧���սᱨ��", m_fntTitle);
                        dtCheck = dt1;
                        m_mthDe(ref dtCheckType, dtDe1);
                        break;
                    case 1:
                        strTiteName = "�շ�ԱIC���սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�ԱIC���սᱨ��", m_fntTitle);
                        dtCheck = dt2;
                        m_mthDe(ref dtCheckType, dtDe2);
                        break;
                    case 2:
                        strTiteName = "�շ�Ա���п��սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա���п��սᱨ��", m_fntTitle);
                        dtCheck = dt3;
                        m_mthDe(ref dtCheckType, dtDe3);
                        break;
                    case 3:
                        strTiteName = "�շ�Ա֧Ʊ�սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա֧Ʊ�սᱨ��", m_fntTitle);
                        dtCheck = dt4;
                        m_mthDe(ref dtCheckType, dtDe4);
                        break;
                    case 4:
                        strTiteName = "�շ�Ա���������սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա���������սᱨ��", m_fntTitle);
                        dtCheck = dt5;
                        m_mthDe(ref dtCheckType, dtDe5);
                        break;
                    case 5:
                        strTiteName = "�շ�Ա���Ѽ����սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա���Ѽ����սᱨ��", m_fntTitle);
                        dtCheck = dt6;
                        m_mthDe(ref dtCheckType, dtDe6);
                        break;
                    case 6:
                        strTiteName = "�շ�Ա�����սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա�����սᱨ��", m_fntTitle);
                        dtCheck = dt7;
                        m_mthDe(ref dtCheckType, dtDe7);
                        break;
                    case 7:
                        strTiteName = "�շ�Ա�����սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա�����սᱨ��", m_fntTitle);
                        dtCheck = dt8;
                        m_mthDe(ref dtCheckType, dtDe8);
                        break;
                    case 8:
                        strTiteName = "�շ�Ա��Ժ�սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա��Ժ�սᱨ��", m_fntTitle);
                        dtCheck = dt9;
                        m_mthDe(ref dtCheckType, dtDe9);
                        break;
                    case 9:
                        strTiteName = "�շ�Ա�ض�ҽ�������սᱨ��";
                        tilWith = e.Graphics.MeasureString("�շ�Ա�ض�ҽ�������սᱨ��", m_fntTitle);
                        dtCheck = dt10;
                        m_mthDe(ref dtCheckType, dtDe10);
                        break;
        

                }
                e.Graphics.DrawString(strTiteName, m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, curRowY);
                curRowY += 35;
                e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
                curRowX += tilWith.Width;
                e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
                curRowX += 150;
                e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
                curRowX += tilWith.Width;
                if (dtCheck.Rows.Count != 0)
                {
                    e.Graphics.DrawString(dtCheck.Rows[0]["minrecorddate_dat"].ToString() + " ~ " + dtCheck.Rows[0]["maxrecorddate_dat"].ToString(), TextFont, Brushes.Black, curRowX, curRowY);
                }
                curRowX += 300;
                e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
                tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
                string NowDate = DateTime.Now.ToShortDateString();
                e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
                #endregion
                curRowX = LeftWith;
                curRowY += 15;
                float strLine = curRowY;

                #region �����
                for (int i1 = 0; i1 < 3; i1++)
                {
                    float tmpWith;//X��
                    switch (i1)
                    {
                        case 0:
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            curRowY += 5;
                            e.Graphics.DrawString("�ϼƽ���д", TextFont, Brushes.Black, curRowX + 5, curRowY);
                            e.Graphics.DrawLine(penLine, curRowX + 100, strLine, curRowX + 100, curRowY + 20);
                            try
                            {
                                if (dtCheck.Rows[0]["totalmoney"].ToString() == "")
                                    dtCheck.Rows[0]["totalmoney"] = 0;

                                e.Graphics.DrawString(clsMain.CurrencyToString(Math.Abs(float.Parse(dtCheck.Rows[0]["totalmoney"].ToString()))), TextFont, Brushes.Black, curRowX + 105, curRowY);
                            }
                            catch
                            {

                            }

                            e.Graphics.DrawLine(penLine, PageWidth - 260, strLine, PageWidth - 260, curRowY + 20);
                            e.Graphics.DrawString("�ϼƽ��Сд", TextFont, Brushes.Black, PageWidth - 255, curRowY);
                            e.Graphics.DrawLine(penLine, PageWidth - 160, strLine, PageWidth - 160, curRowY + 20);

                            e.Graphics.DrawString("��" + dtCheck.Rows[0]["totalmoney"].ToString(), TextFont, Brushes.Black, PageWidth - 160, curRowY);
                            curRowY += 20;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 1:
                            #region ����շ���������
                            if (dtPayType.Rows.Count > 0)
                            {
                                tmpWith = LeftWith + 2;
                                curRowY += 10;
                                for (int f2 = 0; f2 < dtCheckType.Rows.Count; f2++)
                                {
                                    if (f2 % 6 == 0 && f2 != 0)
                                    {
                                        tmpWith = LeftWith + 2;
                                        curRowY += RowHight + 5;
                                    }
                                    tilWith = e.Graphics.MeasureString("��ӡ����", TextFont);
                                    if (dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length > 4)
                                    {
                                        string star = dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0, 4);
                                        string end = dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
                                        e.Graphics.DrawString(star, TextFont, Brushes.Black, tmpWith, curRowY - 5);
                                        e.Graphics.DrawString(end + "��", TextFont, Brushes.Black, tmpWith, curRowY + 10);

                                        if (dtCheckType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        {
                                            e.Graphics.DrawString(dtCheckType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);

                                        if (dtCheckType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        {
                                            e.Graphics.DrawString(dtCheckType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                        }
                                    }
                                    tmpWith += 130;
                                }
                            }
                            curRowY += RowHight;
                            #endregion
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 2:

                            tmpWith = LeftWith + 4;
                            e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                            tmpWith += tilWith.Width;
                            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);


                            tmpWith += 200;
                            e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            tmpWith += 200;
                            e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            curRowY += RowHight;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                    }
                }
                e.Graphics.DrawLine(penLine, curRowX, strLine, curRowX, curRowY);
                e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine, PageWidth - RightWith, curRowY);
                curRowY += 30;
                #endregion
                m_intCurrentPage++;
                if (m_intCurrentPage == 10)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (m_intCurrentPage == 3 || m_intCurrentPage == 6 || m_intCurrentPage == 9)
            {

                e.HasMorePages = true;
                curRowY = 0;
                curRowY += 35;
                curRowX = LeftWith;
                return;

            }
        }
        #endregion
        #region m_mthEndPrintSet
        public void m_mthEndPrint()
        {
            m_intCurrentPage = 0;
        }
        #endregion

        #region �ϲ������ӡ
        /// <summary>
        /// �ϲ������ӡ
        /// </summary>
        /// <param name="e"></param>
        int intRow1 = 0;
        public void printPageCheckUnit(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 20F;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 14);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 10);//����ʹ�õ�����
            const float RowHight = 23F;//��ĸ߶�
            const float LeftWith = 10F;//���޽��ĳ���
            const float RightWith = 40F;//���޽��ĳ���
            #endregion
            DataTable dtCheck = null;


            for (int f1 = intRow1; f1 < 4; f1++)
            {
                if (intRow1 == 2)
                {
                    intRow1 = 0;
                }
                #region ͷ��
                Pen penLine = new Pen(Brushes.Black, 1);
                curRowY += 50;
                curRowX = LeftWith;
                try
                {

                    dtCheckType.Columns.Add("tolMoney");
                }
                catch
                {
                    dtCheckType.Columns.RemoveAt(dtCheckType.Columns.Count - 1);
                    dtCheckType.Columns.Add("tolMoney");

                }
                SizeF tilWith = e.Graphics.MeasureString("�շ�Աҽ����ˢ���սᱨ��", m_fntTitle);
                string strTiteName = "";
                ArrayList arrCheck = new ArrayList();
                ArrayList arrReturn = new ArrayList();
                ArrayList arrBreck = new ArrayList();
                string totailMoney = "0";
                string minCheckDate = "0";
                string maxCheckDate = "0";
                DataTable dtCheckNo = new DataTable();
                ArrayList arr1 = new ArrayList();
                switch (f1)
                {
                    case 0:
                        strTiteName = "�շ�Ա�ֽ��սᱨ��";
                        dtCheck = dt1;
                        m_mthDe(ref dtCheckType, dtDe1);
                        m_mthCheckNO(1, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        clsMain.m_Detach(dtCheckNo, "CheckNo", out arr1);
                        break;
                    case 1:
                        strTiteName = "�շ�Աҽ����ˢ���սᱨ��";
                        dtCheck = dt2;
                        m_mthDe(ref dtCheckType, dtDe2);
                        m_mthCheckNO(2, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                    case 2:
                        strTiteName = "�շ�Ա�����սᱨ��";
                        dtCheck = dt3;
                        m_mthDe(ref dtCheckType, dtDe3);
                        m_mthCheckNO(3, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                    case 3:
                        strTiteName = "�շ�Ա�����սᱨ��";
                        dtCheck = dt4;
                        m_mthDe(ref dtCheckType, dtDe4);
                        m_mthCheckNO(4, out arrCheck, out arrReturn, out arrBreck, out totailMoney, out minCheckDate, out maxCheckDate, out dtCheckNo);
                        break;
                }
                e.Graphics.DrawString(strTiteName, m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, curRowY);
                curRowY += 25;
                e.Graphics.DrawString("ʵ�����ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("ʵ�����ڣ�", TextFont);
                curRowX += tilWith.Width;
                e.Graphics.DrawString(strCheckDate, TextFont, Brushes.Black, curRowX, curRowY);
                curRowX += 150;
                e.Graphics.DrawString("��Ʊ���ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
                tilWith = e.Graphics.MeasureString("��Ʊ���ڣ�", TextFont);
                curRowX += tilWith.Width;
                if (dtCheck.Rows.Count != 0)
                {
                    e.Graphics.DrawString(dtCheck.Rows[0]["minrecorddate_dat"].ToString() + " ~ " + dtCheck.Rows[0]["maxrecorddate_dat"].ToString(), TextFont, Brushes.Black, curRowX, curRowY);
                }
                curRowX += 300;
                e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, curRowX + 5, curRowY);
                tilWith = e.Graphics.MeasureString("��ӡ���ڣ� ", TextFont);
                string NowDate = DateTime.Now.ToShortDateString();
                e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, curRowX + tilWith.Width, curRowY);
                #endregion
                curRowX = LeftWith;
                curRowY += 15;
                float strLine = curRowY;

                #region �����
                for (int i1 = 0; i1 < 5; i1++)
                {
                    float tmpWith = 20;//X��
                    switch (i1)
                    {
                        case 0:
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            curRowY += 5;
                            e.Graphics.DrawString("�ϼƽ���д", TextFont, Brushes.Black, curRowX + 5, curRowY);
                            e.Graphics.DrawLine(penLine, curRowX + 100, strLine, curRowX + 100, curRowY + 20);
                            try
                            {
                                if (dtCheck.Rows[0]["totalmoney"].ToString() == "")
                                    dtCheck.Rows[0]["totalmoney"] = 0;

                                e.Graphics.DrawString(clsMain.CurrencyToString(Math.Abs(float.Parse(dtCheck.Rows[0]["totalmoney"].ToString()))), TextFont, Brushes.Black, curRowX + 105, curRowY);
                            }
                            catch
                            {

                            }

                            e.Graphics.DrawLine(penLine, PageWidth - 260, strLine, PageWidth - 260, curRowY + 20);
                            e.Graphics.DrawString("�ϼƽ��Сд", TextFont, Brushes.Black, PageWidth - 255, curRowY);
                            e.Graphics.DrawLine(penLine, PageWidth - 160, strLine, PageWidth - 160, curRowY + 20);

                            e.Graphics.DrawString("��" + dtCheck.Rows[0]["totalmoney"].ToString(), TextFont, Brushes.Black, PageWidth - 160, curRowY);
                            curRowY += 20;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 1:
                            #region ����շ���������
                            if (dtPayType.Rows.Count > 0)
                            {
                                tmpWith = LeftWith + 2;
                                curRowY += 10;
                                for (int f2 = 0; f2 < dtCheckType.Rows.Count; f2++)
                                {
                                    if (f2 % 6 == 0 && f2 != 0)
                                    {
                                        tmpWith = LeftWith + 2;
                                        curRowY += RowHight + 5;
                                    }
                                    tilWith = e.Graphics.MeasureString("��ӡ����", TextFont);
                                    if (dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length > 4)
                                    {
                                        string star = dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0, 4);
                                        string end = dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
                                        e.Graphics.DrawString(star, TextFont, Brushes.Black, tmpWith, curRowY - 5);
                                        e.Graphics.DrawString(end + "��", TextFont, Brushes.Black, tmpWith, curRowY + 10);

                                        if (dtCheckType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        {
                                            e.Graphics.DrawString(dtCheckType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(dtCheckType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);

                                        if (dtCheckType.Rows[f2]["tolMoney"].ToString() != "0.00")
                                        {
                                            e.Graphics.DrawString(dtCheckType.Rows[f2]["tolMoney"].ToString() + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                                        }
                                    }
                                    tmpWith += 130;
                                }
                            }
                            curRowY += RowHight;
                            #endregion
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 2:
                            curRowY += 50;
                            float temY = 0;
                            if (f1 == 1)
                            {

                                if (arr1.Count > 0)
                                {
                                    curRowY -= 50;
                                    int intRow = 0;
                                    int tolRow = 0;
                                    for (int f2 = 0; f2 < arr1.Count; f2++)
                                    {
                                        if (arr1[f2].ToString() == ",")
                                            tolRow++;
                                    }
                                    if (tolRow >= 4)
                                    {
                                        temY = 5;
                                    }
                                    else
                                    {
                                        temY = 10;
                                    }

                                    e.Graphics.DrawString(arr1[0].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                    int intCount = 0;
                                    for (int j2 = 0; j2 < arr1.Count; j2++)
                                    {

                                        if (intRow == 4 && intCount == 0)
                                        {
                                            intCount++;
                                            curRowY = temY + 10 + curRowY;
                                            tmpWith = 20;
                                        }
                                        try
                                        {
                                            if (arr1[j2].ToString() == ",")
                                            {
                                                intRow++;
                                                tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                                tmpWith += tilWith.Width;

                                                e.Graphics.DrawString("- " + arr1[j2 - 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                                if (j2 != arr1.Count - 1)
                                                {
                                                    tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                                    tmpWith += tilWith.Width;
                                                    e.Graphics.DrawString("," + arr1[j2 + 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    tilWith = e.Graphics.MeasureString("- " + arr1[0].ToString(), TextFont);
                                    tmpWith += tilWith.Width;
                                    e.Graphics.DrawString("- " + arr1[arr1.Count - 1].ToString(), TextFont, Brushes.Black, tmpWith, curRowY + temY);
                                    curRowY += (intCount + 3) * temY;
                                }
                            }
                            else
                            {

                                float tempX;
                                float tempY;
                                if (arrCheck.Count > 0)
                                {
                                    curRowY -= 50;
                                    tempX = tmpWith;
                                    tempY = curRowY + 10;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrCheck.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrCheck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrCheck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString("," + arrCheck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrCheck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrCheck.Count && tempX + tilWith.Width >= PageWidth - RightWith)
                                        {
                                            RowCount++;
                                            tempX = tmpWith;
                                            tempY = curRowY + 15 * RowCount;

                                        }
                                    }
                                    curRowY = tempY + 20;
                                }

                            }
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 3:
                            try
                            {
                                float tempX = 0;
                                float tempY = 0;
                                e.Graphics.DrawString("��Ʊ��Ʊ��", TextFont, Brushes.Black, curRowX + 5, curRowY + 5);
                                e.Graphics.DrawLine(penLine, curRowX + 100, curRowY, curRowX + 100, curRowY + 20);

                                if (arrReturn.Count > 0)
                                {
                                    tempX = curRowX + 105;
                                    tempY = curRowY + 5;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrReturn.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrReturn[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrReturn[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {

                                            e.Graphics.DrawString("," + arrReturn[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrReturn[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrReturn.Count && tempX + tilWith.Width >= PageWidth - 300)
                                        {
                                            RowCount++;
                                            tempX = curRowX + 105;
                                            tempY = curRowY + 10 * RowCount;
                                        }
                                    }
                                }
                                e.Graphics.DrawLine(penLine, PageWidth - 350, curRowY, PageWidth - 350, curRowY + 20);
                                e.Graphics.DrawString("�ָ���Ʊ��", TextFont, Brushes.Black, PageWidth - 345, curRowY + 5);
                                e.Graphics.DrawLine(penLine, PageWidth - 250, curRowY, PageWidth - 250, curRowY + 20);

                                if (arrBreck.Count > 0)
                                {
                                    tempX = PageWidth - 255;
                                    tempY = curRowY + 5;
                                    int RowCount = 1;
                                    for (int k1 = 0; k1 < arrBreck.Count; k1++)
                                    {
                                        if (k1 == 0)
                                        {
                                            e.Graphics.DrawString(arrBreck[k1].ToString(), TextFont, Brushes.Black, tempX, tempY);
                                            tilWith = e.Graphics.MeasureString(arrBreck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString("," + arrBreck[k1].ToString(), TextFont, Brushes.Black, PageWidth - 200, tempY);
                                            tilWith = e.Graphics.MeasureString("," + arrBreck[k1].ToString(), TextFont);
                                            tempX += tilWith.Width;
                                        }
                                        if (k1 != arrBreck.Count && tempX + tilWith.Width >= PageWidth - RightWith)
                                        {
                                            RowCount++;
                                            tempX = PageWidth - 200;
                                            tempY = curRowY + 10 * RowCount;
                                        }
                                    }
                                    curRowY = tempY + 5;

                                }
                            }
                            catch
                            {
                                MessageBox.Show("2");
                            }

                            curRowY += 20;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                        case 4:

                            tmpWith = LeftWith + 4;
                            e.Graphics.DrawString("�ɿ��ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            tilWith = e.Graphics.MeasureString("�ɿ��ˣ�", TextFont);
                            tmpWith += tilWith.Width;
                            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, tmpWith, curRowY + 7);


                            tmpWith += 200;
                            e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            tmpWith += 200;
                            e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, tmpWith, curRowY + 7);
                            curRowY += RowHight;
                            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - RightWith, curRowY);
                            break;
                    }

                }
                e.Graphics.DrawLine(penLine, curRowX, strLine, curRowX, curRowY);
                e.Graphics.DrawLine(penLine, PageWidth - RightWith, strLine, PageWidth - RightWith, curRowY);
                if (f1 == 1)
                {
                    e.HasMorePages = true;
                    intRow1 = 2;
                    return;
                }
                #endregion
            }

        }
        #endregion

        #region ����
        string checkDate;
        public void CheckData()
        {
            long l = Domain.m_lngCheckData(this.m_objViewer.LoginInfo.m_strEmpID, out checkDate);
            if (l > 0)
            {
                this.m_objViewer.ctlDgFind.m_mthAppendRow();
                int intRowIndex = m_objViewer.ctlDgFind.RowCount - 1;
                this.m_objViewer.ctlDgFind[intRowIndex, 0] = checkDate;
                this.m_objViewer.ctlprintShow2.setDocument = this.m_objViewer.printDocument1;
                this.m_objViewer.ctlDgFind.m_mthSelectARow(intRowIndex);
                m_objViewer.ctlDgFind.CurrentCell = new DataGridCell(intRowIndex, 0);

            }
        }
        #endregion

        #region ��������
        DataTable dthistory = new DataTable();
        public void findhistory()
        {
            string startDate = this.m_objViewer.starDate.Value.ToShortDateString();
            string endDate = this.m_objViewer.EndDate.Value.ToShortDateString();
            string checkMan = "";
            if (this.m_objViewer.isDoctorDean == false)
                checkMan = this.m_objViewer.LoginInfo.m_strEmpID;
            else
            {
                if (this.m_objViewer.m_cboCheckMan.SelectItemValue != null)
                    checkMan = this.m_objViewer.m_cboCheckMan.SelectItemValue.ToString();
            }
            long lngRes = Domain.m_lngGetHistory(startDate, endDate, checkMan, out dthistory);
            this.m_objViewer.ctlDgFind.m_mthDeleteAllRow();
            if (lngRes == 1)
            {
                for (int i1 = 0; i1 < dthistory.Rows.Count; i1++)
                {
                    this.m_objViewer.ctlDgFind.m_mthAppendRow();
                    this.m_objViewer.ctlDgFind[i1, 0] = dthistory.Rows[i1]["BALANCE_DAT"].ToString();
                }
                this.m_objViewer.ctlDgFind.CurrentCell = new DataGridCell(0, 0);
                this.m_objViewer.ctlDgFind.m_mthSelectARow(0);
            }

        }
        #endregion

        public void dgSelect()
        {

            intcomand = 1;
            this.m_objViewer.ctlprintShow2.setDocument = this.m_objViewer.printDocument1;
        }

        public void Reset()
        {
            intcomand = 0;
            this.m_objViewer.ctlprintShow2.setDocument = this.m_objViewer.printDocument1;
        }
    }
}
