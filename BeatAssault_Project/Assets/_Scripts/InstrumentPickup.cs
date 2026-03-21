using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum Instruments{
    None,
    Snare,
    Violin,
    Saxophone,
    Piano,
    Kick,
    Bass
}
public class InstrumentPickup : MonoBehaviour
{
    [SerializeField] float _pickupRadius = 1.5f;
    
    private SphereCollider _collider;
    private PlayInstrument _playInstrument;
    
    public Instruments instruments;
    public int abilityIndex = 0;

    [SerializeField] private GameObject[] meshes;
    [SerializeField] private GameObject instrumentHolder;
    
    public float rotationDuration = 3f;
    public float moveHeight = 0.5f;
    public float moveDuration = 2f;
    private Vector3 initialPosition;

    [SerializeField] GameObject shadow;
    private void Awake()
    {
        _collider = gameObject.GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = _pickupRadius;

        ActivateMesh();
    }
    private void Start()
    {
        RotateInstrument();
    }
    public void RotateInstrument()
    {
        initialPosition = instrumentHolder.transform.position;

        // foreach (var mesh in meshes)
        // {
        //     mesh.transform.DORotate(Vector3.forward * 360f, rotationDuration, RotateMode.FastBeyond360)
        //         .SetLoops(-1, LoopType.Restart)
        //         .SetEase(Ease.Linear);
        //     
        //     mesh.transform.DOMoveY(initialPosition.y + moveHeight, moveDuration)
        //         .SetLoops(-1, LoopType.Yoyo)
        //         .SetEase(Ease.InOutSine);
        // }
        //
        // Rotate continuously
        instrumentHolder.transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        
        // Move up and down
        instrumentHolder.transform.DOMoveY(initialPosition.y + moveHeight, moveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        // .OnUpdate(() => shadow.transform.position = new Vector3(shadow.transform.position.x, initialPosition.y, shadow.transform.position.z));
        
        // // Rotate continuously
        // transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
        //     .SetLoops(-1, LoopType.Restart)
        //     .SetEase(Ease.Linear);
        //
        // // Move up and down
        // transform.DOMoveY(initialPosition.y + moveHeight, moveDuration)
        //     .SetLoops(-1, LoopType.Yoyo)
        //     .SetEase(Ease.InOutSine);
            // .OnUpdate(() => shadow.transform.position = new Vector3(shadow.transform.position.x, initialPosition.y, shadow.transform.position.z));

        shadow.transform.DOScale(.8f, moveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void Update()
    {
        // shadow.transform.position = new Vector3(shadow.transform.position.x, initialPosition.y, shadow.transform.position.z);
    }

    public void ActivateMesh()
    {
        switch (instruments)
        {
            case Instruments.Violin:
                meshes[0].SetActive(true);
                break;
            case Instruments.Snare:
                meshes[1].SetActive(true);
                break;
            case Instruments.Saxophone:
                meshes[2].SetActive(true);
                break;
            case Instruments.Piano:
                meshes[3].SetActive(true);
                break;
            case Instruments.Kick:
                meshes[4].SetActive(true);
                break;
            case Instruments.Bass:
                meshes[5].SetActive(true);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            PlayerVariables _variables = other.gameObject.GetComponent<PlayerVariables>();
            // print("Player " + player);
            // print("PlayerVar " + _variables);
            if (player is not null)
            {
                _playInstrument = other.gameObject.GetComponent<PlayInstrument>();
                if (!_variables._hasInstrument && _playInstrument is not null)
                {
                    _playInstrument.ChangeEnum(instruments);
                    _variables._hasInstrument = true;

                    _variables.selectedAbilityIndex = abilityIndex;
                    _variables.playerUI.ChangePatternUI();

                    _variables.playerEvents.PublishEvent("PickupInstrument");
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _pickupRadius);
    }
}
