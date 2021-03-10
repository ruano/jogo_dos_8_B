using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp7
{
    class Tabuleiro
    {
        private readonly char[,] arrayTabuleiro;
        private IList<char> listaLetrasSorteadasTabuleiroOrdenadas;
        private const char VAZIO = '\0';
        private readonly IList<char> letras = new List<char>
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z',
            };

        public Tabuleiro()
        {
            arrayTabuleiro = new char[3, 3];
            listaLetrasSorteadasTabuleiroOrdenadas = new List<char>();

            Embaralhar();
        }

        public int Movimentos { get; private set; }

        private void Embaralhar()
        {
            PreencherListaDasLetrasSorteadas();
            PreencherArrayTabuleiro();
            OrdenarListaDasPalavrasSorteadas();
        }

        public void MostrarTabuleiro()
        {
            Console.Clear();
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    Console.Write(arrayTabuleiro[indexLinha, indexColuna]);
                }
                Console.WriteLine();
            }
        }

        public bool OrdenouTabuleiro()
        {
            int indice = 0;
            for (int indiceLinha = 0; indiceLinha < arrayTabuleiro.GetLength(0); indiceLinha++)
            {
                for (int indiceColuna = 0; indiceColuna < arrayTabuleiro.GetLength(1); indiceColuna++)
                {
                    if (arrayTabuleiro[indiceLinha, indiceColuna] != listaLetrasSorteadasTabuleiroOrdenadas[indice])
                        return false;

                    indice++;
                }
            }

            //for (int indice = 0; indice < listaLetrasSorteadasTabuleiroOrdenadas.Count; indice++)
            //{
            //    if (listaLetrasSorteadasTabuleiroOrdenadas[indice] != listaVerificacao[indice])
            //        return false;
            //}

            return true;
        }

        public void MovimentarLetra(char letra)
        {
            Tuple<int, int> coordenadaLetraMovimentada = arrayTabuleiro.CoordinatesOf(letra);

            if (coordenadaLetraMovimentada.Item1 == -1 && coordenadaLetraMovimentada.Item2 == -1)
                return;

            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                int coordenadaColunaAnalise = coordenadaLetraMovimentada.Item2 - 1;
                if (coordenadaColunaAnalise > -1)
                {
                    if (indexLinha == coordenadaLetraMovimentada.Item1)
                    {
                        if (arrayTabuleiro[indexLinha, coordenadaColunaAnalise] == VAZIO)
                        {
                            arrayTabuleiro[indexLinha, coordenadaColunaAnalise] = arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2];
                            arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2] = VAZIO;
                            Movimentos++;
                            return;
                        }
                    }
                }

                coordenadaColunaAnalise = coordenadaLetraMovimentada.Item2 + 1;
                if (coordenadaColunaAnalise < arrayTabuleiro.GetLength(1))
                {
                    if (indexLinha == coordenadaLetraMovimentada.Item1)
                    {
                        if (arrayTabuleiro[indexLinha, coordenadaColunaAnalise] == VAZIO)
                        {
                            arrayTabuleiro[indexLinha, coordenadaColunaAnalise] = arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2];
                            arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2] = VAZIO;
                            Movimentos++;
                            return;
                        }
                    }
                }

                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    int coordenadaLinhaAnalise = coordenadaLetraMovimentada.Item1 - 1;
                    if (coordenadaLinhaAnalise > -1)
                    {
                        if (indexColuna == coordenadaLetraMovimentada.Item2)
                        {
                            if (arrayTabuleiro[coordenadaLinhaAnalise, indexColuna] == VAZIO)
                            {
                                arrayTabuleiro[coordenadaLinhaAnalise, indexColuna] = arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2];
                                arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2] = VAZIO;
                                Movimentos++;
                                return;
                            }
                        }
                    }

                    coordenadaLinhaAnalise = coordenadaLetraMovimentada.Item1 + 1;
                    if (coordenadaLinhaAnalise < arrayTabuleiro.GetLength(0))
                    {
                        if (indexColuna == coordenadaLetraMovimentada.Item2)
                        {
                            if (arrayTabuleiro[coordenadaLinhaAnalise, indexColuna] == VAZIO)
                            {
                                arrayTabuleiro[coordenadaLinhaAnalise, indexColuna] = arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2];
                                arrayTabuleiro[coordenadaLetraMovimentada.Item1, coordenadaLetraMovimentada.Item2] = VAZIO;
                                Movimentos++;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void PreencherListaDasLetrasSorteadas()
        {
            Random random = new Random();

            int linhas = arrayTabuleiro.GetLength(0);
            int colunas = arrayTabuleiro.GetLength(1);

            int qtdLetrasParaSortear = linhas * colunas - 1;

            for (int index = 0; index < qtdLetrasParaSortear; index++)
            {
                int indexLetras = random.Next(0, letras.Count - 1);

                while (listaLetrasSorteadasTabuleiroOrdenadas.Contains(letras[indexLetras]))
                {
                    indexLetras = random.Next(0, letras.Count - 1);
                }

                listaLetrasSorteadasTabuleiroOrdenadas.Add(letras[indexLetras]);
            }

            listaLetrasSorteadasTabuleiroOrdenadas.Add(VAZIO);
        }

        private void PreencherArrayTabuleiro()
        {
            int indexLista = 0;
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    arrayTabuleiro[indexLinha, indexColuna] = listaLetrasSorteadasTabuleiroOrdenadas[indexLista];
                    indexLista++;
                }
            }
        }

        private void OrdenarListaDasPalavrasSorteadas()
        {
            listaLetrasSorteadasTabuleiroOrdenadas = listaLetrasSorteadasTabuleiroOrdenadas.OrderBy(letra => (int)letra).ToList();
            listaLetrasSorteadasTabuleiroOrdenadas.RemoveAt(0);
            listaLetrasSorteadasTabuleiroOrdenadas.Add(VAZIO);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro();

            do
            {
                tabuleiro.MostrarTabuleiro();
                Console.WriteLine("------------");
                Console.WriteLine($"Movimentos: {tabuleiro.Movimentos}");
                Console.WriteLine("Informe a letra:");

                string letra = Console.ReadLine();

                tabuleiro.MovimentarLetra(Convert.ToChar(letra.ToUpper()));
            } while (!tabuleiro.OrdenouTabuleiro());

            Console.WriteLine("Tabuleiro ordenado!");

            Console.ReadKey();

        }
    }

    public static class ExtensionMethods
    {
        public static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int linhas = matrix.GetLength(0);
            int colunas = matrix.GetLength(1);

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (matrix[i, j].Equals(value))
                        return Tuple.Create(i, j);
                }
            }

            return Tuple.Create(-1, -1);
        }
    }
}