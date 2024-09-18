using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Events;

namespace DigitalTwin_Comms_PlcSimAdvanced.Interfaces.Models;

/// <summary>
/// An interface used for communication with PLCSim Advanced instances.
/// </summary>
public interface IPlcSimAdvInstance : IDisposable
{
    #region Events

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

    #endregion Events

    #region Properties

    /// <summary>
    /// Gets the id of the communication interface.
    /// </summary>
    public uint InterfaceId { get; }

    /// <summary>
    /// Gets wheter the tag list is initialized.
    /// </summary>
    public bool IsInitialized { get; }

    /// <summary>
    /// Gets or sets if the <see cref="SyncPointReached"/> event is invoked in every mode after every cycle.
    /// </summary>
    public bool IsSendSyncEventInDefaultModeEnabled { get; set; }

    /// <summary>
    /// Gets the instance's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the current operating state.
    /// </summary>
    public OperatingState OperatingState { get; }

    /// <summary>
    /// Gets or sets the time scale factor.
    /// </summary>
    public double ScaleFactor { get; set; }

    /// <summary>
    /// Gets or sets the instance's storage path.
    /// </summary>
    public string StoragePath { get; set; }

    #endregion Properties

    #region Virtual memory card functions

    /// <summary>
    /// Saves the user program, hardware configuration and remanent data in a virtual memory card.
    /// </summary>
    /// <param name="fileName">Path and file name of the virtual memory card.</param>
    public void ArchiveStorage(string fileName);

    /// <summary>
    /// Deletes the folder that stores the virtual memory card.
    /// </summary>
    public void CleanupStoragePath();

    /// <summary>
    /// Retrieves saved data from a virtual memory card.
    /// </summary>
    /// <param name="fileName">Path and filename of the virtual memory card.</param>
    public void RetrieveStorage(string fileName);

    #endregion Virtual memory card functions

    #region Operating state functions

    /// <summary>
    /// Creates the process for the instance and starts the booting process.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    /// <returns>An <see cref="ErrorCode"/> value.</returns>
    public ErrorCode PowerOn(uint timeOut = 60000);

    /// <summary>
    /// Ends the simulation and it's process.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    public void PowerOff(uint timeOut = 60000);

    /// <summary>
    /// Requests the virtual controller to change into RUN mode.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    public void Run(uint timeOut = 60000);

    /// <summary>
    /// Requests the virtual controller to change into STOP mode.
    /// </summary>
    /// <param name="timeOut">A timeout value in milliseconds.</param>
    public void Stop(uint timeOut = 60000);

    /// <summary>
    /// Powers off the virtual controller, ends it's process and restarts it. Resets the memory inbetween.
    /// </summary>
    public void MemoryReset();

    #endregion Operating state functions

    #region Variable tables

    /// <summary>
    /// Exports all entries from the variable tables into one xml file.
    /// </summary>
    /// <param name="fileName">Path and file name of the xml file.</param>
    public void CreateConfigurationFile(string fileName);

    /// <summary>
    /// Reads the variables from the virtual controller and stores them into the shared memory.
    /// </summary>
    /// <param name="tagListDetails">A values of <see cref="TagListDetails"/> specifying which variables shall be read.</param>
    /// <param name="isHMIVisibleOnly">Determines, if only variables shall be read, that are marked as 'HMI Visible'.</param>
    /// <param name="dataBlockFilterList">A string to filter variables by the names of data blocks.</param>
    public void UpdateTagList(TagListDetails tagListDetails = TagListDetails.IOMCTDB, bool isHMIVisibleOnly = true, string? dataBlockFilterList = null);

    #endregion Variable tables

    #region Variable access functions using tag names

    /// <summary>
    /// Reads the bool value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The bool value of the tag.</returns>
    public bool ReadBool(string tag);

    /// <summary>
    /// Reads the Int8 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int8 value of the tag.</returns>
    public sbyte ReadInt8(string tag);

    /// <summary>
    /// Reads the Int16 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int16 value of the tag.</returns>
    public short ReadInt16(string tag);

    /// <summary>
    /// Reads the Int32 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int32 value of the tag.</returns>
    public int ReadInt32(string tag);

    /// <summary>
    /// Reads the Int64 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The Int64 value of the tag.</returns>
    public long ReadInt64(string tag);

    /// <summary>
    /// Reads the UInt8 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The UInt8 value of the tag.</returns>
    public byte ReadUInt8(string tag);

    /// <summary>
    /// Reads the UInt16 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The UInt16 value of the tag.</returns>
    public ushort ReadUInt16(string tag);

    /// <summary>
    /// Reads the UInt32 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The UInt32 value of the tag.</returns>
    public uint ReadUInt32(string tag);

    /// <summary>
    /// Reads the UInt64 value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The UInt64 value of the tag.</returns>
    public ulong ReadUInt64(string tag);

    /// <summary>
    /// Reads the float value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The float value of the tag.</returns>
    public float ReadFloat(string tag);

    /// <summary>
    /// Reads the double value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The double value of the tag.</returns>
    public double ReadDouble(string tag);

    /// <summary>
    /// Reads the char value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The char value of the tag.</returns>
    public sbyte ReadChar(string tag);

    /// <summary>
    /// Reads the wchar value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The wchar value of the tag.</returns>
    public char ReadWChar(string tag);

    /// <summary>
    /// Reads the string value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The string value of the tag.</returns>
    public string ReadString(string tag);

    /// <summary>
    /// Reads the wstring value of a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <returns>The wstring value of the tag.</returns>
    public string ReadWString(string tag);

    /// <summary>
    /// Writes a bool value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The bool value to write to the tag.</param>
    public void WriteBool(string tag, bool value);

    /// <summary>
    /// Writes an Int8 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The Int8 value to write to the tag.</param>
    public void WriteInt8(string tag, sbyte value);

    /// <summary>
    /// Writes an Int16 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The Int16 value to write to the tag.</param>
    public void WriteInt16(string tag, short value);

    /// <summary>
    /// Writes an Int32 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The Int32 value to write to the tag.</param>
    public void WriteInt32(string tag, int value);

    /// <summary>
    /// Writes an Int64 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The Int64 value to write to the tag.</param>
    public void WriteInt64(string tag, long value);

    /// <summary>
    /// Writes an UInt8 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The UInt8 value to write to the tag.</param>
    public void WriteUInt8(string tag, byte value);

    /// <summary>
    /// Writes an UInt16 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The UInt16 value to write to the tag.</param>
    public void WriteUInt16(string tag, ushort value);

    /// <summary>
    /// Writes an UInt32 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The UInt32 value to write to the tag.</param>
    public void WriteUInt32(string tag, uint value);

    /// <summary>
    /// Writes an UInt64 value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The UInt64 value to write to the tag.</param>
    public void WriteUInt64(string tag, ulong value);

    /// <summary>
    /// Writes a float value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The float value to write to the tag.</param>
    public void WriteFloat(string tag, float value);

    /// <summary>
    /// Writes a double value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The double value to write to the tag.</param>
    public void WriteDouble(string tag, double value);

    /// <summary>
    /// Writes a char value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The char value to write to the tag.</param>
    public void WriteChar(string tag, sbyte value);

    /// <summary>
    /// Writes a wchar value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The wchar value to write to the tag.</param>
    public void WriteWChar(string tag, char value);

    /// <summary>
    /// Writes a string value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The string value to write to the tag.</param>
    public void WriteString(string tag, string value);

    /// <summary>
    /// Writes a wstring value to a tag.
    /// </summary>
    /// <param name="tag">Name of the tag.</param>
    /// <param name="value">The wstring value to write to the tag.</param>
    public void WriteWString(string tag, string value);

    #endregion Variable access functions using tag names

    #region API interface functions

    /// <summary>
    /// Unregisters the IInstance from the manager.
    /// </summary>
    public void UnregisterInstance();

    #endregion API interface functions
}