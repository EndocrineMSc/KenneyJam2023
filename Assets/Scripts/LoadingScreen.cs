using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Characters;
using UnityEngine.UI;
using DG.Tweening;

namespace Utility
{
    internal class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CharacterCollection _allMobs;
        [SerializeField] private Image _loadImage;

        private bool _isPunching;

        private void Start()
        {
            InstantiateRandomMob();
            StartCoroutine(WaitForLoadingScreen());
        }

        private IEnumerator WaitForLoadingScreen()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(LoadHelper.SceneToBeLoaded.ToString());
        }

        private void InstantiateRandomMob()
        {
            var mobList = new List<GameObject>();
            foreach (var entry in  _allMobs.AllMobs)
                mobList.Add(entry.Value);

            int randomIndex = UnityEngine.Random.Range(0, mobList.Count);
            var mob = mobList[randomIndex];
            _loadImage.sprite = mob.GetComponent<SpriteRenderer>().sprite;
        }

        private void Update()
        {
            if (!_isPunching)
                StartCoroutine(Punch());
        }

        private IEnumerator Punch()
        {
            _isPunching = true;
            _loadImage.transform.DOPunchScale(_loadImage.transform.localScale * 1.2f, 1, 1, 1);
            yield return new WaitForSeconds(1);
            _isPunching = false;
        }
    }
}
