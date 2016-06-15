using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    [TestClass]
    public class MappingSpacesTest
    {
        const string mappingSpacesXml = @"TestData\mappingSpaces.xml";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        [TestMethod]
        public void TestSerializeMappingSpace()
        {
            var mappingSpace = new MappingSpace()
            {
                Url = new Uri(@"http://hl7.org/v3"),
                ColumnName = "RIM Mapping",
                Title = "RIM",
                Id = "rim",
                sort = 1
            };

            const string expected = @"<space><url>http://hl7.org/v3</url><columnName>RIM Mapping</columnName><title>RIM</title><id>rim</id><sort>1</sort></space>";

            XmlWriterSettings xws = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                Indent = false
            };

            string actual;
            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, xws))
            {
                // ser.Serialize(writer, mappingSpace);

                // Suppress default xsd/xsi namespace declarations
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                var ser = new XmlSerializer(typeof(MappingSpace));
                ser.Serialize(writer, mappingSpace, ns);

                actual = sw.ToString();
            }

            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        [TestMethod]
        public void TestSerializeMappingSpaces()
        {
            var mappingSpaces = new MappingSpaces()
            {
                Spaces = {
                    new MappingSpace()
                    {
                        Url = new Uri(@"http://hl7.org/v3"),
                        ColumnName = "RIM Mapping",
                        Title = "RIM",
                        Id = "rim",
                        sort = 1
                    }
                }
            };

            //var mappingSpaces = new MappingSpaces()
            //{
            //    new MappingSpace()
            //    {
            //        Url = new Uri(@"http://hl7.org/v3"),
            //        ColumnName = "RIM Mapping",
            //        Title = "RIM",
            //        Id = "rim",
            //        sort = 1
            //    }
            //};

            const string expected = @"<mappingSpaces><space><url>http://hl7.org/v3</url><columnName>RIM Mapping</columnName><title>RIM</title><id>rim</id><sort>1</sort></space></mappingSpaces>";

            XmlWriterSettings xws = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                Indent = false
            };

            string actual;
            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, xws))
            {
                // ser.Serialize(writer, mappingSpace);

                // Suppress default xsd/xsi namespace declarations
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                var ser = new XmlSerializer(typeof(MappingSpaces));
                ser.Serialize(writer, mappingSpaces, ns);

                actual = sw.ToString();
            }

            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
    }
}