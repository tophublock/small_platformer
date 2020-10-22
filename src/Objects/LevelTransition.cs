using Godot;
using System;

public class LevelTransition : Area2D
{

    [Export(PropertyHint.File, "*.tscn")]
    public string TargetStage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void OnLevelTransitionBodyEntered(Node body)
    {
        if (body is Player)
        {
            GetTree().ChangeScene(TargetStage);
        }
    }
}
