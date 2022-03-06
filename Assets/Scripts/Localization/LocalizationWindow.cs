using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationWindow : MonoBehaviour
{
    [SerializeField] private Transform _buttonsParent;

    [SerializeField]
    private Button _buttonPrefab;


    private void Start()
    {
        StartCoroutine(WaitLocale());
    }

    private IEnumerator WaitLocale()
    {
        yield return LocalizationSettings.InitializationOperation;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        foreach (var locale in locales)
        {
            CreateButtonForLocale(locale);
        }
    }

    private void CreateButtonForLocale(Locale locale)
    {
        var go = GameObject.Instantiate(_buttonPrefab, _buttonsParent);
        var text = go.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
            text.text = locale.Identifier.Code;
        go.onClick.AddListener(()=>LocalizationSettings.SelectedLocale = locale);
    }

    private void ChangeLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
