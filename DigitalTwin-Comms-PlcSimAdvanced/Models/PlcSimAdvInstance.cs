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
        IpAddress = ipAddress;
        SubnetMask = subnetMask;
        Gateway = gateway;
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

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<AlarmNotificationDoneEventArgs>? AlarmNotificationDone;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<DataRecordReadEventArgs>? DataRecordRead;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<DataRecordWriteEventArgs>? DataRecordWrite;

    /// <summary>
    /// Event being invoked if the hardware configuration changed.
    /// </summary>
    public event EventHandler<HardwareConfigurationChangedEventArgs>? HardwareConfigurationChanged;

    /// <summary>
    /// Event being invoked if an ip address changed.
    /// </summary>
    public event EventHandler<IpAddressChangedEventArgs>? IpAddressChanged;

    /// <summary>
    /// Event being invoked if the state of an LED changed.
    /// </summary>
    public event EventHandler<LedChangedEventArgs>? LedChanged;

    /// <summary>
    /// Event being invoked if the CPU's operating state changed.
    /// </summary>
    public event EventHandler<OperatingStateChangedEventArgs>? OperatingStateChanged;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<ProcessEventDoneEventArgs>? ProcessEventDone;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<ProfileEventDoneEventArgs>? ProfileEventDone;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<PullOrPlugEventDoneEventArgs>? PullOrPlugEventDone;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<RackOrStationFaultEventArgs>? RackOrStationFault;

    /// <summary>
    /// Event being invoked if the CPU's software configuration changed.
    /// </summary>
    public event EventHandler<SoftwareConfigurationChangedEventArgs>? SoftwareConfigurationChanged;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<StatusEventDoneEventArgs>? StatusEventDone;

    /// <summary>
    /// Event being invoked if a synchronization point is reached.
    /// </summary>
    public event EventHandler<SyncPointReachedEventArgs>? SyncPointReached;

    /// <summary>
    /// TODO
    /// </summary>
    public event EventHandler<UpdateEventDoneEventArgs>? UpdateEventDone;

    /// <summary>
    /// Gets the default gateway of the communication interface.
    /// </summary>
    public string Gateway { get; }

    /// <summary>
    /// Gets the id of the communication interface.
    /// </summary>
    public uint InterfaceId { get; }

    /// <summary>
    /// Gets the ip address of the communication interface.
    /// </summary>
    public string IpAddress { get; }

    /// <summary>
    /// Gets wheter the tag list is initialized.
    /// </summary>
    public bool IsInitialized { get; set; }

    /// <summary>
    /// Gets or sets if the <see cref="SyncPointReached"/> event is invoked in every mode after every cycle.
    /// </summary>
    public bool IsSendSyncEventInDefaultModeEnabled { get => _instance.IsSendSyncEventInDefaultModeEnabled; set => _instance.IsSendSyncEventInDefaultModeEnabled = value; }

    /// <summary>
    /// Gets the instance's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the current operating state.
    /// </summary>
    public OperatingState OperatingState { get => OperatingStateConverter.ConvertOperatingState(_instance.OperatingState); }

    /// <summary>
    /// Gets or sets the time scale factor.
    /// </summary>
    public double ScaleFactor { get => _instance.ScaleFactor; set => _instance.ScaleFactor = value; }

    /// <summary>
    /// Gets or sets the instance's storage path.
    /// </summary>
    public string StoragePath { get => _instance.StoragePath; set => _instance.StoragePath = value; }

    /// <summary>
    /// Gets the subnet mask of the communication interface.
    /// </summary>
    public string SubnetMask { get; }

    #region Virtual memory card functions
    /// <summary>
    /// Saves the user program, hardware configuration and remanent data in a virtual memory card.
    /// </summary>
    /// <param name="fileName">Path and file name of the virtual memory card.</param>
    public void ArchiveStorage(string fileName)
    {
        _instance.ArchiveStorage(fileName);
    }

    /// <summary>
    /// Deletes the folder that stores the virtual memory card.
    /// </summary>
    public void CleanupStoragePath()
    {
        _instance.CleanupStoragePath();
    }

    /// <summary>
    /// Retrieves saved data from a virtual memory card.
    /// </summary>
    /// <param name="fileName">Path and filename of the virtual memory card.</param>
    public void RetrieveStorage(string fileName)
    {
        _instance.RetrieveStorage(fileName);
    }
    #endregion

    #region Operating state functions
    /// <summary>
    /// Creates the process for the instance and starts the booting process.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    /// <returns>An <see cref="ErrorCode"/> value.</returns>
    public ErrorCode PowerOn(uint timeOut = 60000)
    {
        var errorCode = ErrorCodeConverter.ConvertErrorCode(_instance.PowerOn(timeOut));
        if(errorCode == ErrorCode.OK)
            _instance.SetIPSuite(InterfaceId, _ipSuite, true);

        return errorCode;
    }

    /// <summary>
    /// Ends the simulation and it's process.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    public void PowerOff(uint timeOut = 60000)
    {
        ///TODO: Unregister from all events!
        IsInitialized = false;
        _instance.PowerOff(timeOut);
    }

    /// <summary>
    /// Requests the virtual controller to change into RUN mode.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    public void Run(uint timeOut = 60000)
    {
        _instance.Run(timeOut);
    }

    /// <summary>
    /// Requests the virtual controller to change into STOP mode.
    /// </summary>
    /// <param name="timeOut"></param>
    public void Stop(uint timeOut = 60000)
    {
        IsInitialized = false;
        _instance.Stop(timeOut);
    }

    /// <summary>
    /// Powers off the virtual controller, ends it's process and restarts it. Resets the memory inbetween.
    /// </summary>
    public void MemoryReset()
    {
        _instance.MemoryReset();
    }
    #endregion

    #region Variable tables
    /// <summary>
    /// Exports all entries from the variable tables into one xml file.
    /// </summary>
    /// <param name="fileName">Path and file name of the xml file.</param>
    public void CreateConfigurationFile(string fileName)
    {
        _instance.CreateConfigurationFile(fileName);
    }
    /// <summary>
    /// Reads the variables from the virtual controller and stores them into the shared memory.
    /// </summary>
    /// <param name="tagListDetails">A values of <see cref="TagListDetails"/> specifying which variables shall be read.</param>
    /// <param name="isHMIVisibleOnly">Determines, if only variables shall be read, that are marked as 'HMI Visible'.</param>
    /// <param name="dataBlockFilterList">A string to filter variables by the names of data blocks.</param>
    public void UpdateTagList(TagListDetails tagListDetails = TagListDetails.IOMCTDB, bool isHMIVisibleOnly = true, string? dataBlockFilterList = null)
    {
        _instance.UpdateTagList(TagListDetailsConverter.ConvertTagListDetailsType(tagListDetails), isHMIVisibleOnly, dataBlockFilterList);
    }
    #endregion

    #region Variable access functions using tag names
    /// <summary>
    /// Reads the bool value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The bool value of the tag.</returns>
    public bool ReadBool(string tag)
    {
        return _instance.ReadBool(tag);
    }

    /// <summary>
    /// Reads the Int8 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int8 value of the tag.</returns>
    public sbyte ReadInt8(string tag)
    {
        return _instance.ReadInt8(tag);
    }

    /// <summary>
    /// Reads the Int16 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int16 value of the tag.</returns>
    public short ReadInt16(string tag)
    {
        return _instance.ReadInt16(tag);
    }
    public int ReadInt32(string tag)
    {
        return _instance.ReadInt32(tag);
    }
    public long ReadInt64(string tag)
    {
        return _instance.ReadInt64(tag);
    }

    public byte ReadUInt8(string tag)
    {
        return _instance.ReadUInt8(tag);
    }

    public ushort ReadUInt16(string tag)
    {
        return _instance.ReadUInt16(tag);
    }
    public uint ReadUInt32(string tag)
    {
        return _instance.ReadUInt32(tag);
    }
    public ulong ReadUInt64(string tag)
    {
        return _instance.ReadUInt64(tag);
    }

    public float ReadFlot(string tag)
    {
        return _instance.ReadFloat(tag);
    }

    public double ReadDouble(string tag)
    {
        return _instance.ReadDouble(tag);
    }

    public sbyte ReadChar(string tag)
    {
        return _instance.ReadChar(tag);
    }

    public char ReadWChar(string tag)
    {
        return _instance.ReadWChar(tag);
    }

    public string ReadString(string tag)
    {
        return _instance.ReadString(tag);
    }

    public string ReadWString(string tag)
    {
        return _instance.ReadWString(tag);
    }

    public void WriteBool(string tag, bool value)
    {
        _instance.WriteBool(tag, value);
    }

    public void WriteInt8(string tag, sbyte value)
    {
        _instance.WriteInt8(tag, value);
    }

    public void WriteInt16(string tag, short value)
    {
        _instance.WriteInt16(tag, value);
    }

    public void WriteInt32(string tag, int value)
    {
        _instance.WriteInt32(tag, value);
    }

    public void WriteInt64(string tag, long value)
    {
        _instance.WriteInt64(tag, value);
    }

    public void WriteUInt8(string tag, byte value)
    {
        _instance.WriteUInt8(tag, value);
    }

    public void WriteUInt16(string tag, ushort value)
    {
        _instance.WriteUInt16(tag, value);
    }

    public void WriteUInt32(string tag, uint value)
    {
        _instance.WriteUInt32(tag, value);
    }

    public void WriteUInt64(string tag, ulong value)
    {
        _instance.WriteUInt64(tag, value);
    }

    public void WriteFloat(string tag, float value)
    {
        _instance.WriteFloat(tag, value);
    }

    public void WriteDouble(string tag, double value)
    {
        _instance.WriteDouble(tag, value);
    }

    public void WriteChar(string tag, sbyte value)
    {
        _instance.WriteChar(tag, value);
    }

    public void WriteWChar(string tag, char value)
    {
        _instance.WriteWChar(tag, value);
    }

    public void WriteString(string tag, string value)
    {
        _instance.WriteString(tag, value);
    }

    public void WriteWString(string tag, string value)
    {
        _instance.WriteWString(tag, value);
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





    public void UnregisterInstance()
    {
        _instance.UnregisterInstance();
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