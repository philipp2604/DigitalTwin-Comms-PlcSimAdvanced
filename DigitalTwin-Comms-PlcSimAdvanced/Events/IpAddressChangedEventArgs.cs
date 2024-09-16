using DigitalTwin_Comms_PlcSimAdvanced.Constants;

namespace DigitalTwin_Comms_PlcSimAdvanced.Events;

public class IpAddressChangedEventArgs(ErrorCode errorCode, DateTime eventTime, byte interfaceId, string ipAddress, string subnetMask, string defaultGateway) : EventArgs
{
    public ErrorCode ErrorCode { get; } = errorCode;
    public DateTime EventTime { get; } = eventTime;
    public byte InterfaceId { get; } = interfaceId;
    public string IpAddress { get; } = ipAddress;
    public string SubnetMask { get; } = subnetMask;
    public string DefaultGateway { get; } = defaultGateway;
}