using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class LedModeConverter
{
    public static LedMode ConvertLedMode(ELEDMode ledMode)
    {
        return ledMode switch
        {
            ELEDMode.Off => LedMode.Off,
            ELEDMode.On => LedMode.On,
            ELEDMode.FlashFast => LedMode.FlashFast,
            ELEDMode.FlashSlow => LedMode.FlashSlow,
            ELEDMode.Invalid => LedMode.Invalid,
            _ => throw new NotImplementedException(),
        };
    }
}