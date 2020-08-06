using Godot;
using System;

public class ArrowButton : TextureButton
{
    [Export]
    public int number = 0;
    [Export]
    public bool rotated = false;
    public Grid theGrid;
    public override void _Ready()
    {
        Connect("pressed", this, nameof(_onButtonPressed));
        theGrid = GetParent().GetParent().GetParent().GetParent().GetNode<Grid>("Grid");
    }

    public void _onButtonPressed()
    {
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
