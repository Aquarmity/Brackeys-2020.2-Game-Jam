using Godot;
using System;

public class Room : Node2D
{
    [Export]
    public int[] coord = {0,0};
    public int frameSymbol = 15;

    public Grid theGrid;
    
    [Export]
    public bool left = true;
    [Export]
    public bool right = true;
    [Export]
    public bool up = true;
    [Export]
    public bool down = true;


    public override void _Ready()
    {
        calculateSymbol();
        theGrid = (Grid)GetParent();
    }

    public void calculateSymbol()
    {
        if (left)
        {
            if (right)
            {
                if (up)
                {
                    if (down)
                    {
                        frameSymbol = 15;
                    } 
                    else{
                        frameSymbol = 11;
                    }
                }
                else
                {
                    if (down)
                    {
                        frameSymbol = 13;
                    } 
                    else{
                        frameSymbol = 6;
                    }
                }
            }
            else{
                if (up)
                {
                    if (down)
                    {
                        frameSymbol = 14;
                    } 
                    else{
                        frameSymbol = 7;
                    }
                }
                else
                {
                    if (down)
                    {
                        frameSymbol = 10;
                    } 
                    else{
                        frameSymbol = 4;
                    }
                }
            }
        }
        else
        {
            if (right)
            {
                if (up)
                {
                    if (down)
                    {
                        frameSymbol = 12;
                    } 
                    else{
                        frameSymbol = 8;
                    }
                }
                else
                {
                    if (down)
                    {
                        frameSymbol = 9;
                    } 
                    else{
                        frameSymbol = 2;
                    }
                }
            }
            else{
                if (up)
                {
                    if (down)
                    {
                        frameSymbol = 5;
                    } 
                    else{
                        frameSymbol = 1;
                    }
                }
                else
                {
                    if (down)
                    {
                        frameSymbol = 3;
                    } 
                    else{
                        frameSymbol = 0;
                    }
                }
            }
        }
    }

    public void _onChangeRooms()
    {
        foreach(Room eachRoom in theGrid.GetChildren())
        {
            if(Name == eachRoom.Name)
            {
                theGrid.RemoveChild(this);        
            }
        }      
    }

}
