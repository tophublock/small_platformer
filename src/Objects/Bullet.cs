using Godot;
using System;

public class Bullet : Area2D
{
    const int SPEED = 50;
    public Vector2 Direction = Vector2.Right;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        Vector2 velocity = new Vector2(SPEED * Direction.x * delta, SPEED * Direction.y * delta);
        this.Position += velocity;
    }
}
