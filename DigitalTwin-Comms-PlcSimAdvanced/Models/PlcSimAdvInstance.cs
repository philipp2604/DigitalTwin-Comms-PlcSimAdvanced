using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Converters;
using DigitalTwin_Comms_PlcSimAdvanced.Events;
using DigitalTwin_Comms_PlcSimAdvanced.Interfaces.Models;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Models;

/// <summary>
/// A class implementing <see cref="IPlcSimAdvInstance"/>, used for communicating with PLCSim Advanced instances.
/// </summary>
public class PlcSimAdvInstance : IPlcSimAdvInstance
{
    private readonly IInstance _instance;
    private readonly SIPSuite4 _ipSuite;

    /// <summary>
    /// Creates a new instance of <see cref="PlcSimAdvInstance"/> which implements <see cref="IPlcSimAdvInstance"/>.
    /// </summary>
    /// <param name="name">Name of the PLCSim instance.</param>
    /// <param name="cpuType">CPU type.</param>
    /// <param name="interfaceType">Communication interface type.</param>
    /// <param name="interfaceId">Id of the communication interface.</param>
    /// <param name="ipAddress">Ip address for the interface.</param>
    /// <param name="subnetMask">Subnet mask for the interface.</param>
    /// <param name="gateway">Default gateway for the interface.</param>
    public PlcSimAdvInstance(string name, CpuType cpuType = CpuType.CPU1500_Unspecified, CommunicationInterface interfaceType = CommunicationInterface.Softbus, uint interfaceId = 0, string ipAddress = "192.168.0.1", string subnetMask = "255.255.255.0", string gateway = "0.0.0.0")
    {
        Name = name;
        InterfaceId = interfaceId;

        //Ip settings
        _ipSuite = new SIPSuite4(ipAddress, subnetMask, gateway);

        //Register instance
        _instance = SimulationRuntimeManager.RegisterInstance(CpuTypeConverter.ConvertCpuType(cpuType), Name);

        //Setup CommInterface
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

    #region Events

    /// <inheritdoc/>
    public event EventHandler<AlarmNotificationDoneEventArgs>? AlarmNotificationDone;

    /// <inheritdoc/>
    public event EventHandler<DataRecordReadEventArgs>? DataRecordRead;

    /// <inheritdoc/>
    public event EventHandler<DataRecordWriteEventArgs>? DataRecordWrite;

    /// <inheritdoc/>
    public event EventHandler<HardwareConfigurationChangedEventArgs>? HardwareConfigurationChanged;

    /// <inheritdoc/>
    public event EventHandler<IpAddressChangedEventArgs>? IpAddressChanged;

    /// <inheritdoc/>
    public event EventHandler<LedChangedEventArgs>? LedChanged;

    /// <inheritdoc/>
    public event EventHandler<OperatingStateChangedEventArgs>? OperatingStateChanged;

    /// <inheritdoc/>
    public event EventHandler<ProcessEventDoneEventArgs>? ProcessEventDone;

    /// <inheritdoc/>
    public event EventHandler<ProfileEventDoneEventArgs>? ProfileEventDone;

    /// <inheritdoc/>
    public event EventHandler<PullOrPlugEventDoneEventArgs>? PullOrPlugEventDone;

    /// <inheritdoc/>
    public event EventHandler<RackOrStationFaultEventArgs>? RackOrStationFault;

    /// <inheritdoc/>
    public event EventHandler<SoftwareConfigurationChangedEventArgs>? SoftwareConfigurationChanged;

    /// <inheritdoc/>
    public event EventHandler<StatusEventDoneEventArgs>? StatusEventDone;

    /// <inheritdoc/>
    public event EventHandler<SyncPointReachedEventArgs>? SyncPointReached;

    /// <inheritdoc/>
    public event EventHandler<UpdateEventDoneEventArgs>? UpdateEventDone;

    #endregion Events

    #region Properties

    /// <inheritdoc/>
    public uint InterfaceId { get; }

    /// <inheritdoc/>
    public bool IsInitialized { get; private set; }

    /// <inheritdoc/>
    public bool IsSendSyncEventInDefaultModeEnabled { get => _instance.IsSendSyncEventInDefaultModeEnabled; set => _instance.IsSendSyncEventInDefaultModeEnabled = value; }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public OperatingState OperatingState { get => OperatingStateConverter.ConvertOperatingState(_instance.OperatingState); }

    /// <inheritdoc/>
    public double ScaleFactor { get => _instance.ScaleFactor; set => _instance.ScaleFactor = value; }

    /// <inheritdoc/>
    public string StoragePath { get => _instance.StoragePath; set => _instance.StoragePath = value; }

    #endregion Properties

    #region Virtual memory card functions

    /// <inheritdoc/>
    public void ArchiveStorage(string fileName)
    {
        _instance.ArchiveStorage(fileName);
    }

    /// <inheritdoc/>
    public void CleanupStoragePath()
    {
        _instance.CleanupStoragePath();
    }

    /// <inheritdoc/>
    public void RetrieveStorage(string fileName)
    {
        _instance.RetrieveStorage(fileName);
    }

    #endregion Virtual memory card functions

    #region Operating state functions

    /// <inheritdoc/>
    public ErrorCode PowerOn(uint timeOut = 60000)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(_instance.PowerOn(timeOut));
        if (errorCode == ErrorCode.OK)
            _instance.SetIPSuite(InterfaceId, _ipSuite, true);

        return errorCode;
    }

    /// <inheritdoc/>
    public void PowerOff(uint timeOut = 60000)
    {
        ///TODO: Unregister from all events!
        IsInitialized = false;

        AlarmNotificationDone = null;
        DataRecordRead = null;
        DataRecordWrite = null;
        HardwareConfigurationChanged = null;
        IpAddressChanged = null;
        LedChanged = null;
        OperatingStateChanged = null;
        ProcessEventDone = null;
        ProfileEventDone = null;
        PullOrPlugEventDone = null;
        RackOrStationFault = null;
        SoftwareConfigurationChanged = null;
        StatusEventDone = null;
        SyncPointReached = null;
        UpdateEventDone = null;

        _instance.OnAlarmNotificationDone -= OnAlarmNotificationDone;
        _instance.OnDataRecordRead -= OnDataRecordRead;
        _instance.OnDataRecordWrite -= OnDataRecordWrite;
        _instance.OnHardwareConfigChanged -= OnHardwareConfigChanged;
        _instance.OnIPAddressChanged -= OnIpAddressChanged;
        _instance.OnLedChanged -= OnLedChanged;
        _instance.OnOperatingStateChanged -= OnOperatingStateChanged;
        _instance.OnProcessEventDone -= OnProcessEventDone;
        _instance.OnProfileEventDone -= OnProfileEventDone;
        _instance.OnPullOrPlugEventDone -= OnPullOrPlugEventDone;
        _instance.OnRackOrStationFaultEvent -= OnRackOrStationFaultEvent;
        _instance.OnSoftwareConfigurationChanged -= OnSoftwareConfigurationChanged;
        _instance.OnStatusEventDone -= OnStatusEventDone;
        _instance.OnSyncPointReached -= OnSyncPointReached;
        _instance.OnUpdateEventDone -= OnUpdateEventDone;

        _instance.PowerOff(timeOut);
    }

    /// <inheritdoc/>
    public void Run(uint timeOut = 60000)
    {
        _instance.Run(timeOut);
    }

    /// <inheritdoc/>
    public void Stop(uint timeOut = 60000)
    {
        IsInitialized = false;
        _instance.Stop(timeOut);
    }

    /// <inheritdoc/>
    public void MemoryReset()
    {
        _instance.MemoryReset();
    }

    #endregion Operating state functions

    #region Variable tables

    /// <inheritdoc/>
    public void CreateConfigurationFile(string fileName)
    {
        _instance.CreateConfigurationFile(fileName);
    }

    /// <inheritdoc/>
    public void UpdateTagList(TagListDetails tagListDetails = TagListDetails.IOMCTDB, bool isHMIVisibleOnly = true, string? dataBlockFilterList = null)
    {
        _instance.UpdateTagList(TagListDetailsConverter.ConvertTagListDetailsType(tagListDetails), isHMIVisibleOnly, dataBlockFilterList);
    }

    #endregion Variable tables

    #region Variable access functions using tag names

    /// <inheritdoc/>
    public bool ReadBool(string tag)
    {
        return _instance.ReadBool(tag);
    }

    /// <inheritdoc/>
    public sbyte ReadInt8(string tag)
    {
        return _instance.ReadInt8(tag);
    }

    /// <inheritdoc/>
    public short ReadInt16(string tag)
    {
        return _instance.ReadInt16(tag);
    }

    /// <inheritdoc/>
    public int ReadInt32(string tag)
    {
        return _instance.ReadInt32(tag);
    }

    /// <inheritdoc/>
    public long ReadInt64(string tag)
    {
        return _instance.ReadInt64(tag);
    }

    /// <inheritdoc/>
    public byte ReadUInt8(string tag)
    {
        return _instance.ReadUInt8(tag);
    }

    /// <inheritdoc/>
    public ushort ReadUInt16(string tag)
    {
        return _instance.ReadUInt16(tag);
    }

    /// <inheritdoc/>
    public uint ReadUInt32(string tag)
    {
        return _instance.ReadUInt32(tag);
    }

    /// <inheritdoc/>
    public ulong ReadUInt64(string tag)
    {
        return _instance.ReadUInt64(tag);
    }

    /// <inheritdoc/>
    public float ReadFloat(string tag)
    {
        return _instance.ReadFloat(tag);
    }

    /// <inheritdoc/>
    public double ReadDouble(string tag)
    {
        return _instance.ReadDouble(tag);
    }

    /// <inheritdoc/>
    public sbyte ReadChar(string tag)
    {
        return _instance.ReadChar(tag);
    }

    /// <inheritdoc/>
    public char ReadWChar(string tag)
    {
        return _instance.ReadWChar(tag);
    }

    /// <inheritdoc/>
    public string ReadString(string tag)
    {
        return _instance.ReadString(tag);
    }

    /// <inheritdoc/>
    public string ReadWString(string tag)
    {
        return _instance.ReadWString(tag);
    }

    /// <inheritdoc/>
    public void WriteBool(string tag, bool value)
    {
        _instance.WriteBool(tag, value);
    }

    /// <inheritdoc/>
    public void WriteInt8(string tag, sbyte value)
    {
        _instance.WriteInt8(tag, value);
    }

    /// <inheritdoc/>
    public void WriteInt16(string tag, short value)
    {
        _instance.WriteInt16(tag, value);
    }

    /// <inheritdoc/>
    public void WriteInt32(string tag, int value)
    {
        _instance.WriteInt32(tag, value);
    }

    /// <inheritdoc/>
    public void WriteInt64(string tag, long value)
    {
        _instance.WriteInt64(tag, value);
    }

    /// <inheritdoc/>
    public void WriteUInt8(string tag, byte value)
    {
        _instance.WriteUInt8(tag, value);
    }

    /// <inheritdoc/>
    public void WriteUInt16(string tag, ushort value)
    {
        _instance.WriteUInt16(tag, value);
    }

    /// <inheritdoc/>
    public void WriteUInt32(string tag, uint value)
    {
        _instance.WriteUInt32(tag, value);
    }

    /// <inheritdoc/>
    public void WriteUInt64(string tag, ulong value)
    {
        _instance.WriteUInt64(tag, value);
    }

    /// <inheritdoc/>
    public void WriteFloat(string tag, float value)
    {
        _instance.WriteFloat(tag, value);
    }

    /// <inheritdoc/>
    public void WriteDouble(string tag, double value)
    {
        _instance.WriteDouble(tag, value);
    }

    /// <inheritdoc/>
    public void WriteChar(string tag, sbyte value)
    {
        _instance.WriteChar(tag, value);
    }

    /// <inheritdoc/>
    public void WriteWChar(string tag, char value)
    {
        _instance.WriteWChar(tag, value);
    }

    /// <inheritdoc/>
    public void WriteString(string tag, string value)
    {
        _instance.WriteString(tag, value);
    }

    /// <inheritdoc/>
    public void WriteWString(string tag, string value)
    {
        _instance.WriteWString(tag, value);
    }

    #endregion Variable access functions using tag names

    /// <inheritdoc/>
    public void Dispose()
    {
        IsInitialized = false;
        if (_instance != null)
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

    #region API interface functions

    /// <inheritdoc/>
    public void UnregisterInstance()
    {
        _instance.UnregisterInstance();
    }

    #endregion API interface functions

    #region Event processing

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

        if (newState == OperatingState.Startup)
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
        var configChanged = event_param.ChangeType == ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN ? SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN : SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP;
        SoftwareConfigurationChanged?.Invoke(instance, new SoftwareConfigurationChangedEventArgs(errorCode, event_param.EventCreateTime, configChanged));

        IsInitialized = false;

        if (configChanged == SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP)
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

    #endregion Event processing
}