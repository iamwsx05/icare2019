

using System;

namespace iCare
{
    /// <summary>
    /// ר�Ʋ�����ӡ�����๤��
    /// </summary>
    public class clsInpatMedRecPrintToolFactory
    {
        public clsInpatMedRecPrintToolFactory()
        {
        }


        /// <summary>
        /// ������ӡ����
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <returns></returns> 
        public static clsInpatMedRecPrintBase s_objGeneratePrintTool(string p_strTypeID)
        {
            switch (p_strTypeID)
            {
                case "frmIMR_ChestSurgery_I":
                case "frmIMR_ChestSurgery_II":
                case "frmIMR_ChestSurgery_III":
                    return new clsIMR_ChestSurgeryPrintTool(p_strTypeID);
                case "frmIMR_Cardiovascular":
                    return new clsIMR_CardiovascularPrintTool(p_strTypeID);
                case "frmIMR_Gynecology":
                    return new clsIMR_GynecologyPrintTool(p_strTypeID);
                case "frmIMR_Obstetric":
                    return new clsIMR_ObstetricPrintTool(p_strTypeID);
                case "frmIMR_Ophthalmology":
                    return new clsIMR_OphthalmologyPrintTool(p_strTypeID);
                case "frmIMR_Neurosurgery":
                    return new clsIMR_NeurosurgeryPrintTool(p_strTypeID);
                case "frmIMR_Herbalism":
                    return new clsIMR_HerbalismPrintTool(p_strTypeID);
                case "frmIMR_Herbalist_Western":
                    return new clsIMR_Herbalist_WestPrintTool(p_strTypeID);
                case "frmIMR_Paediatrics":
                    return new clsIMR_PaediatricsPrintTool(p_strTypeID);
                case "frmIMR_Neonatal":
                    return new clsIMR_NeonatalPrintTool(p_strTypeID);
                case "frmIMR_BurnSuergery":
                    return new clsIMR_BurnSuergeryPrintTool(p_strTypeID);
                case "frmIMR_StoIntesChirurgeryl":
                    return new clsIMR_StoIntesChirurgerylPrintTool(p_strTypeID);
                case "frmIMR_Neuromedicine":
                    return new clsIMR_NeuromedicinePrintTool(p_strTypeID);
                case "frmIMR_GestationMisbirth_1": 
                    return new clsIMR_GestationMisbirth_1PrintTool(p_strTypeID);
                case "frmIMR_GestationMisbirth_2":
                    return new clsIMR_GestationMisbirth_2PrintTool(p_strTypeID);
                case "frmIMR_GestationMisbirth_3":
                    return new clsIMR_GestationMisbirth_3PrintTool(p_strTypeID);
                case "frmIMR_Breath":
                    return new clsIMR_BreathPrintTool(p_strTypeID);
                case "frmIMR_NerveSystem"://����������
                    return new clsIMR_NerveSystemPrintTool(p_strTypeID);
                case "frmIMR_NerveMedicineCerebrovascular"://���ڿ���Ѫ�ܲ�����
                    return new clsIMR_NerveMedicineCerebrovascularPrintTool(p_strTypeID);
                case "frmIMR_Cerebrovascular"://��Ѫ�ܲ�����
                    return new clsIMR_CerebrovascularPrintTool(p_strTypeID);
                case "frmIMR_HeadInjure"://­�����˲���
                    return new clsIMR_HeadInjurePrintTool(p_strTypeID);
                case "frmIMR_NerveMedicineRecord"://���ڿ�סԺ����
                    return new clsIMR_NerveMedicineRecordPrintTool(p_strTypeID);
                case "frmIMR_Neuromedicine_F2"://���ڿ�סԺ����---�½�
                    return new clsIMR_Neuromedicine_F2PrintTool(p_strTypeID);
                case "frmIMR_NutrientRecord":
                    return new clsIMR_NutrientRecordPrintTool(p_strTypeID); 

                //weiling.huang
                case "frmChildbearingRecord":               //����������ʱ��¼
                    return new clsChildbearingRecordPrintTool(p_strTypeID);
                case "frmIMR_childbirth":                     //�����¼
                    return new clsIMR_childbirthPrintTool(p_strTypeID);
                case "frmBloodAcadInHospitalCaseHistoryOne":  //ѪҺר��סԺ����
                    return new clsBloodAcadInHospitalCaseHistoryOnePrintTool(p_strTypeID);
                case "frmIMR_PalaceBirthControlLaySkill":  //���ڽ�������������¼��
                    return new clsIMR_PalaceBirthControlLaySkillPrintTool(p_strTypeID);
                case "frmManpowerAbortion":               //�˹��������������ѹܼ�¼��
                    return new clsManpowerAbortionPrintTool(p_strTypeID);
                case "frmKidneyMedicineBeInHospital":     //���ڿ�סԺ����
                    return new clsKidneyMedicineBeInHospitalPrintTool(p_strTypeID);
                case "frmIMR_RheumatismImmunity":        //��ʪ�����ڿ�סԺ����
                    return new clsIMR_RheumatismImmunityPrintTool(p_strTypeID);
                case "frmIMR_ChildbirthTransitSection":  // ������ת�Ƽ�¼
                    return new clsIMR_ChildbirthTransitSectionPrintTool(p_strTypeID);
                case "frmIMR_DiabetesHospital":  // ����סԺ����
                    return new clsIMR_DiabetesHospitalPrintTool(p_strTypeID);

                //xigui.peng
                case "frmIMR_CesareanRecord":  // �ʹ�����¼
                    return new clsIMR_CesareanRecordPrintTool(p_strTypeID);
                case "frmIMR_ForpecsRecord":  // ��ǯ������¼
                    return new clsIMR_ForpecsRecordPrintTool(p_strTypeID);
                case "frmIMR_MedcineMiscarryRecord":  //ҩ��������¼��
                    return new clsIMR_MedcineMiscarryRecordPrintTool(p_strTypeID);
                case "frmIMR_WombBirthControlRecord": //���ڽ�����ȡ������¼��
                    return new clsIMR_WombBirthControlRecordPrintTool(p_strTypeID);
                case "frmIMR_HeartHospitalRecord":
                    return new clsIMR_HeartHospitalRecordPrintTool(p_strTypeID);

                case "frmIMR_Obstetric_Criterion":
                    return new clsIMR_Obstetric_CriterionPrintTool(p_strTypeID);
                //����������Ժ��¼��ӡ
                case "frmIMR_NewChild":
                    return new clsIMR_NewChildPrintTool(p_strTypeID);
                //������Ժ��¼��ӡ
                case "frmIMR_Paediatrics01":
                    return new clsIMR_PaediatricsPrintTool01(p_strTypeID);
                case "frmGynecologyCaseHis":
                    return new clsGynecologyCaseHisPrintTool(p_strTypeID);
                case "frmObstetricCaseHis":
                    return new clsObstetricCaseHisPrintTool(p_strTypeID);
                case "frmIMR_OrthopaedicsSuffererAprrove"://�ǿ�(��������΢�������)����֪��ͬ����
                    return new clsIMR_OrthopaedicsSuffererAprrovePrintTool(p_strTypeID);
                case "frmIMR_CataractSuffererApprove"://��������������֪��ͬ����
                    return new clsIMR_CataractSuffererApprovePrintTool(p_strTypeID);
                case "frm_MODS_ApparatusObserve":  //  MODS����ϵͳ�����ٴ��о��۲�ǼǱ�
                    return new cls_MODS_ApparatusObservePrintTool(p_strTypeID);
                case "frmIMR_Neonatal_GX":  // ������סԺ����
                    return new clsIMR_Neonatal_GXPrintTool(p_strTypeID);
                //���ﲡ��
                case "frm_EmergencyCall":
                    return new cls_EmergencyCallPrintTool(p_strTypeID);
                case "frmIMR_IllnessHistoryRecord":  // ���Ǻ��ר�Ʋ���
                    return new clsIMR_IllnessHistoryRecordPrintTool(p_strTypeID);
                case "frmChildHospitalHistory":  // ����ר�Ʋ���
                    return new clsChildHospitalHistoryPrintTool(p_strTypeID);
                case "frmIMR_InHospitalMarks":
                    return new clsIMR_InHospitalMarksPrintTool(p_strTypeID);
                case "frmIMR_PrePostOperateSee": //��ǰ������ӵ�
                    return new clsIMR_PrePostOperateSeePrintTool(p_strTypeID);
                case "frmIMR_EmergencyCallWound":
                    return new clsIMR_EmergenceWoundPrintTool(p_strTypeID);
                case "frmIMR_NasopharyngelCarcinoma":  // ���ʰ�����
                    return new clsIMR_NasopharyngelCarcinomaPrintTool(p_strTypeID);
                case "frmIMR_CardiovascularDPS"://����Ѫ��������¼
                    return new clsIMR_CardiovascularDPSPrintTool(p_strTypeID);
                case "frmIMR_RetinaDeviate"://����Ĥ����������¼
                    return new clsIMR_RetinaDeviatePrintTool(p_strTypeID);
                case "frmIMR_AskanceDPS"://б��������¼
                    return new clsIMR_AskanceDPSPrintTool(p_strTypeID);
                case "frmIMR_GlassAblate"://�������г�������¼
                    return new clsIMR_GlassAblatePrintTool(p_strTypeID);
                case "frmIMR_GlaucomaEye"://�����������¼
                    return new clsIMR_GlaucomaEyePrintTool(p_strTypeID);
                case "frmIMR_EmbeddedOPS"://�˹�����ֲ��������¼
                    return new clsIMR_EmbeddedOPSPrintTool(p_strTypeID);
                case "frmIMR_Ab_abbortion": //����סԺ����  
                    return new clsIMR_AB_abbortionPrintTool(p_strTypeID);
                case "frmIMR_Derivation"://��������סԺ����
                    return new clsIMR_DerivationPrintTool(p_strTypeID);
                case "frmIMR_InternalMedicineZY"://�ڿ�סԺ��¼
                    return new clsIMR_InternalMedicineZYPrintTool(p_strTypeID);
                case "frmIMR_DeliverRecord"://�����¼����ݸ��
                    return new clsIMR_DeliverRecordPrintTool(p_strTypeID);
                case "frmIMR_Derivation_DG"://����������Ժ��¼����ݸ��
                    return new clsIMR_Derivation_DGPrintTool(p_strTypeID);
                case "frmIMR_ObstetricOutRecord_DG": //���Ƴ�Ժ��¼����ݸ��
                    return new clsIMR_ObstetricOutRecord_DGPrintTool(p_strTypeID);
                case "frmObstetricOutRecord": //���Ƴ�Ժ��¼
                    return new clsIMR_CKOutHospitalRecPrintTool(p_strTypeID);
                case "frmIMR_GynaecologyInHospitalRecord": //������Ժ��¼
                    return new clsIMR_GynaecologyInHospitalRecordPrintTool(p_strTypeID);
                case "frmObstetricOutHospital":  //���Ƴ�ԺС��
                    return new clsIMR_OutHosptialResultPrintTool(p_strTypeID);
                case "frmIMR_ObstetricOutHosptial_LJ": //�׽̲��Ƴ�Ժ��¼
                    return new clsIMR_ObstetricOutHosptialPrintTool_LJ(p_strTypeID);
                case "frmShuLuanGuanJieZaShouShu_LJ"://�׽����ѹܽ���������¼��
                    return new clsIMR_ShuLuanGuanJieZaPrintTool(p_strTypeID);
                case "frmIMR_GynecologyF2"://����סԺ������ɽ��
                    return new clsIMR_GynecologyF2PrintTool(p_strTypeID);
                case "frmIMR_Obstetric_F2"://����סԺ������ɽ��
                    return new clsIMR_Obstetric_F2PrintTool(p_strTypeID);
                case "frmChildbearingRecord_F2"://����������ʱ��¼��ɽ��
                    return new clsChildbearingRecord_F2PrintTool(p_strTypeID);
                case "frmInducedLaborRecord":
                    return new clsIMR_InducedLaborRecordPrintTool(p_strTypeID);
                case "frmIMR_ManpowerAbortionRecord"://����������ȡ�����¾��󸹲����ѹܽ�����¼
                        return new clsIMR_ManpowerAbortionRecordPrintTool(p_strTypeID);
                    case "frmEMR_PullDeliverRecord_F2"://����̥ͷ����������������¼
                        return new clsEMR_PullDeliverRecord_F2PrintTool(p_strTypeID);
                    case "frmEMR_PullDeliverRecord_CS":// ����̥ͷ����������������¼����ɽ��
                        return new clsIMR_PullDeliverRecord_CS(p_strTypeID);
                case "frmEyeCataract"://������
                    return new clsEyeCataractPrintTool(p_strTypeID);
                case "frmIMR_SkinRecord": //Ƥ���Ʋ���
                    return new clsSkinRcreadPrintTool(p_strTypeID);
                case "frmIMR_Ophthalmology_F2": // �ۿ�סԺ�����������
                    return new clsIMR_OphthalmologyPrintTool_F2(p_strTypeID);
               // case "frmIMR_StubbornDisease": //���Ѳ���
               //    return new clsIMR_StubbornDiseasePrint(p_strTypeID);
               //case "frmIMR_OutHospitall": // ��Ժ�������½���
               //     return new clsIMG_OutHospitallPrint(p_strTypeID);
               // case "frmIMR_BonehurtHurt": // ���˿ƣ������½���
               //     return new clsIMR_BonehurtHurtPrint(p_strTypeID);
               // case "frmIMG_Bonehurtcervical": // ���˿ƣ���׵���½���
               //     return new clsIMR_Bonehurtcervicalprint(p_strTypeID);
               // case "frmIMR_Bonearthropathy": // ���˿ƣ��ؽڲ��½���
               //     return new clsIMR_BonearthropathyPrint(p_strTypeID);
               // case "frmIMR_Bonelumbocruralpain": // ���˿ƣ�����ʹ�½���
               //     return new clsIMR_Bonelumbocruralpain(p_strTypeID);
               // case "frmIMG_familyplanningRecord": // �ƻ�����סԺ��¼���½��棩
               //     return new clsIMR_familyplanningPrint(p_strTypeID);
               // case "frmIMG_familyplaningoper": // �ƻ�����������¼���½��棩
               //     return new clsIMR_familyplaningoperPrint(p_strTypeID);
               // case "frmIMG_Tongwater": // ͨˮ������¼���½��棩
               //     return new clsIMR_TongwaterPrint(p_strTypeID);
               // case "frmIMR_peopleLostlay": // �˹�����������¼���½��棩
               //     return new clsIMR_peopleLostlayPrint(p_strTypeID);
               // case "frmIMR_middlelay": // ��������������¼���½��棩
               //     return new clsIMR_middlelayPrint(p_strTypeID);
               // case "frmIMR_inthospitalpinggu": // ��Ժ�������½��棩
               //     return new clsIMR_inthospitalpingguPrint(p_strTypeID);
               // case "frmIMR_intHosptalrecord": // ��Ժ��¼--->�ĳ�����飨�½��棩
               //     return new clsIMR_intHosptalrecordPrint(p_strTypeID);
                case "frmIMR_Neurosurgery_F2": // �������Ժ��¼
                    return new clsIMR_Neurosurgery_F2PrintTool(p_strTypeID);
                case "frmIMR_IllnessInHospitalRecord"://���Ǻ��סԺ����
                    return new clsIMR_IllnessInHospitalRecordPrintTool(p_strTypeID);
                case "frmIMR_HeartVasSurgeryRecord"://��Ѫ�������Ժ��¼
                    return new clsIMR_HeartVasSurgeryRecordPrintTool(p_strTypeID);
                case "frmIMR_EyeTakecare"://�ۿƻ����¼�������
                    return new clsIMR__EyeTakecarePrint(p_strTypeID);

                default:
                    return new clsInpatMedRecPrintToolFactory2().m_objSubGeneratePrintTool(p_strTypeID);
            }
             
            return null;
        }


    }
}
