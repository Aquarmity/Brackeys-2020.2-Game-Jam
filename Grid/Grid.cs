using Godot;
using System;

public class Grid : Node2D
{
    [Signal]
    public delegate void changeRooms();
    const int arraySize = 5;
    
    // Array 0,0 is top left corner Up: y-1 Down: y+1 Left:x-1 Right:x+1 Same as godot cordinate system
    public Room[,] roomArray = new Room[arraySize, arraySize];
    public string roomPathName = "res://Room/Rooms/";

    public int currentX = 0;
    public int currentY = 0;

    public UIGrid theUIGrid;

    public override void _Ready()
    {
        theUIGrid = GetParent().GetNode<UIGrid>("UIUnderHealth/UIGrid");
        fillRoomWithBaseRoom();
        var files = getTscnInDir(roomPathName);
        foreach(string file in files)
        {
            fileToRoomArr(file);
        }
        GD.Print((roomArray[0,0]));
        ShiftRowRight(0);
        GD.Print((roomArray[0,1]));
        DisplayRoom(currentX,currentY);
        
    }

    public void SetUpMenuGrid()
    {
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                string myPath = "UIUnderHealth/UIGrid/RoomGrid/GridContainer/Control";
                myPath = myPath + i.ToString() + j.ToString() + "/RoomSymbol";
                GetParent().GetNode<AnimatedSprite>(myPath).Frame = roomArray[j,i].frameSymbol;
            }
        }
        theUIGrid.UpdatePlayerBlinker();
    }
    private void fillRoomWithBaseRoom()
    {
        for(int i = 0; i < arraySize; i++)
        {
            for(int j = 0; j < arraySize; j++)
            {
                var newRoom = (Room)((GD.Load<PackedScene>("res://Room/Room.tscn")).Instance());
                newRoom.Name = i.ToString() + j.ToString();
                roomArray[i,j] = newRoom;
            }
        }
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
        if (currentX == row) {
            currentY -= 1;
            if(currentY < 0)
            {
                currentY = arraySize - 1;
            }
        }
    }
    public void ShiftRowRight(int row)
    {
        var storage = roomArray[row, arraySize - 1 ];
        for (int i = 1; i <= arraySize - 1; i++)
        {
            roomArray[row, arraySize - i] = roomArray[row, arraySize - i - 1];
        }
        roomArray[row,0] = storage;
        if (row == currentX) {
            currentY += 1;
            if (currentY > arraySize - 1) {
                currentY = 0;
            }
        }
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
        if (currentY == column) {
            currentX -= 1;
            if (currentX < 0)
            {
                currentX = arraySize - 1;
            }
        }
    }
    public void ShiftColumnDown(int column)
    {
        var storage = roomArray[arraySize - 1, column ];
        for (int i = 1; i <= arraySize - 1; i++)
        {
            roomArray[ arraySize - i, column] = roomArray[ arraySize - i - 1, column];
        }
        roomArray[0, column] = storage;
        if(column == currentY) {
            currentX += 1;
            if (currentX > arraySize -1)
            {
                currentX = 0;
            }
        }
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

    public void DisplayRoom(int x, int y)
    {

        var newCurrentRoom = roomArray[x, y];
        EmitSignal(nameof(changeRooms));
        
        AddChild(newCurrentRoom);
        if(!IsConnected(nameof(changeRooms), newCurrentRoom, "_onChangeRooms"))
        {
            Connect(nameof(changeRooms), newCurrentRoom, "_onChangeRooms");
        }

        GD.Print(x, y);
    }

    public bool moveRooms(int x, int y)
    {
        bool didMove = true;
        if (x > arraySize || y > arraySize) {
            GD.Print("moveRooms: X or Y are greater than the size of the map. ");
        }
        if (x > arraySize - 1)
        {
            x = 0; // loop on x axis going over the max place
        }
        if (y > arraySize - 1) 
        {
            y = arraySize - 1; // don't loop on y axis going overboard
            didMove = false;
        }
        if (x < 0)
        {
            x = arraySize - 1; // loop on x going under the min place
        }
        if (y < 0)
        {
            y = 0; // don't loop on the y axis going over the min place
            didMove = false;
        }

        if (x - currentX == 1) {
            if (roomArray[x,y].left == false)
            {
                return false;
            }
        }
        if (x - currentX == -1) {
            if (roomArray[x,y].right == false)
            {
                return false;
            }
        }
        if (y - currentY == 1) {
            if (roomArray[x,y].up == false)
            {
                return false;
            }
        }
        if (y - currentY == -1)
        {
            if (roomArray[x,y].down == false)
            {
                return false;
            }
        }
        currentX = x;
        currentY = y;
        DisplayRoom(x,y);
        SetUpMenuGrid();
        return didMove;
    }
    public bool moveRoomsRelative(int x, int y)
    {
        return moveRooms(currentX + x, currentY + y);
    }

    public bool moveUp()
    {
        return moveRoomsRelative(0, -1);
    }
    public bool moveDown()
    {
        return moveRoomsRelative(0,1);
    }
    public bool moveLeft()
    {
        return moveRoomsRelative(-1, 0);
    }
    public bool moveRight()
    {
        return moveRoomsRelative(1,0);
    }
}
