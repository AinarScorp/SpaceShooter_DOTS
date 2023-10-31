using System.Collections;
using System.Collections.Generic;
using SpaceShooter.Components;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HealthText;
    
    Entity playerEntity;
    EntityManager entityManager;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        
        playerEntity = entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
    }

    void Update()
    {
        if (playerEntity == Entity.Null)
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        
            playerEntity = entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
            
            HealthText.text = $"Health: {123}";

            return;
        }

        float playerCurrentHealth = entityManager.GetComponentData<PlayerHealth>(playerEntity).CurrentHealth;
        HealthText.text = $"Health: {playerCurrentHealth}";
    }
}
