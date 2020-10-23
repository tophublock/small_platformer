using Godot;
using System;

public class Level : Node
{
    [Export]
    public string levelName;
    private HUD _hud;
    private Player _player;
    private PackedScene _gameOverScreen;

    public override void _Ready()
    {
        _hud = GetNode<HUD>("HUD");
        _player = GetNode<Player>("Player");
        _gameOverScreen = ResourceLoader.Load("res://src/UserInterface/GameOverScreen.tscn") as PackedScene;

        _hud.UpdateLives(_player.Health);
    }

    public void OnPlayerUpdateStats(int lives)
    {
        _hud.UpdateLives(lives);
    }

    public void OnPlayerEndGame()
    {
        var gameOver = _gameOverScreen.Instance() as GameOverScreen;
        AddChild(gameOver);

        gameOver.Connect("PlayAgain", this, nameof(Restart));
    }

    private void Restart()
    {
        GetTree().ChangeScene($"res://src/Main/{levelName}.tscn");
    }
}
