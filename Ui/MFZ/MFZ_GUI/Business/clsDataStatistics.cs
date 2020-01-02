using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 叫号信息传到数据库辅助类
    /// </summary>
    internal class clsDataStatistics
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        private clsDataSerial m_dataSerial;
        /// <summary>
        /// 待传输叫号信息的班次Id
        /// </summary>
        private int m_schemeId;
        /// <summary>
        /// 待传输叫号信息的诊区Id
        /// </summary>
        private int m_areaId;
        private DateTime m_dtStart;
        private DateTime m_dtEnd;
        

        internal clsDataStatistics(clsDataSerial dataSerial)
        {
            m_dataSerial = dataSerial;
            m_schemeId = dataSerial.SchemeId;
            m_areaId = dataSerial.diagAreaID;
            Init();
        }

        private void Init() 
        {
            clsMFZSchemeVO scheme = new clsMFZSchemeVO();
            clsTmdSchemeSmp.s_object.m_lngFind(m_schemeId, out scheme);
            if (scheme != null)
            {
                m_dtStart = new DateTime(m_dataSerial.serializeTime.Year, m_dataSerial.serializeTime.Month, m_dataSerial.serializeTime.Day, scheme.m_dtBegin.Hour, scheme.m_dtBegin.Minute,scheme.m_dtBegin.Second);
                m_dtEnd = new DateTime(m_dataSerial.serializeTime.Year, m_dataSerial.serializeTime.Month, m_dataSerial.serializeTime.Day, scheme.m_dtEnd.Hour, scheme.m_dtEnd.Minute, scheme.m_dtEnd.Second);
            }
        }

        public bool Save() 
        {
            if (!IsSaved())
            {
                foreach (string doctorId in m_dataSerial.HasCalledPatient.Keys)
                {
                    clsMFZStatistics stat = new clsMFZStatistics();
                    stat.m_intAreaId = m_areaId;
                    stat.m_intSchemeId = m_schemeId;
                    stat.m_strDoctorId = doctorId;
                    stat.m_strDeptId = " ";

                    try
                    {
                        stat.m_intDoctorCalledNum = int.Parse(m_dataSerial.HasCalledPatient[doctorId].ToString());
                    }
                    catch 
                    {
                        stat.m_intDoctorCalledNum = 0;
                    }
                    stat.m_dtStartTime = m_dtStart;
                    stat.m_dtEndTime = m_dtEnd;
                    long res=clsTmdStatisticsSmp.s_object.m_lngInsert(stat);
                    if (res<=0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 是否已经保存了叫号信息

        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool IsSaved() 
        {
          
            clsMFZStatistics statistics = new clsMFZStatistics();
            statistics.m_intAreaId = m_areaId;
            statistics.m_intSchemeId = m_schemeId;
            statistics.m_dtStartTime = m_dtStart;
            statistics.m_dtEndTime = m_dtEnd ;

            int count;
            clsTmdStatisticsSmp.s_object.m_lngSaved(statistics,out count);
            if (count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
