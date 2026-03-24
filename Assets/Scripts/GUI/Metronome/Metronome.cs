using Godot;
using System;

public partial class Metronome : Control
{
	// Called when the node enters the scene tree for the first time.
	
    [Export] public Sprite2D movingArrow { get; private set; }
    Tween tween;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // if (SoundManager.Instance.CheckTiming(false) && !insideBeatWindow)
        // {
        //     insideBeatWindow=true;
        //     tween.Kill();
        //     tween = GetTree().CreateTween();
        //     metronome.Scale = new Vector2(1f,1f);
        //     tween.TweenProperty(metronome,"scale",new Vector2(0f,0f),60.0f / LevelManager.Instance.CurrentLevel.MusicBPM);
        // }
        // else if(!SoundManager.Instance.CheckTiming(false))
        // {
        //     insideBeatWindow=false;
        // }

        // if (SoundManager.Instance.CheckTiming(false) && !insideBeatWindow)
        // {
        //     insideBeatWindow = true;
        //     movingArrow.Position = new Vector2(300.0f, 550.0f);

        //     if (tween == null || tween.IsValid())
        //     {
        //         if (tween != null)
        //             tween.Kill();
        //         tween = GetTree().CreateTween();
        //         tween.TweenProperty(movingArrow, "position", new Vector2(640.0f, 550.0f), 60.0f / LevelManager.Instance.CurrentLevel.MusicBPM);
        //     }
        //     tween.Play();
        // }
        // else if (!SoundManager.Instance.CheckTiming(false))
        // {
        //     insideBeatWindow = false;
        // }
        //
        // // 1. Get the same exact time the Rect uses
	}

	public override void _PhysicsProcess(double delta)
	{
		UpdateMetronome();
	}

	public void UpdateMetronome()
    {
        if (LevelManager.Instance.CurrentLevel != null && LevelManager.Instance.CurrentLevel.LevelStarted)
        {
            (float songPos, float spb) = SoundManager.Instance.GetSongPlaybackInfo();
            float progress = songPos % spb / spb;
            float currentX = Mathf.Lerp(300.0f, 640.0f, progress);

            movingArrow.Position = new Vector2(currentX, 550.0f);
        }
    }
}
