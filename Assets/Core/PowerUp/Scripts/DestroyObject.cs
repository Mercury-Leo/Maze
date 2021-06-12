using System;
using System.Collections;
using UnityEngine;

namespace Core.PowerUp.Scripts
{
    public class DestroyObject : MonoBehaviour
    {
        private const float DESTROY_TIME = 720f;

        private void Start()
        {
            StartCoroutine(DestroyCounter());
        }
    
        private IEnumerator DestroyCounter()
        {
            yield return new WaitForSeconds(DESTROY_TIME);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            foreach (Transform child in transform.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
