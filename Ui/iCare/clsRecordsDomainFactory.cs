//using System;
//using weCare.Core.Entity;

//namespace iCare
//{
//    /// <summary>
//    /// ���ɲ��������¼�����������ࡣʹ�ø��࣬�Ѿ������������ɺͽ�����롣
//    /// </summary>
//    public abstract class clsRecordsDomainFactory
//    {

//        /// <summary>
//        /// ��ȡָ�������¼������㡣
//        /// </summary>
//        /// <param name="p_enmRecordsType"></param>
//        /// <returns></returns>
//        public static clsRecordsService s_objGetRecordsDomain(enmRecordsType p_enmRecordsType)
//        {
//            switch (p_enmRecordsType)
//            {
//                case enmRecordsType.DiseaseTrack:
//                    {
//                        clsMainDiseaseTrackService objServ =
//                            (clsMainDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMainDiseaseTrackService));
//                        return objServ;
//                    }
//                //return new clsMainDiseaseTrackService();	

//                case enmRecordsType.WatchItem:
//                    {
//                        com.digitalwave.clsWatchItemTrackService.clsWatchItemTrackService objServ =
//                            (com.digitalwave.clsWatchItemTrackService.clsWatchItemTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsWatchItemTrackService.clsWatchItemTrackService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsWatchItemTrackService.clsWatchItemTrackService();

//                case enmRecordsType.IntensiveTend:
//                    {
//                        com.digitalwave.clsRecordsService.clsIntensiveTendMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsIntensiveTendMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsIntensiveTendMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsIntensiveTendMainService();

//                case enmRecordsType.GeneralNurseRecord:
//                    {
//                        com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService objServ =
//                            (com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService();

//                case enmRecordsType.ICUIntensiveTend:
//                    {
//                        com.digitalwave.clsICUIntensiveTrackService.clsICUIntensiveTrackService objServ =
//                            (com.digitalwave.clsICUIntensiveTrackService.clsICUIntensiveTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsICUIntensiveTrackService.clsICUIntensiveTrackService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsICUIntensiveTrackService.clsICUIntensiveTrackService();

//                case enmRecordsType.ICUBreath:
//                    {
//                        com.digitalwave.clsICUBreathTrackService.clsICUBreathTrackService objServ =
//                            (com.digitalwave.clsICUBreathTrackService.clsICUBreathTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsICUBreathTrackService.clsICUBreathTrackService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsICUBreathTrackService.clsICUBreathTrackService();

//                case enmRecordsType.FirstIllnessNote:
//                    {
//                        clsMainDiseaseTrackService objServ =
//                            (clsMainDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMainDiseaseTrackService));
//                        return objServ;
//                    }
//                //return new clsMainDiseaseTrackService();

//                case enmRecordsType.FirstIllnessNote_ZY:
//                    {
//                        clsMainDiseaseTrackService objServ =
//                            (clsMainDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMainDiseaseTrackService));
//                        return objServ;
//                    }
//                //return new clsMainDiseaseTrackService();

//                case enmRecordsType.GeneralNurseRecord_GXRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsGeneralNurseRecord_GXMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsGeneralNurseRecord_GXMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsGeneralNurseRecord_GXMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsGeneralNurseRecord_GXMainService();

//                case enmRecordsType.ICUNurseRecord_GX:
//                    {
//                        com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService();

//                case enmRecordsType.SURGERYICUWARDSHIP:
//                    {
//                        com.digitalwave.clsRecordsService.clsSURGERYICUWARDSHIPMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsSURGERYICUWARDSHIPMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsSURGERYICUWARDSHIPMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsSURGERYICUWARDSHIPMainService();

//                case enmRecordsType.IntensiveTendRecord_GX:
//                    {
//                        com.digitalwave.clsRecordsService.clsIntensiveTendRecord_GXMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsIntensiveTendRecord_GXMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsIntensiveTendRecord_GXMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsIntensiveTendRecord_GXMainService();

//                case enmRecordsType.CardiovascularTend_GX:
//                    {
//                        com.digitalwave.clsRecordsService.clsCardiovascularTend_GXMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsCardiovascularTend_GXMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsCardiovascularTend_GXMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsCardiovascularTend_GXMainService();

//                //�����¼����(��Ҫ�Ķ�)
//                case enmRecordsType.WaitLayRecord_Acad:
//                    {
//                        com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadMainService();

//                //�����¼����
//                case enmRecordsType.PostPartum_Acad:
//                    {
//                        com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService objServ =
//                            (com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService();

//                case enmRecordsType.HurryVeinRecord:
//                    {
//                        com.digitalwave.clsRecordsService.clsHurryVeinRecord_MainService objServ =
//                            (com.digitalwave.clsRecordsService.clsHurryVeinRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsHurryVeinRecord_MainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsHurryVeinRecord_MainService();

//                //̥���໤��
//                case enmRecordsType.QuickeningTutelar_Acad:
//                    {
//                        com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadMainService();

//                //��������������۲��¼
//                case enmRecordsType.PostartumSeeRecord:
//                    {
//                        com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService();

//                //�������⻯����ҩ�۲��¼��(����)
//                case enmRecordsType.VeinSpecialUseDrug:
//                    {
//                        com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService objServ =
//                            (com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService));
//                        return objServ;
//                    }

//                //return new com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService();

//                //���̼�¼
//                case enmRecordsType.EMR_WAITLAYRECORD_GX:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_WAITLAYRECORD_GXMainServ objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_WAITLAYRECORD_GXMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_WAITLAYRECORD_GXMainServ));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsEMR_WAITLAYRECORD_GXMainServ();

//                //�߲��ؾ�����ι۲��
//                case enmRecordsType.EMR_OXTIntravenousDrip:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_OXTIntravenousDripMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_OXTIntravenousDripMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_OXTIntravenousDripMainService));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsEMR_OXTIntravenousDripMainService();

//                //�������ǼǱ�
//                case enmRecordsType.EMR_IntakeAndOutputVolume:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_IntakeAndOutputVolumeMainServ objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_IntakeAndOutputVolumeMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_IntakeAndOutputVolumeMainServ));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsEMR_IntakeAndOutputVolumeMainServ();

//                case enmRecordsType.EMR_MicroBooldSugarCheck:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_MicroBooldSugarCheckMainServ objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_MicroBooldSugarCheckMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_MicroBooldSugarCheckMainServ));
//                        return objServ;
//                    }
//                //return new com.digitalwave.clsRecordsService.clsEMR_MicroBooldSugarCheckMainServ();

//                case enmRecordsType.FirstIllnessNote_F2:
//                    {
//                        clsMainDiseaseTrackService objServ =
//                            (clsMainDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMainDiseaseTrackService));
//                        return objServ;
//                    }
//                //Σ�ػ��߻����¼��ɽ�棨�ڿơ������ơ��ն��ơ�����ơ������ң�
//                case enmRecordsType.GeneralNurseRecord_CSRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSMainService));
//                        return objServ;
//                    }
//                //��������һ�㻼�߻����¼����ɽ��
//                case enmRecordsType.NewBorthBabyGeneralNurseRecord_CSRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsNewBrothGeneralNurseRecord_CSMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsNewBrothGeneralNurseRecord_CSMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsNewBrothGeneralNurseRecord_CSMainService));
//                        return objServ;
//                    }
//                //һ�㻼�߻����¼����ɽ����ƣ�
//                case enmRecordsType.GeneralNurseRecordWK_CSRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsGeneralNurseRecordWK_CSMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsGeneralNurseRecordWK_CSMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsGeneralNurseRecordWK_CSMainService));
//                        return objServ;
//                    }
//                //��������Σ�ػ��߻����¼����ɽ��
//                case enmRecordsType.IntensivetendRecord_CSRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsIntensivetendRecord_CSMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsIntensivetendRecord_CSMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsIntensivetendRecord_CSMainService));
//                        return objServ;
//                    }
//                //Σ�ػ��߻����¼����ɽ����ƣ�
//                case enmRecordsType.IntensiveTendMain_CSWKRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsIntensiveTendMain_CSWKMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsIntensiveTendMain_CSWKMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsIntensiveTendMain_CSWKMainService));
//                        return objServ;
//                    }
//                //һ�㻼�߻����¼��ɽ�棨�ڿơ������ơ��ն��ƣ�
//                case enmRecordsType.GeneralNurseRecord_DGCSRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsGeneralNurseRecord_DGCSMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsGeneralNurseRecord_DGCSMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsGeneralNurseRecord_DGCSMainService));
//                        return objServ;
//                    }
//                //һ�㻼�߻����¼��ɽ�棨������������
//                case enmRecordsType.GeneralNurseRecord_ObstetricNewChildRec:
//                    {
//                        com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSObstetricNewChildMainService objServ =
//                            (com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSObstetricNewChildMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsGeneralNurseRecord_CSObstetricNewChildMainService));
//                        return objServ;
//                    }
//                //�ڷ��ڿ�Ѫ�ǹ۲��
//                case enmRecordsType.EMR_IntBloodSugarWatch:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_intbloodsugarwatchMainServ objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_intbloodsugarwatchMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_intbloodsugarwatchMainServ));
//                        return objServ;
//                    }
//                //���ڷ�ʪ��ѪҺ������¼��
//                case enmRecordsType.frmBloodCleanseRecord:
//                    {
//                        com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService objServ =
//                            (com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService));
//                        return objServ;
//                    }
//                case enmRecordsType.EMR_GestationDiabetesCure:
//                    {
//                        com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ objServ =
//                            (com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ));
//                        return objServ;
//                    }
//                    //return new clsMainDiseaseTrackService();

//            }

//            return null;
//        }

//    }// END CLASS DEFINITION clsRecordsDomainFactory

//}
