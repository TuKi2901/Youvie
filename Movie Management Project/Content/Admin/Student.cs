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
        static Random random = new();
        public ObservableCollection<Student> Items { get; } = new();
        public ObservableCollection<Student> Items1 { get; } = new();
        public IEnumerable<Student> Items2 { get; }
        public Student st;

        public MainPageViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                Items.Add(new Student { Id = i, Name = "Person " + i, Age = random.Next(14, 85), });
            }
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
