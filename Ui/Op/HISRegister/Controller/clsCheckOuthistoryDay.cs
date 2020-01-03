using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// frmCheckOuthistoryDay ��ժҪ˵����
    /// </summary>
    public class clsCheckOuthistoryDay : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCheckOuthistoryDay()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            HospitalTitle = this.m_objComInfo.m_strGetHospitalTitle();
        }
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmCheckOutHistoryDay m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmCheckOutHistoryDay)frmMDI_Child_Base_in;
        }
        #endregion

        private DataTable dtCheckOut = new DataTable();
        private DataTable dtPayType = new DataTable();
        public DataTable dtStatistics;//ͳ�Ʊ�
        clsDomainControl_Register Domain = new clsDomainControl_Register();
        private string strDate;
        string HospitalTitle = "";
        public void getData(string INTERNALFLAG, string checkOutName)
        {
            currRow = 0;
            totailNuber = 0;
            DataTable dtEmp;
            strDate = this.m_objViewer.m_CheckOuDate.Value.ToShortDateString();
            Domain.m_lngGetPayTypeAndCheckOutBetWeenDay(strDate, out dtPayType, out dtCheckOut, out dtEmp, INTERNALFLAG, checkOutName);
            #region ����һ��ͳ�Ʊ�
            dtStatistics = new DataTable();
            dtStatistics.Columns.Add("�ɿ���");
            dtStatistics.Columns.Add("��Ʊ��");
            dtStatistics.Columns.Add("��Ʊ���");
            dtStatistics.Columns.Add("��Ʊ��");
            dtStatistics.Columns.Add("��Ʊ���");
            dtStatistics.Columns.Add("�ָ�Ʊ��");
            dtStatistics.Columns.Add("�ָ����");
            dtStatistics.Columns.Add("��ЧƱ��");
            dtStatistics.Columns.Add("ʵ�ս��");
            dtStatistics.Columns.Add("ʵ���ֽ�");
            dtStatistics.Columns.Add("ˢ�����");
            dtStatistics.Columns.Add("֧Ʊ���");
            dtStatistics.Columns.Add("ҽ������");
            dtStatistics.Columns.Add("���Ѽ���");
            dtStatistics.Columns.Add("ҽ���˴�");
            dtStatistics.Columns.Add("�����˴�");
            dtStatistics.Columns.Add("�Է��˴�");
            dtStatistics.Columns.Add("�������ʽ��");

            dtStatistics.Columns.Add("�������ϼ�");
            dtStatistics.Columns.Add("IC�����ϼ�");
            dtStatistics.Columns.Add("��������");
            dtStatistics.Columns.Add("���ݼ���");
            dtStatistics.Columns.Add("��Ժ����");
            #endregion

            #region ͳ�Ƹ����շ����͵Ľ��
            dtPayType.Columns.Add("tolMoney");
            if (dtCheckOut.Rows.Count >= 0)
            {
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
            #endregion

            #region ͳ������
            if (dtEmp == null)
            {
                dtEmp = new DataTable();
                dtEmp.Columns.Add("BALANCEEMP_CHR");
                dtEmp.Columns.Add("LASTNAME_VCHR");
                if (this.m_objViewer.m_cboCheckMan.SelectItemText != "ȫ��")
                {
                    DataRow newRow = dtEmp.NewRow();
                    newRow["BALANCEEMP_CHR"] = this.m_objViewer.m_cboCheckMan.SelectItemValue.ToString();
                    newRow["LASTNAME_VCHR"] = this.m_objViewer.m_cboCheckMan.SelectItemText.ToString();
                    dtEmp.Rows.Add(newRow);
                }
            }
            DataRow AddRow = dtEmp.NewRow();
            AddRow["LASTNAME_VCHR"] = "ȫ���ϼ�";
            dtEmp.Rows.Add(AddRow);
            for (int f2 = 0; f2 < dtEmp.Rows.Count; f2++)
            {
                DataRow StatisticsRow = dtStatistics.NewRow();
                StatisticsRow["�ɿ���"] = dtEmp.Rows[f2]["LASTNAME_VCHR"].ToString();
                StatisticsRow["��Ʊ��"] = 0;
                StatisticsRow["��Ʊ���"] = 0.00;
                StatisticsRow["��Ʊ��"] = 0;
                StatisticsRow["��Ʊ���"] = 0.00;
                StatisticsRow["�ָ�Ʊ��"] = 0;
                StatisticsRow["�ָ����"] = 0.00;
                StatisticsRow["��ЧƱ��"] = 0;
                StatisticsRow["ʵ�ս��"] = 0.00;
                StatisticsRow["ʵ���ֽ�"] = 0.00;
                StatisticsRow["ˢ�����"] = 0.00;
                StatisticsRow["֧Ʊ���"] = 0.00;
                StatisticsRow["ҽ������"] = 0.00;
                StatisticsRow["���Ѽ���"] = 0.00;
                StatisticsRow["�������ʽ��"] = 0.00;
                StatisticsRow["ҽ���˴�"] = 0;
                StatisticsRow["�����˴�"] = 0;
                StatisticsRow["�Է��˴�"] = 0;
                StatisticsRow["�������ϼ�"] = 0;
                StatisticsRow["IC�����ϼ�"] = 0;
                StatisticsRow["��������"] = 0;
                StatisticsRow["���ݼ���"] = 0;
                StatisticsRow["��Ժ����"] = 0;
                DateTime startDateTime = new DateTime();
                DateTime endDateTime = new DateTime();
                if (dtEmp.Rows[f2]["LASTNAME_VCHR"].ToString() != "ȫ���ϼ�")
                {
                    if (dtCheckOut.Rows.Count > 0)
                    {

                        for (int i1 = 0; i1 < dtCheckOut.Rows.Count; i1++)
                        {
                            //-------------------



                            //ͳ�ƿ�Ʊ��,��Ʊ���
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "1" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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
                            //-------------------------------


                            #region �������˽��
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "3" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "4" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "5" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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

                            if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "3" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "0" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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

                            //if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="0"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="1"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="2"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="3"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="4"&&dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()!="5"&&dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim()==dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
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

                            //��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                    StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                        StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                    }
                                }


                            }
                            //--------------------


                            //�ָ�Ʊ��,�ָ����ϼ�
                            if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "3" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                    StatisticsRow["�ָ����"] = Convert.ToDouble(StatisticsRow["�ָ����"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                        StatisticsRow["�ָ����"] = Convert.ToDouble(StatisticsRow["�ָ����"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                    }
                                }


                            }
                            //-----------------



                            //ͳ���ֽ�ϼ�
                            if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "0" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["ʵ���ֽ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["ʵ���ֽ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                    }
                                }

                            }
                            //-----------------



                            //ˢ���ϼ�
                            if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "1" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["ˢ�����"] = Convert.ToDouble(StatisticsRow["ˢ�����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["ˢ�����"] = Convert.ToDouble(StatisticsRow["ˢ�����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                    }
                                }

                            }
                            //--------------

                            //֧Ʊ
                            if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "2" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["֧Ʊ���"] = Convert.ToDouble(StatisticsRow["֧Ʊ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["֧Ʊ���"] = Convert.ToDouble(StatisticsRow["֧Ʊ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                    }
                                }

                            }
                            //-----------------


                            //ҽ�����˽��˴�
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "2" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                    StatisticsRow["ҽ������"] = Convert.ToDouble(StatisticsRow["ҽ������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                        StatisticsRow["ҽ������"] = Convert.ToDouble(StatisticsRow["ҽ������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                    }
                                }


                            }
                            //-----------------


                            //���Ѽ��˽��˴�
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "1" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                    StatisticsRow["���Ѽ���"] = Convert.ToDouble(StatisticsRow["���Ѽ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                        StatisticsRow["���Ѽ���"] = Convert.ToDouble(StatisticsRow["���Ѽ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                    }
                                }



                            }
                            //-------------------


                            //�������ʽ��
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "1" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "2" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "3" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "4" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "5")
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                    }
                                }



                            }
                            //-------------------

                            //�Է��Ͻɽ��˴�
                            if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "0" && dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString().Trim() == dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString().Trim())
                            {
                                if (i1 == 0)
                                {
                                    StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
                                }
                                else
                                {
                                    if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
                                    }
                                }

                            }

                        }
                    }
                }
                else
                {
                    for (int i1 = 0; i1 < dtCheckOut.Rows.Count; i1++)
                    {
                        //-----------------

                        //ͳ�ƿ�Ʊ��,��Ʊ���
                        if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "1")
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
                        //--------------------


                        //��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
                        if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "2")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["��Ʊ��"] = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString()) + 1;
                                    StatisticsRow["��Ʊ���"] = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString()) - Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                            }


                        }
                        //------------------


                        //�ָ�Ʊ��,�ָ����ϼ�
                        if (dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim() == "3")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                StatisticsRow["�ָ����"] = Convert.ToDouble(StatisticsRow["�ָ����"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["�ָ�Ʊ��"] = Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString()) + 1;
                                    StatisticsRow["�ָ����"] = Convert.ToDouble(StatisticsRow["�ָ����"].ToString()) + Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
                                }
                            }


                        }
                        //-----------------


                        //ͳ���ֽ�ϼ�
                        if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "0")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["ʵ���ֽ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["ʵ���ֽ�"] = Convert.ToDouble(StatisticsRow["ʵ���ֽ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                            }

                        }
                        //--------------------


                        //ˢ���ϼ�
                        if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "1")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["ˢ�����"] = Convert.ToDouble(StatisticsRow["ˢ�����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["ˢ�����"] = Convert.ToDouble(StatisticsRow["ˢ�����"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                            }

                        }
                        //----------------------


                        //֧Ʊ
                        if (dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim() == "2")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["֧Ʊ���"] = Convert.ToDouble(StatisticsRow["֧Ʊ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["֧Ʊ���"] = Convert.ToDouble(StatisticsRow["֧Ʊ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                            }

                        }
                        //---------------------------


                        //ҽ�����˽��˴�
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "2")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                StatisticsRow["ҽ������"] = Convert.ToDouble(StatisticsRow["ҽ������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["ҽ���˴�"] = Convert.ToInt32(StatisticsRow["ҽ���˴�"].ToString()) + 1;
                                    StatisticsRow["ҽ������"] = Convert.ToDouble(StatisticsRow["ҽ������"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }


                        }
                        //--------------------


                        //���Ѽ��˽��˴�
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "1")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                StatisticsRow["���Ѽ���"] = Convert.ToDouble(StatisticsRow["���Ѽ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["�����˴�"] = Convert.ToInt32(StatisticsRow["�����˴�"].ToString()) + 1;
                                    StatisticsRow["���Ѽ���"] = Convert.ToDouble(StatisticsRow["���Ѽ���"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }



                        }

                        //--------------------


                        //�������ʽ��
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "1" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "2" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "3" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "4" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "5")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["�������ʽ��"] = Convert.ToDouble(StatisticsRow["�������ʽ��"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
                                }
                            }



                        }

                        //-----------------------


                        //�Է��Ͻɽ��˴�
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() == "0")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["�Է��˴�"] = Convert.ToInt32(StatisticsRow["�Է��˴�"].ToString()) + 1;
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
                        if (dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "0" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "1" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "2" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "3" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "4" && dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim() != "5")
                        {
                            if (i1 == 0)
                            {
                                StatisticsRow["�������ϼ�"] = Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                            }
                            else
                            {
                                if (dtCheckOut.Rows[i1]["INVOICENO_VCHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["INVOICENO_VCHR"].ToString().Trim() && dtCheckOut.Rows[i1]["SEQID_CHR"].ToString().Trim() == dtCheckOut.Rows[i1 - 1]["SEQID_CHR"].ToString().Trim())
                                {

                                }
                                else
                                {
                                    StatisticsRow["�������ϼ�"] = Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString().Trim()) + Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
                                }
                            }

                        }

                        #endregion
                    }

                }
                int intAvailability = Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) - Convert.ToInt32(StatisticsRow["��Ʊ��"].ToString().Trim()) + Convert.ToInt32(StatisticsRow["�ָ�Ʊ��"].ToString().Trim());
                Double AvailabilityMoney = Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString().Trim()) - Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString().Trim()) + Convert.ToDouble(StatisticsRow["�ָ����"].ToString().Trim());
                StatisticsRow["��ЧƱ��"] = intAvailability.ToString();
                StatisticsRow["ʵ�ս��"] = AvailabilityMoney.ToString();
                dtStatistics.Rows.Add(StatisticsRow);

            }
            #endregion
        }

        public void printPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 15);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 11);//����ʹ�õ�����
            System.Drawing.Font TextFontBold = new Font("����", 11, System.Drawing.FontStyle.Bold);//����ʹ�õ�����(�Ӵ֣�
            const float RowHight = 25F;//��ĸ߶�
            const float LeftWith = 30F;//�����޽��ĳ���
            const float Uphight = 15F;//�����޽��ĳ���
            const float fontHight = 7;//���ڱ������ʾ��λ��
            float SaveStartHight = 0;
            #endregion

            #region ͷ��
            Pen penLine = new Pen(Brushes.Black, 1);
            curRowY = RowHight + Uphight + 10;
            curRowX += LeftWith;

            SizeF tilWith = e.Graphics.MeasureString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle);
            e.Graphics.DrawString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            e.Graphics.DrawString("�������ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("�������ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(strDate, TextFont, Brushes.Black, curRowX, curRowY);
            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, PageWidth - 250, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ�", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, PageWidth - 250 + tilWith.Width, curRowY);
            #endregion
            curRowX = LeftWith;
            curRowY += 18;

            #region �����
            float X1 = 0;
            float X2 = 0;
            float X3 = 0;
            float X4 = 0;
            float X5 = 0;
            float X6 = 0;
            float X7 = 0;
            float X8 = 0;
            for (int i1 = 0; i1 < dtStatistics.Rows.Count; i1++)
            {
                if (i1 == 0)
                {
                    SaveStartHight = curRowY;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawString("�� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("����������", TextFont);
                    curRowX += tilWith.Width;
                    X1 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��  Ʊ  ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X2 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�� Ʊ �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�� Ʊ �� ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X3 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("��Ʊ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��Ʊ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X4 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);


                    e.Graphics.DrawString("��Ʊ���", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��Ʊ���", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X5 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�ָ�Ʊ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�ָ�Ʊ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X6 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�ָ����", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�ָ����", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X7 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("��ЧƱ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��ЧƱ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X8 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("     ʵ �� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("     ʵ �� �� ��", TextFont);
                }

                if (i1 == dtStatistics.Rows.Count - 1)
                {
                    curRowY += RowHight;
                    curRowX = LeftWith;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight, PageWidth - LeftWith, curRowY + RowHight);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 2, PageWidth - LeftWith, curRowY + RowHight * 2);

                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ɿ���"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ʵ���ֽ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("�˴�ͳ��", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X1;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    //					e.Graphics.DrawString(dtStatistics.Rows[i1]["ҽ������"].ToString().Trim()+"Ԫ",TextFontBold,Brushes.Black,curRowX,curRowY+RowHight*2+fontHight);
                    curRowX += 2;
                    e.Graphics.DrawString("ҽ���˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    tilWith = e.Graphics.MeasureString("ҽ���˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["ҽ���˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    curRowX += 100;
                    e.Graphics.DrawString("�����˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    tilWith = e.Graphics.MeasureString("�����˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�����˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX += 100;
                    e.Graphics.DrawString("�Է��˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    tilWith = e.Graphics.MeasureString("�Է��˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�Է��˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    curRowX = X2;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ˢ �� �� ��", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X3;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ˢ�����"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X4;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("֧Ʊ���", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X5;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["֧Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X6;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�ָ����"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    curRowX = X7;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);

                    curRowX = X8;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);
                    e.Graphics.DrawString("���Ѽ���", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    tilWith = e.Graphics.MeasureString("���Ѽ��� ", TextFont);
                    e.Graphics.DrawLine(penLine, curRowX + tilWith.Width, curRowY + RowHight, curRowX + tilWith.Width, curRowY + RowHight * 2);
                    //					e.Graphics.DrawString(dtStatistics.Rows[i1]["���Ѽ���"].ToString().Trim()+"Ԫ",TextFontBold,Brushes.Black,curRowX+tilWith.Width+2,curRowY+RowHight+fontHight);
                    curRowX += 40;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ�ս��"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);

                    curRowY += RowHight * 3;
                    e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);

                }
                else
                {
                    curRowY += RowHight;
                    curRowX = LeftWith;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight, PageWidth - LeftWith, curRowY + RowHight);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 2, PageWidth - LeftWith, curRowY + RowHight * 2);

                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ɿ���"].ToString().Trim(), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ʵ���ֽ�", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X1;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X2;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ˢ �� �� ��", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X3;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ˢ�����"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X4;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("֧Ʊ���", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X5;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["֧Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X6;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�ָ����"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ҽ������", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X7;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ҽ������"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    curRowX = X8;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 2);
                    e.Graphics.DrawString("���Ѽ���", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    tilWith = e.Graphics.MeasureString("���Ѽ��� ", TextFont);
                    e.Graphics.DrawLine(penLine, curRowX + tilWith.Width, curRowY + RowHight, curRowX + tilWith.Width, curRowY + RowHight * 2);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["���Ѽ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX + tilWith.Width + 2, curRowY + RowHight + fontHight);
                    curRowX += 40;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ�ս��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    curRowY += RowHight * 2;
                }

            }
            #region ����շ���������
            if (dtPayType.Rows.Count > 0)
            {
                curRowY += RowHight;
                e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);
                curRowY += 7;
                float tmpWith = LeftWith + 2;
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
                        e.Graphics.DrawString("��" + Convert.ToDouble(dtPayType.Rows[f2]["tolMoney"].ToString()).ToString("0.00") + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                    }
                    else
                    {
                        e.Graphics.DrawString(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim() + "��", TextFont, Brushes.Black, tmpWith, curRowY);
                        e.Graphics.DrawString("��" + Convert.ToDouble(dtPayType.Rows[f2]["tolMoney"].ToString()).ToString("0.00") + "Ԫ", TextFont, Brushes.Black, tmpWith + tilWith.Width, curRowY);
                    }
                    tmpWith += 146;
                }
            }
            curRowY += RowHight;
            curRowX = LeftWith;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
            curRowX += 2;
            curRowY += 7;
            e.Graphics.DrawString("ͳ���ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ͳ���ˣ� ", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowY += RowHight - 7;
            e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);

            e.Graphics.DrawLine(penLine, LeftWith, SaveStartHight, LeftWith, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - LeftWith, SaveStartHight, PageWidth - LeftWith, curRowY);
            #endregion

            #endregion

        }
        public void printPageMB(System.Drawing.Printing.PrintPageEventArgs e, string INTERNALFLAG)
        {
            #region ����
            float PageWidth = e.PageBounds.Width;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ߶�
            float curRowY = 0;//��ǰ�е�Y����
            float curRowX = 0;//��ǰ�е�X����
            System.Drawing.Font m_fntTitle = new Font("����", 15);//����ʹ�õ�����
            System.Drawing.Font TextFont = new Font("����", 11);//����ʹ�õ�����
            System.Drawing.Font TextFontBold = new Font("����", 11, System.Drawing.FontStyle.Bold);//����ʹ�õ�����(�Ӵ֣�
            const float RowHight = 25F;//��ĸ߶�
            const float LeftWith = 30F;//�����޽��ĳ���
            const float Uphight = 15F;//�����޽��ĳ���
            const float fontHight = 7;//���ڱ������ʾ��λ��
            float SaveStartHight = 0;
            #endregion

            #region ͷ��
            Pen penLine = new Pen(Brushes.Black, 1);
            curRowY = RowHight + Uphight + 10;
            curRowX += LeftWith;
            SizeF tilWith = e.Graphics.MeasureString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle);
            if (INTERNALFLAG == "-1")
            {
                e.Graphics.DrawString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            }
            else if (INTERNALFLAG == "0")
            {
                e.Graphics.DrawString(HospitalTitle + "�շѴ��սᱨ��������", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            }
            else
            {
                e.Graphics.DrawString(HospitalTitle + "�շѴ��սᱨ����ᣩ", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            }
            e.Graphics.DrawString("�������ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("�������ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(strDate, TextFont, Brushes.Black, curRowX, curRowY);
            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, PageWidth - 250, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ�", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, PageWidth - 250 + tilWith.Width, curRowY);
            #endregion
            curRowX = LeftWith;
            curRowY += 18;

            #region �����
            float X1 = 0;
            float X2 = 0;
            float X3 = 0;
            float X4 = 0;
            float X5 = 0;
            float X6 = 0;
            float X7 = 0;
            float X8 = 0;
            for (int i1 = 0; i1 < dtStatistics.Rows.Count; i1++)
            {
                if (i1 == 0)
                {
                    SaveStartHight = curRowY;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawString("�� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("����������", TextFont);
                    curRowX += tilWith.Width;
                    X1 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��  Ʊ  ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X2 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�� Ʊ �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�� Ʊ �� ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X3 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�� Ʊ ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�� Ʊ ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X4 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);


                    e.Graphics.DrawString("�� Ʊ �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�� Ʊ �� ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X5 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("�ָ�Ʊ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("�ָ�Ʊ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X6 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString(" �ָ����", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString(" �ָ����", TextFont);
                    curRowX += 15 + tilWith.Width;
                    X7 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString("��ЧƱ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString("��ЧƱ��", TextFont);
                    curRowX += 2 + tilWith.Width;
                    X8 = curRowX;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    e.Graphics.DrawString(" ʵ �� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    tilWith = e.Graphics.MeasureString(" ʵ �� �� ��", TextFont);
                }

                if (i1 == dtStatistics.Rows.Count - 1)
                {
                    curRowY += RowHight;
                    curRowX = LeftWith;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight, PageWidth - LeftWith, curRowY + RowHight);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 2, PageWidth - LeftWith, curRowY + RowHight * 2);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 3, PageWidth - LeftWith, curRowY + RowHight * 3);

                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ɿ���"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ʵ���ֽ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("ҽ������", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    e.Graphics.DrawString("�˴�ͳ��", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    curRowX = X1;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 4);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ҽ������"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);



                    curRowX += 2;
                    e.Graphics.DrawString("ҽ���˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    tilWith = e.Graphics.MeasureString("ҽ���˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["ҽ���˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    curRowX += 100;
                    e.Graphics.DrawString("�����˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    tilWith = e.Graphics.MeasureString("�����˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�����˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);

                    curRowX += 100;
                    e.Graphics.DrawString("�Է��˴Σ�", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    tilWith = e.Graphics.MeasureString("�Է��˴Σ�", TextFontBold);
                    curRowX += tilWith.Width;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�Է��˴�"].ToString().Trim(), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 3 + fontHight);
                    curRowX = X2;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);



                    curRowX = X2;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);


                    curRowX = X3;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ˢ�����", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("���Ѽ���", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X4;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ˢ�����"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["���Ѽ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X5;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);


                    curRowX = X6;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�ָ����"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("֧Ʊ���", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��������", TextFontBold, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);


                    curRowX = X7;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() + "��", TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["֧Ʊ���"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight + RowHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�������ʽ��"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight + RowHight * 2);
                    curRowX = X8;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ�ս��"].ToString()).ToString("0.00"), TextFontBold, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);
                    curRowY += RowHight * 4;
                    e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);

                }
                else
                {
                    curRowY += RowHight;
                    curRowX = LeftWith;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight, PageWidth - LeftWith, curRowY + RowHight);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 2, PageWidth - LeftWith, curRowY + RowHight * 2);
                    e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 3, PageWidth - LeftWith, curRowY + RowHight * 3);
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ɿ���"].ToString().Trim(), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ʵ���ֽ�", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    //					e.Graphics.DrawString("���Ѽ���",TextFont,Brushes.Black,curRowX,curRowY+RowHight*2+fontHight);
                    e.Graphics.DrawString("ҽ������", TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    curRowX = X1;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ҽ������"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X2;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    curRowX = X3;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("ˢ�����", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("���Ѽ���", TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);


                    curRowX = X4;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ˢ�����"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["���Ѽ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
                    //					e.Graphics.DrawString("֧Ʊ���",TextFont,Brushes.Black,curRowX,curRowY+RowHight+fontHight);
                    curRowX = X5;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);

                    curRowX = X6;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�ָ����"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("֧Ʊ���", TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��������", TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X7;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

                    curRowX += 2;
                    e.Graphics.DrawString(dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() + "��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["֧Ʊ���"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�������ʽ��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

                    curRowX = X8;
                    e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);
                    //					e.Graphics.DrawString("���Ѽ���",TextFont,Brushes.Black,curRowX,curRowY+RowHight+fontHight);
                    //					tilWith= e.Graphics.MeasureString("���Ѽ��� ",TextFont);
                    e.Graphics.DrawLine(penLine, curRowX + tilWith.Width, curRowY + RowHight, curRowX + tilWith.Width, curRowY + RowHight);
                    //					curRowX+=40;
                    e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ�ս��"].ToString()).ToString("0.00"), TextFont, Brushes.Black, curRowX, curRowY + fontHight);
                    curRowY += RowHight * 3;
                }

            }
            #region ����շ���������
            if (dtPayType.Rows.Count > 0)
            {
                curRowY += RowHight;
                e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);
                curRowY += 7;
                float tmpWith = LeftWith + 2;
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
            curRowX = LeftWith;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
            curRowX += 2;
            curRowY += 7;
            e.Graphics.DrawString("ͳ���ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ͳ���ˣ� ", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowY += RowHight - 7;
            e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);

            e.Graphics.DrawLine(penLine, LeftWith, SaveStartHight, LeftWith, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - LeftWith, SaveStartHight, PageWidth - LeftWith, curRowY);
            #endregion

            #endregion

        }

        #region ��������
        Pen penLine = new Pen(Brushes.Black, 1);
        System.Drawing.Font m_fntTitle = new Font("����", 15);//����ʹ�õ�����
        System.Drawing.Font TextFont = new Font("����", 11);//����ʹ�õ�����
        System.Drawing.Font TextFontBold = new Font("����", 11, System.Drawing.FontStyle.Bold);//����ʹ�õ�����(�Ӵ֣�
        float curRowY = 0;//��ǰ�е�Y����
        float curRowX = 0;//��ǰ�е�X����

        const float RowHight = 25F;//��ĸ߶�
        const float LeftWith = 30F;//�����޽��ĳ���
        const float Uphight = 15F;//�����޽��ĳ���
        const float fontHight = 7;//���ڱ������ʾ��λ��
        SizeF tilWith;
        float SaveStartHight = 0;
        float X1 = 0;
        float X2 = 0;
        float X3 = 0;
        float X4 = 0;
        float X5 = 0;
        float X6 = 0;
        float X7 = 0;
        float X8 = 0;
        /// <summary>
        /// ҳ��
        /// </summary>
        int totailNuber = 0;
        /// <summary>
        /// ��ǰ�к�
        /// </summary>
        int currRow = 0;
        #endregion
        public void printPageFS(System.Drawing.Printing.PrintPageEventArgs e, string strINTERNALFLAG)
        {
            float PageWidth = e.PageBounds.Width - 2;//���ҳ��Ŀ��
            float PageHight = e.PageBounds.Height;//���ҳ��ĸ�
            if (totailNuber == 0)
            {
                m_mthPrintTitle(e, PageWidth, ref X1, ref X2, ref X3, ref X4, ref X5, ref X6, ref X7, ref X8);
            }
            bool blnFirst = true;
            for (int i1 = currRow; i1 < dtStatistics.Rows.Count; i1++)
            {
                #region alter at 2006-4-25 14:03
                if (curRowY + 18 + RowHight * 2 > PageHight - 20)
                {
                    totailNuber++;
                    e.Graphics.DrawLine(penLine, LeftWith, SaveStartHight, LeftWith, curRowY);
                    e.Graphics.DrawLine(penLine, PageWidth - LeftWith, SaveStartHight, PageWidth - LeftWith, curRowY);
                    curRowY = SaveStartHight - RowHight;
                    e.HasMorePages = true;
                    return;
                }
                #endregion
                if (blnFirst)
                {
                    curRowY += RowHight;
                    blnFirst = false;
                }
                else
                {
                    curRowY += RowHight / 2F;
                }
                curRowX = LeftWith;

                if (i1 == dtStatistics.Rows.Count - 1)
                {
                    m_mthPrintBaby(e, PageWidth, X1, X2, X3, X4, X5, X6, X7, X8, i1, TextFontBold);
                }
                else
                {
                    m_mthPrintBaby(e, PageWidth, X1, X2, X3, X4, X5, X6, X7, X8, i1, TextFont);
                }
                currRow = i1 + 1;

                #region alter at 2006-4-25 14:03
                //if(curRowY+18+RowHight*2>PageHight-20)
                //{
                //    totailNuber++;
                //    e.Graphics.DrawLine(penLine,LeftWith,SaveStartHight,LeftWith,curRowY);
                //    e.Graphics.DrawLine(penLine,PageWidth-LeftWith,SaveStartHight,PageWidth-LeftWith,curRowY);
                //    curRowY=SaveStartHight-RowHight;
                //    e.HasMorePages=true;
                //    return;
                //}
                #endregion
            }
            if (curRowY + 18 + RowHight * 13 > PageHight - 20)
            {
                totailNuber++;
                e.Graphics.DrawLine(penLine, LeftWith, SaveStartHight, LeftWith, curRowY);
                e.Graphics.DrawLine(penLine, PageWidth - LeftWith, SaveStartHight, PageWidth - LeftWith, curRowY);
                curRowY = SaveStartHight - RowHight;
                e.HasMorePages = true;
                return;
            }
            m_mthPrintEnd(e, PageWidth);

        }

        #region ��ӡͷ��
        private void m_mthPrintTitle(System.Drawing.Printing.PrintPageEventArgs e, float PageWidth, ref float X1, ref float X2, ref float X3, ref float X4, ref float X5, ref float X6, ref float X7, ref float X8)
        {
            curRowY = RowHight + Uphight + 10;
            curRowX = LeftWith;
            SizeF tilWith = e.Graphics.MeasureString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle);
            e.Graphics.DrawString(HospitalTitle + "�շѴ��սᱨ��", m_fntTitle, Brushes.Black, (PageWidth - tilWith.Width) / 2, Uphight);
            e.Graphics.DrawString("�������ڣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("�������ڣ�", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(this.m_objViewer.m_CheckOuDate.Value.ToShortDateString(), TextFont, Brushes.Black, curRowX, curRowY);

            e.Graphics.DrawString("��ӡ���ڣ�", TextFont, Brushes.Black, PageWidth - 250, curRowY);
            tilWith = e.Graphics.MeasureString("��ӡ���ڣ�", TextFont);
            string NowDate = DateTime.Now.ToShortDateString();
            e.Graphics.DrawString(NowDate, TextFont, Brushes.Black, PageWidth - 250 + tilWith.Width, curRowY);

            curRowX = LeftWith;
            curRowY += 18;

            SaveStartHight = curRowY;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
            e.Graphics.DrawString("�� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("����������", TextFont);
            curRowX += tilWith.Width;
            X1 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);
            curRowX += 2;
            e.Graphics.DrawString("��  Ʊ  ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("��  Ʊ  ��", TextFont);
            curRowX += 2 + tilWith.Width;
            X2 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString("  �� Ʊ �� ��  ", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("  �� Ʊ ��   ��  ", TextFont);
            curRowX += 2 + tilWith.Width;
            X3 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString("��Ʊ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("��Ʊ��", TextFont);
            curRowX += 2 + tilWith.Width;
            X4 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);


            e.Graphics.DrawString(" ��Ʊ��� ", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString(" ��Ʊ��� ", TextFont);
            curRowX += 2 + tilWith.Width;
            X5 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString("�ָ�Ʊ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("�ָ�Ʊ��", TextFont);
            curRowX += 2 + tilWith.Width;
            X6 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString(" �ָ����", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString(" �ָ����", TextFont);
            curRowX += 15 + tilWith.Width;
            X7 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString("��ЧƱ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString("��ЧƱ��", TextFont);
            curRowX += 2 + tilWith.Width;
            X8 = curRowX;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight);

            e.Graphics.DrawString(" ʵ �� �� ��", TextFont, Brushes.Black, curRowX, curRowY + fontHight);
            tilWith = e.Graphics.MeasureString(" ʵ �� �� ��", TextFont);
        }
        #endregion

        #region ��ӡ����
        private void m_mthPrintBaby(System.Drawing.Printing.PrintPageEventArgs e, float PageWidth, float X1, float X2, float X3, float X4, float X5, float X6, float X7, float X8, int i1, System.Drawing.Font font)
        {
            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
            e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight, PageWidth - LeftWith, curRowY + RowHight);
            e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 2, PageWidth - LeftWith, curRowY + RowHight * 2);
            e.Graphics.DrawLine(penLine, curRowX, curRowY + RowHight * 3, PageWidth - LeftWith, curRowY + RowHight * 3);
            e.Graphics.DrawString(dtStatistics.Rows[i1]["�ɿ���"].ToString().Trim(), font, Brushes.Black, curRowX, curRowY + fontHight);
            e.Graphics.DrawString("ʵ���ֽ�", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            //					e.Graphics.DrawString("���Ѽ���",font,Brushes.Black,curRowX,curRowY+RowHight*2+fontHight);
            e.Graphics.DrawString("���Ѽ���", font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            curRowX = X1;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() != "0")
            {
                e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", font, Brushes.Black, curRowX, curRowY + fontHight);
            }
            if (dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ���ֽ�"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            }
            if (dtStatistics.Rows[i1]["���Ѽ���"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["���Ѽ���"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            }

            curRowX = X2;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + fontHight);
            e.Graphics.DrawString("IC��", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            e.Graphics.DrawString("����", font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            if (dtStatistics.Rows[i1]["IC�����ϼ�"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["IC�����ϼ�"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX + e.Graphics.MeasureString("����", font).Width, curRowY + RowHight + fontHight);
            }
            if (dtStatistics.Rows[i1]["��������"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��������"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX + e.Graphics.MeasureString("����", font).Width, curRowY + RowHight * 2 + fontHight);
            }

            e.Graphics.DrawLine(penLine, curRowX + e.Graphics.MeasureString("����", font).Width, curRowY + RowHight, curRowX + e.Graphics.MeasureString("����", font).Width, curRowY + RowHight * 3);

            curRowX = X3;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() != "0")
                e.Graphics.DrawString(dtStatistics.Rows[i1]["��Ʊ��"].ToString().Trim() + "��", font, Brushes.Black, curRowX, curRowY + fontHight);
            e.Graphics.DrawString("���п�", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            e.Graphics.DrawString("��  ��", font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            curRowX = X4;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["��Ʊ���"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + "-" + Math.Abs(Convert.ToDouble(dtStatistics.Rows[i1]["��Ʊ���"].ToString())).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + fontHight);
            }
            if (dtStatistics.Rows[i1]["ˢ�����"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ˢ�����"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            }
            if (dtStatistics.Rows[i1]["���ݼ���"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["���ݼ���"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            }
            //					e.Graphics.DrawString("֧Ʊ���",font,Brushes.Black,curRowX,curRowY+RowHight+fontHight);
            curRowX = X5;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() != "0")
                e.Graphics.DrawString(dtStatistics.Rows[i1]["�ָ�Ʊ��"].ToString().Trim() + "��", font, Brushes.Black, curRowX, curRowY + fontHight);
            e.Graphics.DrawString("֧Ʊ֧��", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            e.Graphics.DrawString("��Ժ����", font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

            curRowX = X6;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["�ָ����"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�ָ����"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + fontHight);
            }
            if (dtStatistics.Rows[i1]["֧Ʊ���"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["֧Ʊ���"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            }
            if (dtStatistics.Rows[i1]["��Ժ����"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["��Ժ����"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);
            }
            curRowX = X7;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);

            curRowX += 2;
            if (dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() != "0")
                e.Graphics.DrawString(dtStatistics.Rows[i1]["��ЧƱ��"].ToString().Trim() + "��", font, Brushes.Black, curRowX, curRowY + fontHight);
            //��ɽ������֧��->��������
            e.Graphics.DrawString("��������", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            //e.Graphics.DrawString("����֧��", font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);

            System.Drawing.Font ybfont = new Font("����", 9, System.Drawing.FontStyle.Regular);
            e.Graphics.DrawString("�ض�ҽ������", ybfont, Brushes.Black, curRowX, curRowY + RowHight * 2 + fontHight);

            curRowX = X8;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, curRowX, curRowY + RowHight * 3);
            //					e.Graphics.DrawString("���Ѽ���",font,Brushes.Black,curRowX,curRowY+RowHight+fontHight);
            //					tilWith= e.Graphics.MeasureString("���Ѽ��� ",font);
            e.Graphics.DrawLine(penLine, curRowX + tilWith.Width, curRowY + RowHight, curRowX + tilWith.Width, curRowY + RowHight);
            //					curRowX+=40;
            if (dtStatistics.Rows[i1]["ʵ�ս��"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["ʵ�ս��"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + fontHight);
            }
            if (dtStatistics.Rows[i1]["�������ϼ�"].ToString() != "0")
            {
                e.Graphics.DrawString("��" + Convert.ToDouble(dtStatistics.Rows[i1]["�������ϼ�"].ToString()).ToString("0.00"), font, Brushes.Black, curRowX, curRowY + RowHight + fontHight);
            }

            ////�����ҽ�����˴�ӡ����������
            //double hj = Convert.ToDouble(dtStatistics.Rows[i1]["�������ʽ��"].ToString()) + Convert.ToDouble(dtStatistics.Rows[i1]["ҽ������"].ToString());
            //������������ʸ�Ϊҽ������(�ز�ҽ��)
            double hj = Convert.ToDouble(dtStatistics.Rows[i1]["ҽ������"].ToString());
            if (hj.ToString() != "0")
            {
                e.Graphics.DrawString("��" + hj.ToString("0.00"), font, Brushes.Black, curRowX, curRowY + 2 * RowHight + fontHight);
            }
            curRowY += RowHight * 3;
        }
        #endregion

        #region ��ӡ�շ���������
        private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs e, float PageWidth)
        {
            if (dtPayType.Rows.Count > 0)
            {
                curRowY += RowHight;
                e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);
                curRowY += 7;
                float tmpWith = LeftWith + 2;
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
            curRowX = LeftWith;
            e.Graphics.DrawLine(penLine, curRowX, curRowY, PageWidth - LeftWith, curRowY);
            curRowX += 2;
            curRowY += 7;
            e.Graphics.DrawString("ͳ���ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            tilWith = e.Graphics.MeasureString("ͳ���ˣ� ", TextFont);
            curRowX += tilWith.Width;
            e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName, TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("����ˣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowX += 200;
            e.Graphics.DrawString("���ɣ�", TextFont, Brushes.Black, curRowX, curRowY);
            curRowY += RowHight - 7;
            e.Graphics.DrawLine(penLine, LeftWith, curRowY, PageWidth - LeftWith, curRowY);

            e.Graphics.DrawLine(penLine, LeftWith, SaveStartHight, LeftWith, curRowY);
            e.Graphics.DrawLine(penLine, PageWidth - LeftWith, SaveStartHight, PageWidth - LeftWith, curRowY);
        }

        #endregion
    }
}
