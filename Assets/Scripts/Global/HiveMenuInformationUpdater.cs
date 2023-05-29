using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Global
{
    public class HiveMenuInformationUpdater : MonoBehaviour
    {
        public static Action OnUpgradeHoney;
        public static Action<GameObject, string> OnNotify; 

        public GameObject Hive;

        public TextMeshProUGUI HoneyStorageUpgradePrice;
        public TextMeshProUGUI DefendersUpgradePrice;
        public TextMeshProUGUI FixHivePrice;
        public TextMeshProUGUI ProjectileUpgradePrice;

        public Image HoneyStorageBar;
        public Image DefendersBar;
        public Image ProjectileBar;


        public Button UpgradeHoneyStorageButton;
        public Button UpgradeDefendersButton;
        public Button FixHiveButton;
        public Button UpgradeProjectileButton;
        
        
        public void Start()
        {
            ResourcesUpdater.OnUpdated += UpdatePressPossibility;
            InitializeTextFields();
            InitializeBars();

            UpgradeDefendersButton.onClick.AddListener(() =>
            {
                if (DefendersBar.fillAmount < 1 && TryBuy(Globals.DefendersUpgradePrice))
                {
                    Globals.CurrentDefenderUpgradeLevel = Math.Max(Globals.CurrentDefenderUpgradeLevel,
                        Globals.CurrentDefenderUpgradeLevel+1);
                    DefendersBar.fillAmount = (float)Globals.CurrentDefenderUpgradeLevel / Globals.MaxDefendersUpgrade;
                    Globals.DefenderDamage += 20;
                    Globals.DefenderScale += 0.1f;
                }
                else
                {
                    OnNotify?.Invoke(UpgradeDefendersButton.gameObject, "Улучшено до максимума");
                }
            });

            UpgradeHoneyStorageButton.onClick.AddListener(() =>
            {
                if (HoneyStorageBar.fillAmount < 1 && TryBuy(Globals.HoneyStorageUpgradePrice))
                {
                    Globals.CurrentStorageCapacity = Math.Min(
                        Globals.CurrentStorageCapacity + Globals.MaxHoneyStorageCapacity / 10,
                        Globals.MaxHoneyStorageCapacity);
                    HoneyStorageBar.fillAmount = (float)Globals.CurrentStorageCapacity / Globals.MaxHoneyStorageCapacity;
                }
                else
                {
                    OnNotify?.Invoke(UpgradeHoneyStorageButton.gameObject, "Улучшено до максимума");
                }
            });
            
            
            FixHiveButton.onClick.AddListener(() =>
            {
                if (TryBuy(Globals.FixHivePrice))
                {
                    var health = Hive.GetComponent<HouseForBees>().Health;
                    Hive.GetComponent<HouseForBees>().Health = Math.Min(
                        health + (int)(Globals.FixHiveCoeff * Globals.FixHivePrice),
                        Globals.MaxHiveHealth);
                }
                else
                {
                    OnNotify?.Invoke(FixHiveButton.gameObject, "Улучшено до максимума");
                }
            });
            
            UpgradeProjectileButton.onClick.AddListener(() =>
            {
                if (ProjectileBar.fillAmount < 1 && TryBuy(Globals.ProjectileUpgradePrice))
                {
                    Globals.ProjectileDamage = Math.Min(Globals.MaxProjectileDamage, Globals.ProjectileDamage + 20);
                    ProjectileBar.fillAmount = (float)Globals.ProjectileDamage / Globals.MaxProjectileDamage;
                }
                else
                {
                    OnNotify?.Invoke(UpgradeProjectileButton.gameObject, "Улучшено до максимума");
                }
            });
        }

        private void InitializeTextFields()
        {
            HoneyStorageUpgradePrice.text = Globals.HoneyStorageUpgradePrice.ToString();
            DefendersUpgradePrice.text = Globals.DefendersUpgradePrice.ToString();
            FixHivePrice.text = Globals.FixHivePrice.ToString();
            ProjectileUpgradePrice.text = Globals.ProjectileUpgradePrice.ToString();
        }

        private void InitializeBars()
        {
            DefendersBar.fillAmount = (float)Globals.CurrentDefenderUpgradeLevel / Globals.MaxDefendersUpgrade;
            HoneyStorageBar.fillAmount = (float)Globals.CurrentStorageCapacity / Globals.MaxHoneyStorageCapacity;
            ProjectileBar.fillAmount = (float)Globals.ProjectileDamage / Globals.MaxProjectileDamage;
        }

        private bool TryBuy(int price)
        {
            if (price <= Globals.GameResources["honey"].Amount)
            {
                Globals.GameResources["honey"].Amount -= price;
                OnUpgradeHoney?.Invoke();
                return true;
            }

            Debug.Log("Недостаточно мёда для покупки");
            return false;
        }

        private void UpdatePressPossibility()
        {
            Globals.UpdateBuyPossibility(UpgradeHoneyStorageButton, int.Parse(HoneyStorageUpgradePrice.text));
            Globals.UpdateBuyPossibility(UpgradeDefendersButton, int.Parse(DefendersUpgradePrice.text));
            Globals.UpdateBuyPossibility(FixHiveButton, int.Parse(FixHivePrice.text));
            Globals.UpdateBuyPossibility(UpgradeProjectileButton, int.Parse(ProjectileUpgradePrice.text));
        }
    }
}