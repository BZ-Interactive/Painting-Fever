using Godot;

[Tool]
public partial class JumpPointPlacer : Node2D
{
    [Export] private Level level;
    private LevelData data; // must use lazy loading or this will throw a initialization error
    [Export] private float gizmoSize = 200f;
    [Export] Godot.Collections.Array<Marker2D> points = [];

    [Export]
    public bool RunGenerateJumpPoints
    {
        get => false; // Always stays unchecked
        set
        {
            if (value)
            {
                data ??= GD.Load<LevelData>("res://Assets/Data/LevelData.tres");
                if (data == null)
                {
                    GD.PrintErr("Failed to load LevelData.tres");
                    return;
                }

                ClearPoints();
                GenerateJumpPoints();
            }
        }
    }


    [Export]
    public bool ClearAllPoints
    {
        get => false; // Always stays unchecked
        set
        {
            if (value)
            {
                ClearPoints();
            }
        }
    }

    public void GenerateJumpPoints()
    {
        float distancePerBeat = data.DifficultyToSpeedMap[level.Difficulty] / (level.MusicBPM / 60f);
        int JumpPointCount = (int)(level.MaxDistance / distancePerBeat);
        for (int i = 1; i <= JumpPointCount; i++)
        {
            PlaceJumpPoint(new Vector2(i * distancePerBeat, 0f));
        }
    }

    private void PlaceJumpPoint(Vector2 position)
    {
        Marker2D point = new() { GizmoExtents = gizmoSize, Position = position };
        this.AddChild(point);
        point.Owner = GetTree().EditedSceneRoot;
        points.Add(point);
    }

    private void ClearPoints()
    {
        points ??= []; // if null recrate
        if (points.Count == 0) return; // if zero, its empty anyway

        foreach (Marker2D point in points)
        {
            this.RemoveChild(point);
            point.Free(); // Free is better for editor tools
        }
        points.Clear();
    }
}
