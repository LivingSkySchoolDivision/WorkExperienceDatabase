﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public static class Settings
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WorkExperienceDatabase"].ConnectionString;
            }
        }

    }
}