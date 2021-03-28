/***********************************************************************
 * Module:  Director.cs
 * Author:  Wombat
 * Purpose: Definition of the Class Director
 ***********************************************************************/

public class Director
{
    public Director GetDirector(int id)
    {
        // TODO: implement
        return null;
    }

    public System.Collections.ArrayList user;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetUser()
    {
        if (user == null)
            user = new System.Collections.ArrayList();
        return user;
    }

    /// <pdGenerated>default setter</pdGenerated>
    public void SetUser(System.Collections.ArrayList newUser)
    {
        RemoveAllUser();
        foreach (User oUser in newUser)
            AddUser(oUser);
    }

    /// <pdGenerated>default Add</pdGenerated>
    public void AddUser(User newUser)
    {
        if (newUser == null)
            return;
        if (this.user == null)
            this.user = new System.Collections.ArrayList();
        if (!this.user.Contains(newUser))
            this.user.Add(newUser);
    }

    /// <pdGenerated>default Remove</pdGenerated>
    public void RemoveUser(User oldUser)
    {
        if (oldUser == null)
            return;
        if (this.user != null)
            if (this.user.Contains(oldUser))
                this.user.Remove(oldUser);
    }

    /// <pdGenerated>default removeAll</pdGenerated>
    public void RemoveAllUser()
    {
        if (user != null)
            user.Clear();
    }
    public System.Collections.ArrayList room;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetRoom()
    {
        if (room == null)
            room = new System.Collections.ArrayList();
        return room;
    }

    /// <pdGenerated>default setter</pdGenerated>
    public void SetRoom(System.Collections.ArrayList newRoom)
    {
        RemoveAllRoom();
        foreach (Room oRoom in newRoom)
            AddRoom(oRoom);
    }

    /// <pdGenerated>default Add</pdGenerated>
    public void AddRoom(Room newRoom)
    {
        if (newRoom == null)
            return;
        if (this.room == null)
            this.room = new System.Collections.ArrayList();
        if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
    }

    /// <pdGenerated>default Remove</pdGenerated>
    public void RemoveRoom(Room oldRoom)
    {
        if (oldRoom == null)
            return;
        if (this.room != null)
            if (this.room.Contains(oldRoom))
                this.room.Remove(oldRoom);
    }

    /// <pdGenerated>default removeAll</pdGenerated>
    public void RemoveAllRoom()
    {
        if (room != null)
            room.Clear();
    }

}