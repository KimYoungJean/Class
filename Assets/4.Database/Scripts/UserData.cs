using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum CharacterClass
{
    none = 0,
    Warrior = 1,
    Archer = 2,
    Wizard = 3
}

public class UserData
{
    public int Uid => uid;
    private int uid;
    
    public string password;
    public string email;
    public string name;
    public CharacterClass characterClass;
    public int level;
    public string profileText;
    

    public UserData(DataRow row) : this(
            int.Parse(row["uid"].ToString()),
            row["email"].ToString(),
            row["pw"].ToString(),

            row["name"].ToString(),
            (CharacterClass)int.Parse(row["class"].ToString()),
            int.Parse(row["LEVEL"].ToString()),

            row["profile_text"].ToString()
        ) { }
    public UserData(int uid, string email, string password, string name, CharacterClass characterClass, int level, string profileText)
    {
        this.uid = uid;
        this.email = email;
        this.password = password;

        this.name = name;
        this.characterClass = characterClass;
        this.level = level;

        this.profileText = profileText;
    }

    public bool ComparePassword(string password)
    {
        return this.password == password;
    }
    public bool CompareEmail(string email)
    {
        return this.email == email;
    }




}

