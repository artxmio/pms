using ProjectManagementStudio.Model.ResponseModels;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;

public interface IDocumentsGenerator
{
    Task GenerateDocx(List<Project> projects, string filePath);
    Task GeneratePdf(List<Project> projects, string filePath);
    Task GenerateCsv(List<Project> projects, string filePath);
    Task GenerateTxt(List<Project> projects, string filePath);
}