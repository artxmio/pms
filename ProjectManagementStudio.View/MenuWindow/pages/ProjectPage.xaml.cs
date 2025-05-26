using ProjectManagementStudio.ViewModel.APIClient;
using System.Windows.Controls;

namespace ProjectManagementStudio.View.MenuWindow.pages;

public partial class ProjectPage : IProjectPage
{
    private IAPIClient client;

    public ProjectPage(IAPIClient aPIClient)
    {
        InitializeComponent();

        this.client = aPIClient;
        client.GetProjects(1);
    }
}
