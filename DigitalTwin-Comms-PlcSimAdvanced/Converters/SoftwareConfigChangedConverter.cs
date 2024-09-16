using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;
public static class SoftwareConfigChangedConverter
{
    public static SoftwareConfigChanged ConvertSoftwareConfigChangedType(ESoftwareConfigChanged configChanged)
    {
        return configChanged switch
        {
            ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP => SoftwareConfigChanged.SoftwareChangedInStop,
            ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN => SoftwareConfigChanged.SoftwareChangedInRun,
            _ => throw new NotImplementedException(),
        };
    }

    public static ESoftwareConfigChanged ConvertSoftwareConfigChangedType(SoftwareConfigChanged configChanged)
    {
        return configChanged switch
        {
            SoftwareConfigChanged.SoftwareChangedInStop => ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_STOP,
            SoftwareConfigChanged.SoftwareChangedInRun => ESoftwareConfigChanged.SRSCC_SOFTWARE_CHANGED_IN_RUN,
            _ => throw new NotImplementedException(),
        };
    }
}