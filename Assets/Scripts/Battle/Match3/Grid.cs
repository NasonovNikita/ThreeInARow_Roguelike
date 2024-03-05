using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Other;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Match3
{
    public class Grid : MonoBehaviour
    {
        public int sizeX;
        public int sizeY;
    
        [SerializeField] private Vector2 stepX;
        [SerializeField] private Vector2 stepY;
        [SerializeField] private Vector3 baseScale;
        [SerializeField] private Vector3 chosenScale;
        
    
        [SerializeField] private Gem[] prefabs;
        public Dictionary<GemType, Gem> prefabsByType;

        [SerializeField] internal float moveTime;
        [SerializeField] internal float scaleTime;
        [SerializeField] private float refreshTime;

        public static Action onEnd;

        public Dictionary<GemType, int> destroyed;
        public GridState state;

        private Gem first;
        private Gem second;

        public Gem[,] box;

        public Dictionary<GemType, int> EmptyTypesCounter => Tools.FilledEnumDictionary<GemType, int>();

        public void Awake()
        {
            box = new Gem[sizeY, sizeX];
            
            prefabsByType = new Dictionary<GemType, Gem>();
            foreach (GemType type in Enum.GetValues(typeof(GemType)))
            {
                prefabsByType[type] = prefabs.First(g => g.Type == type);
            }
            
            SmartGenGems();
            ClearDestroyed();
        }

        public void OnDestroy()
        {
            foreach (var gem in box)
            {
                if (gem == null) return;
                Destroy(gem.gameObject);
            }
        }

        private Gem GenGem(int i, int j, bool random = true, GemType type = GemType.Red)
        {
            Gem gem = prefabsByType[random
                ? Tools.Random.RandomChoose((GemType[])Enum.GetValues(typeof(GemType)))
                : type];
            var gemTransform = gem.transform;
            gemTransform.position = (Vector2)transform.position + stepX * j + stepY * i;
            gem.grid = this;
            gemTransform.localScale = baseScale;
            gem.mover.time = moveTime;
            gem.scaler.time = scaleTime;
            return gem;
        }

        public void ReplaceGem(int i, int j, GemType type)
        {
            Destroy(box[i, j].gameObject);
            box[i, j] = Instantiate(GenGem(i, j, false, type));
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
                
                    first = gem;
                
                    first.Scale(chosenScale);
                
                    state = GridState.Choosing2;
                
                    yield return new WaitUntil(() => first.EndedScale);
                    break;
            
                case GridState.Choosing2 when gem == first:
                
                    first.Scale(baseScale);
                    yield return new WaitUntil(() => first.EndedScale);
                    
                    first = null;
                    state = GridState.Choosing1;
                    break;
            
                case GridState.Choosing2:
                {
                    second = gem;
                
                
                    if (GemsAreNeighbours(first, second))
                    {
                    
                        second.Scale(chosenScale);
                        yield return new WaitUntil(() => second.EndedScale);
                    
                        state = GridState.Moving;
                        yield return StartCoroutine(MoveGems(first, second));
                    
                        first = null;
                        second = null;
                    }
                    else
                    {
                        first.Scale(baseScale);
                        second.Scale(chosenScale);

                        yield return new WaitUntil(() => first.EndedScale);
                        yield return new WaitUntil(() => second.EndedScale);
                    
                        first = second;
                        second = null;
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
                
                first = box[i, j];
                first.Scale(chosenScale);

                yield return new WaitUntil(() => first.EndedScale);
            }
            else
            {
                state = GridState.EnemyChoosing2;
                
                second = box[i, j];
                second.Scale(chosenScale);
                
                yield return new WaitUntil(() => second.EndedScale);

                state = GridState.Moving;

                yield return StartCoroutine(MoveGems(first, second));

                first = null;
                second = null;
            }
        }

        public void ClearDestroyed()
        {
            destroyed = EmptyTypesCounter;
        }

        public Gem[,] BoxCopy()
        {
            var b = new Gem[sizeY,sizeX];
            Array.Copy(box, b, box.Length);
            return b;
        }

        private IEnumerator MoveGems(Gem gem1, Gem gem2)
        {
            var pos1 = FindGem(gem1);
            var pos2 = FindGem(gem2);
        
            gem1.Move(gem2.transform.position);
            gem2.Move(gem1.transform.position);
            yield return new WaitUntil(() => gem1.EndedMove);
        
            box[pos2.Item1, pos2.Item2] = gem1;
            box[pos1.Item1, pos1.Item2] = gem2;
        
            gem1.Scale(baseScale);
            gem2.Scale(baseScale);
            yield return new WaitUntil(() => gem1.EndedScale);
            yield return new WaitUntil(() => gem2.EndedScale);
        
            state = GridState.Refreshing;
            yield return StartCoroutine(Refresh());
        }

        public IEnumerator Refresh()
        {
            var toDelete = GetDestroyedGems(box);

            destroyed = destroyed.ConcatCounterDictionaries(CountGemTypes(toDelete));

            if (toDelete.Count == 0)
            {
                GridLog.Log(destroyed);
                onEnd?.Invoke();
                Block();
                yield break;
            }

            yield return new WaitForSeconds(refreshTime);
            
            foreach (Gem gem in toDelete)
            {
                Destroy(gem.gameObject);
            }

        
            yield return new WaitForSeconds(refreshTime);
            
            RefillGems();
        
            yield return StartCoroutine(Refresh());
        }

        private void RefillGems()
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (box[i, j] == null)
                    {
                        box[i, j] = Instantiate(GenGem(i, j));
                    }
                }
            }
        }

        public List<Gem> GetDestroyedGems(Gem[,] gemBox)
        {
            var deletedGems = new HashSet<Gem>();
        
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (HorizontalRowExists(i, j, gemBox))
                    {
                        deletedGems.Add(gemBox[i, j]);
                        deletedGems.Add(gemBox[i, j + 1]);
                        deletedGems.Add(gemBox[i, j + 2]);
                    }

                    // ReSharper disable once InvertIf
                    if (VerticalRowExists(i, j, gemBox))
                    {
                        deletedGems.Add(gemBox[i, j]);
                        deletedGems.Add(gemBox[i + 1, j]);
                        deletedGems.Add(gemBox[i + 2, j]);
                    }
                }
            }

            deletedGems = new HashSet<Gem>(deletedGems);

            return deletedGems.ToList();
        }

        public Dictionary<GemType, int> CountGemTypes(List<Gem> gems)
        {
            var res = EmptyTypesCounter;

            foreach (var gem in gems)
            {
                res[gem.Type] += 1;
            }

            return res;
        }

        private void SmartGenGems()
        {
            for (int i = sizeY - 1; i >= 0; i--)
            {
                for (int j = sizeX - 1; j >= 0; j--)
                {
                    do
                    {
                        box[i, j] = GenGem(i, j);
                    } while (HorizontalRowExists(i, j, box) || VerticalRowExists(i, j, box));

                    box[i, j] = Instantiate(box[i, j]);
                }
            }
        }
    
        private (int, int) FindGem(Object gem)
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (box[i, j] == gem)
                    {
                        return (i, j);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        public bool GemsAreNeighbours(Object gem1, Object  gem2)
        {
            var pos1 = FindGem(gem1);
            var pos2 = FindGem(gem2);
        
            return pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1 ||
                   pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1;
        }

        public static bool HorizontalRowExists(int i, int j, Gem[,] box)
        {
            return j < box.GetLength(1) - 2 &&
                   box[i, j].Type == box[i, j + 1].Type &&
                   box[i, j].Type == box[i, j + 2].Type;
        }

        public static bool VerticalRowExists(int i, int j, Gem[,] box)
        {
            return i < box.GetLength(0) - 2 &&
                   box[i, j].Type == box[i + 1, j].Type &&
                   box[i, j].Type == box[i + 2, j].Type;
        }
    }
}