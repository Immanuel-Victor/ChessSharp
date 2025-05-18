namespace ChessSharp.Tabuleiro;

public abstract class Peca
{
    public Posicao? Posicao { get; set; }
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

    protected bool PodeMover(Posicao pos)
    {
        Peca p = TabuleiroJogo.ReturnPeca(pos);
        return p == null || p.Cor == Cor; 
    }
    
    public void IncrementarMovimentos()
    {
        MovesMade++;
    }

    public abstract bool[,] MovimentosPossiveis();
}