using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class OperatingStateConverter
{
    public static OperatingState ConvertOperatingState(EOperatingState operatingState)
    {
        return operatingState switch
        {
            EOperatingState.InvalidOperatingState => OperatingState.InvalidOperatingState,
            EOperatingState.Off => OperatingState.Off,
            EOperatingState.Booting => OperatingState.Booting,
            EOperatingState.Stop => OperatingState.Stop,
            EOperatingState.Startup => OperatingState.Startup,
            EOperatingState.Run => OperatingState.Run,
            EOperatingState.Freeze => OperatingState.Freeze,
            EOperatingState.ShuttingDown => OperatingState.ShuttingDown,
            EOperatingState.Hold => OperatingState.Hold,
            _ => throw new NotImplementedException(),
        };
    }

    public static EOperatingState ConvertOperatingState(OperatingState operatingState)
    {
        return operatingState switch
        {
            OperatingState.InvalidOperatingState => EOperatingState.InvalidOperatingState,
            OperatingState.Off => EOperatingState.Off,
            OperatingState.Booting => EOperatingState.Booting,
            OperatingState.Stop => EOperatingState.Stop,
            OperatingState.Startup => EOperatingState.Startup,
            OperatingState.Run => EOperatingState.Run,
            OperatingState.Freeze => EOperatingState.Freeze,
            OperatingState.ShuttingDown => EOperatingState.ShuttingDown,
            OperatingState.Hold => EOperatingState.Hold,
            _ => throw new NotImplementedException(),
        };
    }
}