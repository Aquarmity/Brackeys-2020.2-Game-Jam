using Godot;
using System;

public class Emblem : Sprite
{

    public Player myPlayer;
    public override void _Ready()
    {
        myPlayer = GetNode<Player>("/root/game/Player");
        GetNode<Area2D>("Area2D").Connect("body_entered", myPlayer, "_onGetEmblem");
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_onGetEmblem");
    }
    public void _onGetEmblem(Node body)
    {
        QueueFree();
    }
}
