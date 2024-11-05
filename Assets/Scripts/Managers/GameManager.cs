using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Input Setup")]
    [SerializeField] private Joystick joystick;

    [SerializeField] private PlayerCharacter player;
    

    public Joystick Joystick => joystick;
    public PlayerCharacter Player => player;

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
        
        DOTween.SetTweensCapacity(2000, 500);
    }

    private void Start()
    {
        UIManager.Instance.Initialize();
    }
}
