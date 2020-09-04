using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHP = 20;
    public int currentHP = 20;

    [SerializeField] Animator animator = null;
    [SerializeField] GameObject particles = null;

    private void Start()
    {
        currentHP = maxHP;
        if (gameObject.tag.Equals("Player") && GameManager.instance != null)
            GameManager.instance.healthText.text = currentHP.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            if (!gameObject.tag.Equals("Player"))
                StartCoroutine(Die());
            else
                GameManager.instance.KillPlayer();

            if (particles != null)
            {
                Destroy(Instantiate(particles, transform.position, Quaternion.identity), 20f);
            }
        }

        if (animator != null)
            animator.SetTrigger("Get Hit");


        if (gameObject.tag.Equals("Player"))
        {
            GameManager.instance.healthText.text = currentHP.ToString();
            AudioManager.instance.Play("Hit Player");
        }
        else
            AudioManager.instance.Play("Hit Enemy");
    }

    public IEnumerator Die()
    {
        if (gameObject.tag.Equals("Player"))
        {
            Time.timeScale = 0;
            gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
            GameManager.instance.ResetGame();
        }
        else if (gameObject.tag.Equals("Enemy"))
        {
            GameManager.instance.GainKill();
        }
        Destroy(gameObject);
    }
}
