using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
    /// �и����̼�¼�ؼ�
	/// </summary>
    public class ctlPartogramRecord : System.Windows.Forms.PictureBox
	{
		private System.ComponentModel.IContainer components;

        #region user-defined variable
        private clsPartogramPrintTool m_objPrintTool;

        private clsPartogramManager m_objPartogramManager;

        private string m_strMessage = string.Empty;


        int m_intSelectPageNumber = 0;

		#endregion

		///<summary>
		/// Windows.Forms ��׫д�����֧���������
		///</summary>
		public ctlPartogramRecord()
		{
			m_objPartogramManager = new clsPartogramManager();
            m_objPrintTool = new clsPartogramPrintTool(clsPartogramLocation.c_intTopBankHeight, m_objPartogramManager);

            m_mthReset();

            this.Size = new Size(clsPartogramLocation.m_intTotalWidth, clsPartogramLocation.m_intTotalHeight);
            this.BackColor = Color.White;
		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
                if (m_objPrintTool != null)
                {
                    m_objPrintTool.m_mthClear();
                    m_objPrintTool = null;
                }
            }
            base.Dispose( disposing );
		}

        #region ����
        /// <summary>
        /// ��ǰҳ������0��ʼ
        /// </summary>
        [Browsable(false)]
        public int m_IntSelectPageNumber
        {
            get { return m_intSelectPageNumber; }
            set
            {
                m_intSelectPageNumber = value;
                if (m_intSelectPageNumber < 0)
                    m_intSelectPageNumber = 0;
                m_objPrintTool.m_IntSelectPageNumber = m_intSelectPageNumber;
                this.Invalidate();
            }
        }
        /// <summary>
        /// ��ȡ��ǰ�����ҳ,���ʱ������
        /// </summary>
        [Browsable(false)]
        public int m_IntMaxPageNumber
        {
            get
            {
                return (m_objPartogramManager.m_IntGetMaxRecordCount<=0? 1:m_objPartogramManager.m_IntGetMaxRecordCount)/24;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public clsPartogramManager m_ObjPartogramManager
        {
            get { return m_objPartogramManager; }
        }
        #endregion ����

		#region //������õ���ɫ

		//�����ɫ
		private Color m_clrBorder=Color.Black ;
        /// <summary>
        /// ��߿���ɫ
        /// </summary>
        [Description("��߿���ɫ"),Category("��ɫ����")]
		public Color m_ClrBorder
		{
			set 
			{
				m_clrBorder=value;
                m_objPrintTool.m_ClrBorder = value;
				Invalidate();
			}
			get
			{
				return m_clrBorder;
			}
		}

		//������ɫ
		private Color m_clrGridLine=Color.Black ;
        
        /// <summary>
        /// ������ɫ
        /// </summary>
        [Description("������ɫ"),Category("��ɫ����")]
		public Color m_ClrGridLine
		{
			get
			{
				return m_clrGridLine;
			}
			set 
			{
                m_clrGridLine = value;
                m_objPrintTool.m_ClrGridLine = value;
				Invalidate();
			}

		}

        //�����ڿ�����ɫ
        private Color m_clrUterineNect = Color.Red;

        /// <summary>
        /// �����ڿ������ɫ
        /// </summary>
        [Description("�����ڿ������ɫ"), Category("��ɫ����")]
        public Color m_ClrUterineNect
		{
			get
			{
                return m_clrUterineNect;
			}
			set 
			{
                m_clrUterineNect = value;
                m_objPrintTool.m_ClrUterineNect = value;
				Invalidate();
			}

		}


		//�ı�����ɫ
		private Color m_clrDrawText=Color.Black  ;

        /// <summary>
        /// �ı�����ɫ
        /// </summary>
        [Description("�ı�����ɫ"), Category("��ɫ����")]
		public Color m_ClrDrawText
		{
			get
			{
				return m_clrDrawText;
			}
			set 
			{
				m_clrDrawText=value;
                m_objPrintTool.m_ClrDrawText = value;
				Invalidate();
			}

		}

        //�����ڿ������������ɫ
        private Color m_clrUterineNectLine = Color.Black;

        /// <summary>
        /// �����ڿ������������ɫ
        /// </summary>
        [Description("�����ڿ������������ɫ"), Category("��ɫ����")]
        public Color m_ClrUterineNectLine
		{
			get
			{
                return m_clrUterineNectLine;
			}
			set 
			{
                m_clrUterineNectLine = value;
                m_objPrintTool.m_ClrUterineNectLine = value;
				Invalidate();
			}

        }
        //̥��ͷ�½���������ɫ
        private Color m_clrFetalHeadLine = Color.Black;

        /// <summary>
        /// �����ڿ������������ɫ
        /// </summary>
        [Description("̥��ͷ�½���������ɫ"), Category("��ɫ����")]
        public Color m_ClrFetalHeadLine
        {
            get
            {
                return m_clrFetalHeadLine;
            }
            set
            {
                m_clrFetalHeadLine = value;
                m_objPrintTool.m_ClrFetalHeadLine = value;
                Invalidate();
            }

        }

        //̥��ͷ�½���������ɫ
        private Color m_clrFetalHead = Color.Black;

        /// <summary>
        /// ̥��ͷ�½�����ɫ
        /// </summary>
        [Description("̥��ͷ�½�����ɫ"), Category("��ɫ����")]
        public Color m_ClrFetalHead
        {
            get
            {
                return m_clrFetalHead;
            }
            set
            {
                m_clrFetalHead = value;
                m_objPrintTool.m_ClrFetalHead = value;
                Invalidate();
            }

        }
        //̥��ͷ�½���������ɫ
        private Color m_clrMarkLine = Color.Black;

        /// <summary>
        /// ��׼����ɫ
        /// </summary>
        [Description("��׼����ɫ"), Category("��ɫ����")]
        public Color m_ClrMarkLine
        {
            get
            {
                return m_clrMarkLine;
            }
            set
            {
                m_clrMarkLine = value;
                m_objPrintTool.m_ClrMarkLine = value;
                Invalidate();
            }

        }
		#endregion 
	
        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            m_mthPaint(pe.Graphics);
        }

		private void m_mthPaint(System.Drawing.Graphics p_objGrp)
		{
            m_objPrintTool.m_mthDrawLine(p_objGrp);

            m_objPrintTool.m_mthDrawText(p_objGrp);

            m_objPrintTool.m_mthDrawValues(p_objGrp);

            m_objPrintTool.m_mthDrawPoints(p_objGrp);

        }
        #endregion Draw

        #region �ؼ����¼�
        /// <summary>
        /// ��ǰСʱ������¼�
        /// </summary>
        public event EventHandler m_evnPartogramEveryHourMouseDown;
        /// <summary>
        /// ���ڴ�С��̥��ͷ�ĵ�ĵ���¼�
        /// Ĭ���ǰ�����ǰ������е�
        /// </summary>
        public event EventHandler m_evnPartogramPointMouseDown;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


            //�����곬����Χ����.
            if (e.X < clsPartogramLocation.c_intLeftTextWidth || e.X > (clsPartogramLocation.m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth))
                return;

            //��ǰСʱ���¼�
            if (m_evnPartogramEveryHourMouseDown != null)
               // && (e.Y <= clsPartogramLocation.c_intFetalRhythmBottom || e.Y >= clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight))
            {
                int intHour = (e.X - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intLeftTextWidth) / clsPartogramLocation.c_intGridWidth+1;
                intHour = intHour + (m_intSelectPageNumber * 24);
                clsPartogram_VO objPartogram= m_objPartogramManager.m_objGetRecord(intHour);
                if (objPartogram != null)
                {
                    clsPartogramEveryHourEventArgs objEventArgs = new clsPartogramEveryHourEventArgs();
                    objEventArgs.m_objPartogramArgs = objPartogram;

                    m_evnPartogramEveryHourMouseDown(null, objEventArgs);
                }
            }
            //���ڴ�С��̥��ͷ�ĵ�ĵ���¼�
            //if (m_evnPartogramPointMouseDown != null
            //    && (e.Y > clsPartogramLocation.c_intFetalRhythmBottom && e.Y < clsPartogramLocation.c_intUterineNectBottom))
            //{
            //    int intHour = (e.X - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intLeftTextWidth) / clsPartogramLocation.c_intGridWidth+1;
            //    intHour = intHour + (m_intSelectPageNumber * 24);
            //    clsPartogram_VO objPartogram  = m_objPartogramManager.m_objGetRecord(intHour);
            //    if (objPartogram != null)
            //    {
            //        if (objPartogram.m_ObjPointArr != null)
            //        {
            //            if (objPartogram.m_ObjPointArr.Length > 0)
            //            {
            //                List<clsPartogram_Point> list = new List<clsPartogram_Point>(2);
            //                int intFirstValue = 10-((e.Y - clsPartogramLocation.c_intFetalRhythmBottom-clsPartogramLocation.c_intFlawHeight)/clsPartogramLocation.c_intGridWidth);
            //                for (int i = 0 ; i < objPartogram.m_ObjPointArr.Length ; i++)
            //                {
            //                    if ((objPartogram.m_ObjPointArr[i].m_intPointType_INT == 0 && objPartogram.m_ObjPointArr[i].m_fltPointValue_INT >= (float)intFirstValue && objPartogram.m_ObjPointArr[i].m_fltPointValue_INT <= (float)(intFirstValue + 1)) || 
            //                        (objPartogram.m_ObjPointArr[i].m_intPointType_INT == 1 && objPartogram.m_ObjPointArr[i].m_fltPointValue_INT <= (float)(5-intFirstValue) && objPartogram.m_ObjPointArr[i].m_fltPointValue_INT >= (float)(5-intFirstValue-1)))
            //                        list.Add(objPartogram.m_ObjPointArr[i]);
            //                }
            //                if (list.Count > 0)
            //                {
            //                    clsPartogramPointEventArgs objEventArgs = new clsPartogramPointEventArgs();
            //                    objEventArgs.m_intHour = intHour;
            //                    objEventArgs.m_objArgsValueArr = list.ToArray();
            //                    m_evnPartogramPointMouseDown(null, objEventArgs);
            //                }
            //            }
            //        }
            //    }
            //}
        }
		#endregion

        /// <summary>
        /// ����
        /// </summary>
        public void m_mthReset()
        {
            m_strMessage = string.Empty;
            m_objPartogramManager.m_mthClear();
            m_IntSelectPageNumber = 0;
        }
		/// <summary>
		/// ˢ��
		/// </summary>
		public void m_mthRefreshDispaly()
		{
			this.Invalidate();
		}
        /// <summary>
        /// ��ȡ��ǰҳ��������Ӽ�¼��Сʱ
        /// </summary>
        /// <returns></returns>
        public int[] m_intGetAvailableHours()
        {
            int intFirstHour = m_intSelectPageNumber * 24+1;
            int intLastHour = intFirstHour+24;
            List<int> arlHours = new List<int>(24);
            for (int i = intFirstHour ; i < intLastHour ; i++)
            {
                if(!m_objPartogramManager.m_blnCheckTimeExist(i))
                {
                    arlHours.Add(i);
                }
            }
            if (arlHours.Count > 0)
                return arlHours.ToArray();
            else
                return null;
        }
    }
	/// <summary>
	/// 
	/// </summary>
    public class clsPartogramEveryHourEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public clsPartogram_VO m_objPartogramArgs;
    }
    /// <summary>
    /// 
    /// </summary>
    public class clsPartogramPointEventArgs : EventArgs
    {
        public int m_intHour = -1; 
        /// <summary>
        /// 
        /// </summary>
        public clsPartogram_Point[] m_objArgsValueArr;
    }
}
