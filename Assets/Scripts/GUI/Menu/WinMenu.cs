using Godot;

public partial class WinMenu : CenterContainer
{
    [Export] private Godot.Collections.Array<TextureRect> stars;

    [Export] private Label scoreValueLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Visible = false;
    }

    /// <summary>
    /// Opens the win menu with the given score. <br/>
    /// Score is a percentage.
    /// </summary>
    /// <param name="score">The score to display.</param>
    public void OpenWinMenu(int score)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].Visible = i < score;
        }
        scoreValueLabel.Text = score.ToString();
        this.Visible = true;
    }

    public void CloseWinMenu()
    {
        this.Visible = false;
        foreach (var star in stars)
        {
            star.Visible = false;
        }
        scoreValueLabel.Text = "";
    }

    private void OnNextButton()
    {
        LevelManager.Instance.LoadNextLevel();
        CloseWinMenu();
    }

    private void OnReturnButton()
    {
        GameManager.Instance.ReturnToMainMenu();
        CloseWinMenu();
    }
}
