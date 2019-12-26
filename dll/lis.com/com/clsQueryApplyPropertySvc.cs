using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryApplyPropertySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region ��ѯ�������뵥Ԫ��������
        [AutoComplete]
        public long m_lngGetAllUnitPropertyAndDetail(
            out clsUnitProperty_VO[] p_objPropertyArr,
            out clsUnitPropertyValue_VO[] p_objValueArr)
        {
            long lngRes = 0;
            p_objPropertyArr = null;
            p_objValueArr = null;

            string strSQL1 = @"SELECT * FROM t_aid_lis_unit_property ORDER BY inuse_flag_num DESC, property_priority_num ";
            string strSQL2 = @"SELECT * FROM t_aid_lis_unit_property_value";

            try
            {
                DataTable dtbData = new DataTable();
                clsVOConstructor objVOConstructor = new clsVOConstructor();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL1, ref dtbData);
                if (lngRes > 0 && dtbData != null)
                {
                    p_objPropertyArr = new clsUnitProperty_VO[dtbData.Rows.Count];
                    for (int i = 0; i < dtbData.Rows.Count; i++)
                    {
                        p_objPropertyArr[i] = objVOConstructor.m_objConstructUnitPropertyVO(dtbData.Rows[i]);
                    }
                    lngRes = 0;
                    dtbData = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtbData);
                    if (lngRes > 0 && dtbData != null)
                    {
                        p_objValueArr = new clsUnitPropertyValue_VO[dtbData.Rows.Count];
                        for (int j = 0; j < dtbData.Rows.Count; j++)
                        {
                            p_objValueArr[j] = objVOConstructor.m_objConstructUnitPropertyValueVO(dtbData.Rows[j]);
                        }
                    }
                }
                if (lngRes <= 0)
                {
                    p_objPropertyArr = null;
                    p_objValueArr = null;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��          
            }
            return lngRes;
        }
        #endregion       

        #region �������뵥ԪID��ѯ�õ�һ��ʹ���е� clsUnitPropertyRelate_VO
        [AutoComplete]
        public long m_lngGetRelatesByUnitID(
            string p_strApplyUnitID, out clsUnitPropertyRelate_VO[] p_objVOArr)
        {
            long lngRes = 0;
            p_objVOArr = null;
            string strSQL = @"SELECT r.*
							FROM t_aid_lis_unit_propert_relate r, 
							t_aid_lis_unit_property p,
							t_aid_lis_unit_property_value v

							where r.UNIT_PROPERTY_ID_CHR = p.property_id_chr 
							and r.VALUE_ID_CHR = v.vlaue_id_chr 
							and p.inuse_flag_num = 1 
							and v.inuse_flag_num = 1 
							and r.APPLY_UNIT_ID_CHR = ?
							order by r.unit_property_id_chr, PRIORITY_NUM
							";

            try
            {
                System.Data.IDataParameter[] objDPArr = null;

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);


                objDPArr[0].Value = p_strApplyUnitID;

                DataTable dtbResult = new DataTable();

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null)
                {
                    clsVOConstructor objVOConstructor = new clsVOConstructor();
                    int intCount = dtbResult.Rows.Count;
                    p_objVOArr = new clsUnitPropertyRelate_VO[intCount];
                    for (int i = 0; i < intCount; i++)
                    {
                        p_objVOArr[i] = objVOConstructor.m_objConstructUnitPropertyRelateVO(dtbResult.Rows[i]);
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region		�������е�ʹ�õ�����id
        /// <summary>
        /// �������е�ʹ�õ�����id
        /// </summary>
        /// <param name="p_objPrincipal">�Ƿ���Ȩ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns>�Ƿ�ɹ�</returns>
        [AutoComplete]
        public long m_lngGetAllPropertyId( out string[] p_strResultArr)
        {
            p_strResultArr = null;
            long lngRes = 0;

            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //			lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc","m_lngGetAllPropertyId");
            //			if(lngRes < 0)
            //			{
            //				return -1;
            //			}

            string strSQL = @"SELECT * FROM t_aid_lis_unit_property WHERE inuse_flag_num = 1 ORDER BY property_priority_num";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strResultArr = new string[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_strResultArr.Length; i1++)
                    {
                        p_strResultArr[i1] = dtbResult.Rows[i1]["PROPERTY_ID_CHR"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region		������������������ֵ����
        /// <summary>
        /// ������������������ֵ����
        /// </summary>
        /// <param name="p_objPrincipal">�Ƿ���Ȩ��</param>
        /// <param name="p_strPropertyId">����id</param>
        /// <param name="p_strApplyUnitId">���뵥Ԫid</param>
        /// <param name="p_arlResult">���ؽ������</param>
        /// <returns>�Ƿ�ɹ�</returns>
        [AutoComplete]
        public long m_lngGetPropertyValue( string p_strPropertyId, string p_strApplyUnitId, out System.Collections.Generic.List<string> p_arlResult)
        {
            p_arlResult = null;
            long lngRes = 0;

            string strSQL = @"SELECT b.vlaue_vchr
								FROM t_aid_lis_unit_propert_relate a, t_aid_lis_unit_property_value b,t_aid_lis_unit_property c
								WHERE a.apply_unit_id_chr = ?
								AND a.unit_property_id_chr = ?
								AND a.value_id_chr = b.vlaue_id_chr
								AND b.inuse_flag_num = 1
								AND a.unit_property_id_chr = c.property_id_chr
								AND c.inuse_flag_num = 1
								ORDER BY a.priority_num";
            try
            {
                System.Data.IDataParameter[] objDPArr = null;
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strApplyUnitId.PadRight(6, ' ');
                objDPArr[1].Value = p_strPropertyId.PadRight(5, ' ');

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_arlResult = new System.Collections.Generic.List<string>();
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        p_arlResult.Add(dtbResult.Rows[i1]["VLAUE_VCHR"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
