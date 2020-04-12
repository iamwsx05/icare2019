using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// MK3ø������Ϣ������
    /// </summary>
    class clsDPEMessage
    {
        /// <summary>
        /// �������������
        /// </summary>
        public const byte m_bytStart = (byte)'R';
        /// <summary>
        /// ����Ӧ��
        /// </summary>
        public const string m_strTSDWN = "OK";
        /// <summary>
        /// �Ͽ�����
        /// HEX 51
        /// </summary>
        public const byte m_bytDisconnect = (byte)'Q';
        /// <summary>
        /// �����հ�
        /// HEX 41
        /// </summary>
        public const byte m_bytAirBlank = (byte)'A';
        /// <summary>
        /// ��ģʽ���ò���
        /// HEX 58
        /// </summary>
        public const byte m_bytShock = (byte)'X';
        /// <summary>
        /// ���ٶ�
        /// HEX 32
        /// </summary>
        public const byte m_bytShockViews = (byte)'2';
        /// <summary>
        /// ��ø���
        /// HEX 5A
        /// </summary>
        public const byte m_bytShockTime = (byte)'Z';
        /// <summary>
        /// ��ø���
        /// HEX 30
        /// </summary>
        public const byte m_bytShockMin = (byte)'0';
        /// <summary>
        /// ��ø���
        /// HEX 35
        /// </summary>
        public const byte m_bytShockSeconds = (byte)'5';
        /// <summary>
        /// ���巽ʽ
        /// HEX 45
        /// </summary>
        public const byte m_bytContinueWay = (byte)'E';
        /// <summary>
        /// ���巽ʽ
        /// HEX 30
        /// </summary>
        public const byte m_bytContinueModel = (byte)'0';
        /// <summary>
        /// ѡ���˹�Ƭ
        /// HEX 46
        /// </summary>
        public const byte m_bytSelectFilter = (byte)'F';
        /// <summary>
        /// ѡ���˹�Ƭ
        /// HEX 32
        /// </summary>
        public const byte m_bytSelectFilterModel = (byte)'2';
        /// <summary>
        /// ����ģʽ
        /// HEX 50
        /// </summary>
        public const byte m_bytMeasurement = (byte)'P';
        /// <summary>
        /// HEX 0A
        /// </summary>
        public const byte m_bytCR = (byte)'\r';
        /// <summary>
        /// HEX 0D
        /// </summary>
        public const byte m_bytLF = (byte)'\n';
    }
}
