using Godot;
using System;

public class BaseGridMoveable : KinematicBody2D
{
	public static int gridSize = 16;
	
	public Vector2 gridPosition;
	
	private Vector2 newPosition;
	public float maxSpeed = 16.0f;
	
	public override void _Ready()
	{
		gridPosition = Position / 16;
		newPosition = Position;
	}
	
	public override void _Process(float delta)
	{
		var xLerp = Mathf.Lerp(Position.x, newPosition.x, 0.5f);
		var yLerp = Mathf.Lerp(Position.y, newPosition.y, 0.5f);
		Position = new Vector2(xLerp, yLerp);
		
	}
	
	// returns false if there is a collision
	public bool MoveGrid(int x, int y)
	{
		var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		
		// check the spot to move to
		var result = spaceState.IntersectRay(new Vector2(x * gridSize + 1,y * gridSize + 1),new Vector2(x * 16 + 2,y * 16 + 2));
		
		if (result.Count == 0) // if there is no one there
		{
			newPosition = new Vector2(x * gridSize, y * gridSize);
			gridPosition = new Vector2(x, y);
			return true;
		}
		return false;
		
	}
	
	// returns false if there is a collision
	public bool MoveGridRelative(int x, int y) 
	{
		return MoveGrid(x + (int)gridPosition.x,y + (int)gridPosition.y);
	}

}
