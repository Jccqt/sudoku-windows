namespace SudokuWindows.WinForms;

/// <summary>
/// Hosts the Windows application. Gameplay controls are added in later milestones.
/// </summary>
public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "Sudoku for Windows";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(800, 600);
        MinimumSize = new Size(640, 480);
        AutoScaleMode = AutoScaleMode.Dpi;

        Controls.Add(new Label
        {
            AutoSize = true,
            Anchor = AnchorStyles.None,
            Text = "Sudoku for Windows",
            Font = new Font(Font.FontFamily, 20, FontStyle.Bold),
            AccessibleName = "Sudoku for Windows",
        });

        Layout += CenterContent;
    }

    private void CenterContent(object? sender, LayoutEventArgs e)
    {
        Control title = Controls[0];
        title.Location = new Point(
            (ClientSize.Width - title.Width) / 2,
            (ClientSize.Height - title.Height) / 2);
    }
}
