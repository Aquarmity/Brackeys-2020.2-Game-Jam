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
		newPosition = gridPosition;
	}
	
	public override void _Process(float delta)
	{
		var xLerp = Mathf.Lerp(Position.x, newPosition.x, 0.5f);
		var yLerp = Mathf.Lerp(Position.y, newPosition.y, 0.5f);
		Position = new Vector2(xLerp, yLerp);
		
	}
	
	public bool MoveGrid(int x, int y)
	{
		var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		
		var result = spaceState.IntersectRay(new Vector2(x * 16 + 1,y * 16 + 1),new Vector2(x * 16 + 2,y * 16 + 2));
		
		if (result.Count == 0)
		{
			newPosition = new Vector2(x * 16, y * 16);
			gridPosition = new Vector2(x, y);
			return true;
		}
		return false;
		
	}
	public bool MoveGridRelative(int x, int y) 
	{
		return MoveGrid(x + (int)gridPosition.x,y + (int)gridPosition.y);
	}

}
