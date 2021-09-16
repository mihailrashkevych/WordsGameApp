using GameApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Service
{
    internal class AutorizeService
    {
        readonly Repository<User> userRepo;
        public AutorizeService()
        {
            userRepo = new Repository<User>();
        }

        public User Login()
        {
            Console.WriteLine();
            do
            {
                Console.WriteLine("Enter your username:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                var entity = userRepo.Get(u => u.Name == name && u.Password == password);
                if (entity.Any())
                {
                    Console.Clear();
                    return entity.First();
                }
                else
                {
                    _ = new ErrorMessage("--Message: Wrong Login or Password");
                }
            } while (true);
        }

        public async Task<User> Register()
        {
            User newUser = new();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to registration form!");
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter your Name (max 20 characters):");
                string name = Console.ReadLine();
                if (name.Length >= 20)
                {
                    _ = new ErrorMessage("--Message: Name must be less then 20 characters");
                    continue;
                }
                Console.WriteLine("Enter your UserName (max 20 characters):");
                string userName = Console.ReadLine();
                if (userName.Length >= 20 || userName.Trim().Length == 0)
                {
                    _ = new ErrorMessage("--Message: UserName must be less then 20 characters and not empty");
                    continue;
                }
                Console.WriteLine("Enter your password (max 20 characters):");
                string password = Console.ReadLine();
                if (password.Length >= 50 || password.Trim().Length == 0)
                {
                    _ = new ErrorMessage("--Message: Password must be less then 20 characters and not empty");
                    continue;
                }
                Console.WriteLine("Enter your phone (max 15 characters):");
                string phone = Console.ReadLine();
                if (phone.Length >15)
                {
                    _ = new ErrorMessage("--Message: Phone must be 15 characters");
                    continue;
                }
                newUser.Name = name;
                newUser.UserName = userName;
                newUser.Password = password;
                newUser.Phone = phone;
                break;
            } while (true);
            Repository<WordsForUser> wordsrepo = new Repository<WordsForUser>();
            var dbUser = await userRepo.Create(newUser);
            await wordsrepo.Create(new WordsForUser() { UserPhone = dbUser.Phone, Word = "SPEED"});
            await wordsrepo.Create(new WordsForUser() { UserPhone = dbUser.Phone, Word = "HOUSE"});
            return dbUser;
        }

        public async Task<User> PrintLoginMenu()
        {
            Console.WriteLine("!!!Game!!!");
            Console.WriteLine();
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Register");
            Console.WriteLine();
            do
            {
                Console.WriteLine("Enter number of operation");
                _ = int.TryParse(Console.ReadLine(), out int input);

                if (input == 1)
                {
                    return Login();
                }
                else if (input == 2)
                {
                    User newUser = await Register();
                    if (newUser != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Success!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                    else
                    {
                        _ = new ErrorMessage("Fail!");
                        Console.WriteLine();
                    }
                }
                else if (input != 1 || input != 2)
                {
                    _ = new ErrorMessage("--Message: Wrong choise. Try again!");
                }
            } while (true);
        }
    }
}
