using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabiola.VersaoBD
{
    class Socio: DadosBase
    {
        public double QtdeAcoes { get; set; }
        public override void MostraDados()
        {
            Console.WriteLine();
        }
        public override void DefineDados()
        {
            nome = Gerador.NomePessoa();
            CPF = Gerador.Cpf();
            idade = Gerador.Idade();
            saldo = Gerador.Saldo();
            saldo = saldo * 0.8;
            
        }

        public  void MostraAcoes()
        {
            Console.WriteLine("{0}", QtdeAcoes);
        }
        public double  DefineAcoes()
        {
            Random ran = new Random();
            QtdeAcoes = ran.Next(0, 4) + ran.NextDouble();
            if (QtdeAcoes > 4.95)
            {
                QtdeAcoes = ran.Next(0, 4) + ran.NextDouble();
            }
            return QtdeAcoes;
        }
    }
}
