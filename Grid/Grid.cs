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
        ShiftRowRight(0);
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
    public void ShiftColumnUp(int column)
    {
        var storage = roomArray[0, column];
        for (int i = 1; i < arraySize; i++)
        {
            roomArray[i - 1, column] = roomArray[i, column];

        }
        roomArray[arraySize - 1, column] = storage;
    }
    public void ShiftColumnDown(int column)
    {
        var storage = roomArray[arraySize - 1, column ];
        for (int i = 1; i <= arraySize - 1; i++)
        {
            roomArray[ arraySize - i, column] = roomArray[ arraySize - i - 1, column];
        }
        roomArray[0, column] = storage;
    }
    public void ShiftColumn(int column, bool up)
    {
        if (up)
        {
            ShiftColumnUp(column);
        }
        else
        {
            ShiftColumnDown(column);
        }
    }

}
