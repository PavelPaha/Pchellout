using UnityEngine;
using UnityEngine.UI;

namespace Hive
{
    public class TownHall: HiveObject, IContainHoney, IContainPollen
    {
        public int HoneyCount;
        public int PollenCount;
        
        public Sprite picture;
        public override string GetName() => "TownHall";

        public override string GetDescription() => "Это ратуша";

        public override Sprite GetImage() => picture;
        
        
        public override GameObject ShowSpecialInformation()
        {
            throw new System.NotImplementedException();
        }

        public int GetHoneyCount() => HoneyCount;

        public void UpdateHoneyCount(int value)
        {
            HoneyCount = value;
        }

        public int GetPollenCount() => PollenCount;

        public void UpdatePollenCount(int value)
        {
            PollenCount = value;
        }
    }
}