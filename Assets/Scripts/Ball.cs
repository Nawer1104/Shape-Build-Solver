using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 startPos;

    public GameObject vfxOnSuccess;

    public GameObject vfxOnDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetGravityScale(0);

        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            StartCoroutine(RemoveFromList(collision.gameObject));
        }
        else if (collision.gameObject.tag == "Wall")
        {
            GameObject vfx = Instantiate(vfxOnDeath, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }

    public void SetGravityScale(int i)
    {
        rb.gravityScale = i; 
    }

    public void ResetPos()
    {
        SetGravityScale(0);

        transform.position = startPos;
    }

    IEnumerator RemoveFromList(GameObject star)
    {
        GameObject vfx = Instantiate(vfxOnSuccess, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        star.SetActive(false);

        yield return new WaitForSeconds(1);

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].stars.Remove(star);
    }
}
