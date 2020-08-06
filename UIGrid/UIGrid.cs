using Godot;
using System;

public class UIGrid : Node2D
{
    public bool hidden = true;

    public AnimatedSprite playerBlinker;
    public Grid theGrid = null;
    
    public override void _Ready()
    {
        playerBlinker = GetNode<AnimatedSprite>("PlayerBlinker");
        theGrid = GetParent().GetParent().GetNode<Grid>("Grid");
        UpdatePlayerBlinker();
    }

    public void UpdatePlayerBlinker()
    {
        if (theGrid == null) {
            theGrid = GetParent().GetParent().GetNode<Grid>("Grid");
        }
        string myPath = "RoomGrid/GridContainer/Control" + theGrid.currentY.ToString() + theGrid.currentX.ToString() + "/RoomSymbol";
        playerBlinker.GlobalPosition = GetNode<AnimatedSprite>(myPath).GlobalPosition;
    }


    public override void _Input(InputEvent myInput)
    {
        if(myInput.IsActionPressed("open_menu"))
        {
            if (hidden)
            {
                Show();
                hidden = false;
                UpdatePlayerBlinker();
            }
            else
            {
                Hide();
                hidden = true;
            }
        }
    }
}
