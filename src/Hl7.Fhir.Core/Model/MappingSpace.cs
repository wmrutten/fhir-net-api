using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model
{
    public static class MappingSpaces
    {
        /// <summary>Initialize a list of mapping space definitions from the specified Xml file (mappingspaces.xml).</summary>
        /// <param name="filePath">The absolute filepath to the mapping definitions Xml file (mappingspaces.xml).</param>
        /// <returns>A list of <see cref="MappingSpace"/> definitions.</returns>
        // public static IReadOnlyList<MappingSpace> LoadXml(string filePath) // IReadOnlyList is not available in Net40 & Portable45 
        public static MappingSpaceList LoadXml(string filePath) // IReadOnlyList
        {
            MappingSpaceList result;
            using (var sr = new StreamReader(filePath))
            using (var reader = XmlReader.Create(sr))
            {
#if true
                // Use DataContractSerializer
                var ser = new DataContractSerializer(typeof(MappingSpaceList));
                result = (MappingSpaceList)ser.ReadObject(reader);
#else
                // Use XmlSerializer
                // var ser = new XmlSerializer(typeof(MappingSpaces));
                result = (MappingSpaces)ser.Deserialize(reader);
#endif
            }
            return result; // result.AsReadOnly();
        }
    }

    /// <summary>A list of <see cref="MappingSpace"/> definitions.</summary>
    [CollectionDataContract(Namespace = "", Name = "mappingSpaces", ItemName = "space")]
    [XmlRoot("mappingSpaces")]
    public sealed class MappingSpaceList : List<MappingSpace>
    {
    }

    /// <summary>A mapping space definition.</summary>
    [DataContract(Namespace = "", Name = "space")]
    [XmlRoot("space", IsNullable = false)]
    [XmlType("space")]
    public sealed class MappingSpace
    {
        // Serializable by DataContractSerializer, but not supported by XmlReader
        [DataMember(Name = "url", Order = 1)]
        [XmlIgnore]
        public Uri Url { get; set; }

        // Serializable hidden shadow property for XmlSerializer, not necessary for DataContractSerializer
        [IgnoreDataMember]
        [XmlElement("url")]
        // [Browsable(false)] // Not available in Portable45
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string UriString
        {
            get { return Url == null ? null : Url.OriginalString; }
            set { Url = value == null ? null : new Uri(value, UriKind.RelativeOrAbsolute); }
        }

        [DataMember(Name = "columnName", Order = 2)]
        [XmlElement("columnName")]
        public string ColumnName { get; set; }

        [DataMember(Name = "title", Order = 3)]
        [XmlElement("title")]
        public string Title { get; set; }

        [DataMember(Name = "id", Order = 4)]
        [XmlElement("id")]
        public string Id { get; set; }

        [DataMember(Name = "sort", Order = 5)]
        [XmlElement("sort")]
        public int Sort { get; set; }
    }
}
