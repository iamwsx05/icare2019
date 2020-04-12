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
using System.Collections.Generic;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class EntityReturn
    {
        public string invoNo { get; set; }
        public double invoMny { get; set; }
        public double retMny { get; set; }
    }

    public class clsCtlCheckOutOfDayNew : com.digitalwave.GUI_Base.clsController_Base
    {
        EntityReturn returnVo = null;
        List<EntityReturn> lstReturn = new List<EntityReturn>();

        public string[] m_strInvoArr = null;
        public decimal[] m_decInvoMny = null;
        DataTable dtCheckOut = new DataTable();
        DataTable dtDiffSum = new DataTable();
        DataTable dtPayType = new DataTable();
        clsDomainControl_Register m_objDomain = new clsDomainControl_Register();
        private DataTable dtStatistics = new DataTable();
        private DataRow StatisticsRow;
        private ArrayList SaveINVOICENO = new ArrayList();
        private double decSaveINVOICENO = 0;
        /// <summary>
        /// 0-δ����1-����
        /// </summary>
        int intcomand = 0;
        string strCheckDate = "";
        private ArrayList arrList;
        private ArrayList arrReList = new ArrayList();
        string strCheckManID = "";
        string hospitalNo = "";
        /// <summary>
        /// ҽ������id
        /// </summary>
        public System.Collections.Generic.List<string> lstYbPayTypeid = null;
        /// <summary>
        /// ��ɽ������������id
        /// </summary>
        public System.Collections.Generic.List<string> lstSpecialOpTypeid = null;
        /// <summary>
        /// ��ɽ��������������������id
        /// </summary>
        public System.Collections.Generic.List<string> lstSpecOpRetiredid = null;
        /// <summary>
        /// ������������ID
        /// </summary>
        string BirthPayTypeID { get; set; }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.Reports.frmCheckOutOfDayNew m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmCheckOutOfDayNew)frmMDI_Child_Base_in;
        }
        #endregion

        internal void GetData()
        {
            if (this.m_objViewer.m_rptId == null || this.m_objViewer.m_rptId == "")
            {
                MessageBox.Show("������Id��Ϊ�գ���ӹ��ܲ˵����뱨��Id�š�");
                return;
            }

            strCheckManID = this.m_objViewer.LoginInfo.m_strEmpID;
            hospitalNo = this.m_objComInfo.m_mthGetHospitalNo();
            string strPatientPayTypeID = string.Empty;

            arrReList.Clear();
            SaveINVOICENO.Clear();
            decSaveINVOICENO = 0;
            this.dtCheckOut = null;
            this.dtDiffSum = null;
            if (intcomand == 0)
            {
                string checkDate = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString();
                (new weCare.Proxy.ProxyReport()).Service.GetCheckOutData(strCheckManID, checkDate, this.m_objViewer.m_rptId, out dtCheckOut, out dtDiffSum);
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError("his " + strCheckDate + " " + strCheckManID + " " + this.m_objViewer.m_rptId);
                (new weCare.Proxy.ProxyReport()).Service.m_mthGetbalancerepeatinvoinfo(strCheckManID, checkDate, out this.m_strInvoArr, intcomand, out m_decInvoMny);
                strCheckDate = checkDate;
                this.m_objViewer.btnPrint.Enabled = false;
            }
            else
            {
                strCheckDate = this.m_objViewer.ctlDgFind[this.m_objViewer.ctlDgFind.CurrentCell.RowNumber, 0].ToString();
                string BALANCEEMP = this.m_objViewer.LoginInfo.m_strEmpID;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError("his " + strCheckDate + " " + strCheckManID + " " + this.m_objViewer.m_rptId);
                (new weCare.Proxy.ProxyReport()).Service.GetCheckOutHistory(strCheckDate, strCheckManID, this.m_objViewer.m_rptId, out dtCheckOut, out dtDiffSum);
                (new weCare.Proxy.ProxyReport()).Service.m_mthGetbalancerepeatinvoinfo(strCheckManID, strCheckDate, out this.m_strInvoArr, intcomand, out m_decInvoMny);
                this.m_objViewer.btnCheck.Enabled = false;
                this.m_objViewer.btnPrint.Enabled = true;
            }

            arrList = new ArrayList();
            DataView dv = new DataView(dtCheckOut);
            dv.RowFilter = "STATUS_INT = 1";
            dv.Sort = "INVOICENO_VCHR";
            clsMain.m_Detach(dv.ToTable(), "INVOICENO_VCHR", out arrList);

            #region ����һ��ͳ�Ʊ�
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
            dtStatistics.Columns.Add("�籣Ԥ�ƷѼ���");
            dtStatistics.Columns.Add("�ض��������");
            dtStatistics.Columns.Add("�����籣����");
            dtStatistics.Columns.Add("ҩƷ����");
            dtStatistics.Columns.Add("��������");
            dtStatistics.Columns.Add("΢�źϼ�");
            dtStatistics.Columns.Add("֧�����ϼ�");
            dtStatistics.Columns.Add("΢��2�ϼ�");
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

            StatisticsRow["΢�źϼ�"] = 0.00;
            StatisticsRow["֧�����ϼ�"] = 0.00;
            StatisticsRow["΢��2�ϼ�"] = 0.00;

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
            StatisticsRow["�籣Ԥ�ƷѼ���"] = 0;
            StatisticsRow["�ض��������"] = 0;
            StatisticsRow["�����籣����"] = 0;
            StatisticsRow["��������"] = 0;
            StatisticsRow["��һ�ŷ�Ʊʱ��"] = "";
            StatisticsRow["���һ�ŷ�Ʊʱ��"] = "";
            double returnMoney = 0;
            lstReturn.Clear();
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
                            StatisticsRow["��Ʊ��"] = Convert.ToInt16(StatisticsRow["��Ʊ��"].ToString()) + 1;
                            if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                            {
                                //Double diff = string.IsNullOrEmpty(dtCheckOut.Rows[i1]["diffPriceSum"].ToString()) ? 0 : Convert.ToDouble(dtCheckOut.Rows[i1]["diffPriceSum"].ToString());
                                StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["��Ʊ��"] = Convert.ToInt16(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                                {
                                    //Double diff = string.IsNullOrEmpty(dtCheckOut.Rows[i1]["diffPriceSum"].ToString()) ? 0 : Convert.ToDouble(dtCheckOut.Rows[i1]["diffPriceSum"].ToString());

                                    StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString());
                                }
                            }
                        }

                    }

                    //-------------------------


                    //��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                    if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")//��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                    {
                        returnVo = new EntityReturn();
                        if (i1 == 0)
                        {
                            StatisticsRow["��Ʊ��"] = Convert.ToInt16(StatisticsRow["��Ʊ��"].ToString()) + 1;
                            if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                            {
                                Double diff = string.IsNullOrEmpty(dtCheckOut.Rows[i1]["diffPriceSum"].ToString()) ? 0 : Convert.ToDouble(dtCheckOut.Rows[i1]["diffPriceSum"].ToString());
                                StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());//��������
                                returnMoney = returnMoney - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()) + diff;
                                decSaveINVOICENO = returnMoney;
                                returnVo.invoMny = Math.Abs(Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString())) + diff;
                                returnVo.retMny = Math.Abs(diff);
                            }
                            else
                            {
                                StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString());
                                decSaveINVOICENO = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString());
                                returnVo.invoMny = Math.Abs(Convert.ToDouble(decSaveINVOICENO));
                            }
                            SaveINVOICENO.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                            // 
                            returnVo.invoNo = dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString();
                            lstReturn.Add(returnVo);
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["��Ʊ��"] = Convert.ToInt16(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                                {
                                    //StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());//��Ʊ����ȥ ��Ʊ
                                    Double diff = string.IsNullOrEmpty(dtCheckOut.Rows[i1]["diffPriceSum"].ToString()) ? 0 : Convert.ToDouble(dtCheckOut.Rows[i1]["diffPriceSum"].ToString());
                                    StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());//��������
                                    returnMoney = returnMoney - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()) + diff;
                                    decSaveINVOICENO = returnMoney;

                                    returnVo.invoMny = Math.Abs(Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString())) + diff;
                                    returnVo.retMny = Math.Abs(diff);
                                }
                                else
                                {
                                    StatisticsRow["��Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString());
                                    decSaveINVOICENO = Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString());
                                    returnVo.invoMny = Math.Abs(decSaveINVOICENO);
                                }
                                SaveINVOICENO.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());
                                //
                                returnVo.invoNo = dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString();
                                lstReturn.Add(returnVo);
                            }
                        }
                    }
                    //--------------------------

                    //�ָ�Ʊ��,�ָ����ϼ�
                    if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "3")//�ָ�Ʊ��,�ָ����ϼ�
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt16(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                            if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                            {
                                StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                            else
                            {
                                StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString());
                            }
                            arrReList.Add(dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt16(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                if (!string.IsNullOrEmpty(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString()))
                                {
                                    StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                                else
                                {
                                    StatisticsRow["�ָ����ϼ�"] = Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString());
                                }
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
                            StatisticsRow["֧Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["֧Ʊ���ϼ�"] = Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }
                    }

                    // ΢�źϼ�
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "8")
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["΢�źϼ�"] = Convert.ToDouble(StatisticsRow["΢�źϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["΢�źϼ�"] = Convert.ToDouble(StatisticsRow["΢�źϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }
                    }
                    // ֧�����ϼ�
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "6")        //"9") 6 -- �ֳ�֧���� ; 9 -- ����֧����
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["֧�����ϼ�"] = Convert.ToDouble(StatisticsRow["֧�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["֧�����ϼ�"] = Convert.ToDouble(StatisticsRow["֧�����ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }

                    }
                    // ΢��2�ϼ�
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "5")
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["΢��2�ϼ�"] = Convert.ToDouble(StatisticsRow["΢��2�ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());

                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["΢��2�ϼ�"] = Convert.ToDouble(StatisticsRow["΢��2�ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                        }
                    }
                    //------------------

                    //�籣Ԥ�ƷѼ���
                    #region �籣Ԥ�ƷѼ���
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "4")//�籣Ԥ�Ʒ�
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["�籣Ԥ�ƷѼ���"] = Convert.ToDouble(StatisticsRow["�籣Ԥ�ƷѼ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                StatisticsRow["�籣Ԥ�ƷѼ���"] = Convert.ToDouble(StatisticsRow["�籣Ԥ�ƷѼ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }

                        //�Ը��ֽ�Ҫ�ӵ� "ʵ���ֽ�"
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
                    #endregion
                    //------------------

                    strPatientPayTypeID = dtCheckOut.Rows[i1]["PAYTYPEID_CHR"].ToString().Trim();
                    //�ض��������
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() != "4" && (lstSpecialOpTypeid.Contains(dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim()) || lstSpecOpRetiredid.Contains(dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim())))
                    {
                        if (i1 == 0)
                        {
                            string strTemp = StatisticsRow["�ض��������"].ToString().Trim();
                            StatisticsRow["�ض��������"] = Convert.ToDouble(StatisticsRow["�ض��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                string strTemp = StatisticsRow["�ض��������"].ToString().Trim();
                                StatisticsRow["�ض��������"] = Convert.ToDouble(StatisticsRow["�ض��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                    }
                    //------------------

                    //�����籣����
                    if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() != "4" && lstYbPayTypeid.Contains(dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim()) && !lstSpecialOpTypeid.Contains(dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim()) && !lstSpecOpRetiredid.Contains(dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim()))
                    {
                        if (i1 == 0)
                        {
                            StatisticsRow["�����籣����"] = Convert.ToDouble(StatisticsRow["�����籣����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            if (!string.IsNullOrEmpty(BirthPayTypeID) && BirthPayTypeID == dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim())
                            {
                                StatisticsRow["��������"] = Convert.ToDouble(StatisticsRow["��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                        }
                        else
                        {
                            if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                            {

                            }
                            else
                            {
                                string strTemp = StatisticsRow["�����籣����"].ToString().Trim();
                                StatisticsRow["�����籣����"] = Convert.ToDouble(StatisticsRow["�����籣����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                if (!string.IsNullOrEmpty(BirthPayTypeID) && BirthPayTypeID == dtCheckOut.Rows[i1]["paytypeid_chr"].ToString().Trim())
                                {
                                    StatisticsRow["��������"] = Convert.ToDouble(StatisticsRow["��������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
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
                    if (this.hospitalNo == "00001")
                    {
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
                    }
                    else
                    {
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "1" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "2" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "4" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "5")
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

                    ////���������֧����>��������
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

                    #endregion
                }
            }

            if (dtDiffSum != null && dtDiffSum.Rows.Count > 0)
            {
                StatisticsRow["ҩƷ����"] = dtDiffSum.Compute("sum(diffPriceSum)", "true");
                //if (!string.IsNullOrEmpty(dtDiffSum.Rows[0]["diffPriceSum"].ToString()))
                //{
                //    StatisticsRow["ҩƷ����"] = dtDiffSum.Rows[0]["diffPriceSum"].ToString();
                //}
                //else
                //{
                //    StatisticsRow["ҩƷ����"] = "0";
                //}
            }
            else
            {
                StatisticsRow["ҩƷ����"] = "0";
            }
            //---------------------

            //������ЧƱ��
            int intAvailability = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) - Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) + Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString().Trim());

            //������Ч���
            Double AvailabilityMoney = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString().Trim()) - Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString().Trim()) + Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString().Trim());

            StatisticsRow["��ЧƱ��"] = intAvailability.ToString();
            StatisticsRow["ʵ�ս��ϼ�"] = AvailabilityMoney.ToString();
            #endregion

            #region ������ʾ���ݴ���
            SetDatawindow(StatisticsRow, returnMoney.ToString());

            #endregion
        }

        private void SetDatawindow(DataRow StatisticsRow, string returnMoney)
        {
            this.m_objViewer.m_dwShow.Reset();
            this.m_objViewer.m_dwShow.SetRedrawOff();

            for (int i1 = 1; i1 < 13; i1++)
            {
                this.m_objViewer.m_dwShow.Modify("sum_" + i1.ToString().PadLeft(4, '0') + ".text = ' '");

            }

            int row;

            #region ��Ч��Ʊ��ϸ
            row = this.m_objViewer.m_dwShow.InsertRow(0);
            this.m_objViewer.m_dwShow.SetItemString(row, "row_head", "��ֹ��Ʊ���룺");
            string startNo = "";
            string invoiceNo = "";
            int invoiceCount = 0; //��Ʊ����

            if (this.arrList != null && this.arrList.Count > 0)
            {
                startNo = this.arrList[0].ToString();

                string strTemp;
                //int ivcountTemp = 0; //��¼��,ָʾ����
                decimal partsum = 0; //�ֶη�Ʊ�ϼƽ��
                DataRow[] drPartArr = null;
                DataRow[] drDiff = null;
                for (int i = 0; i < this.arrList.Count; i++)
                {
                    invoiceCount++;
                    strTemp = this.arrList[i].ToString();
                    drPartArr = dtCheckOut.Select("invoiceno_vchr = '" + strTemp + "'");
                    if (drPartArr.Length > 0)
                        partsum += clsPublic.ConvertObjToDecimal(drPartArr[0]["totalsum_mny"]) - clsPublic.ConvertObjToDecimal(drPartArr[0]["diffPriceSum"]);

                    if (strTemp == ",")
                    {
                        //ivcountTemp++;
                        //if (ivcountTemp == 2)
                        //{
                        //    invoiceNo += startNo + " - " + this.arrList[i - 1].ToString() + "(" + partsum + ") \r";
                        //    ivcountTemp = 0;
                        //}
                        //else
                        //{
                        invoiceNo += startNo + " - " + this.arrList[i - 1].ToString() + "(��" + partsum + "), ";
                        //}
                        partsum = 0; //���������
                        invoiceCount--;

                        if (i != this.arrList.Count - 1)
                        {
                            startNo = this.arrList[i + 1].ToString();
                        }
                    }
                    else if (i == this.arrList.Count - 1)
                    {
                        invoiceNo += startNo + " - " + this.arrList[i].ToString() + "(��" + partsum + ")";
                    }
                }

                this.m_objViewer.m_dwShow.SetItemString(row, "invoice_no", invoiceNo);
                this.m_objViewer.m_dwShow.SetItemString(row, "invoice_count", "������" + invoiceCount.ToString());

            }
            #endregion

            string temp = "";
            int countTemp = 0;
            double retMnyDiff = 0;
            if (lstReturn.Count > 0)
            {
                foreach (EntityReturn item in lstReturn)
                {
                    temp += item.invoNo + "(��" + item.invoMny + ")" + ",";
                    retMnyDiff += item.retMny;
                }
                temp = temp.TrimEnd(',');
            }


            //��ʾ��Ʊʱ�� yongdian.luo
            this.m_objViewer.m_dwShow.Modify("t_chectdate.text = '" + StatisticsRow["��һ�ŷ�Ʊʱ��"].ToString().Trim() + "��" + StatisticsRow["���һ�ŷ�Ʊʱ��"].ToString().Trim() + "'");

            this.m_objViewer.m_dwShow.Modify("sum_total.text = '��" + (Math.Abs(Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString())) - Math.Abs(Convert.ToDouble(StatisticsRow["ҩƷ����"])) - Math.Abs(retMnyDiff)).ToString() + "'");
            // this.m_objViewer.m_dwShow.Modify("sum_reimbursement.text = '��" + StatisticsRow["��Ʊ���ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("sum_reimbursement.text = '��" + returnMoney + "'");
            this.m_objViewer.m_dwShow.Modify("sum_recover.text = '��" + StatisticsRow["�ָ����ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("sum_real.text = '��" + (Convert.ToDouble(StatisticsRow["ʵ�ս��ϼ�"].ToString()) - Convert.ToDouble(StatisticsRow["ҩƷ����"].ToString())).ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("count_real.text = '" + StatisticsRow["��ЧƱ��"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_diffpricesum.text = '��-" + StatisticsRow["ҩƷ����"].ToString() + "'");
            float tolMoney = float.Parse((Convert.ToDouble(StatisticsRow["ʵ�ս��ϼ�"].ToString()) - Convert.ToDouble(StatisticsRow["ҩƷ����"].ToString())).ToString());
            string strMoney = clsMain.CurrencyToString(Math.Abs(tolMoney));
            this.m_objViewer.m_dwShow.Modify("sum_realupper.text = '" + strMoney + "'");

            double acctSum = Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString())
                             + Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString())
                             + Convert.ToDouble(StatisticsRow["��������"].ToString())
                             + Convert.ToDouble(StatisticsRow["���ݼ���"].ToString())
                             + Convert.ToDouble(StatisticsRow["��Ժ����"].ToString());
            //+ Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString());

            this.m_objViewer.m_dwShow.Modify("sum_cash.text = '��" + StatisticsRow["ʵ���ֽ�ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("sum_ic.text = '��" + StatisticsRow["IC�����ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("sum_bankcard.text = '��" + StatisticsRow["ˢ�����ϼ�"].ToString() + "'");
            //this.m_objViewer.m_dwShow.Modify("sum_acct.text = '��" + acctSum.ToString("0.00") + "'");
            //this.m_objViewer.m_dwShow.Modify("sum_acctspes.text = '��" + StatisticsRow["ҽ�����˽��"].ToString() + "'");
            //this.m_objViewer.m_dwShow.Modify("sum_other.text = '��" + StatisticsRow["֧Ʊ���ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("sum_acct.text = '��" + StatisticsRow["�籣Ԥ�ƷѼ���"].ToString() + "'");
            //this.m_objViewer.m_dwShow.Modify("sum_acctspes.text = '��" + StatisticsRow["�ض��������"].ToString() + "'");
            //���ض�������˽��ͳ�Ƶ������籣���ˣ�2010-01-18��ɽҪ��
            this.m_objViewer.m_dwShow.Modify("sum_acctspes.text = '��" + 0 + "'");
            double dblTempSum_Other = Convert.ToDouble(StatisticsRow["�����籣����"].ToString().Trim()) + Convert.ToDouble(StatisticsRow["�ض��������"].ToString().Trim());
            double birthSum = Convert.ToDouble(StatisticsRow["��������"].ToString().Trim());
            if (!string.IsNullOrEmpty(BirthPayTypeID) && lstYbPayTypeid.IndexOf(BirthPayTypeID) >= 0)
            {
                dblTempSum_Other -= birthSum;
                this.m_objViewer.m_dwShow.Modify("sum_sybx.text = '��" + birthSum.ToString() + "'");
            }
            this.m_objViewer.m_dwShow.Modify("sum_other.text = '��" + dblTempSum_Other.ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_zhipiao.text = '��" + StatisticsRow["֧Ʊ���ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_wx.text = '��" + StatisticsRow["΢�źϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_zfb.text = '��" + StatisticsRow["֧�����ϼ�"].ToString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_wx2.text = '��" + StatisticsRow["΢��2�ϼ�"].ToString() + "'");

            //for (int i = 0; i < SaveINVOICENO.Count; i++)
            //{
            //    temp += SaveINVOICENO[i].ToString();
            //    if (i != SaveINVOICENO.Count - 1)
            //    {
            //        temp += ", ";
            //    }
            //}
            //if (!string.IsNullOrEmpty(temp))
            //{
            //    temp += "(��" + decSaveINVOICENO.ToString() + ")";
            //}

            //this.m_objViewer.m_dwShow.Modify("t_reimbursementinv.text = '" + temp + "'");
            //this.m_objViewer.m_dwShow.Modify("count_reimbursement.text = '" + StatisticsRow["��Ʊ��"].ToString() + "'");
            row = this.m_objViewer.m_dwShow.InsertRow(0);
            this.m_objViewer.m_dwShow.SetItemString(row, "row_head", "�˿Ʊ���룺");
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_no", temp);
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_count", "������" + StatisticsRow["��Ʊ��"].ToString());

            temp = "";
            countTemp = 0;
            for (int i = 0; i < arrReList.Count; i++)
            {
                temp += arrReList[i].ToString();
                if (i != arrReList.Count - 1)
                {
                    temp += ", ";
                }
            }
            row = this.m_objViewer.m_dwShow.InsertRow(0);
            this.m_objViewer.m_dwShow.SetItemString(row, "row_head", "�ָ���Ʊ���룺");
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_no", temp);
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_count", "������" + StatisticsRow["�ָ�Ʊ��"].ToString());

            int invoNum = 0;
            string invoNo = string.Empty;
            if (m_strInvoArr != null)
            {
                invoNum = m_strInvoArr.Length;
                decimal invoMny = 0;
                string invoNo1 = string.Empty;
                string invoNo2 = string.Empty;
                List<string> lstInvoNo = new List<string>();
                Dictionary<string, decimal> dicInv = new Dictionary<string, decimal>();
                for (int i = 0; i < m_strInvoArr.Length; i++)
                {
                    int pos = m_strInvoArr[i].IndexOf("(");
                    if (pos > 0)
                    {
                        lstInvoNo.Add(m_strInvoArr[i].Substring(0, pos));
                        dicInv.Add(m_strInvoArr[i].Substring(0, pos), m_decInvoMny[i]);
                    }
                    else
                    {
                        lstInvoNo.Add(m_strInvoArr[i]);
                        dicInv.Add(m_strInvoArr[i], m_decInvoMny[i]);
                    }
                }
                invoNo = string.Empty;
                for (int i = 0; i < lstInvoNo.Count; i++)
                {
                    if (i > 0)
                    {
                        //if (lstInvoNo[i].Substring(4, 6) == Convert.ToString(Convert.ToDecimal(lstInvoNo[i - 1].Substring(4, 6)) + 1))
                        if (Convert.ToDecimal(lstInvoNo[i].Substring(4, 6)).ToString() == Convert.ToString(Convert.ToDecimal(lstInvoNo[i - 1].Substring(4, 6)) + 1))
                        {
                            invoMny += dicInv[lstInvoNo[i]];
                            invoNo2 = lstInvoNo[i];
                        }
                        else
                        {
                            if (invoNo1 == invoNo2)
                            {
                                invoNo += invoNo1 + "(��" + invoMny + "), ";
                            }
                            else
                            {
                                invoNo += invoNo1 + "-" + invoNo2 + "(��" + invoMny + "), ";
                            }
                            invoNo1 = lstInvoNo[i];
                            invoNo2 = invoNo1;
                            invoMny = dicInv[invoNo1];
                        }
                    }
                    else
                    {
                        invoNo1 = lstInvoNo[i];
                        invoNo2 = invoNo1;
                        invoMny = dicInv[invoNo1];
                    }
                }
                if (invoNo1 == invoNo2)
                {
                    invoNo += invoNo1 + "(��" + invoMny + ")";
                }
                else
                {
                    invoNo += invoNo1 + "-" + invoNo2 + "(��" + invoMny + ")";
                }

                #region bak
                //for (int i = 0; i < m_strInvoArr.Length; i++)
                //{
                //    if (i > 0)
                //    {
                //        if (m_strInvoArr[i].Substring(2, 8) == Convert.ToString(Convert.ToDecimal(m_strInvoArr[i - 1].Substring(2, 8)) + 1))
                //        {
                //            invoMny += m_decInvoMny[i];
                //        }
                //        else
                //        {
                //            invoNo = invoNo.TrimEnd(',') + "(��" + invoMny + "), ";
                //            invoMny = m_decInvoMny[i];
                //        }
                //    }
                //    else
                //    {
                //        invoMny += m_decInvoMny[i];
                //    }
                //    invoNo += m_strInvoArr[i] + ", ";
                //}
                //invoNo = invoNo.TrimEnd(',') + "(��" + invoMny + ")";
                #endregion
            }
            row = this.m_objViewer.m_dwShow.InsertRow(0);
            this.m_objViewer.m_dwShow.SetItemString(row, "row_head", "�ش�Ʊ���룺");
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_no", invoNo);
            this.m_objViewer.m_dwShow.SetItemString(row, "invoice_count", "������" + invoNum);

            this.m_objViewer.m_dwShow.Modify("t_yyname.text = '" + this.m_objComInfo.m_strGetHospitalTitle() + "'");
            this.m_objViewer.m_dwShow.Modify("t_date.text = '" + Convert.ToDateTime(this.strCheckDate).ToShortDateString() + "'");
            this.m_objViewer.m_dwShow.Modify("t_dept.text = '" + this.m_objViewer.LoginInfo.m_strdepartmentName + "'");
            this.m_objViewer.m_dwShow.Modify("t_operator.text = '" + this.m_objViewer.LoginInfo.m_strEmpName + "'");

            #region ����շ���������
            if (dtCheckOut.Rows.Count > 0)
            {
                DataView dv = new DataView(dtCheckOut);
                string code;
                string name;
                double tempMoney;

                for (int i1 = 1; i1 < 13; i1++)
                {
                    code = i1.ToString().PadLeft(4, '0');
                    dv.RowFilter = "groupid_chr = '" + code + "'";
                    if (dv.Count > 0)
                    {
                        tempMoney = 0;
                        name = dv[0]["groupname_chr"].ToString().Trim();
                        for (int i2 = 0; i2 < dv.Count; i2++)
                        {
                            tempMoney += Convert.ToDouble(dv[i2]["tolfee_mny"].ToString());
                        }
                        this.m_objViewer.m_dwShow.Modify("t_" + code + ".text = '" + name + "'");
                        this.m_objViewer.m_dwShow.Modify("sum_" + code + ".text = '" + tempMoney.ToString("0.00") + "'");
                    }
                    else
                    {
                        this.m_objViewer.m_dwShow.Modify("sum_" + code + ".text = ' '");
                    }
                }
            }

            #endregion
            this.m_objViewer.m_dwShow.AcceptText();
            this.m_objViewer.m_dwShow.SetRedrawOn();
            this.m_objViewer.m_dwShow.Refresh();
        }

        #region ����
        string checkDate;
        internal void CheckData()
        {
            long l = this.m_objDomain.m_lngCheckData(this.m_objViewer.LoginInfo.m_strEmpID, out checkDate);
            if (l > 0)
            {
                this.m_objViewer.ctlDgFind.m_mthAppendRow();
                int intRowIndex = m_objViewer.ctlDgFind.RowCount - 1;
                this.m_objViewer.ctlDgFind[intRowIndex, 0] = checkDate;
                //this.m_objViewer.ctlprintShow2.setDocument = this.m_objViewer.printDocument1;
                this.m_objViewer.ctlDgFind.m_mthSelectARow(intRowIndex);
                this.m_objViewer.btnCheck.Enabled = false;
                this.m_objViewer.btnPrint.Enabled = true;
                //ѡ������н��˼�¼
                this.m_objViewer.ctlDgFind.CurrentCell = new DataGridCell(intRowIndex, 0);
            }
        }
        #endregion

        internal void dgSelect()
        {
            this.intcomand = 1;
            GetData();
        }

        #region ��������
        DataTable dthistory = new DataTable();
        internal void FindHistory()
        {
            string startDate = this.m_objViewer.starDate.Value.ToShortDateString();
            string endDate = this.m_objViewer.EndDate.Value.ToShortDateString();
            string checkMan;

            checkMan = this.m_objViewer.LoginInfo.m_strEmpID;

            long lngRes = this.m_objDomain.m_lngGetHistory(startDate, endDate, checkMan, out dthistory);
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

        internal void Reset()
        {
            this.m_objViewer.m_dwShow.Modify("datawindow.print.preview=yes datawindow.print.preview.rulers=yes");
            this.intcomand = 0;
            GetData();
        }

        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        public void m_mthGetParameters()
        {
            System.Collections.Generic.Dictionary<string, string> hasParamValue = null;
            string[] strParamKeyArr = new string[] { "0001", "0069", "0073", "0084" };
            //com.digitalwave.iCare.middletier.HIS.Reports.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Reports.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Reports.clsDoctorWorkStationSvc));
            (new weCare.Proxy.ProxyOP()).Service.m_lngGetSysparm(strParamKeyArr, out hasParamValue);
            string[] strTypeid = null;
            if (hasParamValue.ContainsKey("0001"))
            {
                strTypeid = hasParamValue["0001"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (strTypeid == null || strTypeid.Length == 0)
                {
                    lstYbPayTypeid = new System.Collections.Generic.List<string>();
                }
                else
                {
                    lstYbPayTypeid = new System.Collections.Generic.List<string>(strTypeid);
                }
            }
            if (hasParamValue.ContainsKey("0069"))
            {
                strTypeid = hasParamValue["0069"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (strTypeid == null || strTypeid.Length == 0)
                {
                    lstSpecialOpTypeid = new System.Collections.Generic.List<string>();
                }
                else
                {
                    lstSpecialOpTypeid = new System.Collections.Generic.List<string>(strTypeid);
                }
            }
            if (hasParamValue.ContainsKey("0073"))
            {
                strTypeid = hasParamValue["0073"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (strTypeid == null || strTypeid.Length == 0)
                {
                    lstSpecOpRetiredid = new System.Collections.Generic.List<string>();
                }
                else
                {
                    lstSpecOpRetiredid = new System.Collections.Generic.List<string>(strTypeid);
                }
            }
            if (hasParamValue.ContainsKey("0084"))
            {
                BirthPayTypeID = hasParamValue["0084"].ToString().Trim();
            }
        }
    }
}