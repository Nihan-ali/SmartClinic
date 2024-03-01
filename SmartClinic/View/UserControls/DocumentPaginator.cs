using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

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
        PageSize = new Size(8.5 * 96, 11 * 96); // Letter size with default margins
    }

    public override DocumentPage GetPage(int pageNumber)
    {
        // Create a new StackPanel for each page
        StackPanel panel = new StackPanel();
        panel.Width = PageSize.Width;
        panel.Height = PageSize.Height;

        // Add controls to the StackPanel
        panel.Children.Add(CloneUserControl(header));

        double availableHeight = PageSize.Height - header.ActualHeight - footer.ActualHeight;

        // Calculate how much content to display on this page
        double remainingHeight = prescriptionContent.ActualHeight - availableHeight * pageNumber;
        double displayHeight = Math.Min(availableHeight, remainingHeight);

        // Add prescription content for this page
        UserControl pagePrescriptionContent = CloneUserControl(prescriptionContent);
        pagePrescriptionContent.Height = displayHeight;
        panel.Children.Add(pagePrescriptionContent);

        // Add footer
        panel.Children.Add(CloneUserControl(footer));

        // Measure and arrange the StackPanel
        panel.Measure(PageSize);
        panel.Arrange(new Rect(new Point(0, 0), PageSize));

        // Return the page
        return new DocumentPage(panel);
    }

    private UserControl CloneUserControl(UserControl source)
    {
        if (source == null)
        {
            return null;
        }

        UserControl clonedControl = Activator.CreateInstance(source.GetType()) as UserControl;

        // Copy relevant properties from the source to the cloned control
        // For example, if your UserControl has specific properties, copy them here

        return clonedControl;
    }

    public override bool IsPageCountValid => true;

    public override int PageCount => (int)Math.Ceiling(prescriptionContent.ActualHeight / (PageSize.Height - header.ActualHeight - footer.ActualHeight));

    public override Size PageSize
    {
        get => pageSize;
        set => pageSize = value;
    }

    public override IDocumentPaginatorSource Source => null;
}

