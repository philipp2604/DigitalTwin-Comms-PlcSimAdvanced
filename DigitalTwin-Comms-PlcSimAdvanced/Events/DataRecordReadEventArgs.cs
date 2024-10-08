﻿using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class DataRecordReadEventArgs(ErrorCode errorCode, DateTime eventTime, uint hardwareId, uint recordId, uint dataSize) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint RecordId { get; } = recordId;
    public uint DataSize { get; } = dataSize;
}