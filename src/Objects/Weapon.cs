using Godot;
using System;

public class Weapon : Area2D
{
    public Vector2 Direction = Vector2.Right;
    private Position2D _bulletStartPosition;
    private Position2D _bulletStartPositionMirrored;
    private PackedScene _bulletScene;

    public override void _Ready()
    {
        _bulletStartPosition = GetNode<Position2D>("BulletPosition");
        _bulletStartPositionMirrored = new Position2D();
        _bulletStartPositionMirrored.Position = new Vector2(-_bulletStartPosition.Position.x, _bulletStartPosition.Position.y);

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
        b.Position = (b.Direction == Vector2.Right) ? _bulletStartPosition.Position : _bulletStartPositionMirrored.Position;
        b.Position += parent.Position + this.Position;

        Node game = GetTree().Root.GetNode("Game");
        game.AddChild(b);
    }
}
