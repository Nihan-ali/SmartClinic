using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

public class PrescriptionDocumentPaginator : DocumentPaginator
{
    private readonly UserControl header;
    private readonly UserControl footer;
    private readonly UserControl prescriptionContent;

    private Size pageSize; // New field to store page size

    public PrescriptionDocumentPaginator(UserControl header, UserControl footer, UserControl prescriptionContent)
    {
        this.header = header;
        this.footer = footer;
        this.prescriptionContent = prescriptionContent;

        // Set up an initial default page size
        PageSize = new Size(8.27 * 96, 11.69 * 96);
        // Set your desired default page size with margins included
    }

    public override DocumentPage GetPage(int pageNumber)
    {
        // Create a new StackPanel for each page
        StackPanel panel = new StackPanel();
        panel.Width = PageSize.Width;
        panel.Height = PageSize.Height;

        // Add controls to the StackPanel
        panel.Children.Add(header);
        panel.Children.Add(prescriptionContent);
        panel.Children.Add(footer);

        // Measure and arrange the StackPanel
        panel.Measure(PageSize);
        panel.Arrange(new Rect(new Point(0, 0), PageSize));

        // Return the page
        return new DocumentPage(panel);
    }

    public override bool IsPageCountValid => true;

    public override int PageCount => 1; // For simplicity, you may need to adjust this based on the actual size of the content

    public override Size PageSize
    {
        get => pageSize;
        set => pageSize = value;
    }

    public override IDocumentPaginatorSource Source => null; // Implementing the missing property
}
