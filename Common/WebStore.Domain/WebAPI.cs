﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain
{
    public class WebAPI
    {
        public const string Values = "api/v1/values";
        public const string Employees = "api/v1/employees";
        public const string MyTestClient = "api/v1/mytest";
        public const string Products = "api/v1/products";
        public const string Orders = "api/v1/orders";
        public static class Identity
        {
            public const string Users = "api/v1/users";
            public const string Roles = "api/v1/roles";
        }
    }
}
