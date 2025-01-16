using Autofac;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.AboutText;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewEmail;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewLogin;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewPassword;
using ProjectManagementStudio.Model.SettingSize;
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
            builder.RegisterType<CurrentUserModel.CurrentUserModel>().As<ICurrentUserModel>().SingleInstance();

            builder.RegisterType<ChangeLoginModel>().As<IChangeLoginModel>().SingleInstance();
            builder.RegisterType<ChangePasswordModel>().As<IChangePasswordModel>().SingleInstance();
            builder.RegisterType<ChangeEmailModel>().As<IChangeEmailModel>().SingleInstance();
            builder.RegisterType<AboutTextModel>().As<IAboutTextModel>().SingleInstance();

            builder.RegisterType<WindowSizes>().As<IWindowSizes>().InstancePerDependency();

            builder.RegisterType<UserDataMementoWrapper>()
                .As<IUserDataMementoWrapper>()
                .As<IUserDataMementoWrapperInitializer>()
                .SingleInstance();
        }
    }
}