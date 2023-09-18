using ShopiyfyX.Presentation.UI;

namespace ShopiyfyX.Presentation.ForRun;

public class Program
{
    public static async Task Main(string[] args)
    {
        ProjectUI projectUI = new ProjectUI();
        await projectUI.PrintAsync();
    }
}
