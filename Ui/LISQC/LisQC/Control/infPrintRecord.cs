using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;
using System.Data;

namespace com.digitalwave.iCare.gui.LIS
{
    public interface infPrintRecord
    {
        // Methods
        void m_mthBeginPrint(object p_objPrintArg);
        void m_mthDisposePrintTools(object p_objArg);
        void m_mthEndPrint(object p_objPrintArg);
        void m_mthInitPrintContent();
        void m_mthInitPrintTool(object p_objArg);
        void m_mthPrintPage(object p_objPrintArg);
        SizeF m_SFGetPrintSize(int p_intPrintWidth, DataTable p_dtSample, DataTable p_dtResult);
    }
}
