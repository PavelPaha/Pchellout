using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _initializationTime;
    private float _timeSinceInitialization;
    private float _explosionTime;
    private Animator _animator;
    private CircleCollider2D _collider2D;

    void Start()
    {
        _initializationTime = Time.timeSinceLevelLoad;
        _animator = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<CircleCollider2D>();
        var explosionClip =
            _animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Explosion");
        _explosionTime = explosionClip!.length;
        DamageOtherObjects();
    }

    void Update()
    {
        _timeSinceInitialization = Time.timeSinceLevelLoad - _initializationTime;
        if (_timeSinceInitialization > _explosionTime)
            Destroy(gameObject);
    }

    private void DamageOtherObjects()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _collider2D.radius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<BasicBee>(out var basicBee))
                basicBee.Damage(50);
        }
    }
}