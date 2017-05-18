using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Field
    {
        public int MaxMinions = 7;
        public int MaxSecrets = 5;
        public List<Minion> MinionList { get; set; }
        private List<Secret> SecretList;
        public Boolean MaxWeapons { get; set; }
        public int ActualMinions { get; set; }
        public int ActualSecrets { get; set; }
        public Field()
        {
            this.ActualMinions = 0;
            this.ActualSecrets = 0;
            this.MinionList = new List<Minion>() ;
            this.SecretList = new List<Secret>() ;
    }
        public void CheckMinionsLife()
        {
            foreach( Minion minion in this.MinionList)
            {
                int LifeOfMinion = minion.ActualLifePoints;
                // Verificamos si la vida del minion es cero
                if(LifeOfMinion <= 0)
                {
                    // Si la vida del minion es cero, lo eliminamos del juego
                    this.MinionList.Remove(minion);
                    // mostramos el resultado de esta acción al usuario
                    Console.WriteLine("Se ha destruido '{0}'", minion.getCardName()); Console.ReadLine();
                    // Se vuelve a llamar al método, de esta manera se actualiza el array...
                    // ... de los minions en el Field y se evita el problema de acceder...
                    // ... a un 'Out Of Range'.
                    this.CheckMinionsLife();
                    break;
                }
            }
        }
        public void InsertMinionToField(Minion minion)
        {
            //Pimero vemos si hay espacio para minions
            if(this.MaxMinions >= this.ActualMinions)
            {
                MinionList.Add(minion);
                this.ActualMinions++;
            }
        }
        public Boolean CheckIfSelectedMinionExist(int Index)
        {
            if(0 < Index  && Index <= this.MinionList.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Minion GetMinionFromField(int Index)
        {
            return this.MinionList[Index - 1];
        }
    }
}
