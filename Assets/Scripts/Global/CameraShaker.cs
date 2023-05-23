using DefaultNamespace;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Camera Camera;
    public GameObject DamageTint;

    private SpriteRenderer spriteRenderer;
    private float _shakeDuration = 0.2f;
    private float _shakeAmount = 0.3f;
    private float _decreaseFactor = 1.0f;

    private Vector3 _originalPosition;
    private float _shakeTimer = 0.0f;
    
    private float fadeTime = 0.5f;
    private float waitTime = 0.1f;
    
    private float currentAlpha = 0.0f;
    private float targetAlpha = 0.0f;
    private bool isFading = false;
    private float timer = 0.0f;
    
    public float duration = 1.0f;

    public void Start()
    {
        Bomb.OnExploded += Shake;
        BeeEnemy.OnHiveDamage += () =>
        {
            Shake();
            PaintRed();
            
        };
        BuildingPlacer.OnShake += Shake;
        spriteRenderer = DamageTint.GetComponent<SpriteRenderer>();
        _originalPosition = Camera.transform.position;
    }

    private void Shake()
    {
        _shakeTimer = _shakeDuration;
    }
    
    void Update()
    {
        if (_shakeTimer > 0)
        {
            Vector3 shakeVector = Random.insideUnitSphere * _shakeAmount;
            Camera.transform.position = _originalPosition + shakeVector;
            _shakeTimer -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeTimer = 0.0f;
            Camera.transform.position = _originalPosition;
        }
        
        timer += Time.deltaTime;
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, timer / duration);

        Color color = spriteRenderer.color;
        color.a = currentAlpha;
        spriteRenderer.color = color;

        if (currentAlpha.CompareTo(targetAlpha) == 0)
        {
            targetAlpha = 0;
        }
    }

    private void PaintRed()
    {
        currentAlpha = spriteRenderer.color.a;
        targetAlpha = 1.0f;
        timer = 0.0f;
        isFading = true;
    }
}