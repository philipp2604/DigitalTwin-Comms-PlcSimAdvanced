using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Events;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Models;

public class PlcSimAdvInstance
{
    private readonly IInstance _instance;
    private readonly SIPSuite4 _ipSuite;

    public PlcSimAdvInstance(string name, CommunicationInterfaceType interfaceType = CommunicationInterfaceType.Softbus, string ipAddress = "192.168.0.1", string subnetMask = "255.255.255.0", string gateway = "0.0.0.0", uint interfaceId = 0)
    {
        Name = name;
        IpAddress = ipAddress;
        SubnetMask = subnetMask;
        Gateway = gateway;
        InterfaceId = interfaceId;

        _ipSuite = new SIPSuite4(ipAddress, subnetMask, gateway);

        _instance = SimulationRuntimeManager.RegisterInstance(Name);
        _instance.IsSendSyncEventInDefaultModeEnabled = true;
        _instance.OnSyncPointReached += OnSyncPointReached;
        _instance.OnSoftwareConfigurationChanged += OnSoftwareConfigurationChanged;

        if (interfaceType == CommunicationInterfaceType.None)
            _instance.CommunicationInterface = ECommunicationInterface.None;
        else if (interfaceType == CommunicationInterfaceType.Softbus)
            _instance.CommunicationInterface = ECommunicationInterface.Softbus;
        else if (interfaceType == CommunicationInterfaceType.TCPIP)
            _instance.CommunicationInterface = ECommunicationInterface.TCPIP;
    }

    public event EventHandler? OnInitialized;

    public event EventHandler<PlcSimAdvEndOfCycleEventArgs>? EndOfCycle;

    public event EventHandler<PlcSimAdvSoftwareConfigurationChangedEventArgs>? SoftwareConfigurationChanged;

    public string Name { get; }
    public bool Initialized { get; private set; }
    public string IpAddress { get; }
    public string SubnetMask { get; }
    public string Gateway { get; }
    public uint InterfaceId { get; }

    public enum CommunicationInterfaceType
    {
        None,
        Softbus,
        TCPIP
    }

    public void PowerOn(uint timeOut = 60000)
    {
        _instance.PowerOn(timeOut);
        _instance.SetIPSuite(InterfaceId, _ipSuite, true);
    }

    public void PowerOff(uint timeOut = 60000)
    {
        _instance.PowerOff(timeOut);
    }

    public void Run(uint timeOut = 60000)
    {
        _instance.Run(timeOut);
    }

    public void Stop(uint timeOut = 60000)
    {
        _instance.Stop(timeOut);
    }

    private void OnSoftwareConfigurationChanged(IInstance instance, SOnSoftwareConfigChangedParameter event_param)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(event_param.ErrorCode);
        PlcSimAdvSoftwareConfigChanged configChanged = event_param.ChangeType == ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN ? PlcSimAdvSoftwareConfigChanged.SoftwareChangedInRun : PlcSimAdvSoftwareConfigChanged.SoftwareChangedInStop;
        SoftwareConfigurationChanged?.Invoke(instance, new PlcSimAdvSoftwareConfigurationChangedEventArgs(errorCode, event_param.EventCreateTime, configChanged));
        Initialized = false;

        instance.UpdateTagList(ETagListDetails.IOMCTDB);
        Initialized = true;
        OnInitialized?.Invoke(instance, EventArgs.Empty);
    }

    private void OnSyncPointReached(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, uint in_Id, long in_TimeSinceSameSyncPoint_ns, long in_TimeSinceAnySyncPoint_ns, uint in_SyncPointCount)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        EndOfCycle?.Invoke(in_Sender, new PlcSimAdvEndOfCycleEventArgs(errorCode, in_DateTime, in_Id, in_TimeSinceSameSyncPoint_ns, in_TimeSinceAnySyncPoint_ns, in_SyncPointCount));
    }
}