using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField]
    private RectTransform _mountRootTransform;

    [SerializeField]
    private AssetReferenceGameObject _loadPrefab;

    private List<AsyncOperationHandle<GameObject>> _addressablePrefabs = new List<AsyncOperationHandle<GameObject>>();

    private void Start()
    {
        CreatePrefab();
    }

    private void OnDestroy()
    {

        foreach (var addressablePrefab in _addressablePrefabs)
            Addressables.ReleaseInstance(addressablePrefab);

        _addressablePrefabs.Clear();
    }

    private void CreatePrefab()
    {
        var addressablePrefab = Addressables.InstantiateAsync(_loadPrefab, _mountRootTransform);
        _addressablePrefabs.Add(addressablePrefab);
    }

    private IEnumerator UnloadAssets()
    {
        yield return new WaitForSeconds(4f);
        foreach (var go in _addressablePrefabs)
        {
            Addressables.ReleaseInstance(go);
        }
    }
}
