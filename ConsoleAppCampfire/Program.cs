using ConsoleAppCampfire;

Unit Unit1 = new Unit("Hanry", 20, 80, false, Faction.Neutral);
Unit Unit2 = new Unit("Michael", 20, 80, false, Faction.Neutral);

while (!Unit1.IsDead() && !Unit2.IsDead())
{
    Unit1.TakeDamage(Unit2);
    Console.WriteLine($"{Unit2.Name} нанес урон {Unit1.Name}. У {Unit1.Name} осталось {Unit1.Health.ToString("00")} HP!");
    if (Unit1.IsDead()) break;
    Unit2.TakeDamage(Unit1);
    Console.WriteLine($"{Unit1.Name} нанес урон {Unit2.Name}. У {Unit2.Name} осталось {Unit2.Health.ToString("00")} HP!");
}

if (Unit1.Health > 0)
        Console.WriteLine($"{Unit1.Name} победил!!");
else    Console.WriteLine($"{Unit2.Name} победил!!");

