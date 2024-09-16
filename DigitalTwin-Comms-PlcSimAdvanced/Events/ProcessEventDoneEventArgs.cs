using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class ProcessEventDoneEventArgs(ErrorCode errorCode, DateTime eventTime, uint hardwareId, uint channel, ProcessEventType processEventType, uint sequenceNumber) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint Channel { get; } = channel;
    public ProcessEventType ProcessEventType { get; } = processEventType;
    public uint SequenceNumber { get; } = sequenceNumber;
}