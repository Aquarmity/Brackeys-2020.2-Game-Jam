using Godot;
using System;

public class Pyratrid : Monster
{

    private Timer MoveTimer;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    private PackedScene laserScene = GD.Load<PackedScene>("res://BaseGridMoveable/Monster/Pyratrid/Laser.tscn");
    
    public override void _Ready()
    {
        rng.Randomize();
        MoveTimer = GetNode<Timer>("MoveTimer");
        MoveTimer.Connect("timeout", this, nameof(_on_Move_Timeout));
        base._Ready();
    }

    public void _on_Move_Timeout()
    {
        var randomnum = rng.RandiRange(1,4);
        RandMove(randomnum);
        LaserAttack(randomnum);
    }

    public void RandMove(int dir)
    {
        switch (dir)
        {
            case 1:
                MoveGridRelative(1,0);
                break;
            case 2:
                MoveGridRelative(-1,0);
                break;
            case 3:
                MoveGridRelative(0,1);
                break;
            case 4:
                MoveGridRelative(0,-1);
                break;
        }
    }
    public void LaserAttack(int dir)
    {
        switch(dir)
        {
            case 1:
                ShootLaser(false, 1);
                break;
            case 2:
                ShootLaser(false, -1);
                break;
            case 3:
                ShootLaser(true, 1);
                break;
            case 4:
                ShootLaser(true, -1);
                break;
        }
    }  
    private void ShootLaser(bool isYDir, int pos)
    {
        Vector2 attackLoc = new Vector2(0,0);
        
        for(int i = 0; i < 20; i++)
        {
            if (isYDir)
            {
                attackLoc.x = gridPosition.x;
                attackLoc.y = gridPosition.y + pos * (i + 1);
            } 
            else {
                attackLoc.x = gridPosition.x + pos * (i + 1);
                attackLoc.y = gridPosition.y;
            }
            var result = Attack((int)attackLoc.x, (int)attackLoc.y);
            if (result.Count != 0) {
                if (((Node2D)result["collider"]).GetNode<LightOccluder2D>("LightOccluder2D") != null)
                {
                    break;
                }
            }
            SpawnLaser(attackLoc * 16, isYDir );
        }
    }
    private void SpawnLaser(Vector2 spawnLaserPosition, bool rot)
    {
        var newLaser = (Area2D)laserScene.Instance();
        newLaser.Position = spawnLaserPosition;
        newLaser.Set("rot", rot);
        GetParent().AddChild(newLaser);
    }
}
