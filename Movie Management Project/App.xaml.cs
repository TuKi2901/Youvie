﻿using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.Content.User;
using Movie_Management_Project.Content.Admin;

namespace Movie_Management_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        
    }
}