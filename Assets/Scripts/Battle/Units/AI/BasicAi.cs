using System.Collections;

namespace Battle.Units.AI
{
    public class BasicAi : Ai
    {
        private void Attack()
        {
            attachedEnemy.target.hp.TakeDamage(attachedEnemy.damage.ApplyDamage(1));
        }

        public override IEnumerator Act()
        {
            Attack();
            yield break;
        }
    }
}