using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Data;

namespace MovieApi.Controllers
{
    public abstract class ApiBase : ControllerBase
    {
        
        private ModelFactory modelFactory;
        

        public ApiBase(IMovieRepository repo, ILogger logger) 
        {
            this.Repository = repo;
            Logger = logger;
        }

        protected IMovieRepository Repository { get; }

        protected ILogger Logger { get; }

        protected ModelFactory ModelFactory {
            get {
                if (modelFactory == null) {
                    modelFactory = new ModelFactory(Repository);
                }
                return modelFactory;
            }

        }
    }
}
