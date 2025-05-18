using ChessSharp.Xadrez;

namespace ChessSharp;
using Tabuleiro;

public class Tela
{
    public static void ImprimirTabuleiro(TabuleiroJogo tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write((8 - i) + " ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                ImprimirPeca(tab.ReturnPeca(new Posicao(i, j)));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

    public static void ImprimirTabuleiro(TabuleiroJogo tab, bool[,] posicoesPossiveis)
    {
        
        ConsoleColor backgroundOriginal = Console.BackgroundColor;
        ConsoleColor alteredBackground = ConsoleColor.DarkGray;
        
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write((8 - i) + " ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                Console.BackgroundColor = posicoesPossiveis[i, j] ? alteredBackground : backgroundOriginal;
                ImprimirPeca(tab.ReturnPeca(new Posicao(i, j)));
                Console.BackgroundColor = backgroundOriginal;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

    public static void ImprimirPartida(PartidaXadrez partida)
    {
        ImprimirTabuleiro(partida.Tabuleiro);
        Console.WriteLine();
        ImprimirPecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("Turno: " + partida.Turno);
        if (!partida.Terminada)
        {
            Console.WriteLine("Aguardando jogada da cor: " + partida.JogadorAtual);
            if (partida.Xeque)
            {
                Console.WriteLine("Você está em Cheque");
            }
        }
        else
        {
            Console.WriteLine("Xeque Mate");
            Console.WriteLine($"Vencedor: ${partida.JogadorAtual}");
        }

    }

    public static void ImprimirPecasCapturadas(PartidaXadrez partida)
    {
        Console.WriteLine("Peças Capturadas: ");
        Console.Write("Brancas ");
        ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
        Console.Write("Pretas: ");
        ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
    }

    public static void ImprimirConjunto(HashSet<Peca> pecas)
    {
        Console.Write("[");
        foreach (Peca p in pecas)
        {
            Console.Write(p + " ");
        }
        Console.WriteLine("]");
    }

    public static void ImprimirPeca(Peca peca)
    {
        if (peca == null)
        {
            Console.Write("- ");
        }
        else
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }

    public static PosicaoXadrez LerPosicaoXadrez()
    {
        string s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s.Substring(1));
        return new PosicaoXadrez(coluna, linha);
    }
}