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
}
