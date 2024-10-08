﻿using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class SyncPointReachedEventArgs(ErrorCode errorCode, DateTime eventTime, uint ioSystemId, long timeSinceSameSyncPoint, long timeSinceAnySyncPoint, uint syncPointCount) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint IoSystemId { get; } = ioSystemId;
    public long TimeSinceSameSyncPoint { get; } = timeSinceSameSyncPoint;
    public long TimeSinceAnySyncPoint { get; } = timeSinceAnySyncPoint;
    public uint SyncPointCount { get; } = syncPointCount;
}