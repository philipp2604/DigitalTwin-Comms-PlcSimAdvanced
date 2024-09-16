using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class PullOrPlugEventTypeConverter
{
    public static PullOrPlugEventType ConvertPullOrPlugEventType(EPullOrPlugEventType pullOrPlugEventType)
    {
        return pullOrPlugEventType switch
        {
            EPullOrPlugEventType.Undefined => PullOrPlugEventType.Undefined,
            EPullOrPlugEventType.Pull => PullOrPlugEventType.Pull,
            EPullOrPlugEventType.Plug => PullOrPlugEventType.Plug,
            EPullOrPlugEventType.PlugErrorRemains => PullOrPlugEventType.PlugErrorRemains,
            EPullOrPlugEventType.PlugWrongModule => PullOrPlugEventType.PlugWrongModule,
            _ => throw new NotImplementedException(),
        };
    }
}