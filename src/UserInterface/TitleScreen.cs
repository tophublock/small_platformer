using Godot;
using System;

public class TitleScreen : Control
{

    const string BUTTON_PARENT_PATH = "TitleContent/VBoxContainer/CenterContainer/VBoxContainer/";
    private Button startBtn;
    private Button settingsBtn;
    private Button quitBtn;
    public override void _Ready()
    {
        startBtn = GetNode<Button>(BUTTON_PARENT_PATH + "Start");
        settingsBtn = GetNode<Button>(BUTTON_PARENT_PATH + "Settings");
        quitBtn = GetNode<Button>(BUTTON_PARENT_PATH + "Quit");
    }
    public override void _PhysicsProcess(float delta)
    {
        //
    }

    public void OnStartPressed()
    {
        // Load Game
        GetTree().ChangeScene("res://src/Main/Level0.tscn");
    }

    public void OnSettingsPressed()
    {
        // Load settings
    }
    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
