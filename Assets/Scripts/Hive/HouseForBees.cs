using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseForBees: BasicBee
{
    public void Loss()
    {
        SceneManager.LoadScene("Menu");
    }
}
