using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    private float healt = 100f;

    public void TakeDamage(float _amount, Image _healtImage)
    {
        GetComponent<PlayerController>().playHurtSound();
        healt -= _amount;
        float x = healt / 100f;
        _healtImage.fillAmount = x;
        _healtImage.color = Color.Lerp(Color.red, Color.green, x);

        if (healt <= 0)
        {
           Die();
        }
    }

    public void IncreaseHealt(float _amount, Image _healtImage,GameObject go)
    {
        GetComponent<PlayerController>().playTakingHeartSound();
        if (healt < 100f)
            healt += _amount;
        float x = healt / 100f;
        _healtImage.fillAmount = x;
        _healtImage.color = Color.Lerp(Color.red, Color.green, x);
        Destroy(go);
    }

    void Die()
    {
        GetComponent<PlayerController>().playDieSound();
        GetComponent<PlayerController>().gControl.GameOver();
    }
}
