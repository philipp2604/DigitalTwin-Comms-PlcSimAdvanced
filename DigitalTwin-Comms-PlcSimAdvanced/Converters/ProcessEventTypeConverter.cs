using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class ProcessEventTypeConverter
{
    public static ProcessEventType ConvertProcessEventType(EProcessEventType processEventType)
    {
        return processEventType switch
        {
            EProcessEventType.Undefined => ProcessEventType.Undefined,
            EProcessEventType.RisingEdge => ProcessEventType.RisingEdge,
            EProcessEventType.FallingEdge => ProcessEventType.FallingEdge,
            EProcessEventType.Limit_1_Underrun => ProcessEventType.Limit_1_Underrun,
            EProcessEventType.Limit_1_Overrun => ProcessEventType.Limit_1_Overrun,
            EProcessEventType.Limit_2_Underrun => ProcessEventType.Limit_2_Underrun,
            EProcessEventType.Limit_2_Overrun => ProcessEventType.Limit_2_Overrun,
            _ => throw new NotImplementedException(),
        };
    }
}