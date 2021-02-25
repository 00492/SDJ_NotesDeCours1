using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Slider _lifebar;

    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _xpCountText;

    private int _killCount;
    private int _xp;

    private void Start()
    {
        PlayerController._onKill += UpdateXP;
        PlayerController._onKill += UpdateKillCount;

        PlayerController._onPlayerHit += UpdateHP;
    }

    private void UpdateXP(int xp)
    {
        _xp += xp;
        _xpCountText.SetText("Total XP : " + _xp);
    }

    private void UpdateKillCount(int xp)
    {
        _killCount++;
        _killCountText.SetText("Kill Count : " + _killCount);
    }

    private void UpdateHP()
    {
        _lifebar.value -= 0.1f;
    }

    private void OnDestroy()
    {
        PlayerController._onKill -= UpdateXP;
        PlayerController._onKill -= UpdateKillCount;

        PlayerController._onPlayerHit -= UpdateHP;
    }
}
