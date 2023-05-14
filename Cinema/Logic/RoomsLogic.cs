using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class RoomsLogic
{
    public List<RoomModel> _rooms;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public RoomModel? CurrentRoom { get; private set; }

    public RoomsLogic()
    {
        _rooms = RoomAccess.LoadAll();
    }


    public void UpdateList(RoomModel room)
    {
        //Find if there is already an model with the same id
        int index = _rooms.FindIndex(s => s.Id == room.Id);

        if (index != -1)
        {
            //update existing model
            _rooms[index] = room;
        }
        else
        {
            //add new model
            _rooms.Add(room);
        }
        RoomAccess.WriteAll(_rooms);
    }

    public RoomModel GetById(int id)
    {
        RoomModel room = _rooms.Find(i => i.Id == id)!;
        // System.Console.WriteLine(room);
        return room;
    }

    public RoomModel CheckEnter(int? roomNumber)
    {
        if (roomNumber == null!)
        {
            return null!;
        }
        CurrentRoom = _rooms.Find(i => i.RoomNumber == roomNumber);
        return CurrentRoom!;
    }
}