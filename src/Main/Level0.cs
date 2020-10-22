using Godot;
using System;

public class Level0 : Node
{

    private HUD _hud;
    private Player _player;

    public override void _Ready()
    {
        _hud = GetNode<HUD>("HUD");
        _player = GetNode<Player>("Player");

        _hud.UpdateLives(_player.Health);
    }

    public void OnPlayerUpdateStats(int lives)
    {
        _hud.UpdateLives(lives);
    }
}
