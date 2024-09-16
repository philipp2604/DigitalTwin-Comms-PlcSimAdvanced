using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Converters;
using DigitalTwin_Comms_PlcSimAdvanced.Events;
using DigitalTwin_Comms_PlcSimAdvanced.Interfaces.Models;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Models;

public class PlcSimAdvInstance : IPlcSimAdvInstance
{
    private readonly IInstance _instance;
    private readonly SIPSuite4 _ipSuite;

    public PlcSimAdvInstance(string name, CpuType cpuType = CpuType.CPU1500_Unspecified, CommunicationInterface interfaceType = CommunicationInterface.Softbus, string ipAddress = "192.168.0.1", string subnetMask = "255.255.255.0", string gateway = "0.0.0.0", uint interfaceId = 0, bool sendSyncEventInDefaultModeEnabled = true)
    {
        Name = name;
        IpAddress = ipAddress;
        SubnetMask = subnetMask;
        Gateway = gateway;
        InterfaceId = interfaceId;

        //Ip settings
        _ipSuite = new SIPSuite4(ipAddress, subnetMask, gateway);

        //Register instance
        _instance = SimulationRuntimeManager.RegisterInstance(CpuTypeConverter.ConvertCpuType(cpuType), Name);

        //Setup
        _instance.IsSendSyncEventInDefaultModeEnabled = sendSyncEventInDefaultModeEnabled;
        _instance.CommunicationInterface = CommunicationInterfaceConverter.ConvertCommunicationInterfaceType(interfaceType);
        //Events
        _instance.OnAlarmNotificationDone += OnAlarmNotificationDone;
        _instance.OnDataRecordRead += OnDataRecordRead;
        _instance.OnDataRecordWrite += OnDataRecordWrite;
        _instance.OnHardwareConfigChanged += OnHardwareConfigChanged;
        _instance.OnIPAddressChanged += OnIpAddressChanged;
        _instance.OnLedChanged += OnLedChanged;
        _instance.OnOperatingStateChanged += OnOperatingStateChanged;
        _instance.OnProcessEventDone += OnProcessEventDone;
        _instance.OnProfileEventDone += OnProfileEventDone;
        _instance.OnPullOrPlugEventDone += OnPullOrPlugEventDone;
        _instance.OnRackOrStationFaultEvent += OnRackOrStationFaultEvent;
        _instance.OnSoftwareConfigurationChanged += OnSoftwareConfigurationChanged;
        _instance.OnStatusEventDone += OnStatusEventDone;
        _instance.OnSyncPointReached += OnSyncPointReached;
        _instance.OnUpdateEventDone += OnUpdateEventDone;
    }

    public event EventHandler<AlarmNotificationDoneEventArgs>? AlarmNotificationDone;

    public event EventHandler<DataRecordReadEventArgs>? DataRecordRead;

    public event EventHandler<DataRecordWriteEventArgs>? DataRecordWrite;

    public event EventHandler<HardwareConfigurationChangedEventArgs>? HardwareConfigurationChanged;

    public event EventHandler<IpAddressChangedEventArgs>? IpAddressChanged;

    public event EventHandler<LedChangedEventArgs>? LedChanged;

    public event EventHandler<OperatingStateChangedEventArgs>? OperatingStateChanged;

    public event EventHandler<ProcessEventDoneEventArgs>? ProcessEventDone;

    public event EventHandler<ProfileEventDoneEventArgs>? ProfileEventDone;

    public event EventHandler<PullOrPlugEventDoneEventArgs>? PullOrPlugEventDone;

    public event EventHandler<RackOrStationFaultEventArgs>? RackOrStationFault;

    public event EventHandler<SoftwareConfigurationChangedEventArgs>? SoftwareConfigurationChanged;

    public event EventHandler<StatusEventDoneEventArgs>? StatusEventDone;

    public event EventHandler<SyncPointReachedEventArgs>? SyncPointReached;

    public event EventHandler<UpdateEventDoneEventArgs>? UpdateEventDone;

    public string Name { get; }
    public bool IsInitialized { get; set; }
    public string IpAddress { get; }
    public string SubnetMask { get; }
    public string Gateway { get; }
    public uint InterfaceId { get; }
    public string StoragePath { get; set; }

    public void ArchiveStorage(string fileName)
    {
        _instance.ArchiveStorage(fileName);
    }

    public void RetrieveStorage(string fileName)
    {
        _instance.RetrieveStorage(fileName);
    }

    public void CleanupStoragePath()
    {
        _instance.CleanupStoragePath();
    }

    public ErrorCode PowerOn(uint timeOut = 60000)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(_instance.PowerOn(timeOut));
        if(errorCode == ErrorCode.OK)
            _instance.SetIPSuite(InterfaceId, _ipSuite, true);

        return errorCode;
    }

    public OperatingState OperatingState { get => OperatingStateConverter.ConvertOperatingState(_instance.OperatingState); }

    public void MemoryReset()
    {
        _instance.MemoryReset();
    }

    public void UpdateTagList()
    {
        _instance.UpdateTagList(ETagListDetails.)
    }

    public void PowerOff(uint timeOut = 60000)
    {
        IsInitialized = false;
        _instance.PowerOff(timeOut);
    }

    public void Run(uint timeOut = 60000)
    {
        _instance.Run(timeOut);
    }

    public void Stop(uint timeOut = 60000)
    {
        IsInitialized = false;
        _instance.Stop(timeOut);
    }

    public void UnregisterInstance()
    {
        _instance.UnregisterInstance();
    }

    public bool ReadBool(string tag)
    {
        return _instance.ReadBool(tag);
    }

    public void WriteBool(string tag, bool value)
    {
        _instance.WriteBool(tag, value);
    }

    public void Dispose()
    {
        IsInitialized = false;
        if(_instance != null)
        {
            try
            {
                PowerOff(5000);
                UnregisterInstance();
            }
            catch { }
        }

        GC.SuppressFinalize(this);
    }

    private void OnAlarmNotificationDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, uint in_SequenceNumber)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        AlarmNotificationDone?.Invoke(in_Sender, new AlarmNotificationDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier, in_SequenceNumber));
    }

    private void OnDataRecordRead(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, SDataRecordInfo in_DataRecordInfo)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        DataRecordRead?.Invoke(in_Sender, new DataRecordReadEventArgs(errorCode, in_DateTime, in_DataRecordInfo.HardwareId, in_DataRecordInfo.RecordIdx, in_DataRecordInfo.DataSize));
    }

    private void OnDataRecordWrite(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, SDataRecord in_DataRecord)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        DataRecordWrite?.Invoke(in_Sender, new DataRecordWriteEventArgs(errorCode, in_DateTime, in_DataRecord.Info.HardwareId, in_DataRecord.Info.RecordIdx, in_DataRecord.Info.DataSize, in_DataRecord.Data));
    }

    private void OnHardwareConfigChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        HardwareConfigurationChanged?.Invoke(in_Sender, new HardwareConfigurationChangedEventArgs(errorCode, in_DateTime));
    }

    private void OnIpAddressChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, byte in_InterfaceId, SIPSuite4 in_SIP)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        IpAddressChanged?.Invoke(in_Sender, new IpAddressChangedEventArgs(errorCode, in_SystemTime, in_InterfaceId, in_SIP.IPAddress.IPString, in_SIP.SubnetMask.IPString, in_SIP.DefaultGateway.IPString));
    }

    private void OnLedChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, ELEDType in_LEDType, ELEDMode in_LEDMode)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        var ledType = LedTypeConverter.ConvertLedType(in_LEDType);
        var ledMode = LedModeConverter.ConvertLedMode(in_LEDMode);
        LedChanged?.Invoke(in_Sender, new LedChangedEventArgs(errorCode, in_DateTime, ledType, ledMode));
    }

    private void OnOperatingStateChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, EOperatingState in_PrevState, EOperatingState in_OperatingState)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        var prevState = OperatingStateConverter.ConvertOperatingState(in_PrevState);
        var newState = OperatingStateConverter.ConvertOperatingState(in_OperatingState);
        OperatingStateChanged?.Invoke(in_Sender, new OperatingStateChangedEventArgs(errorCode, in_DateTime, prevState, newState));

        IsInitialized = false;

        if(newState == OperatingState.Startup)
        {
            try
            {
                _instance.UpdateTagList();
            }
            catch (Exception)
            {
                return;
            }
            IsInitialized = true;
        }
    }

    private void OnProcessEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, uint in_Channel, EProcessEventType in_ProcessEventType, uint in_SequenceNumber)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        var processEventType = ProcessEventTypeConverter.ConvertProcessEventType(in_ProcessEventType);
        ProcessEventDone?.Invoke(in_Sender, new ProcessEventDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier, in_Channel, processEventType, in_SequenceNumber));
    }

    private void OnProfileEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        ProfileEventDone?.Invoke(in_Sender, new ProfileEventDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier));
    }

    private void OnPullOrPlugEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, EPullOrPlugEventType in_PullOrPlugEventType, uint in_SequenceNumber)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        var pullOrPlugEventType = PullOrPlugEventTypeConverter.ConvertPullOrPlugEventType(in_PullOrPlugEventType);
        PullOrPlugEventDone?.Invoke(in_Sender, new PullOrPlugEventDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier, pullOrPlugEventType, in_SequenceNumber));
    }

    private void OnRackOrStationFaultEvent(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, ERackOrStationFaultType in_EventType)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        var faultType = RackOrStationFaultTypeConverter.ConvertRackOrStationFaultType(in_EventType);
        RackOrStationFault?.Invoke(in_Sender, new RackOrStationFaultEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier, faultType));
    }

    private void OnSoftwareConfigurationChanged(IInstance instance, SOnSoftwareConfigChangedParameter event_param)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(event_param.ErrorCode);
        var configChanged = event_param.ChangeType == ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN ? SoftwareConfigChanged.SoftwareChangedInRun : SoftwareConfigChanged.SoftwareChangedInStop;
        SoftwareConfigurationChanged?.Invoke(instance, new SoftwareConfigurationChangedEventArgs(errorCode, event_param.EventCreateTime, configChanged));

        IsInitialized = false;

        if (configChanged == SoftwareConfigChanged.SoftwareChangedInStop)
        {
            try
            {
                _instance.UpdateTagList();
            }
            catch (Exception)
            {
                return;
            }
            IsInitialized = true;
        }
    }

    private void OnStatusEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        StatusEventDone?.Invoke(in_Sender, new StatusEventDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier));
    }

    private void OnSyncPointReached(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, uint in_Id, long in_TimeSinceSameSyncPoint_ns, long in_TimeSinceAnySyncPoint_ns, uint in_SyncPointCount)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        SyncPointReached?.Invoke(in_Sender, new SyncPointReachedEventArgs(errorCode, in_DateTime, in_Id, in_TimeSinceSameSyncPoint_ns, in_TimeSinceAnySyncPoint_ns, in_SyncPointCount));
    }

    private void OnUpdateEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(in_ErrorCode);
        UpdateEventDone?.Invoke(in_Sender, new UpdateEventDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier));
    }
}