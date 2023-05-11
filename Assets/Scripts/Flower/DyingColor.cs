using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(BasicBee))]
public class DyingColor : MonoBehaviour
{
    private Renderer _renderer;
    private BasicBee _basicBee;

    void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _basicBee = gameObject.GetComponent<BasicBee>();
    }

    void Update()
    {
        if (_basicBee.Health > 50)
            _renderer.material.color = Color.white;
        else
        {
            var grayness = Mathf.Lerp(0.5f, 1f, _basicBee.Health / 50f);
            _renderer.material.color = new Color(grayness, grayness, grayness, 1);
        }
    }
}