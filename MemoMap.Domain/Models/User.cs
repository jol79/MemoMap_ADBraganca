﻿using MemoMap.Domain.Models;
using MemoMap.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoMap.Domain
{
    public class User : Entity
    {
        public User()
        {
            GroupUsers = new List<GroupUser>();
        }
        /*
         username string 
         email string
         password string
         isAdmin boolean 
        */
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }

        // user can be part of many groups
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<UserMap> UserMaps { get; set; }
    }
}
