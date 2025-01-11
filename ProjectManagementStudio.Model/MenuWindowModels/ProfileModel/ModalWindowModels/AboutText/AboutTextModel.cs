namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.AboutText;

public class AboutTextModel : IAboutTextModel
{
    private string _aboutText = "";

    public string AboutText
    {
        get => _aboutText; 
        set => _aboutText = value;
    }
}
