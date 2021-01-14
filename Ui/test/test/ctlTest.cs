using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public  class ctlTest : IDisposable
    {

        public static int Add()
        {
            return 1;
        }

        ~ctlTest()
         {
            MessageBox.Show("ctlTest");
            Dispose();
         }

        public void Dispose()
        {
            MessageBox.Show("Dispose");
        }
    }
}
