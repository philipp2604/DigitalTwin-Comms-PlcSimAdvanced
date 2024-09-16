using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Events;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Interfaces.Models;

public interface IPlcSimAdvInstance : IDisposable
{
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

    public void PowerOn(uint timeOut = 60000);

    public void PowerOff(uint timeOut = 60000);

    public void Run(uint timeOut = 60000);

    public void Stop(uint timeOut = 60000);

    public void UnregisterInstance();

    public bool ReadBool(string tag);

    public void WriteBool(string tag, bool value);
}