using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Grid = Battle.Match3.Grid;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;

        private Grid _grid;

        public new void TurnOn()
        {
            base.TurnOn();
            _grid = FindFirstObjectByType<Grid>();
        }

        private int CountMana()
        {
            return _grid.destroyed.ContainsKey(GemType.Mana) ? _grid.destroyed[GemType.Mana] * manaPerGem : 0;
        }

        private Damage CountDamage()
        {
            Damage dmg =  new Damage(
                (int) damage[DmgType.Fire].GetValue() * _grid.destroyed[GemType.Red],
                (int) damage[DmgType.Cold].GetValue() * _grid.destroyed[GemType.Blue],
                (int) damage[DmgType.Poison].GetValue() * _grid.destroyed[GemType.Green],
                (int) damage[DmgType.Light].GetValue() * _grid.destroyed[GemType.Yellow],
                (int) damage[DmgType.Physic].GetValue() * _grid.destroyed.Sum(gems => gems.Key != GemType.Mana ? gems.Value : 0)
            );
            return dmg;
        }

        public override void Act()
        {
            mana += CountMana();
            Damage doneDamage = CountDamage();
        
            _grid.ClearDestroyed();

            if (doneDamage.IsZero()) return;
        
            PToEDamageLog.Log(manager.target, this, doneDamage);
            manager.target.DoDamage(doneDamage);
        }

        public override void DoDamage(Damage dmg)
        {
            base.DoDamage(dmg);
        
            AudioManager.instance.Play(AudioEnum.PlayerHit);
        }

        protected override void NoHp()
        {
            DeathLog.Log(this);
            StartCoroutine(manager.Die());
        }

        public void Load()
        {
            manaPerGem = data.manaPerGem;
            hp = data.hp;
            mana = data.mana;
            fDmg = data.fDmg;
            cDmg = data.cDmg;
            pDmg = data.pDmg;
            lDmg = data.lDmg;
            phDmg = data.phDmg;
            items = new List<Item>(data.items);
            spells = new List<Spell>(data.spells);
        }

        public void Save()
        {
            data.manaPerGem = manaPerGem;
            data.hp = hp;
            data.mana = mana;
            data.fDmg = fDmg;
            data.cDmg = cDmg;
            data.pDmg = pDmg;
            data.lDmg = lDmg;
            data.phDmg = phDmg;
        }
    }
}