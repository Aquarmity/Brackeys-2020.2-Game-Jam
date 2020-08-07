using Godot;
using System;

public class TreasureChest : StaticBody2D
{
	private PackedScene coin = GD.Load<PackedScene>("res://Coin/Coin.tscn");
	float goldBrightness = 1.27f;

	AnimatedSprite sprite;
	Light2D goldShine;
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite>("AnimatedSprite");
		goldShine = GetNode<Light2D>("Light2D");
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
				OpenChest();
			}
		}
	}

	public void OpenChest()
	{
		if (sprite.Frame == 1) {
			return;
		}
		ZIndex = 101;

		sprite.Frame = 1; // open treasure chest
		goldShine.Energy = goldBrightness;

		for(int i = 0; i < 4; i++)
		{
			var coinInstance = (AnimatedSprite)coin.Instance();
			AddChild(coinInstance);

		}
	}
}
