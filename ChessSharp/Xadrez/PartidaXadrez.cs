using ChessSharp.Tabuleiro;
using ChessSharp.Tabuleiro.Exeptions;

namespace ChessSharp.Xadrez;

public class PartidaXadrez
{
    public TabuleiroJogo Tabuleiro { get; private set; }
    public bool Finished { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }

    public PartidaXadrez()
    {
        Tabuleiro = new TabuleiroJogo(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        ColocarPecas();
    }

    public void ExecutarMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tabuleiro.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(p, destino);
    }

    public void ValidarOrigem(Posicao origem)
    {
        if (Tabuleiro.ReturnPeca(origem) == null)
        {
            throw new TabuleiroException("Não existe peça nessa posição");
        }

        if (JogadorAtual != Tabuleiro.ReturnPeca(origem).Cor)
        {
            throw new TabuleiroException("A peça escolhida não é sua");
        }

        if (!Tabuleiro.ReturnPeca(origem).MovimentosDisponiveis())
        {
            throw new TabuleiroException("Essa peça não possui movimentos disponiveis");
        }
    }

    public void ValidarDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.ReturnPeca(origem).MovimentoPermitido(destino))
        {
            throw new TabuleiroException("Posição de destino inválida");
        }
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        ExecutarMovimento(origem, destino);
        Turno++;
    }

    private void MudaJogador()
    {
        JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
    }

    public void ColocarPecas()
    {
        Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
    }
}