using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
namespace com.digitalwave.BornScheduleService
{
	/// <summary>
	/// clsBornScheduleService ��ժҪ˵����
	///�м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBornScheduleService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsBornScheduleService()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMainXml"></param>
		/// <returns>
		/// ���������

		/// 0��ʧ�ܡ�

		/// 1���ɹ���

		/// </returns>
		[AutoComplete]
		public long m_lngAddNew( string p_strMainXml,bool p_blnIsAddNew)
        { 

			clsHRPTableService objHRPService = new clsHRPTableService();

			long lngRes = 0;
            
			if(p_blnIsAddNew)
			{
				lngRes = objHRPService.add_new_record("T_EMR_BORNSCHEDULE",p_strMainXml);
                //lngRes = objHRPService.add_new_record("T_EMR_BORNSCHEDULETO", p_strMainXml);	
			}
			else
			{
				lngRes = objHRPService.modify_record("T_EMR_BORNSCHEDULE",p_strMainXml,"INPATIENTID","INPATIENTDATE","OPENDATE");
			}
            //objHRPService.Dispose();
			return lngRes;
		}
        [AutoComplete]
        public long m_lngAddBornto(  string p_strMainXml, bool p_blnIsAddNew)
        { 

            clsHRPTableService objHRPService = new clsHRPTableService();

            long lngRes = 0;

            if (p_blnIsAddNew)
            {
              //  lngRes = objHRPService.add_new_record("T_EMR_BORNSCHEDULE", p_strMainXml);
               lngRes = objHRPService.add_new_record("T_EMR_BORNSCHEDULETO", p_strMainXml);	
            }
            else
            {
                lngRes = objHRPService.modify_record("T_EMR_BORNSCHEDULETO", p_strMainXml, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
            }
            //objHRPService.Dispose();
            return lngRes;
        }

         

		///һ�����˵����в��̼�¼����
		[AutoComplete]
		public long m_GetPatientRecordDate( string p_InPatientID,DateTime p_dtmInPatientDate,out DataTable p_dtResult)
		{
			p_dtResult=null; 
			if(p_InPatientID == null || p_dtmInPatientDate.ToString() == "" )
			{
				p_dtResult=null;
				return -1;
			}
			clsHRPTableService objHRPService = new clsHRPTableService();
			
			long lngRes = 0;
            string strSql="select opendate from t_emr_bornschedule where trim(inpatientid)=? and inpatientdate=?";
			//DataTable m_dtbResult = new DataTable();
            IDataParameter[] objDPArr = null;
            objHRPService.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_InPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            lngRes = objHRPService.lngGetDataTableWithParameters(strSql, ref p_dtResult, objDPArr);
             //objHRPService.Dispose();
			return lngRes;
		}


		///һ�����˵����в��̼�¼
		[AutoComplete]
		public long m_GetPatientBornScheduleRecord( string p_InPatientID,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate,out DataTable p_dtResult)
		{
			p_dtResult=null; 
			
            long lngCheckRes =0;
            ////obj.Dispose();
            //if(lngCheckRes <= 0)
				//return lngCheckRes;
			if(p_InPatientID == null || p_dtmInPatientDate.ToString() == "" )
			{
				p_dtResult=null;
				return -1;
			}
			clsHRPTableService objHRPService = new clsHRPTableService();
			
			long lngRes = 0;
            string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       childbirthdate,
       createid,
       modifydate,
       forecastdate,
       firstpoint,
       secondpoint,
       threepoint,
       foutpoint,
       pregnancynum,
       venterpointxml,
       checkventertimexml,
       bloodpressurexml,
       embryoheartxml,
       venterscalexml,
       exceptionnotexml,
       dealnotexml,
       signxml
  from t_emr_bornschedule
 where trim(inpatientid) = ?
   and inpatientdate = ?
   and opendate = ?";
			//DataTable m_dtbResult = new DataTable();
			//return new clsHRPTableService().lngGetXMLTable(strSql,ref p_strResultXml,ref p_intResultRows);
            IDataParameter[] objDPArr = null;
            objHRPService.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_InPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));

            lngRes = objHRPService.lngGetDataTableWithParameters(strSql, ref p_dtResult, objDPArr);
            //objHRPService.Dispose();
			return lngRes;
		}

        ///һ�����˵����ֱ���Ϣ
        [AutoComplete]
        public long m_GetfenRecord(  string p_InPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate, out DataTable p_dtResult)
        {
            p_dtResult = null; 

            long lngCheckRes = 0;
            ////obj.Dispose();
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            if (p_InPatientID == null || p_dtmInPatientDate.ToString() == "")
            {
                p_dtResult = null;
                return -1;
            }
            clsHRPTableService objHRPService = new clsHRPTableService();

            long lngRes = 0;
            string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       heart,
       breath,
       power,
       foot,
       skin,
       singer,
       soon,
       onemini,
       fivemini 
  from t_emr_bornscheduleto
 where trim(inpatientid) = ?
   and inpatientdate = ?
   and opendate = ?";
            //DataTable m_dtbResult = new DataTable();
            //return new clsHRPTableService().lngGetXMLTable(strSql,ref p_strResultXml,ref p_intResultRows);
            IDataParameter[] objDPArr = null;
            objHRPService.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_InPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));

            lngRes = objHRPService.lngGetDataTableWithParameters(strSql, ref p_dtResult, objDPArr);
            //objHRPService.Dispose();
            return lngRes;
        }



	}
}
