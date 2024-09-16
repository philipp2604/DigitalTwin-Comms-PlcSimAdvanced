using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class OperatingStateConverter
{
    public static OperatingStateType ConvertOperatingState(EOperatingState operatingState)
    {
        return operatingState switch
        {
            EOperatingState.InvalidOperatingState => OperatingStateType.InvalidOperatingState,
            EOperatingState.Off => OperatingStateType.Off,
            EOperatingState.Booting => OperatingStateType.Booting,
            EOperatingState.Stop => OperatingStateType.Stop,
            EOperatingState.Startup => OperatingStateType.Startup,
            EOperatingState.Run => OperatingStateType.Run,
            EOperatingState.Freeze => OperatingStateType.Freeze,
            EOperatingState.ShuttingDown => OperatingStateType.ShuttingDown,
            EOperatingState.Hold => OperatingStateType.Hold,
            _ => throw new NotImplementedException(),
        };
    }
}