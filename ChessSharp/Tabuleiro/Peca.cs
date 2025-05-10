namespace Tabuleiro;

public class Peca
{
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int MovesMade { get; protected set; }
    public Tabuleiro Tabuleiro { get; protected set; }

    public Peca(Posicao posicao, Tabuleiro tabuleiro, Cor cor)
    {
        Posicao = posicao;
        Cor = cor;
        Tabuleiro = tabuleiro;
        MovesMade = 0;
    }
}