using System;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using WebCalendar.DAL;

namespace WebCalendar.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IMeetingRepository>().To<MeetingRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            ninjectKernel.Bind<IContactRepository>().To<ContactRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}