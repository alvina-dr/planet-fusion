using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance;
    public SphereBehavior spherePrefab;
    SphereBehavior currentSphere;
    public float sizeFactor;
    public List<Sprite> planetSpriteList = new List<Sprite>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InstantiateSphere();
    }

    private void Update()
    {
        if (currentSphere != null)
        {
            currentSphere.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, currentSphere.transform.position.y, 0);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentSphere.rb.bodyType = RigidbodyType2D.Dynamic;
                currentSphere.collider.enabled = true;
                DOVirtual.DelayedCall(.5f, () => InstantiateSphere());
            }
        }
    }

    public void InstantiateSphere()
    {
        currentSphere = Instantiate(spherePrefab);
        currentSphere.Setup(SphereBehavior.SphereType.Moon);
        currentSphere.transform.position = transform.position;
        currentSphere.rb.bodyType = RigidbodyType2D.Static;
        currentSphere.collider.enabled = false;
    }
}
