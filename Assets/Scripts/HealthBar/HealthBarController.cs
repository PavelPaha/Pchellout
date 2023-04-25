using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image _bar;
    public int Health;
    public float Margin = 0.5f;
    public GameObject HealthBar;
    public float Fill;
    
    // Start is called before the first frame update
    void Start()
    {
        // HealthBar = Instantiate(HealthBar);
        _bar = HealthBar.transform.Find("Bar").GetComponent<Image>();
        Fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        var spriteHeight = GetComponent<Renderer>().bounds.size.y / 2;
        HealthBar.transform.position = transform.position + new Vector3(0, spriteHeight + Margin, 0);
    }

    public void DamageHealth(float damage)
    {
        Fill -= damage / Health;
        _bar.fillAmount = Fill;
    }
    
    
}
