using Godot;
using System;

public class Weapon : Area2D
{
    public Vector2 Direction = Vector2.Right;
    private double _shootCountdownSec = 0.0;
    private double _shootDelaySec = 0.5;
    private Position2D _bulletStartPosition;
    private PackedScene _bulletScene;

    public override void _Ready()
    {
        _bulletStartPosition = GetNode<Position2D>("BulletPosition");
        _bulletScene = ResourceLoader.Load("res://src/Objects/Bullet.tscn") as PackedScene;
    }

    public override void _Process(float delta)
    {
        _shootCountdownSec -= delta;
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
        if (_shootCountdownSec <= 0)
        {
            Bullet b = _bulletScene.Instance() as Bullet;
            b.Direction = this.Direction;

            Player parent = this.GetParent() as Player;
            b.Position = _bulletStartPosition.Position + parent.Position;

            Node game = GetTree().Root.GetNode("Game");
            game.AddChild(b);

            _shootCountdownSec = _shootDelaySec;
        }
    }
}
