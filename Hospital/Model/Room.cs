/***********************************************************************
 * Module:  Room.cs
 * Author:  Wombat
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
   public class Room
   {
      private int id;
      private String name;
      private String detail;
      private int floor;
      private RoomType roomType;
      
      public Room GetRoom(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> GetAllRooms()
      {
         throw new NotImplementedException();
      }
      
      public Boolean CreateRoom(int id, String name, String detail, int floor, RoomType roomType)
      {
         throw new NotImplementedException();
      }
      
      public Boolean UpdateRoom(String name, String detail, int floor, RoomType roomType)
      {
         throw new NotImplementedException();
      }
      
      public Boolean DeleteRoom(int id)
      {
         throw new NotImplementedException();
      }
   
   }
}