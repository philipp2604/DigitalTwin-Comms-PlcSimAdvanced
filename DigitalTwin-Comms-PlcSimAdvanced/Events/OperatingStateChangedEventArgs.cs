using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class OperatingStateChangedEventArgs(ErrorCodeType errorCode, DateTime eventTime, OperatingStateType prevState, OperatingStateType newState) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public OperatingStateType PreviousState { get; } = prevState;
    public OperatingStateType NewState { get; } = newState;
}