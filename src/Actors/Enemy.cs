using Godot;
using System;

public class Enemy : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    public Vector2 UP = Vector2.Up; // const

    public int Health = 2;
    public Vector2 Direction = Vector2.Left;
    private Vector2 _motion;
    private AnimatedSprite _sprite;
    private RayCast2D _rayCast;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _rayCast = GetNode<RayCast2D>("RayCast2D");
    }

    public override void _PhysicsProcess(float delta)
    {
        _sprite.Play("walk");
        _motion.x = SPEED * Direction.x;
        _motion.y += GRAVITY;
        _motion = this.MoveAndSlide(_motion, UP);

        if (this.IsOnWall() || !_rayCast.IsColliding()) {
            FlipHDirection();
        }
        ProcessCollisions();
    }

    // Flip RayCast and sprite when enemy changes directions
    private void FlipHDirection()
    {
        _sprite.FlipH = !_sprite.FlipH;
        Direction.x *= -1;
        _rayCast.Position = new Vector2(-1 *_rayCast.Position.x + 2.5f, _rayCast.Position.y);
    }

    // If enemy collides with player, hit player
    private void ProcessCollisions()
    {
        for (int i = 0; i < GetSlideCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.Collider is Player player)
            {
                player.Hit();
            }
        }
    }

    public void Hit()
    {
        Health--;
        if (Health == 0)
        {
            // Die
        }
    }
}
