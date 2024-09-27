using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
}
