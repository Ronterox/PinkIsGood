using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHP = 20;
    public int currentHP = 20;

    [SerializeField] Animator animator = null;

    private void Start()
    {
        currentHP = maxHP;
        if (gameObject.tag.Equals("Player") && GameManager.instance != null)
        {
            GameManager.instance.healthText.text = currentHP.ToString();
            GameManager.instance.heartAnimator.SetFloat("heartbeat", 1);
        }
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
        }

        if (animator != null)
            animator.SetTrigger("Get Hit");


        if (gameObject.tag.Equals("Player"))
        {
            GameManager.instance.healthText.text = currentHP.ToString();
            AudioManager.instance.Play("Hit Player");
            if (currentHP <= maxHP / 3)
                GameManager.instance.heartAnimator.SetFloat("heartbeat", 5);
            else if(currentHP <= maxHP/1.5)
                GameManager.instance.heartAnimator.SetFloat("heartbeat", 2.5f);
            FindObjectOfType<Camera>().GetComponent<Animator>().SetTrigger("Shake");
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
