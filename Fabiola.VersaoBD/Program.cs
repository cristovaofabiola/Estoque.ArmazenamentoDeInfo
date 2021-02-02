using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Fabiola.VersaoBD
{
    class Program
    {
        static void Main(string[] args)
        {
            bool mostraMenu = true;
            SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
            SqlCommand cmd;
            // COMO O CÓDIGO UTILIZOU UM BD COMO REFERÊNCIA E O ID NÃO PODE SER ALTERADO, 
            //FORA DELIMITADA UMA QUANIDADE MÁXIMA DE CLIENTES SOCIO, PORTANTO, 
            //CASO ACONTEÇA ALGUM ERRO, E A OPÇÃO 7 NÃO FOR SELECIONADA(NESTA OPÇÃO DELETO AS INFO DA TABELA CLIENTE SOCIO)
            //DELETE AS INFO DA TABELA CLIENTE SOCIO MANUALMENTE PARA NÃO TENTAR SOBREESCREVER O ID, TRAVANDO O CÓDIGO.

            // OS SALDOS DOS FUNCIONÁRIOS NÃO ESTÃO APARENCENDO NA TELA, DEU ERRO OUTOFRANGE E NÃO CONSEGUI CORRIGIR A EXIBIÇÃO.
            //COMENTEI AS LINHAS, MAS OS VALORES ESTÃO SENDO IMPUTADOS CORRETAMENTE, CONFORME PODE VERIFICAR DIRETO NA TABELA.


            //GERA UM NUMERO ALEATORIO DE CLIENTES ENTRE 5 e 10000
            Random ran = new Random();
            Cliente[] ClienteNormal = new Cliente[(ran.Next(5, 50))];
            //PREENCHE O BANCO DE DADOS COM O NUMERO DE CLIENTES E AS INFORMAÇÕES DOS OBJETOS
            for (int i = 0; i < ClienteNormal.Length; i++)
            {
                Cliente cli = new Cliente();
                cli.DefineDados();
                var insert = $"INSERT into Cliente (Nome , Idade, Saldo, CPF) values ('{cli.nome}','{cli.idade}','{cli.saldo}','{cli.cpf}')";
                
                cmd = new SqlCommand(insert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            //GERA UM NUMERO ALEATORIO DE CLIENTES SOCIO ENTRE 2 e 10
            Socio[] ClienteSocio = new Socio[(ran.Next(2, 11))];
            //PREENCHE O BANCO DE DADOS, TABELA SOCIO, COM O NUMERO ALEATORIO E AS INFORMAÇÕES DO OBJETO
            for (int i = 0; i < ClienteSocio.Length; i++)
            {
                Socio CliSoc = new Socio();
                CliSoc.DefineDados();
                Random Ran = new Random();
                double QtdeAcoes = ran.Next(1, 5);
                if (QtdeAcoes > 4.95)
                {
                    QtdeAcoes = Ran.Next(0, 4) + Ran.NextDouble();
                }
                string insert = $"INSERT into ClienteSocio ( Id, Nome,Idade,CPF,Saldo,QtdeAcoes) " +
                    $" values ({i + 1}, '{CliSoc.nome}','{CliSoc.idade}','{CliSoc.CPF}','{CliSoc.saldo}','{QtdeAcoes}')";

                cmd = new SqlCommand(insert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            //GERA UM NUMERO ALEATORIO DE FUNCIONARIOS ENTRE 5 e 50
            Funcionario[] Fun = new Funcionario[(ran.Next(5, 50))];
            //PREENCHE O BANCO DE DADOS NA TABELA Funcionario. 
            for (int i = 0; i < Fun.Length; i++)
            {
                Funcionario fun = new Funcionario();
                fun.DefineDados();

                string insert = $"INSERT into Funcionarios(nome, idade, CPF ,saldo, cargo, salarioPh) " +
                    $" values ('{fun.nome}','{fun.idade}','{fun.CPF}','{fun.saldo}','{fun.cargo}','{fun.salarioPh}')";

                cmd = new SqlCommand(insert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            //GERA UM NUMERO ALEATORIO DE FORNECEDORES ENTRE 0 e 100
            Fornecedor[] Fornece = new Fornecedor[(ran.Next(5, 50))];
            ////PREENCHE O BANCO DE DADOS NA TABELA FORNECEDOR. 
            for (int i = 0; i < Fornece.Length; i++)
            {
                Fornecedor Fornecer = new Fornecedor();
                Fornecer.MostraDados();

                Random Ran = new Random();
                int TipoDeProduto = Ran.Next(1, 7);
                Random Rano = new Random();
                var QtdMes = ran.Next(1, 500);
                double saldo = 0;
                
                if (TipoDeProduto == 1)
                {
                    saldo = (int)(QtdMes * 05.45);
                    
                }
                else if (TipoDeProduto == 2)
                {
                    saldo = (int)(QtdMes * 06.78);
                    
                }
                else if (TipoDeProduto == 3)
                {
                    saldo = (int)(QtdMes * 01.43);
                     
                }
                else if (TipoDeProduto == 4)
                {
                    saldo = (int)(QtdMes * 02.68);
                    
                }
                else if (TipoDeProduto == 5)
                {
                    saldo = (int)(QtdMes * 03.78);
                    
                }
                else if (TipoDeProduto == 6)
                {
                    saldo = (int)(QtdMes * 02.96);
                }

                SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                string insert = $"INSERT into Fornecedores(NomeDaEmpresa, CNPJ, TipoDeProduto , QtdeMes, Saldo) " +
                    $" values ('{Fornecer.nomeEmpresa}','{Fornecer.cnpj}','{TipoDeProduto}','{QtdMes}','{saldo}')";

                SqlCommand cmdforn = new SqlCommand(insert, conn1);
                conn1.Open();
                cmdforn.ExecuteNonQuery();
                conn1.Close();


            }
            while (mostraMenu)
            {
                mostraMenu = Menu();
            }
        }
        public static bool Menu()
        {
            Console.WriteLine("\r\nMenu:");
            Console.WriteLine("1) Adicionar ente:");
            Console.WriteLine("2) Remover ente:");
            Console.WriteLine("3) Comprar:");
            Console.WriteLine("4) Alterações:");
            Console.WriteLine("5) Bater cartão:");
            Console.WriteLine("6) Calcular Lucro:");
            Console.WriteLine("7) Encerrar/Sair:");
            Console.Write("\r\nSelecione uma opção: ");

            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1://ADICIONAR ENTE
                    Console.WriteLine("Qual ente gostaria de adicionar?\n " +
                        "1- Cliente\n " +
                        "2- Cliente Sócio\n " +
                        "3- Funcionário\n " +
                        "4 - Fornecedor");
                    int EscolhaIf = int.Parse(Console.ReadLine());
                    if (EscolhaIf == 1)
                    {
                        Console.WriteLine("Insira o CPF do cliente:");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                        SqlCommand cmdcli = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdcli.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();

                        //SE JÁ CADASTRADO APARECE MSG DE ERRO E REDIRECIONA PARA O MENU PRONCIPAL
                        if (contagem > 0)
                        {
                            Console.WriteLine("Cliente já cadastrado, não é possivel fazer novo cadastro");
                            Menu();
                        }
                        //DO CONTRÁRIO ADICIONA O CLIENTE A TABEL CLIENTE
                        else
                        {
                            Console.WriteLine("Cliente não cadastrado. Para prosseguir preencha as informações");
                            Console.WriteLine(" Informe o nome do cliente:");
                            string Nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do cliente:");
                            int Idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do cliente:");
                            double Saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do cliente:");
                            var CPF = Console.ReadLine();


                            string insert = $"INSERT into dbo.Cliente ( Nome, Idade, Saldo, CPF) values ('{Nome}', '{Idade}', '{Saldo}', '{CPF}')";
                            SqlCommand cmdcliInserir = new SqlCommand(insert, conn);
                            conn.Open();
                            cmdcliInserir.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Cliente adicionado com sucesso!");
                            Menu();
                        }

                    }
                    else if (EscolhaIf == 2)//INSERE NOVO CLIENTE SOCIO
                    {
                        Console.WriteLine("Insira o CPF do Cliente Socio:");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.ClienteSocio WHERE CPF ='{cpf}'";
                        SqlCommand cmdcliSocio = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdcliSocio.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();


                        //SE JÁ CADASTRADO APARECE MSG DE ERRO E REDIRECIONA PARA O MENU PRONCIPAL
                        if (contagem > 0)
                        {
                            Console.WriteLine("Cliente já cadastrado, não é possivel fazer novo cadastro");
                            Menu();
                        }
                        //DO CONTRÁRIO ADICIONA O CLIENTE A TABEL CLIENTE SOCIO
                        else
                        {
                            Console.WriteLine("Cliente socio não cadastrado. Forneça as seguintes informações:");
                            Console.WriteLine(" Informe o nome do cliente:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do cliente:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do cliente:");
                            double saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do cliente:");
                            var CPF = Console.ReadLine();
                            Console.WriteLine("Informe a quantidade de ações possui.");
                            double QtdeAcoes = double.Parse(Console.ReadLine());
                            if (QtdeAcoes > 4.95)
                            {
                                Console.Write("A quantidade de ações excede o permitido");
                                Menu();
                            }
                            int i = 0;
                            string insert = $"INSERT into dbo.ClienteSocio (Id, Nome, Idade, Saldo, CPF) values ('{i + 1}''{nome}', '{idade}', '{saldo}', '{CPF}', '{QtdeAcoes}')";
                            cmdcliSocio = new SqlCommand(insert, conn);
                            conn.Open();
                            cmdcliSocio.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Cliente adicionado com sucesso!");
                            Menu();
                        }
                    }
                    else if (EscolhaIf == 3)
                    {
                        Console.WriteLine("Insira o CPF do Funcionário:");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf}'";
                        SqlCommand cmdfun = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdfun.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();

                        //SE JÁ CADASTRADO APARECE MSG DE ERRO E REDIRECIONA PARA O MENU PRONCIPAL
                        if (contagem > 0)
                        {
                            Console.WriteLine("Funcionário já cadastrado, não é possivel fazer novo cadastro");
                            Menu();
                        }
                        //DO CONTRÁRIO ADICIONA O CLIENTE A TABEL CLIENTE FUNCIONARIOS
                        else
                        {
                            Console.WriteLine("Funcionario não cadastrado, forneça as seguintes informações:");
                            Console.WriteLine(" Informe o nome do Funcionário:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do Funcionário:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do Funcionário:");
                            double saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do Funcionário:");
                            var CPF = Console.ReadLine();
                            Console.WriteLine("Informe qual o cargo que irá ocupar:");
                            string cargo = Console.ReadLine();
                            Console.WriteLine(" Informe qual o valor por hora que o Funcionário receberá:");
                            double salarioPh = double.Parse(Console.ReadLine());

                            string insert = $"INSERT into dbo.Funcionarios (nome, idade, saldo, CPF, cargo, salarioPH) values ('{nome}', '{idade}', '{saldo}', '{CPF}', '{cargo}', '{salarioPh}')";
                            cmdfun = new SqlCommand(insert, conn);

                            conn.Open();
                            cmdfun.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Funcionário adicionado com sucesso!");
                            Menu();
                        }
                    }
                    else if (EscolhaIf == 4)
                    {
                        Console.WriteLine("Insira o CNPJ do Fornecedor:");
                        var cnpj = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CNPJ from dbo.Fornecedores WHERE CNPJ ='{cnpj}'";
                        SqlCommand cmdforn = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdforn.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();


                        //SE JÁ CADASTRADO APARECE MSG DE ERRO E REDIRECIONA PARA O MENU PRONCIPAL
                        if (contagem > 0)
                        {
                            Console.WriteLine("Fornecedor já cadastrado, não é possivel fazer novo cadastro");
                            Menu();
                        }
                        //DO CONTRÁRIO ADICIONA O CLIENTE A TABELA FORNECEDOR
                        else
                        {
                            Console.WriteLine(" Informe o nome da empresa :");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe o CNPJ:");
                            var CNPJ = Console.ReadLine();
                            Console.WriteLine("Informe o tipo de produto fornecido (Utilize os ID de 1 a 6)");
                            Console.WriteLine("1 - Produto 01 (R$05,45)\n 2 - Produto 02 (R$06,78)\n " +
                                "3 - Produto 03 (R$1,43)\n 4 - Produto 04 (R$02,68)\n 5 - Produto 05 (R$03,78)\n" +
                                "6 - Produto 06 (R$02,96)\n;");
                            var TipoDeProduto = int.Parse(Console.ReadLine());
                            Console.WriteLine("Qual a quantidade que fornecerá este mês?");
                            int QtdMes = int.Parse(Console.ReadLine());

                            double saldo = 0;
                            if (TipoDeProduto == 1)
                            {
                                QtdMes = (int)(QtdMes * 05.45);
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 2)
                            {
                                QtdMes = (int)(QtdMes * 06.78);
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 3)
                            {
                                QtdMes = (int)(QtdMes * 01.43);
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 4)
                            {
                                QtdMes = (int)(QtdMes * 02.68);
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 5)
                            {
                                QtdMes = (int)(QtdMes * 03.78);
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 6)
                            {
                                QtdMes = (int)(QtdMes * 02.96);
                                saldo = QtdMes;
                            }
                            else
                            {
                                Console.WriteLine("Tipo de produto não encontrado");
                            }

                            string insert = $"INSERT into dbo.Fornecedores ( NomeDaEmpresa, Cnpj, TipoDeProduto, QtdeMes, Saldo) values ('{nome}', '{CNPJ}', '{TipoDeProduto}', '{QtdMes}','{saldo}')";
                            cmdforn = new SqlCommand(insert, conn);
                            conn.Open();
                            cmdforn.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Fornecedor adicionado com sucesso!");
                            Menu();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Escolha inválida, volte ao Menu principal");
                        Menu();
                    }
                    break;
                case 2://DELETAR ENTE
                    Console.WriteLine("Qual ente gostaria de remover?\n 1- Cliente\n" +
                        " 2- Cliente Sócio\n " +
                        "3- Funcionário\n " +
                        "4 - Fornecedor");
                    int escolha2 = int.Parse(Console.ReadLine());
                    if (escolha2 == 1)
                    {
                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();
                        //confere o cpf
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                        SqlCommand cmdcli = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdcli.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();

                        //SE ENCONTRADO, DLETA-SE DO BANCO DE DADOS
                        if (contagem > 0)
                        {
                            string delete = $"DELETE from Cliente WHERE CPF = '{cpf}'";
                            cmdcli = new SqlCommand(delete, conn);
                            conn.Open();
                            cmdcli.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Cliente deletado com sucesso!");
                            Menu();
                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha2 == 2)
                    {
                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                        SqlCommand cmdclisocio = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdclisocio.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();


                        //SE ENCONTRADO, DLETA-SE DO BANCO DE DADOS
                        if (contagem > 0)
                        {
                            conn.Open();
                            string delete = $"DELETE from ClienteSocio WHERE CPF = '{cpf}'";
                            cmdclisocio = new SqlCommand(delete, conn);
                            cmdclisocio.ExecuteNonQuery();
                            conn.Close();
                            Console.WriteLine("Cliente Socio removido com sucesso!");
                            Menu();

                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Cliente Socio não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha2 == 3)
                    {
                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf}'";
                        SqlCommand cmdfun = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdfun.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();


                        //SE ENCONTRADO, DLETA-SE DO BANCO DE DADOS
                        if (contagem > 0)
                        {
                            string delete = $"DELETE from Funcionarios WHERE CPF = '{cpf}'";
                            cmdfun = new SqlCommand(delete, conn);

                            conn.Open();
                            cmdfun.ExecuteNonQuery();
                            conn.Close();
                            Console.WriteLine("Funcionário removido com sucesso!");
                            Menu();
                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Funcionario não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha2 == 4)
                    {
                        Console.WriteLine("Informe o CNPJ");
                        var cnpj = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr;
                        int contagem = 0;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta = $"SELECT CPF from dbo.Fornecedores WHERE CPF ='{cnpj}'";
                        SqlCommand cmdforn = new SqlCommand(consulta, conn);
                        conn.Open();
                        dr = cmdforn.ExecuteReader();

                        while (dr.Read())
                        {
                            contagem++;
                        }

                        conn.Close();
                        dr.Close();


                        //SE ENCONTRADO, DELETA-SE DO BANCO DE DADOS
                        if (contagem > 0)
                        {
                            string delete = $"DELETE from Fornecedores WHERE CNPJ = '{cnpj}'";
                            cmdforn = new SqlCommand(delete, conn);

                            conn.Open();
                            cmdforn.ExecuteNonQuery();
                            conn.Close();

                            Console.WriteLine("Fornecedor removido com sucesso!");
                            Menu();
                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Fornecedor não encontrado");
                            Menu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Escolha inválida, volte ao Menu principal");
                        Menu();
                    }
                    break;

                case 3://COMPRAR

                    Console.WriteLine("Que tipo de cliente você é? 1 - Cliente Comum\n 2 - Cliente Sócio ");
                    int escolha3 = int.Parse(Console.ReadLine());
                    switch (escolha3)
                    {
                        case 1:
                            Console.WriteLine("Informe o CPF");
                            var cpf = Console.ReadLine();

                            //CONFERE  O CPF
                            SqlDataReader dr;
                            int contagem = 0;
                            SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                            var consulta = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                            SqlCommand cmdcli = new SqlCommand(consulta, conn);
                            conn.Open();
                            dr = cmdcli.ExecuteReader();

                            while (dr.Read())
                            {
                                contagem++;
                            }

                            conn.Close();
                            dr.Close();

                            if (contagem > 0)
                            {
                                Console.WriteLine(" Qual o valor da sua compra?");
                                double saldo = double.Parse(Console.ReadLine());
                                string alterar = $"UPDATE Cliente  Set saldo = saldo + {saldo} WHERE CPF = '{cpf}'";
                                cmdcli = new SqlCommand(alterar, conn);

                                conn.Open();
                                cmdcli.ExecuteNonQuery();
                                conn.Close();
                                Console.WriteLine("Saldo alterado com sucesso!");
                                Menu();
                            }
                            else
                            {
                                Console.WriteLine("CPF digitado não encontrado");
                                Menu();
                            }
                            break;
                        case 2:
                            Console.WriteLine("Informe o CPF");
                            var cpf1 = Console.ReadLine();
                            //CONFERE O CPF
                            SqlDataReader dr1;
                            int contagem1 = 0;
                            SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                            var consulta1 = $"SELECT CPF from dbo.ClienteSocio WHERE CPF ='{cpf1}'";
                            SqlCommand cmdclisocio = new SqlCommand(consulta1, conn1);
                            conn1.Open();
                            dr1 = cmdclisocio.ExecuteReader();

                            while (dr1.Read())
                            {
                                contagem1++;
                            }

                            conn1.Close();
                            dr1.Close();

                            if (contagem1 > 0)
                            {
                                Console.WriteLine(" Qual o valor da sua compra?");
                                double saldo = double.Parse(Console.ReadLine());
                                string alterar = $"UPDATE ClienteSocio Set saldo = saldo + {saldo} WHERE CPF = '{cpf1}'";
                                cmdclisocio = new SqlCommand(alterar, conn1);

                                conn1.Open();
                                cmdclisocio.ExecuteNonQuery();
                                conn1.Close();
                                Console.WriteLine("Saldo alterado com sucesso!");
                                Menu();
                            }
                            else
                            {
                                Console.WriteLine("CPF digitado não encontrado");
                                Menu();
                            }
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                    break;
                case 4://ATERAR INFORMAÇÕES
                    Console.WriteLine("O que gostaria de alterar?\n" +
                        " 1- Cliente\n " +
                        "2- Cliente Sócio\n " +
                        "3- Funcionário\n " +
                        "4 - Fornecedor\n " +
                        "5 -Compras\n" +
                        "6- Bater Cartão");
                    int escolha4 = int.Parse(Console.ReadLine());
                    if (escolha4 == 1)
                    {
                        //EXIBE TABELA CLIENTE
                        SqlDataReader dr;
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        string exibir = "SELECT * FROM Cliente";
                        SqlCommand cmdClientes = new SqlCommand(exibir, conn);
                        conn.Open();
                        dr = cmdClientes.ExecuteReader();

                        while (dr.Read())
                        {
                            Console.WriteLine(dr["Id"]);
                            Console.WriteLine(dr["Nome"]);
                            Console.WriteLine(dr["Idade"]);
                            Console.WriteLine(dr["Saldo"]);
                            Console.WriteLine(dr["CPF"]);
                            Console.WriteLine("----------//----------");
                        }

                        conn.Close();
                        dr.Close();

                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr1;
                        int contagem1 = 0;
                        SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta1 = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                        SqlCommand cmdcli = new SqlCommand(consulta1, conn1);
                        conn1.Open();
                        dr1 = cmdcli.ExecuteReader();

                        while (dr1.Read())
                        {
                            contagem1++;
                        }

                        conn1.Close();
                        dr1.Close();

                        //SE ENCONTRADO, ALTERA-SE NO BANCO DE DADOS
                        if (contagem1 > 0)
                        {
                            Console.WriteLine(" Informe o nome do cliente:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do cliente:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do cliente:");
                            double saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do cliente:");
                            var CPF = Console.ReadLine();

                            string alterar = $"UPDATE Cliente Set nome = '{nome}', idade = '{idade}', saldo = '{saldo}',CPF = '{cpf}' WHERE CPF = '{cpf}'";
                            cmdcli = new SqlCommand(alterar, conn);

                            conn.Open();
                            cmdcli.ExecuteNonQuery();
                            conn.Close();
                            Console.WriteLine("Cliente alterado com sucesso!");
                            Menu();

                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha4 == 2)
                    {
                        //EXIBE TABELA CLIENTES SOCIO
                        SqlDataReader dr02;
                        SqlConnection conn02 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        string exibir02 = "SELECT * FROM ClienteSocio";
                        SqlCommand cmdClientes01 = new SqlCommand(exibir02, conn02);
                        conn02.Open();
                        dr02 = cmdClientes01.ExecuteReader();

                        while (dr02.Read())
                        {
                            Console.WriteLine(dr02["id"]);
                            Console.WriteLine(dr02["Nome"]);
                            Console.WriteLine(dr02["Idade"]);
                            Console.WriteLine(dr02["Saldo"]);
                            Console.WriteLine(dr02["CPF"]);
                            Console.WriteLine("----------//----------");
                        }

                        conn02.Close();
                        dr02.Close();

                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr1;
                        int contagem1 = 0;
                        SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta1 = $"SELECT CPF from dbo.ClienteSocio WHERE CPF ='{cpf}'";
                        SqlCommand cmdclisocio = new SqlCommand(consulta1, conn1);
                        conn1.Open();
                        dr1 = cmdclisocio.ExecuteReader();

                        while (dr1.Read())
                        {
                            contagem1++;
                        }

                        conn1.Close();
                        dr1.Close();

                        //SE ENCONTRADO, ALTERA-SE AS INFORMAÇÕES
                        if (contagem1 > 0)
                        {
                            Console.WriteLine(" Informe o nome do cliente:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do cliente:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do cliente:");
                            double saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do cliente:");
                            var CPF = Console.ReadLine();
                            Console.WriteLine("Informe a quantidade de ações que o Cliente Sócio possui");
                            double QtdAcoes = int.Parse(Console.ReadLine());
                            if (QtdAcoes > 4.98)
                            {
                                Console.WriteLine("Ações excedem o valor permitido. Cliente não alterado");
                                Menu();
                            }

                            string alterar = $"UPDATE ClienteSocio Set nome = '{nome}', idade = '{idade}', saldo = '{saldo}', CPF = '{CPF}', QtdeAcoes = '{QtdAcoes}' WHERE CPF = '{CPF}'";
                            cmdclisocio = new SqlCommand(alterar, conn1);

                            conn1.Open();
                            cmdclisocio.ExecuteNonQuery();
                            conn1.Close();
                            Console.WriteLine("Cliente alterado com sucesso!");
                            Menu();

                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Cliente Socio não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha4 == 3)
                    {
                        //EXIBE TABELA FUNCIONARIOS
                        SqlDataReader dr02;
                        SqlConnection conn02 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        string exibir02 = $"SELECT * FROM Funcionarios";
                        SqlCommand cmdClientes01 = new SqlCommand(exibir02, conn02);
                        conn02.Open();
                        dr02 = cmdClientes01.ExecuteReader();

                        while (dr02.Read())
                        {
                            Console.WriteLine(dr02["Id"]);
                            Console.WriteLine(dr02["nome"]);
                            Console.WriteLine(dr02["idade"]);
                            Console.WriteLine(dr02["CPF"]);
                            //Console.WriteLine(dr02["Saldo"]);
                            Console.WriteLine(dr02["cargo"]);
                            Console.WriteLine(dr02["salarioPh"]);
                            Console.WriteLine("----------//----------");
                        }

                        conn02.Close();
                        dr02.Close();
                        Console.WriteLine("Informe o CPF");
                        var cpf = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr1;
                        int contagem1 = 0;
                        SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta1 = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf}'";
                        SqlCommand cmdfun = new SqlCommand(consulta1, conn1);
                        conn1.Open();
                        dr1 = cmdfun.ExecuteReader();

                        while (dr1.Read())
                        {
                            contagem1++;
                        }

                        conn1.Close();
                        dr1.Close();

                        //SE ENCONTRADO, ALTERA-SE NO BANCO DE DADOS
                        if (contagem1 > 0)

                        {
                            conn1.Open();
                            //EXIBIR BD CLIENTE
                            Console.WriteLine(" Informe o nome do Funcionário:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe a idade do Funcionário:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o saldo do Funcionário:");
                            double saldo = double.Parse(Console.ReadLine());
                            Console.WriteLine(" Informe o CPF do Funcionário:");
                            var CPF = Console.ReadLine();
                            Console.WriteLine("Informe o salário que o funcionário receberá por hora");
                            var salarioPh = Console.ReadLine();

                            string alterar = $"UPDATE Funcionarios  Set nome = '{nome}', idade = '{idade}', saldo = '{saldo}', CPF = '{cpf}', salarioPh = '{salarioPh}' WHERE CPF = '{cpf}'";
                            cmdfun = new SqlCommand(alterar, conn1);
                            cmdfun.ExecuteNonQuery();
                            conn1.Close();
                            Console.WriteLine("Funcionário  alterado com sucesso!");
                            Menu();
                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Funcionário não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha4 == 4)
                    {
                        //EXIBE TABELA FORNECEDORES
                        SqlDataReader dr02;
                        SqlConnection conn02 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        string exibir02 = "SELECT * FROM Fornecedores";
                        SqlCommand cmdClientes01 = new SqlCommand(exibir02, conn02);
                        conn02.Open();
                        dr02 = cmdClientes01.ExecuteReader();

                        while (dr02.Read())
                        {
                            Console.WriteLine(dr02["id"]);
                            Console.WriteLine(dr02["NomeDaEmpresa"]);
                            Console.WriteLine(dr02["CNPJ"]);
                            Console.WriteLine(dr02["TipoDeProduto"]);
                            Console.WriteLine(dr02["QtdeMes"]);
                            Console.WriteLine("----------//----------");
                        }

                        conn02.Close();
                        dr02.Close();
                        Console.WriteLine("Informe o CNPJ");
                        var cnpj = Console.ReadLine();

                        //SELECT PARA BUSCAR O CPF DIGITADO
                        SqlDataReader dr1;
                        int contagem1 = 0;
                        SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        var consulta1 = $"SELECT CNPJ from dbo.Fornecedores WHERE CNPJ ='{cnpj}'";
                        SqlCommand cmdforn = new SqlCommand(consulta1, conn1);
                        conn1.Open();
                        dr1 = cmdforn.ExecuteReader();

                        while (dr1.Read())
                        {
                            contagem1++;
                        }

                        conn1.Close();
                        dr1.Close();

                        //SE ENCONTRADO, ALTERA-SE NO BANCO DE DADOS
                        if (contagem1 > 0)
                        {

                            Console.WriteLine(" Informe o Nome da Empresa:");
                            string nome = Console.ReadLine();
                            Console.WriteLine(" Informe o CNPJ da empresa:");
                            var CNPJ = Console.ReadLine();
                            Console.WriteLine("Informe o tipo de produto fornecido (Utilize os ID de 1 a 6)");
                            Console.WriteLine("1 - Produto 01 (R$05,45)\n 2 - Produto 02 (R$06,78)\n " +
                                "3 - Produto 03 (R$1,43)\n 4 - Produto 04 (R$02,68)\n 5 - Produto 05 (R$03,78)\n" +
                                "6 - Produto 06 (R$02,96)\n;");
                            var TipoDeProduto = int.Parse(Console.ReadLine());
                            Console.WriteLine("Qual a quantidade que fornecerá este mês?");
                            var QtdMes = double.Parse(Console.ReadLine());
                            double saldo = 0;
                            if (TipoDeProduto == 1)
                            {
                                QtdMes = QtdMes * 05.45;
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 2)
                            {
                                QtdMes = QtdMes * 06.78;
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 3)
                            {
                                QtdMes = QtdMes * 01.43;
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 4)
                            {
                                QtdMes = QtdMes * 02.68;
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 5)
                            {
                                QtdMes = QtdMes * 03.78;
                                saldo = QtdMes;
                            }
                            else if (TipoDeProduto == 6)
                            {
                                QtdMes = QtdMes * 02.96;
                                saldo = QtdMes;
                            }
                            else
                            {
                                Console.WriteLine("Tipo de produto não encontrado");
                            }

                            string alterar = $"UPDATE Fornecedores Set NomeDaEmpresa = '{nome}', CNPJ = '{cnpj}', TipoDeProduto = '{TipoDeProduto}', QtdeMes = '{QtdMes}', Saldo = '{saldo}'WHERE CNPJ = '{cnpj}'";
                            cmdforn = new SqlCommand(alterar, conn1);

                            conn1.Open();
                            cmdforn.ExecuteNonQuery();
                            conn1.Close();
                            Console.WriteLine("Cliente alterado com sucesso!");
                            Menu();
                        }
                        //DO CONTRÁRIO APRESENTA MSG INFORMANDO QUE NÃO FOI ENCONTRADO
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                            Menu();
                        }
                    }
                    else if (escolha4 == 6)//ALTERAR COMPRAS
                    {
                        Console.WriteLine("Que tipo de cliente você é? 1 - Cliente Comum\n 2 - Cliente Sócio ");
                        int escolha6 = int.Parse(Console.ReadLine());
                        switch (escolha6)
                        {
                            case 1:
                                Console.WriteLine("Informe o CPF");
                                var cpf = Console.ReadLine();

                                //CONFERE  O CPF
                                SqlDataReader dr;
                                int contagem = 0;
                                SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                                var consulta = $"SELECT CPF from dbo.Cliente WHERE CPF ='{cpf}'";
                                SqlCommand cmdcli = new SqlCommand(consulta, conn);
                                conn.Open();
                                dr = cmdcli.ExecuteReader();

                                while (dr.Read())
                                {
                                    contagem++;
                                }

                                conn.Close();
                                dr.Close();

                                if (contagem > 0)
                                {
                                    Console.WriteLine(" Qual o valor da sua compra?");
                                    double saldo = double.Parse(Console.ReadLine());
                                    string alterar = $"UPDATE from Cliente WHERE Set CPF = '{cpf}' Set saldo = '{saldo}'";
                                    cmdcli = new SqlCommand(alterar, conn);

                                    conn.Open();
                                    cmdcli.ExecuteNonQuery();
                                    conn.Close();
                                    Console.WriteLine("Saldo alterado com sucesso!");
                                    Menu();
                                }
                                else
                                {
                                    Console.WriteLine("CPF digitado não encontrado");
                                    Menu();
                                }
                                break;
                            case 2:
                                Console.WriteLine("Informe o CPF");
                                var cpf1 = Console.ReadLine();
                                //CONFERE O CPF
                                SqlDataReader dr1;
                                int contagem1 = 0;
                                SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                                var consulta1 = $"SELECT CPF from dbo.ClienteSocio WHERE CPF ='{cpf1}'";
                                SqlCommand cmdclisocio = new SqlCommand(consulta1, conn1);
                                conn1.Open();
                                dr1 = cmdclisocio.ExecuteReader();

                                while (dr1.Read())
                                {
                                    contagem1++;
                                }

                                conn1.Close();
                                dr1.Close();

                                if (contagem1 > 0)
                                {
                                    Console.WriteLine(" Qual o valor da sua compra?");
                                    double saldo = double.Parse(Console.ReadLine());
                                    string alterar = $"UPDATE ClienteSocio  Set saldo = '{saldo}'WHERE Set CPF = '{cpf1}'";
                                    cmdclisocio = new SqlCommand(alterar, conn1);

                                    conn1.Open();
                                    cmdclisocio.ExecuteNonQuery();
                                    conn1.Close();
                                    Console.WriteLine("Saldo alterado com sucesso!");
                                    Menu();
                                }
                                else
                                {
                                    Console.WriteLine("CPF digitado não encontrado");
                                    Menu();
                                }
                                break;
                            default:
                                Console.WriteLine("Opção inválida");
                                break;
                        }
                        break;

                    }
                    else if (escolha4 == 5)//ALTERAR CARTÃO PONTO
                    {
                        //EXIBE TABELA FUNCIONARIOS
                        SqlDataReader dr05;
                        SqlConnection conn05 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        string exibir05 = "SELECT * FROM Funcinarios";
                        SqlCommand cmdFuncionarios5 = new SqlCommand(exibir05, conn05);
                        conn05.Open();
                        dr05 = cmdFuncionarios5.ExecuteReader();

                        while (dr05.Read())
                        {
                            Console.WriteLine(dr05["id"]);
                            Console.WriteLine(dr05["Nome"]);
                            Console.WriteLine(dr05["Idade"]);
                            Console.WriteLine(dr05["Saldo"]);
                            Console.WriteLine(dr05["CPF"]);
                            Console.WriteLine("----------//----------");
                        }

                        conn05.Close();
                        dr05.Close();


                        Console.WriteLine("Gostaria de inserir as informações:\n 1- Ponto diário\n 2-Ponto mensal");
                        int escolha5 = int.Parse(Console.ReadLine());
                        Funcionario fun5 = new Funcionario();

                        switch (escolha5)//ALTERAR CARTÃO PONTO
                        {
                            case 1:
                                // EXIBE TABELA FUNCIONARIOS
                                SqlDataReader dr03;
                                SqlConnection conn03 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                                string exibir03 = "SELECT * FROM Funcinarios";
                                SqlCommand cmdFuncionarios03 = new SqlCommand(exibir03, conn03);
                                conn03.Open();
                                dr03 = cmdFuncionarios03.ExecuteReader();

                                while (dr03.Read())
                                {
                                    Console.WriteLine(dr03["id"]);
                                    Console.WriteLine(dr03["Nome"]);
                                    Console.WriteLine(dr03["Idade"]);
                                    Console.WriteLine(dr03["Saldo"]);
                                    Console.WriteLine(dr03["CPF"]);
                                    Console.WriteLine("----------//----------");
                                }

                                conn05.Close();
                                dr05.Close();

                                // CONFERE O CPF DO FUCNIONÁRIO
                                Console.WriteLine("Insira seu CPF:");
                                var cpf = Console.ReadLine();

                                SqlDataReader dr1;
                                int contagem1 = 0;
                                SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                                var consulta1 = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf}'";
                                SqlCommand cmdfunc = new SqlCommand(consulta1, conn1);
                                conn1.Open();
                                dr1 = cmdfunc.ExecuteReader();

                                while (dr1.Read())
                                {
                                    contagem1++;
                                }

                                conn1.Close();
                                dr1.Close();
                                if (contagem1 > 0)
                                {
                                    Funcionario func = new Funcionario();
                                    Console.WriteLine("Que horas entrou? (Formato 24hr)");
                                    double HraEntrada = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Que horas saiu? (Formato 24hr)");
                                    double HraSaida = double.Parse(Console.ReadLine());
                                    double HraDia = HraSaida - HraEntrada;

                                    if (HraDia < 7 || HraDia > 9)
                                    {
                                        Console.WriteLine("Não é possivel cadastrar valor inferior a 7 hras diárias e maior que 9 hras diárias");
                                    }


                                    string alterar = $"UPDATE Funcionarios Set saldo = saldo + (salarioPh * '{HraDia}')WHERE CPF = '{cpf}'";
                                    cmdfunc = new SqlCommand(alterar, conn1);

                                    conn1.Open();
                                    cmdfunc.ExecuteNonQuery();
                                    conn1.Close();
                                    Console.WriteLine("Ponto diário registrado");
                                    Menu();


                                }
                                else
                                {
                                    Console.WriteLine(" CPF não encontrado.");
                                    Menu();
                                }
                                break;
                            case 2:
                                double saldoMes = fun5.Salario();
                                string alterarMes = $"UPDATE Funcionarios  Set saldo = saldo + '{saldoMes}'WHERE CPF = '{fun5.CPF}'";
                                SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                                cmdfunc = new SqlCommand(alterarMes, connect);

                                connect.Open();
                                cmdfunc.ExecuteNonQuery();
                                connect.Close();
                                Console.WriteLine("Saldo mensal alterado com sucesso!");
                                Menu();

                                break;
                            default:
                                Console.WriteLine("Opção inválida!");
                                Menu();
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida, retorne ao Menu");
                        Menu();
                    }
                    break;

                case 5://BATER CARTÃO
                    //EXIBE TABELA FUNCIONARIOS
                    SqlDataReader dr01;
                    SqlConnection conn01 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                    string exibir01 = "SELECT * FROM Funcionarios";
                    SqlCommand cmdFuncionarios = new SqlCommand(exibir01, conn01);
                    conn01.Open();
                    dr01 = cmdFuncionarios.ExecuteReader();


                    while (dr01.Read())
                    {
                        Console.WriteLine(dr01["id"]);
                        Console.WriteLine(dr01["nome"]);
                        Console.WriteLine(dr01["idade"]);
                        Console.WriteLine(dr01["CPF"]);
                        //Console.WriteLine(dr01["saldo"]);
                        Console.WriteLine(dr01["cargo"]);
                        Console.WriteLine(dr01["salarioPh"]);
                        Console.WriteLine("----------//----------");
                    }

                    conn01.Close();
                    dr01.Close();

                    Console.WriteLine("Gostaria de inserir as informações:\n 1- Ponto diário\n 2-Ponto mensal");
                    escolha = int.Parse(Console.ReadLine());
                    Funcionario fun = new Funcionario();
                    switch (escolha)
                    {
                        case 1:
                            // EXIBE TABELA FUNCIONARIOS
                            SqlDataReader dr;
                            SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                            string exibir = "SELECT * FROM Funcionarios";
                            SqlCommand cmdFunc = new SqlCommand(exibir, conn);
                            conn.Open();
                            dr = cmdFunc.ExecuteReader();


                            while (dr.Read())
                            {
                                Console.WriteLine(dr["Id"]);
                                Console.WriteLine(dr["nome"]);
                                Console.WriteLine(dr["idade"]);
                                //Console.WriteLine(dr["saldo"]);
                                Console.WriteLine(dr["CPF"]);
                                Console.WriteLine("----------//----------");
                            }



                            conn.Close();
                            dr.Close();

                            Console.WriteLine("Insira seu CPF:");
                            var cpf = Console.ReadLine();

                            //confere o cpf
                            SqlDataReader dr1;
                            int contagem1 = 0;
                            SqlConnection conn1 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                            var consulta1 = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf}'";
                            SqlCommand cmdfunc = new SqlCommand(consulta1, conn1);
                            conn1.Open();
                            dr1 = cmdfunc.ExecuteReader();

                            while (dr1.Read())
                            {
                                contagem1++;
                            }

                            conn1.Close();
                            dr1.Close();
                            if (contagem1 > 0)
                            {
                                Funcionario func = new Funcionario();
                                Console.WriteLine("Que horas entrou? (Formato 24hr)");
                                double HraEntrada = double.Parse(Console.ReadLine());
                                Console.WriteLine("Que horas saiu? (Formato 24hr)");
                                double HraSaida = double.Parse(Console.ReadLine());
                                double HraDia = HraSaida - HraEntrada;

                                if (HraDia < 7 || HraDia > 9)
                                {
                                    Console.WriteLine("Não é possivel cadastrar valor inferior a 7 hras diárias e maior que 9 hras diárias");
                                }
                                double mes = HraDia * 30;

                                string alterar = $"UPDATE Funcionarios Set saldo = saldo + (salarioPh * {mes}) WHERE CPF = '{cpf}'";
                                cmdfunc = new SqlCommand(alterar, conn1);

                                conn1.Open();
                                cmdfunc.ExecuteNonQuery();
                                conn1.Close();
                                Console.WriteLine("Ponto diário registrado");
                                Menu();

                            }
                            else
                            {
                                Console.WriteLine(" CPF não encontrado.");
                                Menu();
                            }
                            break;
                        case 2:
                            Console.WriteLine("Insira seu CPF:");
                            var cpf1 = Console.ReadLine();

                            //confere o cpf
                            SqlDataReader dr00;
                            int contagem = 0;
                            SqlConnection conn00 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                            var consulta = $"SELECT CPF from dbo.Funcionarios WHERE CPF ='{cpf1}'";
                            SqlCommand cmdfunci = new SqlCommand(consulta, conn00);
                            conn00.Open();
                            dr00 = cmdfunci.ExecuteReader();

                            while (dr00.Read())
                            {
                                contagem++;
                            }

                            conn00.Close();
                            dr00.Close();

                            if (contagem > 0)
                            {
                                Funcionario func = new Funcionario();
                                Console.WriteLine("Que horas entrou? (Formato 24hr)");
                                func.HraEntrada = double.Parse(Console.ReadLine());
                                Console.WriteLine("Que horas saiu? (Formato 24hr)");
                                func.HraSaida = double.Parse(Console.ReadLine());
                                func.HraDia = func.HraSaida - func.HraEntrada;

                                if (func.HraDia < 7 || func.HraDia > 9)
                                {
                                    Console.WriteLine("Não é possivel cadastrar valor inferior a 7 hras diárias e maior que 9 hras diárias");
                                }

                                double saldoMes = func.HraDia * 30;
                                string alterarMes = $"UPDATE Funcionarios  Set saldo = saldo + {saldoMes} WHERE CPF = '{fun.CPF}'";
                                SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                                cmdfunc = new SqlCommand(alterarMes, connect);

                                connect.Open();
                                cmdfunc.ExecuteNonQuery();
                                connect.Close();
                                Console.WriteLine("Saldo mensal alterado com sucesso!");
                                Menu();
                            }
                            else
                            {
                                Console.WriteLine("Funcionário não encontrado");
                                Menu();
                            }

                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            Menu();
                            break;
                    }
                    break;

                case 6://calculo dos lucros
                    //lucros: soma do saldo de todos os clientes. Soma de salario funcionario mais fornecedores.
                    //SOMA OS VALORES DOS SALDOS DOS CLIENTES
                    string getSomaSaldos = "Select saldo from cliente";
                    SqlConnection connecta = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                    SqlCommand cmdfunciona = new SqlCommand(getSomaSaldos, connecta);
                    connecta.Open();
                    SqlDataReader somaFunciona = cmdfunciona.ExecuteReader();
                    double [] recebe = new double[50];
                    double soma = 0;
                    while (somaFunciona.Read())
                    {
                        for (int i = 0; i < recebe.Length; i++)
                        {
                            recebe [i] = Convert.ToDouble(somaFunciona["saldo"]);
                            for (int j = 0; j < recebe.Length; j++)
                            {
                                soma += recebe[i];
                                
                            }
                            
                        }

                    }
                    connecta.Close();
                    somaFunciona.Close();

                    //SOMA OS SALDOS DOS CLIENTES SOCIO

                    string getSomaSocio = "Select saldo from ClienteSocio";
                    SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                    SqlCommand cmdSocio = new SqlCommand(getSomaSocio, connection);
                    connection.Open();
                    SqlDataReader somaSocio = cmdSocio.ExecuteReader();
                    double[] recebeSocio = new double[50];
                    double somaSoc = 0;
                    while (somaSocio.Read())
                    {
                        for (int i = 0; i < recebe.Length; i++)
                        {
                            recebeSocio[i] = Convert.ToDouble(somaSocio["saldo"]);
                            for (int j = 0; j < recebe.Length; j++)
                            {
                                somaSoc += recebeSocio[i];

                            }

                        }

                    }
                    connection.Close();
                    somaSocio.Close();

                    //SOMA O SALDO DE TODOS FUNCIONARIOS
                    string getSomaFuncionarios = "Select saldo from Funcionarios";
                    SqlConnection connecte = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                    SqlCommand cmdSomaFun = new SqlCommand(getSomaFuncionarios, connecte);
                    connecte.Open();
                    SqlDataReader somaFun = cmdSomaFun.ExecuteReader();
                    double[] recebesomafun = new double[50];
                    double somafun = 0;
                    while (somaFun.Read())
                    {
                        for (int i = 0; i < recebesomafun.Length; i++)
                        {
                            recebesomafun[i] = Convert.ToDouble(somaFun["saldo"]);
                            for (int j = 0; j < recebe.Length; j++)
                            {
                                somafun += recebesomafun[i];

                            }

                        }

                    }
                    connecte.Close();
                    somaFun.Close();

                    //SOMA OS SALDOS DOS FORNECEDORES
                    string getSomaforne = "Select saldo from Fornecedores";
                    SqlConnection conni = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                    SqlCommand cmdfornecedores = new SqlCommand(getSomaforne, conni);
                    conni.Open();
                    SqlDataReader drfuncionario = cmdfornecedores.ExecuteReader();
                    double[] recebefornece = new double[50];
                    double somafornece = 0;
                    while (drfuncionario.Read())
                    {
                        for (int i = 0; i < recebefornece.Length; i++)
                        {
                            recebefornece[i] = Convert.ToDouble(drfuncionario["saldo"]);
                            for (int j = 0; j < recebefornece.Length; j++)
                            {
                                somafornece += recebefornece[i];

                            }

                        }

                    }
                    conni.Close();
                    drfuncionario.Close();

                    // SOMA AS AÇÕES DOS CLIENTES SOCIO
                    string AcoesSocio = "Select  QtdeAcoes from  ClienteSocio";
                    SqlConnection connectando = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");

                    SqlCommand cmdAcoesSocio = new SqlCommand(AcoesSocio, connectando);
                    connectando.Open();
                    SqlDataReader drAcoesSocio = cmdAcoesSocio.ExecuteReader();
                    double[] recebeAcoes = new double[100];
                    double somaAcoes = 0;
                    while (drAcoesSocio.Read())
                    {
                        for (int i = 0; i < recebe.Length; i++)
                        {
                            recebeAcoes[i] = Convert.ToDouble(drAcoesSocio["QtdeAcoes"]);
                            for (int j = 0; j < recebeAcoes.Length; j++)
                            {
                                somaAcoes += recebeAcoes[i];
                            }

                        }

                    }
                    
                    connectando.Close();
                    drAcoesSocio.Close();

                    double lucro = soma + somaSoc;
                    double prejuizo = somafun + somafornece;
                    double caixa = lucro - prejuizo;
                    somaAcoes = somaAcoes * 0.100;
                    caixa = caixa - somaAcoes;

                    if (caixa > 0)
                    {
                        Console.WriteLine(" O lucro foi de:{0}", caixa);
                        System.Threading.Thread.Sleep(5000);

                        //LIMPA O SALDO DE TODOS OS ENTES
                        
                        string limpaSaldoCliente = $"UPDATE Cliente  Set saldo = null"; 
                        SqlConnection conn001 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                        SqlCommand cmd01;
                        cmd01 = new SqlCommand(limpaSaldoCliente, conn001);

                        conn001.Open();
                        cmd01.ExecuteNonQuery();
                        conn001.Close();

                        string limpaClienteSocio01 = $"UPDATE ClienteSocio Set saldo = null";
                        SqlCommand cmd002;
                        cmd002 = new SqlCommand(limpaClienteSocio01, conn001);

                        conn001.Open();
                        cmd002.ExecuteNonQuery();
                        conn001.Close();

                        string limpaFuncionarios = $"UPDATE Funcionarios Set saldo = null ";
                        SqlCommand cmd003;
                        cmd003 = new SqlCommand(limpaFuncionarios, conn001);

                        conn001.Open();
                        cmd003.ExecuteNonQuery();
                        conn001.Close();

                        string limpaFornecedores = $"UPDATE Fornecedores Set saldo = null";
                        SqlCommand cmd004;
                        cmd004 = new SqlCommand(limpaFornecedores, conn001);

                        conn001.Open();
                        cmd004.ExecuteNonQuery();
                        conn001.Close();

                        //elimina a metade dos produtos
                        string meioprodutos = $"UPDATE Funcionarios SET QtdeMes = QtdeMes / 2 ";
                        SqlCommand cmd005;
                        cmd005 = new SqlCommand(limpaFuncionarios, conn001);

                        conn001.Open();
                        cmd005.ExecuteNonQuery();
                        conn001.Close();

                        Menu();
                    }
                    else
                    {
                        Console.WriteLine("Não houve lucro.");
                        Menu();
                    }
                    

                    break;

                case 7:
                    Console.WriteLine("\nObrigado por usar este programa. \nFinalizando programa");
                    System.Threading.Thread.Sleep(5000);

                    //LIMPA OS DADOS DE TODAS AS TABELAS AO FINALIZAR O PROG.PARA NÃO OCORRER A "DUPLICAÇÃO" DO ID da TABELA CLIENTE SOCIO(VISTO QUE FORAM DELIMITADOS O NUMERO DE CADA ENTE)
                    string limpaCliente = "DELETE from Cliente";
                    SqlConnection conne = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\fabio_000\source\repos\Fabiola.VersaoBD\EmpresaDados.mdf; Integrated Security = True");
                    SqlCommand cmd;
                    cmd = new SqlCommand(limpaCliente, conne);

                    conne.Open();
                    cmd.ExecuteNonQuery();
                    conne.Close();

                    string limpaClienteSocio = "DELETE from ClienteSocio";
                    SqlCommand cmdClienteSocio;
                    cmdClienteSocio = new SqlCommand(limpaClienteSocio, conne);

                    conne.Open();
                    cmdClienteSocio.ExecuteNonQuery();
                    conne.Close();

                    Environment.Exit(0);
                    return false;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inexistente, selecione entre as opções de 1 a 7.");
                    return true;

            }
            return false;

        }
        


    }

    
}
