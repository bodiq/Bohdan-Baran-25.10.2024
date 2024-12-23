using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Tiles
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private IndicatorInformation indicatorInformation;
        [SerializeField] private ParticleSystem fillingParticle;
        
        private MainTile _nextMainTileToOpen;
        private MainTile _myMainTile;

        public MainTile NextMainTileToOpen => _nextMainTileToOpen;

        private bool _isCollecting = false;

        private Coroutine _collectingCoroutine;
        private Tween _activationAnimationTween;
        private Tween _scaleAnimationTween;
        
        private readonly Dictionary<ResourcesIndicator, int> _resourcesTextIndicatorsToIncrease = new();
        private readonly Dictionary<ResourcesIndicator, int> _resourcesTextRemainderToIncrease = new();

        private static readonly Vector3 EndScaleValue = new (1.0f, 1.0f, 1.0f);
        private static readonly Vector3 StartScaleValue = new (0.8f, 0.8f, 0.8f);

        private static readonly float DurationAnimationScale = 0.3f;
        
        public void SetIndicatorDependence(MainTile mainTile)
        {
            _myMainTile = mainTile;
        
            if (_myMainTile != null)
            {
                _nextMainTileToOpen = _myMainTile.neighbours[gameObject.transform.GetSiblingIndex()];
                if (_nextMainTileToOpen == null || _nextMainTileToOpen.IsTileUnlocked)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.localScale = Vector3.zero;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void ActivateIndicator()
        {
            gameObject.SetActive(true);
            _nextMainTileToOpen.ReserveTile();
            
            _activationAnimationTween = transform.DOScale(indicatorInformation.AnimationEndScale, indicatorInformation.ActivationAnimationDuration).OnComplete(() =>
            {
                _nextMainTileToOpen.ResourcesIndicatorManager.gameObject.SetActive(true);
                _nextMainTileToOpen.ResourcesIndicatorManager.MyMainTile = _nextMainTileToOpen;
                _nextMainTileToOpen.MyIndicator = this;
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollecting || !other.CompareTag("Player"))
                return; 
            
            _resourcesTextIndicatorsToIncrease.Clear();
            _resourcesTextRemainderToIncrease.Clear();
            
            _scaleAnimationTween?.Kill();
            _scaleAnimationTween = transform.DOScale(EndScaleValue, DurationAnimationScale);
            
            fillingParticle.Play();

            foreach (var indicator in _nextMainTileToOpen.ResourcesIndicatorManager.ActiveResourceIndicators)
            {
                if (GameManager.Instance.Player.PlayerResourceCount.TryGetValue(indicator.Key, out var value))
                {
                    var hasToBeEarned = indicator.Value.ResourceToEarn - indicator.Value.ResourceEarned;

                    hasToBeEarned = Mathf.Min(hasToBeEarned, value);
                    
                    
                    if (hasToBeEarned > 0)
                    {
                        var countToIncrease = 0;
                        if (hasToBeEarned <= 20)
                        {
                            countToIncrease = 1;
                        }
                        else
                        {
                            countToIncrease = hasToBeEarned / 20;
                            var remainder = hasToBeEarned % 20;
                            indicator.Value.RemainderCount = remainder;
                            _resourcesTextRemainderToIncrease.Add(indicator.Value, remainder);
                        }
                        indicator.Value.CountToIncrease = countToIncrease;
                        _resourcesTextIndicatorsToIncrease[indicator.Value] = countToIncrease;
                    }
                }
            }
            
            if (!_isCollecting)
            {
                _collectingCoroutine = StartCoroutine(CollectResource());
            }
        }


        private void OnTriggerExit(Collider other)
        {
            _isCollecting = false;
            if (_collectingCoroutine != null)
            {
                StopCoroutine(_collectingCoroutine);
                _collectingCoroutine = null;
            }
            
            fillingParticle.Stop();
            
            _scaleAnimationTween?.Kill();
            _scaleAnimationTween = transform.DOScale(StartScaleValue, DurationAnimationScale);
        }

        private IEnumerator CollectResource()
        {
            _isCollecting = true;

            while (!_nextMainTileToOpen.ResourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
            {
                foreach (var resourcesIndicator in _resourcesTextIndicatorsToIncrease)
                {
                    if (GameManager.Instance.Player.PlayerResourceCount[resourcesIndicator.Key.ResourceType] <= 0)
                    {
                        continue;
                    }
                    var resourceIndicator = resourcesIndicator;
                    if (!resourceIndicator.Key.IsResourcesFull)
                    {
                        resourceIndicator.Key.IncreaseResourceCount();
                        var resource = ResourcePoolManager.Instance.GetResource(resourceIndicator.Key.ResourceType);
                        resource.gameObject.SetActive(true);
                        var remainder = 0;
                        if (_resourcesTextRemainderToIncrease.TryGetValue(resourceIndicator.Key, out var value))
                        {
                            remainder = value;
                            _resourcesTextRemainderToIncrease.Remove(resourceIndicator.Key);
                        }
                        resource.AnimateResourceForTileFilling(GameManager.Instance.Player.transform.position, _nextMainTileToOpen.ResourcesEndPoint.position, resourceIndicator.Key, resourcesIndicator.Value, remainder);
                    }
                }

                if (_nextMainTileToOpen.ResourcesIndicatorManager.CheckIfResourceAreFull())
                {
                    _isCollecting = false;
                    fillingParticle.Stop();
                }

                yield return indicatorInformation.WaitBetweenSpawnResources;
            }
            _isCollecting = false;
        }

        private void OnDisable()
        {
            _activationAnimationTween?.Kill();
            _scaleAnimationTween?.Kill();
            if (_collectingCoroutine != null)
            {
                StopCoroutine(_collectingCoroutine);
                _collectingCoroutine = null;
            }
        }
    }
}
