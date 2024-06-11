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

    bool EnoughMoney(int value) => _money < value;
    int GetMoney() => _money;

    void SetMoney(int value) {
        _money = value;
    }

    bool TryGetPayment(int value) {
        if (!EnoughMoney(value)) return false;
        
        SetMoney(_money - value);
        return true;
    }

    void UpdateUI() {
        moneyT.text = $"{_money:N0}원";
    }
}
 