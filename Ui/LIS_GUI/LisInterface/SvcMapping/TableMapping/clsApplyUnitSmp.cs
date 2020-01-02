﻿using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui
{

    /// <summary>
    /// 申请单元Mapping
    /// </summary>
    internal class clsApplyUnitSmp : clsDomainController_Base
    {
        #region 构  造


        private clsApplyUnitSmp()
        {
        }

        public static clsApplyUnitSmp s_obj
        {
            get
            {
                return new clsApplyUnitSmp();
            }
        }

        #endregion

        #region 根据申请单元Id获取申请单元VO

        /// <summary>
        /// 根据申请单元Id获取申请单元VO
        /// </summary>
        /// <param name="unitId">申请单元Id</param>
        /// <param name="objApplayUnit">申请单元VO</param>
        /// <returns></returns>
        public long m_lngGetApplyUnitVO(string unitId, out clsApplUnit_VO applayUnitVO)
        {
            long lngRes = 0;
            applayUnitVO = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetApplyUnitVOByApplyUnitID(unitId, out applayUnitVO);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

        #region 根据一组申请单元ID得到其所包含的样本ID列表

        /// <summary>
        /// 根据一组申请单元ID得到其所包含的样本ID列表
        /// </summary>
        /// <param name="p_strApplyUnitIDArr"></param>
        /// <param name="p_strSampleTypeIDArr"></param>
        /// <returns></returns>
        public long m_lngGetSampleTypeIdList(string[] arrApplyUnitId, out string[] arrSampleTypeId)
        {
            long lngRes = 0;
            arrSampleTypeId = null;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetSampleTypeIDList(arrApplyUnitId, out arrSampleTypeId);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

    }
}
