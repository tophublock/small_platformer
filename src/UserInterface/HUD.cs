using Godot;
using System;

public class HUD : CanvasLayer
{
    private Label _livesLabel;
    public override void _Ready()
    {
        _livesLabel = GetNode<Label>("MarginContainer/Panel/CenterContainer/MarginContainer/HBoxContainer/HBoxContainer/Lives");
    }

    public void UpdateLives(int lives)
    {
        _livesLabel.Text = lives.ToString();
    }
}
