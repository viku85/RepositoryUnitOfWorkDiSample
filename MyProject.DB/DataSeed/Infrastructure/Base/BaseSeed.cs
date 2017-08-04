using MyProject.Interface.Infrastructure;
using MyProject.Interface.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.String;

namespace MyProject.DB.DataSeed.Infrastructure.Base
{
    /// <summary>
    /// Base seed foe the application.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    internal abstract class BaseSeed<TModel>
         where TModel : class
    {
        #region " Variables "

        /// <summary>
        /// The My Project unit of work
        /// </summary>
        protected readonly IMyProjectUnitOfWork GemsUnitOfWork;

        /// <summary>
        /// The model repository
        /// </summary>
        protected readonly IRepository<TModel> ModelRepository;

        /// <summary>
        /// The application base path
        /// </summary>
        protected string ApplicationBasePath;

        #endregion " Variables "

        #region " Constructors "

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeed{TModel}"/> class.
        /// </summary>
        /// <param name="myProjectUnitOfWork">The Project's unit of work.</param>
        protected BaseSeed(IMyProjectUnitOfWork myProjectUnitOfWork)
            : this(myProjectUnitOfWork, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeed{TModel}"/> class.
        /// </summary>
        /// <param name="myProjectUnitOfWork">The project's unit of work.</param>
        /// <param name="repository">The repository.</param>
        protected BaseSeed(IMyProjectUnitOfWork myProjectUnitOfWork, IRepository<TModel> repository)
            : this(myProjectUnitOfWork, repository, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeed{TModel}"/> class.
        /// </summary>
        /// <param name="myProjectUnitOfWork">The project's unit of work.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        protected BaseSeed(IMyProjectUnitOfWork myProjectUnitOfWork, string applicationBasePath)
            : this(myProjectUnitOfWork, null, applicationBasePath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeed{TModel}"/> class.
        /// </summary>
        /// <param name="projectUnitOfWork">The project's unit of work.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        protected BaseSeed(IMyProjectUnitOfWork projectUnitOfWork, IRepository<TModel> repository, string applicationBasePath)
        {
            ModelRepository = repository;
            GemsUnitOfWork = projectUnitOfWork;
            if (!IsNullOrEmpty(applicationBasePath))
            {
                ApplicationBasePath = Path.GetFullPath(Path.Combine(
                    applicationBasePath,
                    $"..\\{nameof(MyProject)}.{nameof(DB)}\\DataSeed"));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeed{TModel}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        protected BaseSeed(IRepository<TModel> repository, string applicationBasePath)
            : this(null, repository, applicationBasePath)
        {
        }

        #endregion " Constructors "

        /// <summary>
        /// JSON read and parse in a opened stream.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <returns>Parsed model from JSON Stream.</returns>
        public virtual IEnumerable<TModel> JsonRead(string file)
        {
            // No JSON Schema check done.
            if (File.Exists(file))
            {
                var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                using (var sr = new StreamReader(fs))
                using (var reader = new JsonTextReader(sr))
                {
                    var jsonSerializer = JsonSerializer.Create();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            yield return jsonSerializer.Deserialize<TModel>(reader);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        protected virtual void Save(TModel model)
        {
            ModelRepository.Create(model);
            GemsUnitOfWork.Save();
        }

        /// <summary>
        /// Saves the specified model asynchronously.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task for saving of seeds</returns>
        protected virtual async Task SaveAsync(TModel model)
        {
            await ModelRepository.CreateAsync(model);
            await GemsUnitOfWork.SaveAsync();
        }
    }
}