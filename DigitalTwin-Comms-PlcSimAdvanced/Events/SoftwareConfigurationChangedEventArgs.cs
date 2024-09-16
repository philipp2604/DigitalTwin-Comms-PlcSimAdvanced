﻿using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class SoftwareConfigurationChangedEventArgs(ErrorCodeType errorCode, DateTime eventTime, SoftwareConfigChangedType state) : EventArgs
{
    public ErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public SoftwareConfigChangedType ChangeType { get; } = state;
}