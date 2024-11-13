using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;

    private List<Transform> detectedEnemies = new List<Transform>();
    private Transform character;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] ShootingBehavior shootingBehavior;
    private bool canShoot = false;

    [SerializeField] private int interactionSound;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameManager.Instance.audioManager;
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
        transform.SetParent(character.transform);
        transform.localPosition = new Vector3(0, 3f, 0);
        PlayInteractionSound();

        Collider petCollider = GetComponent<Collider>();
        Collider characterCollider = character.GetComponent<Collider>();
        if (petCollider != null && characterCollider != null)
        {
            Physics.IgnoreCollision(petCollider, characterCollider, true);
        }

        canShoot = true;
    }

    private void Start()
    {
        Character playerComponent = FindObjectOfType<Character>();
        if (playerComponent != null)
        {
            character = playerComponent.transform;
        }
    }

    private void Update()
    {
        if (canShoot && detectedEnemies.Count > 0)
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                LookAtEnemy(closestEnemy);
                Vector3 shootDirection = (closestEnemy.position - transform.position).normalized;
                shootingBehavior.Shoot(shootDirection);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            if (!detectedEnemies.Contains(other.transform))
            {
                detectedEnemies.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            detectedEnemies.Remove(other.transform);
        }
    }

    private Transform FindClosestEnemy()
    {
        detectedEnemies.RemoveAll(enemy => enemy == null);

        if (detectedEnemies.Count == 0) return null;

        Transform[] enemiesArray = detectedEnemies.ToArray();
        QuickSort(enemiesArray, 0, enemiesArray.Length - 1);

        return enemiesArray[0];
    }

    private void LookAtEnemy(Transform enemy)
    {
        Vector3 direction = (enemy.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    private void QuickSort(Transform[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            QuickSort(arr, left, pivot);
            QuickSort(arr, pivot + 1, right);
        }
    }

    private int Partition(Transform[] arr, int left, int right)
    {
        float pivotDistance = Vector3.Distance(transform.position, arr[(left + right) / 2].position);

        while (true)
        {
            while (Vector3.Distance(transform.position, arr[left].position) < pivotDistance && left < right)
            {
                left++;
            }
            while (Vector3.Distance(transform.position, arr[right].position) > pivotDistance && left < right)
            {
                right--;
            }
            if (left < right)
            {
                Transform temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                return right;
            }
        }
    }
}