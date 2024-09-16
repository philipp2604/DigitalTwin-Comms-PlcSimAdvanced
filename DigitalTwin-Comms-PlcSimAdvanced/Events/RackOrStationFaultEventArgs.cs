using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class RackOrStationFaultEventArgs(ErrorCodeType errorCode, DateTime eventTime, uint hardwareId, RackOrStationFaultType faultType) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public RackOrStationFaultType FaultType { get; } = faultType;
}