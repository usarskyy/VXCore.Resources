using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

using NUnit.Framework;

using VXCore.Resources.Exceptions;
using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml.Tests
{
    [TestFixture]
    public class XResourceCollectionTests
    {
        [Test]
        public void TestNullResourceFileLoad()
        {
            try
            {
                new XResourceCollection(null);

                Assert.Fail("Constructor call has to fail");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf(typeof (ArgumentNullException), ex);
            }
        }

        [Test]
        public void TestLoadEmptyResourceFile()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.EmptyFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();
            
            Assert.IsTrue(xc.IsLoaded);
        }

        [Test]
        public void TestLoadFileWithComments()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.FileWithComments.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);
        }

        [Test]
        public void TestLoadSimpleResourceFile()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SimpleTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);
        }

        [Test]
        public void TestLoadSimpleResourceFileWithBoolValue()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.BoolTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            object result = xc.GetItem("boolResource");

            Assert.IsInstanceOf<bool>(result);
            Assert.IsTrue((bool) result);
        }

        [Test]
        public void TestLoadResourceFileWithDictionary()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.DictionaryFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            object result = xc.GetItem("dictionaryResource");

            Assert.IsInstanceOf<NameValueCollection>(result);

            NameValueCollection tmp = (NameValueCollection) result;

            Assert.AreEqual(3, tmp.Count);
            Assert.AreEqual(tmp["1"], "test");
            Assert.IsEmpty(tmp["2"]);
            Assert.AreEqual(tmp["3"], "test2");
        }

        [Test]
        public void TestLoadResourceFileWithEmptyDictionary()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.DictionaryFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            object result = xc.GetItem("emptyDictionary");

            Assert.IsInstanceOf<NameValueCollection>(result);

            NameValueCollection tmp = (NameValueCollection)result;

            Assert.AreEqual(0, tmp.Count);
        }

        [Test]
        public void TestMultiLevelSwitchWithIntegerParameter()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.BoolTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            bool pendingResult17 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                              {
                                                                                  {"ACTIVITY_STATUS", "PENDING"},
                                                                                  {"ACTIVITY_DEF_ID", "17"}
                                                                              });

            bool errorResult17 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                            {
                                                                                {"ACTIVITY_STATUS", "ERROR"},
                                                                                {"ACTIVITY_DEF_ID", "17"}
                                                                            });

            Assert.IsTrue(pendingResult17);
            Assert.IsFalse(errorResult17);

            bool pendingResult65 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                              {
                                                                                  {"ACTIVITY_STATUS", "PENDING"},
                                                                                  {"ACTIVITY_DEF_ID", "65"}
                                                                              });

            bool errorResult65 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                            {
                                                                                {"ACTIVITY_STATUS", "ERROR"},
                                                                                {"ACTIVITY_DEF_ID", "65"}
                                                                            });

            Assert.IsTrue(pendingResult65);
            Assert.IsTrue(errorResult65);

            bool pendingResult77 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                              {
                                                                                  {"ACTIVITY_STATUS", "PENDING"},
                                                                                  {"ACTIVITY_DEF_ID", "77"}
                                                                              });

            bool errorResult77 = (bool) xc.GetItem("SKIP_ACTIVITY_DEF", new Dictionary<string, string>
                                                                            {
                                                                                {"ACTIVITY_STATUS", "ERROR"},
                                                                                {"ACTIVITY_DEF_ID", "77"}
                                                                            });

            Assert.IsFalse(pendingResult77);
            Assert.IsFalse(errorResult77);
        }

        [Test]
        public void TestSwitchResourceFileLoad()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SwitchTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);
        }

        [Test]
        public void TestSwitchSelect_WhenParmetersArePassedAndMatch_ThenValueFromRightBranchReturned()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SwitchTestFile.xml"));
            Dictionary<string, string> parameters = new Dictionary<string, string>
                                                        {
                                                            {"someParamName", "test"}
                                                        };
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value = xc.GetItem("someText", parameters);
            StringAssert.AreEqualIgnoringCase("ytgvfsdf", value.ToString());
        }

        [Test]
        public void TestSwitchSelect_WhenParmetersDoesNotMatch_ThenDefaultValueFromSecondSwitchReturned()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SwitchTestFile.xml"));
            Dictionary<string, string> parameters = new Dictionary<string, string>
                                                        {
                                                            {"someParamName", "test56"}
                                                        };
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value1 = xc.GetItem("someText", parameters);
            StringAssert.AreEqualIgnoringCase("FH*567u", value1.ToString());
        }

        [Test]
        public void TestSwitchSelect_WhenParmetersAreNotPassed_ThenDefaultValueFromFirstSwitchReturned()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SwitchTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value = xc.GetItem("someText", null);
            StringAssert.AreEqualIgnoringCase("FH*567u", value.ToString());
        }

        [Test]
        public void
            TestSwitchSelect_WhenMultipleCallsForSameBranchWithDifferentParamsAreDone_ThenSameValueIsReturnedEveryTime
            ()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.SwitchTestFile.xml"));
            Dictionary<string, string> parameters1 = new Dictionary<string, string>
                                                         {
                                                             {"someParamName", "test1"}
                                                         };
            Dictionary<string, string> parameters2 = new Dictionary<string, string>
                                                         {
                                                             {"someParamName", "test2"}
                                                         };
            Dictionary<string, string> parameters3 = new Dictionary<string, string>
                                                         {
                                                             {"someParamName", "test3"}
                                                         };
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value1 = xc.GetItem("someText", parameters1);
            StringAssert.AreEqualIgnoringCase("dtfryguhERTYU", value1.ToString());

            object value2 = xc.GetItem("someText", parameters2);
            StringAssert.AreEqualIgnoringCase("dtfryguhERTYU", value2.ToString());

            object value3 = xc.GetItem("someText", parameters3);
            StringAssert.AreEqualIgnoringCase("dtfryguhERTYU", value3.ToString());
        }

        [Test]
        public void TestTwoLevelSwitchSelect_WhenParmetersArePassedAndMatch_ThenValueFromRightBranchReturned()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.TwoLevelSwitchFile.xml"));
            Dictionary<string, string> parameters = new Dictionary<string, string>
                                                        {
                                                            {"someParamName", "test"},
                                                            {"someParamName2", "qwerty1"}
                                                        };
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value1 = xc.GetItem("someText", parameters);
            StringAssert.AreEqualIgnoringCase("edrtfyughjk4567", value1.ToString());
        }

        [Test]
        public void
            TestTwoLevelSwitchSelect_WhenParmetersArePassedAndSecondParamDoesNotMatch_ThenDefaultValueFromSecondSwitchReturned
            ()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.TwoLevelSwitchFile.xml"));
            Dictionary<string, string> parameters = new Dictionary<string, string>
                                                        {
                                                            {"someParamName", "test"},
                                                            {"someParamName2", "qwerty2"}
                                                        };
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value1 = xc.GetItem("someText", parameters);
            StringAssert.AreEqualIgnoringCase("ertyDFGH45", value1.ToString());
        }

        [Test]
        public void TestTwoLevelSwitchSelect_WhenParmetersAreNotPassed_ThenDefaultValueFromFirstSwitchReturned()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.TwoLevelSwitchFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            Assert.IsTrue(xc.IsLoaded);

            object value2 = xc.GetItem("someText", null);
            StringAssert.AreEqualIgnoringCase("dfghj", value2.ToString());
        }

        [Test]
        public void TestLoadResource_WhenItemKeyIsDuplicated_ThenExceptionIsThrown()
        {
            XDocument xdoc =
                XDocument.Parse(
                    EmbededFileReader.Instance.Read(
                        "VXCore.Resources.Xml.Tests.DuplicatedResourceItemKeyTestFile.xml"));
            
            try
            {
                new XResourceCollection(xdoc);

                Assert.Fail("LoadResources() method call has to fail because of duplicated resource item key");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf(typeof (ResourceLoadException), ex);
                Assert.IsInstanceOf(typeof (XsdValidationException), ex.InnerException);
                Assert.IsInstanceOf(typeof (XmlSchemaValidationException), ex.InnerException.InnerException);
            }
        }

        [Test]
        public void TestStringArrayRead()
        {
            XDocument xdoc =
                XDocument.Parse(
                    EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.StringArrayTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            object result = xc.GetItem("arrayList", null);

            Assert.IsInstanceOf<ArrayList>(result);

            ArrayList tmp = (ArrayList) result;

            Assert.AreEqual(3, tmp.Count);

            Assert.IsInstanceOf<string>(tmp[0]);
            Assert.IsInstanceOf<string>(tmp[1]);
            Assert.IsInstanceOf<string>(tmp[2]);

            Assert.IsTrue(tmp.Cast<string>().Count(x => string.Equals("Test1", x)) == 1);
            Assert.IsTrue(tmp.Cast<string>().Count(x => string.Equals("Test2 34567", x)) == 1);
            Assert.IsTrue(tmp.Cast<string>().Count(x => string.Equals("Test3 %$^#$", x)) == 1);
        }

        [Test]
        public void TestInt32ArrayRead()
        {
            XDocument xdoc =
                XDocument.Parse(EmbededFileReader.Instance.Read("VXCore.Resources.Xml.Tests.Int32ArrayTestFile.xml"));
            XResourceCollection xc = new XResourceCollection(xdoc);

            xc.LoadResources();

            object result = xc.GetItem("arrayList", null);

            Assert.IsInstanceOf<ArrayList>(result);

            ArrayList tmp = (ArrayList) result;

            Assert.AreEqual(5, tmp.Count);

            Assert.IsInstanceOf<int>(tmp[0]);
            Assert.IsInstanceOf<int>(tmp[1]);
            Assert.IsInstanceOf<int>(tmp[2]);
            Assert.IsInstanceOf<int>(tmp[3]);
            Assert.IsInstanceOf<int>(tmp[4]);

            Assert.IsTrue(tmp.Cast<int>().Count(x => 45 == x) == 2);
            Assert.IsTrue(tmp.Cast<int>().Count(x => 89561 == x) == 1);
            Assert.IsTrue(tmp.Cast<int>().Count(x => 56511165 == x) == 1);
            Assert.IsTrue(tmp.Cast<int>().Count(x => -56511165 == x) == 1);
        }
    }
}