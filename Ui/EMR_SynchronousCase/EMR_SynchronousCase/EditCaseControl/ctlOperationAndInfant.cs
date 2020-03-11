using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// ���������Ʒ���Ӥ����¼��
    /// </summary>
    public partial class ctlOperationAndInfant : UserControl, infSynchronousCaseControl
    {
        /// <summary>
        /// ���������Ʒ���Ӥ����¼��
        /// </summary>
        public ctlOperationAndInfant()
        {
            InitializeComponent();
            m_dgwInfant.AutoGenerateColumns = false;
            m_dgwOperation.AutoGenerateColumns = false;
        }

        #region infSynchronousCaseControl ��Ա
        /// <summary>
        /// �Ƿ��ѳ�ʼ��
        /// </summary>
        private bool m_blnHasInit = false;
        /// <summary>
        /// �Ƿ��ѳ�ʼ��
        /// </summary>
        public bool m_BlnHasInit
        {
            get
            {
                return m_blnHasInit;
            }
            set
            {
                m_blnHasInit = value;
            }
        }

        #region ��ʼ����������
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="p_dsContent">���ݿ��л�ȡ���ѱ�������</param>
        public void m_mthInitCase(DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                return;
            }

            DataTable dtbOP = new DataTable();
            dtbOP.Columns.Add("registerid_chr");
            dtbOP.Columns.Add("opcode");//����
            dtbOP.Columns.Add("opdate");//��������
            dtbOP.Columns.Add("opname");//��������
            dtbOP.Columns.Add("operator");//����
            dtbOP.Columns.Add("assistant1");//һ��
            dtbOP.Columns.Add("assistant2");//����
            dtbOP.Columns.Add("aanaesthesiamodeid");//����ʽ
            dtbOP.Columns.Add("cutlevel");//�пڵȼ�
            dtbOP.Columns.Add("anaesthetist");//����ҽ��
            dtbOP.Columns.Add("Ananame");//����ʽ
            dtbOP.Columns.Add("anadoctor");//����ҽ��
            dtbOP.Columns.Add("cutlevelid");//�п�ID
            dtbOP.Columns.Add("opdoctor");//����ID
            dtbOP.Columns.Add("opdoctorno");//
            dtbOP.Columns.Add("firstassist");
            dtbOP.Columns.Add("firstassistno");
            dtbOP.Columns.Add("secondassist");
            dtbOP.Columns.Add("secondassistno");
            dtbOP.Columns.Add("anadoctorno");
            dtbOP.Columns.Add("anacode");
            dtbOP.Columns.Add("operationlevel");//su.liang
            dtbOP.Columns.Add("operationelective");
            m_dgwOperation.DataSource = dtbOP;

            DataTable dtbLabor = new DataTable();
            dtbLabor.Columns.Add("male");
            dtbLabor.Columns.Add("female");
            dtbLabor.Columns.Add("liveborn");
            dtbLabor.Columns.Add("dieborn");
            dtbLabor.Columns.Add("dienotborn");
            dtbLabor.Columns.Add("infantweight");
            dtbLabor.Columns.Add("die");
            dtbLabor.Columns.Add("changedepartment");
            dtbLabor.Columns.Add("outhospital");
            dtbLabor.Columns.Add("suffocate2");
            dtbLabor.Columns.Add("naturalcondiction");
            dtbLabor.Columns.Add("suffocate1");
            dtbLabor.Columns.Add("infectiontimes");
            dtbLabor.Columns.Add("name");
            dtbLabor.Columns.Add("code");
            dtbLabor.Columns.Add("rescuetimes");
            dtbLabor.Columns.Add("rescuesucctimes");
            dtbLabor.Columns.Add("registerid_chr");
            dtbLabor.Columns.Add("seqid");
            dtbLabor.Columns.Add("sex");
            dtbLabor.Columns.Add("LaborResult");
            dtbLabor.Columns.Add("InfantResult");
            dtbLabor.Columns.Add("InfantBreath");
            m_dgwInfant.DataSource = dtbLabor;

            if (p_dsContent.Tables.Contains("HIS_BA4") && p_dsContent.Tables["HIS_BA4"].Rows.Count > 0)
            {
                DataTable dtbBA4= p_dsContent.Tables["HIS_BA4"];
                int intRowsCount = dtbBA4.Rows.Count;
                DataRow drTemp = null;
                dtbOP.BeginLoadData();
                DateTime dtmTemp = DateTime.MinValue;
                for (int iRow = 0; iRow < intRowsCount; iRow++)
                {
                    DataRow drNew = dtbOP.NewRow();
                    drTemp = dtbBA4.Rows[iRow];
                    drNew["opcode"] = drTemp["FOPCODE"].ToString();
                    drNew["opname"] = drTemp["FOP"].ToString();
                    if (DateTime.TryParse(drTemp["FOPDATE"].ToString(),out dtmTemp))
                    {
                        drNew["opdate"] = dtmTemp;
                    }
                    if (drTemp["FQIEKOU"] != DBNull.Value || drTemp["FYUHE"] != DBNull.Value)
                    {
                        drNew["cutlevel"] = drTemp["FQIEKOU"].ToString() + "/" + drTemp["FYUHE"].ToString();
                    }  
                    drNew["opdoctorno"] = drTemp["FDOCBH"].ToString();
                    drNew["opdoctor"] = drTemp["FDOCNAME"].ToString();
                    drNew["anacode"] = drTemp["FMAZUIBH"].ToString();
                    drNew["Ananame"] = drTemp["FMAZUI"].ToString();
                    drNew["firstassistno"] = drTemp["FOPDOCT1BH"].ToString();
                    drNew["firstassist"] = drTemp["FOPDOCT1"].ToString();
                    drNew["secondassistno"] = drTemp["FOPDOCT2BH"].ToString();
                    drNew["secondassist"] = drTemp["FOPDOCT2"].ToString();
                    drNew["anadoctorno"] = drTemp["FMZDOCTBH"].ToString();
                    drNew["anadoctor"] = drTemp["FMZDOCT"].ToString();
                    drNew["operationlevel"] = drTemp["FSSJB"].ToString();//su.liang
                    drNew["operationelective"] = drTemp["FZQSS"].ToString();
                    dtbOP.LoadDataRow(drNew.ItemArray, true);
                }
                dtbOP.EndLoadData();
            }

            if (p_dsContent.Tables.Contains("HIS_BA5") && p_dsContent.Tables["HIS_BA5"].Rows.Count > 0)
            {
                DataTable dtbBA5= p_dsContent.Tables["HIS_BA5"];
                int intRowsCount = dtbBA5.Rows.Count;
                DataRow drTemp = null;
                dtbLabor.BeginLoadData();
                DateTime dtmTemp = DateTime.MinValue;
                for (int iRow = 0; iRow < intRowsCount; iRow++)
                {
                    DataRow drNew = dtbLabor.NewRow();
                    drTemp = dtbBA5.Rows[iRow];
                    drNew["sex"] = drTemp["FBABYSEX"].ToString();
                    drNew["infantweight"] = drTemp["FTZ"].ToString();
                    drNew["InfantResult"] = drTemp["FZG"].ToString();
                    drNew["LaborResult"] = drTemp["FRESULT"].ToString();
                    drNew["code"] = "";// drTemp["FGRICD10"].ToString();
                    drNew["name"] = "";// drTemp["FGRNAME"].ToString();
                    drNew["infectiontimes"] = "";// drTemp["FBABYGR"].ToString();
                    drNew["rescuetimes"] = drTemp["FBABYQJ"].ToString();
                    drNew["rescuesucctimes"] = drTemp["FBABYSUC"].ToString();
                    drNew["InfantBreath"] = drTemp["FHX"].ToString();
                    dtbLabor.LoadDataRow(drNew.ItemArray, true);
                }
                dtbLabor.EndLoadData();
            }
            m_blnHasInit = true;
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strPatientID">����ID</param>
        public void m_mthInitCase(string p_strRegisterID, string p_strPatientID)
        {
            //��ȡ������Ϣ
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            DataTable dtbOP = null;
            long lngRes = objDomain.m_lngGetOperationInfo(p_strRegisterID, out dtbOP);
            if (dtbOP != null)
            {
                dtbOP.Columns.Add("anacode");
                if (dtbOP.Rows.Count > 0)
                {
                    DataRow drTemp = null;
                    int intRowsCount = dtbOP.Rows.Count;
                    for (int iRow = 0; iRow < intRowsCount; iRow++)
                    {
                        drTemp = dtbOP.Rows[iRow];
                        //drTemp["seqid"] = Convert.ToInt32(drTemp["seqid"]) + 1;
                        if (drTemp["operationlevel"].ToString() == "һ������")
                        {
                            drTemp["operationlevel"] = "һ��";
                        }
                        else if (drTemp["operationlevel"].ToString() == "��������")
                        {
                            drTemp["operationlevel"] = "����";
                        }
                        else if (drTemp["operationlevel"].ToString() == "��������")
                        {
                            drTemp["operationlevel"] = "����";
                        }
                        else if (drTemp["operationlevel"].ToString() == "�ļ�����")
                        {
                            drTemp["operationlevel"] = "�ļ�";
                        }
                        drTemp["operationelective"] = drTemp["operationelective"].ToString().Trim();
                        //if (drTemp["operationelective"].ToString() == "" || drTemp["operationelective"] == null)
                        //{
                        //    drTemp["operationelective"] = "��";
                        //}
                         if (drTemp["operationelective"].ToString() != "��" && drTemp["operationelective"].ToString() != "��")
                        {
                            drTemp["operationelective"] = "��";
                        }
                        
                    }
                    //DataRow drTemp2 = dtbOP.NewRow();
                    //drTemp2["opcode"] = "";//����
                    //drTemp2["opdate"] = "";//��
                    //drTemp2["opname"] = "";//��������
                    //drTemp2["operator"] = "";//����
                    //drTemp2["assistant1"] = "";//һ��
                    //drTemp2["assistant2"] = "";//����
                    //drTemp2["aanaesthesiamodeid"] = "";//����ʽ
                    //drTemp2["cutlevel"] = "";//�пڵȼ�
                    //drTemp2["anaesthetist"] = "";//����ҽ��
                    //drTemp2["ananame"] = "";//����ʽ
                    //drTemp2["anadoctor"] = "";//����ҽ��
                    //drTemp2["cutlevelid"] = "";//�п�ID
                    //drTemp2["operationlevel"] = "";//su.liang
                    //drTemp2["operationelective"] = "";
                    //drTemp2["opdoctor"] = "";//����ID
                    //drTemp2["opdoctorno"] = "";//
                    //drTemp2["firstassist"] = "";
                    //drTemp2["firstassistno"] = "";
                    //drTemp2["secondassist"] = "";
                    //drTemp2["secondassistno"] = "";
                    //drTemp2["anadoctorno"] = "";
                    //dtbOP.Rows.InsertAt(drTemp2, intRowsCount + 1);
                }
                //dtbOP.Rows.insertat(DataTable.NewRow,)
            }
            
            //m_dgwOperation.DataSource = "";
            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = dtbOP;
            //dgTaskList.DataSource = bindingSource;
            //dtbOP.Rows.Add(dtbOP.NewRow);
            m_dgwOperation.DataSource = dtbOP;// bindingSource;

            DataTable dtbLabor = null;
            lngRes = objDomain.m_lngLaborInfo(p_strRegisterID, out dtbLabor);
            if (dtbLabor != null)
            {
                dtbLabor.Columns.Add("sex");
                dtbLabor.Columns.Add("LaborResult");
                dtbLabor.Columns.Add("InfantResult");
                dtbLabor.Columns.Add("InfantBreath");

                if (dtbLabor.Rows.Count > 0)
                {
                    DataRow drTemp = null;
                    int intRowsCount = dtbLabor.Rows.Count;
                    for (int iRow = 0; iRow < intRowsCount; iRow++)
                    {
                        drTemp = dtbLabor.Rows[iRow];
                        drTemp["seqid"] = Convert.ToInt32(drTemp["seqid"]) + 1;
                        if (drTemp["male"].ToString() == "1")
                        {
                            drTemp["sex"] = "��";
                        }
                        else if (drTemp["female"].ToString() == "1")
                        {
                            drTemp["sex"] = "Ů";
                        }
                        if (drTemp["liveborn"].ToString() == "1")
                        {
                            drTemp["LaborResult"] = "���";
                        }
                        else if (drTemp["dieborn"].ToString() == "1")
                        {
                            drTemp["LaborResult"] = "����";
                        }
                        else if (drTemp["dienotborn"].ToString() == "1")
                        {
                            drTemp["LaborResult"] = "��̥";
                        }
                        if (drTemp["die"].ToString() == "1")
                        {
                            drTemp["InfantResult"] = "����";
                        }
                        else if (drTemp["changedepartment"].ToString() == "1")
                        {
                            drTemp["InfantResult"] = "ת��";
                        }
                        else if (drTemp["outhospital"].ToString() == "1")
                        {
                            drTemp["InfantResult"] = "��Ժ";
                        }
                        if (drTemp["NATURALCONDICTION"].ToString() == "1")
                        {
                            drTemp["InfantBreath"] = "��Ȼ";
                        }
                        else if (drTemp["suffocate1"].ToString() == "1")
                        {
                            drTemp["InfantBreath"] = "�����Ϣ";
                        }
                        else if (drTemp["suffocate2"].ToString() == "1")
                        {
                            drTemp["InfantBreath"] = "�����Ϣ";
                        }
                    }
                }                
            }
            m_dgwInfant.DataSource = dtbLabor;

            m_blnHasInit = true;
        } 
        #endregion

        #region �����ֵ��ʼ������̶�ѡ��ֵ
        /// <summary>
        /// �����ֵ��ʼ������̶�ѡ��ֵ
        /// </summary>
        /// <param name="p_dtbDict">�ֵ�</param>
        public void m_mthInitDict(DataTable p_dtbDict)
        {
            
            if (p_dtbDict == null || p_dtbDict.Rows.Count == 0)
            {
                return;
            }
            DataView drView = new DataView(p_dtbDict);
            //��������
            drView.RowFilter = "fcode='GBSSJB'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                ColOpLevel.Items.Clear();
                List<string> lstSsjb = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstSsjb.Add(drView[iM]["fmc"].ToString());
                }
                ColOpLevel.Items.AddRange(lstSsjb.ToArray());//ColOpLevel
            }
            //��������
            drView.RowFilter = "fcode='GBIFZQSS'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                ColOpzheqi.Items.Clear();
                List<string> lstZqss = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstZqss.Add(drView[iM]["fmc"].ToString());
                }
                ColOpzheqi.Items.AddRange(lstZqss.ToArray());//
            }
            //�Ա�
            drView.RowFilter = "fcode='GBSEX'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clmSex.Items.Clear();
                List<string> lstSex = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstSex.Add(drView[iM]["fmc"].ToString());
                }
                clmSex.Items.AddRange(lstSex.ToArray());
            }
            //������
            drView.RowFilter = "fcode='GBFMJG'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clmLaborResult.Items.Clear();
                List<string> lstItems = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstItems.Add(drView[iM]["fmc"].ToString());
                }
                clmLaborResult.Items.AddRange(lstItems.ToArray());
            }
            //Ӥ��ת��
            drView.RowFilter = "fcode='GBYEZG'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clmInfantResult.Items.Clear();
                List<string> lstItems = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstItems.Add(drView[iM]["fmc"].ToString());
                }
                clmInfantResult.Items.AddRange(lstItems.ToArray());
            }
            //Ӥ������
            drView.RowFilter = "fcode='GBYEHX'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clmInfantBreath.Items.Clear();
                List<string> lstItems = new List<string>(2);
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    lstItems.Add(drView[iM]["fmc"].ToString());
                }
                clmInfantBreath.Items.AddRange(lstItems.ToArray());
            }
        } 
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_dsCaseContent">��������</param>
        public void m_mthGetCaseContent(System.Data.DataSet p_dsCaseContent)
        {
            if (p_dsCaseContent == null)
            {
                p_dsCaseContent = new DataSet("CaseContent");
            }
            DataTable dtbBA4 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA4"))
            {
                dtbBA4 = new DataTable();
                dtbBA4.TableName = "HIS_BA4";
                dtbBA4.Columns.Add("fprn");
                dtbBA4.Columns.Add("FTIMES");
                dtbBA4.Columns.Add("FNAME");
                dtbBA4.Columns.Add("FOPTIMES");
                dtbBA4.Columns.Add("FOPCODE");
                dtbBA4.Columns.Add("FOP");
                dtbBA4.Columns.Add("FOPDATE");
                dtbBA4.Columns.Add("FQIEKOUBH");
                dtbBA4.Columns.Add("FQIEKOU");
                dtbBA4.Columns.Add("FYUHEBH");
                dtbBA4.Columns.Add("FYUHE");
                dtbBA4.Columns.Add("FDOCBH");
                dtbBA4.Columns.Add("FDOCNAME");
                dtbBA4.Columns.Add("FMAZUIBH");
                dtbBA4.Columns.Add("FMAZUI");
                dtbBA4.Columns.Add("FIFFSOP");
                dtbBA4.Columns.Add("FOPDOCT1BH");
                dtbBA4.Columns.Add("FOPDOCT1");
                dtbBA4.Columns.Add("FOPDOCT2BH");
                dtbBA4.Columns.Add("FOPDOCT2");
                dtbBA4.Columns.Add("FMZDOCTBH");
                dtbBA4.Columns.Add("FMZDOCT");
                dtbBA4.Columns.Add("FZQSSBH");
                dtbBA4.Columns.Add("FZQSS");
                dtbBA4.Columns.Add("FSSJBBH");
                dtbBA4.Columns.Add("FSSJB");
                dtbBA4.Columns.Add("FOPKSNAME");
                dtbBA4.Columns.Add("FOPTYKH");
                p_dsCaseContent.Tables.Add(dtbBA4);
            }
            else
            {
                dtbBA4 = p_dsCaseContent.Tables["HIS_BA4"];
            }

            DataTable dtbBA1 = p_dsCaseContent.Tables["HIS_BA1"];
            string strPRN = dtbBA1.Rows[0]["fprn"].ToString();
            string strTimes = dtbBA1.Rows[0]["FTIMES"].ToString();
            string strName = dtbBA1.Rows[0]["FNAME"].ToString();
            
            DataTable dtbOP = m_dgwOperation.DataSource as DataTable;
            DataRow drNew = null;
            DateTime dtmTemp = DateTime.MinValue;
            DataRow drTemp = null;
            if (dtbOP != null && dtbOP.Rows.Count > 0)
            {
                dtbBA1.Rows[0]["FIFSS"] = 1;
                int intOpTimes = 0;
                dtbBA4.BeginLoadData();
                for (int iRow = 0; iRow < dtbOP.Rows.Count; iRow++)
                {
                    drTemp = dtbOP.Rows[iRow];
                    //if (string.IsNullOrEmpty(drTemp["opcode"].ToString()))
                    //{
                    //    continue;
                    //}
                    drNew = dtbBA4.NewRow();
                    drNew["fprn"] = strPRN;
                    drNew["FTIMES"] = strTimes;
                    drNew["FNAME"] = strName;
                    drNew["FOPTIMES"] = ++intOpTimes;
                    drNew["FOPCODE"] = drTemp["opcode"].ToString();
                    drNew["FOP"] = drTemp["opname"].ToString();
                    if (DateTime.TryParse(drTemp["opdate"].ToString(), out dtmTemp))
                    {
                        drNew["FOPDATE"] = dtmTemp;
                    }
                    #region �п��������
                    string[] strCut = drTemp["cutlevel"].ToString().Split('/');
                    if (strCut != null && strCut.Length == 2)
                    {
                        drNew["FQIEKOU"] = strCut[0];
                        drNew["FYUHE"] = strCut[1];
                        if (strCut[0] == "��")
                        {
                            drNew["FQIEKOUBH"] = "1";
                        }
                        else if (strCut[0] == "��")
                        {
                            drNew["FQIEKOUBH"] = "2";
                        }
                        else if (strCut[0] == "��" || strCut[0] == "III")//����ͬ����������("III"��Ӧ��"1")   junming.zhang
                        {
                            drNew["FQIEKOUBH"] = "3";
                        }
                        else
                        {
                            drNew["FQIEKOUBH"] = "1";
                        }
                        if (strCut[1] == "��")
                        {
                            drNew["FYUHEBH"] = "1";
                        }
                        else if (strCut[1] == "��")
                        {
                            drNew["FYUHEBH"] = "2";
                        }
                        else if (strCut[1] == "��")
                        {
                            drNew["FYUHEBH"] = "3";
                        }                       
                        else
                        {
                            drNew["FYUHEBH"] = "4";
                        }
                    }
                    #endregion
                    drNew["FDOCBH"] = drTemp["opdoctorno"].ToString();
                    drNew["FDOCNAME"] = drTemp["opdoctor"].ToString();
                    drNew["FMAZUIBH"] = drTemp["anacode"].ToString();
                    drNew["FMAZUI"] = drTemp["Ananame"].ToString();
                    drNew["FIFFSOP"] = false;
                    drNew["FOPDOCT1BH"] = drTemp["firstassistno"].ToString();
                    drNew["FOPDOCT1"] = drTemp["firstassist"].ToString();
                    drNew["FOPDOCT2BH"] = drTemp["secondassistno"].ToString();
                    drNew["FOPDOCT2"] = drTemp["secondassist"].ToString();
                    drNew["FMZDOCTBH"] = drTemp["anadoctorno"].ToString();
                    drNew["FMZDOCT"] = drTemp["anadoctor"].ToString();
                    /////////////new
                    if (drTemp["operationelective"].ToString() == "��")
                        drNew["FZQSSBH"] = "1";
                    else
                        drNew["FZQSSBH"] = "0";
                    drNew["FZQSS"] = drTemp["operationelective"].ToString();
                    if (drTemp["operationlevel"].ToString() == "һ������")
                    {
                        drNew["FSSJBBH"] = "1";
                        drNew["FSSJB"] = "һ��";
                    }
                    else if (drTemp["operationlevel"].ToString() == "��������")
                    {
                        drNew["FSSJBBH"] = "2";
                        drNew["FSSJB"] = "����";
                    }
                    else if (drTemp["operationlevel"].ToString() == "��������")
                    {
                        drNew["FSSJBBH"] = "3";
                        drNew["FSSJB"] = "����";
                    }
                    else if (drTemp["operationlevel"].ToString() == "�ļ�����")
                    {
                        drNew["FSSJBBH"] = "4";
                        drNew["FSSJB"] = "�ļ�";
                    }
                    
                    drNew["FOPKSNAME"] = "";
                    drNew["FOPTYKH"] = "";// drTemp["anadoctor"].ToString();
                    dtbBA4.LoadDataRow(drNew.ItemArray, true);
                }
                dtbBA4.EndLoadData();
            }
            else
            {
                dtbBA1.Rows[0]["FIFSS"] = 0;
            }

            DataTable dtbBA5 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA5"))
            {
                dtbBA5 = new DataTable();
                dtbBA5.TableName = "HIS_BA5";
                dtbBA5.Columns.Add("fprn");
                dtbBA5.Columns.Add("FTIMES");
                dtbBA5.Columns.Add("FBABYNUM");
                dtbBA5.Columns.Add("FNAME");
                dtbBA5.Columns.Add("FBABYSEXBH");
                dtbBA5.Columns.Add("FBABYSEX");
                dtbBA5.Columns.Add("FTZ");
                dtbBA5.Columns.Add("FRESULTBH");
                dtbBA5.Columns.Add("FRESULT");
                dtbBA5.Columns.Add("FZGBH");
                dtbBA5.Columns.Add("FZG");
                //dtbBA5.Columns.Add("FGRICD10");
                //dtbBA5.Columns.Add("FGRNAME");
                //dtbBA5.Columns.Add("FBABYGR");
                dtbBA5.Columns.Add("FBABYQJ");
                dtbBA5.Columns.Add("FBABYSUC");
                dtbBA5.Columns.Add("FHXBH");
                dtbBA5.Columns.Add("FHX");
                p_dsCaseContent.Tables.Add(dtbBA5);
            }
            else
            {
                dtbBA5 = p_dsCaseContent.Tables["HIS_BA5"];
            }

            DataTable dtbInfant = m_dgwInfant.DataSource as DataTable;
            if (dtbInfant != null && dtbInfant.Rows.Count > 0)
            {
                dtbBA1.Rows[0]["FIFFYK"] = 1;
                dtbBA1.Rows[0]["FBABYNUM"] = dtbBA1.Rows.Count;

                dtbBA5.BeginLoadData();
                int intTemp = 0;
                for (int iRow = 0; iRow < dtbInfant.Rows.Count; iRow++)
                {
                    drTemp = dtbInfant.Rows[iRow];
                    drNew = dtbBA5.NewRow();
                    drNew["fprn"] = strPRN;
                    drNew["FTIMES"] = strTimes;
                    drNew["FNAME"] = strName;
                    drNew["FBABYNUM"] = iRow + 1;
                    if (drTemp["sex"].ToString() == "��")
                    {
                        drNew["FBABYSEXBH"] = "1";
                    }
                    else if (drTemp["sex"].ToString() == "Ů")
                    {
                        drNew["FBABYSEXBH"] = "2";
                    }
                    drNew["FBABYSEX"] = drTemp["sex"].ToString();
                    drNew["FTZ"] = drTemp["infantweight"].ToString();
                    if (drTemp["LaborResult"].ToString() == "���")
                    {
                        drNew["FRESULTBH"] = "1";
                    }
                    else if (drTemp["LaborResult"].ToString() == "����")
                    {
                        drNew["FRESULTBH"] = "2";
                    }
                    else if (drTemp["LaborResult"].ToString() == "��̥")
                    {
                        drNew["FRESULTBH"] = "3";
                    }
                    drNew["FRESULT"] = drTemp["LaborResult"].ToString();
                    if (drTemp["InfantResult"].ToString() == "����")
                    {
                        drNew["FZGBH"] = "1";
                    }
                    else if (drTemp["InfantResult"].ToString() == "ת��")
                    {
                        drNew["FZGBH"] = "2";
                    }
                    else if (drTemp["InfantResult"].ToString() == "��Ժ")
                    {
                        drNew["FZGBH"] = "3";
                    }
                    drNew["FZG"] = drTemp["InfantResult"].ToString();
                    //drNew["FGRICD10"] = drTemp["code"].ToString();
                    //drNew["FGRNAME"] = drTemp["name"].ToString();
                    //if (int.TryParse(drTemp["infectiontimes"].ToString(),out intTemp))
                    //{
                    //    drNew["FBABYGR"] = intTemp;
                    //}
                    //else
                    //{
                    //    drNew["FBABYGR"] = 0;
                    //}
                    if (int.TryParse(drTemp["rescuetimes"].ToString(),out intTemp))
                    {
                        drNew["FBABYQJ"] = intTemp;
                    }
                    else
                    {
                        drNew["FBABYQJ"] = 0;
                    }
                    if (int.TryParse(drTemp["rescuesucctimes"].ToString(),out intTemp))
                    {
                        drNew["FBABYSUC"] = intTemp;
                    }
                    else
                    {
                        drNew["FBABYSUC"] = 0;
                    }
                    if (drTemp["InfantBreath"].ToString() == "��Ȼ")
                    {
                        drNew["FHXBH"] = "1";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "�����Ϣ")
                    {
                        drNew["FHXBH"] = "2";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "�����Ϣ")
                    {
                        drNew["FHXBH"] = "3";
                    }
                    drNew["FHX"] = drTemp["InfantBreath"].ToString();
                    dtbBA5.LoadDataRow(drNew.ItemArray, true);
                }
                dtbBA5.EndLoadData();
            }
            else
            {
                dtbBA1.Rows[0]["FIFFYK"] = 0;
            }
        } 
        #endregion
        #endregion

        private void m_cmdAddOP_Click(object sender, EventArgs e)
        {
            DataTable dtbICD = m_dgwOperation.DataSource as DataTable;
            if (dtbICD != null && dtbICD.Columns.Count > 0)
            {
                DataRow drNew = dtbICD.NewRow();
                dtbICD.Rows.Add(drNew);
            }
            else
            {
                dtbICD = new DataTable();
                dtbICD.Columns.Add("registerid_chr");
                dtbICD.Columns.Add("opcode");
                dtbICD.Columns.Add("opname");
                dtbICD.Columns.Add("opdate");
                dtbICD.Columns.Add("operator");
                dtbICD.Columns.Add("assistant1");
                dtbICD.Columns.Add("assistant2");
                dtbICD.Columns.Add("aanaesthesiamodeid");
                dtbICD.Columns.Add("cutlevel");
                dtbICD.Columns.Add("anaesthetist");
                dtbICD.Columns.Add("Ananame");
                dtbICD.Columns.Add("anacode");
                dtbICD.Columns.Add("anadoctor");
                dtbICD.Columns.Add("cutlevelid");
                dtbICD.Columns.Add("opdoctor");
                dtbICD.Columns.Add("firstassist");
                dtbICD.Columns.Add("secondassist");
                m_dgwOperation.DataSource = dtbICD;
            }
            m_dgwOperation.CurrentCell = m_dgwOperation.Rows[m_dgwOperation.Rows.Count - 1].Cells[0];
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            bool blnHasGetICD = objDomain.m_blnAddOperationToDataGridView(m_dgwOperation);
            if (!blnHasGetICD)
            {
                dtbICD.Rows.RemoveAt(dtbICD.Rows.Count - 1);
            }
            objDomain = null;
        }

        private void m_cmdDeleteOP_Click(object sender, EventArgs e)
        {
            if (m_dgwOperation.SelectedCells.Count > 0)
            {
                DataRowView drCurrent = m_dgwOperation.Rows[m_dgwOperation.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
                DataTable dtbICD = m_dgwOperation.DataSource as DataTable;
                dtbICD.Rows.Remove(drCurrent.Row);
            }
        }

        private void m_cmdAddInfant_Click(object sender, EventArgs e)
        {
            DataTable dtbICD = m_dgwInfant.DataSource as DataTable;
            if (dtbICD == null || dtbICD.Columns.Count == 0)
            {
                dtbICD = new DataTable();
                dtbICD.Columns.Add("male");
                dtbICD.Columns.Add("female");
                dtbICD.Columns.Add("liveborn");
                dtbICD.Columns.Add("dieborn");
                dtbICD.Columns.Add("dienotborn");
                dtbICD.Columns.Add("infantweight");
                dtbICD.Columns.Add("die");
                dtbICD.Columns.Add("changedepartment");
                dtbICD.Columns.Add("outhospital");
                dtbICD.Columns.Add("suffocate2");
                dtbICD.Columns.Add("naturalcondiction");
                dtbICD.Columns.Add("suffocate1");
                dtbICD.Columns.Add("infectiontimes");
                dtbICD.Columns.Add("infectionname");
                dtbICD.Columns.Add("infectionicd");
                dtbICD.Columns.Add("rescuetimes");
                dtbICD.Columns.Add("rescuesucctimes");
                dtbICD.Columns.Add("registerid_chr");
                dtbICD.Columns.Add("seqid");
                dtbICD.Columns.Add("sex");
                dtbICD.Columns.Add("LaborResult");
                dtbICD.Columns.Add("InfantResult");
                dtbICD.Columns.Add("InfantBreath");
                m_dgwInfant.DataSource = dtbICD;
            }

            DataRow drNew = dtbICD.NewRow();
            dtbICD.Rows.Add(drNew);
            drNew["seqid"] = dtbICD.Rows.Count;
        }

        private void m_cmdDeleteInfant_Click(object sender, EventArgs e)
        {
            if (m_dgwInfant.SelectedCells.Count > 0)
            {
                DataRowView drCurrent = m_dgwInfant.Rows[m_dgwInfant.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
                DataTable dtbICD = m_dgwInfant.DataSource as DataTable;
                dtbICD.Rows.Remove(drCurrent.Row);

                for (int iRow = 0; iRow < dtbICD.Rows.Count; iRow++)
                {
                    dtbICD.Rows[iRow]["seqid"] = iRow+1;
                }
            }
        }

        private void m_dgwOperation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //ȡ�ñ���ʾ�Ŀؼ�
            DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
            if (tb == null)
            {
                return; 
            }

            //�¼�������ɾ��
            tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_OP_KeyDown);
            tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_EMP_KeyDown);
            tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_ANA_KeyDown);

            //����Ӧ��
            if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clmOpCode" || m_dgwOperation.CurrentCell.OwningColumn.Name == "clmOpName")
            {//����
                // KeyDown�¼�������׷��
                tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_OP_KeyDown);
            }
            else if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clmOperator" || m_dgwOperation.CurrentCell.OwningColumn.Name == "clm1stAssistant"
                || m_dgwOperation.CurrentCell.OwningColumn.Name == "clm2ndAssistant" || m_dgwOperation.CurrentCell.OwningColumn.Name == "clmAnaDoctor")
            {//Ա��
                // KeyDown�¼�������׷��
                tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_EMP_KeyDown);
            }
            else if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clmAnaName")
            {//������
                // KeyDown�¼�������׷��
                tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_ANA_KeyDown);
            }
        }

        private void DataGridViewTextBoxCell_OP_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl CurrentCell = sender as System.Windows.Forms.DataGridViewTextBoxEditingControl;
            if (e.KeyCode == Keys.F9 && CurrentCell.EditingControlDataGridView.SelectedCells.Count > 0)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_blnAddOperationToDataGridView(CurrentCell.EditingControlDataGridView);
                objDomain = null;
            }
        }

        private void DataGridViewTextBoxCell_ANA_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl CurrentCell = sender as System.Windows.Forms.DataGridViewTextBoxEditingControl;
            if (e.KeyCode == Keys.F9 && CurrentCell.EditingControlDataGridView.SelectedCells.Count > 0)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_blnAddAnaToDataGridView(CurrentCell.EditingControlDataGridView);
                objDomain = null;
            }
        }

        private void DataGridViewTextBoxCell_EMP_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl CurrentCell = sender as System.Windows.Forms.DataGridViewTextBoxEditingControl;
            if (e.KeyCode == Keys.F9 && CurrentCell.EditingControlDataGridView.SelectedCells.Count > 0)
            {
                com.digitalwave.Emr.Signature_gui.frmCommonUsePanel frmcommonusepanel = new com.digitalwave.Emr.Signature_gui.frmCommonUsePanel();
                frmcommonusepanel.m_mthSetParentForm(CurrentCell, false);
                frmcommonusepanel.m_mthSetCommonUserType(-5);
                frmcommonusepanel.m_StrDeptID = string.Empty;
                frmcommonusepanel.m_StrEmployeeID = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.m_strCurrentLoginEmpID;
                frmcommonusepanel.m_BlnIsMultiSignAndNoTag = false;
                frmcommonusepanel.FormClosed += new FormClosedEventHandler(frmcommonusepanel_FormClosed);

                frmcommonusepanel.TopMost = true;
                frmcommonusepanel.ShowDialog(this);
            }            
        }

        private void frmcommonusepanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            com.digitalwave.Emr.Signature_gui.frmCommonUsePanel frmcommonusepanel = sender as com.digitalwave.Emr.Signature_gui.frmCommonUsePanel;
            if (frmcommonusepanel.DialogResult == DialogResult.OK)
            {
                Control ctl = frmcommonusepanel.m_objSelectedControl;
               clsEmrEmployeeBase_VO objVO = ctl.Tag as clsEmrEmployeeBase_VO;

                DataRowView drCurrent = m_dgwOperation.CurrentCell.OwningRow.DataBoundItem as DataRowView;
                if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clmOperator")
                {
                    drCurrent["opdoctor"] = objVO.m_strGetTechnicalRankAndName;
                    drCurrent["operator"] = objVO.m_strEMPID_CHR;
                    drCurrent["opdoctorno"] = objVO.m_strEMPNO_CHR;
                }
                else if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clm1stAssistant")
                {
                    drCurrent["firstassist"] = objVO.m_strGetTechnicalRankAndName;
                    drCurrent["firstassistno"] = objVO.m_strEMPNO_CHR;
                    drCurrent["assistant1"] = objVO.m_strEMPID_CHR;
                }
                else if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clm2ndAssistant")
                {
                    drCurrent["secondassist"] = objVO.m_strGetTechnicalRankAndName;
                    drCurrent["secondassistno"] = objVO.m_strEMPNO_CHR;
                    drCurrent["assistant2"] = objVO.m_strEMPID_CHR;
                }
                else if (m_dgwOperation.CurrentCell.OwningColumn.Name == "clmAnaDoctor")
                {
                    drCurrent["anadoctor"] = objVO.m_strGetTechnicalRankAndName;
                    drCurrent["anadoctorno"] = objVO.m_strEMPNO_CHR;
                    drCurrent["anaesthetist"] = objVO.m_strEMPID_CHR;
                }
            }
        }

        private void m_dgwInfant_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //ȡ�ñ���ʾ�Ŀؼ�
            DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
            if (tb == null)
            {
                return;
            }

            //�¼�������ɾ��
            tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_ICD_KeyDown);
            //����Ӧ��
            if (m_dgwInfant.CurrentCell.OwningColumn.Name == "clmInfectionName" || m_dgwInfant.CurrentCell.OwningColumn.Name == "clmInfectionICD")
            {//����
                // KeyDown�¼�������׷��
                tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_ICD_KeyDown);
            }
        }

        private void DataGridViewTextBoxCell_ICD_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl CurrentCell = sender as System.Windows.Forms.DataGridViewTextBoxEditingControl;
            if (e.KeyCode == Keys.F9 && CurrentCell.EditingControlDataGridView.SelectedCells.Count > 0)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_blnAddICDToDataGridView(CurrentCell.EditingControlDataGridView);
                objDomain = null;
            }
        }
    }
}
