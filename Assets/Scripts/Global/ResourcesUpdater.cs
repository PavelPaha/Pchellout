using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class ResourcesUpdater : MonoBehaviour
    {
        public TextMeshProUGUI Honey;
        public TextMeshProUGUI HoneyCapacity;
        public TextMeshProUGUI BombCount;
        public Image BombBar;
        public static Action OnUpdated;
        
        public void Start()
        {
            Extractor.OnResourcesUpdated += UpdateResources;
            BuildingPlacer.OnBuildingPlaced += UpdateResources;
            HiveMenuInformationUpdater.OnUpgradeHoney += UpdateResources;
            UpdateResources();
        }
        
        public void UpdateResources()
        {
            OnUpdated?.Invoke();
            Honey.text = Globals.GameResources["honey"].Amount.ToString();
            BombCount.text = Globals.CurrentBombCount.ToString();
            BombBar.fillAmount = (float)Globals.CurrentBombCount / Globals.BombCapacity;
            HoneyCapacity.text = $"/ {Globals.CurrentStorageCapacity.ToString()}";
        }
    }
}