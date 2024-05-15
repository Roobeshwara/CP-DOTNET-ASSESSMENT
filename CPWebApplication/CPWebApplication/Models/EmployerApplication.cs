namespace CPWebApplication.Models
{
    public class EmployerApplication
    {
        #region Application Details
        public string? id { get; set; }
        public string? ProgramTitle { get; set; }
        public string? ProgramDescription { get; set; }
        #endregion
        #region Personal Information
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidance { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DateOFBirth { get; set; }
        public string? Gender { get; set; }
        #endregion

        public List<QuestionModel> Questions { get; set; }

        public EmployerApplication()
        {
            Questions = new List<QuestionModel>();
        }
    }
    public class QuestionModel
    {
        public string? id { get; set; }
        public string? Type { get; set; }
        public string? Question { get; set; }
        // Properties specific to certain question types
        public List<string>? Choices { get; set; }
        public bool EnableOtherOption { get; set; }
        public int MaxChoiceAllowed { get; set; }
    }
    public static class QuestionTypes
    {
        public const string MultipleChoice = "MultipleChoice";
        public const string Paragraph = "Text";
        public const string Number = "Number";
        public const string YesNo = "Yes/No";
        public const string Date = "Date";
    }
}
