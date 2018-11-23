using System.Xml.Serialization;

namespace XmlSerializeManager.Model
{
    /* Example ------------------------------------------ [Serializable] or */
    [XmlRoot("config")]
    public class Configuration
    {
        [XmlArray]
        public ServerInfo[] Servers { get; set; }
    }

    public class ServerInfo
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlElement]
        public string baseAddress { get; set; }
        [XmlAttribute]
        public ServerType serverType { get; set; }
        [XmlElement]
        public Identity identity { get; set; }
    }

    public class Identity
    {
        [XmlElement]
        public string UserName { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
    }


    public enum ServerType
    {
        [XmlEnum(Name = "Database")]
        db,
        [XmlEnum(Name = "Application")]
        app,
        [XmlEnum(Name = "Proxy")]
        proxy,
    }
}
