using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class HiveMenuInformationUpdater : MonoBehaviour
    {
        public static Action OnUpgradeHoney;

        public GameObject Hive;

        public TextMeshProUGUI HoneyStorageUpgradePrice;
        public TextMeshProUGUI DefendersUpgradePrice;
        public TextMeshProUGUI BombPrice;
        public TextMeshProUGUI FixHivePrice;
        public TextMeshProUGUI ProjectileUpgradePrice;

        public Image HoneyStorageBar;
        public Image DefendersBar;
        public Image BombBar;
        public Image ProjectileBar;


        public Button UpgradeHoneyStorageButton;
        public Button UpgradeDefendersButton;
        public Button BuyBomb;
        public Button FixHiveButton;
        public Button UpgradeProjectileButton;

        public void Start()
        {
            InitializeTextFields();
            InitializeBars();

            UpgradeDefendersButton.onClick.AddListener(() =>
            {
                if (TryBuy(Globals.DefendersUpgradePrice))
                {
                    Globals.CurrentDefenderUpgradeLevel = Math.Max(Globals.CurrentDefenderUpgradeLevel,
                        Globals.CurrentDefenderUpgradeLevel+1);
                    DefendersBar.fillAmount = (float)Globals.CurrentDefenderUpgradeLevel / Globals.MaxDefendersUpgrade;
                }
            });

            UpgradeHoneyStorageButton.onClick.AddListener(() =>
            {
                if (TryBuy(Globals.HoneyStorageUpgradePrice))
                {
                    Globals.CurrentStorageCapacity = Math.Min(
                        Globals.CurrentStorageCapacity + Globals.MaxHoneyStorageCapacity / 10,
                        Globals.MaxHoneyStorageCapacity);
                    HoneyStorageBar.fillAmount = (float)Globals.CurrentStorageCapacity / Globals.MaxHoneyStorageCapacity;
                }
            });

            BuyBomb.onClick.AddListener(() =>
            {
                if (TryBuy(Globals.BombPrice))
                {
                    Globals.CurrentBombCount = Math.Min(Globals.BombCapacity, Globals.CurrentBombCount + 1);
                    BombBar.fillAmount = (float)Globals.CurrentBombCount / Globals.BombCapacity;
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
            });
            
            UpgradeProjectileButton.onClick.AddListener(() =>
            {
                if (TryBuy(Globals.ProjectileUpgradePrice))
                {
                    Globals.ProjectileDamage = Math.Min(Globals.MaxProjectileDamage, Globals.ProjectileDamage + 20);
                    ProjectileBar.fillAmount = (float)Globals.ProjectileDamage / Globals.MaxProjectileDamage;
                }
            });
        }

        private void InitializeTextFields()
        {
            HoneyStorageUpgradePrice.text = Globals.HoneyStorageUpgradePrice.ToString();
            DefendersUpgradePrice.text = Globals.DefendersUpgradePrice.ToString();
            BombPrice.text = Globals.BombPrice.ToString();
            FixHivePrice.text = Globals.FixHivePrice.ToString();
            ProjectileUpgradePrice.text = Globals.ProjectileUpgradePrice.ToString();
        }

        private void InitializeBars()
        {
            DefendersBar.fillAmount = (float)Globals.CurrentDefenderUpgradeLevel / Globals.MaxDefendersUpgrade;
            HoneyStorageBar.fillAmount = (float)Globals.CurrentStorageCapacity / Globals.MaxHoneyStorageCapacity;
            BombBar.fillAmount = (float)Globals.CurrentBombCount / Globals.BombCapacity;
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
    }
}