using System.Xml.Serialization;
    
namespace QuotesSong
{
/// <summary>
/// Class-helper for serialization of dictionary
/// </summary>
    public class item
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public string value;
    }
}
