namespace ChessSharp.Tabuleiro;

public class Peca
{
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int MovesMade { get; protected set; }
    public TabuleiroJogo TabuleiroJogo { get; protected set; }

    public Peca(TabuleiroJogo tabuleiroJogo, Cor cor)
    {
        Posicao = null;
        Cor = cor;
        TabuleiroJogo = tabuleiroJogo;
        MovesMade = 0;
    }
}