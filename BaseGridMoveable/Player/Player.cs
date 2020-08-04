using Godot;
using System;

public class Player : BaseGridMoveable
{
	public float maxHealth = 100;
	[Export]
	public float health
	{
		get
		{
			return health;
		}
		set{
			health = value;
			ColorRect fill = GetNode<ColorRect>("CanvasLayer/Sprite/ColorRect");
			fill.RectScale = new Vector2(fill.RectScale.x, health/maxHealth);
		}
	}
	[Signal]
	public delegate void chestTest(Vector2 pos);
	
	public override void _Ready()
	{
		health = 75;
		base._Ready();
	}
	public override void _Process(float delta)
	{
		base._Process(delta);
	}
	public override void _Input(InputEvent inputEvent)
	{
		int x = 0;
		int y = 0;
		if (inputEvent.IsActionPressed("mv_up"))
		{
			y = -1;
		}
		if (inputEvent.IsActionPressed("mv_down"))
		{
			y = 1;
		}
		if (inputEvent.IsActionPressed("mv_left"))
		{
			x = -1;
		}
		if (inputEvent.IsActionPressed("mv_right"))
		{
			x = 1;
		}
		// only move one axis at a time (no diagonal movement)
		if (x != 0)
		{
			y = 0;
		}
		
		PlayerMoveGrid(MoveGridRelativeBase(x,y));
		
		
	}
	
	private bool PlayerMoveGrid(Godot.Collections.Dictionary result)
	{
		if (result.Count != 0)  // if there is something there
		{
			EmitSignal(nameof(chestTest), result["position"]); // tell the chests to check if it was them that we hit
			return false;
		}
		
		return true;
	}
}
