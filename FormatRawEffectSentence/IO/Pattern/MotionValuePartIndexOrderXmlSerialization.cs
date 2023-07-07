﻿using LocalUtilities.GeneralSerialization;
using LocalUtilities.Interface;
using LocalUtilities.XmlUtilities;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FormatRawEffectSentence.IO.Pattern;

[XmlRoot("PartIndex")]
public class MotionValuePartIndexOrderXmlSerialization : Serialization<int>, IXmlSerialization<int>
{
    public MotionValuePartIndexOrderXmlSerialization() : base("PartIndex")
    {
    }

    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        reader.Read();
        var index = XmlReadTool.GetIntValue(reader.Value);
        if (index is not null)
            Source = (int)index;
    }

    public void WriteXml(XmlWriter writer) => writer.WriteValue(Source);
}