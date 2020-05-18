using System;
using System.EnterpriseServices;

//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.ThreeMeasureEventManagerService
{
    /// <summary>
    /// 的中间件�?
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsThreeMeasureEventManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strResultXml">返回的结�?/param>
        /// <param name="p_intResultRows">记录的数�?/param>
        /// <returns>
        /// 操作结果�?
        /// 0：失败�?
        /// 1：成功�?
        /// </returns>
        [AutoComplete]
        public long m_lngGetAllEvent(ref string p_strResultXml, ref int p_intResultRows)
        {

            string strSQL = @"select threemeasureeventid,
       begineventdate,
       threemeasureeventname,
       threemeasureeventflag,
       status,
       deactiveddate,
       operatorid
  from threemeasureevent
 where status = 0";
            clsHRPTableService objHRPService = new clsHRPTableService();
            return objHRPService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);


        }
        [AutoComplete]
        public long m_lngGetEventItemByType(string p_strFlag,
            ref string p_strResultXml, ref int p_intResultRows)
        {
            string strSQL = @"select threemeasureeventid,
       begineventdate,
       threemeasureeventname,
       threemeasureeventflag,
       status,
       deactiveddate,
       operatorid
  from threemeasureevent
 where status = 0
   and threemeasureeventflag = " + p_strFlag;
            clsHRPTableService objHRPService = new clsHRPTableService();
            return objHRPService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strResultXml">返回的结�?/param>
        /// <param name="p_intResultRows">记录的数�?/param>
        /// <returns>
        /// <returns>
        /// 操作结果�?
        /// 0：失�?
        /// 1：成功�?
        /// </returns>
        [AutoComplete]
        public long m_lngInitRecord(ref string p_strResultXml, ref int p_intResultRows)
        {
            string strSQL = "";
            clsHRPTableService objHRPService = new clsHRPTableService();
            long lngCheckRes = objHRPService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            //objHRPService.Dispose();
            return lngCheckRes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strMainXml"></param>
        /// <returns>
        /// 操作结果�?
        /// 0：失败�?
        /// 1：成功�?
        /// </returns>
        [AutoComplete]
        public long m_lngAddNew(string p_strMainXml)
        {
            clsHRPTableService objHRPService = new clsHRPTableService();
            long lngCheckRes = objHRPService.add_new_record("ThreeMeasureEvent", p_strMainXml);
            //objHRPService.Dispose();
            return lngCheckRes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strMainXml"></param>
        /// <returns>
        /// 操作结果�?
        /// 0：失败�?
        /// 1：成功�?
        /// </returns>
        [AutoComplete]
        public long m_lngDelete(string p_strMainXml)
        {
            string[] strTemp = new string[1] { "THREEMEASUREEVENTID" };
            clsHRPTableService objHRPService = new clsHRPTableService();
            long lngCheckRes = objHRPService.modify_record("ThreeMeasureEvent", p_strMainXml, strTemp);
            //objHRPService.Dispose();
            return lngCheckRes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strOldXml"></param>
        /// <param name="p_strNewXml"></param>
        /// <returns>
        /// 操作结果�?
        /// 0：失败�?
        /// 1：成功�?
        /// </returns>
        [AutoComplete]
        public long m_lngModify(string p_strOldXml, string p_strNewXml)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string[] strTemp = new string[1] { "THREEMEASUREEVENTID" };
            long lngRes = objHRPServ.modify_record("ThreeMeasureEvent", p_strOldXml, strTemp);

            if (lngRes <= 0)
                return lngRes;
            return objHRPServ.add_new_record("ThreeMeasureEvent", p_strNewXml);



        }


        [AutoComplete]
        public string m_strGetMaxThreeMeasureEventID()
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();

            //string strsql="select max(THREEMEASUREEVENTID) FROM  THREEMEASUREEVENT";
            string strMaxValue = objHRPServ.m_strGetNewID("THREEMEASUREEVENT", "THREEMEASUREEVENTID", 4);

            if (strMaxValue == null || strMaxValue == "")
                return Convert.ToString(1).PadLeft(4, '0');
            double intNewValue = Convert.ToDouble(strMaxValue) + 1;
            if (strMaxValue.Length != Convert.ToString(intNewValue).Length)
                strMaxValue = intNewValue.ToString().Trim().PadLeft(strMaxValue.Length, '0');
            else
                strMaxValue = intNewValue.ToString();
            //objHRPServ.Dispose();
            return strMaxValue;
        }



    }
}