using System.Windows.Controls;

public static class UserControlExtensions
{
    public static UserControl Clone(this UserControl source)
    {
        // Implement your cloning logic here
        // For simplicity, we'll create a new instance of the same type
        var clone = Activator.CreateInstance(source.GetType()) as UserControl;

        // Copy any properties or settings if necessary

        return clone;
    }
}
