using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet = null;
    [SerializeField] GameObject bulletRepealable = null;

    [SerializeField] Transform shotPosition = null;

    [SerializeField] int shotsUntilRepeal = 3;
    [SerializeField] float timeBTWShotsValue = 1f;
    private float timeBTWShots;
    private int shots = 0;

    [SerializeField] Animator animator = null;

    private void Update()
    {
        if (timeBTWShots > 0)
            timeBTWShots -= Time.deltaTime;
        else
        {
            timeBTWShots = timeBTWShotsValue;
            if (shots == shotsUntilRepeal)
            {
                Instantiate(bulletRepealable, shotPosition.position, Quaternion.identity);
                shots = 0;
            }
            else
            {
                Instantiate(bullet, shotPosition.position, Quaternion.identity);
                shots++;
            }
            AudioManager.instance.Play("Monster Attack");
            if (animator != null)
                animator.SetTrigger("Shoot");
        }
    }
}
