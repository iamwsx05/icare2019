using System;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// �и����̼�¼������
    /// </summary>
    public class clsPartogramManager
    {
        //�������̼�¼��
        private Hashtable m_hasPartogramAll;

        /// <summary>
        /// ���캯��
        /// </summary>
        public clsPartogramManager()
        {
            m_hasPartogramAll = new Hashtable(24);
        }
        /// <summary>
        /// ��ղ��̼�¼
        /// </summary>
        public void m_mthClear()
        {
            m_hasPartogramAll.Clear();
        }
        /// <summary>
        /// �Ƿ��Ѿ������ض���ʱ���ļ�¼
        /// </summary>
        /// <param name="p_intTime"></param>
        /// <returns></returns>
        public bool m_blnCheckTimeExist(int p_intTime)
        {
            return m_hasPartogramAll.ContainsKey(p_intTime);
        }
        /// <summary>
        /// ���һСʱ��¼
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns>��1������Ϊ�գ�0����¼�Ѿ����ڣ�1����ӳɹ�</returns>
        public int m_intAdd(clsPartogram_VO p_objPartogram)
        {
            if (p_objPartogram == null)
                return -1;
            if (m_blnCheckTimeExist(p_objPartogram.m_intPARTOGRAM_INT))
                return 0;
            m_hasPartogramAll.Add(p_objPartogram.m_intPARTOGRAM_INT, p_objPartogram);
            return 1;
        }
        /// <summary>
        /// �������ȫ����¼
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns></returns>
        public string m_strReAddRange(clsPartogram_VO[] p_objPartogram)
        {
            if (p_objPartogram == null || p_objPartogram.Length == 0)
                return "�������ݴ���";
            //p_objPartogram = m_objSort(p_objPartogram);
            m_hasPartogramAll.Clear();
            string str = string.Empty;
            for (int i = 0 ; i < p_objPartogram.Length ; i++)
            {
                if (!m_blnCheckTimeExist(p_objPartogram[i].m_intPARTOGRAM_INT))
                    m_hasPartogramAll.Add(p_objPartogram[i].m_intPARTOGRAM_INT, p_objPartogram[i]);
                else 
                {
                    str += "'"+p_objPartogram[i].m_intPARTOGRAM_INT+"'";
                }
            }
            if (str == string.Empty)
                str = "�ɹ��������ݣ�";
            else
                str = "�е�" + str+"Сʱ��¼�ظ���";
            return str;
        }
        /// <summary>
        /// �Ƴ���¼
        /// </summary>
        /// <param name="p_intTime">��¼����</param>
        /// <returns>��1������Ϊ�գ�0����¼�����ڣ�1���Ƴ��ɹ�</returns>
        public int m_intRemove(int p_intTime)
        {
            if (p_intTime == -1)
                return -1;
            if (!m_blnCheckTimeExist(p_intTime))
                return 0;
            m_hasPartogramAll.Remove(p_intTime);
            return 1;
        }
        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns>��1������Ϊ�գ�0����¼�����ڣ�1���޸ĳɹ�</returns>
        public int m_intModify(clsPartogram_VO p_objPartogram)
        {
            if (p_objPartogram == null)
                return -1;
            if (!m_blnCheckTimeExist(p_objPartogram.m_intPARTOGRAM_INT))
                return 0;
            m_hasPartogramAll[p_objPartogram.m_intPARTOGRAM_INT] = p_objPartogram;
            return 1;
        }
        /// <summary>
        /// ��ȡĳСʱ�ļ�¼
        /// </summary>
        /// <param name="p_intTime"></param>
        /// <returns></returns>
        public clsPartogram_VO m_objGetRecord(int p_intTime)
        {
            if (!m_blnCheckTimeExist(p_intTime))
                return null;
            return (clsPartogram_VO)m_hasPartogramAll[p_intTime];
        }

        /// <summary>
        /// ���ص����ֵ��
        /// </summary>
        /// <param name="p_intFirstHour">��ʼ��Сʱ</param>
        /// <param name="p_intLastHour">������Сʱ</param>
        /// <param name="p_intTypes">0��������1��̥��ͷ</param>
        /// <returns></returns>
        public List<clsPointValues> m_arlGetPointArr(int p_intFirstHour, int p_intLastHour,int p_intTypes)
        {
            List<clsPointValues> arlValues = new List<clsPointValues>(m_hasPartogramAll.Keys.Count*2);
            int[] intKeys = new int[m_hasPartogramAll.Count];
             m_hasPartogramAll.Keys.CopyTo(intKeys, 0);
            if (intKeys.Length > 0)
            {
                for (int i = 0 ; i < intKeys.Length ; i++)
                {
                    if (intKeys[i] >= p_intFirstHour && intKeys[i] <= p_intLastHour)
                    {
                        clsPartogram_VO objPartogram = (clsPartogram_VO)m_hasPartogramAll[intKeys[i]];
                        if (objPartogram.m_ObjPointArr != null)
                        {
                            for (int j = 0 ; j < objPartogram.m_ObjPointArr.Length ; j++)
                            {
                                if (p_intTypes == objPartogram.m_ObjPointArr[j].m_intPointType_INT)
                                {
                                    clsPointValues obj = new clsPointValues();
                                    obj.m_dtmCheckDate = objPartogram.m_ObjPointArr[j].m_dtmCheckDate;
                                    obj.m_intHour = objPartogram.m_ObjPointArr[j].m_intPARTOGRAM_INT;
                                    obj.m_intMimuteOfHour = objPartogram.m_ObjPointArr[j].m_intPointMin_INT;
                                    obj.m_fltPointValue = objPartogram.m_ObjPointArr[j].m_fltPointValue_INT;
                                    if (objPartogram.m_ObjPointArr[j].m_intChildbearingPoint == 1)
                                        obj.m_blnIsChildbearingPoint = true;
                                    arlValues.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            if (arlValues.Count > 0)
            {
                m_mthSetEndPointByCheckDate(arlValues);
                arlValues.Sort();
            }
            return arlValues;
        }

        /// <summary>
        /// ��ȡ��¼��
        /// </summary>
        public int m_IntGetRecordCount
        {
            get { return m_hasPartogramAll.Count; }
        }

        /// <summary>
        /// ��ȡ���ļ�¼ʱ����û��ʱ���أ�1
        /// </summary>
        public int m_IntGetMaxRecordCount
        {
            get
            {
                if (m_hasPartogramAll.Keys.Count > 0)
                {
                    ArrayList arlInt = new ArrayList(m_hasPartogramAll.Keys.Count);
                    arlInt.AddRange(m_hasPartogramAll.Keys);
                    arlInt.Sort();
                    return Convert.ToInt32(arlInt[arlInt.Count - 1]);
                }
                return -1;
            }
        }
        private void m_mthSetEndPointByCheckDate(List<clsPointValues> p_arlValues)
        {
            if (p_arlValues == null || p_arlValues.Count ==0) return;
            DateTime m_dtmMinDate = DateTime.MaxValue;
            DateTime m_dtmMaxDate = DateTime.MinValue;
            DateTime m_dtmMarkDate = DateTime.MaxValue;
            for (int i = 0; i < p_arlValues.Count; i++)
            {
                if (p_arlValues[i].m_dtmCheckDate < m_dtmMinDate) m_dtmMinDate = p_arlValues[i].m_dtmCheckDate;
                if(p_arlValues[i].m_dtmCheckDate > m_dtmMaxDate) m_dtmMaxDate = p_arlValues[i].m_dtmCheckDate;
                if(p_arlValues[i].m_fltPointValue >= 3 && p_arlValues[i].m_dtmCheckDate < m_dtmMarkDate) m_dtmMarkDate = p_arlValues[i].m_dtmCheckDate;
            }
            for (int j = 0; j < p_arlValues.Count; j++)
            {
                if (p_arlValues[j].m_dtmCheckDate == m_dtmMinDate) p_arlValues[j].m_intEndPoint = 0;
                if (p_arlValues[j].m_dtmCheckDate == m_dtmMaxDate) p_arlValues[j].m_intEndPoint = 1;
                if (p_arlValues[j].m_dtmCheckDate == m_dtmMarkDate) p_arlValues[j].m_blnIsMark = true;
            }
        }
    }

    /// <summary>
    /// ���λ��
    /// </summary>
    public class clsPointValues : IComparable<clsPointValues>
    {
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime m_dtmCheckDate;
        /// <summary>
        /// �ڼ�Сʱ
        /// </summary>
        public int m_intHour;
        /// <summary>
        /// ��Сʱ�ĵڼ�����
        /// </summary>
        public int m_intMimuteOfHour;
        /// <summary>
        /// �����ֵ
        /// </summary>
        public float m_fltPointValue;
        /// <summary>
        /// �Ƿ�˵�(-1=���Ƕ˵㣻0����ʼ�˵㣻1�������˵�)
        /// </summary>
        public int m_intEndPoint = -1;
        /// <summary>
        /// ��Ҫ�Ӵ˿�ʼ������
        /// </summary>
        public bool m_blnIsMark = false;
        public bool m_blnIsChildbearingPoint = false;

        /// <summary>
        /// ��ȡX����
        /// </summary>
        /// <returns></returns>
        public float m_fltGetX()
        {
            int intLeftCount = (m_intHour-1) % 24;
            if (intLeftCount < 0)
                intLeftCount = 0;
            int intLeftX = clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + clsPartogramLocation.c_intGridWidth * intLeftCount;
            float fltSubLeftW = (float)m_intMimuteOfHour / 60f * clsPartogramLocation.c_intGridWidth;
            return (float)intLeftX + fltSubLeftW;
        }
        /// <summary>
        /// ���ڿ���Y����
        /// </summary>
        /// <returns></returns>
        public float m_fltGetUterineNectY()
        { 
            int intWidth = clsPartogramLocation.c_intGridWidth;
            int intHeightCount = 11 - (int)Math.Floor(m_fltPointValue);
            float fltSubH = ((float)Math.Floor(m_fltPointValue) - m_fltPointValue) * (float)clsPartogramLocation.c_intGridWidth;
            return clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + clsPartogramLocation.c_intGridWidth * intHeightCount + fltSubH;
        }
        /// <summary>
        /// ̥��ͷ�½�Y����
        /// </summary>
        /// <returns></returns>
        public float m_fltGeFetalHeadY()
        {
            int intWidth = clsPartogramLocation.c_intGridWidth;
            int intHeightCount = 6 + (int)Math.Floor(m_fltPointValue);
            float fltSubH = (m_fltPointValue - (float)Math.Floor(m_fltPointValue)) * (float)clsPartogramLocation.c_intGridWidth;
            return clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + clsPartogramLocation.c_intGridWidth * intHeightCount + fltSubH;
        }

        #region IComparable<clsPointValues> ��Ա

        /// <summary>
        /// �Ƚ�������������
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(clsPointValues other)
        {
            int intCurrent = m_intHour * 100 + m_intMimuteOfHour;
            int intOther = other.m_intHour * 100 + other.m_intMimuteOfHour;
            return intCurrent.CompareTo(intOther);
        }

        #endregion
    }

}