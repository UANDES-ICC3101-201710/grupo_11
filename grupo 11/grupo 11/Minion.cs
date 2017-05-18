using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Minion : Card
    {
        public int MaxLifePoints, ActualLifePoints, AttackPoints;
        public Boolean AttackEnable;
        private string Effect, Status;
        // Metodos get y set
        public void setAttackEnable(Boolean tf) { this.AttackEnable = tf; }
        // Otros Metodos
        public Minion(int MaxLifePoints, int ActualLifePoints, int AttackPoints
            ,Boolean AttackEnable, string Effect, string Status, int CardCost
            , string CardName, string CardRarity, string CardClass, int CardGamePosition) 
            : base(CardCost, CardName, CardRarity,CardClass,CardGamePosition)
        {
            this.MaxLifePoints = MaxLifePoints;
            this.ActualLifePoints = ActualLifePoints;
            this.AttackPoints = AttackPoints;
            this.AttackEnable = AttackEnable;
            this.Effect = Effect;
            this.Status = Status;

        }
        public void ActivateEffect(string effect)
        {

        }
        public void MinionRecibeDamage(int damageRecibe)
        {
            int damageTaken = this.ActualLifePoints - damageRecibe;
            if (damageTaken <= 0)
            {
                this.ActualLifePoints = 0;
            }
            else
            {
                this.ActualLifePoints = damageTaken;
            }
        }
        public override void IAttack(Minion EnemyMinion)
        {
            if (this.AttackEnable)
            {
                // Mostramos al usuario quien ataca a quien:
                Console.WriteLine(" '{0}'[A:{1},L:{2}] a atacado a '{3}'[A:{4},L:{5}]",
                        this.getCardName(), this.AttackPoints, this.ActualLifePoints,
                        EnemyMinion.getCardName(), EnemyMinion.AttackPoints, EnemyMinion.ActualLifePoints);

                // Nuestro minion recibe el daño proporcionado por el enemigo.
                this.MinionRecibeDamage(EnemyMinion.AttackPoints);
                // El minion enemigo recibe el daño proporcionado por el nuestro.
                EnemyMinion.MinionRecibeDamage(this.AttackPoints);

                // Mostramos al usuario el resultado del ataque:
                Console.WriteLine("Resultado");
                Console.WriteLine(" '{0}'[A:{1},L:{2}] ; '{3}'[A:{4},L:{5}]",
                        this.getCardName(), this.AttackPoints, this.ActualLifePoints,
                        EnemyMinion.getCardName(), EnemyMinion.AttackPoints, EnemyMinion.ActualLifePoints);

                this.AttackEnable = false;
            }
            else
            {
                Console.WriteLine("Este Minion No Puede Atacar");
                Console.ReadLine();
            }
        }
        public override void IAttackPlayer(Player enemyPlayer)
        {
            if (this.AttackEnable)
            {
                // Mostramos al usuario quien ataca a quien:
                Console.WriteLine(" '{0}'[A:{1},L:{2}] a atacado a '{3}'[A:{4},D:{5},L:{6}]",
                    this.getCardName(), this.AttackPoints, this.ActualLifePoints,
                    enemyPlayer.PlayerID, enemyPlayer.HeroAttackPoints, enemyPlayer.DefensePoints,
                    enemyPlayer.ActualLifePoints);

                // El jugador atacado recibe el daño.
                enemyPlayer.RecibeDamage(this.AttackPoints);
                // El minion que ataca tambien recibe daño
                this.MinionRecibeDamage(enemyPlayer.HeroAttackPoints);


                // Mostramos al usuario el resultado del ataque:
                Console.WriteLine("Resultado");
                Console.WriteLine(" '{0}'[A:{1},L:{2}] ; '{3}'[A:{4},D:{5},L:{6}]",
                        this.getCardName(), this.AttackPoints, this.ActualLifePoints,
                        enemyPlayer.PlayerID, enemyPlayer.HeroAttackPoints, enemyPlayer.DefensePoints,
                        enemyPlayer.ActualLifePoints);

                this.AttackEnable = false;
            }
            else
            {
                Console.WriteLine("Este Minion No Puede Atacar");
                Console.ReadLine();
            }
        }

    }
}
