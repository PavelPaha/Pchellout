using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseForBees: BasicBee
{
    public GameObject Menu;

    public void Update()
    {
        if (Time.deltaTime > 0)
            Globals.AddHoney(1);
    }

    public void OnMouseDown()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
    
    public override void ShowName()
    {
        OnNotify?.Invoke(gameObject, $"Улье. Здоровье - {Health}");
    }
}
