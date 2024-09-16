using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class SoftwareConfigurationChangedEventArgs(ErrorCode errorCode, DateTime eventTime, SoftwareConfigChanged state) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public SoftwareConfigChanged ChangeType { get; } = state;
}