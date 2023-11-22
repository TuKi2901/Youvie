using BUS;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Management_Project.Content.Admin
{
    public class MainPageViewModel : BindableObject
    {
        BUS_Project1 bus_project1 = new BUS_Project1();

        static Random random = new();
        public ObservableCollection<Student> Items { get; } = new();
        public ObservableCollection<DTO_Users> Items1 { get; } = new();
        public IEnumerable<Student> Items2 { get; }
        public Student st;

        public MainPageViewModel()
        {
            haha();
        }

        public async void haha()
        {
            List<DTO_Users> users = await bus_project1.BusGetUser();

            //DTO_Users dTO_Users = users.Rows;
            foreach (DTO_Users u in users)
            {
                Items1.Add(u);
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
