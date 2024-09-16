using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class HardwareConfigurationChangedEventArgs(ErrorCodeType errorCode, DateTime eventTime) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
}