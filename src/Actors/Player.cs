using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    const int JUMP_POWER = -350;
    public Vector2 UP = Vector2.Up;
    private Vector2 _motion;
    private AnimatedSprite _playerSprite;
    private Sprite _weaponSprite;
    private Position2D _weaponPosition;
    private Weapon _weapon;

    public override void _Ready()
    {
        _playerSprite = GetNode<AnimatedSprite>("PlayerSprite");
        _playerSprite.Play("idle");

        _weapon = null;
        _weaponSprite = null;
        _weaponPosition = GetNode<Position2D>("WeaponPosition");
    }

    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;
        _motion.y += GRAVITY;
        if (Input.IsActionPressed("ui_right"))
        {
            _motion.x += SPEED;
            _playerSprite.Play("walk");
            FaceRight();
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _motion.x -= SPEED;
            _playerSprite.Play("walk");
            FaceLeft();
        }

        if (this.IsOnFloor())
        {
            if (Input.IsActionPressed("ui_up"))
            {
                _motion.y += JUMP_POWER;
            }
            else if (_motion.x == 0)
            {
                _playerSprite.Play("idle");
            }
        }
        else
        {
            _playerSprite.Play("jump");
        }

        _motion = this.MoveAndSlide(_motion, UP);
    }

    private void FaceRight()
    {
        _playerSprite.FlipH = false;
        if (_weapon != null)
        {
            _weaponSprite.Scale = new Vector2(1, 1);
            _weapon.Position = _weaponPosition.Position;
        }
    }

    private void FaceLeft()
    {
        _playerSprite.FlipH = true;
        if (_weapon != null)
        {
            _weaponSprite.Scale = new Vector2(-1, 1);
            _weapon.Position = new Vector2(-_weaponPosition.Position.x, _weaponPosition.Position.y);
        }
    }

    public void PickUpObject(Node obj)
    {
        if (obj is Weapon weapon)
        {
            weapon.Hide();
            Node game = GetTree().Root.GetNode<Node>("Game");
            game.RemoveChild(weapon);

            weapon.Scale = Vector2.One;
            weapon.SetDeferred("position", _weaponPosition.Position);
            this.AddChild(weapon);
            weapon.Show();

            _weapon = weapon;
            _weaponSprite = weapon.GetNode<Sprite>("Sprite");
        }
    }
}
