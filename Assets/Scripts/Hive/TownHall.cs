using UnityEngine;
using UnityEngine.UI;

namespace Hive
{
    public class TownHall : HiveBuilding, IHoneyContainer
    {
        public int Honey { get; set; }
        public int Pollen { get; set; }

        public Sprite picture;
        public override string GetName() => "TownHall";

        public override string GetDescription() => "Это ратуша";

        public override Sprite GetImage() => picture;


        public override GameObject ShowSpecialInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}