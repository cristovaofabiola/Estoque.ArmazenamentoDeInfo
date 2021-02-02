using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabiola.VersaoBD
{
    class Fornecedor: DadosBase
    {
        public int TipoDeProduto { get; set; }
        public double QtdMes { get; set; }

        public string nomeEmpresa { get; set; }
        public virtual void DefineQtdFornecida()
        {
            TipoDeProduto = int.Parse(Console.ReadLine());
            QtdMes = int.Parse(Console.ReadLine());
        }
        public override void MostraDados()
        {
            nomeEmpresa = Gerador.NomeEmpresa();
            cnpj = DadosBase.Cnpj();
            //Console.WriteLine("{0}{1}", nomeEmpresa, cnpj);
        }
        public virtual void MostraQtdFornecida()
        {
            Console.WriteLine("{0}{1}", TipoDeProduto, QtdMes);
        }
        
        
    }
}

