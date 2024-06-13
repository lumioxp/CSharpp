using System;
using System.Collections.Generic;

namespace ConsoleRPG
{
    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Level { get; set; }
        public int Mana { get; set; }

        public Character(string name, int health, int attackpower, int level, int mana)
        {
            Name = name;
            Health = health;
            AttackPower = attackpower;
            Level = level;
            Mana = mana;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} otrzymał {damage} obrażeń");
        }
        //enemy.TakeDamage(player.Attack());
        public int Attack()
        {
            Console.WriteLine($"{Name} atakuje i zadaje {AttackPower} obrażeń");
            return AttackPower;
        }

        public void NextLvl(int lev) //jak będzie mechanika lvl
        {
            Level += lev;
            Console.WriteLine($"Gratulacje! Zdobyłeś kolejny poziom {Level}");
        }

        public void EnemyDeath(string name, int lev)
        {
            Level += lev;
            Console.WriteLine($"Gratulacje! Zabiłeś {name}, twój poziom wzrósł do {Level}!");
        }

        public void UseSkill(Skill skill, Character target)
        {
            if (Mana >= skill.ManaCost)
            {
                Mana -= skill.ManaCost;
                target.TakeDamage(skill.Damage);
                Console.WriteLine($"{Name} używa {skill.Name} i zadaje {skill.Damage} obrażeń");
            }
            else
            {
                Console.WriteLine($"Nie masz wystarczająco many, aby użyć {skill.Name}");
            }
        }
    }

    class Player : Character
    {
        public List<Skill> Skills { get; set; }

        public Player(string name) : base(name, 100, 10, 1, 20)
        {
            Skills = new List<Skill>();
        }

        public void ChooseSkills(List<Skill> availableSkills)
        {
            Console.WriteLine("Wybierz 4 umiejętności:");
            for (int i = 0; i < availableSkills.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {availableSkills[i].Name} - {availableSkills[i].Description} (DMG: {availableSkills[i].Damage}, Mana: {availableSkills[i].ManaCost})");
            }

            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Wybierz umiejętność {i + 1}: ");
                int choice = int.Parse(Console.ReadLine());
                Skills.Add(availableSkills[choice - 1]);
            }
        }
    }

    class Enemy : Character
    {
        public Enemy(string name, int health, int attackPower, int level, int mana) : base(name, health, attackPower, level, mana)
        {
        }
    }

    class EnemyRepositor
    {
        private List<Enemy> enemies;

        public EnemyRepositor()
        {
            enemies = new List<Enemy>
            {
                new Enemy("Robak", 100, 5, 1, 0),
                new Enemy("Tutel", 1000, 50, 20, 0)
            };
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }

    class Text
    {
        public static bool IsTxt(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return false;
            }

            if (int.TryParse(playerName, out _))
            {
                return false;
            }

            return true;
        }
    }

    class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public int ManaCost { get; set; }

        public Skill(int skillId, string name, string description, int damage, int manaCost)
        {
            SkillId = skillId;
            Name = name;
            Description = description;
            Damage = damage;
            ManaCost = manaCost;
        }
    }

    class SkillRepositor
    {
        private List<Skill> skills;

        public SkillRepositor()
        {
            skills = new List<Skill>()
            {
                new Skill(1, "Broń", "Wcześniej wybrana broń", 0, 0), //przygotowane do modułu broni
                new Skill(2, "Uderzenie", "Zwykłe uderzenie z pięści", 5, 0),
                new Skill(3, "Kopniecie", "Zwykły kop", 3, 0),
                new Skill(4, "Podpalenie", "Niech przeciwnik stanie w ogniu", 20, 40),
                new Skill(5, "Pomijacz", "Pomiń turę, potem coś wymyślisz", 0, 0)
            };
        }

        public List<Skill> GetSkills()
        {
            return skills;
        }
    }

    class Weapon //moduł broni
    {
        
    }

    class WeaponRepositor //lista broni
    {
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            string playerName;
            do
            {
                Console.WriteLine("Witaj, jak się nazywasz?");
                playerName = Console.ReadLine()!;
            } while (!Text.IsTxt(playerName));

            Player player = new Player(playerName);
            EnemyRepositor enemyRepositor = new EnemyRepositor();
            SkillRepositor skillRepositor = new SkillRepositor();

            player.ChooseSkills(skillRepositor.GetSkills());

            List<Enemy> enemies = enemyRepositor.GetEnemies();

            Random random = new Random();
            int index = random.Next(enemies.Count);
            Enemy enemy = enemies[index];

            Console.WriteLine($"{playerName} posiada {player.Health} HP oraz {player.AttackPower} DMG");
            Console.WriteLine($"{enemy.Name} posiada {enemy.Health} HP oraz {enemy.AttackPower} DMG");

            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine("Twoja tura");
                Console.WriteLine("Wybierz umiejętność:");
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {player.Skills[i].Name} (DMG: {player.Skills[i].Damage}, Mana: {player.Skills[i].ManaCost})");
                }

                int skillChoice = int.Parse(Console.ReadLine());
                Skill chosenSkill = player.Skills[skillChoice - 1];
                player.UseSkill(chosenSkill, enemy);

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"Pokonałeś {enemy.Name}");
                    player.EnemyDeath(enemy.Name, enemy.Level);
                    break;
                }

                Console.WriteLine($"Tura {enemy.Name}");
                player.TakeDamage(enemy.Attack());

                if (player.Health <= 0)
                {
                    Console.WriteLine($"Zostałeś pokonany przez {enemy.Name}");
                    break;
                }
            }

            Console.WriteLine($"{playerName} posiada {player.Health} HP oraz {player.AttackPower} DMG oraz {player.Level} poziom");
            Console.WriteLine($"{enemy.Name} posiada {enemy.Health} HP oraz {enemy.AttackPower} DMG oraz {enemy.Level} poziom");

            Console.ReadKey();
        }
    }
}

