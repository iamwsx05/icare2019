using System;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// 孕妇产程记录管理类
    /// </summary>
    public class clsPartogramManager
    {
        //产妇产程记录集
        private Hashtable m_hasPartogramAll;

        /// <summary>
        /// 构造函数
        /// </summary>
        public clsPartogramManager()
        {
            m_hasPartogramAll = new Hashtable(24);
        }
        /// <summary>
        /// 清空产程记录
        /// </summary>
        public void m_mthClear()
        {
            m_hasPartogramAll.Clear();
        }
        /// <summary>
        /// 是否已经包含特定的时间点的记录
        /// </summary>
        /// <param name="p_intTime"></param>
        /// <returns></returns>
        public bool m_blnCheckTimeExist(int p_intTime)
        {
            return m_hasPartogramAll.ContainsKey(p_intTime);
        }
        /// <summary>
        /// 添加一小时记录
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns>－1：参数为空；0：记录已经存在；1：添加成功</returns>
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
        /// 重新添加全部记录
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns></returns>
        public string m_strReAddRange(clsPartogram_VO[] p_objPartogram)
        {
            if (p_objPartogram == null || p_objPartogram.Length == 0)
                return "加载数据错误！";
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
                str = "成功加载数据！";
            else
                str = "有第" + str+"小时记录重复！";
            return str;
        }
        /// <summary>
        /// 移除记录
        /// </summary>
        /// <param name="p_intTime">记录索引</param>
        /// <returns>－1：参数为空；0：记录不存在；1：移除成功</returns>
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
        /// 修改记录
        /// </summary>
        /// <param name="p_objPartogram"></param>
        /// <returns>－1：参数为空；0：记录不存在；1：修改成功</returns>
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
        /// 获取某小时的记录
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
        /// 返回点的数值集
        /// </summary>
        /// <param name="p_intFirstHour">开始的小时</param>
        /// <param name="p_intLastHour">结束的小时</param>
        /// <param name="p_intTypes">0＝宫颈；1＝胎儿头</param>
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
        /// 获取记录数
        /// </summary>
        public int m_IntGetRecordCount
        {
            get { return m_hasPartogramAll.Count; }
        }

        /// <summary>
        /// 获取最大的记录时数，没有时返回－1
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
    /// 点的位置
    /// </summary>
    public class clsPointValues : IComparable<clsPointValues>
    {
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime m_dtmCheckDate;
        /// <summary>
        /// 第几小时
        /// </summary>
        public int m_intHour;
        /// <summary>
        /// 本小时的第几分钟
        /// </summary>
        public int m_intMimuteOfHour;
        /// <summary>
        /// 点的数值
        /// </summary>
        public float m_fltPointValue;
        /// <summary>
        /// 是否端点(-1=不是端点；0＝开始端点；1＝结束端点)
        /// </summary>
        public int m_intEndPoint = -1;
        /// <summary>
        /// 需要从此开始画虚线
        /// </summary>
        public bool m_blnIsMark = false;
        public bool m_blnIsChildbearingPoint = false;

        /// <summary>
        /// 获取X坐标
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
        /// 宫口开大Y坐标
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
        /// 胎儿头下降Y坐标
        /// </summary>
        /// <returns></returns>
        public float m_fltGeFetalHeadY()
        {
            int intWidth = clsPartogramLocation.c_intGridWidth;
            int intHeightCount = 6 + (int)Math.Floor(m_fltPointValue);
            float fltSubH = (m_fltPointValue - (float)Math.Floor(m_fltPointValue)) * (float)clsPartogramLocation.c_intGridWidth;
            return clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + clsPartogramLocation.c_intGridWidth * intHeightCount + fltSubH;
        }

        #region IComparable<clsPointValues> 成员

        /// <summary>
        /// 比较器，用于排序
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