﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExceptionSchema.Entities.Inventory;
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
