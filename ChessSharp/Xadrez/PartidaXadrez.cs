﻿using ChessSharp.Tabuleiro;
using ChessSharp.Tabuleiro.Exeptions;

namespace ChessSharp.Xadrez;

public class PartidaXadrez
{
    public TabuleiroJogo Tabuleiro { get; private set; }
    public bool Terminada { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    private HashSet<Peca> _pecas;
    private HashSet<Peca> _capturadas;
    public bool Xeque { get; private set; }
    public  Peca VulneravelEnPassant { get; private set; }

    public PartidaXadrez()
    {
        Tabuleiro = new TabuleiroJogo(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        _pecas = new HashSet<Peca>();
        _capturadas = new HashSet<Peca>();
        Xeque = false;
        ColocarPecas();
    }

    public Peca ExecutarMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tabuleiro.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(p, destino);
        if (pecaCapturada != null)
        {
            _capturadas.Add(pecaCapturada);
        }
        
        // Jogada Especial - Roque Pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(destino.Linha, destino.Coluna + 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.IncrementarMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }
        
        // Jogada Especial - Roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(destino.Linha, destino.Coluna - 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.IncrementarMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }

        return pecaCapturada;
    }
    
    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        
        Peca pecaCapturada = ExecutarMovimento(origem, destino);
        Peca p = Tabuleiro.ReturnPeca(destino);
        if (EmCheque(JogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Voce não pode se Colocar em cheque");
        }
        
        //Promoção

        if (p is Peao)
        {
            if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
            {
                p = Tabuleiro.RetirarPeca(destino);
                _pecas.Remove(p);
                Peca dama = new Dama(Tabuleiro, p.Cor);
                Tabuleiro.ColocarPeca(dama, destino);
                _pecas.Add(dama);
            }
        }
        
        Xeque = EmCheque(Adversario(JogadorAtual));

        if (XequeMate(Adversario(JogadorAtual)))
        {
            Terminada = true;
        }
        else
        {
            Turno++;
            MudaJogador();
        }
        //En passant
        if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            VulneravelEnPassant = p;
        }
        else
        {
            VulneravelEnPassant = null;
        }

        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posP;
                posP = p.Cor == Cor.Branca
                    ? new Posicao(destino.Linha + 1, destino.Coluna)
                    : posP = new Posicao(destino.Linha - 1, destino.Coluna);
                pecaCapturada = Tabuleiro.RetirarPeca(posP);
                _capturadas.Add(pecaCapturada);
            }
        }
    }

    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = Tabuleiro.RetirarPeca(destino);
        p.DecrementarMovimentos();
        if (pecaCapturada != null)
        {
            Tabuleiro.ColocarPeca(pecaCapturada, destino);
            _capturadas.Remove(pecaCapturada);
        }
        Tabuleiro.ColocarPeca(p, origem);
        
        // Jogada Especial - Roque Pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(destino.Linha, destino.Coluna + 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.DecrementarMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }
        
        // Jogada Especial - Roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(destino.Linha, destino.Coluna - 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.DecrementarMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }
        
        // En Passant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = Tabuleiro.RetirarPeca(destino);
                Posicao posP;
                posP = p.Cor == Cor.Branca
                    ? new Posicao(3, destino.Coluna)
                    : posP = new Posicao(4, destino.Coluna);
            }
        }
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

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca p in _capturadas)
        {
            if (p.Cor == cor)
            {
                aux.Add(p);
            }
        }
        return aux;
    }
    
    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca p in _pecas)
        {
            if (p.Cor == cor)
            {
                aux.Add(p);
            }
        }
        
        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }

    private Cor Adversario(Cor cor)
    {
        if (cor == Cor.Branca)
        {
            return Cor.Preta;
        }
        return Cor.Branca;
    }

    private Peca? ReturnRei(Cor cor)
    {
        foreach (Peca p in PecasEmJogo(cor))
        {
            if (p is Rei)
            {
                return p;
            }   
        }
        return null;
    }

    public bool EmCheque(Cor cor)
    {
        Peca rei = ReturnRei(cor);
        if (rei == null)
        {
            throw new TabuleiroException("Sem rei no tabuleiro");
        }
        
        foreach (Peca p in PecasEmJogo(Adversario(cor)))
        {
            bool[,] mat = p.MovimentosPossiveis();
            if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
            {
                return true;
            }
        }

        return false;
    }

    public bool XequeMate(Cor cor)
    {
        if (!EmCheque(cor))
        {
            return false;
        }

        foreach (Peca p in PecasEmJogo(cor))
        {
            bool[,] mat = p.MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        Posicao origem = p.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = ExecutarMovimento(origem, destino);
                        bool cheque = EmCheque(cor);
                        DesfazMovimento(origem, destino, pecaCapturada);
                        if (!cheque)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }
    
    public void ValidarDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.ReturnPeca(origem).MovimentoPermitido(destino))
        {
            throw new TabuleiroException("Posição de destino inválida");
        }
    }
    
    private void MudaJogador()
    {
        JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
    }

    public void ColocarPecas()
    {
        //Initialize White Board
        ColocarNovaPeca(new PosicaoXadrez('a', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('b', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('c', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('d', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('e', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('f', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('g', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('h', 2), new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('a', 1), new Torre(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('b', 1), new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('c', 1), new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('d', 1), new Dama(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('e', 1), new Rei(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca(new PosicaoXadrez('f', 1), new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('g', 1), new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca(new PosicaoXadrez('h', 1), new Torre(Tabuleiro, Cor.Branca));
        
        //Initialize Black Board
        ColocarNovaPeca(new PosicaoXadrez('a', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('b', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('c', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('d', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('e', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('f', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('g', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('h', 7), new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('a', 8), new Torre(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('b', 8), new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('c', 8), new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('d', 8), new Dama(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('e', 8), new Rei(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca(new PosicaoXadrez('f', 8), new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('g', 8), new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca(new PosicaoXadrez('h', 8), new Torre(Tabuleiro, Cor.Preta));
    }

    public void ColocarNovaPeca(PosicaoXadrez posicaoPeca, Peca peca)
    {
        Tabuleiro.ColocarPeca(peca, posicaoPeca.ToPosicao());
        _pecas.Add(peca);
    }
}