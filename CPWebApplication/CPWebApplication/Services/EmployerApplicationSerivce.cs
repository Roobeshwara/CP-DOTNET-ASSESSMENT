using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class EmployerApplicationSerivce : IEmployerApplicationService
    {
        private static Container _container;

        public static void Initialize(Container container)
        {
            _container = container;
        }
        public async Task AddEmployerApplicationAsync(EmployerApplication application)
        {
            try
            {
                await _container.CreateItemAsync<EmployerApplication>(application, new PartitionKey(application.ProgramTitle));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EmployerApplication> UpadteEmployerApplicationAsync(EmployerApplication application)
        {
            try
            {
                ItemResponse<EmployerApplication> res = await _container.ReadItemAsync<EmployerApplication>(application.id, new PartitionKey(application.ProgramTitle));
                //Get Existing Item
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

                var updateRes = await _container.ReplaceItemAsync(existingItem, application.id, new PartitionKey(application.ProgramTitle));
                return updateRes.Resource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<QuestionModel>> GetQuestionsByTypeAsync(string applicationId, string partitionKey, string questionType)
        {
            try
            {
                ItemResponse<EmployerApplication> response = await _container.ReadItemAsync<EmployerApplication>(applicationId, new PartitionKey(partitionKey));
                var existingItem = response.Resource;
                //Remove all other types
                List<QuestionModel> typedQuestions = existingItem.Questions.Where(q => q.Type == questionType).ToList();

                return typedQuestions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
