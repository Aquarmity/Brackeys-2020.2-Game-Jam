using Godot;
using System;

public class Player : BaseGridMoveable
{
	[Signal]
	public delegate void chestTest(Vector2 pos);

	public float maxHealth = 100;
	private float health;
	[Export]
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health < 0)
			{
				health = 0;
				Die();
			}
			ColorRect fill = GetNode<ColorRect>("CanvasLayer/HealthBar/ColorRect");
			fill.RectScale = new Vector2(health/maxHealth, 1);
		}
	}
	
	public override void _Ready()
	{
		Health = 90;
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

			if (((Node2D)result["collider"]).HasMethod("SubtractHeart"))
			{
				((Monster)result["collider"]).SubtractHeart();
			}
			return false;
		}
		
		return true;
	}
	public void TakeLaserDamage(float damage)
	{
		Health -= damage;
	}

	public void Die()
	{
		QueueFree();
	}
}
