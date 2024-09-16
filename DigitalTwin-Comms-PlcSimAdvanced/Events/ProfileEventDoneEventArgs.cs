using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class ProfileEventDoneEventArgs(ErrorCode errorCode, DateTime eventTime, uint hardwareId) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
}