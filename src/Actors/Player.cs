using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    const int HIT_POWER = 75;
    const int JUMP_POWER = -350;
    public Vector2 UP = Vector2.Up; // const
    
    public int Health = 5;
    public Vector2 Direction;
    private bool _isHit = false;
    private Vector2 _motion;
    private AnimatedSprite _playerSprite;
    private Sprite _weaponSprite;
    private Position2D _weaponPosition;
    private Weapon _weapon;

    public override void _Ready()
    {
        Direction = Vector2.Right;
        _playerSprite = GetNode<AnimatedSprite>("PlayerSprite");
        _playerSprite.Play("idle");

        _weapon = null;
        _weaponSprite = null;
        _weaponPosition = GetNode<Position2D>("WeaponPosition");
    }

    public override void _PhysicsProcess(float delta)
    {
        ProcessState();

        _motion.x = 0;
        _motion.y += GRAVITY;
        if (_isHit)
        {
            _motion.x -= HIT_POWER * Direction.x;
        }
        else
        {
            HandleWalking();
            HandleJumping();
            HandleShooting();
        }

        _motion = this.MoveAndSlide(_motion, UP);
    }

    // Take care of any player states that need to expire
    private void ProcessState()
    {
        if (_playerSprite.Animation == "hit")
        {
            if (_playerSprite.Frame == _playerSprite.Frames.GetFrameCount("hit") - 1)
            {
                _isHit = false;
            }
        }
    }

    private void HandleWalking()
    {
        if (Input.IsActionPressed("ui_right"))
        {
            _motion.x += SPEED;
            PlayAnimation("walk");
            if (Direction != Vector2.Right)
            {
                FlipHDirection();
            }
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _motion.x -= SPEED;
            PlayAnimation("walk");
            if (Direction == Vector2.Right)
            {
                FlipHDirection();
            }
        }
    }

    private void HandleJumping()
    {
        if (this.IsOnFloor())
        {
            if (Input.IsActionPressed("ui_up"))
            {
                _motion.y += JUMP_POWER;
            }
            else if (_motion.x == 0)
            {
                PlayAnimation("idle");
            }
        }
        else
        {
            PlayAnimation("jump");
        }
    }

    private void HandleShooting()
    {
        if (Input.IsActionJustPressed("ui_select") && _weapon != null)
        {
            Console.WriteLine("shoot");
            _weapon.Shoot();
        }
    }

    private void FlipHDirection()
    {
        Console.WriteLine("flip direction");
        Direction.x *= -1;
        _playerSprite.FlipH = !_playerSprite.FlipH;

        // Flip weapon sprite
        if (_weapon != null)
        {
            _weapon.Direction.x *= -1;
            _weapon.Position = new Vector2(_weaponPosition.Position.x * Direction.x, _weaponPosition.Position.y);
            _weaponSprite.Scale = new Vector2(Direction.x, 1);
        }
    }

    // Handler for playing animations
    // Always let "hit" interrupt animations
    // Always let "hit" finish
    private void PlayAnimation(string animation)
    {
        string currAnimation = _playerSprite.Animation;
        if (animation == "hit")
        {
            _playerSprite.Play(animation);
            return;
        }

        if (currAnimation == "hit")
        {
            Console.WriteLine(_playerSprite.Frame);
            Console.WriteLine(_playerSprite.Frames.GetFrameCount("hit"));
            if (_playerSprite.Frame == _playerSprite.Frames.GetFrameCount("hit") - 1)
            {
                _playerSprite.Play(animation);
                _isHit = false;
            }
        }
        else
        {
            _playerSprite.Play(animation);
        }
    }
    public void PickUpObject(Node obj)
    {
        Console.WriteLine("picked up object");
        if (obj is Weapon weapon)
        {
            weapon.Hide();
            this.CallDeferred(nameof(AdoptChild), weapon);
            weapon.Scale = Vector2.One;
            weapon.Position = _weaponPosition.Position;
            weapon.Show();

            _weapon = weapon;
            _weaponSprite = weapon.GetNode<Sprite>("Sprite");
        }
    }

    // https://godotengine.org/qa/1754/how-to-change-the-parent-of-a-node-from-gdscript
    // Might be better to use _Process and a flag to reparent
    private void AdoptChild(Node child)
    {
        Node parent = child.GetParent();
        parent.RemoveChild(child);
        this.AddChild(child);
    }

    public void Hit()
    {
        Console.WriteLine("hit");
        _isHit = true;
        Health--;
        PlayAnimation("hit");
        if (Health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Fade away player
        //QueueFree();
    }
}
