using ProjectManagementStudio.Model.ResponseModels;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;

public interface IDocumentsGenerator
{
    Task GenerateDocx(List<Project> projects, string filePath);
    Task GeneratePdf(List<Project> projects, string filePath);
    void GenerateCsv();
    void GenerateTxt();
}