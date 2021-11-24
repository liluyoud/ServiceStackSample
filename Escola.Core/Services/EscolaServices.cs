using Escola.Core.Commands;
using Escola.Core.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Core.Services
{
    public class EscolaServices : Service
    {
        public object Any(TestCommand command)
        {
            return new TestResponse { Text = "Serviço da Escola OK" };
        }
    }
}
