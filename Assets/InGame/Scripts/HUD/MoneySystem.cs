using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoSingleton<MoneySystem>
{
    [SerializeField] int _money = 0;
    [SerializeField] TextMeshProUGUI moneyT;

    private void Awake() {
        UpdateUI();
    }

    public bool EnoughMoney(int value) => _money >= value;
    public int GetMoney() => _money;

    public void SetMoney(int value) {
        _money = value;
        UpdateUI();
    }

    public bool TryGetPayment(int value) {
        if (!EnoughMoney(value)) return false;
        
        SetMoney(_money - value);
        return true;
    }
    public void GiveMoney(int value) {
        SetMoney(_money + value);
    }

    void UpdateUI() {
        moneyT.text = $"{_money:N0}원";
    }
}
 