using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

        private List<Unit> _UnitList = new List<Unit>();
        private List<Unit> _FriendlyUnitList = new List<Unit>();
        private List<Unit> _EnemyUnitList = new List<Unit>();


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            Unit.OnAnyUnitSpawned += UnitSpawned;
            Unit.OnAnyUnitDied += UnitDead;
        }


        private void UnitSpawned(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            _UnitList.Add(unit);

            if(unit.IsEnemy())
            {
                _EnemyUnitList.Add(unit);
            }
            else
            {
                _FriendlyUnitList.Add(unit);
            }
        }



        private void UnitDead(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            _UnitList.Remove(unit);

            if (unit.IsEnemy())
            {
                _EnemyUnitList.Remove(unit);
            }
            else
            {
                _FriendlyUnitList.Remove(unit);
            }
        }




        public List<Unit> GetUnitList()
        {
            return _UnitList;
        }

        public List<Unit> GetFriendlyUnitList()
        {
            return _FriendlyUnitList;
        }

        public List<Unit> GetEnemyUnitList()
        {
            return _EnemyUnitList;
        }


    }
}

