using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Model.Tables;
using Equin.ApplicationFramework;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot]
    public class Entities
    {
        [XmlArray] [XmlArrayItem("Person")] public List<Persons> Persons;
        [XmlArray] [XmlArrayItem("Groups")] public List<Groups> Groups;
        [XmlArray] [XmlArrayItem("Payments")] public List<Payments> Payments;
        [XmlArray] [XmlArrayItem("GroupsDays")] public List<GroupsDays> GroupsDays;
        [XmlArray] [XmlArrayItem("Events")] public List<Events> Events;
        [XmlArray] [XmlArrayItem("EventsParticipants")] public List<EventsParticipants> EventsParticipants;
        [XmlArray] [XmlArrayItem("Settings")] public List<Settings> Settings;
        [XmlArray] [XmlArrayItem("GridsSettings")] public List<GridsSettings> GridsSettings;
        [XmlArray] [XmlArrayItem("Budget")] public List<Budget> Budget;
    }

    public partial class DataBase
    {
        public List<Persons> Persons
        {
            get { return Entities.Persons; }
        }

        public List<Groups> Groups
        {
            get { return Entities.Groups; }
        }

        public List<Payments> Payments
        {
            get { return Entities.Payments; }
        }

        public List<GroupsDays> GroupsDays
        {
            get { return Entities.GroupsDays; }
        }

        public List<Events> Events
        {
            get { return Entities.Events; }
        }

        public List<EventsParticipants> EventsParticipants
        {
            get { return Entities.EventsParticipants; }
        }

        public List<Settings> Settings
        {
            get { return Entities.Settings; }
        }

        public List<GridsSettings> GridsSettings
        {
            get { return Entities.GridsSettings; }
        }

        public List<Budget> Budget
        {
            get { return Entities.Budget; }
        }
        

        private readonly string _connectionString;
        private readonly XmlSerializer _serializer;
        private readonly string _path;
        private readonly string _fullPath;
        private Entities _entities;

        private Entities Entities
        {
            get
            {
                if (_entities == null)
                {
                    var filestream = new FileStream(_fullPath, FileMode.Open);
                    _entities = _serializer.Deserialize(filestream) as Entities;
                    filestream.Close();
                }
                return _entities;
            }
        }

        public DataBase(string fileName)
        {
            _connectionString = @"DB\" + fileName;
            _serializer = new XmlSerializer(typeof(Entities));
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _fullPath = _path + _connectionString + ".xml";
        }

        public void CreateNewDatabase()
        {
            using (var stream = new FileStream(_fullPath, FileMode.CreateNew, FileAccess.Write))
            {
                _serializer.Serialize(stream, new Entities());
                stream.Close();
            }
            UpdateDB();
        }

        public void CleanDatabase()
        {
            if (File.Exists(_fullPath))
            {
                File.Delete(_fullPath);
            }
            CreateNewDatabase();
        }

        public bool DatabaseExists()
        {
            return File.Exists(_fullPath);
        }

        public void SubmitChanges()
        {
            using (var steam = new FileStream(_fullPath, FileMode.Create))
            {
                _serializer.Serialize(steam, Entities);
                steam.Close();
            }
        }

        public string UniqueCode()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#";
            string ticks = DateTime.UtcNow.Ticks.ToString();
            var code = "";
            for (var i = 0; i < characters.Length; i += 2)
            {
                if ((i + 2) <= ticks.Length)
                {
                    var number = int.Parse(ticks.Substring(i, 2));
                    if (number > characters.Length - 1)
                    {
                        var one = double.Parse(number.ToString().Substring(0, 1));
                        var two = double.Parse(number.ToString().Substring(1, 1));
                        code += characters[Convert.ToInt32(one)];
                        code += characters[Convert.ToInt32(two)];
                    }
                    else
                        code += characters[number];
                }
            }
            return code;
        }
        
        public void SwapColumnsOrder(int gridId, string colName1, string colName2)
        {
            var col1 = GridsSettings.FirstOrDefault(c => c.GridId == gridId && c.ColumnName == colName1);
            var col2 = GridsSettings.FirstOrDefault(c => c.GridId == gridId && c.ColumnName == colName2);
            if (col1 == null || col2 == null) return;

            col1.OrderNr++;
            if (col2.OrderNr >= 0)
                col2.OrderNr--;
            SubmitChanges();
        }
    }
}
