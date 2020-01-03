using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmTISSValuation
    {

        #region Dispose
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTISSValuation));
            this.chkItem1 = new System.Windows.Forms.CheckBox();
            this.chkItem2 = new System.Windows.Forms.CheckBox();
            this.chkItem3 = new System.Windows.Forms.CheckBox();
            this.chkItem4 = new System.Windows.Forms.CheckBox();
            this.chkItem5 = new System.Windows.Forms.CheckBox();
            this.chkItem6 = new System.Windows.Forms.CheckBox();
            this.chkItem9 = new System.Windows.Forms.CheckBox();
            this.chkItem10 = new System.Windows.Forms.CheckBox();
            this.chkItem8 = new System.Windows.Forms.CheckBox();
            this.chkItem7 = new System.Windows.Forms.CheckBox();
            this.chkItem11 = new System.Windows.Forms.CheckBox();
            this.chkItem14 = new System.Windows.Forms.CheckBox();
            this.chkItem13 = new System.Windows.Forms.CheckBox();
            this.chkItem12 = new System.Windows.Forms.CheckBox();
            this.chkItem15 = new System.Windows.Forms.CheckBox();
            this.chkItem16 = new System.Windows.Forms.CheckBox();
            this.chkItem19 = new System.Windows.Forms.CheckBox();
            this.chkItem20 = new System.Windows.Forms.CheckBox();
            this.chkItem18 = new System.Windows.Forms.CheckBox();
            this.chkItem17 = new System.Windows.Forms.CheckBox();
            this.chkItem22 = new System.Windows.Forms.CheckBox();
            this.chkItem23 = new System.Windows.Forms.CheckBox();
            this.chkItem21 = new System.Windows.Forms.CheckBox();
            this.chkItem24 = new System.Windows.Forms.CheckBox();
            this.chkItem25 = new System.Windows.Forms.CheckBox();
            this.chkItem26 = new System.Windows.Forms.CheckBox();
            this.chkItem27 = new System.Windows.Forms.CheckBox();
            this.chkItem28 = new System.Windows.Forms.CheckBox();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.tabPage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.txtEvalDoctor = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblMonth2 = new System.Windows.Forms.Label();
            this.lblDay2 = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.m_cmdEvalDoctor = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvActivityTime
            // 
            this.trvActivityTime.LineColor = System.Drawing.Color.Black;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = true;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // chkItem1
            // 
            this.chkItem1.ForeColor = System.Drawing.Color.Black;
            this.chkItem1.Location = new System.Drawing.Point(12, 12);
            this.chkItem1.Name = "chkItem1";
            this.chkItem1.Size = new System.Drawing.Size(744, 24);
            this.chkItem1.TabIndex = 50;
            this.chkItem1.Tag = "5";
            this.chkItem1.Text = "1、标准检测：每小时生命特征、液体平衡的常规记录和计算";
            // 
            // chkItem2
            // 
            this.chkItem2.ForeColor = System.Drawing.Color.Black;
            this.chkItem2.Location = new System.Drawing.Point(12, 44);
            this.chkItem2.Name = "chkItem2";
            this.chkItem2.Size = new System.Drawing.Size(744, 24);
            this.chkItem2.TabIndex = 60;
            this.chkItem2.Tag = "1";
            this.chkItem2.Text = "2、实验室检查：生化和微生物学检查";
            // 
            // chkItem3
            // 
            this.chkItem3.ForeColor = System.Drawing.Color.Black;
            this.chkItem3.Location = new System.Drawing.Point(12, 76);
            this.chkItem3.Name = "chkItem3";
            this.chkItem3.Size = new System.Drawing.Size(744, 24);
            this.chkItem3.TabIndex = 70;
            this.chkItem3.Tag = "2";
            this.chkItem3.Text = "3、单一药物：静脉、肌肉、皮下注射，和（或）口服（例如经胃管给药）";
            // 
            // chkItem4
            // 
            this.chkItem4.ForeColor = System.Drawing.Color.Black;
            this.chkItem4.Location = new System.Drawing.Point(12, 108);
            this.chkItem4.Name = "chkItem4";
            this.chkItem4.Size = new System.Drawing.Size(744, 24);
            this.chkItem4.TabIndex = 80;
            this.chkItem4.Tag = "3";
            this.chkItem4.Text = "4、静脉使用多种药物：单次静注或持续输注1种以上药物";
            // 
            // chkItem5
            // 
            this.chkItem5.ForeColor = System.Drawing.Color.Black;
            this.chkItem5.Location = new System.Drawing.Point(12, 140);
            this.chkItem5.Name = "chkItem5";
            this.chkItem5.Size = new System.Drawing.Size(744, 24);
            this.chkItem5.TabIndex = 90;
            this.chkItem5.Tag = "1";
            this.chkItem5.Text = "5、常规更换敷料：褥疮的护理和预防，每日更换一次敷料";
            // 
            // chkItem6
            // 
            this.chkItem6.ForeColor = System.Drawing.Color.Black;
            this.chkItem6.Location = new System.Drawing.Point(12, 172);
            this.chkItem6.Name = "chkItem6";
            this.chkItem6.Size = new System.Drawing.Size(744, 24);
            this.chkItem6.TabIndex = 100;
            this.chkItem6.Tag = "1";
            this.chkItem6.Text = "6、频繁更换敷料：频繁更换敷料（每次护理班至少更换1次）和（或）大面积伤口护理";
            // 
            // chkItem9
            // 
            this.chkItem9.Location = new System.Drawing.Point(12, 40);
            this.chkItem9.Name = "chkItem9";
            this.chkItem9.Size = new System.Drawing.Size(763, 23);
            this.chkItem9.TabIndex = 130;
            this.chkItem9.Tag = "2";
            this.chkItem9.Text = "9、其他通气支持：经气管插管自主呼吸，不应用PEEP；除了所应用的机械通气模式外， 提供任何形式的氧疗";
            // 
            // chkItem10
            // 
            this.chkItem10.Location = new System.Drawing.Point(12, 76);
            this.chkItem10.Name = "chkItem10";
            this.chkItem10.Size = new System.Drawing.Size(763, 23);
            this.chkItem10.TabIndex = 140;
            this.chkItem10.Tag = "1";
            this.chkItem10.Text = "10、人工气道的护理：气管插管或气管切开的护理";
            // 
            // chkItem8
            // 
            this.chkItem8.Location = new System.Drawing.Point(12, 12);
            this.chkItem8.Name = "chkItem8";
            this.chkItem8.Size = new System.Drawing.Size(763, 23);
            this.chkItem8.TabIndex = 120;
            this.chkItem8.Tag = "5";
            this.chkItem8.Text = "8、机械通气：任何形式的机械通气/辅助通气，无论是否使用PEEP或肌松药；加用PEEP的自主呼吸";
            // 
            // chkItem7
            // 
            this.chkItem7.ForeColor = System.Drawing.Color.Black;
            this.chkItem7.Location = new System.Drawing.Point(12, 204);
            this.chkItem7.Name = "chkItem7";
            this.chkItem7.Size = new System.Drawing.Size(744, 24);
            this.chkItem7.TabIndex = 110;
            this.chkItem7.Tag = "3";
            this.chkItem7.Text = "7、引流管的护理：除胃管以外的所有导管的护理";
            // 
            // chkItem11
            // 
            this.chkItem11.Location = new System.Drawing.Point(12, 108);
            this.chkItem11.Name = "chkItem11";
            this.chkItem11.Size = new System.Drawing.Size(763, 23);
            this.chkItem11.TabIndex = 150;
            this.chkItem11.Tag = "1";
            this.chkItem11.Text = "11、改善肺功能：胸部理疗，刺激性肺量计、吸入疗法、气管内吸痰";
            // 
            // chkItem14
            // 
            this.chkItem14.Location = new System.Drawing.Point(8, 76);
            this.chkItem14.Name = "chkItem14";
            this.chkItem14.Size = new System.Drawing.Size(752, 21);
            this.chkItem14.TabIndex = 180;
            this.chkItem14.Tag = "4";
            this.chkItem14.Text = "14、静脉补充丢失的大量液体：输液量>3L/(m  * d)，不论液体种类";
            // 
            // chkItem13
            // 
            this.chkItem13.Location = new System.Drawing.Point(8, 44);
            this.chkItem13.Name = "chkItem13";
            this.chkItem13.Size = new System.Drawing.Size(752, 21);
            this.chkItem13.TabIndex = 170;
            this.chkItem13.Tag = "4";
            this.chkItem13.Text = "13、多种血管活性药物：使用1种以上的血管活性药物，不论种类和剂量";
            // 
            // chkItem12
            // 
            this.chkItem12.Location = new System.Drawing.Point(8, 12);
            this.chkItem12.Name = "chkItem12";
            this.chkItem12.Size = new System.Drawing.Size(752, 21);
            this.chkItem12.TabIndex = 160;
            this.chkItem12.Tag = "3";
            this.chkItem12.Text = "12、单一血管活性药物：使用任何血管活性药物";
            // 
            // chkItem15
            // 
            this.chkItem15.Location = new System.Drawing.Point(8, 108);
            this.chkItem15.Name = "chkItem15";
            this.chkItem15.Size = new System.Drawing.Size(752, 21);
            this.chkItem15.TabIndex = 190;
            this.chkItem15.Tag = "5";
            this.chkItem15.Text = "15、放置外周动脉导管";
            // 
            // chkItem16
            // 
            this.chkItem16.Location = new System.Drawing.Point(8, 140);
            this.chkItem16.Name = "chkItem16";
            this.chkItem16.Size = new System.Drawing.Size(752, 21);
            this.chkItem16.TabIndex = 200;
            this.chkItem16.Tag = "8";
            this.chkItem16.Text = "16、左心房监测：放置肺动脉漂浮导管，不论是否测量心排出量";
            // 
            // chkItem19
            // 
            this.chkItem19.Location = new System.Drawing.Point(8, 12);
            this.chkItem19.Name = "chkItem19";
            this.chkItem19.Size = new System.Drawing.Size(523, 22);
            this.chkItem19.TabIndex = 230;
            this.chkItem19.Tag = "3";
            this.chkItem19.Text = "19、血液过滤、血液透析";
            // 
            // chkItem20
            // 
            this.chkItem20.Location = new System.Drawing.Point(8, 44);
            this.chkItem20.Name = "chkItem20";
            this.chkItem20.Size = new System.Drawing.Size(523, 22);
            this.chkItem20.TabIndex = 240;
            this.chkItem20.Tag = "2";
            this.chkItem20.Text = "20、定量测定尿量（例如，经导尿管测量）";
            // 
            // chkItem18
            // 
            this.chkItem18.Location = new System.Drawing.Point(8, 204);
            this.chkItem18.Name = "chkItem18";
            this.chkItem18.Size = new System.Drawing.Size(752, 21);
            this.chkItem18.TabIndex = 220;
            this.chkItem18.Tag = "3";
            this.chkItem18.Text = "18、在过去24小时进行过心跳骤停后心肺复苏（单次心前区叩击除外）";
            // 
            // chkItem17
            // 
            this.chkItem17.Location = new System.Drawing.Point(8, 172);
            this.chkItem17.Name = "chkItem17";
            this.chkItem17.Size = new System.Drawing.Size(752, 21);
            this.chkItem17.TabIndex = 210;
            this.chkItem17.Tag = "2";
            this.chkItem17.Text = "17、中心静脉置管";
            // 
            // chkItem22
            // 
            this.chkItem22.Location = new System.Drawing.Point(12, 12);
            this.chkItem22.Name = "chkItem22";
            this.chkItem22.Size = new System.Drawing.Size(211, 24);
            this.chkItem22.TabIndex = 260;
            this.chkItem22.Tag = "4";
            this.chkItem22.Text = "22、颅内压测量";
            // 
            // chkItem23
            // 
            this.chkItem23.Location = new System.Drawing.Point(8, 13);
            this.chkItem23.Name = "chkItem23";
            this.chkItem23.Size = new System.Drawing.Size(528, 24);
            this.chkItem23.TabIndex = 270;
            this.chkItem23.Tag = "3";
            this.chkItem23.Text = "23、复杂性代谢性中毒或碱中毒的治疗";
            // 
            // chkItem21
            // 
            this.chkItem21.Location = new System.Drawing.Point(8, 76);
            this.chkItem21.Name = "chkItem21";
            this.chkItem21.Size = new System.Drawing.Size(523, 22);
            this.chkItem21.TabIndex = 250;
            this.chkItem21.Tag = "3";
            this.chkItem21.Text = "21、积极利尿（例如，呋塞米>0.5mg/(kg * d)治疗体液超负荷）";
            // 
            // chkItem24
            // 
            this.chkItem24.Location = new System.Drawing.Point(8, 44);
            this.chkItem24.Name = "chkItem24";
            this.chkItem24.Size = new System.Drawing.Size(528, 23);
            this.chkItem24.TabIndex = 280;
            this.chkItem24.Tag = "4";
            this.chkItem24.Text = "24、静脉营养支持";
            // 
            // chkItem25
            // 
            this.chkItem25.Location = new System.Drawing.Point(8, 74);
            this.chkItem25.Name = "chkItem25";
            this.chkItem25.Size = new System.Drawing.Size(528, 24);
            this.chkItem25.TabIndex = 290;
            this.chkItem25.Tag = "2";
            this.chkItem25.Text = "25、胃肠内营养：经胃管或其他胃肠道途径（例如，空肠造瘘）";
            // 
            // chkItem26
            // 
            this.chkItem26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkItem26.Location = new System.Drawing.Point(12, 16);
            this.chkItem26.Name = "chkItem26";
            this.chkItem26.Size = new System.Drawing.Size(715, 68);
            this.chkItem26.TabIndex = 300;
            this.chkItem26.Tag = "3";
            this.chkItem26.Text = "26、ICU内单一特殊干预措施：经鼻或经口气管插管、放置起博器、心律转复、内镜检查、过去24h内急诊手术、洗胃。对患者临床情况不产生直接影响的常规干预措施，入X线" +
                "检查、超声检查、心电图检查、更换敷料、放置静脉或动脉导管等不包括在内";
            // 
            // chkItem27
            // 
            this.chkItem27.Location = new System.Drawing.Point(12, 92);
            this.chkItem27.Name = "chkItem27";
            this.chkItem27.Size = new System.Drawing.Size(715, 27);
            this.chkItem27.TabIndex = 310;
            this.chkItem27.Tag = "5";
            this.chkItem27.Text = "27、ICU内多种特殊干预措施：上述项目种1种以上的干预措施";
            // 
            // chkItem28
            // 
            this.chkItem28.Location = new System.Drawing.Point(12, 140);
            this.chkItem28.Name = "chkItem28";
            this.chkItem28.Size = new System.Drawing.Size(715, 26);
            this.chkItem28.TabIndex = 320;
            this.chkItem28.Tag = "5";
            this.chkItem28.Text = "28、ICU外的特殊干预措施：手术或急诊性操作";
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.AutoSize = true;
            this.lblResultTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResultTitle.ForeColor = System.Drawing.Color.Black;
            this.lblResultTitle.Location = new System.Drawing.Point(663, 412);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(75, 14);
            this.lblResultTitle.TabIndex = 344;
            this.lblResultTitle.Text = "评分结果:";
            this.lblResultTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.tabPage1);
            this.tabPage.Controls.Add(this.tabPage2);
            this.tabPage.Controls.Add(this.tabPage3);
            this.tabPage.Controls.Add(this.tabPage4);
            this.tabPage.Controls.Add(this.tabPage5);
            this.tabPage.Controls.Add(this.tabPage6);
            this.tabPage.Controls.Add(this.tabPage7);
            this.tabPage.Location = new System.Drawing.Point(4, 104);
            this.tabPage.Name = "tabPage";
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(844, 276);
            this.tabPage.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.chkItem6);
            this.tabPage1.Controls.Add(this.chkItem5);
            this.tabPage1.Controls.Add(this.chkItem4);
            this.tabPage1.Controls.Add(this.chkItem3);
            this.tabPage1.Controls.Add(this.chkItem2);
            this.tabPage1.Controls.Add(this.chkItem1);
            this.tabPage1.Controls.Add(this.chkItem7);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(836, 249);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础项目";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.chkItem8);
            this.tabPage2.Controls.Add(this.chkItem9);
            this.tabPage2.Controls.Add(this.chkItem10);
            this.tabPage2.Controls.Add(this.chkItem11);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(836, 249);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通气支持";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.lblTitle10);
            this.tabPage3.Controls.Add(this.chkItem12);
            this.tabPage3.Controls.Add(this.chkItem13);
            this.tabPage3.Controls.Add(this.chkItem14);
            this.tabPage3.Controls.Add(this.chkItem15);
            this.tabPage3.Controls.Add(this.chkItem16);
            this.tabPage3.Controls.Add(this.chkItem17);
            this.tabPage3.Controls.Add(this.chkItem18);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(836, 249);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "心血管支持";
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle10.Location = new System.Drawing.Point(332, 68);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(11, 10);
            this.lblTitle10.TabIndex = 346;
            this.lblTitle10.Text = "2";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.chkItem19);
            this.tabPage4.Controls.Add(this.chkItem20);
            this.tabPage4.Controls.Add(this.chkItem21);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(836, 249);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "肾脏支持";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.chkItem22);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(836, 249);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "神经系统支持";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.chkItem23);
            this.tabPage6.Controls.Add(this.chkItem24);
            this.tabPage6.Controls.Add(this.chkItem25);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(836, 249);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "代谢支持";
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.chkItem26);
            this.tabPage7.Controls.Add(this.chkItem27);
            this.tabPage7.Controls.Add(this.chkItem28);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(836, 249);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "特殊干预措施";
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.SystemColors.Control;
            this.lblResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.Black;
            this.lblResult.Location = new System.Drawing.Point(744, 412);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(53, 18);
            this.lblResult.TabIndex = 324;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(45, 440);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 346;
            this.lblEvalDate.Text = "评分时间：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpEvalDate
            // 
            this.dtpEvalDate.BorderColor = System.Drawing.Color.Black;
            this.dtpEvalDate.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpEvalDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpEvalDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpEvalDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpEvalDate.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEvalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEvalDate.Location = new System.Drawing.Point(137, 410);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(219, 22);
            this.dtpEvalDate.TabIndex = 330;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // txtEvalDoctor
            // 
            this.txtEvalDoctor.AccessibleDescription = "评估者";
            this.txtEvalDoctor.BackColor = System.Drawing.Color.White;
            this.txtEvalDoctor.BorderColor = System.Drawing.Color.White;
            this.txtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.Location = new System.Drawing.Point(496, 412);
            this.txtEvalDoctor.Name = "txtEvalDoctor";
            this.txtEvalDoctor.Size = new System.Drawing.Size(108, 23);
            this.txtEvalDoctor.TabIndex = 340;
            // 
            // lblMonth2
            // 
            this.lblMonth2.BackColor = System.Drawing.SystemColors.Control;
            this.lblMonth2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMonth2.ForeColor = System.Drawing.Color.Black;
            this.lblMonth2.Location = new System.Drawing.Point(754, 132);
            this.lblMonth2.Name = "lblMonth2";
            this.lblMonth2.Size = new System.Drawing.Size(24, 19);
            this.lblMonth2.TabIndex = 10000010;
            this.lblMonth2.Text = "月";
            this.lblMonth2.Visible = false;
            // 
            // lblDay2
            // 
            this.lblDay2.BackColor = System.Drawing.SystemColors.Control;
            this.lblDay2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDay2.ForeColor = System.Drawing.Color.Black;
            this.lblDay2.Location = new System.Drawing.Point(717, 147);
            this.lblDay2.Name = "lblDay2";
            this.lblDay2.Size = new System.Drawing.Size(24, 19);
            this.lblDay2.TabIndex = 10000010;
            this.lblDay2.Text = "日";
            this.lblDay2.Visible = false;
            // 
            // lblDay
            // 
            this.lblDay.BackColor = System.Drawing.SystemColors.Control;
            this.lblDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDay.ForeColor = System.Drawing.Color.Black;
            this.lblDay.Location = new System.Drawing.Point(777, 132);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(24, 19);
            this.lblDay.TabIndex = 10000010;
            // 
            // lblMonth
            // 
            this.lblMonth.BackColor = System.Drawing.SystemColors.Control;
            this.lblMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMonth.ForeColor = System.Drawing.Color.Black;
            this.lblMonth.Location = new System.Drawing.Point(731, 132);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(24, 19);
            this.lblMonth.TabIndex = 10000010;
            // 
            // m_cmdEvalDoctor
            // 
            this.m_cmdEvalDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEvalDoctor.DefaultScheme = true;
            this.m_cmdEvalDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_cmdEvalDoctor.Hint = "";
            this.m_cmdEvalDoctor.Location = new System.Drawing.Point(384, 405);
            this.m_cmdEvalDoctor.Name = "m_cmdEvalDoctor";
            this.m_cmdEvalDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEvalDoctor.Size = new System.Drawing.Size(91, 32);
            this.m_cmdEvalDoctor.TabIndex = 10000011;
            this.m_cmdEvalDoctor.Text = "评估者：";
            // 
            // frmTISSValuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(874, 673);
            this.Controls.Add(this.tabPage);
            this.Controls.Add(this.m_cmdEvalDoctor);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.lblResultTitle);
            this.Controls.Add(this.txtEvalDoctor);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblMonth2);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblDay2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTISSValuation";
            this.Text = "TISS-28评分";
            this.Load += new System.EventHandler(this.frmTISSValuation_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.lblDay2, 0);
            this.Controls.SetChildIndex(this.lblDay, 0);
            this.Controls.SetChildIndex(this.lblMonth2, 0);
            this.Controls.SetChildIndex(this.lblMonth, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.Controls.SetChildIndex(this.txtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblResultTitle, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.m_cmdEvalDoctor, 0);
            this.Controls.SetChildIndex(this.tabPage, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
