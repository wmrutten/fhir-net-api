using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model
{
    [DataContract, XmlRoot("mappingSpaces")]
    public class MappingSpaces
    {
        [XmlElement("space")]
        public List<MappingSpace> Spaces { get; set; } = new List<MappingSpace>();
    }

    //[DataContract, XmlRoot("mappingSpaces")]
    //public class MappingSpaces
    //{
    //    [DataMember, XmlElement("space")]
    //    public MappingSpaceList Spaces { get; set; } = new MappingSpaceList();
    //}

    //[CollectionDataContract(ItemName = "space")]
    //public class MappingSpaceList : List<MappingSpace> { }


    [DataContract, XmlRoot("space", IsNullable = false)]
    public class MappingSpace
    {
        [IgnoreDataMember, XmlIgnore]
        public Uri Url { get; set; }

        // Serializable hidden shadow property
        [DataMember, XmlElement("url")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string UriString
        {
            get { return Url == null ? null : Url.OriginalString; }
            set { Url = value == null ? null : new Uri(value, UriKind.RelativeOrAbsolute); }
        }

        [DataMember, XmlElement("columnName")]
        public string ColumnName { get; set; }

        [DataMember, XmlElement("title")]
        public string Title { get; set; }

        [DataMember, XmlElement("id")]
        public string Id { get; set; }

        [DataMember, XmlElement("sort")]
        public int sort { get; set; }
    }
}
