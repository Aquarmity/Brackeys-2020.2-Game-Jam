using Godot;
using System;

public class ArrowButton : TextureButton
{
    [Export]
    public int number = 0;
    [Export]
    public bool rotated = false;
    public AudioStreamPlayer soundManager;
    public Grid theGrid;
    public override void _Ready()
    {
        soundManager = GetNode<AudioStreamPlayer>("/root/SoundManager");
        Connect("pressed", this, nameof(_onButtonPressed));
        theGrid = GetParent().GetParent().GetParent().GetParent().GetNode<Grid>("Grid");
    }
    public void _onButtonPressed()
    {
        if(GetNodeOrNull("/root/game/Camera2D") != null) {
            GetNodeOrNull("/root/game/Camera2D").Call("add_trauma", 0.25);
        }
        soundManager.Call("PlayAffect", "res://Sounds/Untitled.wav");
        if (rotated) 
        {
            theGrid.ShiftColumnDown(number);
            theGrid.SetUpMenuGrid();
        }
        else
        {
            theGrid.ShiftRowRight(number);
            theGrid.SetUpMenuGrid();
        }
    }
}
