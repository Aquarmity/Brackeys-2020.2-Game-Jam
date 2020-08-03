using Godot;
using System;

public class Pyratrid : Monster
{

    private Timer MoveTimer;
    private RandomNumberGenerator rng = new RandomNumberGenerator();
    public override void _Ready()
    {
        MoveTimer = GetNode<Timer>("MoveTimer");
        MoveTimer.Connect("timeout", this, nameof(_on_Move_Timeout));
        base._Ready();
    }

    public void _on_Move_Timeout()
    {
        switch (rng.RandiRange(1,4))
        {
            case 1:
                MoveGridRelative(1,0);
                break;
            case 2:
                MoveGridRelative(-1,0);
                break;
            case 3:
                MoveGridRelative(0,1);
                break;
            case 4:
                MoveGridRelative(0,-1);
                break;
        }
    }

}
