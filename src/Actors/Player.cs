using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    const int JUMP_POWER = -350;
    public Vector2 UP = Vector2.Up;
    private Vector2 _motion;
    private AnimatedSprite _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _sprite.Play("idle");
    }

    public override void _PhysicsProcess(float delta)
    {
        _motion.x = 0;
        _motion.y += GRAVITY;
        if (Input.IsActionPressed("ui_right"))
        {
            _motion.x += SPEED;
            _sprite.Play("walk");
            _sprite.FlipH = false;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _motion.x -= SPEED;
            _sprite.Play("walk");
            _sprite.FlipH = true;
        }

        if (this.IsOnFloor())
        {
            Console.WriteLine("on floor");
            if (Input.IsActionPressed("ui_up"))
            {
                _motion.y += JUMP_POWER;
            }
            else if (_motion.x == 0)
            {
                _sprite.Play("idle");
            }
        }
        else
        {
            Console.WriteLine("not on floor");
            _sprite.Play("jump");
        }

        _motion = this.MoveAndSlide(_motion, UP);
    }
}
