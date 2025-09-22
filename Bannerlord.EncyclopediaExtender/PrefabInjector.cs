using Bannerlord.UIExtenderEx.ResourceManager;
using System.IO;
using System.Xml;
using TaleWorlds.Engine.GauntletUI;

namespace Bannerlord.EncyclopediaExtender;

public static class PrefabInjector
{
    public static void RegisterHeroItemTuple()
    {
        var doc = LoadVanillaGuiPrefabXml("SandBox", "Inventory", "InventoryItemTuple");

        var inventoryItemTupleWidget = doc
            .SelectSingleNode("/Prefab/Window/InventoryItemTupleWidget");
        var inventoryTupleExtensionControlsWidget = inventoryItemTupleWidget
            .SelectSingleNode("Children/InventoryTupleExtensionControlsWidget");

        RenameNode(doc, inventoryTupleExtensionControlsWidget, "Widget");
        RenameNode(doc, inventoryItemTupleWidget, "ListPanel");

        var transferButtonElement = doc
            .SelectSingleNode("//InventoryTransferButtonWidget[@Id='TransferButton']");
        transferButtonElement.Attributes["IsVisible"].Value = "false";

        var commentNodes = doc.SelectNodes("//comment()");
        foreach (XmlNode item in commentNodes)
        {
            item.ParentNode.RemoveChild(item);
        }

        WidgetFactoryManager.CreateAndRegister(
            "HeroItemTuple",
            doc);
    }

    public static void RegisterSettlementProducedItem()
    {
        string textureAttribute;
#if LOWER_THAN_1_3
        textureAttribute = @"ImageTypeCode=""@ImageTypeCode""";
#else
        textureAttribute = @"TextureProviderName=""@TextureProviderName""";
#endif

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        var xml = $@"
<Prefab>
  <Constants>
    <Constant Name=""Encyclopedia.SubPage.Element.Width"" BrushLayer=""Default"" BrushName=""Encyclopedia.SubPage.Element"" BrushValueType=""Width"" />
    <Constant Name=""Encyclopedia.SubPage.Element.Height"" BrushLayer=""Default"" BrushName=""Encyclopedia.SubPage.Element"" BrushValueType=""Height"" />
  </Constants>
  <Window>
    <ButtonWidget DoNotPassEventsToChildren=""true"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""!Encyclopedia.SubPage.Element.Width"" SuggestedHeight=""!Encyclopedia.SubPage.Element.Height"" HorizontalAlignment=""Center"" Command.Click=""ExecuteLink"">
      <Children>

        <Widget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"">
          <Children>
            <BrushWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Brush=""Encyclopedia.SubPage.Element""  />
            <InventoryImageIdentifierWidget Id=""ImageIdentifier"" {textureAttribute} DoNotAcceptEvents=""true"" DataSource=""{{ImageIdentifier}}"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""111"" SuggestedHeight=""51"" MarginTop=""2"" MarginBottom=""2"" ImageId=""@Id"" AdditionalArgs=""@AdditionalArgs"" LoadingIconWidget=""LoadingIconWidget""  HorizontalAlignment=""Center"" VerticalAlignment=""Bottom"">
              <Children>
                <Widget DoNotAcceptEvents=""true"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Sprite=""Inventory\tuple_shadow"" AlphaFactor=""0.7""/>
                <Standard.CircleLoadingWidget HorizontalAlignment=""Center"" VerticalAlignment=""Center"" Id=""LoadingIconWidget""/>
              </Children>
            </InventoryImageIdentifierWidget>
          </Children>
        </Widget>

        <TextWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""Fixed"" SuggestedHeight=""30"" VerticalAlignment=""Bottom"" PositionYOffset=""34"" Brush=""Encyclopedia.SubPage.Element.Name.Text"" Brush.TextVerticalAlignment=""Top"" Text=""@ItemDescription"" />

        <HintWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Command.HoverBegin=""ExecuteBeginHint"" Command.HoverEnd=""ExecuteEndHint"" />
      </Children>
    </ButtonWidget>
  </Window>
</Prefab>";

        var doc = new XmlDocument();
        doc.LoadXml(xml);

        WidgetFactoryManager.CreateAndRegister(
            "SettlementProducedItem",
            doc);
    }

    private static XmlDocument LoadVanillaGuiPrefabXml(string module, string directory, string prefabName)
    {
        var basePath = TaleWorlds.Engine.Utilities.GetBasePath();
        var originalPrefabPath = Path.Combine(
            basePath,
            "Modules",
            module,
            "GUI",
            "Prefabs",
            directory,
            $"{prefabName}.xml");
        
        var doc = new XmlDocument();
        doc.Load(originalPrefabPath);

        return doc;
    }

    private static XmlNode RenameNode(XmlDocument doc, XmlNode oldNode, string newName)
    {
        // Create a new node with the new name
        XmlNode newNode = doc.CreateElement(newName);

        // Copy attributes
        if (oldNode.Attributes != null)
        {
            foreach (XmlAttribute attr in oldNode.Attributes)
            {
                XmlAttribute newAttr = doc.CreateAttribute(attr.Name);
                newAttr.Value = attr.Value;
                newNode.Attributes.Append(newAttr);
            }
        }

        // Copy children
        foreach (XmlNode child in oldNode.ChildNodes)
        {
            newNode.AppendChild(child.CloneNode(true));
        }

        // Replace old with new
        if (oldNode.ParentNode != null)
        {
            oldNode.ParentNode.ReplaceChild(newNode, oldNode);
        }

        return newNode;
    }
}
