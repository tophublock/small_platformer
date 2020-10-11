using Godot;
using System;

public class Player : KinematicBody2D
{
    const int GRAVITY = 20;
    const int SPEED = 125;
    const int JUMP_POWER = -350;
    public Vector2 UP = Vector2.Up;
    private bool _isFacingRight;
    private Vector2 _motion;
    private AnimatedSprite _playerSprite;
    private Sprite _weaponSprite;
    private Position2D _weaponPosition;
    private Weapon _weapon;

    public override void _Ready()
    {
        _isFacingRight = true;
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
            if (!_isFacingRight)
            {
                FaceRight();
            }
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _motion.x -= SPEED;
            _playerSprite.Play("walk");
            if (_isFacingRight)
            {
                FaceLeft();
            }
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

        if (Input.IsKeyPressed((int)KeyList.Space) && _weapon != null)
        {
            Console.WriteLine("shoot");
            _weapon.Shoot();
        }

        _motion = this.MoveAndSlide(_motion, UP);
    }

    private void FaceRight()
    {
        Console.WriteLine("facing right");
        _isFacingRight = true;
        _playerSprite.FlipH = false;
        if (_weapon != null)
        {
            _weaponSprite.Scale = new Vector2(1, 1);
            _weapon.Position = _weaponPosition.Position;
        }
    }

    private void FaceLeft()
    {
        Console.WriteLine("facing left");
        _isFacingRight = false;
        _playerSprite.FlipH = true;
        if (_weapon != null)
        {
            _weaponSprite.Scale = new Vector2(-1, 1);
            _weapon.Position = new Vector2(-_weaponPosition.Position.x, _weaponPosition.Position.y);
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
}
