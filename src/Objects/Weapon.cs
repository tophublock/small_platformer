using Godot;
using System;

public class Weapon : Area2D
{
    public Vector2 Direction = Vector2.Right;
    private Position2D _bulletStartPosition;
    private PackedScene _bulletScene;

    public override void _Ready()
    {
        _bulletStartPosition = GetNode<Position2D>("BulletPosition");
        _bulletScene = ResourceLoader.Load("res://src/Objects/Bullet.tscn") as PackedScene;
    }

    public override void _Process(float delta)
    {

    }

    public void OnWeaponBodyEntered(Node body)
    {
        if (body is Player player)
        {
            player.PickUpObject(this);
            this.Disconnect("body_entered", this, nameof(OnWeaponBodyEntered));
        }
    }

    public void Shoot()
    {
        Bullet b = _bulletScene.Instance() as Bullet;
        b.Direction = this.Direction;

        Player parent = this.GetParent() as Player;
        b.Position = _bulletStartPosition.Position + parent.Position;
        Console.WriteLine(b.Position);

        Node game = GetTree().Root.GetNode("Game");
        game.AddChild(b);
    }
}
