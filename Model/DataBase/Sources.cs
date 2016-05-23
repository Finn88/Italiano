using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using Model.Tables;

namespace Model
{
    public partial class DataBase
    {

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
                                                               CustomerName = c.FullName,
                                                               PaymentsDate = p.PaymentDate
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
                                                           CustomerName = c.FullName,
                                                           PaymentsDate = p.PaymentDate
                                                       }).ToList());
        }

        public BindingListView<BudgetEntity> GetBudgetList(bool isEarning, DateTime datefrom, DateTime dateto)
        {
            return new BindingListView<BudgetEntity>((from p in Budget
                                                      where p.PaymentDate >= datefrom && p.PaymentDate <= dateto && p.IsEarning == isEarning
                                                      orderby p.PaymentDate
                                                      select new BudgetEntity
                                                      {
                                                          Id = p.Id,
                                                          Costs = p.Costs,
                                                          PaymentDateString = p.PaymentDateString,
                                                          Comments = p.Comments,
                                                          IsEarning = p.IsEarning,
                                                          PaymentsDate = p.PaymentDate
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
                        join grDays in GroupsDays.Where(c => c.Day == (int)dStart.DayOfWeek) on gr.Id equals grDays.GroupId
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

        public BindingListView<PaymentsReportEntity> GetPaymentsReportList(DateTime datefrom, DateTime dateto)
        {
            var expenses = GetBudgetList(false, datefrom, dateto);
            var earning = GetBudgetList(true, datefrom, dateto);
            var payments = GetPaymentsList(false, string.Empty, datefrom, dateto);

            var returnList = new List<PaymentsReportEntity>();
            for (var i = 0; i <= (dateto - datefrom).TotalDays; i++)
            {
                var date = datefrom.AddDays(i);
                var item = new PaymentsReportEntity
                {
                    PaymentDate = date.ToString("yyyy/MM/dd"),
                    Costs = payments.Where(c => c.PaymentsDate == date).Sum(c => c.Costs),
                    CostsComments = string.Join(", ",
                            payments.Where(c => c.PaymentsDate == date).Select(c => c.CustomerName)),
                    Earnings = earning.Where(c => c.PaymentsDate == date).Sum(c => c.Costs),
                    EarningsComments = string.Join(", ", earning.Where(c => c.PaymentsDate == date).Select(c => c.Comments)),
                    Expenses = expenses.Where(c => c.PaymentsDate == date).Sum(c => c.Costs),
                    ExpensesComments = string.Join(", ",
                            expenses.Where(c => c.PaymentsDate == date).Select(c => c.Comments))
                };
                if (!(item.Costs == 0 && item.Earnings == 0 && item.Expenses == 0))
                    returnList.Add(item);
            }

            return new BindingListView<PaymentsReportEntity>(returnList);
        }
    }
}
