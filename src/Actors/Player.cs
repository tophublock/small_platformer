using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    const int JUMP_POWER = -350;
    public Vector2 UP = Vector2.Up;
    private Vector2 _motion;
    private AnimatedSprite _player;
    private Sprite _weapon;

    public override void _Ready()
    {
        _player = GetNode<AnimatedSprite>("PlayerSprite");
        _player.Play("idle");

        _weapon = GetNode<Sprite>("Weapon/WeaponSprite");
        //_weapon.Hide();
    }

    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;
        _motion.y += GRAVITY;
        if (Input.IsActionPressed("ui_right"))
        {
            _motion.x += SPEED;
            _player.Play("walk");
            _player.FlipH = false;
            _weapon.Scale = new Vector2(1, 1);
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _motion.x -= SPEED;
            _player.Play("walk");
            _player.FlipH = true;
            _weapon.Scale = new Vector2(-1, 1);
        }

        if (this.IsOnFloor())
        {
            if (Input.IsActionPressed("ui_up"))
            {
                _motion.y += JUMP_POWER;
            }
            else if (_motion.x == 0)
            {
                _player.Play("idle");
            }
        }
        else
        {
            _player.Play("jump");
        }

        _motion = this.MoveAndSlide(_motion, UP);
    }
}
