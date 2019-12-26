using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using weCare.Core.Entity;
using ZedGraph;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsQCDataPair
    {
        // Fields
        private List<clsLisQCDataVO> m_arrData;
        private DateTime m_dtQcdate;
        private int m_intSeq;

        // Methods
        public clsQCDataPair()
        {
            this.m_arrData = new List<clsLisQCDataVO>(); 
        }

        public bool Add(clsLisQCDataVO p_objDataVO)
        {
            bool isExist = IsExistSameConcentration(p_objDataVO);
            if (isExist)
            {
                return false;
            }
            m_arrData.Add(p_objDataVO);
            return true;
        }

        private static int CompareQCDataVO(clsLisQCDataVO x, clsLisQCDataVO y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                    return 1;
                else
                {
                    if (x.m_datQCDate > y.m_datQCDate)
                    {
                        return 1;
                    }
                    if (x.m_datQCDate == y.m_datQCDate)
                    {
                        if (x.m_intSeq > y.m_intSeq)
                        {
                            return 1;
                        }
                        else
                        {
                            if (x.m_intSeq == y.m_intSeq)
                            {
                                return 0;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public static List<clsQCDataPair> GetQCDataPairList(List<clsLisQCDataVO> p_dataList)
        {
            List<clsQCDataPair> lstDataPair = new List<clsQCDataPair>();
            p_dataList.Sort(CompareQCDataVO);

            bool isAdd = false;
            foreach (clsLisQCDataVO vo in p_dataList)
            {
                foreach (clsQCDataPair pair in lstDataPair)
                {
                    isAdd = false;
                    if (vo.m_datQCDate == pair.m_dtQcdate)
                    {
                        if (pair.Add(vo))
                        {
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                //不存在相同时间的
                if (!isAdd)
                {
                    clsQCDataPair newPair2 = new clsQCDataPair();
                    newPair2.Add(vo);
                    newPair2.m_dtQcdate = vo.m_datQCDate;
                    lstDataPair.Add(newPair2);
                }

            }
            return lstDataPair;
        }

        private bool IsExistSameConcentration(clsLisQCDataVO p_objDataVO)
        {
            foreach (clsLisQCDataVO vo in m_arrData)
            {
                if (p_objDataVO.m_intConcentrationSeq == vo.m_intConcentrationSeq)
                {
                    return true;
                }
            }
            return false;
        }

        // Properties
        public clsLisQCDataVO this[int contrationId]
        {
            get
            {
                for (int i = 0; i < m_arrData.Count; i++)
                {
                    if (m_arrData[i].m_intConcentrationSeq == contrationId)
                    {
                        return m_arrData[i];
                    }
                }
                return null;
            }
        }

        public DateTime QCDate
        {
            get { return m_dtQcdate; }
            set { m_dtQcdate = value; }
        }

        public int Seq
        {
            get { return m_intSeq; }
            set { m_intSeq = value; }
        }
    } 
}
