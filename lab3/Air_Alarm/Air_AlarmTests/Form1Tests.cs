using Microsoft.VisualStudio.TestTools.UnitTesting;
using Air_Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Air_Alarm.Models;

namespace Air_Alarm.Tests
{
    [TestClass()]
    public class Form1Tests
    {

        [TestMethod()]
        public void IsAirAlarmTestIsCityNotChosen()
        {
            Form1 form1 = new Form1();
            var result = form1.IsCityEmpty("");
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void IsAirAlarmTestIsCityChosen()
        {
            Form1 form1 = new Form1();
            var result = form1.IsCityEmpty("Lviv");
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void IsStatusChangedTest()
        {
            Form1 form1 = new Form1();
            MyCityModel.AlertNow = false;
            var result = form1.IsStatusChanged(true);
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void IsStatusNotChangedChangedTest()
        {
            Form1 form1 = new Form1();
            MyCityModel.AlertNow = false;
            var result = form1.IsStatusChanged(false);
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void IsFileExistTest()
        {
            Form1 form1 = new Form1();
            AirRaidModel airRaid = null;
            form1.PutDataIntoXML(airRaid, "test1.xml");

            var isFileEmpty = File.Exists("test1.xml");

            Assert.IsFalse(isFileEmpty);
        }
        [TestMethod()]
        public void IsDataForWriteIntoXmlTest()
        {
            Form1 form1 = new Form1();
            AirRaidModel airRaid = new AirRaidModel()
            {
                AlertNow = false,
                Region = "lviv"
            };
            form1.PutDataIntoXML(airRaid, "test.xml");
            var isFileEmpty = File.ReadAllText("test.xml");
            Assert.AreNotEqual(isFileEmpty, "");
        }
    }
}