using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class PlcSimAdvDataRecordWriteEventArgs(PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode, DateTime eventTime, uint hardwareId, uint recordId, uint dataSize, byte[] data) : EventArgs
{
    public PlcSimAdvErrorCode.PlcSimAdvErrorCodeType ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public uint HardwareId { get; } = hardwareId;
    public uint RecordId { get; } = recordId;
    public uint DataSize { get; } = dataSize;
    public byte[] Data { get; } = data;
}