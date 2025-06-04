using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Xceed.Words.NET;
using System.Windows.Threading;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using System.Text;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;

public class DocumentsGenerator : IDocumentsGenerator, IDocumentsGeneratorInitializer
{
    private bool _initialized;
    private readonly IAPIClient _client;

    public DocumentsGenerator(IAPIClient client)
    {
        _client = client;

        GlobalFontSettings.FontResolver = new FontResolver.FontResolver();
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


    public async Task GenerateCsv(List<Project> projects, string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("Проект;Количество спринтов;Участники");

        foreach (var project in projects)
        {
            var sprints = await _client.GetSprintsByProjectID(project.Id);
            var users = await _client.GetProjectUsers(project.Id);
            string userNames = string.Join(", ", users.Select(x => x.UserName));

            writer.WriteLine($"{project.Title};{sprints.Count};{userNames}");
        }
    }

    public async Task GeneratePdf(List<Project> projects, string filePath)
    {
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Отчёт о проектах";

        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont titleFont = new XFont("OpenSans", 18, XFontStyleEx.Bold);
        XFont textFont = new XFont("OpenSans", 14, XFontStyleEx.Regular);

        double yPosition = 50;
        double pageHeight = page.Height.Point - 50; // Максимальная высота страницы

        gfx.DrawString("Отчёт о проектах", titleFont, XBrushes.Black, new XPoint(200, yPosition));
        yPosition += 30;

        foreach (var project in projects)
        {
            if (yPosition > pageHeight) // Если достигнут конец страницы, добавляем новую
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                yPosition = 50;
            }

            gfx.DrawString($"Проект: {project.Title}", textFont, XBrushes.Black, new XPoint(50, yPosition));
            yPosition += 20;

            var sprints = await _client.GetSprintsByProjectID(project.Id);
            gfx.DrawString($"Количество спринтов: {sprints.Count}", textFont, XBrushes.Black, new XPoint(50, yPosition));
            yPosition += 20;

            var users = await _client.GetProjectUsers(project.Id);
            gfx.DrawString($"Участники: {string.Join(", ", users.Select(x => x.UserName))}", textFont, XBrushes.Black, new XPoint(50, yPosition));
            yPosition += 40;
        }

        gfx.DrawString("Диаграммы", titleFont, XBrushes.Black, new XPoint(200, yPosition));
        yPosition += 30;

        string[] chartPaths = { "Temp/PieChart.png", "Temp/CartesianChart.png", "Temp/LinearChart.png" };
        foreach (var path in chartPaths)
        {
            if (File.Exists(path))
            {
                if (yPosition + 200 > pageHeight) // Проверяем, хватит ли места
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPosition = 50;
                }

                XImage image = XImage.FromFile(path);
                gfx.DrawImage(image, 50, yPosition, image.PixelWidth / 2, image.PixelHeight / 2);
                yPosition += image.PixelHeight / 2 + 20;
            }
        }

        document.Save(filePath);
    }

    public async Task GenerateTxt(List<Project> projects, string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);

        writer.WriteLine("Отчёт о проектах");
        writer.WriteLine(new string('-', 50));

        foreach (var project in projects)
        {
            writer.WriteLine($"Проект: {project.Title}");

            var sprints = await _client.GetSprintsByProjectID(project.Id);
            writer.WriteLine($"Количество спринтов: {sprints.Count}");

            var users = await _client.GetProjectUsers(project.Id);
            writer.WriteLine($"Участники: {string.Join(", ", users.Select(x => x.UserName))}");
            writer.WriteLine();
        }
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
