using Godot;

[Tool]
public partial class JumpPointPlacer : Node2D
{
    [Export] private Level level;
    [Export] private LevelData data;
    [Export] Godot.Collections.Array<Marker2D> points = [];

    [Export]
    public bool RunGenerateJumpPoints
    {
        get => false; // Always stays unchecked
        set
        {
            if (value)
            {
                if (points.Count > 0)
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
                if (points.Count > 0)
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
        Marker2D point = new() { GizmoExtents = 200f, Position = position };
        points.Add(point);
        this.AddChild(point);
    }

    private void ClearPoints()
    {
        foreach (Marker2D point in points)
        {
            this.RemoveChild(point);
            point.QueueFree();
        }
        points.Clear();
    }
}
