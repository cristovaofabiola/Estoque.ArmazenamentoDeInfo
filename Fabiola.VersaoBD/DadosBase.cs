using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabiola.VersaoBD
{
    class DadosBase
    {
        public string cnpj { get; set; }
        public string nome { get; set; }
        public string CPF { get; set; }
        public int idade { get; set; }
        public double saldo { get; set; }
        

        public virtual void MostraDados()
        {
            Console.WriteLine("{ 0}{1 }{2 }", nome, CPF, idade);
        }
        public virtual void DefineDados()
        {
            nome = Gerador.NomePessoa();
            CPF = Gerador.Cpf();
            idade = Gerador.Idade();
            saldo = Gerador.Saldo();
            
        }
        public static string Cnpj()
        {
            Random ran = new Random();
            string cnpj = "";
            for (int i = 1; i < 15; i++)
            {
                if (i % 4 == 0)
                {
                    if (i == 12)
                    {
                        cnpj += "-";
                    }
                    else
                    {
                        cnpj += ".";
                    }
                }
                else
                {
                    cnpj += ran.Next(0, 10);
                }
            }
            return cnpj;
        }
    }
}
