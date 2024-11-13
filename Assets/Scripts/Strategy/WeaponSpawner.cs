﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Low;

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private int interactionSound;
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void PlayInteractionSound()
    {
        if (interactionSound >= 0 && interactionSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(interactionSound);
        }
    }

    public void Interact()
    {
        if (weapons != null && weapons.Length > 0)
        {
            int randomIndex = Random.Range(0, weapons.Length);
            GameObject randomWeapon = weapons[randomIndex];
            
            PlayInteractionSound();
            Instantiate(randomWeapon, spawnPoint.position, spawnPoint.rotation);
            
            Destroy(gameObject);
        }
    }
}
