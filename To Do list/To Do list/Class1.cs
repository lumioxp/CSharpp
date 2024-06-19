using System;

namespace To_Do_list
{
    public class QuestsItems
    {
        private static int nextId = 1;
        public int Id { get; private set; }
        public string Quest { get; set; }

        public QuestsItems(string quest)
        {
            Id = nextId++;
            Quest = quest;
        }
    }

    public class QuestList
    {
        List<QuestsItems> quests = new List<QuestsItems>();

        public void addQuest(string quest)
        {
            quests.Add(new QuestsItems(quest));
            Console.WriteLine("Zadanie dodane.");         
        }

        public void displeyQuest()
        {
            foreach (var quest in quests)
            {
                Console.WriteLine($"{quest.Id}. {quest.Quest}");
            }
        }

        public void removeId(int id)
        {
            var quest = quests.Find(q => q.Id == id);
            if (quest != null)
            {
                quests.Remove(quest);
                Console.WriteLine("Rekord usunięty");
            }
            else
            {
                Console.WriteLine("Zadania nie znaleziono");
            }
        }
        
        
    }

    public class Program
    {
        static void Main(string[]args)
        {
            QuestList tasks = new QuestList();
            
            Console.WriteLine("Witaj w liście to do, co chcesz zrobić?");

            bool running = true;
            while(running)
            {
                Console.Clear();;
                Console.WriteLine();
                Console.WriteLine("1. Wyświetl listę");
                Console.WriteLine("2. Dodaj zadanie");
                Console.WriteLine("3. Oznacz jako wykonane");
                Console.WriteLine("4. Usun zadanie");
                Console.WriteLine("5. Zakończ");
                Console.WriteLine("Wybierz opcję");
                int choice = Convert.ToInt32(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Lista zadaniń do realizacji: ");
                        tasks.displeyQuest();
                        break;
                    
                    case 2:
                        Console.WriteLine("Napisz zadanie do zrealizowania.");
                        string quest = Console.ReadLine();
                        tasks.addQuest(quest);
                        break;
                    
                    case 3:
                        Console.WriteLine("Które zadanie wykonałeś?");
                        int completedId = Convert.ToInt32(Console.ReadLine());
                        //metoda wykonane
                        break;
                    
                    case 4:
                        Console.WriteLine("Które zadanie chcesz usunąć?");
                        int removeId = Convert.ToInt32(Console.ReadLine());
                        tasks.removeId(removeId);
                        break;
                    default:
                        Console.WriteLine("Nieprawdłowy wybór");
                        break;
                }
            }
        }

    }
}