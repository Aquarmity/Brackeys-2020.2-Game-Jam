using Godot;
using System;

public class Player : BaseGridMoveable
{

	public override void _Ready()
	{
		base._Ready();
	}
	public override void _Process(float delta)
	{
		base._Process(delta);
	}
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("mv_up"))
		{
			MoveGridRelative(0,-1);
		}
		if (inputEvent.IsActionPressed("mv_down"))
		{
			MoveGridRelative(0,1);
		}
		if (inputEvent.IsActionPressed("mv_left"))
		{
			MoveGridRelative(-1,0);
		}
		if (inputEvent.IsActionPressed("mv_right"))
		{
			MoveGridRelative(1,0);
		}
	}
}
