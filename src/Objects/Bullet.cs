using Godot;
using System;

public class Bullet : Area2D
{
    const int SPEED = 200;
    public Vector2 Direction = Vector2.Right;

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        Vector2 velocity = new Vector2(SPEED * Direction.x * delta, SPEED * Direction.y * delta);
        this.Position += velocity;
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
