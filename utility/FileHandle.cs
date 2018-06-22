using System;
using System.Collections.Generic;
using System.IO;
using SpringHeroBank.entity;

namespace SpringHeroBank.utility
{
    public class FileHandle
    {
        private List<Transaction> listTransactions;
        private List<Account> listAccounts;
        
        public List<Transaction> ReadFileTrans()
        {
            var lines = File.ReadAllLines("NeverEverGetBackTogether.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                var lineSplited = lines[i].Split("|");
                if (lines.Length == 8)
                {
                    var transaction = new Transaction()
                    {
                        Id = lineSplited[0],
                        SenderAccountNumber = lineSplited[1],
                        ReceiverAccountNumber = lineSplited[2],
                        Type = (Transaction.TransactionType)Int32.Parse(lineSplited[3]),
                        Amount = Decimal.Parse(lineSplited[4]),
                        Content = lineSplited[5],
                        CreatedAt = lineSplited[6],
                        Status = (Transaction.ActiveStatus)Int32.Parse(lineSplited[7])
                    };
                    listTransactions.Add(transaction);
                }            
            }
            return listTransactions;
        }

        public List<Account> ReadFileAccounts()
        {
            var lines = File.ReadAllLines("ForgetMeNot.txt");
            
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                var lineSplited = lines[i].Split("|");
                if (lineSplited.Length == 6)
                {
                    var account = new Account()
                    {
                        AccountNumber = lineSplited[0],
                        Username = lineSplited[1],
                        FullName = lineSplited[2],
                        Balance = Int32.Parse(lineSplited[3]),
                        Salt = lineSplited[4],
                        Status = (Account.ActiveStatus)Int32.Parse(lineSplited[5])
                    };
                    listAccounts.Add(account);
                }
            }
            
            return listAccounts;
        }

        public void CheckAccount()
        {
            Dictionary<String, decimal> dictionary = new Dictionary<string, decimal>();
           
            foreach (var transaction in listTransactions)
            {
                if (dictionary.ContainsKey(transaction.SenderAccountNumber))
                {
                    dictionary[transaction.SenderAccountNumber] += transaction.Amount;
                }
                else
                {
                    dictionary.Add(transaction.SenderAccountNumber, transaction.Amount);
                }
            }

            foreach (var item in dictionary)
            {
                Console.WriteLine(item.Key+ "-"+ item.Value);
            }
        }

        public static void WriteTransactionToFile(string[] strings)
        {
            File.WriteAllLines(Program.currentLoggedIn.FullName +".txt", strings);
        }

    }
}