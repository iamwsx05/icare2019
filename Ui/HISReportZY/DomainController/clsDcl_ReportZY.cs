using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    /// <summary>
    /// ��������Domain��
    /// </summary>
    public class clsDcl_ReportZY : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_ReportZY()
        {
        }
        #endregion

        #region ��Ŀͳ�Ʒ�����ϸ����
        /// <summary>
        /// ��Ŀͳ�Ʒ�����ϸ����
        /// </summary>
        /// <param name="CodeNo"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptItemDetailStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                            (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngRptItemDetailStat(CodeNo, BeginDate, EndDate, DeptIDArr, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡ���ܿ���רҵ�����ͳ������
        /// <summary>
        /// ��ȡ���ܿ���רҵ�����ͳ������
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        public long m_lngGetGroupInComeByDoctor(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.Report.clsDemoMiddleTierSVC objSvc =(com.digitalwave.iCare.middletier.HIS.Report.clsDemoMiddleTierSVC)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsDemoMiddleTierSVC));

            //�����м��COM����
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngGetGroupInComeByDoctor(ref objvalue_Param, out dtbResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ���ܿ��Һ���ʵ��ͳ������-����ҽ��
        /// <summary>
        /// ��ȡ���ܿ��Һ���ʵ��ͳ������
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        public long m_lngGetGroupInComeByArea(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, ref DataTable dtbResult)
        {
            long lngRes = 0;

            //�����м��COM����
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            //lngRes = objSvc.m_lngGetGroupInComeByDoctor(objPrincipal, ref objvalue_Param, ref dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="strFindCode"></param>
        /// <param name="objItemArr"></param>
        /// <returns></returns>
        internal long m_lngFindArea(string strFindCode, out clsBIHArea[] objItemArr)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));
            return (new weCare.Proxy.ProxyReport()).Service.m_lngFindArea(strFindCode, out objItemArr);
        }

        #endregion

        #region ��ȡ��ˮ��
        /// <summary>
        /// ��ȡ��ˮ��
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal long m_lngGetRegisterID(string inPatientID, out DataTable dt, int p_intType)
        {
            //        com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));
            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetRegisterID(inPatientID, out dt, p_intType);
            //objSvc.Dispose();
            return l;
        }

        /// <summary>
        /// ̨ɽ�л���ҽ�Ʊ���סԺ�Է���Ŀǩ�ֵ�
        /// </summary>
        /// <param name="strInpinsurancetype"></param>
        /// <param name="RegisterID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetOwnCastItem(string strInpinsurancetype, string RegisterID, out DataTable dtResult)
        {
            //            com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));
            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetOwnCastItem(strInpinsurancetype, RegisterID, out dtResult);
            //objSvc.Dispose();
            return l;
        }
        #endregion 

        public long m_lngGetRptNursingLog(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                   (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetRptNursingLog(dtmTmp, DeptID, out dt);
            //objSvc.Dispose();

            return l;
        }

        public long m_lngGetRptNusingPatientCount(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                       (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetRptNusingPatientCount(dtmTmp, DeptID, out dt);
            //objSvc.Dispose();

            return l;
        }


        #region סԺЭ�鵥λ��ѯͳ�Ʊ���
        /// <summary>
        /// סԺЭ�鵥λ��ѯͳ�Ʊ���
        /// </summary>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngContractUnitPayType(string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                       (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngContractUnitPayType(p_strStartDate, p_strEndDate, out p_dtbResult);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

    }

    public class clsDcl_CommonFind : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_CommonFind()
        {
        }
        #endregion

        #region ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// <summary>
        /// ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="status"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetBedinfo(string AreaID, int status, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetBedinfo(AreaID, status, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// <summary>
        /// ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetPatientinfoByZyh(no, out dt, type);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ͨ�ò��Ҵ�����
        /// <summary>
        /// ͨ�ò��Ҵ�����
        /// </summary>
        /// <param name="SqlWhereZY"></param>
        /// <param name="Status">0 ȫ�� 1 ��Ժ 2 ��Ժ</param>
        /// <param name="IsIncludeMZ"></param>
        /// <param name="SqlWhereMZ"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetPatientinfo(SqlWhereZY, Status, IsIncludeMZ, SqlWhereMZ, CommonQueryDate_VO, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ����סԺ�Ż�ȡ���˻�������
        /// <summary>
        /// ����סԺ�Ż�ȡ���˻�������
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetPatientinfoByZyh(Zyh, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion
    }

    /// <summary>
    /// ����DOMIAN��
    /// </summary>
    public class clsDcl_Charge : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_Charge()
        {
        }
        #endregion

        #region ��ȡ���(�ѱ�)��Ϣ
        /// <summary>
        /// ��ȡ���(�ѱ�)��Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPayTypeInfo(out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetPayTypeInfo(out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ò�����Ϣ
        /// <summary>
        /// ��ò�����Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Flag">1 ���� 2 ����</param>
        /// <returns></returns>
        public long m_lngGetDeptArea(out DataTable dt, int Flag)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetDeptArea(out dt, Flag);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ѯ�շ���Ŀ
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="PatType">�������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngFindChargeItem(FindStr, PatType, out dt);
            //objSvc.Dispose();

            return l;
        }

        /// <summary>
        /// ������ĿID�����շ���Ŀ
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string ItemID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngFindChargeItem(ItemID, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// <summary>
        /// ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));

            long l = (new weCare.Proxy.ProxyReport()).Service.m_lngGetPatientFeeDetByPayType(RegID, PayTypeID, out dt);
            //objSvc.Dispose();

            return l;
        }
        #endregion
    }

    /// <summary>
    /// ͳ�Ʋ�ѯ�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-09-23
    /// </summary>
    public class clsDcl_StatQuery : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        public clsDcl_StatQuery()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ������Ժ��ͳ�Ʊ�  liuyingrui 2006.05.08
        /// <summary>
        /// ������Ժ��ͳ�Ʊ�  liuyingrui 2006.05.08
        /// </summary>
        /// <param name="dtStartTime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime">ͳ����ֹʱ��</param>
        /// <returns></returns>
        public long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, object strPaytypeId, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));
            try
            {
                //change
                //lngRes = objSvc.GetPatientBihStatistics(objPrincipal, dtStartime, dtEndTime, out dtbResult);
                //--------------->
                if (strPaytypeId == null)
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientBihStatistics(dtStartime, dtEndTime, out dtbResult);

                }
                else if (((string)strPaytypeId).Equals("0000"))
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientBihStatistics(dtStartime, dtEndTime, out dtbResult);
                }
                else
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientBihStatistics(dtStartime, dtEndTime, strPaytypeId.ToString(), out dtbResult);

                }
                /*<--------------------*/
            }
            catch
            {
                return 0;
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ���˳�Ժ��ͳ�Ʊ� 2006.11.18
        /// <summary>
        ///  ���˳�Ժ��ͳ�Ʊ� 2006.11.18
        /// </summary>
        /// <param name="dtStartTime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime">ͳ����ֹʱ��</param>
        /// <returns></returns>
        public long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, object strPaytypeId, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.Report.clsReportZY_Svc));
            try
            {
                //change
                //lngRes = objSvc.GetPatientLeftStatistics(objPrincipal, dtStartime, dtEndTime, out dtbResult);
                //--------------------------------->
                if (strPaytypeId == null)
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientLeftStatistics(dtStartime, dtEndTime, out dtbResult);
                }
                else if (strPaytypeId.Equals("0000"))
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientLeftStatistics(dtStartime, dtEndTime, out dtbResult);
                }
                else
                {
                    lngRes = (new weCare.Proxy.ProxyReport()).Service.GetPatientLeftStatistics(dtStartime, dtEndTime, strPaytypeId.ToString(), out dtbResult);
                }
                //<---------------------------------

            }
            catch
            {
                return 0;
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
