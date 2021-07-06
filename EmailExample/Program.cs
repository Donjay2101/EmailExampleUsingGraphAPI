using System;

namespace EmailExample
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailService.EmailService emailService = new EmailService.EmailService();
            emailService.SendEmail().Wait();
            Console.WriteLine("Hello World!");
        }
    }
}
