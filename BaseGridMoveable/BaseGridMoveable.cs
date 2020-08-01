using Godot;
using System;

public class BaseGridMoveable : KinematicBody2D
{
	public static int gridSize = 16;
	
	public Vector2 gridPosition = new Vector2(0, 0);
	
	private Vector2 newPosition = new Vector2(0, 0);
	public float maxSpeed = 16.0f;
	
	public override void _Ready()
	{
		gridPosition = new Vector2(Position.x / gridSize,Position.y / gridSize);
		MoveGridRelative(2,2);
	}
	
	public override void _Process(float delta)
	{
		var xLerp = Mathf.Lerp(Position.x, newPosition.x, 0.1f);
		var yLerp = Mathf.Lerp(Position.y, newPosition.y, 0.1f);
		Position = new Vector2(xLerp, yLerp);
		
	}
	
	public bool MoveGrid(int x, int y)
	{
		var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		
		var result = spaceState.IntersectRay(new Vector2(x * 16,y * 16) - new Vector2(1,1),new Vector2(x * 16,y * 16) + new Vector2(1,1));
		
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
