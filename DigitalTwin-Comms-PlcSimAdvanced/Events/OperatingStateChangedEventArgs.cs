using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class OperatingStateChangedEventArgs(ErrorCode errorCode, DateTime eventTime, OperatingState prevState, OperatingState newState) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public OperatingState PreviousState { get; } = prevState;
    public OperatingState NewState { get; } = newState;
}