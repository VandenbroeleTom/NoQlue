using System;
using System.Collections.Generic;
using NoQlue.Model;

namespace NoQlue.Data
{
    public interface ClassRepository
    {
        List<Class> GetClassesByEmail(string email);
    }
}
