using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabiola.VersaoBD
{
    class Funcionario: DadosBase
    {
        
        public double salarioPh { get; set; }
        public string cargo { get; set; }
        public double HraEntrada { get; set; }
        public double HraSaida { get; set; }
        public double HraDia { get; set; }
        public override void MostraDados()
        {
            Console.WriteLine("{0}{1}{2}{3}{4}", nome, CPF, idade, saldo);
        }
        public override void DefineDados()
        {
            nome = Gerador.NomePessoa();
            CPF = Gerador.Cpf();
            idade = Gerador.Idade();
            saldo = Gerador.Saldo();
        }
        public void MostraCargo()
        {
            Console.WriteLine("{0}", cargo);
        }
        public  void DefineCargoSalario()
        {
            Console.WriteLine("Qual o cargo do funcionário que deseja cadastrar?");
            cargo = Console.ReadLine();
            Console.WriteLine("Qual o valor por hora que o funcionário receberá?");
            Random RAN = new Random();
            int salarioPh = RAN.Next(1, 100);
            
            
        }
        public double BatePonto(double HraEntrada, double HraSaida, double HraDia)
        {
            Console.WriteLine("Que horas entrou? (Formato 24hr)");
            HraEntrada = double.Parse(Console.ReadLine());
            Console.WriteLine("Que horas saiu? (Formato 24hr)");
            HraSaida = double.Parse(Console.ReadLine());
            HraDia = HraSaida - HraEntrada;
            
            if (HraDia < 7 || HraDia > 9)
            {
                Console.WriteLine("Não é possivel cadastrar valor inferior a 7 hras diárias e maior que 9 hras diárias");
            }
            else 
            {
                 saldo = HraDia * salarioPh;
            }
            
            return saldo;
        }
        public double Salario()
        {
            saldo = saldo * 30;
            return saldo;
        }
        
    }
}
