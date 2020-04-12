using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
//using Oracle.DataAccess.Types;
using weCare.Core.Entity;

namespace com.digitalwave.GLS_WS_SERV
{
    /// <summary>
    /// ���뱨�浥�м��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsApplyReportServerNew : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����

        //private int m_intApplyType; //������������ID
        ///// <summary>
        ///// ��������ID
        ///// </summary>
        //public int WhereApplyTypeID
        //{
        //    get
        //    {
        //        return m_intApplyType;
        //    }
        //    set
        //    {
        //        m_intApplyType = value;
        //    }
        //}


        //private string m_strDiagnosePart = ""; //������ϲ�λ
        ///// <summary>
        ///// ��ϲ�λ
        ///// </summary>
        //public string WhereDiagnosePart
        //{
        //    get
        //    {
        //        return m_strDiagnosePart;
        //    }
        //    set
        //    {
        //        m_strDiagnosePart = value;
        //    }
        //}


        //private string m_strPatientName = "";//��������
        ///// <summary>
        ///// ����
        ///// </summary>
        //public string WherePatientName
        //{
        //    get
        //    {
        //        return m_strPatientName;
        //    }
        //    set
        //    {
        //        m_strPatientName = value;
        //    }
        //}


        //private string m_strInPatientNo = ""; //����סԺ��
        ///// <summary>
        ///// סԺ��
        ///// </summary>
        //public string WhereInPatientNo
        //{
        //    get
        //    {
        //        return m_strInPatientNo;
        //    }
        //    set
        //    {
        //        m_strInPatientNo = value;
        //    }
        //}


        //private string m_strCardNo = "";//�������ƿ���
        ///// <summary>
        ///// ���ƿ���
        ///// </summary>
        //public string WhereCardINo
        //{
        //    get
        //    {
        //        return m_strCardNo;
        //    }
        //    set
        //    {
        //        m_strCardNo = value;
        //    }
        //}


        //private string m_strArea = "";//���没��
        ///// <summary>
        ///// ����
        ///// </summary>
        //public string WhereArea
        //{
        //    get
        //    {
        //        return m_strArea;
        //    }
        //    set
        //    {
        //        m_strArea = value;
        //    }
        //}


        //private DateTime m_dtmApplytDate;//������������-statr
        ///// <summary>
        ///// ������������-statr
        ///// </summary>
        //public DateTime WhereStartApplytDate
        //{
        //    get
        //    {
        //        return m_dtmApplytDate;
        //    }
        //    set
        //    {
        //        m_dtmApplytDate = value;
        //    }
        //}


        //private DateTime m_strEndApplytDate;//������������-end
        ///// <summary>
        ///// ������������-end
        ///// </summary>
        //public DateTime WhereEndApplytDate
        //{
        //    get
        //    {
        //        return m_strEndApplytDate;
        //    }
        //    set
        //    {
        //        m_strEndApplytDate = value;
        //    }
        //}

        #endregion

        /// <summary>
        /// ��ȡ���뵥���ݱ�
        /// </summary>
        /// <param name="strErrorMessage">���ز�ѯʱ������Ϣ</param>
        /// <param name="dtbResult">���ز�ѯ���</param>
        /// <returns>����ֵΪTrue��ʾ��ѯ�ɹ��������ѯʧ��</returns>
        [AutoComplete]
        public bool m_blnGetApplyReportDataTable(ref string p_strErrorMessage,ref DataTable p_dtbResult, string WhereApplyTypeID, string WhereDiagnosePart, string WherePatientName,
                                                string WhereInPatientNo, string WhereCardINo, string WhereArea, DateTime WhereStartApplytDate, DateTime WhereEndApplytDate)
        {
            //���뵥��ѯ���
            string strSelectText = @"SELECT DISTINCT t1.applyid,
                                                    t1.NAME,
                                                    t1.diagnosepart AS diagnose_part,
                                                    CASE
                                                      WHEN t1.status_int = 0 THEN
                                                       '������'
                                                      WHEN t1.status_int = 1 THEN
                                                       '�ѵǼ�'
                                                      WHEN t1.status_int = 2 THEN
                                                       '�����'
                                                    END AS apply_status,
                                                    t1.applydate,
                                                    CASE
                                                      WHEN t1.submitted = 0 THEN
                                                       'δ�ύ'
                                                      WHEN t1.submitted = 1 THEN
                                                       '���ύ'
                                                    END AS submitted,
                                                    t1.chargedetail,
                                                    CASE
                                                      WHEN t2.status_int = 0 THEN
                                                       'δ�շ�'
                                                      WHEN t2.status_int = 1 THEN
                                                       '���շ�'
                                                      WHEN t2.status_int = 2 THEN
                                                       '���˷�'
                                                    END AS charge_status,
                                                    t1.finishdate
                                      FROM ar_common_apply t1
                                     INNER JOIN t_opr_attachrelation t2 ON t1.applyid = t2.attachid_vchr
                                     WHERE t1.status_int != 2
                                       AND t1.bihno LIKE ?
                                       AND t1.cardno LIKE ?
                                       AND t1.typeid = ?
                                       AND t1.area LIKE ?
                                       AND t1.diagnosepart LIKE ?
                                       AND t1.NAME LIKE ?
                                       AND t1.applydate BETWEEN ? AND ?";

            clsHRPTableService objHRPServer = new clsHRPTableService(); //����Զ�̶���
                
            //���ò�ѯ����
            IDataParameter[] objParamterArr = null;
            objHRPServer.CreateDatabaseParameter(8, out objParamterArr);
            objParamterArr[0].Value = WhereInPatientNo.Trim() + "%";
            objParamterArr[1].Value = WhereCardINo.Trim() + "%";
            objParamterArr[2].Value = WhereApplyTypeID;
            objParamterArr[3].Value = WhereArea.Trim() + "%";
            objParamterArr[4].Value = WhereDiagnosePart.Trim() + "%";
            objParamterArr[5].Value = WherePatientName.Trim() + "%";
            objParamterArr[6].Value = WhereStartApplytDate;
            objParamterArr[6].DbType = DbType.Date;
            objParamterArr[7].Value = WhereEndApplytDate;
            objParamterArr[7].DbType = DbType.Date;

            try
            {
                if (objHRPServer.lngGetDataTableWithParameters(strSelectText, ref p_dtbResult, objParamterArr) < 0) return false; //��ȡ���뵥����
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message; //���ش�����Ϣ
                return false;
            }

            return true;
        }

        /// <summary>
        /// �������뵥״̬
        /// </summary>
        /// <returns>����True��ʾ���³ɹ����������ʧ��</returns>
        [AutoComplete]
        public bool m_blnUpdateApplyStatus(string p_strApplyStatus, ref DateTime p_dtmFinishDate, string p_strApplyID)
        {
            p_dtmFinishDate = DateTime.Now;
            string strUpdateApplyStatus = @"UPDATE ar_common_apply SET status_int = ?,finishdate =? WHERE applyid = ?";

            clsHRPTableService objHRPServer = new clsHRPTableService(); //����Զ�̶���
            
            long lngRowAffected = 0;            //Ӱ���¼��
            IDataParameter[] objParamterArr = null; 

            objHRPServer.CreateDatabaseParameter(3, out objParamterArr);
            objParamterArr[0].Value = p_strApplyStatus.Trim();
            objParamterArr[1].Value = p_dtmFinishDate;
            objParamterArr[2].Value = p_strApplyID.Trim();

            if (objHRPServer.lngExecuteParameterSQL(strUpdateApplyStatus, ref lngRowAffected, objParamterArr) < 0) //�������뵥״̬SQL���
                return false;
            
            return true;
        }

        /// <summary>
        /// ��ȡ���뵥��ϲ�λ�б�����
        /// </summary>
        /// <param name="p_strErrorMessage">���ز�ѯʱ������Ϣ</param>
        /// <param name="p_dtbResult">���ز�ѯ���</param>
        /// <returns>����ֵΪTrue��ʾ��ѯ�ɹ��������ѯʧ��</returns>
        [AutoComplete]
        public bool m_blnGetApplyPartList(ref string p_strErrorMessage, ref DataTable p_dtbResult)
        {
            string strQuertText = @"SELECT DISTINCT partname,typeid FROM ar_apply_partlist WHERE deleted=0 ORDER BY typeid,partname";

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                if (objHRPServer.DoGetDataTable(strQuertText, ref p_dtbResult) < 0) return false;
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ���뵥�����б�����
        /// </summary>
        /// <param name="p_strErrorMessage">���ز�ѯʱ������Ϣ</param>
        /// <param name="p_dtbResult">���ز�ѯ���</param>
        /// <returns>����ֵΪp_dtbResult��ʾ��ѯ�ɹ��������ѯʧ��</returns>
        [AutoComplete]
        public bool m_blnGetPatientAreaList(ref string p_strErrorMessage, ref DataTable p_dtbResult)
        {
            string strQuertText = @"SELECT DISTINCT ctl_content as patient_area FROM ar_content WHERE controlid = 'm_txtArea' ORDER BY ctl_content";

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                if (objHRPServer.DoGetDataTable(strQuertText, ref p_dtbResult) < 0) return false;
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ���뵥�����б�����
        /// </summary>
        /// <param name="p_strErrorMessage">���ز�ѯʱ������Ϣ</param>
        /// <param name="p_dtbResult">���ز�ѯ���</param>
        /// <param name="p_strApplyTypeArr"></param>
        /// <returns>����ֵΪTrue��ʾ��ѯ�ɹ��������ѯʧ��</returns>
        [AutoComplete]
        public bool m_blnGetApplyTypeListDataTable(ref string p_strErrorMessage, ref DataTable p_dtbResult, string[] p_strApplyTypeArr )
        {
            string strSelectText = @"SELECT DISTINCT typeid, typetext
                                           FROM ar_apply_typelist ";
            
            string strWhereText = "";
            if (p_strApplyTypeArr.Length <= 0)
            {
                strWhereText = " WHERE null = null ";
            }
            else
            {
                try
                {
                    //��ϲ�ѯ����
                    foreach (string strApplyTypeID in p_strApplyTypeArr)
                    {
                        strWhereText += (strApplyTypeID.Trim() + ",");
                    }
                    strWhereText = strWhereText.Substring(0, strWhereText.Length - 1);
                    strWhereText = " WHERE typeid IN (" + strWhereText + ")";
                }
                catch (Exception ex)
                {
                    p_strErrorMessage = ex.Message;
                    return false;
                }
            }

            string strOrderByText = @" ORDER BY typeid";

            string strQueryText = strSelectText + strWhereText + strOrderByText;    //���Ӳ�ѯ���

            clsHRPTableService objHRPServer = new clsHRPTableService();             //����Զ�̶���

            try
            {
                if (objHRPServer.DoGetDataTable(strQueryText, ref p_dtbResult) < 0) return false; //��ȡ���������б�����
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message; //���ش�����Ϣ
                return false;
            }
            return true;
        }
    }
}
