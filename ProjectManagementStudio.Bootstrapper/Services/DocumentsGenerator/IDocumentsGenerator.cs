using ProjectManagementStudio.Model.ResponseModels;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;

public interface IDocumentsGenerator
{
    Task GenerateDocx(List<Project> projects, string filePath);
    void GeneratePdf();
    void GenerateCsv();
    void GenerateTxt();
}