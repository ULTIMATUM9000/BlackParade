using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Player _Player;
    [SerializeField] ParadeMovement _Parade;
    [Space]
    [SerializeField] Image _HealthTransform;
    [SerializeField] RectTransform _SlashTransform;
    [SerializeField] RectTransform _BowTransform;
    [SerializeField] TextMeshProUGUI _PersonsJoinedText;
    [SerializeField] RectTransform _ParadeProgressTransform;
    [SerializeField] float _StartPosX = 0;
    [SerializeField] float _EndPosX = 0;

    float _Distance = 0;

    void Start()
    {
        _Distance = Vector2.Distance(Vector2.right * _StartPosX, Vector2.right * _EndPosX);
        _SlashTransform.sizeDelta = new Vector2(_SlashTransform.sizeDelta.x, 0);
        _BowTransform.sizeDelta = new Vector2(_BowTransform.sizeDelta.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _HealthTransform.fillAmount = (float)_Player.CurrentHealth / (float)_Player.MaxHealth;

        _Player.Ability1.CoolDownFuncs = (float timeLeft, float coolDownTime) => _SlashTransform.sizeDelta = new Vector2(_SlashTransform.sizeDelta.x, timeLeft / coolDownTime * 100);
        _Player.Ability2.CoolDownFuncs = (float timeLeft, float coolDownTime) => _BowTransform.sizeDelta = new Vector2(_BowTransform.sizeDelta.x, timeLeft / coolDownTime * 100);

        _ParadeProgressTransform.localPosition = new Vector3(_StartPosX + _Distance * _Parade.percent, _ParadeProgressTransform.localPosition.y);

        _PersonsJoinedText.text = ": " + ScoreManager.Score.ToString();
    }
}