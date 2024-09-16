using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class LedChangedEventArgs(ErrorCode errorCode, DateTime eventTime, LedType ledType, LedMode ledMode) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public LedType LedType { get; } = ledType;
    public LedMode LedMode { get; } = ledMode;
}