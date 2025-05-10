using ChessSharp.Tabuleiro;

namespace ChessSharp.Xadrez;

public class Torre : Peca
{
    public Torre(TabuleiroJogo tabuleiroJogo, Cor cor) : base(tabuleiroJogo, cor)
    {
    }

    public override string ToString()
    {
        return "T";
    }
}