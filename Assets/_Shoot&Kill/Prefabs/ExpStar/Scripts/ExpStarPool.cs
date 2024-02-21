using Assets.Architecture.Scripts.PoolObject;

namespace Assets.Prefabs.ExpStar.Scripts
{
    
    public class ExpStarPool : Pool<ExpStar>
    {
        public static ExpStarPool Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}