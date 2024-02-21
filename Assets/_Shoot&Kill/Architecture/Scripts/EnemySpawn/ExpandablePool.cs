using System;
using UnityEngine;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class ExpandablePool<T> : SimplePool<T>
    {
        protected ExpandablePool(Action<T> getAction, Action<T> returnAction, Func<T> createAction, int capacity)
            : base(getAction, returnAction, createAction, capacity) {
        }

        protected override T EmptyPool() {
            return _createAction();
        }
    }
}