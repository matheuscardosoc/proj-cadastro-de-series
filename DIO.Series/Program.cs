using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine(" ==== Opção digitada inválida! Tente novamente. ==== ");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ExcluirSerie()
        {
            try
            {
                Console.WriteLine("=>  !Exclusão de Série!  <=");
                Console.WriteLine();

                Console.Write("= Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("= Quer mesmo excluir essa série?");
                Console.Write("Y para sim / N para não: ");
                string escolha = Console.ReadLine().ToUpper();
                
                if (escolha == "Y")
                {
                    repositorio.Exclui(indiceSerie);
                    Console.WriteLine();
                    Console.WriteLine("==Série excluida com sucesso!==");
                }
                else
                    Console.WriteLine("= Exclusão cancelada! =");
            }
            catch (Exception)
            {
                Console.WriteLine(" ==== ID ou Escolha digitada de forma errada! Tente novamente. ====");
                Console.WriteLine();
            }
        }

        private static void VisualizarSerie()
        {
            try
            {
                Console.WriteLine("=>   Visualização da Série   <=");
                Console.WriteLine();

                Console.Write("= Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                Console.WriteLine();

                var serie = repositorio.RetornaPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            catch (Exception)
            {
                Console.Write(" ==== ID não encontrado, ou digitado de forma errada! ====");
                Console.WriteLine();
            }
        }

        private static void AtualizarSerie()
        {
            try
            {
                Console.Write("=  Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("= {0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("= Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("= Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("= Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("= Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Console.WriteLine();

                Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Atualiza(indiceSerie, atualizaSerie);
            }
            catch (Exception)
            {
                Console.WriteLine(" ==== ID não encontrado, ou digitado de forma errada! ====");
                Console.WriteLine();
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("  =>  Listar séries  <=");
            Console.WriteLine();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("= Nenhuma série cadastrada.");
                Console.WriteLine();
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("= #ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "==Excluido==" : ""));
            }
        }

        private static void InserirSerie()
        {
            try
            {
                Console.WriteLine("  =>  Inserir nova série  <=");

                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("= {0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("= Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("= Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("= Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("= Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Console.WriteLine();

                Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Insere(novaSerie);

                Console.WriteLine("=== Série cadastrada com sucesso! ===");
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Dado inserido de forma errada! Tente novamente!");
            }
        }

        private static string ObterOpcaoUsuario()
        {

            Console.WriteLine("+============================+");
            Console.WriteLine("||DIO Séries a seu dispor!!!||");
            Console.WriteLine("||Informe a opção desejada: ||");

            Console.WriteLine("||1 - Listar séries         ||");
            Console.WriteLine("||2 - Inserir nova série    ||");
            Console.WriteLine("||3 - Atualizar série       ||");
            Console.WriteLine("||4 - Excluir série         ||");
            Console.WriteLine("||5 - Visualizar série      ||");
            Console.WriteLine("||C - Limpar Tela           ||");
            Console.WriteLine("||X - Sair                  ||");
            Console.WriteLine("+============================+");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
