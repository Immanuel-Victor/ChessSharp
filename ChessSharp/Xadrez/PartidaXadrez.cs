using ChessSharp.Tabuleiro;

namespace ChessSharp.Xadrez;

public class PartidaXadrez
{
    public TabuleiroJogo Tabuleiro { get; private set; }
    public bool Finished { get; private set; }
    private int _turno;
    private Cor _jogadorAtual;

    public PartidaXadrez()
    {
        Tabuleiro = new TabuleiroJogo(8, 8);
        _turno = 1;
        _jogadorAtual = Cor.Branca;
        ColocarPecas();
    }

    public void ExecutarMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tabuleiro.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(p, destino);
    }

    public void ColocarPecas()
    {
        Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
    }
}