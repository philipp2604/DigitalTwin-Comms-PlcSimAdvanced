using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;
public static class CpuTypeConverter
{
    public static CpuType ConvertCpuType(ECPUType type)
    {
        return type switch
        {
            ECPUType.CPU1500_Unspecified => CpuType.CPU1500_Unspecified,
            ECPUType.CPU1511 => CpuType.CPU1511,
            ECPUType.CPU1513 => CpuType.CPU1513,
            ECPUType.CPU1515 => CpuType.CPU1515,
            ECPUType.CPU1516 => CpuType.CPU1516,
            ECPUType.CPU1517 => CpuType.CPU1517,
            ECPUType.CPU1518 => CpuType.CPU1518,
            ECPUType.CPU1511C => CpuType.CPU1511C,
            ECPUType.CPU1512C => CpuType.CPU1512C,
            ECPUType.CPU1511F => CpuType.CPU1511F,
            ECPUType.CPU1513F => CpuType.CPU1513F,
            ECPUType.CPU1515F => CpuType.CPU1515F,
            ECPUType.CPU1516F => CpuType.CPU1516F,
            ECPUType.CPU1517F => CpuType.CPU1517F,
            ECPUType.CPU1518F => CpuType.CPU1518F,
            ECPUType.CPU1511T => CpuType.CPU1511T,
            ECPUType.CPU1515T => CpuType.CPU1515T,
            ECPUType.CPU1516T => CpuType.CPU1516T,
            ECPUType.CPU1517T => CpuType.CPU1517T,
            ECPUType.CPU1511TF => CpuType.CPU1511TF,
            ECPUType.CPU1515TF => CpuType.CPU1515TF,
            ECPUType.CPU1516TF => CpuType.CPU1516TF,
            ECPUType.CPU1517TF => CpuType.CPU1517TF,
            ECPUType.CPU1518ODK => CpuType.CPU1518ODK,
            ECPUType.CPU1518FODK => CpuType.CPU1518FODK,
            ECPUType.CPU1518MFP => CpuType.CPU1518MFP,
            ECPUType.CPU1518FMFP => CpuType.CPU1518FMFP,
            ECPUType.ET200SP_Unspecified => CpuType.ET200SP_Unspecified,
            ECPUType.CPU1510SP => CpuType.CPU1510SP,
            ECPUType.CPU1512SP => CpuType.CPU1512SP,
            ECPUType.CPU1510SPF => CpuType.CPU1510SPF,
            ECPUType.CPU1512SPF => CpuType.CPU1512SPF,
            ECPUType.CPU1514SP => CpuType.CPU1514SP,
            ECPUType.CPU1514SPT => CpuType.CPU1514SPT,
            ECPUType.CPU1514SPF => CpuType.CPU1514SPF,
            ECPUType.CPU1514SPTF => CpuType.CPU1514SPTF,
            ECPUType.CPU1500_RH_Unspecified => CpuType.CPU1500_RH_Unspecified,
            ECPUType.CPU1513R => CpuType.CPU1513R,
            ECPUType.CPU1515R => CpuType.CPU1515R,
            ECPUType.CPU1517H => CpuType.CPU1517H,
            ECPUType.CPU1518HF => CpuType.CPU1518HF,
            ECPUType.CPU1504DTF => CpuType.CPU1504DTF,
            ECPUType.CPU1507DTF => CpuType.CPU1507DTF,
            ECPUType.CPU1518T => CpuType.CPU1518T,
            ECPUType.CPU1518TF => CpuType.CPU1518TF,
            ECPUType.ET200PRO_Unspecified => CpuType.ET200PRO_Unspecified,
            ECPUType.CPU1513PRO => CpuType.CPU1513PRO,
            ECPUType.CPU1513PROF => CpuType.CPU1513PROF,
            ECPUType.CPU1516PRO => CpuType.CPU1516PRO,
            ECPUType.CPU1516PROF => CpuType.CPU1516PROF,
            _ => throw new NotImplementedException(),
        };
    }

    public static ECPUType ConvertCpuType(CpuType type)
    {
        return type switch
        {
            CpuType.CPU1500_Unspecified => ECPUType.CPU1500_Unspecified,
            CpuType.CPU1511 => ECPUType.CPU1511,
            CpuType.CPU1513 => ECPUType.CPU1513,
            CpuType.CPU1515 => ECPUType.CPU1515,
            CpuType.CPU1516 => ECPUType.CPU1516,
            CpuType.CPU1517 => ECPUType.CPU1517,
            CpuType.CPU1518 => ECPUType.CPU1518,
            CpuType.CPU1511C => ECPUType.CPU1511C,
            CpuType.CPU1512C => ECPUType.CPU1512C,
            CpuType.CPU1511F => ECPUType.CPU1511F,
            CpuType.CPU1513F => ECPUType.CPU1513F,
            CpuType.CPU1515F => ECPUType.CPU1515F,
            CpuType.CPU1516F => ECPUType.CPU1516F,
            CpuType.CPU1517F => ECPUType.CPU1517F,
            CpuType.CPU1518F => ECPUType.CPU1518F,
            CpuType.CPU1511T => ECPUType.CPU1511T,
            CpuType.CPU1515T => ECPUType.CPU1515T,
            CpuType.CPU1516T => ECPUType.CPU1516T,
            CpuType.CPU1517T => ECPUType.CPU1517T,
            CpuType.CPU1511TF => ECPUType.CPU1511TF,
            CpuType.CPU1515TF => ECPUType.CPU1515TF,
            CpuType.CPU1516TF => ECPUType.CPU1516TF,
            CpuType.CPU1517TF => ECPUType.CPU1517TF,
            CpuType.CPU1518ODK => ECPUType.CPU1518ODK,
            CpuType.CPU1518FODK => ECPUType.CPU1518FODK,
            CpuType.CPU1518MFP => ECPUType.CPU1518MFP,
            CpuType.CPU1518FMFP => ECPUType.CPU1518FMFP,
            CpuType.ET200SP_Unspecified => ECPUType.ET200SP_Unspecified,
            CpuType.CPU1510SP => ECPUType.CPU1510SP,
            CpuType.CPU1512SP => ECPUType.CPU1512SP,
            CpuType.CPU1510SPF => ECPUType.CPU1510SPF,
            CpuType.CPU1512SPF => ECPUType.CPU1512SPF,
            CpuType.CPU1514SP => ECPUType.CPU1514SP,
            CpuType.CPU1514SPT => ECPUType.CPU1514SPT,
            CpuType.CPU1514SPF => ECPUType.CPU1514SPF,
            CpuType.CPU1514SPTF => ECPUType.CPU1514SPTF,
            CpuType.CPU1500_RH_Unspecified => ECPUType.CPU1500_RH_Unspecified,
            CpuType.CPU1513R => ECPUType.CPU1513R,
            CpuType.CPU1515R => ECPUType.CPU1515R,
            CpuType.CPU1517H => ECPUType.CPU1517H,
            CpuType.CPU1518HF => ECPUType.CPU1518HF,
            CpuType.CPU1504DTF => ECPUType.CPU1504DTF,
            CpuType.CPU1507DTF => ECPUType.CPU1507DTF,
            CpuType.CPU1518T => ECPUType.CPU1518T,
            CpuType.CPU1518TF => ECPUType.CPU1518TF,
            CpuType.ET200PRO_Unspecified => ECPUType.ET200PRO_Unspecified,
            CpuType.CPU1513PRO => ECPUType.CPU1513PRO,
            CpuType.CPU1513PROF => ECPUType.CPU1513PROF,
            CpuType.CPU1516PRO => ECPUType.CPU1516PRO,
            CpuType.CPU1516PROF => ECPUType.CPU1516PROF,
            _ => throw new NotImplementedException(),
        };
    }
}