using Godot;
using System;

public class GameOverScreen : CanvasLayer
{
    [Signal]
    public delegate void PlayAgain();

    const string BTN_PATH = "PanelContainer/MarginContainer/CenterContainer/VBoxContainer/MarginContainer/VBoxContainer/";

    private Button _playAgainBtn;
    private Button _quitBtn;

    public override void _Ready()
    {
        _playAgainBtn = GetNode<Button>(BTN_PATH + "PlayAgain");
        _quitBtn = GetNode<Button>(BTN_PATH + "Quit");
    }

    public void OnPlayAgainPressed()
    {
        EmitSignal(nameof(PlayAgain));
    }

    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
