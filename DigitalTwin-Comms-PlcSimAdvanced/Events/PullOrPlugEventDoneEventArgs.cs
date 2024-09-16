using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class PullOrPlugEventDoneEventArgs(ErrorCode errorCode, DateTime eventTime, uint hardwareId, PullOrPlugEventType pullOrPlugEventType, uint sequenceNumber) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public PullOrPlugEventType PullOrPlugEventType { get; } = pullOrPlugEventType;
    public uint SequenceNumber { get; } = sequenceNumber;
}