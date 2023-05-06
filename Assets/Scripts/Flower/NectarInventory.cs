using System;
using UnityEngine;

public class NectarInventory : MonoBehaviour
{
    [SerializeField] private int maxNectarCount;
    [SerializeField] private int nectarCount;
    [SerializeField] private int productionRate;
    [SerializeField] private int extractingRate;

    public bool IsEmpty => nectarCount == 0;
    public bool IsFull => nectarCount == maxNectarCount;
    public int NectarCount => nectarCount;

    public void ProduceNectar()
    {
        nectarCount = Math.Min(nectarCount + productionRate, maxNectarCount);
    }

    public void ExtractNectar(GameObject otherObject)
    {
        var otherInventory = otherObject.GetComponent<NectarInventory>();
        var nectarToExtract = Math.Min(maxNectarCount - nectarCount, extractingRate);
        nectarCount += otherInventory.DeliverNectar(nectarToExtract);
    }

    public int DeliverNectar(int value)
    {
        var nectarToGet = Math.Min(nectarCount, value);
        nectarCount -= nectarToGet;
        return nectarToGet;
    }
}