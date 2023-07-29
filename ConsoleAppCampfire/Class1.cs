using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCampfire
{

    public class Unit: IDamageable
    {
        private const float BuffAttackMultiplier = 1.5f;
        private const float DebuffAttackMultiplier = 0.5f;
        private const float DefaultAttackMultiplier = 1.0f;
        private const int ArmorPersent = 100;
        private const float BerserkDefenseMultiplier = 0.2f;
        private const float BerserkAttackMultiplier = 2.0f;
        private const int CriticalHealth = 30;

        public float Health { get; private set; }
        public Faction Faction { get; }
        public string Name { get; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public bool BerserkMode { get; private set; }
        public bool InRage { get; private set; }
       

        public Unit(string name, int attack, int defense, bool berserkMode, Faction faction)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            BerserkMode = berserkMode;
            Faction = faction;
            Health = 100;
        }

        public void TakeDamage(Unit enemy)
        {
            float damage = CalculateIncomingDamage(AttackCalculate(enemy.Attack, enemy.InRage), enemy.Faction);
            Health -= damage;
            Console.WriteLine($"{enemy.Name} наносит {damage} урона!");
            if (BerserkMode && Health <= CriticalHealth)
            {
                RageActivate();
            }
        }
        public bool IsDead()
        {
            return Health <= 0;
        }
        
        private void RageActivate()
        {
            InRage = true;
            Console.WriteLine($"{Name} переходит во состояние ЯРОСТИ!!!");
            BerserkMode = false;
        }

        private float CalculateIncomingDamage(float damage, Faction enemyFaction)
        {
            return (damage * GetMultiplier(Faction, enemyFaction)) * ((ArmorPersent - DefenseCalculate()) / ArmorPersent);
        }
        
        private float GetMultiplier(Faction defender, Faction attacker)
        {
            if (defender == Faction.Neutral || attacker == Faction.Neutral)
            {
                return DefaultAttackMultiplier;
            }
            if (defender == attacker)
            {
                return DebuffAttackMultiplier;
            }
            return BuffAttackMultiplier;
        }

        private float DefenseCalculate()
        {
            if (InRage)
                return Defense * BerserkDefenseMultiplier;
            else return Defense;
        }

        private float AttackCalculate(float damage, bool inRage)
        {
            if (inRage)
                return damage * BerserkAttackMultiplier;
            else return damage;
        }
    }
}
