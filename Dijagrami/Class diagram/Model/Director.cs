/***********************************************************************
 * Module:  Director.cs
 * Author:  Wombat
 * Purpose: Definition of the Class Director
 ***********************************************************************/

using System;

namespace Model
{
   public class Director
   {
      public Director GetDirector(int id)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList user;
      
      public System.Collections.ArrayList User
      {
         get
         {
            if (user == null)
               user = new System.Collections.ArrayList();
            return user;
         }
         set
         {
            RemoveAllUser();
            if (value != null)
            {
               foreach (User oUser in value)
                  AddUser(oUser);
            }
         }
      }
      
      public void AddUser(User newUser)
      {
         if (newUser == null)
            return;
         if (this.user == null)
            this.user = new System.Collections.ArrayList();
         if (!this.user.Contains(newUser))
            this.user.Add(newUser);
      }
      
      public void RemoveUser(User oldUser)
      {
         if (oldUser == null)
            return;
         if (this.user != null)
            if (this.user.Contains(oldUser))
               this.user.Remove(oldUser);
      }
      
      public void RemoveAllUser()
      {
         if (user != null)
            user.Clear();
      }
      public System.Collections.ArrayList room;
      
      public System.Collections.ArrayList Room
      {
         get
         {
            if (room == null)
               room = new System.Collections.ArrayList();
            return room;
         }
         set
         {
            RemoveAllRoom();
            if (value != null)
            {
               foreach (Room oRoom in value)
                  AddRoom(oRoom);
            }
         }
      }
      
      public void AddRoom(Room newRoom)
      {
         if (newRoom == null)
            return;
         if (this.room == null)
            this.room = new System.Collections.ArrayList();
         if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
      }
      
      public void RemoveRoom(Room oldRoom)
      {
         if (oldRoom == null)
            return;
         if (this.room != null)
            if (this.room.Contains(oldRoom))
               this.room.Remove(oldRoom);
      }
      
      public void RemoveAllRoom()
      {
         if (room != null)
            room.Clear();
      }
   
   }
}