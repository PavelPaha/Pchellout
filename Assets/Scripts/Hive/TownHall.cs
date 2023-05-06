using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Hive
{
    public class TownHall : HiveBuilding, IHoneyContainer
    {
        public int Honey { get; set; }

        public Sprite Picture;
        public override string GetName() => "TownHall";

        public override string GetDescription() => "Это ратуша";

        public override Sprite GetImage() => Picture;
    }
}