using ChessSharp.Tabuleiro;
using ChessSharp.Tabuleiro.Exeptions;
using ChessSharp.Xadrez;
using FluentAssertions;

namespace ChessSharp.UnitTests.Tabuleiro;

public class TabuleiroJogoTests
{
    private readonly TabuleiroJogo _tabuleiroXadres;

    public TabuleiroJogoTests()
    {
        _tabuleiroXadres = new TabuleiroJogo(8, 8);
    }
    [Theory]
    [InlineData(1, 1, 0 ,0 )]
    public void TabuleiroJogo_ReturnPeca_ShouldReturnNull(int linha, int coluna, int pecaPosX, int pecaPosY)
    {
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        var pos = new Posicao(pecaPosY, pecaPosY);
        
        //Act
        
        var result = tab.ReturnPeca(pos);
        
        //Assert
        
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(1, 1, 0 ,0 )]
    public void TabuleiroJogo_ColocarPeca_ShouldPlacePecaInTheBoard(int linha, int coluna, int pecaPosX, int pecaPosY)
    {
        
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        var pos = new Posicao(pecaPosY, pecaPosY);
        
        //Act

        tab.ColocarPeca(new Peca(tab, Cor.Branca), pos);
        
        var result = tab.ReturnPeca(pos);
        
        //Assert
        result.Should().NotBeNull();
        result.Posicao.Should().BeEquivalentTo(new Posicao(pecaPosY, pecaPosY));
    }

    [Theory]
    [InlineData(2, 2)]
    public void TabuleiroJogo_ReturnPeca_ShouldReturnPecaAndUpdatePecaPosition(int linha, int coluna)
    {
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        var peca = new Peca(tab, Cor.Branca);
        var newPos = new Posicao(1, 1);
        
        //Act

        tab.ColocarPeca(peca, newPos);
        
        var result = tab.ReturnPeca(newPos);
        
        //Assert
        result.Should().NotBeNull();
        peca.Posicao.Linha.Should().Be(newPos.Linha);
        peca.Posicao.Coluna.Should().Be(newPos.Coluna);
    }

    [Theory]
    [InlineData(8, 8, -1, 0)]
    [InlineData(8, 8, 9, 0)]
    [InlineData(8, 8, 0, -1)]
    [InlineData(8, 8, 0, 9)]
    public void TabuleiroJogo_PosicaoValida_ShouldReturnFalseWhenValueIsNotValid(int sizeX, int sizeY, int posX, int posY)
    {
        //Arrange
        var tab = new TabuleiroJogo(sizeX, sizeY);
        //Act
        var result = tab.PosicaoValida(new Posicao(posX, posY));
        //Assert
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(8, 8, 0, 0)]
    [InlineData(8, 8, 1, 3)]
    [InlineData(8, 8, 7, 7)]
    public void TabuleiroJogo_PosicaoValida_ShouldReturnTrueWhenAllMembersAreValid(int sizeX, int sizeY, int posX, int posY)
    {
        //Arrange
        var tab = new TabuleiroJogo(sizeX, sizeY);
        //Act
        var result = tab.PosicaoValida(new Posicao(posX, posY));
        //Assert
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(8, 8, -1, 0)]
    [InlineData(8, 8, 9, 0)]
    [InlineData(8, 8, 0, -1)]
    [InlineData(8, 8, 0, 9)]
    public void TabuleiroJogo_ValidarPosicão_ShouldThrowException(int sizeX, int sizeY, int posX, int posY)
    {
        //Arrange
        var tab = new TabuleiroJogo(sizeX, sizeY);
        
        //Act
        
        Action act = () =>tab.ValidarPosicão(new Posicao(posX, posY));
        
        //Assert
        act.Should().Throw<TabuleiroException>();
        act.Should().Throw<TabuleiroException>().WithMessage("Posição inválida!");
    }

    [Theory]
    [InlineData( 0, 0)]
    [InlineData( 7, 7)]
    public void TabuleiroJogo_ExistePeca_ShouldReturnTrue(int posX, int posY)
    {
        //Arrange

        Rei rei = new Rei(_tabuleiroXadres, Cor.Branca);
        
        //Act
        
        _tabuleiroXadres.ColocarPeca(rei, new Posicao(posX, posY));
        var result = _tabuleiroXadres.ExistePeca(new Posicao(posX, posY));

        //Assert
        
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData( 0, 0)]
    [InlineData( 6, 6)]
    public void TabuleiroJogo_ExistePeca_ShouldReturnFalse(int posX, int posY)
    {
        //Arrange

        Rei rei = new Rei(_tabuleiroXadres, Cor.Branca);
        
        //Act
        
        _tabuleiroXadres.ColocarPeca(rei, new Posicao(posX, posY));
        var result = _tabuleiroXadres.ExistePeca(new Posicao((posX+1), (posY+1)));

        //Assert
        
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData( 0, 0)]
    [InlineData( 7, 7)]
    public void TabuleiroJogo_ColocarPeca_ShouldThrowException(int posX, int posY)
    {
        //Arrange

        Rei rei = new Rei(_tabuleiroXadres, Cor.Branca);
        Torre torre = new Torre(_tabuleiroXadres, Cor.Branca);
        
        //Act
        
        _tabuleiroXadres.ColocarPeca(rei, new Posicao(posX, posY));
        Action act = () => _tabuleiroXadres.ColocarPeca(torre, new Posicao(posX, posY));

        //Assert

        act.Should().Throw<TabuleiroException>();
    }
    
}