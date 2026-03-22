using Godot;
using System;

public partial class FailMenu : CenterContainer
{
    [Export] private Label failReasonLabel;
    public string FailReason { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Visible = false;
    }

    public void OpenFailMenu(string failReason)
    {
        FailReason = failReason;
        failReasonLabel.Text = FailReason;
        this.Visible = true;
    }

    public void CloseFailMenu()
    {
        FailReason = "";
        failReasonLabel.Text = FailReason;
        this.Visible = false;
    }

    private void OnRetryButton()
    {
        LevelManager.Instance.RestartCurrentLevel();
        CloseFailMenu();
    }

    private void OnReturnButton()
    {
        GameManager.Instance.ReturnToMainMenu();
        CloseFailMenu();
    }
}
