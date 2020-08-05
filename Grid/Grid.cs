using Godot;
using System;

public class Grid : Node2D
{
    const int arraySize = 5;
    public Room[,] roomArray = new Room[arraySize, arraySize];
    public string roomPathName = "res://Room/Rooms/";
    public override void _Ready()
    {
        var files = getTscnInDir(roomPathName);
        foreach(string file in files)
        {
            fileToRoomArr(file);
        }
        GD.Print((roomArray[0,0]));
        ShiftRowLeft(0);
        GD.Print((roomArray[0,1]));
    }

    private void fileToRoomArr(string file)
    {
        PackedScene roomScene = GD.Load<PackedScene>((roomPathName + file));
        Room roomInstance = (Room)roomScene.Instance();
        roomArray[0,0] = roomInstance;
        
    }

    public Godot.Collections.Array getTscnInDir(string pathName)
    {
        var files = new Godot.Collections.Array();
        var dir = new Directory();
        
        dir.Open(pathName);
        
        dir.ListDirBegin();
        
        while (true)
        {
            string file = dir.GetNext();
            if (file == "")
            {
                break;
            }
            else if (!(file.BeginsWith(".")) && (file.EndsWith(".tscn")))
            {
                files.Add(file);
            }
        }

        return files;
    }

    public void ShiftRowLeft(int row)
    {
        var storage = roomArray[row, 0];
        for (int i = 1; i < arraySize; i++)
        {
            roomArray[row,i - 1] = roomArray[row, i];

        }
        roomArray[row,arraySize - 1] = storage;
    }
    public void ShiftRowRight(int row)
    {
        var storage = roomArray[row, arraySize - 1 ];
        for (int i = 1; i <= arraySize - 1; i++)
        {
            roomArray[row, arraySize - i] = roomArray[row, arraySize - i - 1];
        }
        roomArray[row,0] = storage;
    }
    public void ShiftRow(int row, bool left)
    {
        if (left)
        {
            ShiftRowLeft(row);
        } 
        else 
        {
            ShiftRowRight(row);
        }
    }


}
