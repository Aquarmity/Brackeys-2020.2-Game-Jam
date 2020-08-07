using Godot;
using System;

public class Player : BaseGridMoveable
{
	public bool hasEmblem = false;
	public bool gameEnd = false;
	[Signal]
	public delegate void chestTest(Vector2 pos);

	private PackedScene myEmblem = GD.Load<PackedScene>("res://BaseGridMoveable/Player/EmblemGUI.tscn");

	private PackedScene playerSlash = GD.Load<PackedScene>("res://BaseGridMoveable/Player/PlayerSlash.tscn");
	

	private PackedScene endSceneLose = GD.Load<PackedScene>("res://GameEndLose/GameEndLose.tscn");
	private PackedScene endSceneWin = GD.Load<PackedScene>("res://GameEndWin/GameEndWin.tscn");
	public AnimatedSprite currectSlash = null;
	
	public bool attacking = false;

	public float maxHealth = 100;
	private float health;
	public Timer myInputTimer;
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
		Health = 100;
		base._Ready();
		myGrid = GetParent().GetNode<Grid>("Grid");
		myInputTimer = GetNode<Timer>("InputTimer");
		myInputTimer.Connect("timeout", this, nameof(_onInputTimeout));
	}
	public override void _Process(float delta)
	{
		base._Process(delta);
		checkForRoomChange();
		if (gameEnd) {
			GD.Print("GAME OVER");
			var globalvariables = (GlobalValues)GetNode("/root/GlobalValues");
			globalvariables.score = GetNode<Label>("CanvasLayer/Sprite2/Label").Text;
	
			GetTree().ChangeSceneTo(endSceneWin);
		}
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
		if(myInputTimer.IsStopped()) {
			myInputTimer.Start();
		}
		
	}

	int lastDirection = 0;

	public void _onInputTimeout()
	{
		bool changeInput = false;
		int x = 0;
		int y = 0;
		bool anyInput = false;
		if (Input.IsActionPressed("mv_up"))
		{
			y = -1;
			anyInput = true;
			if (lastDirection != 1 && lastDirection != 0) {
				changeInput = true;
			}
			lastDirection = 1;
		}
		if (Input.IsActionPressed("mv_down"))
		{
			y = 1;
			anyInput = true;
			if(lastDirection != 2 && lastDirection != 0) {
				changeInput = true;
			}
			lastDirection = 2;
		}
		if (Input.IsActionPressed("mv_left"))
		{
			x = -1;
			anyInput = true;
			if (lastDirection != 3 && lastDirection != 0) {
				changeInput = true;
			}
			lastDirection = 3;
		}
		if (Input.IsActionPressed("mv_right"))
		{
			x = 1;
			anyInput = true;
			if (lastDirection != 4 && lastDirection != 0) {
				changeInput = true;
			}
			lastDirection = 4;
		}
		// only move one axis at a time (no diagonal movement)
		if (x != 0)
		{
			y = 0;
		}
		
		if (!changeInput) {
			PlayerMoveGrid(MoveGridRelativeBase(x,y));
		} else {
			lastDirection = 0;
		}
		GD.Print(anyInput);
		if(!anyInput)
		{
			lastDirection = 0;
			myInputTimer.Stop();
		}
	}
	private void checkForRoomChange()
	{
		if (Position.x < 0) {
			if (myGrid.moveLeft())
			{
				UpdatePos(new Vector2(GetViewportRect().Size.x - 16, Position.y));
			} else
			{
				PlayerMoveGrid(MoveGridRelativeBase(1,0));
			}
		}
		if (Position.x > GetViewportRect().Size.x - 16) 
		{
			if (myGrid.moveRight())
			{
				UpdatePos(new Vector2(0, Position.y));
			} else
			{
				PlayerMoveGrid(MoveGridRelativeBase(-1,0));
			}
		}
		if (Position.y < 0) {
			if (myGrid.moveUp())
			{
				UpdatePos(new Vector2(Position.x, GetViewportRect().Size.y - 16));
			} else
			{
				PlayerMoveGrid(MoveGridRelativeBase(0,1));
			}
		}
		if (Position.y > GetViewportRect().Size.y - 16)
		{
			if (myGrid.moveDown())
			{
				UpdatePos(new Vector2(Position.x, 0));
			} else
			{
				PlayerMoveGrid(MoveGridRelativeBase(0,-1));
			}
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

			if (((Node2D)result["collider"]).HasMethod("SubtractHeart") && !attacking)
			{
				((Monster)result["collider"]).SubtractHeart();
				attacking = true;
				currectSlash = (AnimatedSprite)playerSlash.Instance();
				GetParent().AddChild(currectSlash);
				currectSlash.Playing = true;
				currectSlash.Frame = 0;
				currectSlash.Position = ((Monster)result["collider"]).Position;
				currectSlash.Connect("animation_finished", this, nameof(_onSlashEnd));
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
		GetNode<AnimatedSprite>("AnimatedSprite").Hide();
		GetTree().ChangeSceneTo(endSceneLose);
	}

	public void _onSlashEnd()
	{
		currectSlash.QueueFree();
		attacking = false;
		currectSlash = null;
	}

	public void _onGetEmblem(Node body) 
	{
		if (body is Player) {
			hasEmblem = true;
			Sprite myEmblemInstance = (Sprite)myEmblem.Instance();
			GetNode<CanvasLayer>("CanvasLayer").AddChild(myEmblemInstance);
		}
	}
}
