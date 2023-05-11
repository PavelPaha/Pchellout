using System;
using TMPro;
using UnityEngine;

namespace Global
{
    public class ResourcesUpdater : MonoBehaviour
    {
        public TextMeshProUGUI Honey;
        
        public void Start()
        {
            Extractor.OnResourcesUpdated += UpdateResources;
        }
        
        public void UpdateResources()
        {
            Honey.text = Globals.GameResources["honey"].Amount.ToString();
        }
    }
}