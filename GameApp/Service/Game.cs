using ConsoleTables;
using GameApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Service
{
    public class Game
    {
        User user;
        public async void PrintMenu()
        {
            AutorizeService autorizeService = new AutorizeService();
            user =  await autorizeService.PrintLoginMenu();
            bool flag = true;
            do
            {
                Console.WriteLine("Choose an option");
                Console.WriteLine("1) Words ready for you to play");
                Console.WriteLine("2) Assign word for another player");
                Console.WriteLine("3) See score board");
                Console.WriteLine("0) Exit");
                Console.WriteLine("Enter your choice");

                if (!int.TryParse(Console.ReadLine(), out int choise))
                {
                    new ErrorMessage("-- Wrong Input! Type a number!--");
                }
                else
                {
                    switch (choise)
                    {
                        case 1:
                            Play(user);
                            break;
                        case 2:
                            SetWord();
                            break;
                        case 3:
                            GetScoreTable();
                            break;
                        case 0:
                            flag = false;
                            break;
                        default:
                            new ErrorMessage("-- Wrong Input! Try again!--");
                            break;
                    }
                }
            } while (flag);
        }

        private void GetScoreTable()
        {
            Console.WriteLine("Score Board");
            var users =(List<User>) new Repository<User>().Get();
            users.Sort();
            var table = new ConsoleTable("Name", "Score");
            table.Options.EnableCount = false;

            foreach (var item in users)
            {
                table.AddRow(item.UserName, item.Score);
            }
            table.Write();
        }

        private async void SetWord()
        {
            Console.WriteLine("Enter the word");
            var word = Console.ReadLine().ToUpper();

            Console.WriteLine("The available users are given bellow");
            var users = (List<User>) new Repository<User>().Get();
            var toRemove = users.Find(u=>u.Phone == user.Phone);
            users.Remove(toRemove);
            int i = 1;
            foreach (var item in users)
            {
                    Console.WriteLine($"{i}) {item.UserName}");
                    i++;
            }

            Console.WriteLine("Select the user");
            do
            {
                if (!int.TryParse(Console.ReadLine(), out int choise))
                {
                    new ErrorMessage("-- Wrong Input! Type a number!--");
                }
                else if (choise < 0 || choise > users.Count())
                {
                    new ErrorMessage("-- Wrong Input! Word not exist!--");
                }
                else
                {
                    await new Repository<WordsForUser>().Create(new WordsForUser() { UserPhone = users[choise-1].Phone, Word = word});
                    break;
                }
            } while (true);

        }

        private void Play(User user)
        {
            PrintPlayMenu(user);
            GetChoiseAndPlay(user);
        }

        private void GetChoiseAndPlay(User user)
        {
            var words = (List<WordsForUser>)new Repository<WordsForUser>().Get(w => w.UserPhone == user.Phone);
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter your choice");
                if (!int.TryParse(Console.ReadLine(), out int choise))
                {
                    new ErrorMessage("-- Wrong Input! Type a number!--");
                }
                else if (choise < 0 || choise > words.Count)
                {
                    new ErrorMessage("-- Wrong Input! Word not exist!--");
                }
                else
                {
                    GuessLogic(words.ElementAt(choise - 1).Word);
                    user.Score += 10;
                    new Repository<User>().Update(user);
                    break;
                }
            } while (true);
        }

        private void GuessLogic(string word)
        {
            bool result = false;
            do
            {
                Console.WriteLine("Enter your Guess");
                var guess = Console.ReadLine();
                if (guess.Length == word.Length)
                {
                    result = GetResult(guess.ToUpper(), word);
                }
                else
                {
                    new ErrorMessage("Lenghth not are the same. Try again!");
                }
            } while (!result);
        }

        private bool GetResult(string guess, string word)
        {
            int cow = 0;
            int bull = 0;
            char[] set = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (i != j && word[i] == guess[j])
                    {
                        set[i] = word[i];
                        bull++;
                    }
                    else if (i == j && word[i] == guess[j] )
                    {
                        cow++;
                    }
                }
            }
            Console.WriteLine($"The word you entered is {guess} - Cow - {cow} , Bull - {bull}");
            if (cow == word.Length)
            {
                Console.WriteLine("That is the word. Congrats!!!!!");
                new Repository<WordsForUser>().Remove(new Repository<WordsForUser>().Get().Where(w=>w.UserPhone == user.Phone && w.Word == word).First());
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            else 
                return false;
        }

        private void PrintPlayMenu(User user)
        {
            Console.WriteLine("The words given to you are");
            var repo = new Repository<WordsForUser>();
            var words = repo.Get(w => w.UserPhone == user.Phone);
            int i = 1;
            foreach (var item in words)
            {
                string hidenWord = string.Empty;
                for (int j = 0; j < item.Word.Length; j++)
                {
                    hidenWord += "X";
                }
                Console.WriteLine($"{i}) {hidenWord}");
                i++;
            }
        }
    }
}
