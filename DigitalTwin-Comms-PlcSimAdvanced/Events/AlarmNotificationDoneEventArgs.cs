using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class AlarmNotificationDoneEventArgs(ErrorCodeType errorCode, DateTime eventTime, uint hardwareId, uint sequenceNumber) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint SequenceNumber { get; } = sequenceNumber;
}