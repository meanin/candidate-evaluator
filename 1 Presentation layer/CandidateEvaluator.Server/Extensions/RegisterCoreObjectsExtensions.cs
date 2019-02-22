using System.Collections.Generic;
using CandidateEvaluator.Contract.CoreObjects.Commands.Category;
using CandidateEvaluator.Contract.CoreObjects.Commands.Question;
using CandidateEvaluator.Contract.CoreObjects.Models;
using CandidateEvaluator.Contract.CoreObjects.Queries.Category;
using CandidateEvaluator.Contract.CoreObjects.Queries.Question;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Core.CoreObjects.CommandHandlers.Category;
using CandidateEvaluator.Core.CoreObjects.CommandHandlers.Question;
using CandidateEvaluator.Core.CoreObjects.QueryHandlers.Category;
using CandidateEvaluator.Core.CoreObjects.QueryHandlers.Question;
using CandidateEvaluator.Data.CoreObjects.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateEvaluator.Server.Extensions
{
    public static class RegisterCoreObjectsExtensions
    {
        public static IServiceCollection AddCoreObjects(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();

            services.AddTransient<ICommandHandler<CreateCategoryCommand>, CreateCategoryHandler>();
            services.AddTransient<ICommandHandler<DeleteCategoryCommand>, DeleteCategoryHandler>();
            services.AddTransient<ICommandHandler<UpdateCategoryCommand>, UpdateCategoryHandler>();

            services.AddTransient<ICommandHandler<CreateQuestionCommand>, CreateQuestionHandler>();
            services.AddTransient<ICommandHandler<DeleteQuestionCommand>, DeleteQuestionHandler>();
            services.AddTransient<ICommandHandler<UpdateQuestionCommand>, UpdateQuestionHandler>();

            services.AddTransient<IQueryHandler<GetCategoryQuery, Category>, GetCategoryHandler>();
            services.AddTransient<IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>, GetAllCategoriesHandler>();

            services.AddTransient<IQueryHandler<GetQuestionQuery, Question>, GetQuestionHandler>();
            services.AddTransient<IQueryHandler<GetAllQuestionsQuery, IEnumerable<Question>>, GetQuestionsHandler>();

            return services;
        }
    }
}
