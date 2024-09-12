using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class PlcSimAdvAlarmNotificationDoneEventArgs(PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode, DateTime eventTime, uint hardwareId, uint sequenceNumber) : EventArgs
{
    public PlcSimAdvErrorCode.PlcSimAdvErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint SequenceNumber { get; } = sequenceNumber;
}