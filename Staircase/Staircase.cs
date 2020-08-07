using Godot;
using System;

public class Staircase : Sprite
{
    public Player myPlayer;
    public override void _Ready()
    {
        myPlayer = GetNode<Player>("/root/game/Player");
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_onBodyEntered");
    }
    public void _onBodyEntered(Node body)
    {
        if(body is Player)
        {
            if(((Player)body).hasEmblem) {
                myPlayer.gameEnd = true;
            }
        }
    }
}
