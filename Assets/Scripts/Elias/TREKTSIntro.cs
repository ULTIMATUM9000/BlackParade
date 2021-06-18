using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TREKTSIntro : MonoBehaviour
{
    [SerializeField] SpriteRenderer _Logo;
    [SerializeField] AudioSource _AudioSource;

    bool _IsDissipate = false;
    void Start()
    {
        _AudioSource.Play();
        _Logo.color = _Logo.color + new Color(0, 0, 0, -1);
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsDissipate)
        {
            _Logo.color = _Logo.color - new Color(0, 0, 0, Time.deltaTime);

            if (_Logo.color.a <= 0)
                SceneManager.LoadScene(1);
        }

        else
            _Logo.color = _Logo.color + new Color(0, 0, 0, Time.deltaTime);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        _IsDissipate = true;
    }
}
