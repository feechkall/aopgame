using System;
using System.Collections.Generic;

abstract class Creature
{
    //приватные поля 
    private string name;
    private int health, mindamage, maxdamage, defense;

    //публичные свойства для доступа к полям
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public int MinDamage
    {
        get { return mindamage; }
        set { mindamage = value; }
    }
    public int MaxDamage
    {
        get { return maxdamage; }
        set { maxdamage = value; }
    }
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    //свойство проверки здоровья
    public bool IsAlive => Health > 0;

    //базовый конструктор
    public Creature()
    {
        name = "Сущность";
        health = 10;
        mindamage = 2;
        maxdamage = 5;
        defense = 2;
    }

    //конструктор 
    public Creature(string name, int health, int minDamage, int maxDamage, int defense)
    {
        Name = name;
        Health = health;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        Defense = defense;
    }

    //метод движения
    public virtual void Move()
    {
        Console.WriteLine($"{Name} бежит!");
    }

    //метод атаки 
    public void Attack(Creature target)
    {
        Random random = new Random();
        int damage = random.Next(MinDamage, MaxDamage + 1);
        if (damage > target.Defense)
        {
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
            Console.WriteLine($"{Name} атакует и наносит {damage} урона. {target.Name}: {target.Health} здоровья.");
        }
        else
        {
            Console.WriteLine($"Защита спасла {target.Name} от урона");
        }


        if (!target.IsAlive)
        {
            Console.WriteLine($"{target.Name} погиб.");
        }
    }
}

class Knight : Creature
{
    public Knight(string name) : base(name, 100, 10, 20, 5)
    {
    }

    //наследование метода движения
    public override void Move()
    {
        Console.WriteLine($"{Name} едет верхом на коне!");
    }
}

class Goblin : Creature
{
    public Goblin(string name) : base(name, 50, 5, 10, 2)
    {
    }

    //наследование метода движения
    public override void Move()
    {
        Console.WriteLine($"{Name} идет!");
    }
}

class Orc : Creature
{
    public Orc(string name) : base(name, 80, 8, 15, 3)
    {
    }

    //наследование метода движения
    public override void Move()
    {
        Console.WriteLine($"{Name} идет!");
    }
}

class CentaurArcher : Creature
{
    public CentaurArcher(string name) : base(name, 70, 7, 14, 4)
    {
    }

    //наследование метода движения
    public override void Move()
    {
        Console.WriteLine($"{Name} бежит!");
    }
}

class Dragon : Creature
{
    public Dragon(string name) : base(name, 150, 15, 25, 6)
    {
    }

    //наследование метода движения
    public override void Move()
    {
        Console.WriteLine($"{Name} летит!");
    }
}

class Game
{
    private Knight knight;
    private Creature[] enemies;

    //создание игры
    public Game()
    {
        knight = new Knight("Рыцарь");
        enemies = new Creature[]
        {
            new Goblin("Гоблин"),
            new Orc("Орк"),
            new CentaurArcher("Кентавр-лучник"),
            new Dragon("Дракон")
        };
    }

    //запуск игры
    public void Start()
    {
        Console.WriteLine("Игра началась!");

        foreach (var enemy in enemies)
        {
            enemy.Move();
            while (knight.IsAlive && enemy.IsAlive)
            {
                knight.Attack(enemy);
                System.Threading.Thread.Sleep(5);
                if (enemy.IsAlive)
                {
                    enemy.Attack(knight);
                    System.Threading.Thread.Sleep(5);
                }
            }

            if (!knight.IsAlive)
            {
                Console.WriteLine("Рыцарь погиб. Игра окончена.");
                return;
            }
        }

        Console.WriteLine("Рыцарь победил всех противников!");
    }
}

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Start();
        Console.ReadLine();
    }
}
