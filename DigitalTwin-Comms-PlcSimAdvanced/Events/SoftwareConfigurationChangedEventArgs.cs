using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class SoftwareConfigurationChangedEventArgs(ErrorCodeType errorCode, DateTime eventTime, SoftwareConfigChangedStates state) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public SoftwareConfigChangedStates ChangeType { get; } = state;
}