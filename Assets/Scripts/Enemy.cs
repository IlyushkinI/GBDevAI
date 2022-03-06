using UnityEngine;

public class Enemy : IEnemy
{
    private const int PistolsPowerAddition = 3;
    private const int KnifePowerAddition = 2;
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimePlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                break;

            case DataType.Money:
                _moneyPlayer = dataPlayer.CountMoney;
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                break;

            case DataType.Crime:
                _crimePlayer = dataPlayer.CountCrime;
                break;
        }

        Debug.Log($"Update {_name}, change {dataType}");
    }

    public int OverallPower
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer + _crimePlayer;
            return power;
        }
    }
    public int PistolsPower
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer + PistolsPowerAddition + _crimePlayer;
            return power;
        }
    }

    public int KnifePower
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer + _crimePlayer + KnifePowerAddition;
            return power;
        }
    }
}
