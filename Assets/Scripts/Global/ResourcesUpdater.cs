using System;
using TMPro;
using UnityEngine;

namespace Global
{
    public class ResourcesUpdater : MonoBehaviour
    {
        public TextMeshProUGUI Honey;

        public void Update()
        {
            Honey.text = Globals.GameResources["honey"].Amount.ToString();
        }
    }
}