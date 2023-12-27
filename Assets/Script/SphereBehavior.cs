using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SphereBehavior : MonoBehaviour
{
    public enum SphereType
    {
        Moon = 0,
        Pluton = 1,
        Mars = 2, 
        Earth = 3, 
        Saturn = 4, 
        Jupiter = 5,
        Sun = 6
    }

    SphereType sphereType = SphereType.Moon;
    public Rigidbody2D rb;
    public CircleCollider2D collider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        Debug.Log("START");
    }


    public void Setup(SphereType _sphereType)
    {
        sphereType = _sphereType;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * ((int)sphereType + 1) * GPCtrl.Instance.sizeFactor, .3f).SetEase(Ease.OutElastic);
        spriteRenderer.sprite = GPCtrl.Instance.planetSpriteList[(int)sphereType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SphereBehavior _sphere = collision.GetComponent<SphereBehavior>();
        if (_sphere != null)
        {
            if (_sphere.sphereType == sphereType)
            {
                if (_sphere.transform.position.y < transform.position.y)
                {
                    Fusion(_sphere);
                }
            }
        }
        
    }

    void Fusion(SphereBehavior _sphere)
    {
        Destroy(_sphere.gameObject);
        SphereBehavior _newSphere = Instantiate(GPCtrl.Instance.spherePrefab);
        _newSphere.transform.position = transform.position;
        _newSphere.Setup((SphereType) sphereType + 1);
        Destroy(gameObject);
    }
}
