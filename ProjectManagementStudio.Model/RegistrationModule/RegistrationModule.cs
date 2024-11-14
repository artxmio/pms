using Autofac;
using ProjectManagementStudio.Model.AuthModel;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;

namespace ProjectManagementStudio.Model.RegistrationModule
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AuthModel.AuthModel>().As<IAuthModel>().SingleInstance();

            builder.RegisterType<UserDataMementoWrapper>()
                .As<IUserDataMementoWrapper>()
                .As<IUserDataMementoWrapperInitializer>()
                .SingleInstance();
        }
    }
}