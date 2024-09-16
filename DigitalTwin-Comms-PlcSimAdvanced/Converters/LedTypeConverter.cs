using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class LedTypeConverter
{
    public static LedType ConvertLedType(ELEDType ledType)
    {
        return ledType switch
        {
            ELEDType.Stop => LedType.Stop,
            ELEDType.Run => LedType.Run,
            ELEDType.Error => LedType.Error,
            ELEDType.Maint => LedType.Maintenance,
            ELEDType.Redund => LedType.Redundancy,
            ELEDType.Force => LedType.Force,
            ELEDType.Busf1 => LedType.BusFault1,
            ELEDType.Busf2 => LedType.BusFault2,
            ELEDType.Busf3 => LedType.BusFault3,
            ELEDType.Busf4 => LedType.BusFault4,
            _ => throw new NotImplementedException(),
        };
    }

    public static ELEDType ConvertLedType(LedType ledType)
    {
        return ledType switch
        {
            LedType.Stop => ELEDType.Stop,
            LedType.Run => ELEDType.Run,
            LedType.Error => ELEDType.Error,
            LedType.Maintenance => ELEDType.Maint,
            LedType.Redundancy => ELEDType.Redund,
            LedType.Force => ELEDType.Force,
            LedType.BusFault1 => ELEDType.Busf1,
            LedType.BusFault2 => ELEDType.Busf2,
            LedType.BusFault3 => ELEDType.Busf3,
            LedType.BusFault4 => ELEDType.Busf4,
            _ => throw new NotImplementedException(),
        };
    }
}