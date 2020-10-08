using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 200;
    const int JUMP_HEIGHT = 400;
    public Vector2 UP = Vector2.Up;
    private Vector2 motion = new Vector2(0, 0);

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        motion.x = 0;
        motion.y = GRAVITY; // gravity
        if (Input.IsActionPressed("ui_right"))
        {
            motion.x += SPEED;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            motion.x -= SPEED;
        }

        //if (this.IsOnFloor() && Input.IsActionPressed("ui_up"))
        if (this.IsOnFloor())
        {
            if (Input.IsActionPressed("ui_up"))
            {
                motion.y = -JUMP_HEIGHT;
            }
        }

        motion = this.MoveAndSlide(motion, UP);
    }
}
