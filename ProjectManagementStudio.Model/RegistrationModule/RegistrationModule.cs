using Autofac;
using ProjectManagementStudio.Model.AuthModel;

namespace ProjectManagementStudio.Model.RegistrationModule
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AuthModel.AuthModel>().As<IAuthModel>().SingleInstance();
        }
    }
}