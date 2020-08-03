using Godot;
using System;

public class Monster : BaseGridMoveable
{
    private PackedScene heartScene = GD.Load<PackedScene>("res://BaseGridMoveable/Monster/Heart.tscn");

    [Export]
    public int numHearts = 3;

    public Sprite[] heartArray;


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
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
