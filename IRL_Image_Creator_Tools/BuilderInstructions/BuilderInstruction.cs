using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderInstructions
{
    [XmlInclude(typeof(TextInstruction))]
    [XmlInclude(typeof(FontInstrucion))]
    [Serializable]
    public abstract class BuilderInstruction
    {
        [XmlElement]
        public string Name { get; set; } = "";
    }
}
