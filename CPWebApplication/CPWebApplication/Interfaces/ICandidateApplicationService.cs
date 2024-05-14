using CPWebApplication.Models;

namespace CPWebApplication.Interfaces
{
    public interface ICandidateApplicationService
    {
        Task AddCandidateApplicationAsync(CandidateApplication application);
    }
}
