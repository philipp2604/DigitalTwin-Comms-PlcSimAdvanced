using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class RackOrStationFaultTypeConverter
{
    public static RackOrStationFaultType ConvertRackOrStationFaultType(ERackOrStationFaultType faultType)
    {
        return faultType switch
        {
            ERackOrStationFaultType.Fault => RackOrStationFaultType.Fault,
            ERackOrStationFaultType.Return => RackOrStationFaultType.Return,
            _ => throw new NotImplementedException(),
        };
    }

    public static ERackOrStationFaultType ConvertRackOrStationFaultType(RackOrStationFaultType faultType)
    {
        return faultType switch
        {
            RackOrStationFaultType.Fault => ERackOrStationFaultType.Fault,
            RackOrStationFaultType.Return => ERackOrStationFaultType.Return,
            _ => throw new NotImplementedException(),
        };
    }
}