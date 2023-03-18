using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataGame 
{
    public static string Coin = "Coin";
    public static string Gen = "Gen";
    public static string Car = "Car";
    public static string Stage = "Stage";
    public static int GetCoin()
    {
        if (!PlayerPrefs.HasKey(Coin))
        {
            PlayerPrefs.SetInt(Coin, 0);
        }
        return PlayerPrefs.GetInt(Coin);
    }
    public static void SetCoin(int value)
    {
        PlayerPrefs.SetInt(Coin, value);
    }
    public static int GetGem()
    {
        if (!PlayerPrefs.HasKey(Gen))
        {
            PlayerPrefs.SetInt(Gen, 0);
        }
        return PlayerPrefs.GetInt(Gen);
    }
    public static void SetGen(int value)
    {
        PlayerPrefs.SetInt(Gen, value);
    }
    public static int GetCar()
    {
        if (!PlayerPrefs.HasKey(Car))
        {
            PlayerPrefs.SetInt(Car, 0);
        }
        return PlayerPrefs.GetInt(Car);
    }
    public static void SetCar(int value)
    {
        PlayerPrefs.SetInt(Car, value);
    }
    public static int Get(string Key)
    {
        if (!PlayerPrefs.HasKey(Key))
        {
            PlayerPrefs.SetInt(Key, 0);
        }
        return PlayerPrefs.GetInt(Key);
    }
    public static void Set(string Key, int value)
    {
        PlayerPrefs.SetInt(Key, value);
    }
    public static void Save()
    {
        PlayerPrefs.Save();
    }

    
}
