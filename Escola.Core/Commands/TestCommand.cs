using Escola.Core.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Core.Commands
{
    [Route("/test")]
    public class TestCommand : IReturn<TestResponse>
    {
    }
}
