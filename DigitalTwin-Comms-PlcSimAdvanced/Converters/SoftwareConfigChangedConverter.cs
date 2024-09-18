using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class SoftwareConfigChangedConverter
{
    public static SoftwareConfigChanged ConvertSoftwareConfigChangedType(ESoftwareConfigChanged configChanged)
    {
        return configChanged switch
        {
            ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP => SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP,
            ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN => SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN,
            _ => throw new NotImplementedException(),
        };
    }

    public static ESoftwareConfigChanged ConvertSoftwareConfigChangedType(SoftwareConfigChanged configChanged)
    {
        return configChanged switch
        {
            SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP => ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP,
            SoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN => ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN,
            _ => throw new NotImplementedException(),
        };
    }
}