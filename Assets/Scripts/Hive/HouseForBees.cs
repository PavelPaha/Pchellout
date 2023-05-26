using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseForBees: BasicBee
{
    public GameObject Menu;
    public void Loss()
    {
        Globals.GameOutcome = GameOutcome.Loss;
        Health = Globals.MaxHiveHealth;
        SceneManager.LoadScene("Menu");
    }

    public void OnMouseDown()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
}
