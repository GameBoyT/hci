/***********************************************************************
 * Module:  Room.cs
 * Author:  Wombat
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;

public class Room
{
    public Room GetRoom(int id)
    {
        // TODO: implement
        return null;
    }

    public List<Room> GetAllRooms()
    {
        // TODO: implement
        return null;
    }

    public Boolean CreateRoom(int id, String name, String detail, int floor, RoomType roomType)
    {
        // TODO: implement
        return null;
    }

    public Boolean UpdateRoom(String name, String detail, int floor, RoomType roomType)
    {
        // TODO: implement
        return null;
    }

    public Boolean DeleteRoom(int id)
    {
        // TODO: implement
        return null;
    }

    private int Id;
    private String Name;
    private String Detail;
    private int Floor;
    private RoomType RoomType;

}