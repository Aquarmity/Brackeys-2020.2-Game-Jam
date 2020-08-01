using Godot;
using System;

public class BaseGridMoveable : KinematicBody2D
{
	public static int gridSize = 16;
	
	public Vector2 gridPosition = new Vector2(0, 0);
	
	private Vector2 newPosition = new Vector2(0, 0);
	
	
	public override void _Ready()
	{
		MoveGrid(0,0);
	}
	
	public override void _Process(float delta)
	{
		var xLerp = Mathf.Lerp(Position.x, newPosition.x, 0.1f);
		var yLerp = Mathf.Lerp(Position.y, newPosition.y, 0.1f);
		Position = new Vector2(xLerp, yLerp);
		
		MoveGridRelative(1,0);
	}
	
	public bool MoveGrid(int x, int y)
	{
		var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		
		var result = spaceState.IntersectRay(new Vector2(x * 16,y * 16) - new Vector2(1,1),new Vector2(x * 16,y * 16) + new Vector2(1,1));
		
		if (result.Count == 0)
		{
			newPosition = new Vector2(x * 16, y * 16);
			return true;
		}
		return false;
		
	}
	public bool MoveGridRelative(int x, int y) 
	{
		return MoveGrid((int)gridPosition.x + x, (int)gridPosition.y + y);
	}

}
