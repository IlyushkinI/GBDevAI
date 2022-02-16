using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    private const int MaxLevelCrime = 5;
    private const int AbxoluteZero = 0;

    [SerializeField]
    private TMP_Text _countMoneyText;//

    [SerializeField]
    private TMP_Text _countHealthText;//

    [SerializeField]
    private TMP_Text _countPowerText;//

    [SerializeField]
    private TMP_Text _countCrimeText;//

    [SerializeField]
    private TMP_Text _countPowerEnemyText;//

    [SerializeField]
    private TMP_Text _countPistolsPowerEnemyText;//

    [SerializeField]
    private TMP_Text _countKnifePowerEnemyText;


    [SerializeField]
    private Button _addMoneyButton;//

    [SerializeField]
    private Button _minusMoneyButton;//


    [SerializeField]
    private Button _addHealthButton;//

    [SerializeField]
    private Button _minusHealthButton;//


    [SerializeField]
    private Button _addPowerButton;//

    [SerializeField]
    private Button _minusPowerButton;//

    [SerializeField]
    private Button _addCrimeButton;//

    [SerializeField]
    private Button _minusCrimeButton;//

    [SerializeField]
    private Button _fightButton;//

    [SerializeField]
    private Button _leaveFightButton;//

    [SerializeField]
    private Button _fightPistolsButton;//

    [SerializeField]
    private Button _fightKnifesButton;//

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountCrimePlayer;

    private void Start()
    {
        _enemy = new Enemy("Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _crime = new Crime(nameof(Crime));
        _crime.Attach(_enemy);

        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));

        _addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
        _minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));

        _fightButton.onClick.AddListener(OverallFight);

        _leaveFightButton.onClick.AddListener(LeaveFight);

        _fightPistolsButton.onClick.AddListener(PistolsFight);
        _fightKnifesButton.onClick.AddListener(KnifeFight);
    }


    private void LeaveFight()
    {
        if(_allCountCrimePlayer <= 2)
        {
            _leaveFightButton.GetComponent<Image>().color = Color.green;
            Debug.Log("Leaving The Fight");
        }
        else
        {
            _leaveFightButton.GetComponent<Image>().color = Color.grey;
            Debug.Log("Can't Leaving The Fight");
        }
        
    }

    private void PistolsFight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.PistolsPower ? "Win Pistols" : "Lose Pistols");
    }

    private void KnifeFight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.KnifePower ? "Win Knifes" : "Lose Knifes");
    }

    private void OverallFight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.OverallPower ? "Win" : "Lose");
    }

    private void CheckUpperAndLowerMeaning(ref int checkInt, int upperBoard, int lowerBoard, bool checkUpper = false, bool checkLower = false)
    {
        if (checkUpper && checkInt > upperBoard)
        {
            checkInt = upperBoard;
        }
        if(checkLower && checkInt < lowerBoard)
        {
            checkInt = lowerBoard;
        }
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        CheckUpperAndLowerMeaning(ref _allCountPowerPlayer, AbxoluteZero, AbxoluteZero, false, true);

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        CheckUpperAndLowerMeaning(ref _allCountHealthPlayer, AbxoluteZero, AbxoluteZero, false, true);

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        CheckUpperAndLowerMeaning(ref _allCountMoneyPlayer, AbxoluteZero, AbxoluteZero, false, true);

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeCrime(bool isAddCount)
    {
        if (isAddCount)
            _allCountCrimePlayer++;
        else
            _allCountCrimePlayer--;

        CheckUpperAndLowerMeaning(ref _allCountCrimePlayer, MaxLevelCrime, AbxoluteZero, true, true);

        ChangeDataWindow(_allCountCrimePlayer, DataType.Crime);
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoneyText.text = $"Player Money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;

            case DataType.Health:
                _countHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                _countPowerText.text = $"Player Power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;

            case DataType.Crime:
                _countCrimeText.text = $"Player Crime: {countChangeData}";
                _crime.CountCrime = countChangeData;
                break;
        }

        _countPowerEnemyText.text = $"Enemy Power: {_enemy.OverallPower}";
        _countPistolsPowerEnemyText.text = $"Enemy Power Pistols: {_enemy.PistolsPower}";
        _countKnifePowerEnemyText.text = $"Enemy Power Knife: {_enemy.KnifePower}";
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeButton.onClick.RemoveAllListeners();
        _minusCrimeButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        _leaveFightButton.onClick.RemoveAllListeners();

        _fightPistolsButton.onClick.RemoveAllListeners();

        _fightKnifesButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _crime.Detach(_enemy);
    }

}
