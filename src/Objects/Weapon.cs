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
            Console.WriteLine("player picked me up");
            player.PickUpObject(this);
        }
    }
}
