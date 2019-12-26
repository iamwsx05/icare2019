using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;  
using System.Collections;
using com.digitalwave.iCare.gui.HIS; 

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_BedPatientList : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ��������
        clsDcl_ExecuteOrder m_objManage = null;
        
        #endregion

        #region ���캯��
        public clsCtl_BedPatientList()
        {
            m_objManage = new clsDcl_ExecuteOrder();
        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmBedPatientList m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBedPatientList)frmMDI_Child_Base_in;

        }
        #endregion

     
        /// <summary>
        /// ���浼�뵱ǰ��������
        /// </summary>
        internal void LoadTheDate()
        {
           
            this.m_objViewer.m_dtvPersonList.Rows.Clear();
            
            //long lngRes = m_objManage.m_lngGetBihBedByArea((string)m_objViewer.m_txtArea.Tag, "", out arrBed);
            BindThePersonList(this.m_objViewer.arrBed);
            SelectAll();

        }

        #region �𶨲����б�
        private void BindThePersonList(clsBIHCanExecOrder[] arrBed)
        {
            int k=0;
            for (int i = 0; i < arrBed.Length; i++)
            {
                if (arrBed[i] == null)
                {
                    return;
                }
                this.m_objViewer.m_dtvPersonList.Rows.Add();
                k=this.m_objViewer.m_dtvPersonList.RowCount-1;
                //this.m_objViewer.m_dtvPersonList.Rows[k].Cells["dtv_DEPTNAME_VCHR"].Value = arrBed[i].m_strCURAREAName;
                //this.m_objViewer.m_dtvPersonList.Rows[k].Cells["dtv_bedcode"].Value = arrBed[i].m_strCURBEDName;
                this.m_objViewer.m_dtvPersonList.Rows[k].Cells["dtv_bedcode"].Value = arrBed[i].m_strBedName;
                this.m_objViewer.m_dtvPersonList.Rows[k].Cells["m_dtvLastName"].Value = arrBed[i].m_strPatientName;
                this.m_objViewer.m_dtvPersonList.Rows[k].Cells["sex_chr"].Value = arrBed[i].m_strPatientSex;
                this.m_objViewer.m_dtvPersonList.Rows[k].Cells["ISINCEPT_INT"].Value = "0";
                this.m_objViewer.m_dtvPersonList.Rows[k].Tag = arrBed[i];
            }
        }
        #endregion


        

        

        #region ѡ����¼�����
        /// <summary>
        /// ѡ����¼�����
        /// </summary>
        internal void SelectAll()
        {

           
            if (m_objViewer.m_chkSelectAll.Checked == true)
            {
                for (int i = 0; i < m_objViewer.m_dtvPersonList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvPersonList.Rows[i].Cells["ISINCEPT_INT"].Value = "1";
                }
            }
            else
            {
                for (int i = 0; i < m_objViewer.m_dtvPersonList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvPersonList.Rows[i].Cells["ISINCEPT_INT"].Value = "0";
                }
               
            }
           
        }
        #endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        internal void IniTheForm()
        {
            //this.m_objViewer.m_chkSelectAll.Checked = true;
            //this.m_objViewer.m_txtArea.Text = this.m_objViewer.LoginInfo.m_strInpatientAreaName;
            //this.m_objViewer.m_txtArea.Tag = this.m_objViewer.LoginInfo.m_strInpatientAreaID;
            LoadTheDate();
           
        }

        internal void ChangeTheSelectState(int rowNum)
        {
            if (this.m_objViewer.m_dtvPersonList.Rows.Count > 0 && rowNum >= 0)
            {
                if (this.m_objViewer.m_dtvPersonList.Rows[rowNum].Cells["ISINCEPT_INT"].Value.ToString().Trim().Equals("0"))
                {
                    this.m_objViewer.m_dtvPersonList.Rows[rowNum].Cells["ISINCEPT_INT"].Value = "1";
                }
                else if (this.m_objViewer.m_dtvPersonList.Rows[rowNum].Cells["ISINCEPT_INT"].Value.ToString().Trim().Equals("1"))
                {
                    this.m_objViewer.m_dtvPersonList.Rows[rowNum].Cells["ISINCEPT_INT"].Value = "0";
                }

                
            }
           
        }

        

        internal void sendTheBill()
        {
           //IPutMadicine madicine;
           this.m_objViewer.m_arrPersionID = getListArray();
         
        }

        private ArrayList getListArray()
        {
            //Ƿ�ѵĲ����б�
            ArrayList m_arrLessMoney = new ArrayList();
            //Ƥ�Բ�ͨ���Ĳ����б�
            ArrayList m_arrFeel = new ArrayList();
            ArrayList m_arrBedID = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvPersonList.RowCount; i++)
            {
                if (this.m_objViewer.m_dtvPersonList.Rows[i].Cells["ISINCEPT_INT"].Value.ToString().Trim().Equals("1"))
                {
                    if (this.m_objViewer.m_intStatus == 1)
                    {
                        //Ƿ�ѹ���
                        if (this.m_objViewer.m_arrPatient.Contains(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag).m_strRegisterID.ToString()))
                        {
                            m_arrLessMoney.Add(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag));

                        }
                        else
                        {
                            m_arrBedID.Add(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag).m_strRegisterID);
                        }
                        /*<================================*/
                    }
                    else if (this.m_objViewer.m_intStatus == 0)
                    {
                        //Ƥ�Թ���
                        if (this.m_objViewer.m_arrFeelTest.Contains(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag).m_strRegisterID.ToString()))
                        {
                            m_arrFeel.Add(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag));

                        }
                        else
                        {
                            m_arrBedID.Add(((clsBIHCanExecOrder)this.m_objViewer.m_dtvPersonList.Rows[i].Tag).m_strRegisterID);
                        }
                        /*<================================*/
                    }
                }
            }
            
            if (this.m_objViewer.m_intStatus == 1)
            {
                //Ƿ�Ѳ��˲���ִ����ʾ��
                LessMoneyAlert(m_arrLessMoney);
               
            }
            else if (this.m_objViewer.m_intStatus == 0)
            {
                //Ƥ�Բ�ͨ�����˲���ִ����ʾ��
                string m_strMessage = "";
                for (int i = 0; i < m_arrFeel.Count; i++)
                {
                    m_strMessage += ((clsBIHCanExecOrder)m_arrFeel[i]).m_strBedName + "������" + ((clsBIHCanExecOrder)m_arrFeel[i]).m_strPatientName + "\r\n";
                }
                if (!m_strMessage.Equals(""))
                {
                    m_strMessage += "����Ƥ����Ŀ��û�н����Ϊ����,���ʱ���Ƚ�����Ӧ����!";
                    MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                /*<======================================*/
            }
            return m_arrBedID;
        }

        /// <summary>
        /// Ƿ�Ѳ��˲���ִ����ʾ��
        /// </summary>
        /// <param name="m_arrLessMoney"></param>
        private void LessMoneyAlert(ArrayList m_arrLessMoney)
        {
            string m_strMessage = "";
            for (int i = 0; i < m_arrLessMoney.Count; i++)
            {
                m_strMessage += ((clsBIHCanExecOrder)m_arrLessMoney[i]).m_strBedName + "������" + ((clsBIHCanExecOrder)m_arrLessMoney[i]).m_strPatientName + "\r\n";
            }
            if (!m_strMessage.Equals(""))
            {
                m_strMessage += "ΪǷ�Ѳ���,ҽ������ִ��!";
                MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            /*<======================================*/
        }
    }
}
