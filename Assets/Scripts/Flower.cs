using System;
using UnityEngine;

public enum LifeStep
{
    Child = 0,
    Adult = 1,
    Aged = 2
}

public class Flower : MonoBehaviour
{
    [SerializeField] protected LifeStep lifeStep;
    [SerializeField] protected float lifePeriod;
    [SerializeField] protected int maxNectarCount;
    [SerializeField] protected int nectarCount;
    [SerializeField] protected int productionRate;
    [SerializeField] protected int returningRate;

    protected float initializationTime;
    protected float timeSinceInitialization;
    protected FlowerState flowerState;

    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        lifeStep = LifeStep.Child;
        flowerState = new GrowingState(this);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
        UpdateLifePeriod();
        flowerState.ProduceNectar();
        flowerState.ReturnNectar();
    }

    protected void UpdateLifePeriod()
    {
        switch (lifeStep)
        {
            case < LifeStep.Adult when timeSinceInitialization > lifePeriod:
                lifeStep = LifeStep.Adult;
                flowerState = new ProducingState(this);
                return;
            case < LifeStep.Aged when timeSinceInitialization > 2 * lifePeriod:
                lifeStep = LifeStep.Aged;
                maxNectarCount /= 2;
                break;
        }
    }

    protected abstract class FlowerState
    {
        protected Flower flower;

        protected FlowerState(Flower flower)
        {
            this.flower = flower;
        }

        public abstract void ProduceNectar();
        public abstract int ReturnNectar();
    }

    protected class ProducingState : FlowerState
    {
        public ProducingState(Flower flower) : base(flower) { }

        public override void ProduceNectar() =>
            flower.nectarCount = Math.Min(flower.nectarCount + flower.productionRate, flower.maxNectarCount);

        public override int ReturnNectar() => 0;
    }

    protected class ReturningState : FlowerState
    {
        public ReturningState(Flower flower) : base(flower) { }

        public override void ProduceNectar() { }

        public override int ReturnNectar()
        {
            if (flower.nectarCount - flower.returningRate < 0)
                return 0;
            flower.nectarCount -= flower.returningRate;
            return flower.returningRate;
        }
    }

    protected class GrowingState : FlowerState
    {
        public GrowingState(Flower flower) : base(flower) { }

        public override void ProduceNectar() { }

        public override int ReturnNectar() => 0;
    }
}