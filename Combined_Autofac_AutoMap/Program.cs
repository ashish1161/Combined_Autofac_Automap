using System;
using AutoMapper;
using Autofac;

namespace Combined_Autofac_AutoMap
{
    class StudentDetails
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }
        public DateTime RegisterDateTime { get; set; }
    }
    class Student1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }
        public int id { get; set; }

    }
    class Client : IClient
    {

        public void Execute()
        {
            var studentDetails = new StudentDetails()
            {
                id = 111,
                FirstName = "Ravi",
                LastName = "Batra",
                age = 28,
                RegisterDateTime = DateTime.Now

            };
            var Config = new MapperConfiguration(cfg => cfg.CreateMap<StudentDetails, Student1>());
            var mapper = Config.CreateMapper();

            var student1 = mapper.Map<StudentDetails, Student1>(studentDetails);
            Console.WriteLine($"ID - {student1.id}");
            Console.Write(student1.FirstName);
            Console.Write(" ");
            Console.Write(student1.LastName);
            Console.WriteLine($" Age is {student1.age}");
            Console.Write($"Registered Time is {studentDetails.RegisterDateTime}");  
        }
    }
    public class Application
    {
        IClient _client;
       public Application(IClient client)
        {
            _client = client;
        }
        public void Run()
        {
            _client.Execute();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           
            var builder = new ContainerBuilder();
            builder.RegisterType<Client>();
            var container = builder.Build();

            Client client = container.Resolve<Client>();
            Application app = new Application(client);       
            app.Run();
            
        }
    }
}
