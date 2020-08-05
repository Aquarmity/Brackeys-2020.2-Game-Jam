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
			ColorRect fill = GetNode<ColorRect>("CanvasLayer/Sprite/ColorRect");
			fill.RectScale = new Vector2(health/maxHealth, 1);
		}
	}
	Grid myGrid;
	public override void _Ready()
	{
		base._Ready();
		myGrid = GetParent().GetNode<Grid>("Grid");
	}
	public override void _Process(float delta)
	{
		base._Process(delta);
		checkForRoomChange();
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
	private void checkForRoomChange()
	{
		if (Position.x < 0) {
			myGrid.moveLeft();
			UpdatePos(new Vector2(GetViewportRect().Size.x - 16, Position.y));
		}
		if (Position.x > GetViewportRect().Size.x - 16) 
		{
			myGrid.moveRight();
			UpdatePos(new Vector2(0, Position.y));
		}
		if (Position.y < 0) {
			myGrid.moveUp();
			UpdatePos(new Vector2(Position.x, GetViewportRect().Size.y - 16));
		}
		if (Position.y > GetViewportRect().Size.y - 16)
		{
			myGrid.moveDown();
			UpdatePos(new Vector2(Position.x, 0));
		}
		
	}

	private void UpdatePos(Vector2 newPos)
	{
		Position = newPos;
		newPosition = Position;
		gridPosition.x = Position.x / 16;
		gridPosition.y = Position.y / 16;
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
