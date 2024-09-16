namespace DigitalTwin_Comms_PlcSimAdvanced.Constants;

public enum LedType
{
    Stop,
    Run,
    Error,
    Maintenance,
    Redundancy,
    Force,
    BusFault1,
    BusFault2,
    BusFault3,
    BusFault4
}

public enum LedMode
{
    Off,
    On,
    FlashFast,
    FlashSlow,
    Invalid
}