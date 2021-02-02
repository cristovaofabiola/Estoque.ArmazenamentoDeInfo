using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabiola.VersaoBD
{
    class Cliente: DadosBase
    {
        
        public string nome { get; set; }
        public string cpf { get; set; }
        public int idade { get; set; }
        public double saldo { get; set; }
        
        public override void MostraDados()
        {
            Console.WriteLine("{0}{1}{2}{3}", nome, cpf, idade, saldo);
        }
        public override void DefineDados()
        {
            nome = Gerador.NomePessoa();
            cpf = Gerador.Cpf();
            idade = Gerador.Idade();
            saldo = Gerador.Saldo();

        }
    }
}
