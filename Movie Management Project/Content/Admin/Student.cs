using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Management_Project.Content.Admin
{
    public class MainPageViewModel : BindableObject
    {
        DTO_Users users = new DTO_Users();
        static Random random = new();
        public ObservableCollection<Student> Items { get; } = new();
        public ObservableCollection<Student> Items1 { get; } = new();
        public ObservableCollection<Student> Items2 { get; } = new();
        public ObservableCollection<Student> Items4 { get; } = new();
        

        public MainPageViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                Items.Add(new Student { Id = i, Name = "Person " + i, Age = random.Next(14, 85), });
            }
            Items1.Add(new Student { Id=10, Name="Person", Age=232 });
            Items2.Add(new Student { Id=10, Name="Person", Age=232 });
            Items2.Add(new Student { Id=10, Name="Person", Age=232 });
            Items4.Add(new Student { Id=10, Name="Person", Age=232 });
            Items4.Add(new Student { Id=10, Name="Person", Age=232 });
            Items4.Add(new Student { Id=10, Name="Person", Age=232 });
            Items4.Add(new Student { Id=10, Name="Person", Age=232 });
            Items4.Add(new Student { Id=10, Name="Person", Age=232 });
            Items.Add(new Student { Id = 1, Name = "person", Age = 30 });
        }

        public class Student
        {
            [DisplayName("Identity Number")]
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }


    }
}
