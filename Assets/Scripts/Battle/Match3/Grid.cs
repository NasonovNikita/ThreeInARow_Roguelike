using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Battle.Match3
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int sizeX;
        [SerializeField] private int sizeY;
    
        [SerializeField] private Vector2 stepX;
        [SerializeField] private Vector2 stepY;
        [SerializeField] private Vector3 baseScale;
        [SerializeField] private Vector3 chosenScale;
        
    
        [SerializeField]
        private Gem[] prefabs;

        [SerializeField] internal float moveTime;
        [SerializeField] internal float scaleTime;
        [SerializeField] private float refreshTime;

        public static Action onEnd;

        public Dictionary<GemType, int> destroyed;

        public BattleManager manager;

        public GridState state;

        private Gem _first;
        private Gem _second;
    
        private Gem[,] _box;

        public void Awake()
        {
            manager = FindFirstObjectByType<BattleManager>();
        
            _box = new Gem[sizeY, sizeX];
            SmartGenGems();
            ClearDestroyed();
        }

        public void OnDestroy()
        {
            foreach (var gem in _box)
            {
                if (gem == null) return;
                Destroy(gem.gameObject);
            }
        }

        private Gem GenGem(int i, int j, int type = 0)
        {
            Gem gem = prefabs[type != 0 ? type : Random.Range(0, prefabs.Length)];
            var gemTransform = gem.transform;
            gemTransform.position = (Vector2)transform.position + stepX * j + stepY * i;
            gem.grid = this;
            gemTransform.localScale = baseScale;
            return gem;
        }

        public void Block()
        {
            state = GridState.Blocked;
        }
        public void Unlock()
        {
            state = GridState.Choosing1;
        }

        public IEnumerator OnClick(Gem gem)
        {
            switch (state)
            {
                case GridState.Choosing1:
                
                    _first = gem;
                
                    _first.Scale(chosenScale, scaleTime);
                
                    state = GridState.Choosing2;
                
                    yield return new WaitForSeconds(scaleTime);
                    break;
            
                case GridState.Choosing2 when gem == _first:
                
                    _first.Scale(baseScale, scaleTime);
                
                    _first = null;
                
                    state = GridState.Choosing1;
                
                    yield return new WaitForSeconds(scaleTime);
                    break;
            
                case GridState.Choosing2:
                {
                    _second = gem;
                
                
                    if (GemsAreNeighbours(_first, _second))
                    {
                    
                        _second.Scale(chosenScale, scaleTime);
                        yield return new WaitForSeconds(scaleTime);
                    
                        state = GridState.Moving;
                        StartCoroutine(MoveGems(_first, _second));
                    
                        _first = null;
                        _second = null;
                    }
                    else
                    {
                        _first.Scale(baseScale, scaleTime);
                        _second.Scale(chosenScale, scaleTime);
                    
                        _first = _second;
                        _second = null;
                    
                        yield return new WaitForSeconds(scaleTime);
                    }
                    break;
                }
                case GridState.Moving: break;
                case GridState.Refreshing: break;
                case GridState.Blocked: break;
                case GridState.EnemyChoosing1: break;
                case GridState.EnemyChoosing2: break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerator Choose(int i, int j)
        {
            if (state != GridState.EnemyChoosing1 && state != GridState.EnemyChoosing2)
            {
                state = GridState.EnemyChoosing1;
                
                _first = _box[i, j];
                _first.Scale(chosenScale, scaleTime);
                
                yield return new WaitForSeconds(scaleTime);
            }
            else
            {
                state = GridState.EnemyChoosing2;
                
                _second = _box[i, j];
                _second.Scale(chosenScale, scaleTime);
                
                yield return new WaitForSeconds(scaleTime);

                state = GridState.Moving;

                StartCoroutine(MoveGems(_first, _second));

                _first = null;
                _second = null;
            }
        }

        public void ClearDestroyed()
        {
            destroyed = new Dictionary<GemType, int>
            {
                { GemType.Red, 0 },
                { GemType.Blue, 0 },
                { GemType.Green, 0 },
                { GemType.Yellow, 0 },
                { GemType.Mana, 0 }
            };
        }

        public Gem[,] BoxCopy()
        {
            var b = new Gem[sizeY,sizeX];
            Array.Copy(_box, b, _box.Length);
            return b;
        }

        private IEnumerator MoveGems(Gem gem1, Gem gem2)
        {
            int[] pos1 = FindGem(gem1);
            int[] pos2 = FindGem(gem2);
        
            gem1.Move(gem2.transform.position, moveTime);
            gem2.Move(gem1.transform.position, moveTime);
            yield return new WaitForSeconds(moveTime);
        
            _box[pos2[0], pos2[1]] = gem1;
            _box[pos1[0], pos1[1]] = gem2;
        
            gem1.Scale(baseScale, scaleTime);
            gem2.Scale(baseScale, scaleTime);
            yield return new WaitForSeconds(scaleTime);
        
            state = GridState.Refreshing;
            StartCoroutine(Refresh());
        }

        private IEnumerator Refresh()
        {
            var toDelete = new HashSet<Gem>();
        
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (HorizontalRowExists(i, j, _box))
                    {
                        toDelete.Add(_box[i, j]);
                        toDelete.Add(_box[i, j + 1]);
                        toDelete.Add(_box[i, j + 2]);
                    }

                    // ReSharper disable once InvertIf
                    if (VerticalRowExists(i, j, _box))
                    {
                        toDelete.Add(_box[i, j]);
                        toDelete.Add(_box[i + 1, j]);
                        toDelete.Add(_box[i + 2, j]);
                    }
                }
            }

            if (toDelete.Count == 0)
            {
                GridLog.Log(destroyed);
                onEnd?.Invoke();
                Block();
                yield break;
            }
        
            foreach (Gem gem in toDelete)
            {
                destroyed[gem.Type] += 1;
            }

            yield return new WaitForSeconds(refreshTime);
            foreach (Gem gem in toDelete)
            {
                Destroy(gem.gameObject);
            }

        
            yield return new WaitForSeconds(refreshTime);
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (_box[i, j] == null)
                    {
                        _box[i, j] = Instantiate(GenGem(i, j));
                    }
                }
            }
        
            StartCoroutine(Refresh());
        }

        private void SmartGenGems()
        {
            for (int i = sizeY - 1; i >= 0; i--)
            {
                for (int j = sizeX - 1; j >= 0; j--)
                {
                    do
                    {
                        _box[i, j] = GenGem(i, j);
                    } while (HorizontalRowExists(i, j, _box) || VerticalRowExists(i, j, _box));

                    _box[i, j] = Instantiate(_box[i, j]);
                }
            }
        }
    
        private int[] FindGem(Object gem)
        {
            int[] res = { 0, 0 };
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (_box[i, j] == gem)
                    {
                        res = new[] { i, j };
                    }
                }
            }
            return res;
        }

        public bool GemsAreNeighbours(Object gem1, Object  gem2)
        {
            int[] pos1 = FindGem(gem1);
            int[] pos2 = FindGem(gem2);
        
            return pos1[0] == pos2[0] && Math.Abs(pos1[1] - pos2[1]) == 1 ||
                   pos1[1] == pos2[1] && Math.Abs(pos1[0] - pos2[0]) == 1;
        }

        public static bool HorizontalRowExists(int i, int j, Gem[,] box)
        {
            return j < box.GetLength(1) - 2 && box[i, j].Type == box[i, j + 1].Type && box[i, j].Type == box[i, j + 2].Type;
        }

        public static bool VerticalRowExists(int i, int j, Gem[,] box)
        {
            return i < box.GetLength(0) - 2 && box[i, j].Type == box[i + 1, j].Type && box[i, j].Type == box[i + 2, j].Type;
        }
    }
}