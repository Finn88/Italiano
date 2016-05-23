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

        public BindingListView<PaymentEntity> GetPaymentsList(bool isDetailsGrid, string detailsId, DateTime datefrom,
            DateTime dateto)
        {
            if (isDetailsGrid)
                return new BindingListView<PaymentEntity>((from p in Payments
                    join c in Persons on p.PersonId equals c.Id
                    where c.Id == detailsId && c.PersonType == 'S'
                    where p.PaymentDate >= datefrom && p.PaymentDate <= dateto
                    orderby c.Id, p.PaymentDate
                    select new PaymentEntity
                           {
                               Id = p.Id,
                               PersonId = p.PersonId,
                               Costs = p.Costs,
                               DateFromString = p.DateFromString,
                               DateToString = p.DateToString,
                               PaymentDateString = p.PaymentDateString,
                               CustomerName = c.FullName
                           }).ToList());
            return new BindingListView<PaymentEntity>((from p in Payments
                join c in Persons on p.PersonId equals c.Id
                where p.PaymentDate >= datefrom && p.PaymentDate <= dateto
                orderby p.PaymentDate
                select new PaymentEntity
                       {
                           Id = p.Id,
                           PersonId = p.PersonId,
                           Costs = p.Costs,
                           DateFromString = p.DateFromString,
                           DateToString = p.DateToString,
                           PaymentDateString = p.PaymentDateString,
                           CustomerName = c.FullName
                       }).ToList());
        }

        public BindingListView<PersonEntity> GetPersonsList(char type, bool isDetailsGrid, string detailsId)
        {
            if (isDetailsGrid)
                return new BindingListView<PersonEntity>((from p in Persons
                    join g in Groups on p.GroupId equals g.Id
                    where p.PersonType == type && p.GroupId == detailsId
                    orderby p.LastName, p.FirstName, p.MiddleName
                    select new PersonEntity
                           {
                               Id = p.Id,
                               FirstName = p.FirstName,
                               LastName = p.LastName,
                               MiddleName = p.MiddleName,
                               Sex = p.Sex,
                               Phone = p.Phone,
                               Email = p.Email,
                               BirthDateString = p.BirthDateString,
                               SecondaryDateString = p.SecondaryDateString,
                               IsActive = p.IsActive,
                               Group = (g != null ? g.Name : "")
                           }).ToList());
            return new BindingListView<PersonEntity>((from p in Persons
                join g in Groups on p.GroupId equals g.Id into joinG
                from g in joinG.DefaultIfEmpty()
                where p.PersonType == type
                orderby p.LastName, p.FirstName, p.MiddleName
                select new PersonEntity
                       {
                           Id = p.Id,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           MiddleName = p.MiddleName,
                           Sex = p.Sex,
                           Phone = p.Phone,
                           Email = p.Email,
                           BirthDateString = p.BirthDateString,
                           SecondaryDateString = p.SecondaryDateString,
                           IsActive = p.IsActive,
                           Group = (g != null ? g.Name : "")
                       }).ToList());
        }

        public BindingListView<GroupEntity> GetGroupsList(char type)
        {
            var list = (from g in Groups.Where(c => c.GroupType == type)
                join p in Persons.Where(c => c.PersonType == 'T') on g.TeacherId equals p.Id into joinP
                from p in joinP.DefaultIfEmpty()
                orderby g.Name
                select new GroupEntity
                       {
                           Id = g.Id,
                           Name = g.Name,
                           Level = g.Level,
                           TeacherId = (p != null ? p.LastName + " " + p.FirstName : ""),
                           StartEducationString = g.StartEducationString,
                           IsActive = g.IsActive
                       }).ToList();

            list.ForEach(c =>
                         {
                             var lessons = GroupsDays.Where(x => x.GroupId == c.Id).OrderBy(x => x.Day).ToList();
                             c.LessonsDays = string.Join(", ", lessons.Select(
                                 i => i.Day.DayOfWeek() + " " + i.StartTimeString + "-" + i.EndTimeString)
                                 .ToArray());

                         });

            return new BindingListView<GroupEntity>(list);
        }

        public BindingListView<EventsEntity> GetEventsList(char type)
        {
            return new BindingListView<EventsEntity>((from e in Events.Where(c => c.EventType == type)
                join p in Persons.Where(c => c.PersonType == 'T' || c.PersonType == 'C')
                    on e.MasterId equals p.Id into joinP
                from p in joinP.DefaultIfEmpty()
                orderby e.StartDate, e.EventName
                select new EventsEntity
                       {
                           Id = e.Id,
                           EventName = e.EventName,
                           MasterName = (p != null ? p.LastName + " " + p.FirstName : ""),
                           Date = e.DateString,
                           StartTime = e.StartTimeString,
                           EndTime = e.EndTimeString,
                           IsActive = e.IsActive
                       }).ToList());
        }

        public List<GroupsDays> GetGroupsDaysList(string groupId)
        {
            return (from g in GroupsDays
                where g.GroupId == groupId
                select g).ToList();
        }

        public ListViewItem[] GetScheduler(DateTime dStart)
        {
            //var date = dStart.StartOfWeek(DayOfWeek.Monday);
            var returnList = new List<ListViewItem>();
            var list = (from gr in Groups
                join grDays in GroupsDays.Where(c => c.Day == (int) dStart.DayOfWeek) on gr.Id equals grDays.GroupId
                where gr.StartEducation <= dStart
                orderby grDays.StartTime, gr.GroupType
                select new
                       {
                           gr.Name,
                           gr.GroupType,
                           time = (grDays.StartTimeString + " - " + grDays.EndTimeString)
                       }).ToList();
            foreach (var item in list)
            {
                var listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.time);
                listItem.BackColor = item.GroupType == 'C' ? Color.GreenYellow : Color.LightCoral;
                returnList.Add(listItem);
            }
            return returnList.ToArray();
        }

        public List<GridsSettings> GetColumnsSettingsList(int gridId)
        {
            return (from g in GridsSettings
                where g.GridId == gridId
                orderby g.OrderNr
                select g).ToList();
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
    }

    public interface IEntity{}

    public class PaymentEntity : IEntity
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public Decimal Costs { get; set; }
        public string DateFromString { get; set; }
        public string DateToString { get; set; }
        public string PaymentDateString { get; set; }
        public string CustomerName { get; set; }
    }

    public class PersonEntity : IEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public char? Sex { get; set; }
        public string SexString
        {
            get
            {
                if (Sex != null)
                    return Sex == 'M' ? "Муж." : "Жен.";
                return "Муж.";
            }
        }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BirthDateString { get; set; }
        public string SecondaryDateString { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; }
        public char PersonType { get; set; }
    }

    public class GroupEntity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string TeacherId { get; set; }
        public string StartEducationString { get; set; }
        public bool IsActive { get; set; }
        public string LessonsDays { get; set; }
    }

    public class EventsEntity : IEntity
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public string MasterName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
