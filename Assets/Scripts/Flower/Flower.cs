using System;
using UnityEngine;

public enum LifeStep
{
    Child = 0,
    Adult = 1,
}

public class Flower : BasicBee
{
    public LifeStep lifeStep;
    public float lifePeriod;

    private float _timeSinceInitialization;
    private float _initializationTime;
    private FlowerState _flowerState;
    private NectarInventory _inventory;
    private Animator _animator;

    private void Start()
    {
        lifeStep = LifeStep.Child;
        _initializationTime = Time.timeSinceLevelLoad;
        _flowerState = new IdleState(this);
        _inventory = GetComponent<NectarInventory>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _flowerState.ProduceNectar();
    }

    private void Update()
    {
        _timeSinceInitialization = Time.timeSinceLevelLoad - _initializationTime;
        UpdateLifePeriod();
    }


    private void UpdateLifePeriod()
    {
        if (_timeSinceInitialization < lifePeriod)
            return;
        lifeStep = LifeStep.Adult;
        _flowerState = new ProducingState(this);
        _animator.SetTrigger("IsAdult");
    }

    public abstract class FlowerState
    {
        protected readonly Flower Flower;

        protected FlowerState(Flower flower)
        {
            Flower = flower;
        }

        public abstract void ProduceNectar();
    }

    public class ProducingState : FlowerState
    {
        public ProducingState(Flower flower) : base(flower) { }

        public override void ProduceNectar()
        {
            Flower._inventory.ProduceNectar();
        }
    }

    public class IdleState : FlowerState
    {
        public IdleState(Flower flower) : base(flower) { }

        public override void ProduceNectar() { }
    }

    public override void ShowName()
    {
        OnNotify?.Invoke(gameObject, $"Цветок\n Нектар - {_inventory.NectarCount}");
        Debug.Log("Цветок");
    }
}