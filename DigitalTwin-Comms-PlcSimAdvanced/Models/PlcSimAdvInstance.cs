using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Events;
using Siemens.Simatic.Simulation.Runtime;
using System.Data;

namespace DigitalTwin_Comms_PlcSimAdvanced.Models;

public class PlcSimAdvInstance
{
    private readonly IInstance _instance;
    private readonly SIPSuite4 _ipSuite;

    public PlcSimAdvInstance(string name, CommunicationInterfaceType interfaceType = CommunicationInterfaceType.Softbus, string ipAddress = "192.168.0.1", string subnetMask = "255.255.255.0", string gateway = "0.0.0.0", uint interfaceId = 0, bool sendSyncEventInDefaultModeEnabled = true)
    {
        Name = name;
        IpAddress = ipAddress;
        SubnetMask = subnetMask;
        Gateway = gateway;
        InterfaceId = interfaceId;

        _ipSuite = new SIPSuite4(ipAddress, subnetMask, gateway);

        //Register instance
        _instance = SimulationRuntimeManager.RegisterInstance(Name);

        //Setup
        _instance.IsSendSyncEventInDefaultModeEnabled = sendSyncEventInDefaultModeEnabled;

        if (interfaceType == CommunicationInterfaceType.None)
            _instance.CommunicationInterface = ECommunicationInterface.None;
        else if (interfaceType == CommunicationInterfaceType.Softbus)
            _instance.CommunicationInterface = ECommunicationInterface.Softbus;
        else if (interfaceType == CommunicationInterfaceType.TCPIP)
            _instance.CommunicationInterface = ECommunicationInterface.TCPIP;

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


    public event EventHandler<PlcSimAdvAlarmNotificationDoneEventArgs>? AlarmNotificationDone;
    public event EventHandler<PlcSimAdvDataRecordReadEventArgs>? DataRecordRead;
    public event EventHandler<PlcSimAdvDataRecordWriteEventArgs>? DataRecordWrite;
    public event EventHandler<PlcSimAdvHardwareConfigurationChangedEventArgs>? HardwareConfigurationChanged;
    public event EventHandler<PlcSimAdvIpAddressChangedEventArgs>? IpAddressChanged;
    public event EventHandler<PlcSimAdvLedChangedEventArgs>? LedChanged;
    public event EventHandler<PlcSimAdvOperatingStateChangedEventArgs>? OperatingStateChanged;
    public event EventHandler<PlcSimAdvProcessEventDoneEventArgs>? ProcessEventDone;
    public event EventHandler<PlcSimAdvProfileEventDoneEventArgs>? ProfileEventDone;
    public event EventHandler<PlcSimAdvPullOrPlugEventDoneEventArgs>? PullOrPlugEventDone;
    public event EventHandler<PlcSimAdvRackOrStationFaultEventArgs>? RackOrStationFault;
    public event EventHandler<PlcSimAdvSoftwareConfigurationChangedEventArgs>? SoftwareConfigurationChanged;
    public event EventHandler<PlcSimAdvStatusEventDoneEventArgs>? StatusEventDone;
    public event EventHandler<PlcSimAdvSyncPointReachedEventArgs>? SyncPointReached;
    public event EventHandler<PlcSimAdvUpdateEventDoneEventArgs>? UpdateEventDone;



    public string Name { get; }
    public bool IsInitialized { get; set; }
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
        _instance.Stop(timeOut);
        _instance.PowerOff(timeOut);
    }

    public void Run(uint timeOut = 60000)
    {
        _instance.Run(timeOut);
        if(!IsInitialized)
        {
            //_instance.UpdateTagList();
        }
        IsInitialized = true;
    }

    public void Stop(uint timeOut = 60000)
    {
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



    private void OnAlarmNotificationDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, uint in_SequenceNumber)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        AlarmNotificationDone?.Invoke(in_Sender, new PlcSimAdvAlarmNotificationDoneEventArgs(errorCode, in_SystemTime, in_HardwareIdentifier, in_SequenceNumber));
    }

    private void OnDataRecordRead(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, SDataRecordInfo in_DataRecordInfo)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        DataRecordRead?.Invoke(in_Sender, new PlcSimAdvDataRecordReadEventArgs(errorCode, in_DateTime, in_DataRecordInfo.HardwareId, in_DataRecordInfo.RecordIdx, in_DataRecordInfo.DataSize));
    }

    private void OnDataRecordWrite(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, SDataRecord in_DataRecord)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        DataRecordWrite?.Invoke(in_Sender, new PlcSimAdvDataRecordWriteEventArgs(errorCode, in_DateTime, in_DataRecord.Info.HardwareId, in_DataRecord.Info.RecordIdx, in_DataRecord.Info.DataSize, in_DataRecord.Data));
    }
    private void OnHardwareConfigChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        HardwareConfigurationChanged?.Invoke(in_Sender, new PlcSimAdvHardwareConfigurationChangedEventArgs(errorCode, in_DateTime));
    }
    private void OnIpAddressChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, byte in_InterfaceId, SIPSuite4 in_SIP)
    {
        throw new NotImplementedException();
    }
    private void OnLedChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, ELEDType in_LEDType, ELEDMode in_LEDMode)
    {
        throw new NotImplementedException();
    }

    private void OnOperatingStateChanged(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, EOperatingState in_PrevState, EOperatingState in_OperatingState)
    {
        throw new NotImplementedException();
    }
    private void OnProcessEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, uint in_Channel, EProcessEventType in_ProcessEventType, uint in_SequenceNumber)
    {
        throw new NotImplementedException();
    }
    private void OnProfileEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        throw new NotImplementedException();
    }
    private void OnPullOrPlugEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, EPullOrPlugEventType in_PullOrPlugEventType, uint in_SequenceNumber)
    {
        throw new NotImplementedException();
    }

    private void OnRackOrStationFaultEvent(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier, ERackOrStationFaultType in_EventType)
    {
        throw new NotImplementedException();
    }
    private void OnSoftwareConfigurationChanged(IInstance instance, SOnSoftwareConfigChangedParameter event_param)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(event_param.ErrorCode);
        PlcSimAdvSoftwareConfigChanged configChanged = event_param.ChangeType == ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN ? PlcSimAdvSoftwareConfigChanged.SoftwareChangedInRun : PlcSimAdvSoftwareConfigChanged.SoftwareChangedInStop;
        SoftwareConfigurationChanged?.Invoke(instance, new PlcSimAdvSoftwareConfigurationChangedEventArgs(errorCode, event_param.EventCreateTime, configChanged));

        IsInitialized = false;

        if (configChanged == PlcSimAdvSoftwareConfigChanged.SoftwareChangedInStop)
        {
            try
            {
                _instance.UpdateTagList();
            }
            catch (Exception ex)
            {
                return;
            }
            IsInitialized = true;
        }
    }
    private void OnStatusEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        throw new NotImplementedException();
    }

    private void OnSyncPointReached(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_DateTime, uint in_Id, long in_TimeSinceSameSyncPoint_ns, long in_TimeSinceAnySyncPoint_ns, uint in_SyncPointCount)
    {
        PlcSimAdvErrorCode.PlcSimAdvErrorCodeType errorCode = PlcSimAdvErrorCode.PlcSimAdvConvertErrorCode(in_ErrorCode);
        SyncPointReached?.Invoke(in_Sender, new PlcSimAdvSyncPointReachedEventArgs(errorCode, in_DateTime, in_Id, in_TimeSinceSameSyncPoint_ns, in_TimeSinceAnySyncPoint_ns, in_SyncPointCount));
    }
    private void OnUpdateEventDone(IInstance in_Sender, ERuntimeErrorCode in_ErrorCode, DateTime in_SystemTime, uint in_HardwareIdentifier)
    {
        throw new NotImplementedException();
    }






    

}