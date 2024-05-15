using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class EmployerApplicationSerivce : IEmployerApplicationService
    {
        private static Container? _container;

        public EmployerApplicationSerivce(IConfiguration configuration)
        {
            InitializeContainerAsync(configuration).Wait(); // Call an async method synchronously
        }

        private async Task InitializeContainerAsync(IConfiguration configuration)
        {
            await CosmosDBConnectionService.GetStartedCosmosDBAsync(configuration);
            _container = CosmosDBConnectionService.EmployerApplicationContainer;
        }
        public async Task AddEmployerApplicationAsync(EmployerApplication application)
        {
            try
            {
                await _container.CreateItemAsync<EmployerApplication>(application, new PartitionKey(application.id));
            }
            catch (CosmosException)
            {
                throw;
            }
        }
        public async Task<EmployerApplication> UpadteEmployerApplicationAsync(EmployerApplication application)
        {
            try
            {
                // Read the existing item using its unique id
                ItemResponse<EmployerApplication> res = await _container.ReadItemAsync<EmployerApplication>(application.id, new PartitionKey(application.id));
                var existingItem = res.Resource;
                //Replace existing item values with new values
                existingItem.ProgramTitle = application.ProgramTitle;
                existingItem.ProgramDescription = application.ProgramDescription;
                existingItem.FirstName = application.FirstName;
                existingItem.LastName = application.LastName;
                existingItem.Email = application.Email;
                existingItem.Phone = application.Phone;
                existingItem.Nationality = application.Nationality;
                existingItem.CurrentResidance = application.CurrentResidance;
                existingItem.IDNumber = application.IDNumber;
                existingItem.DateOFBirth = application.DateOFBirth;
                existingItem.Gender = application.Gender;

                // Update questions
                foreach (var question in application.Questions)
                {
                    // Check if the question already exists in the existingItem
                    var existingQuestion = existingItem.Questions.FirstOrDefault(q => q.id == question.id);

                    if (existingQuestion != null)
                    {
                        // If the question exists, update its properties
                        existingQuestion.Type = question.Type;
                        existingQuestion.Question = question.Question;
                        existingQuestion.Choices = question.Choices;
                        existingQuestion.EnableOtherOption = question.EnableOtherOption;
                        existingQuestion.MaxChoiceAllowed = question.MaxChoiceAllowed;
                    }
                    else
                    {
                        // If the question does not exist, add it to the existingItem's questions
                        existingItem.Questions.Add(question);
                    }
                }
                // Remove questions that are not present in the updated application
                existingItem.Questions.RemoveAll(q => !application.Questions.Any(aq => aq.id == q.id));

                var updateRes = await _container.ReplaceItemAsync(existingItem, application.id, new PartitionKey(application.id));
                return updateRes.Resource;
            }
            catch (CosmosException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<QuestionModel>> GetQuestionsByTypeAsync(string applicationId, string questionType)
        {
            try
            {
                ItemResponse<EmployerApplication> response = await _container.ReadItemAsync<EmployerApplication>(applicationId, new PartitionKey(applicationId));
                var existingItem = response.Resource;
                //Remove all other types
                List<QuestionModel> typedQuestions = existingItem.Questions.Where(q => q.Type == questionType).ToList();

                return typedQuestions;
            }
            catch (CosmosException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
