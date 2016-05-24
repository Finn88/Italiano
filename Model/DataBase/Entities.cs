using System;

namespace Model
{
    public interface IEntity
    {
    }

    public class PaymentEntity : IEntity
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public decimal Costs { get; set; }
        public string DateFromString { get; set; }
        public string DateToString { get; set; }
        public string PaymentDateString { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentsDate { get; set; }
    }

    public class BudgetEntity : IEntity
    {
        public string Id { get; set; }
        public decimal Costs { get; set; }
        public bool IsEarning { get; set; }
        public string PaymentDateString { get; set; }
        public string Comments { get; set; }
        public DateTime PaymentsDate { get; set; }
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
        public string MasterId { get; set; }
        public string DateString { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public bool IsActive { get; set; }
        public decimal Costs { get; set; }
    }

    public class PaymentsReportEntity : IEntity
    {
        public string PaymentDate { get; set; }
        public decimal Costs { get; set; }
        public string CostsComments { get; set; }
        public decimal Earnings { get; set; }
        public string EarningsComments { get; set; }
        public decimal Expenses { get; set; }
        public string ExpensesComments { get; set; }

        public decimal TotalPerDate
        {
            get { return Costs + Earnings + Expenses; }
        }
    }

}
