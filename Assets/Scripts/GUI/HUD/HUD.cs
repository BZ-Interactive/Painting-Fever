using Godot;

public partial class HUD : Control, IEventSubscriber
{
    [Export] private ProgressBar levelProgressBar;
    [Export] private Godot.Collections.Array<ColorRect> colorRects;

    [ExportCategory("UI Subscenes")]
    [Export] public PauseMenu PauseMenu { get; private set; }
    [Export] public ColorRect metronome { get; private set; }
    [Export] public Sprite2D movingArrow { get; private set; }

    Tween tween;

    public override void _Ready()
    {
        base._Ready();

    }
    private bool insideBeatWindow = false;
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

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            PauseMenu.Visible = !PauseMenu.Visible;
        }
        base._Input(@event);
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
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

    public void UpdateProgressBar(float progress)
    {
        levelProgressBar.Value = progress;
    }

    public void SelectColour(int index)
    {
        //colorRects[index].Shine(); // TODO: add Shine effect
    }

    public void OnPauseButtonPressed()
    {
        PauseMenu.Visible = !PauseMenu.Visible;
    }

    public void OnGameStateChanged(GameState oldState, GameState targetState)
    {
        if (targetState == GameState.Menu)
        {
            PauseMenu.Visible = false;
        }
    }
}
