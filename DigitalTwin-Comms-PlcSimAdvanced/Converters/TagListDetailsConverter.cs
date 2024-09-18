using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;

public static class TagListDetailsConverter
{
    public static TagListDetails ConvertTagListDetailsType(ETagListDetails tagListDetails)
    {
        return tagListDetails switch
        {
            ETagListDetails.None => TagListDetails.None,
            ETagListDetails.IO => TagListDetails.IO,
            ETagListDetails.M => TagListDetails.M,
            ETagListDetails.IOM => TagListDetails.IOM,
            ETagListDetails.CT => TagListDetails.CT,
            ETagListDetails.IOCT => TagListDetails.IOCT,
            ETagListDetails.MCT => TagListDetails.MCT,
            ETagListDetails.IOMCT => TagListDetails.IOMCT,
            ETagListDetails.DB => TagListDetails.DB,
            ETagListDetails.IODB => TagListDetails.IODB,
            ETagListDetails.MDB => TagListDetails.MDB,
            ETagListDetails.IOMDB => TagListDetails.IOMDB,
            ETagListDetails.CTDB => TagListDetails.CTDB,
            ETagListDetails.IOCTDB => TagListDetails.IOCTDB,
            ETagListDetails.MCTDB => TagListDetails.MCTDB,
            ETagListDetails.IOMCTDB => TagListDetails.IOMCTDB,
            _ => throw new NotImplementedException(),
        };
    }

    public static ETagListDetails ConvertTagListDetailsType(TagListDetails tagListDetails)
    {
        return tagListDetails switch
        {
            TagListDetails.None => ETagListDetails.None,
            TagListDetails.IO => ETagListDetails.IO,
            TagListDetails.M => ETagListDetails.M,
            TagListDetails.IOM => ETagListDetails.IOM,
            TagListDetails.CT => ETagListDetails.CT,
            TagListDetails.IOCT => ETagListDetails.IOCT,
            TagListDetails.MCT => ETagListDetails.MCT,
            TagListDetails.IOMCT => ETagListDetails.IOMCT,
            TagListDetails.DB => ETagListDetails.DB,
            TagListDetails.IODB => ETagListDetails.IODB,
            TagListDetails.MDB => ETagListDetails.MDB,
            TagListDetails.IOMDB => ETagListDetails.IOMDB,
            TagListDetails.CTDB => ETagListDetails.CTDB,
            TagListDetails.IOCTDB => ETagListDetails.IOCTDB,
            TagListDetails.MCTDB => ETagListDetails.MCTDB,
            TagListDetails.IOMCTDB => ETagListDetails.IOMCTDB,
            _ => throw new NotImplementedException(),
        };
    }
}