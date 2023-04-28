using UnityEngine;

public enum LifeStep
{
    Child = 0,
    Adult = 1,
}

public class Flower : MonoBehaviour
{
    [SerializeField] public LifeStep lifeStep;
    [SerializeField] protected float lifePeriod;

    protected float timeSinceInitialization;
    protected float initializationTime;
    protected FlowerState flowerState;
    protected NectarInventory inventory;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        lifeStep = LifeStep.Child;
        initializationTime = Time.timeSinceLevelLoad;
        flowerState = new IdleState(this);
        inventory = GetComponent<NectarInventory>();
        animator = GetComponent<Animator>();
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
        if (timeSinceInitialization < lifePeriod)
            return;
        lifeStep = LifeStep.Adult;
        flowerState = new ProducingState(this);
        animator.SetTrigger("IsAdult");
    }

    public abstract class FlowerState
    {
        protected Flower _flower;

        protected FlowerState(Flower flower)
        {
            _flower = flower;
        }

        public abstract void ProduceNectar();
        public abstract void ReturnNectar();
    }

    public class ProducingState : FlowerState
    {
        public ProducingState(Flower flower) : base(flower) { }

        public override void ProduceNectar()
        {
            _flower.inventory.ProduceNectar();
        }

        public override void ReturnNectar() { }
    }

    public class IdleState : FlowerState
    {
        public IdleState(Flower flower) : base(flower) { }

        public override void ProduceNectar() { }

        public override void ReturnNectar() { }
    }
}