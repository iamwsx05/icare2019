using System;
using System.Collections.Generic;
using System.Text;

namespace iCare
{
    public class clsInpatMedRecPrintToolFactory2
    {        
        /// <summary>
        /// ������ӡ����
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <returns></returns>
        public virtual clsInpatMedRecPrintBase m_objSubGeneratePrintTool(string p_strTypeID)
        {
            switch (p_strTypeID)
            {
                case "frmIMR_NutrientRecord":
                    return new clsIMR_NutrientPrintTool(p_strTypeID);

                case "frmIMR_Ophthalmology_F2":   // �ۿ�ר�Ʋ����������
                    return new clsIMR_OphthalmologyPrintTool_F2(p_strTypeID);

                case "frmIMR_OphthalmologyNurseRecord": // �ۿ����������¼��
                    return new clsIMR_OphthalmologyNurseRecordPrintTool(p_strTypeID);

                case "frmIMR_CesareanRecord_LJ":    // �ʹ���������¼���׽̣�
                    return new clsIMR_CesareanRecordPrintTool_LJ(p_strTypeID);

                case "frmInducedLaborRecord_LJ":    // �����ǼǱ��׽̣�
                    return new clsIMR_InducedLaborRecordPrintTool_LJ(p_strTypeID);

                default:
                    break;
            }

            return null;
        }

    }
}
