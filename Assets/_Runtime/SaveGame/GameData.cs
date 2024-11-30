using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerData player = new PlayerData();
    public WorldState worldState = new WorldState();
    public GameSettings gameSettings = new GameSettings();
    public List<Pet> pets = new List<Pet>(); // Thêm danh sách thú cưng
}

[Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int experience;
    public int health;
    public int mana;
    public Vector3 position = Vector3.zero;
    public int currency; // Thêm thuộc tính để lưu trữ tiền tệ
}

[Serializable]
public class WorldState
{
    public string dayTime;
    public string weather;
}

[Serializable]
public class GameSettings
{
    public float volume;
    public string graphics;
}

[Serializable]
public class Pet
{
    public string petName;
    public int petID;
    public string type;
    public int level;
    public Vector3 position;
}
