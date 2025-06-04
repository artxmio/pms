using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Xceed.Words.NET;
using System.Windows.Threading;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;

public class DocumentsGenerator : IDocumentsGenerator, IDocumentsGeneratorInitializer
{
    private bool _initialized;
    private readonly IAPIClient _client;

    public DocumentsGenerator(IAPIClient client)
    {
        _client = client;
    }

    public async Task GenerateDocx(List<Project> projects, string filePath)
    {
        using var doc = DocX.Create(filePath);
        doc.InsertParagraph("Отчёт о проектах")
            .FontSize(18).Bold().Alignment = Xceed.Document.NET.Alignment.center;

        foreach (var project in projects)
        {
            doc.InsertParagraph($"Проект: {project.Title}")
                .FontSize(14).Bold();

            var sprints = await _client.GetSprintsByProjectID(project.Id);
            doc.InsertParagraph($"Количество спринтов: {sprints.Count}");

            var users = await _client.GetProjectUsers(project.Id);
            doc.InsertParagraph($"Участники: {string.Join(", ", users.Select(x=>x.UserName))}").SpacingAfter(20);
        }

        doc.InsertParagraph($"Диаграммы").SpacingAfter(20).FontSize(18).Bold().Alignment = Xceed.Document.NET.Alignment.center;

        string[] chartPaths = { "Temp/PieChart.png", "Temp/CartesianChart.png", "Temp/LinearChart.png" };
        foreach (var path in chartPaths)
        {
            if (File.Exists(path))
            {
                var image = doc.AddImage(path);
                var picture = image.CreatePicture();
                doc.InsertParagraph().AppendPicture(picture);
            }
        }

        doc.Save();
    }


    public void GenerateCsv()
    {
        throw new NotImplementedException();
    }

    public void GeneratePdf()
    {
        throw new NotImplementedException();
    }

    public void GenerateTxt()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(DocumentsGenerator)} is already initialized");
        }

        _initialized = true;
    }
}
