namespace ChessSharp;
using Tabuleiro;

public class Tela
{
    public static void ImprimirTabuleiro(TabuleiroJogo tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            for (int j = 0; j < tab.Colunas; j++)
            {
                if (tab.ReturnPeca(i, j) == null)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(tab.ReturnPeca(i,j) + " ");
                }
            }

            Console.WriteLine();
        }
    }
}