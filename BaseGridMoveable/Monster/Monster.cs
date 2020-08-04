using Godot;
using System;

public class Monster : BaseGridMoveable
{
    private PackedScene heartScene = GD.Load<PackedScene>("res://BaseGridMoveable/Monster/Heart.tscn");
    private PackedScene coinScene = GD.Load<PackedScene>("res://Coin/Coin.tscn");
    [Export]
    public int numHearts = 3;

    public Sprite[] heartArray;

    public int numCoins = 3;

    public override void _Ready()
    {
        base._Ready();
        for(int i = 0; i < numHearts; i++)
        {
            Sprite heartInstance = (Sprite)heartScene.Instance();
            heartInstance.Position = new Vector2(i * 3, 0);
            heartInstance.Name = i.ToString();
            AddChild(heartInstance);
        }
    }

    public void AddHeart()
    {
        
        Sprite heartInstance = (Sprite)heartScene.Instance();
        heartInstance.Position = new Vector2((numHearts) * 3, 0);
        heartInstance.Name = numHearts.ToString();
        AddChild(heartInstance);
        numHearts += 1;
    }

    public void SubtractHeart()
    {
        numHearts -= 1;
        Sprite rightHeart = GetNode<Sprite>(numHearts.ToString());
        rightHeart.QueueFree();
        if (numHearts == 0)
        {
            Die();
        }
    }

    public void AddHearts(int num)
    {
        for(int i = 0; i < num; i++)
        {
            AddHeart();
        }
    }

    public void SubtractHearts(int num)
    {
        for(int i = 0; i < num; i++)
        {
            SubtractHeart();
        }
    }

    public Godot.Collections.Dictionary Attack(int x, int y)
    {
        var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		
		// check the spot to move to
		var result = spaceState.IntersectRay(new Vector2(x * gridSize + 1,y * gridSize + 1),new Vector2(x * 16 + 2,y * 16 + 2));

        return result;
    }

    public Godot.Collections.Dictionary AttackRelative(int x, int y)
    {
        return Attack(x + (int)gridPosition.x, y + (int)gridPosition.y);
    }

    public Godot.Collections.Dictionary AttackColumn(int x, int y, bool up)
    {
        var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		var rayStart = new Vector2(16 * x + 8, 16 * y + 8);
		// check the spot to move to
        var rayEnd = new Vector2(0,0);
        if (up)
        {
            rayEnd = new Vector2(16 * x + 8, 8);
        } 
        else
        {
            rayEnd = new Vector2(16 * x + 8, 16 * 20 + 8);
        }
		var result = spaceState.IntersectRay(rayStart, rayEnd );
        return result;
    }
    public Godot.Collections.Dictionary AttackColumnRelative(int x, int y, bool up)
    {
        return AttackColumn(x + (int)gridPosition.x, y + (int)gridPosition.y, up);
    }
    public Godot.Collections.Dictionary AttackRow(int x, int y, bool left)
    {
        var spaceRid = GetWorld2d().Space;
		var spaceState = Physics2DServer.SpaceGetDirectState(spaceRid);
		var rayStart = new Vector2(16 * x + 8, 16 * y + 8);
		// check the spot to move to
        var rayEnd = new Vector2(0,0);
        if (left)
        {
            rayEnd = new Vector2(8, 16 * x + 8);
        } 
        else
        {
            rayEnd = new Vector2(16 * 20 + 8, 16 * x + 8);
        }
		var result = spaceState.IntersectRay(rayStart, rayEnd );
        return result;
    }
    public Godot.Collections.Dictionary AttackRowRelative(int x, int y, bool left)
    {
        return AttackRow(x + (int)gridPosition.x, y + (int)gridPosition.y, left);
    }

    public void Die()
    {
        for(int i = 0; i < numCoins; i++)
        {
            AnimatedSprite newCoin = (AnimatedSprite)coinScene.Instance();
            newCoin.Position = Position;
            newCoin.Set("start_pos", Position);
            GetParent().AddChild(newCoin);
        }
        QueueFree();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
