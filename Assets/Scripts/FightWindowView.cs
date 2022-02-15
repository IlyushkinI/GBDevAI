using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    [SerializeField] private TMP_Text _countPowerText;
    [SerializeField] private TMP_Text _countCrimeRateText;
    [SerializeField] private TMP_Text _countPowerEnemyText;
    [SerializeField] private TMP_Text _fightResult;

    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;
    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;
    [SerializeField] private Button _addCrimeRate;
    [SerializeField] private Button _minusCrimeRate;
    [SerializeField] private Button _setKnife;
    [SerializeField] private Button _setGun;

    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _skipButton;

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCrimeRate;
    private const int _maxCrimeRate = 5;

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

        _addCrimeRate.onClick.AddListener(() => ChangeCrimeRate(true));
        _minusCrimeRate.onClick.AddListener(() => ChangeCrimeRate(false));

        _fightButton.onClick.AddListener(Fight);
        _skipButton.onClick.AddListener(Skip);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeRate.onClick.RemoveAllListeners();
        _minusCrimeRate.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
    }

    private void Fight()
    {
        _fightResult.text = _allCountPowerPlayer >= _enemy.Power ? "Win" : "Lose";
    }

    private void Skip()
    {
        _fightResult.text = "You Succesfully avoid fight";
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            NotNegativeValueControllerOnDecrease(ref _allCountPowerPlayer);

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            NotNegativeValueControllerOnDecrease(ref _allCountHealthPlayer);

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            NotNegativeValueControllerOnDecrease(ref _allCountMoneyPlayer);

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeCrimeRate(bool isAddCount)
    {
        if (isAddCount)
        {
            _allCrimeRate++;

            if (_allCrimeRate > _maxCrimeRate)
                _allCrimeRate = _maxCrimeRate;
        }
        else
            NotNegativeValueControllerOnDecrease(ref _allCrimeRate);

        ChangeDataWindow(_allCrimeRate, DataType.Crime);
    }

    private void NotNegativeValueControllerOnDecrease(ref int value)
    {
        value = value == 0 ? value : value - 1;        
    }

    private void ChangeSkipButtonStatus(int value)
    {
        _skipButton.interactable = value > 2 ? false : true;
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
                _countCrimeRateText.text = $"Player CrimeRate: {countChangeData}";
                _crime.CountCrime = countChangeData;
                ChangeSkipButtonStatus(countChangeData);
                break;
        }

        _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    }
}
