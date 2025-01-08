using System;
using TMPro;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    GameObject damageTextPrefab;
    public GameObject healthBarPrefab;
    public Transform healthBarTransform;
    public float enemyMaxHealth;
    public float enemyCurrentHealth;
    HealthBarManager healthBarManager;
    GameObject player;

    public int scoreOfEnemy = 1;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damageTextPrefab = Resources.Load<GameObject>("Prefab/Damage Text");
        CreateHealthBar();
    }

    void CreateHealthBar()
    {
        var healthbarObject = Instantiate(healthBarPrefab, healthBarTransform.position, Quaternion.identity, healthBarTransform);
        healthBarManager = healthbarObject.GetComponent<HealthBarManager>();
        healthBarManager.SetHelth(enemyCurrentHealth, enemyMaxHealth);
    }

    public void UpdateHealth(float number)
    {
        ShowDamege(number);
        enemyCurrentHealth = enemyCurrentHealth + number;
        if(enemyCurrentHealth <= 0)
        {
            player.GetComponent<PickItemsManager>().UpdateScore(scoreOfEnemy);
            Destroy(this.gameObject);
        }
        healthBarManager.SetHelth(enemyCurrentHealth,enemyMaxHealth);
    }

    private void ShowDamege(float damage)
    {
        var damageText =  Instantiate(damageTextPrefab, healthBarTransform.position+new Vector3(0,0.5f,0), Quaternion.identity, null);
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown("b") == true)
        {
            UpdateHealth(-1);
        }
    }
}
