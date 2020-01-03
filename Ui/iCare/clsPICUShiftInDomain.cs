using System;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsPICUShiftInDomain.
    /// PICUת��
    /// </summary>
    public class clsPICUShiftInDomain : clsPICUShiftBaseDomain
    {
        public clsPICUShiftInDomain()
        {
        }

        protected override long m_lngSubAddNew(string p_strMainXml, string p_strContentXml)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngAddNew(p_strMainXml, p_strContentXml);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        protected override long m_lngSubModify(string p_strMainXml, string p_strContentXml)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngModify(p_strMainXml, p_strContentXml);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public override long m_lngCheckNewCreateDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out bool p_blnIsAddNew)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngCheckNewCreateDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_blnIsAddNew);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        protected override long m_lngSubGetPICUShiftInfo(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngGetPICUShiftInfo(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref p_strResultXml, ref p_intResultRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        protected override long m_lngSubGetDeletedPICUShiftInfo(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngGetDeletedPICUShiftInfo(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref p_strResultXml, ref p_intResultRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public override long m_lngCheckLastModifyDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, string p_strLastModifyDate, out bool p_blnIsLast)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngCheckLastModifyDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, p_strLastModifyDate, out p_blnIsLast);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        protected override clsPICUShiftTurnInfo m_objGetTurnInfo()
        {
            return new clsPICUShiftInTurnInfo();
        }

        protected override long m_lngSubGetCreateDateArr(string p_strInPatientID, string p_strInPatientDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftOutService_m_lngGetPatientShiftInfo(p_strInPatientID, p_strInPatientDate, ref p_strResultXml, ref p_intResultRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ɾ����¼��
        /// </summary>
        /// <param name="p_objRecordContent">��ǰҪɾ���ļ�¼</param>
        /// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        public override long m_lngDeleteRecord(clsTrackRecordContent p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //clsPICUShiftInService m_objServ =
            //    (clsPICUShiftInService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPICUShiftInService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsPICUShiftInService_m_lngDeleteRecord(p_objRecordContent, out p_objModifyInfo);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }


    [Serializable]
    public class clsPICUShiftInTurnInfo : clsPICUShiftTurnInfo
    {
        public override bool m_BlnIsShiftIn
        {
            get
            {
                return true;
            }
        }

        public override void m_mthMakeContentXML(XmlTextWriter p_objXmlWriter)
        {
            p_objXmlWriter.WriteAttributeString("INPATIENTID", m_strInPatientID);
            p_objXmlWriter.WriteAttributeString("INPATIENTDATE", m_strINPATIENTDATE);
            p_objXmlWriter.WriteAttributeString("CREATEDATE", m_dtmTurnTime.ToString("yyyy-MM-dd HH:mm:ss"));

            p_objXmlWriter.WriteAttributeString("TURNFROMDEPTID", m_strTurnFromDeptID);
            if (m_strTurnToEmployeeID != null)
                p_objXmlWriter.WriteAttributeString("PICUDOCTORID", m_strTurnToEmployeeID);
        }

        public override void m_mthSetValue(XmlTextReader p_objReader)
        {
            m_dtmTurnTime = DateTime.Parse(p_objReader.GetAttribute("CREATEDATE"));
            m_strTurnFromDeptID = p_objReader.GetAttribute("TURNFROMDEPTID");
            m_strTurnFromEmployeeID = p_objReader.GetAttribute("CREATEID");

            if (p_objReader.GetAttribute("PICUDOCTORID") != DBNull.Value.ToString())
            {
                m_strTurnToEmployeeID = new clsEmployee(p_objReader.GetAttribute("PICUDOCTORID")).m_StrEmployeeID;

                //m_strTurnToDeptID = m_objTurnToDoctor.m_ObjDepartment;
            }
        }
    }
}
