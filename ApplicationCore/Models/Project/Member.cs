﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Project
{
    public class Member
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public Member(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public Member() { 
        }

    }
}
