using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҽ������վ����� Create By Sam 2004-6-17
    /// </summary>
    //	[Transaction(TransactionOption.Required)]
    //	[ObjectPooling(Enabled=true)]
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOPDoctorSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        public clsOPDoctorSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        //����
        #region �����б�
        /// <summary>
        /// �����б�
        /// </summary>
        [AutoComplete]
        public long m_lngFindWaitDiagList(string strDocID, ref DataTable dtResult)
        {
            long lngRes = 0;

            string strSQL = "select a.waitdiaglistid_chr, a.registerid_chr, a.waitdiagdept_chr, a.waitdiagdr_chr, a.order_int, a.registerdate_dat, a.treatdate_dat, a.pstatus_int, a.registerop_vchr,b.deptname,b.docname,b.RegNo,b.name,b.sex,b.Age " +
                          " from t_opr_waitdiaglist a,vw_opregister b " +
                          " where a.registerid_chr=b.regid " +
                          " and a.waitdiagdr_chr=? " +
                          " and a.pstatus_int=1 " +
                          " and a.REGISTERDATE_DAT=? " +
                          " order by a.order_int ";
            System.DateTime sDate = clsGetServerDate.s_GetServerDate().Date;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strDocID, sDate });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���ĺ����б����
        /// <summary>
        /// ���ĺ����б����
        /// </summary>
        /// <param name="objPrin"></param>
        /// <param name="clsVO"></param>
        [AutoComplete]
        public long m_lngTakeDiag(string strWaitID, string strRegID, string DepID, string DocID)
        {
            long lngRes = 0;

            string strSQL = "UPDate t_opr_waitdiaglist Set pstatus_int=2 " +
                          " Where waitdiaglistid_chr='" + strWaitID + "' ";
            //System.Data.IDataParameter objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(new object[]{strWaitListID});
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);
                if (lngRes <= 0)
                    return -1;
                strSQL = "Insert Into t_opr_takediagrec " +
                       "(takediagrecid_chr, registerid_chr, takediagdr_chr, " +
                       " takediagdept_chr, takediagtime_dat) " +
                       "Select '" + strWaitID + "',registerid_chr,'" + DepID + "','" + DocID + "',sysdate from t_opr_waitdiaglist " +
                       " Where waitdiaglistid_chr='" + strWaitID + "' and " +
                       " not exists(Select takediagrecid_chr From t_opr_takediagrec " +
                       " Where takediagrecid_chr='" + strWaitID + "') ";

                lngRes = HRPSvc.DoExcute(strSQL);
                //���ĹҺű��־
                strSQL = "Update t_opr_patientregister  " +
                       " Set pstatus_int='2' Where registerid_chr='" + strRegID + "'";
                lngRes = HRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���ĺ����б��������
        /// <summary>
        /// ���ĺ����б��������
        /// </summary>
        /// <param name="objPrin"></param>
        /// <param name="clsVO"></param>
        [AutoComplete]
        public long m_lngUndoTakeDiag(string strWaitID, string strRegID)
        {
            long lngRes = 0;

            string strSQL = "UPDate t_opr_waitdiaglist Set pstatus_int=1 " +
                " Where waitdiaglistid_chr='" + strWaitID + "' ";
            //System.Data.IDataParameter objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(new object[]{strWaitListID});
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);
                if (lngRes <= 0)
                    return -1;
                strSQL = "Delete t_opr_takediagrec " +
                       " Where takediagrecid_chr='" + strWaitID + "'";

                lngRes = HRPSvc.DoExcute(strSQL);
                //���ĹҺű��־
                strSQL = "Update t_opr_patientregister  " +
                    " Set pstatus_int='1' Where registerid_chr='" + strRegID + "'";
                lngRes = HRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �鿴�ѽ����б�
        /// <summary>
        /// �鿴�ѽ����б�
        /// </summary>
        [AutoComplete]
        public long m_lngFindTakeDiagList(string strDocID, ref DataTable dtResult)
        {
            long lngRes = 0;

            string strSQL = "select a.takediagrecid_chr, a.registerid_chr, a.takediagdr_chr, a.takediagdept_chr, a.takediagtime_dat, a.endtime_dat, a.pstatus_int, a.patientid_chr, a.source_int, a.paytypeid_chr,b.deptname,b.docname,b.RegNo,b.name,b.sex,b.Age " +
                          " from t_opr_takediagrec a,vw_opregister b " +
                          " where a.registerid_chr=b.regid " +
                          " and a.takediagdr_chr=? " +
                          " and a.takediagtime_dat between ? and ? ";

            System.DateTime sDate = clsGetServerDate.s_GetServerDate().Date;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strDocID, sDate, sDate.AddDays(1) });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion


        //��������
        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        [AutoComplete]
        public long m_lngAddRecipeMain(clsOutpatientRecipe_VO clsVO, out string p_strID)
        {
            p_strID = "";
            long lngRes = 0;
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            this.m_lngCreateMainSQL(clsVO, out strSQL, out objPara, out p_strID);

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                long lngRec = 0;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        //������������ִ�����
        [AutoComplete]
        private long m_lngCreateMainSQL(clsOutpatientRecipe_VO clsVO, out string strSQL,
            out System.Data.IDataParameter[] objPara, out string p_strID)
        {
            strSQL = "";
            p_strID = "";
            objPara = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = objHRPSvc.lngGenerateID(18, "OUTPATRECIPEID_CHR", "T_OPR_OUTPATIENTRECIPE", out p_strID);
            if (lngRes < 0)
                return lngRes;
            strSQL = "Insert Into t_opr_outpatientrecipe " +
                "(outpatrecipeid_chr,outpatrecipeno_vchr,patientid_chr, " +
                "createdate_dat,registerid_chr,diagdr_chr,diagdept_chr, " +
                "recordemp_chr,recorddate_dat,pstauts_int) " +
                "Values(?,?,?,sysdate,?,?,?,?,sysdate,?)";
            object[] obj = new object[]{p_strID,clsVO.m_strOutpatRecipeNo,clsVO.m_objPatient.strPatientID,
                                         clsVO.m_strRegisterID,clsVO.m_objDiagDr.strEmpID,
                                         clsVO.m_objDiagDept.strDeptID,clsVO.m_objRecordEmp.strEmpID,
                                         1};
            objPara = objHRPSvc.CreateDatabaseParameter(obj);
            return lngRes;
        }
        #endregion

        #region ������ҩ������ϸ
        /// <summary>
        /// ������ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddWestRecipe(clsOutpatientPWMRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }

            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;
            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    //					lngRes=HRPSvc.lngGenerateID(20,"outpatrecipedeid_chr","t_opr_outpatientpwmrecipede",out p_strID);
                    //					if(lngRes<0)
                    //						return lngRes; 		
                    //					strSQL="Insert Into t_opr_outpatientpwmrecipede " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, unitid_chr, " +
                    //						"usageid_chr, tolqty_dec, unitprice_mny, tolprice_mny, " +
                    //						"outpatrecipedeid_chr, days_int, qty_dec, FREQID_CHR) " +
                    //						" Values (?,?,?,?,?,?,?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_objUsage.m_strUsageID,
                    //												 clsVO[i1].m_decTolQty,clsVO[i1].m_decPrice,clsVO[i1].m_decTolPrice,
                    //												 p_strID,clsVO[i1].m_decDays,clsVO[i1].m_decQty,clsVO[i1].m_strFrequency};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ������ҩ������ϸ
        /// <summary>
        /// ������ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddCMRecipe(clsOutpatientCMRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }
            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    //					lngRes=HRPSvc.lngGenerateID(20,"outpatrecipedeid_chr","T_OPR_OUTPATIENTCMRECIPEDE",out p_strID);
                    //					if(lngRes<0)
                    //						return lngRes; 		
                    //					strSQL="Insert Into T_OPR_OUTPATIENTCMRECIPEDE " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, unitid_chr, " +
                    //						"usageid_chr, unitprice_mny, tolprice_mny, " +
                    //						"outpatrecipedeid_chr,qty_dec) " +
                    //						" Values (?,?,?,?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_objUsage.m_strUsageID,
                    //												 clsVO[i1].m_decPrice,clsVO[i1].m_decTolPrice,
                    //												 p_strID,clsVO[i1].m_decQty};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �������鵥��ϸ
        /// <summary>
        /// �������鵥��ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddCHKRecipe(clsOutpatientCHKRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }
            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    lngRes = HRPSvc.lngGenerateID(20, "outpatrecipedeid_chr", "t_opr_outpatientchkrecipede", out p_strID);
                    if (lngRes < 0)
                        return lngRes;
                    //					strSQL="Insert Into t_opr_outpatientchkrecipede " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, " +
                    //						"price_mny,outpatrecipedeid_chr,oprdept_chr) " +
                    //						" Values (?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_decPrice,p_strID,clsVO[i1].m_objOprDept.strDeptID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���� ��� ����ϸ
        /// <summary>
        /// ���� ��� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddTestRecipe(clsOutpatientTestRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }
            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    //					lngRes=HRPSvc.lngGenerateID(20,"outpatrecipedeid_chr","t_opr_outpatienttestrecipede",out p_strID);
                    //					if(lngRes<0)
                    //						return lngRes; 		
                    //					strSQL="Insert Into t_opr_outpatienttestrecipede " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, " +
                    //						"price_mny,outpatrecipedeid_chr,oprdept_chr) " +
                    //						" Values (?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_decPrice,p_strID,clsVO[i1].m_objOprDept.strDeptID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���� ���� ����ϸ
        /// <summary>
        /// ���� ���� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddOPSRecipe(clsOutpatientOPSRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }
            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    //					lngRes=HRPSvc.lngGenerateID(20,"outpatrecipedeid_chr","t_opr_outpatientopsrecipede",out p_strID);
                    //					if(lngRes<0)
                    //						return lngRes; 		
                    //					strSQL="Insert Into t_opr_outpatientopsrecipede " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, " +
                    //						"price_mny,outpatrecipedeid_chr,oprdept_chr) " +
                    //						" Values (?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_decPrice,p_strID,clsVO[i1].m_objOprDept.strDeptID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���� ���� ������ϸ
        /// <summary>
        /// ���� ���� ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddOtherRecipe(clsOutpatientOtherRecipeDe_VO[] clsVO, clsOutpatientRecipe_VO clsRecipe)
        {
            string strRecipeID = "";
            string p_strID = "";
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            if (clsRecipe.m_strOutpatRecipeID == null)//���´���
            {
                lngRes = m_lngCreateMainSQL(clsRecipe, out strSQL, out objPara, out strRecipeID);
                if (lngRes < 0)
                    return lngRes;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else //��������
                {
                    if (clsRecipe.objRecDesc != null)
                    {
                        clsRecipe.objRecDesc.m_strOutpatRecipeID = strRecipeID;
                        m_lngUPDRecipeDesc(new clsOutpatientRecipe_VO[] { clsRecipe });
                    }
                }
            }
            else
                strRecipeID = clsRecipe.m_strOutpatRecipeID;

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					if(clsVO[i1].m_objItem==null || clsVO[i1].m_objItem.m_strItemID==null && clsVO[i1].m_objItem.m_strItemID=="")
                    //						continue;
                    //					lngRes=HRPSvc.lngGenerateID(20,"outpatrecipedeid_chr","t_opr_outpatientothrecipede",out p_strID);
                    //					if(lngRes<0)
                    //						return lngRes; 		
                    //					strSQL="Insert Into t_opr_outpatientothrecipede " +
                    //						"(outpatrecipeid_chr, rowno_chr, itemid_chr, " +
                    //						"unitprice_mny,outpatrecipedeid_chr,qty_dec, " +
                    //						"unitid_chr, tolprice_mny,discount_dec) " +
                    //						" Values (?,?,?,?,?,?,?,?,?) ";
                    //					object[] obj=new object[]{strRecipeID,clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_decPrice,p_strID,clsVO[i1].m_decQty,
                    //												 clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_decTolPrice,
                    //					                             clsVO[i1].m_decDiscount};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        //�޸Ĵ���
        #region �޸Ĵ�������
        /// <summary>
        /// �޸Ĵ�������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDRecipeDesc(clsOutpatientRecipe_VO[] clsVO)
        {

            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            object[] obj;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            string strID;
            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    strID = clsVO[i1].objRecDesc.m_strOutpatRecipeID;
                    if (clsVO[i1].objRecDesc == null || strID == null)
                        continue;
                    strSQL = "Select cookingmethodid_chr,outpatrecipeid_chr,memo_vchr,dosage_int From t_opr_opcmrecipedesc Where outpatrecipeid_chr=?";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[]{strID
    });
                    DataTable dtResult = new DataTable();
                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                    if (lngRes < 0)
                        return lngRes;
                    if (dtResult.Rows.Count > 0) //�Ѵ��ڼ�¼
                    {
                        strSQL = "UPDate t_opr_opcmrecipedesc Set cookingmethodid_chr=?," +
                            "memo_vchr=?,dosage_int=? " +
                            " Where outpatrecipeid_chr=? ";
                    }
                    else
                    {
                        strSQL = "Insert Into t_opr_opcmrecipedesc(cookingmethodid_chr," +
                            "memo_vchr,dosage_int,outpatrecipeid_chr) Values " +
                            " (?,?,?,?) ";
                    }

                    obj = new object[]{clsVO[i1].objRecDesc.m_strCookingID,
                                     clsVO[i1].objRecDesc.m_strMemo,
                                     clsVO[i1].objRecDesc.intDosage,strID};
                    objPara = HRPSvc.CreateDatabaseParameter(obj);
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                    if (lngRes < 0)
                        return lngRes;
                }
                HRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸���ҩ������ϸ
        /// <summary>
        /// �޸���ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDWestRecipe(clsOutpatientPWMRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate t_opr_outpatientpwmrecipede " +
                    //						"Set rowno_chr=?,itemid_chr=?,unitid_chr=?, " +
                    //						"usageid_chr=?, tolqty_dec=?,unitprice_mny=?,tolprice_mny=?, " +
                    //						"days_int=?, qty_dec=?, FREQID_CHR=? " +
                    //						" Where outpatrecipedeid_chr=? ";
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //											  clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_objUsage.m_strUsageID,
                    //											  clsVO[i1].m_decTolQty,clsVO[i1].m_decPrice,clsVO[i1].m_decTolPrice,
                    //											  clsVO[i1].m_decDays,clsVO[i1].m_decQty,clsVO[i1].m_strFrequency,
                    //											  clsVO[i1].m_strOutpatRecipeDeID.Trim()};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
                HRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸���ҩ������ϸ
        /// <summary>
        /// �޸���ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDCMRecipe(clsOutpatientCMRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate T_OPR_OUTPATIENTCMRECIPEDE " +
                    //						"Set rowno_chr=?,itemid_chr=?,unitid_chr=?, " +
                    //						"usageid_chr=?,unitprice_mny=?,tolprice_mny=?,qty_dec=? " +
                    //						" Where outpatrecipedeid_chr=?  " ;
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_objUsage.m_strUsageID,
                    //												 clsVO[i1].m_decPrice,clsVO[i1].m_decTolPrice,
                    //												 clsVO[i1].m_decQty,clsVO[i1].m_strOutpatRecipeDeID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸ļ��鵥��ϸ
        /// <summary>
        /// �޸ļ��鵥��ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDCHKRecipe(clsOutpatientCHKRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate t_opr_outpatientchkrecipede " +
                    //						"Set rowno_chr=?,itemid_chr=?, " +
                    //						"price_mny=?,oprdept_chr=?  " +
                    //						" Where outpatrecipedeid_chr=? ";
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //											  clsVO[i1].m_decPrice,clsVO[i1].m_objOprDept.strDeptID,
                    //					                          clsVO[i1].m_strOutpatRecipeDeID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸� ��� ����ϸ
        /// <summary>
        /// �޸� ��� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDTestRecipe(clsOutpatientTestRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate t_opr_outpatienttestrecipede " +
                    //						"Set rowno_chr=?,itemid_chr=?, " +
                    //						"price_mny=?,oprdept_chr=? " +
                    //						" Where outpatrecipedeid_chr=? ";
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //											  clsVO[i1].m_decPrice,clsVO[i1].m_objOprDept.strDeptID,
                    //					                          clsVO[i1].m_strOutpatRecipeDeID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸� ���� ����ϸ
        /// <summary>
        /// �޸� ���� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDOPSRecipe(clsOutpatientOPSRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate t_opr_outpatientopsrecipede " +
                    //						"Set rowno_chr=?, itemid_chr=?, " +
                    //						"price_mny=?,oprdept_chr=? " +
                    //						" Where outpatrecipedeid_chr=? ";
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //											  clsVO[i1].m_decPrice,clsVO[i1].m_objOprDept.strDeptID,
                    //					                          clsVO[i1].m_strOutpatRecipeDeID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸���������
        /// <summary>
        /// �޸���������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO">������ϸVO</param>
        /// <param name="clsRecipe">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUPDOtherRecipe(clsOutpatientOtherRecipeDe_VO[] clsVO)
        {
            System.Data.IDataParameter[] objPara;
            string strSQL = "";
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                for (int i1 = 0; i1 < clsVO.Length; i1++)
                {
                    //					strSQL="UPDate t_opr_outpatientothrecipede " +
                    //						"Set rowno_chr=?, itemid_chr=?, " +
                    //						"unitprice_mny=?,qty_dec=?, " +
                    //						"unitid_chr=?,tolprice_mny=?,discount_dec=? " +
                    //						" Where outpatrecipedeid_chr=? ";
                    //					object[] obj=new object[]{clsVO[i1].m_strRowNo,clsVO[i1].m_objItem.m_strItemID,
                    //												 clsVO[i1].m_decPrice,clsVO[i1].m_decQty,
                    //                                                 clsVO[i1].m_objUnit.m_strUnitID,clsVO[i1].m_decTolPrice,
                    //												 clsVO[i1].m_decDiscount,
                    //												 clsVO[i1].m_strOutpatRecipeDeID};
                    //					objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(obj);
                    //					lngRes=HRPSvc.lngExecuteParameterSQL(strSQL,ref lngRec,objPara);
                    //					if(lngRes<0)
                    //						return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        //ɾ������
        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDelRecipe(com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc,
                    string strID)
        {
            long lngRec = 0;
            long lngRes = 0;
            string strSQL = "";
            try
            {
                strSQL = "Select outpatrecipedeid_chr From t_opr_outpatientpwmrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "' " +
                       "union all " +
                       "Select outpatrecipedeid_chr From t_opr_outpatientcmrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "' " +
                       "union all " +
                       "Select outpatrecipedeid_chr From t_opr_outpatientchkrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "' " +
                       "union all " +
                       "Select outpatrecipedeid_chr From t_opr_outpatienttestrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "' " +
                       "union all " +
                       "Select outpatrecipedeid_chr From t_opr_outpatientopsrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "' " +
                       "union all " +
                       "Select outpatrecipedeid_chr From t_opr_outpatientothrecipede " +
                       "Where outpatrecipeid_chr='" + strID + "'"; //�Ƿ���ڴ�����ϸ
                DataTable dtResult = new DataTable();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > -1 && dtResult.Rows.Count == 0) //����0��¼
                {
                    strSQL = "Delete t_opr_outpatientrecipe " +
                        " Where outpatrecipeid_chr=? ";
                    System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                }
                else
                    return lngRes;
                if (lngRes < 0)
                    return lngRes;
                else //ɾ����������
                {
                    strSQL = "Delete t_opr_opcmrecipedesc " +
                        " Where outpatrecipeid_chr=? ";
                    System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ����ҩ������ϸ
        /// <summary>
        /// ɾ����ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelWestRecipe(string strID, string RecID)
        {
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete t_opr_outpatientpwmrecipede " +
                        " Where outpatrecipedeid_chr=? ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ����ҩ������ϸ
        /// <summary>
        /// ɾ����ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelCMRecipe(string strID, string RecID)
        {
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete T_OPR_OUTPATIENTCMRECIPEDE " +
                       " Where outpatrecipedeid_chr=?  ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ�����鵥��ϸ
        /// <summary>
        /// ɾ�����鵥��ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelCHKRecipe(string strID, string RecID)
        {
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete t_opr_outpatientchkrecipede " +
                          " Where outpatrecipedeid_chr=? ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ�� ��� ����ϸ
        /// <summary>
        /// ɾ�� ��� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelTestRecipe(string strID, string RecID)
        {
            long lngRec = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete t_opr_outpatienttestrecipede " +
                              " Where outpatrecipedeid_chr=? ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ�� ���� ����ϸ
        /// <summary>
        /// ɾ�� ���� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOPSRecipe(string strID, string RecID)
        {
            //Ȩ��
            long lngRec = 0;
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete t_opr_outpatientopsrecipede " +
                             " Where outpatrecipedeid_chr=? ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ������������ϸ
        /// <summary>
        ///  ɾ������������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOtherRecipe(string strID, string RecID)
        {
            //Ȩ��
            long lngRec = 0; long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                string strSQL = "Delete t_opr_outpatientothrecipede " +
                    " Where outpatrecipedeid_chr=? ";
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
                if (lngRes < 0)
                    return lngRes;
                else
                    m_lngDelRecipe(HRPSvc, RecID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        //��ѯ����
        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <param name="clsVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMainRecipe(string strRegID, out clsOutpatientRecipe_VO[] clsVO)
        {
            clsVO = new clsOutpatientRecipe_VO[0];
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                DataTable dtResult = new DataTable();
                string strSQL = @"select outpatrecipeid_chr,
       patientid_chr,
       createdate_dat,
       registerid_chr,
       diagdr_chr,
       diagdept_chr,
       recordemp_chr,
       recorddate_dat,
       pstauts_int,
       recipeflag_int,
       outpatrecipeno_vchr,
       paytypeid_chr,
       casehisid_chr,
       groupid_chr,
       type_int,
       confirm_int,
       confirmdesc_vchr,
       createtype_int,
       deptmed_int,
       archtakeflag_int,
       printed_int,
       chargedeptid_chr
  from t_opr_outpatientrecipe " +
                    " Where registerid_chr=? Order By outpatrecipeid_chr";

                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    clsVO = new clsOutpatientRecipe_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < clsVO.Length; i1++)
                    {
                        clsVO[i1] = new clsOutpatientRecipe_VO();
                        clsVO[i1].m_strOutpatRecipeID = dtResult.Rows[i1]["outpatrecipeid_chr"].ToString().Trim();
                        clsVO[i1].m_strOutpatRecipeNo = dtResult.Rows[i1]["outpatrecipeno_vchr"].ToString().Trim();
                        clsVO[i1].m_strRecordDate = dtResult.Rows[i1]["recorddate_dat"].ToString().Trim();
                        clsVO[i1].m_strRegisterID = dtResult.Rows[i1]["registerid_chr"].ToString().Trim();
                        clsVO[i1].m_strCreateDate = dtResult.Rows[i1]["createdate_dat"].ToString().Trim();
                        clsVO[i1].m_objRecordEmp = new clsEmployeeVO();
                        clsVO[i1].m_objRecordEmp.strEmpID = dtResult.Rows[i1]["recordemp_chr"].ToString().Trim();
                        clsVO[i1].m_objPatient = new clsPatientVO();
                        clsVO[i1].m_objPatient.strPatientID = dtResult.Rows[i1]["patientid_chr"].ToString().Trim();
                        clsVO[i1].m_objDiagDr = new clsEmployeeVO();
                        clsVO[i1].m_objDiagDr.strEmpID = dtResult.Rows[i1]["diagdr_chr"].ToString().Trim();
                        clsVO[i1].m_objDiagDept = new clsDepartmentVO();
                        clsVO[i1].m_objDiagDept.strDeptID = dtResult.Rows[i1]["diagdept_chr"].ToString().Trim();
                        clsVO[i1].m_intPStatus = dtResult.Rows[i1]["pstauts_int"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��������������(t_opr_opcmrecipedesc)
        /// <summary>
        /// ����������(t_opr_opcmrecipedesc)
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <param name="clsVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindRecipeDesc(string strRecID, out clsOutpatientRecipeDesc_VO clsVO)
        {
            clsVO = new clsOutpatientRecipeDesc_VO();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                DataTable dtResult = new DataTable();
                string strSQL = "select cookingmethodid_chr,outpatrecipeid_chr,memo_vchr,dosage_int from t_opr_opcmrecipedesc " +
                              " where outpatrecipeid_chr=? ";

                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRecID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    clsVO.m_strOutpatRecipeID = dtResult.Rows[0]["outpatrecipeid_chr"].ToString().Trim();
                    if (dtResult.Rows[0]["dosage_int"].ToString().Trim() != "")
                        clsVO.intDosage = int.Parse(dtResult.Rows[0]["dosage_int"].ToString().Trim());
                    clsVO.m_strCookingID = dtResult.Rows[0]["cookingmethodid_chr"].ToString().Trim();
                    clsVO.m_strMemo = dtResult.Rows[0]["memo_vchr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ������ҩ������ϸ
        /// <summary>
        /// ������ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID">�Һ�ID</param>
        /// <param name="strRecID">����ID,����ɸѡ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindWestRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "select RowNum, a.outpatrecipeid_chr RecID,a.outpatrecipedeid_chr DetID, " +
                              "a.rowno_chr RowNo,a.itemid_chr ItemID,a.unitid_chr UnitID, " +
                              "a.unitprice_mny Price,a.FREQID_CHR FreID,a.qty_dec QTY,a.days_int Days, " +
                              "a.usageid_chr UsageID,a.tolqty_dec Count," +
                              "a.tolprice_mny Amount,b.itemname_vchr ItemName," +
                              "b.itemcode_vchr ItemCode,c.unitname_chr UnitName," +
                              "d.usagename_vchr UsageName,e.freqname_chr FreName," +
                              "e.frequency_int intFre,f.patientid_chr PatID,f.registerid_chr RegID," +
                              "f.pstauts_int pStaut,b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo,  " +
                              "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                              "from t_opr_outpatientrecipe f,t_opr_outpatientpwmrecipede a," +
                              "t_bse_chargeitem b,t_aid_unit c,t_bse_usageType d,T_AID_RECIPEFREQ e " +
                              " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                               " and a.itemid_chr=b.itemid_chr and a.unitid_chr=c.unitid_chr(+) " +
                               " and a.usageid_chr=d.usageid_chr(+) and a.FREQID_CHR=e.freqid_chr(+) " +
                               " Order by a.rowno_chr";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ��ҩ������ϸ
        /// <summary>
        /// ��ѯ��ҩ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCMRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "select RowNum, a.outpatrecipeid_chr RecID,a.outpatrecipedeid_chr DetID," +
                              "a.rowno_chr RowNo,a.itemid_chr ItemID,a.unitid_chr UnitID," +
                              "a.unitprice_mny Price,a.qty_dec Count,a.usageid_chr UsageID, " +
                              "a.tolprice_mny Amount,b.itemname_vchr ItemName," +
                              "b.itemcode_vchr ItemCode,c.unitname_chr UnitName," +
                              "d.usagename_vchr UsageName," +
                              "f.patientid_chr PatID,f.registerid_chr RegID," +
                              "f.pstauts_int pStaut,b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo, " +
                              "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                              "from t_opr_outpatientrecipe f,t_opr_outpatientcmrecipede a," +
                              "t_bse_chargeitem b,t_aid_unit c,t_bse_usageType d " +
                              " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                    " and a.itemid_chr=b.itemid_chr and a.unitid_chr=c.unitid_chr(+) " +
                    " and a.usageid_chr=d.usageid_chr(+) ";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ���鵥��ϸ
        /// <summary>
        /// ��ѯ���鵥��ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCHKRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "SELECT ROWNUM, a.outpatrecipeid_chr recid, a.outpatrecipedeid_chr detid, " +
                              "a.rowno_chr rowno, a.itemid_chr itemid, a.price_mny Amount,a.oprdept_chr DepID, " +
                              "b.itemname_vchr itemname,b.itemcode_vchr itemcode, f.patientid_chr patid, " +
                              "f.registerid_chr regid, f.pstauts_int pstaut,c.deptname_vchr DepName," +
                              "b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo,  " +
                              "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                              " FROM t_opr_outpatientrecipe f,t_opr_outpatientchkrecipede a, " +
                              " t_bse_chargeitem b,t_bse_deptbaseinfo c " +
                              " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                    " and a.itemid_chr=b.itemid_chr and a.oprdept_chr=c.deptid_chr(+) ";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ ��� ����ϸ
        /// <summary>
        /// ��ѯ ��� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindTestRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "SELECT ROWNUM, a.outpatrecipeid_chr recid, a.outpatrecipedeid_chr detid, " +
                    "a.rowno_chr rowno, a.itemid_chr itemid, a.price_mny Amount,a.oprdept_chr DepID, " +
                    "b.itemname_vchr itemname,b.itemcode_vchr itemcode, f.patientid_chr patid, " +
                    "f.registerid_chr regid, f.pstauts_int pstaut,c.deptname_vchr DepName," +
                    "b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo,  " +
                    "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                    " FROM t_opr_outpatientrecipe f,t_opr_outpatienttestrecipede a, " +
                    " t_bse_chargeitem b,t_bse_deptbaseinfo c " +
                    " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                    " and a.itemid_chr=b.itemid_chr and a.oprdept_chr=c.deptid_chr(+) ";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ ���� ����ϸ
        /// <summary>
        /// ��ѯ ���� ����ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOPSRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "SELECT ROWNUM, a.outpatrecipeid_chr recid, a.outpatrecipedeid_chr detid, " +
                    "a.rowno_chr rowno, a.itemid_chr itemid, a.price_mny Amount,a.oprdept_chr DepID, " +
                    "b.itemname_vchr itemname,b.itemcode_vchr itemcode, f.patientid_chr patid, " +
                    "f.registerid_chr regid, f.pstauts_int pstaut,c.deptname_vchr DepName," +
                    "b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo,  " +
                    "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                    " FROM t_opr_outpatientrecipe f,t_opr_outpatientopsrecipede a, " +
                    " t_bse_chargeitem b,t_bse_deptbaseinfo c " +
                    " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                    " and a.itemid_chr=b.itemid_chr and a.oprdept_chr=c.deptid_chr(+) ";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ����������ϸ
        /// <summary>
        /// ��ѯ����������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOtherRecipe(string strRegID, string strRecID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                System.Data.IDataParameter[] objPara;
                string strSQL = "SELECT ROWNUM, a.outpatrecipeid_chr recid, a.outpatrecipedeid_chr detid, " +
                              "a.rowno_chr rowno, a.itemid_chr itemid, a.unitprice_mny Price,a.qty_dec Count," +
                              "a.tolprice_mny Amount, " +
                              "a.unitid_chr UnitID,c.UnitName_chr UnitName,a.tolprice_mny Amount,a.discount_dec Dis," +
                              "b.itemname_vchr itemname,b.itemcode_vchr itemcode, f.patientid_chr patid, " +
                              "f.registerid_chr regid, f.pstauts_int pstaut,b.itemspec_vchr Spec,f.OutPatRecipeNO_VCHR RecNo,  " +
                              "f.diagdr_chr DocID,f.diagdept_chr DepID " +
                              " FROM t_opr_outpatientrecipe f,t_opr_outpatientothrecipede a,  " +
                              "t_bse_chargeitem b,t_Aid_Unit c " +
                              " Where f.registerid_chr=?";
                if (strRecID != null)
                {
                    strSQL = strSQL + " And a.outpatrecipeid_chr=? ";
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID, strRecID });
                }
                else
                    objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                strSQL = strSQL + " and a.outpatrecipeid_chr=f.outpatrecipeid_chr " +
                    " and a.itemid_chr=b.itemid_chr and a.unitid_chr=c.unitid_chr(+) ";
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ��鲡���Ƿ���ڴ���
        /// <summary>
        /// ��鲡���Ƿ���ڴ���
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMainRecipe(string strRegID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                DataTable dtResult = new DataTable();
                string strSQL = @"select outpatrecipeid_chr,
       patientid_chr,
       createdate_dat,
       registerid_chr,
       diagdr_chr,
       diagdept_chr,
       recordemp_chr,
       recorddate_dat,
       pstauts_int,
       recipeflag_int,
       outpatrecipeno_vchr,
       paytypeid_chr,
       casehisid_chr,
       groupid_chr,
       type_int,
       confirm_int,
       confirmdesc_vchr,
       createtype_int,
       deptmed_int,
       archtakeflag_int,
       printed_int,
       chargedeptid_chr
  from t_opr_outpatientrecipe " +
                              " Where registerid_chr=?";

                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
                return dtResult.Rows.Count;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ����Ƿ��ܲ鿴���˴���
        /// <summary>
        /// ����Ƿ��ܲ鿴���˴���
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <param name="DocID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckPatRecipe(string strRegID, string DocID, ref DataTable dtResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                dtResult = new DataTable();
                string strSQL = "SELECT a.waitdiaglistid_chr ID, a.registerid_chr RegID, a.waitdiagdept_chr RegDepID," +
                              "a.waitdiagdr_chr RegDocID, a.order_int Ord, a.registerdate_dat RegDate, " +
                              "a.treatdate_dat DiagDate,a.pstatus_int pstatus,b.takediagdr_chr DiagDocID, " +
                              "b.takediagdept_chr DiagDepID,c.lastname_vchr DocName,d.deptname_vchr DepName" +
                              " FROM t_opr_waitdiaglist a,t_opr_takediagrec b, " +
                              " t_bse_employee c,t_bse_deptbaseinfo d " +
                              " Where a.registerid_chr=? And a.registerdate_dat<sysdate " +
                              " and a.waitdiagdept_chr=d.deptid_chr " +
                              " and a.waitdiagdr_chr=c.empid_chr " +
                              " And a.registerid_chr=b.registerid_chr(+)";

                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { strRegID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        //����And���Ƽ�¼
        #region ���Ҳ��������Ƽ�¼
        /// <summary>
        /// ���Ҳ��������Ƽ�¼
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <param name="clsCase"></param>
        /// <param name="clsDiag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCaseAndCure(string strRegID, out clsOutpatientCaseHis_VO clsCase, out clsOutpatientDiagRec_VO clsDiag)
        {
            clsCase = new clsOutpatientCaseHis_VO();
            clsDiag = new clsOutpatientDiagRec_VO();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            //			try
            //			{
            //				DataTable dtResult=new DataTable();
            //				string strSQL="SELECT a.casehisid_chr, a.modifydate_dat, a.patientid_chr, a.registerid_chr, " +
            //                              "a.diagdr_chr, a.diagdept_chr, a.diseasememo_vchr, a.diagmemo_vchr," +
            //                              "a.diagresult_vchr, a.recordemp_chr, a.recorddate_dat, a.status_int," +
            //					          "a.diagmain_vchr, a.diagmain_xml_vchr, a.diagcurr_vchr, " +
            //                              "a.diagcurr_xml_vhcr, a.diaghis_vchr, a.diaghis_xml_vchr, " +
            //                              "a.aidcheck_vchr, a.aidcheck_xml_vchr, a.diag_vchr, a.diag_xml_vchr, " +
            //                              "a.treatment_vchr, a.treatment_xml_vchr, a.remark_vchr, " +
            //                              "a.remark_xml_vchr, a.anaphylaxis_vchr," +
            //                              "b.outpatientdiagrecid_chr,b.diagimport_vchr, b.diagmemo_vchr diagmemo, b.diagstd_vchr," +
            //                              "b.curprinciple_vchr, b.curestd_vchr, b.defend_vchr " +
            //                              " FROM t_opr_outpatientcasehis a,t_opr_outpatientdiagrec b " +
            //                              " Where a.registerid_chr=?  And a.registerid_chr=b.registerid_chr(+) ";
            //				
            //				System.Data.IDataParameter[] objPara=clsIDataParameterCreator.s_objConstructIDataParameterArr(new object[]{strRegID});
            //				lngRes=HRPSvc.lngGetDataTableWithParameters(strSQL,ref dtResult,objPara);
            //				if(lngRes>0 && dtResult.Rows.Count>0)
            //				{
            //					//����
            //					clsCase.m_strCaseHisID=dtResult.Rows[0]["casehisid_chr"].ToString().Trim();
            //					clsCase.m_strModifyDate=dtResult.Rows[0]["modifydate_dat"].ToString().Trim();
            //					clsCase.m_strPatientID=dtResult.Rows[0]["patientid_chr"].ToString().Trim();
            //					clsCase.m_strRegisterID=dtResult.Rows[0]["registerid_chr"].ToString().Trim();
            //					clsCase.m_strDiagDrID=dtResult.Rows[0]["diagdr_chr"].ToString().Trim();
            //					clsCase.m_strDiagDeptID=dtResult.Rows[0]["diagdept_chr"].ToString().Trim();
            //					clsCase.m_strDiseaseMemo=dtResult.Rows[0]["diseasememo_vchr"].ToString().Trim();
            ////					clsCase.m_strDiagMemo=dtResult.Rows[0]["diagmemo_vchr"].ToString().Trim();
            //					clsCase.m_strDiagResult=dtResult.Rows[0]["diagresult_vchr"].ToString().Trim();
            //					clsCase.m_strRecordEmpID=dtResult.Rows[0]["recordemp_chr"].ToString().Trim();
            //					clsCase.m_strRecordDate=dtResult.Rows[0]["recorddate_dat"].ToString().Trim();
            //                    if(dtResult.Rows[0]["STATUS_INT"].ToString().Trim()!="")
            //					  clsCase.m_intStatus=int.Parse(dtResult.Rows[0]["STATUS_INT"].ToString().Trim());
            //				
            //					clsCase.strDiagMain=dtResult.Rows[0]["diagmain_vchr"].ToString().Trim();
            //					clsCase.strDiagMain_XML=dtResult.Rows[0]["diagmain_xml_vchr"].ToString().Trim();
            //					clsCase.strDiagCurr=dtResult.Rows[0]["diagcurr_vchr"].ToString().Trim();
            //					clsCase.strDiagCurr_XML=dtResult.Rows[0]["diagcurr_xml_vhcr"].ToString().Trim();
            //					clsCase.strDiagHis=dtResult.Rows[0]["diaghis_vchr"].ToString().Trim();
            //					clsCase.strDiagHis_XML=dtResult.Rows[0]["diaghis_xml_vchr"].ToString().Trim();
            //					clsCase.strAidCheck=dtResult.Rows[0]["aidcheck_vchr"].ToString().Trim();
            //					clsCase.strAidCheck_XML=dtResult.Rows[0]["aidcheck_xml_vchr"].ToString().Trim();
            //					clsCase.strDiag=dtResult.Rows[0]["diag_vchr"].ToString().Trim();
            //					clsCase.strDiag_XML=dtResult.Rows[0]["diag_xml_vchr"].ToString().Trim();
            //					clsCase.strTreatMent=dtResult.Rows[0]["treatment_vchr"].ToString().Trim();
            //					clsCase.strTreatMent_XML=dtResult.Rows[0]["treatment_xml_vchr"].ToString().Trim();
            //					clsCase.strReMark=dtResult.Rows[0]["remark_vchr"].ToString().Trim();
            //					clsCase.strReMark_XML=dtResult.Rows[0]["remark_xml_vchr"].ToString().Trim();
            //					clsCase.strAnaPhyLaXis=dtResult.Rows[0]["anaphylaxis_vchr"].ToString().Trim();
            //					//���Ƽ�¼
            //                    clsDiag.m_strOutpatientDiagRecID=dtResult.Rows[0]["outpatientdiagrecid_chr"].ToString().Trim(); 
            //					clsDiag.m_strDiagImport=dtResult.Rows[0]["diagimport_vchr"].ToString().Trim(); 
            //					clsDiag.m_strDiagMemo=dtResult.Rows[0]["diagmemo"].ToString().Trim(); 
            //					clsDiag.m_strDiagStd=dtResult.Rows[0]["diagstd_vchr"].ToString().Trim(); 
            //					clsDiag.m_strCurePrinciple=dtResult.Rows[0]["curprinciple_vchr"].ToString().Trim(); 
            //					clsDiag.m_strCureStd=dtResult.Rows[0]["curestd_vchr"].ToString().Trim(); 
            //					clsDiag.m_strDefend=dtResult.Rows[0]["defend_vchr"].ToString().Trim(); 
            //				}
            //			}
            //			catch(Exception objEx)
            //			{
            //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //				bool blnRes = objLogger.LogError(objEx);
            //				lngRes=-2;
            //			}
            return lngRes;
        }
        #endregion

        #region ���没��
        /// <summary>
        /// ���没��(�ô����������޸ı��棩
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegID"></param>
        /// <param name="clsCase"></param>
        /// <param name="clsDiag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveCase(clsOutpatientCaseHis_VO clsVO)
        {
            string strCaseID = "";
            string strSQL;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                if (clsVO.m_strCaseHisID == null || clsVO.m_strCaseHisID == "") //����
                {
                    lngRes = HRPSvc.lngGenerateID(18, "casehisid_chr", "t_opr_outpatientcasehis", out strCaseID);
                    if (lngRes < 0)
                        return lngRes;
                    strSQL = "insert into  t_opr_outpatientcasehis " +
                           "(modifydate_dat,patientid_chr,registerid_chr,diagdr_chr, " +
                           "diagdept_chr,diseasememo_vchr,diagmemo_vchr,diagresult_vchr,recordemp_chr, " +
                           "recorddate_dat,status_int,diagmain_vchr,diagmain_xml_vchr,diagcurr_vchr, " +
                           "diagcurr_xml_vhcr,diaghis_vchr,diaghis_xml_vchr,aidcheck_vchr,aidcheck_xml_vchr, " +
                           "diag_vchr,diag_xml_vchr,treatment_vchr,treatment_xml_vchr,remark_vchr, " +
                           "remark_xml_vchr,anaphylaxis_vchr,casehisid_chr )" +
                           " Values (sysdate,?,?,?,?,?,?,?,?,sysdate,1,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";
                }
                else
                {
                    strCaseID = clsVO.m_strCaseHisID;
                    strSQL = "UPDate  t_opr_outpatientcasehis " +
                           "Set modifydate_dat=sysdate,patientid_chr=?,registerid_chr=?,diagdr_chr=?, " +
                           "diagdept_chr=?,diseasememo_vchr=?,diagmemo_vchr=?,diagresult_vchr=?,recordemp_chr=?, " +
                           "recorddate_dat=sysdate,status_int=2,diagmain_vchr=?,diagmain_xml_vchr=?,diagcurr_vchr=?, " +
                           "diagcurr_xml_vhcr=?,diaghis_vchr=?,diaghis_xml_vchr=?,aidcheck_vchr=?,aidcheck_xml_vchr=?, " +
                           "diag_vchr=?,diag_xml_vchr=?,treatment_vchr=?,treatment_xml_vchr=?,remark_vchr=?, " +
                           "remark_xml_vchr=?,anaphylaxis_vchr=? " +
                           "Where casehisid_chr=? ";
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �������Ƽ�¼
        /// <summary>
        /// �������Ƽ�¼
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="clsVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveCure(clsOutpatientDiagRec_VO clsVO)
        {
            string strID = "";
            long lngRec = 0;
            string strSQL;
            System.Data.IDataParameter[] objPara;
            long lngRes = 0;
            long lngRegs = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            try
            {
                if (clsVO.m_strOutpatientDiagRecID == null || clsVO.m_strOutpatientDiagRecID == "") //����
                {
                    lngRes = HRPSvc.lngGenerateID(18, "outpatientdiagrecid_chr", "t_opr_outpatientdiagrec", out strID);
                    if (lngRes < 0)
                        return lngRes;
                    strSQL = "Insert into t_opr_outpatientdiagrec (outpatientdiagrecid_chr,patientid_chr,registerid_chr, " +
                           "diagdr_chr,diagdept_chr,recordemp_chr,recorddate_dat, " +
                           "diagimport_vchr,diagmemo_vchr,diagstd_vchr, " +
                           "curprinciple_vchr,curestd_vchr,defend_vchr ) Values " +
                           " (?,?,?,?,?,?,sysdate,?,?,?,?,?,?)";

                    objPara = HRPSvc.CreateDatabaseParameter(new object[]
                                                   {strID,clsVO.m_strPatientID,clsVO.m_strRegisterID,
                                                    clsVO.m_strDiagDrID,clsVO.m_strDiagDeptID,
                                                    clsVO.m_strRecordEmpID,clsVO.m_strDiagImport,
                                                    clsVO.m_strDiagMemo,clsVO.m_strDiagStd,clsVO.m_strCurePrinciple,
                                                    clsVO.m_strCureStd,clsVO.m_strDefend});
                }
                else
                {
                    strID = clsVO.m_strOutpatientDiagRecID;
                    strSQL = "UPdate t_opr_outpatientdiagrec Set diagdr_chr=?, " +
                           "diagdept_chr=?,recordemp_chr=?," +
                           "diagimport_vchr=?, diagmemo_vchr=?,diagstd_vchr=?, " +
                           "curprinciple_vchr=?, curestd_vchr=?, defend_vchr=?  " +
                           " Where outpatientdiagrecid_chr=? ";

                    objPara = HRPSvc.CreateDatabaseParameter(new object[]
                                                   {clsVO.m_strDiagDrID,clsVO.m_strDiagDeptID,
                                                    clsVO.m_strRecordEmpID,clsVO.m_strDiagImport,
                                                    clsVO.m_strDiagMemo,clsVO.m_strDiagStd,clsVO.m_strCurePrinciple,
                                                    clsVO.m_strCureStd,clsVO.m_strDefend,strID});
                }

                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

    }
}
