using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class PlcSimAdvHardwareConfigurationChangedEventArgs(PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode, DateTime eventTime) : EventArgs
{
    public PlcSimAdvErrorCode.PlcSimAdvErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
}