using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class clsDcl_MicReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_MicReport()
        {
        }

        #region ��ȡ���п�����
        /// <summary>
        /// ��ȡ���п�����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long lngGetAllAnti(out DataTable dt)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAllAnti(out dt);
            //}
        }
        #endregion

        #region ģ����ѯ������
        /// <summary>
        /// ģ����ѯ������
        /// </summary>
        /// <param name="micName"></param>
        /// <param name="IsEnglish"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetFuzzyQueryAnti(micName, IsEnglish, out dtbResult);
            //}
        }
        #endregion

        #region ��ȡ����ϸ��
        /// <summary>
        /// ��ȡ����ϸ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long lngGetAllMic(out DataTable dt)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAllMic(out dt);
            //}
        }
        #endregion

        #region ��ȡ���и���ϸ��
        /// <summary>
        /// ��ȡ���и���ϸ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long lngGetAllGlMic(out DataTable dt)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAllGlMic(out dt);
            //}
        }
        #endregion


        #region ϸ�����Ƽ���ϸ��
        /// <summary>
        /// ϸ�����Ƽ���ϸ��
        /// </summary>
        /// <param name="micName"></param>
        /// <param name="IsEnglish"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
                lngRes = (new weCare.Proxy.ProxyReport()).Service.lngGetFuzzyQueryMic(micName, IsEnglish, out dtbResult);
            return lngRes;
        }
        #endregion

        #region ϸ���ֲ�����ͳ�Ʊ���
        /// <summary>
        /// ϸ���ֲ�����ͳ�Ʊ���
        /// </summary>
        /// <param name="micname"></param>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="SamtNo"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="AgeFrom"></param>
        /// <param name="AgeTo"></param>
        /// <param name="TestMethod"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns> 
        public long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetBacteriaDistribution(micname, p_dtDateFrom, p_dtDateTO, applicationStr, DeptIdArr, sampleId, DisNo, Sex, TestMethod, out dtbResult);
            //}
        }
        #endregion

        #region ϸ���ֲ����Ʊ���
        /// <summary>
        /// ϸ���ֲ����Ʊ���
        /// </summary>
        /// <param name="micname"></param>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="SamtNo"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string TestMethod, out DataTable dtbResult)
        {

            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetMicdistributionTend(micname, p_dtDateFrom, p_dtDateTO, applicationStr,DeptIdArr, sampleId, DisNo, Sex, TestMethod, out dtbResult);
            //}
        }
        #endregion

        #region �ۼ�������
        /// <summary>
        /// �ۼ�������
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTo"></param>
        /// <param name="SamtName"></param>
        /// <param name="DisName"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>

        public long lngGetMicSensitive(string applicationStr, string sampleId, string DisName, string Sex, string strTempAntiName,string strTempAnti, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetMicSensitive(applicationStr, sampleId, DisName, Sex, strTempAntiName,strTempAnti, out dtbResult);
            //}
        }
        #endregion

        #region ���������Ʊ���
        /// <summary>
        /// ���������Ʊ���
        /// </summary>
        /// <param name="micname"></param>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="SamtNo"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="AgeFrom"></param>
        /// <param name="AgeTo"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns> 
        public long lngGetSensitiveTend(string applictionStr, string sampleId, string DisNo, string Sex,  string strTempAntiID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
                lngRes = (new weCare.Proxy.ProxyReport()).Service.lngGetSensitiveTend(applictionStr, sampleId, DisNo, Sex, strTempAntiID, out dtbResult);
            return lngRes;
        }
        #endregion

        #region �ۼ�MIC����
        /// <summary>
        /// �ۼ�MIC����
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="SamtNo"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetMicCumulative(string applicationStr, string sampleId, string DisNo, string Sex, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetMicCumulative( applicationStr, sampleId, DisNo, Sex, TestMethod, strTempAntiID, out dtbResult);
            //}
        }
        #endregion

        #region ϸ����ϸ
        /// <summary>
        /// ϸ����ϸ
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="applicationStr"></param>
        /// <param name="sampleId"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAntiDetail(string strTempName,DateTime p_dtDateFrom, DateTime p_dtDateTO, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAntiDetail(strTempName,p_dtDateFrom, p_dtDateTO,DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
            //}
        }
        #endregion

        #region ϸ�� ������
        /// <summary>
        /// ϸ�� ������
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="applicationStr"></param>
        /// <param name="sampleId"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAntiSample(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAntiSample(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
            //}
        }
        #endregion

        #region ϸ�� ������
        /// <summary>
        /// ϸ�� ������
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="applicationStr"></param>
        /// <param name="sampleId"></param>
        /// <param name="DisNo"></param>
        /// <param name="Sex"></param>
        /// <param name="TestMethod"></param>
        /// <param name="strTempAntiID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAntiByDept(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAntiByDept(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
            //}
        }
        #endregion

        #region ��ȡitem 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAntiCheckItem(DateTime p_dtDateFrom, DateTime p_dtDateTO, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAntiCheckItem(p_dtDateFrom, p_dtDateTO, out dtbResult);
            //}
        }
        #endregion

        #region  ϸ������ 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAnti(string strTempAntiName, DateTime p_dtDateFrom, DateTime p_dtDateTo, string DeptIdArr, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetAnti( strTempAntiName, p_dtDateFrom, p_dtDateTo, DeptIdArr, out dtbResult);
            //}
        }
        #endregion

        #region ��ȡapplication
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTo"></param>
        /// <param name="SamtName"></param>
        /// <param name="strTempAntiName"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>

        public long lngGetMicApplication(string strTempAntiName,DateTime p_dtDateFrom, DateTime p_dtDateTo,string criticalStr,string DeptIdArr,out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetApplicateion(strTempAntiName, p_dtDateFrom, p_dtDateTo, criticalStr, DeptIdArr, out dtbResult);
            //}
        }
        #endregion

        #region ��ȡ������
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationStr"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetGss(string applicationStr, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetGss(applicationStr, out dtbResult);
            //}
        }
        #endregion
        

        #region ȡ�ò���
        /// <summary>
        /// ȡ�ò���
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetDeptName(out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetDeptName(out  dtbResult);
            //}
        }
        #endregion

        #region ȡ����������
        /// <summary>
        /// ȡ����������
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>

        public long lngGetSamType(out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetSamType(out  dtbResult);
            //}
        }
        #endregion
    }
}