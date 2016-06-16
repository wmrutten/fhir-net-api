using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace Hl7.Fhir.Model
{
    [TestClass]
    public class MappingSpacesTest
    {
        const string mappingSpacesXml = @"TestData\mappingSpaces.xml";

        static readonly XmlWriterSettings exampleXmlWriterSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            OmitXmlDeclaration = true,
            Indent = false
        };

        static readonly MappingSpace exampleMappingSpace = new MappingSpace()
        {
            Url = new Uri(@"http://hl7.org/v3"),
            ColumnName = "RIM Mapping",
            Title = "RIM",
            Id = "rim",
            Sort = 1
        };

        const string exampleMappingSpaceXml = @"<space><url>http://hl7.org/v3</url><columnName>RIM Mapping</columnName><title>RIM</title><id>rim</id><sort>1</sort></space>";

        static readonly MappingSpaceList exampleMappingSpaces = new MappingSpaceList() { exampleMappingSpace };

        const string exampleMappingSpacesXml = @"<mappingSpaces>" + exampleMappingSpaceXml + "</mappingSpaces>";

        const string xmlSchemaInstanceNamespace = " xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        string XmlSerialize<T>(T data)
        {
            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, exampleXmlWriterSettings))
            {
                // Suppress default xsd/xsi namespace declarations
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(writer, data, ns);
                return sw.ToString();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        string DataContractSerialize<T>(T data)
        {
            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, exampleXmlWriterSettings))
            {
                var settings = new DataContractSerializerSettings()
                {
                    SerializeReadOnlyTypes = true
                };
                var ser = new DataContractSerializer(typeof(T), settings);
                ser.WriteObject(writer, data);

                writer.Flush();
                var result = sw.ToString();

                // DataContractSerializer always emits XMLSchema namespace, cannot suppress...
                // xmlns:i="http://www.w3.org/2001/XMLSchema-instance"
                return result.Replace(xmlSchemaInstanceNamespace, string.Empty);
            }
        }

        [TestMethod]
        public void TestMappingSpaceXmlSerializer()
        {
            string actual = XmlSerialize(exampleMappingSpace);
            Console.WriteLine(actual);
            Assert.AreEqual(exampleMappingSpaceXml, actual);
        }

        [TestMethod]
        public void TestMappingSpaceDataContractSerializer()
        {
            string actual = DataContractSerialize(exampleMappingSpace);
            Console.WriteLine(actual);
            Assert.AreEqual(exampleMappingSpaceXml, actual);
        }

        [TestMethod]
        public void TestMappingSpacesXmlSerializer()
        {
            string actual = XmlSerialize(exampleMappingSpaces);
            Console.WriteLine(actual);
            Assert.AreEqual(exampleMappingSpacesXml, actual);
        }

        [TestMethod]
        public void TestMappingSpacesDataContractSerializer()
        {
            string actual = DataContractSerialize(exampleMappingSpaces);
            Console.WriteLine(actual);
            Assert.AreEqual(exampleMappingSpacesXml, actual);
        }

        // private static void AssertMappingSpaces(IReadOnlyList<MappingSpace> mappingSpaces)
        private static void AssertMappingSpaces(ICollection<MappingSpace> mappingSpaces)
        {
            Assert.IsNotNull(mappingSpaces);
            Assert.AreEqual(mappingSpaces.Count, 30);
            foreach (var space in mappingSpaces.OrderBy(space => space.Sort))
            {
                Console.WriteLine($"{space.Sort,3} | {space.Id} | {space.Title} | {space.ColumnName} | {space.Url}");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        void TestMappingSpacesDeserializer(Func<XmlReader, MappingSpaceList> deserialize)
        {
            MappingSpaceList mappingSpaces;
            using (var sr = new StreamReader(mappingSpacesXml))
            using (var reader = XmlReader.Create(sr))
            {
                mappingSpaces = deserialize(reader);
            }
            AssertMappingSpaces(mappingSpaces);
        }

        [TestMethod]
        public void TestMappingSpacesXmlDeserializer()
        {
            TestMappingSpacesDeserializer(
                reader => 
                {
                    var ser = new XmlSerializer(typeof(MappingSpaceList));
                    return (MappingSpaceList)ser.Deserialize(reader);
                }
            );
        }

        [TestMethod]
        public void TestMappingSpacesDataContractDeserializer()
        {
            TestMappingSpacesDeserializer(
                reader =>
                {
                    var ser = new DataContractSerializer(typeof(MappingSpaceList));
                    return (MappingSpaceList)ser.ReadObject(reader);
                }
            );
        }

        [TestMethod]
        public void TestLoadMappingSpaces()
        {
            var mappingSpaces = MappingSpaces.LoadXml(mappingSpacesXml);
            AssertMappingSpaces(mappingSpaces);
        }

    }
}