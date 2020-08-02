using Godot;
using System;

public class TreasureChest : StaticBody2D
{
	AnimatedSprite sprite;
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite>("AnimatedSprite");
		var players = GetTree().GetNodesInGroup("Player");
		foreach(KinematicBody2D player in players)
		{
			player.Connect("chestTest", this, nameof(_on_Chest_Test) );
		}
	}
	public void _on_Chest_Test(Vector2 pos)
	{
		if((int)(pos.x/16) == Position.x/16) // check if both points can be rounded to the current grid cell
		{
			if ((int)(pos.y/16) == Position.y/16)
			{
				sprite.Frame = 1; // open treasure chest
			}
		}
	}
}
