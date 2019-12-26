using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_ApplyUnitProperty ��ժҪ˵����
    /// </summary>
    public class clsDomainController_ApplyUnitProperty : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainController_ApplyUnitProperty()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public long m_lngGetAllUnitPropertyData(out clsUnitProperty_VO[] p_objPropertyArr,
            out clsUnitPropertyValue_VO[] p_objValueArr)
        {
            p_objPropertyArr = null;
            p_objValueArr = null;
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllUnitPropertyAndDetail(out p_objPropertyArr, out p_objValueArr);

            return lngRes;
        }


        public long m_lngSave(clsApplyUnitPropertyDoc p_objDoc)
        {
            long lngRes = 0;
            clsApplyUnitPropertyDoc objDocOut = null;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSavePropertyAndValue(p_objDoc, out objDocOut);

            return lngRes;
        }


        public long m_lngConstructDoc(out clsApplyUnitPropertyDoc p_objDoc)
        {
            p_objDoc = null;
            clsUnitProperty_VO[] objPropertyArr;
            clsUnitPropertyValue_VO[] objValueArr;

            long lngRes = m_lngGetAllUnitPropertyData(out objPropertyArr, out objValueArr);

            if (lngRes <= 0)
            {
                return lngRes;
            }

            p_objDoc = new clsApplyUnitPropertyDoc();

            Hashtable hasPorperty = new Hashtable();
            if (objPropertyArr != null)
            {
                foreach (clsUnitProperty_VO objPropertyVO in objPropertyArr)
                {
                    clsApplyUnitProperty objProperty = new clsApplyUnitProperty();
                    objProperty.PropertyVO = objPropertyVO;
                    objProperty.State = enmRecordState.Original;
                    p_objDoc.Properties.Add(objProperty);
                    if (!hasPorperty.ContainsKey(objPropertyVO.m_strPROPERTY_ID_CHR))
                    {
                        hasPorperty.Add(objPropertyVO.m_strPROPERTY_ID_CHR, objProperty);
                    }
                }
            }
            if (objValueArr != null)
            {
                foreach (clsUnitPropertyValue_VO objValueVO in objValueArr)
                {
                    clsPropertyValue objValue = new clsPropertyValue();
                    objValue.ValueVO = objValueVO;
                    objValue.State = enmRecordState.Original;
                    if (hasPorperty.ContainsKey(objValueVO.m_strPROPERTY_ID_CHR))
                    {
                        ((clsApplyUnitProperty)hasPorperty[objValueVO.m_strPROPERTY_ID_CHR]).Values.Add(objValue);
                    }
                }
            }
            return lngRes;
        }


        public long m_lngGetRelatesByUnitID(string p_strApplyUnitID, out clsUnitPropertyRelate_VO[] p_objVOArr)
        {
            long lngRes = 0;
            p_objVOArr = null;
            lngRes = proxy.Service.m_lngGetRelatesByUnitID(p_strApplyUnitID, out p_objVOArr);

            return lngRes;
        }


        public long m_lngSaveRelate(string p_strApplyUnitID, clsUnitPropertyRelate_VO[] p_objVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSaveRelate(p_strApplyUnitID, p_objVOArr);

            return lngRes;
        }





        #region		�������е�����id
        /// <summary>
        /// �������е�����id
        /// </summary>
        /// <param name="p_strPropertyIDArr">�������</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public long m_mthGetAllPropertyId(out string[] p_strPropertyIDArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllPropertyId(out p_strPropertyIDArr);

            return lngRes;
        }
        #endregion

        #region		������������������ֵ����
        /// <summary>
        /// ������������������ֵ����
        /// </summary>
        /// <param name="p_strPropertyId">����id</param>
        /// <param name="p_strApplyUnitId"><���뵥Ԫid/param>
        /// <param name="p_arlResult">�������</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public long m_mthGetPropertyValue(string p_strPropertyId, string p_strApplyUnitId, out List<string> p_arlResult)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetPropertyValue(p_strPropertyId, p_strApplyUnitId, out p_arlResult);

            return lngRes;
        }
        #endregion
    }

    /// <summary>
    /// ���зֵ�����  --> ����ͨ��˵��: "�ϵ�"
    /// </summary>
    public class clsSeparateCheckApplication
    {
        public clsSeparateCheckApplication()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        private ArrayList m_hasSeparate(string p_strPropertyID, string[] p_strApplyUnitIDArr)
        {
            Hashtable htbValueArlUnitId = new Hashtable();
            ArrayList arlUnitArlValue = new ArrayList();
            foreach (string unitId in p_strApplyUnitIDArr)      //ѭ�����뵥Ԫid���������뵥Ԫid��������ֵ�Ľṹ
            {
                arlUnitArlValue.Add(this.m_mthGetUnitArlValueByPropertyAndUnitId(p_strPropertyID, unitId));
            }

            foreach (UnitArlValue uv in arlUnitArlValue)
            {
                //��������ֵΪ��ֵ�����뵥Ԫ��
                if (uv.arlValue.Count == 0)
                {
                    if (htbValueArlUnitId.Contains("!@#$%^&*"))
                    {
                        ((ArrayList)htbValueArlUnitId["!@#$%^&*"]).Add(uv.UnitId);
                    }
                    else
                    {
                        ArrayList arl = new ArrayList();
                        arl.Add(uv.UnitId);
                        htbValueArlUnitId.Add("!@#$%^&*", arl);
                    }
                }
            }

            foreach (UnitArlValue uv in arlUnitArlValue)
            {
                //��������ֵΪ��ֵ�����뵥Ԫ��������ֵΪ��ֵ�����뵥Ԫ��
                if (uv.arlValue.Count == 1)
                {
                    if (htbValueArlUnitId.Contains(uv.arlValue[0].ToString()))
                    {
                        ((ArrayList)htbValueArlUnitId[uv.arlValue[0].ToString()]).Add(uv.UnitId);
                    }
                    else
                    {
                        ArrayList arl = new ArrayList();
                        arl.Add(uv.UnitId);
                        htbValueArlUnitId.Add(uv.arlValue[0].ToString(), arl);
                    }
                }
            }

            //��������ֵΪ��ֵ�����뵥Ԫ�ķֵ�
            foreach (UnitArlValue uv in arlUnitArlValue)
            {
                if (uv.arlValue.Count <= 1)
                    continue;
                //�Զ������ֵ��ѭ���������е�����ֵΪ��ֵ�����뵥Ԫ���Ƚϣ��Ӷ����зֵ�
                bool blnAdded = false;
                foreach (string strValue in uv.arlValue)
                {
                    if (htbValueArlUnitId.Contains(strValue))
                    {
                        ((ArrayList)htbValueArlUnitId[strValue]).Add(uv.UnitId);
                        blnAdded = true;
                        break;
                    }
                }
                if (!blnAdded)
                {
                    ArrayList arlTemp = new ArrayList();
                    arlTemp.Add(uv.UnitId);
                    htbValueArlUnitId.Add(uv.arlValue[0], arlTemp);
                }
            }
            ArrayList arl11 = new ArrayList();
            arl11.AddRange(htbValueArlUnitId.Values);
            return arl11;
        }
        /// <summary>
        /// �ֵ�����
        /// </summary>
        /// <param name="p_arlAppUnit">���������������뵥Ԫ</param>
        /// <returns>�ֵ��������</returns>
        public clsSeparatedApp[] m_mthSeparateCheckApplication(string[] p_arlAppUnit)
        {
            com.digitalwave.iCare.gui.LIS.clsDomainController_ApplyUnitProperty m_objSeparateApplyUnit = new clsDomainController_ApplyUnitProperty();

            ArrayList arlUnitArlValue = new ArrayList();

            string[] strPropertyIdArr = null;
            long lngRes = 0;
            try
            {
                lngRes = m_objSeparateApplyUnit.m_mthGetAllPropertyId(out strPropertyIdArr);//��ȡ���е�����

            }
            catch (Exception ex)
            {
            }

            if (lngRes > 0 && strPropertyIdArr != null)
            {
                ArrayList arlValueArlUnitId = new ArrayList();
                ArrayList arlTemp = new ArrayList();
                arlTemp.AddRange(p_arlAppUnit);
                arlValueArlUnitId.Add(arlTemp);

                foreach (string strPropertyId in strPropertyIdArr)      //ѭ������
                {
                    ArrayList arlResult = new ArrayList();
                    foreach (ArrayList arl in arlValueArlUnitId)
                    {
                        ArrayList objarl = m_hasSeparate(strPropertyId, (string[])arl.ToArray(typeof(string)));
                        arlResult.Add(objarl);
                    }

                    arlValueArlUnitId.Clear();

                    foreach (ArrayList arlarl in arlResult)
                    {
                        arlValueArlUnitId.AddRange(arlarl);
                    }
                }

                // Result in arlValueArlUnitId 

                clsSeparatedApp[] objApps = null;
                ArrayList arl1 = new ArrayList();
                foreach (ArrayList arl2 in arlValueArlUnitId)
                {
                    clsSeparatedApp objApp = new clsSeparatedApp();
                    objApp.m_strApplyUnits = (string[])arl2.ToArray(typeof(string));
                    arl1.Add(objApp);
                }
                objApps = (clsSeparatedApp[])arl1.ToArray(typeof(clsSeparatedApp));
                return objApps;
            }
            else
            {
                clsSeparatedApp objApp = new clsSeparatedApp();
                objApp.m_strApplyUnits = p_arlAppUnit;
                clsSeparatedApp[] objApps = new clsSeparatedApp[1] { objApp };
                return objApps;
            }
        }


        //�����뵥Ԫ������ֵ�飩�ṹ		���뵥Ԫ������������ֵ����
        private class UnitArlValue
        {
            public string UnitId = null;

            public List<string> arlValue = new List<string>();
        }

        /// <summary>
        /// ��������id�����뵥Ԫid��ȡ�����뵥Ԫ������ֵ�飩�ṹ
        /// </summary>
        /// <param name="p_strPropertyId">����id</param>
        /// <param name="p_strUnitId">���뵥Ԫid</param>
        /// <returns>�����뵥Ԫ������ֵ�飩�ṹ</returns>
        private UnitArlValue m_mthGetUnitArlValueByPropertyAndUnitId(string p_strPropertyId, string p_strUnitId)
        {
            UnitArlValue uv = new UnitArlValue();

            com.digitalwave.iCare.gui.LIS.clsDomainController_ApplyUnitProperty m_objSeparateApplyUnit = new com.digitalwave.iCare.gui.LIS.clsDomainController_ApplyUnitProperty();

            List<string> arlPropertyValue = null;
            long lngRes = 0;
            try
            {
                lngRes = m_objSeparateApplyUnit.m_mthGetPropertyValue(p_strPropertyId, p_strUnitId, out arlPropertyValue);//
            }
            catch { }

            if (lngRes > 0 && arlPropertyValue != null)
            {
                uv.UnitId = p_strUnitId;
                uv.arlValue = arlPropertyValue;
            }
            else
            {
                uv.UnitId = p_strUnitId;
                uv.arlValue = new List<string>();
            }

            return uv;
        }
    }
    public class clsSeparatedApp
    {
        public string[] m_strApplyUnits;
    }
}
