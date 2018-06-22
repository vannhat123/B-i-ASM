using System;
using SpringHeroBank.controller;
using SpringHeroBank.entity;
using SpringHeroBank.utility;

namespace SpringHeroBank.view
{
    public class MainView
    {
        private static AccountController controller = new AccountController();
        private static TransactionController tcontroller = new TransactionController();

        public static void GenerateMenu()
        {
            while (true)
            {
                // nếu chưa đăng nhập thì đăgn nhập vào sau đó chạy while đăng nhập rồi thì chạy hàm dưới.
                if (Program.currentLoggedIn == null)
                {
                    GenerateGeneralMenu();
                }
                else
                {
                    GenerateCustomerMenu();
                }
            }
        }

        private static void GenerateCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------SPRING HERO BANK---------");
                Console.WriteLine("Welcome back: " + Program.currentLoggedIn.FullName);
                Console.WriteLine("1. Balance.");
                Console.WriteLine("2. Withdraw.");
                Console.WriteLine("3. Deposit.");
                Console.WriteLine("4. Transfer.");
                Console.WriteLine("5. History.");
                Console.WriteLine("6. Exit.");
                ;
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Please enter your choice (1|2|3|4|5): ");
                var choice = Utility.GetInt32Number();
                switch (choice)
                {
                    case 1:
                        controller.CheckBalance();
                        break;
                    case 2:
                        controller.Withdraw();
                        break;
                    case 3:
                        controller.Deposit();
                        break;
                    case 4:
                        controller.Transfer();
                        break;
                    case 5:
                        GenerateHistoryMenu();
                        break;
                    case 6:
                        Console.WriteLine("See you later.");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static void GenerateGeneralMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------WELCOME TO SPRING HERO BANK---------");
                Console.WriteLine("1. Register for free.");
                Console.WriteLine("2. Login.");
                Console.WriteLine("3. Exit.");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Please enter your choice (1|2|3): ");
                var choice = Utility.GetInt32Number();
                switch (choice)
                {
                    case 1:
                        controller.Register();
                        break;
                    case 2:
                        if (controller.DoLogin())
                        {
                            Console.WriteLine("Login success.");
                        }

                        break;
                    case 3:
                        Console.WriteLine("See you later.");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (Program.currentLoggedIn != null)
                {
                    break;
                }
            }
        }
        // viết ra lịch sử menu
        private static void GenerateHistoryMenu()
        {
            Console.Clear();
            Console.WriteLine("---------Transaction history---------");
            Console.WriteLine("1. All Transaction History.");
            Console.WriteLine("2. Search transaction by date");
            Console.WriteLine("3. Search transaction in 10 days");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Please enter your choice (1|2|3): ");
            var choice = Utility.GetInt32Number();
            switch (choice)
            {
                case 1:
                    tcontroller.GetListTransaction();
                    AskForPrint();
                    break;
                case 2:
                    tcontroller.GetListTransactionByDate();
                    AskForPrint();
                    break;
                case 3:
                    tcontroller.GetListTransactionIn10Days();
                    AskForPrint();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        
        // In ra lựa chọn có hay không khi y hoặc n để in
        private static void AskForPrint()
        {
            Console.WriteLine("Do you want to print it ?(Y/N)");
            var choice = Console.ReadLine();
            if (choice == "y" || choice == "Y")
            {
                tcontroller.PrintTransaction();
                Console.WriteLine("Print Succes");
            }
            else if (choice == "N" || choice == "n")
            {
                Console.WriteLine("You choose No");
            }
            else Console.WriteLine("Invalid choice");
        }
    }
}