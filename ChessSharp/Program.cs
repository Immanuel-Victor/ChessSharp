using ChessSharp.Tabuleiro;
using ChessSharp.Xadrez;

namespace ChessSharp;

class Program
{
    static void Main(string[] args)
    {
        PartidaXadrez partida = new PartidaXadrez();

        while (!partida.Terminada)
        {
            try
            {
                Console.Clear();
                Tela.ImprimirPartida(partida);
            
                Console.WriteLine();
                Console.Write("Origem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                partida.ValidarOrigem(origem);

                bool[,] posicoesPossiveis = partida.Tabuleiro.ReturnPeca(origem).MovimentosPossiveis();
            
                Console.Clear();
                Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
                
                Console.Write("Destino: ");
                Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                partida.ValidarDestino(origem, destino);
            
                partida.RealizaJogada(origem, destino);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }   
        
        Tela.ImprimirTabuleiro(partida.Tabuleiro);
    }
}