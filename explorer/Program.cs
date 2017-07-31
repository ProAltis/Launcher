using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace explorer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Type t = Type.GetTypeFromProgID("Shell.Application");

            dynamic shell = Activator.CreateInstance(t);

            var objFolder = shell.NameSpace(@"shell:::{4234d49b-0245-4df3-b780-3893943456e1}");
            foreach (var item in objFolder.Items())
            {
                if (item.Name != "Project Altis")
                    continue;
                foreach (var verb in item.Verbs())
                {
                    if(verb.Name == "Pin to tas&kbar")
                        verb.DoIt();
                }
            }
            Environment.Exit(0);
        }
    }
}
