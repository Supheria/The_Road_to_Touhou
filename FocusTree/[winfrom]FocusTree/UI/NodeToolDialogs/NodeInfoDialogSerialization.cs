﻿using LocalUtilities.SerializeUtilities;
using System.Xml;
using System.Xml.Serialization;

namespace FocusTree.UI.NodeToolDialogs;

public class NodeInfoDialogSerialization() : XmlSerialization<NodeInfoDialog>(new())
{
    public override string LocalName => nameof(NodeInfoDialog);

    public override void ReadXml(XmlReader reader)
    {
        Source = new()
        {
            FocusNameText = reader.GetAttribute(nameof(Source.FocusNameText)) ?? "",
            DurationText = reader.GetAttribute(nameof(Source.DurationText)) ?? "",
            DescriptText = reader.GetAttribute(nameof(Source.DescriptText)) ?? "",
            EffectsText = reader.GetAttribute(nameof(Source.EffectsText)) ?? "",
        };
    }

    public override void WriteXml(XmlWriter writer)
    {
        if (Source is null)
            return;
        writer.WriteAttributeString(nameof(Source.FocusNameText), Source.FocusNameText);
        writer.WriteAttributeString(nameof(Source.DurationText), Source.DurationText);
        writer.WriteAttributeString(nameof(Source.DescriptText), Source.DescriptText);
        writer.WriteAttributeString(nameof(Source.EffectsText), Source.EffectsText);
    }
}
