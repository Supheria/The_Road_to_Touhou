﻿using FormatRawEffectSentence.LocalSign;
using FormatRawEffectSentence.Model;
using LocalUtilities.SerializeUtilities;
using LocalUtilities.StringUtilities;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FormatRawEffectSentence.IO;

[XmlRoot(nameof(EffectSentence))]
public class EffectSentenceXmlSerialization : XmlSerialization<EffectSentence>
{
    protected const string LocalNameType = "Type";
    protected const string LocalNameValue = "Value";

    public EffectSentenceXmlSerialization() : base(nameof(EffectSentence))
    {
    }

    public override void ReadXml(XmlReader reader)
    {
        var motion = reader.GetAttribute(nameof(Source.Motion)).ToEnum<Motions>();
        var typePair = reader.GetAttribute(LocalNameType);
        var valuePair = reader.GetAttribute(LocalNameValue);
        var (valueType, triggerType) = typePair.ToPair(Types.None, Types.None, StringSimpleTypeConverter.ToEnum<Types>,
            StringSimpleTypeConverter.ToEnum<Types>);
        var (value, triggers) = valuePair.ToPair("", Array.Empty<string>(), str => str, StringSimpleTypeConverter.ToArray);
        Source = new(motion, valueType, value, triggerType, triggers);

        Source.SubSentences.ReadXmlCollection(reader, LocalRootName, new EffectSentenceXmlSerialization());
    }

    public override void WriteXml(XmlWriter writer)
    {
        if (Source is null)
            return;
        writer.WriteAttributeString(nameof(Source.Motion), Source.Motion.ToString());
        writer.WriteAttributeString(LocalNameType,
            StringSimpleTypeConverter.ToArrayString(Source.Type, Source.TriggerType));
        writer.WriteAttributeString(LocalNameValue,
            StringSimpleTypeConverter.ToArrayString(Source.Value, Source.Triggers.ToArrayString()));

        Source.SubSentences.WriteXmlCollection(writer, new EffectSentenceXmlSerialization());
    }
}