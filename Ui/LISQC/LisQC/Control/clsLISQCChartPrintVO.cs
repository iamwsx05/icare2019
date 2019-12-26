using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsLISQCChartPrintVO
    {
        // Fields
        public clsQCBatchNew objBatch;
        public clsLisQCConcentrationVO objSelectQCCon;
        public ZedGraphControl zedChart;

        // Methods
        public clsLISQCChartPrintVO()
        { 
        }
    } 
}
