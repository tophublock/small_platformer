using Godot;
using System;

public class Weapon : Area2D
{
    public override void _Ready()
    {
        
    }
    public override void _Process(float delta)
    {

    }

    public void OnWeaponBodyEntered(Node body)
    {
        if (body is Player player)
        {
            // Make player pick up weapon
            Console.WriteLine("player picked me up");
        }
    }
}
