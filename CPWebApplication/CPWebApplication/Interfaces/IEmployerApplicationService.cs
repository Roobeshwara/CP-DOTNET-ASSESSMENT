using CPWebApplication.Models;

namespace CPWebApplication.Interfaces
{
    public interface IEmployerApplicationService
    {
        Task AddEmployerApplicationAsync(EmployerApplication application);
        Task<EmployerApplication> UpadteEmployerApplicationAsync(EmployerApplication application);

        Task<List<QuestionModel>> GetQuestionsByTypeAsync(string applicationId, string questionType);
    }
}
