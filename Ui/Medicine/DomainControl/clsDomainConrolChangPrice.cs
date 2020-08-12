using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrolChangPrice ��ժҪ˵����
    /// </summary>
    public class clsDomainConrolChangPrice : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrolChangPrice()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��ȡҩƷ������Ϣ
        /// <summary>
        /// ��ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            long lngRes = 0;

            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMedicine(out dtbMedicine);
            return lngRes;
        }
        #endregion

        #region ͳ�ƿ��
        /// <summary>
        /// ͳ�ƿ��
        /// </summary>
        /// <param name="MedicineID"></param>
        /// <param name="AllAmount"></param>
        /// <returns></returns>
        public long m_lngCountStroage(string MedicineID, out int AllAmount)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngCountStroage(MedicineID, out AllAmount);
            return lngRes;
        }
        #endregion
        #region ������۵�
        /// <summary>
        /// ������۵�
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <returns></returns>
        public long m_lngSaveChangPriceData(clsPriceChgeAppl objPriceChgeApplVO, clsPriceChgeApplDe[] objPriceChgeApplDe, out string strID)
        {
            long lngRes = 0;
            strID = "";
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));

            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveChangPriceData(objPriceChgeApplVO, objPriceChgeApplDe, out strID);
            }
            catch (Exception ee)
            {
                string err = ee.Message;
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���еĵ��۵����ݸ��ݲ�����
        /// <summary>
        /// ��ȡ���еĵ��۵�����
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="nowPriod">������</param>
        /// <returns></returns>
        public long m_lngGetAllChgAppl(out clsPriceChgeAppl[] objPriceChgeApplVO, string nowPriod)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllChgAppl(out objPriceChgeApplVO, nowPriod);
            return lngRes;
        }
        #endregion


        #region ɾ���������뵥
        /// <summary>
        /// ɾ���������뵥
        /// </summary>
        /// <param name="ChangPriceID"></param>
        /// <returns></returns>
        public long m_mthDele(string ChangPriceID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_mthDele(ChangPriceID);
            return lngRes;
        }
        #endregion

        #region �ϲ����۵�
        /// <summary>
        /// �ϲ����۵�
        /// </summary>
        /// <param name="ArrAppLID"></param>
        /// <param name="ArrAppLNO"></param>
        /// <param name="strPriod"></param>
        /// <param name="strEmp"></param>
        /// <param name="newNO"></param>
        /// <param name="objPriceChgeApplVO"></param>
        /// <returns></returns>
        public long m_lngUniteChangPriceData(System.Collections.Generic.List<string> ArrAppLID, System.Collections.Generic.List<string> ArrAppLNO, string strPriod, string strEmp, string newNO, out clsPriceChgeAppl objPriceChgeApplVO)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUniteChangPriceData(ArrAppLID, ArrAppLNO, strPriod, strEmp, newNO, out objPriceChgeApplVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ���еĵ��۵����ݸ��ݲ�����
        /// <summary>
        /// ��ȡ���еĵ��۵�����
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="nowPriod">������</param>
        /// <returns></returns>
        public long m_lngGetAllChgAppl(out DataSet ds, string nowPriod)
        {
            long lngRes = 0;
            ds = null;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllChgAppl(out ds, nowPriod);
            }
            catch
            {
                return -1;
            }

            return lngRes;
        }
        #endregion

        #region ͨ������ID��õ��۵���ϸ
        /// <summary>
        /// ͨ������ID��õ��۵���ϸ
        /// </summary>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetChgApplDe(string p_strID, out clsPriceChgeApplDe[] p_objResult, int PSTATUS_INT)
        {
            p_objResult = null;
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChgApplDe(p_strID, out p_objResult, PSTATUS_INT);
            return lngRes;
        }
        #endregion

        #region �޸ĵ��۵�
        /// <summary>
        /// �޸ĵ��۵�
        /// </summary>
        /// <param name="p_objResultDe">��ϸ</param>
        /// <param name="p_objResult">������</param>
        /// <returns></returns>
        public long m_lngMondifiy(clsPriceChgeApplDe p_objResultDe, clsPriceChgeAppl p_objResult)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngMondifiy(p_objResultDe, p_objResult);
            return lngRes;
        }
        #endregion

        #region ɾ�����۵�
        /// <summary>
        /// ɾ�����۵�
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleAppl(string strID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleAppl(strID);
            return lngRes;
        }
        #endregion

        #region ɾ�����۵���ϸ
        /// <summary>
        /// ɾ�����۵���ϸ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleteDeById(string strID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteDeById(strID);
            return lngRes;
        }
        #endregion

        #region ��˵��۵�
        /// <summary>
        /// ��˵��۵�
        /// </summary>
        /// <param name="ADUITEMPID"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngTrialChang(clsPriceChgeAppl objApplVO, clsPriceChgeApplDe[] objApplDeArr)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAuditChangePrice(objApplVO, objApplDeArr);
            }
            catch (Exception ee)
            {
                string err = ee.Message;
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion

        #region ���ӵ�����ϸ����
        /// <summary>
        /// ���ӵ�����ϸ����
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="Odds"></param>
        /// <param name="strAPPLID"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <returns></returns>
        public long m_lngAddDe(string p_strID, clsPriceChgeApplDe objPriceChgeApplDe)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddDe(p_strID, objPriceChgeApplDe);
            return lngRes;
        }
        #endregion

        #region ��ȡ�������
        public long m_lngGetChangeType(out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAppChangePriceType(out dt);
            return lngRes;
        }
        #endregion ��ȡ�������

        #region ���۱���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strStartDate">yyyy-mm-dd</param>
        /// <param name="p_strEndDate">yyyy-mm-dd</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChangePriceRpt(System.Collections.Generic.List<string> arrList, out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChangePriceRpt(arrList, out dt);
            return lngRes;
        }
        #endregion ���۱��� 
        #region ��ȡ��󵥺�
        public string m_mthGetMaxNo(string strDate)
        {
            string ret = "";
            try
            {
                //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
                ret = (new weCare.Proxy.ProxyMedStore()).Service.m_mthGetMaxNo(strDate);
            }
            catch
            {

            }
            return ret;
        }
        #endregion
    }
}
