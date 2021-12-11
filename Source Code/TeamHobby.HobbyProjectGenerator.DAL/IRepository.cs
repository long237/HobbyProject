﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.DAL
{
    public interface IRepository<T>
    {
        bool Create(T model);

        T Read();

        bool Update(T model);

        bool Delete(T model);
    }
}
