using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class AlarmNotificationDoneEventArgs(ErrorCode errorCode, DateTime eventTime, uint hardwareId, uint sequenceNumber) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint SequenceNumber { get; } = sequenceNumber;
}