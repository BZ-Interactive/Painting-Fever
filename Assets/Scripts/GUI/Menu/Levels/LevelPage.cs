using System;
using Godot;

public partial class LevelPage : VBoxContainer
{
    [Export] public Difficulty Difficulty;
    [Obsolete("For editor use only, do not call directly.")]
    [Export] private HBoxContainer exptUpperlevelRow;
    [Obsolete("For editor use only, do not call directly.")]
    [Export] private HBoxContainer exptLowerlevelRow;
    private LevelRow upperlevelRow;
    private LevelRow lowerlevelRow;

    public override void _Ready()
    {
        upperlevelRow = exptUpperlevelRow as LevelRow;
        lowerlevelRow = exptLowerlevelRow as LevelRow;
        base._Ready();
    }

    public bool AddLevelButton(LevelButton levelButton)
    {
        if (upperlevelRow.AddLevelButton(levelButton))
        {
            levelButton.Difficulty = Difficulty;
            return true;
        }
        else if (lowerlevelRow.AddLevelButton(levelButton))
        {
            levelButton.Difficulty = Difficulty;
            return true;
        }
        
        return false;
    }
}
