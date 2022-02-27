using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public GameObject player;
    public float lifeTime;
    public float minDistance;
    public float maxDistance;

    private Vector3 initialPosition;
    [SerializeField] private Vector3 targetPosition;
    private float timePass;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //transform.LookAt(transform.position - player.transform.position);
        bool direction = Random.value > 0.5f;
        float distance = Random.Range(minDistance, maxDistance);
        float distanceY = Random.Range(minDistance, maxDistance);
        initialPosition = transform.position;
        if (direction) targetPosition = initialPosition + new Vector3(distance, distanceY, 0f); ;
        if (!direction) targetPosition = initialPosition + new Vector3(-distance, distanceY, 0f); ;
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        Quaternion lookAt = Quaternion.LookRotation(player.transform.position - transform.position);
        Quaternion actualRotation = transform.rotation;
        transform.rotation = Quaternion.Lerp(actualRotation, lookAt, 1);
        timePass += Time.deltaTime;
        float fraction = lifeTime / 4f;

        if (timePass >= lifeTime) Destroy(gameObject);
        else if (timePass > (fraction * 3)) text.color = Color.Lerp(text.color, Color.clear, (timePass - fraction) / (lifeTime - fraction) * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, Mathf.Sin(timePass / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timePass / lifeTime));
    }
    public void SetDamageNumber(float damage)
    {
        text.text = damage.ToString();
    }
}
