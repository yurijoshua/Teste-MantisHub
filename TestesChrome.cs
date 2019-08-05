using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Threading;

namespace TesteBase2
{
    public class TestesChrome : Driver
    {
        string filePathLogin = PathDataDrivenGet.PathDataDriven("DataDrivenLogin.xlsx");
        string filePathCat = PathDataDrivenGet.PathDataDriven("DataDrivenCat.xlsx");
        
        [Test]
        public void A_AcessarConta()
        {
            //Firefox //IE
            SetUp("Chrome", "https://mantis-prova.base2.com.br/");
            PageObjects PagObj = new PageObjects(_driver);
            ExcelUtil util = new ExcelUtil();
            util.PopulateInCollection(filePathLogin);
            PagObj.Login_Senha_Acessar(util.ReadData(1, "Column0"), util.ReadData(1, "Column1"));
            PagObj.AcessarAccount();
            try
            {
                PagObj.VerificarConta(util.ReadData(1, "Column0"), util.ReadData(1, "Column2"), util.ReadData(1, "Column3"), util.ReadData(1, "Column4"));
            }
            catch
            {
                PagObj.Screenshot("ErrorVerifAccount");   
            }
            PagObj.Screenshot("AcessarConta");   
        }


        [Test]
        public void B_CriarCateg()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            PageObjects PagObj = new PageObjects(_driver);
            ExcelUtil util = new ExcelUtil();
            util.PopulateInCollection(filePathCat);
            for (int r = 1; r <= 5; r++)
            {
                PagObj.AcessarManager();
                PagObj.AcessarProj();
                PagObj.CriarCateg(util.ReadData(r, "Column0"));
                PagObj.AcessarCateg(util.ReadData(r, "Column0"));
                PagObj.Screenshot("CriarCateg");
                PagObj.AcessarProj();
                PagObj.ScreenshotTabCat("ValidElement", util.ReadData(r, "Column0"));
            }
        }

      
        [Test]
        public void C_AlterarCat()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            PageObjects PagObj = new PageObjects(_driver);
            ExcelUtil util = new ExcelUtil();
            util.PopulateInCollection(filePathCat);
            for (int r = 1; r <= 5; r++)
            {
                PagObj.AcessarManager();
                PagObj.AcessarProj();
                PagObj.AcessarCateg(util.ReadData(r, "Column0"));
                PagObj.AltCategNome(util.ReadData(r, "Column1"));
                PagObj.AltAtribCateg(util.ReadData(r, "Column2"));
                //Obj public
                PagObj.UpdtCat.Submit();
                PagObj.Screenshot("ConfirmAltCat");
                //ScreenShot para determinado elemento da tabela Categorias
                PagObj.ScreenshotTabCat("ValidAltElement", util.ReadData(r, "Column1"));
            }
        }


        [Test]
        public void D_DeletarCateg()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            PageObjects PagObj = new PageObjects(_driver);
            ExcelUtil util = new ExcelUtil();
            util.PopulateInCollection(filePathCat);
            for (int r = 1; r <= 5; r++)
            {
                PagObj.AcessarManager();
                PagObj.AcessarProj();
                Random n = new Random();
                //Testar ambos caminhos para Delete, Acessando o Edit e Deletando, ou Deletando direto da Tabela
                if ( n.Next(0, 1) == 0)
                {
                    PagObj.AcessarCateg(util.ReadData(1, "Column1"));
                }
                PagObj.DeletarCateg(util.ReadData(r, "Column1"));
                PagObj.Screenshot("ConfirmDeletCat");
            }
            TearDown();
        }
    }
}
