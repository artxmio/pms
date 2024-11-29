using Autofac;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;

namespace ProjectManagementStudio.Model.RegistrationModule
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AuthModel>().As<IAuthModel>().SingleInstance();
            builder.RegisterType<RegisterModel>().As<IRegisterModel>().SingleInstance();

            builder.RegisterType<ProfileModel>().As<IProfileModel>().SingleInstance();

            builder.RegisterType<UserDataMementoWrapper>()
                .As<IUserDataMementoWrapper>()
                .As<IUserDataMementoWrapperInitializer>()
                .SingleInstance();
        }
    }
}