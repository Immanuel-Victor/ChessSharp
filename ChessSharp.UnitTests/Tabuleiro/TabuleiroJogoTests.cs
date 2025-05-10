using ChessSharp.Tabuleiro;
using FluentAssertions;

namespace ChessSharp.UnitTests.Tabuleiro;

public class TabuleiroJogoTests
{
    [Theory]
    [InlineData(1, 1, 0 ,0 )]
    public void TabuleiroJogo_ReturnPeca_ShouldReturnNull(int linha, int coluna, int pecaPosX, int pecaPosY)
    {
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        
        //Act
        
        var result = tab.ReturnPeca(pecaPosX, pecaPosY);
        
        //Assert
        
        result.Should().BeNull();
    }

    [InlineData(1, 1, 0 ,0 )]
    [Theory]
    public void TabuleiroJogo_ColocarPeca_ShouldPlacePecaInTheBoard(int linha, int coluna, int pecaPosX, int pecaPosY)
    {
        
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        
        //Act

        tab.ColocarPeca(new Peca(tab, Cor.Branca), new Posicao(pecaPosY, pecaPosY));
        
        var result = tab.ReturnPeca(pecaPosX, pecaPosY);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Peca(tab, Cor.Branca));
    }

    [InlineData(2, 2)]
    [Theory]
    public void TabuleiroJogo_ReturnPeca_ShouldReturnPecaAndUpdatePecaPosition(int linha, int coluna)
    {
        //Arrange
        
        var tab = new TabuleiroJogo(linha, coluna);
        var peca = new Peca(tab, Cor.Branca);
        var newPos = new Posicao(1, 1);
        
        //Act

        tab.ColocarPeca(peca, newPos);
        
        var result = tab.ReturnPeca(newPos.Linha, newPos.Coluna);
        
        //Assert
        result.Should().NotBeNull();
        peca.Posicao.Linha.Should().Be(newPos.Linha);
        peca.Posicao.Coluna.Should().Be(newPos.Coluna);
    }
    
    
}