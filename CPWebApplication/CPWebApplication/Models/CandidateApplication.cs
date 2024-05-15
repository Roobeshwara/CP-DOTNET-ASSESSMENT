namespace CPWebApplication.Models
{
    public class CandidateApplication
    {
        #region Personal details
        public string? id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidance { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DateOFBirth { get; set; }
        public string? Gender { get; set; }
        #endregion
        #region Additonal questions
        public string? AbountCandidate { get; set; }
        public string? YearOfGraduation { get; set; }
        public string? YearOfGraduationOther { get; set; }
        public List<string>? CandidateInterest { get; set; }
        public bool IsRejectedByUkEmbassy { get; set; }
        public int YearsOfExperince { get; set; }
        public DateTime? DateMovedToUK { get; set; }
        #endregion
    }
}
