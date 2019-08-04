using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace TesteBase2
{
    class PageObjects 
    {
        private IWebDriver _driver;
        private WebDriverWait wait;
        int contador = 1;

        public PageObjects(IWebDriver driver)
        {
            this._driver = driver;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            PageFactory.InitElements(_driver, this);
        }
        
        //LOGIN
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[2]/td[2]/input")]
        private IWebElement Login;
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[3]/td[2]/input")]
        private IWebElement Senha;
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[6]/td/input")]
        private IWebElement BtnLogar;

        //Account
        [FindsBy(How = How.XPath, Using = "/html/body/table[2]/tbody/tr/td[1]/a[8]")]
        private IWebElement BtnAccount;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/table/tbody/tr[2]/td[2]")]
        private IWebElement Username;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/table/tbody/tr[5]/td[2]/input")]
        private IWebElement Email;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/table/tbody/tr[6]/td[2]/input")]
        private IWebElement RealName;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/table/tbody/tr[7]/td[2]")]
        private IWebElement AccessLvl;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/table/tbody/tr[8]/td[2]")]
        private IWebElement ProjAccessLvl;

        //MANAGER Project
        [FindsBy(How = How.XPath, Using = "/html/body/table[2]/tbody/tr/td[1]/a[7]")]
        private IWebElement BtnManager;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/p/span[1]/a")]
        private IWebElement BtnProjects;
        //CATEGORIAS
        [FindsBy(How = How.XPath, Using = "/html/body/a/div/table/tbody")]
        private IWebElement TabCat;
        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/form/input[4]")]
        private IWebElement BtnMenuCatDel;
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/input[5]")]
        private IWebElement ConfirmCatDel;
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[2]/td[2]/input")]
        private IWebElement InptAltCat;
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[3]/td[2]/select")]
        private IWebElement SelectCat;
        //MENU BUTTON CATEGORIAS - PUBLIC
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/form/table/tbody/tr[4]/td[2]/input")]
        public IWebElement UpdtCat;

        //LOGAR
        public void Login_Senha_Acessar(string val1, string val2)
        {
            Login.SendKeys(val1);
            Senha.SendKeys(val2);
            BtnLogar.Click();
        }

        //ACESSAR MANAGER
        public void AcessarManager()
        {
            BtnManager.Click();
        }
        public void AcessarProj()
        {
            BtnProjects.Click();
        }

        //ACESSAR CONTA
        public void AcessarAccount()
        {
            BtnAccount.Click();
        }

        //VERIFICAR CONTA
        public void VerificarConta(string vlr1, string vlr2, string vlr3, string vlr4)
        {
            Assert.AreEqual(Username.Text, vlr1);
            Assert.AreEqual(Email.GetAttribute("value"), vlr2);
            Assert.AreEqual(RealName.GetAttribute("value"), vlr3);
            Assert.AreEqual(AccessLvl.Text, vlr4);
            Console.WriteLine("Username: " + Username.Text + " Email: " + Email.GetAttribute("value") + " RealName: " + RealName.GetAttribute("value") + " AccessLvl: " + AccessLvl.Text + " ProjAccessLvl: " + ProjAccessLvl.Text);
            Console.WriteLine("Username: " + vlr1 + " Email: " + vlr2 + " RealName: " + vlr3 + " AccessLvl: " + vlr4 + " ProjAccessLvl: " + ProjAccessLvl.Text);
        }

        //CATEGORIAS - CRIAR
        public void CriarCateg(string vlr1)
        {
            var rows = TabCat.FindElements(By.TagName("tr")).Count;
            for (int c = 1; c <= rows; c++)
            {
                IWebElement tdElement = TabCat.FindElement(By.XPath("//tr[" + c + "]/td[1]"));
                string td = tdElement.GetAttribute("innerHTML");
                Console.WriteLine(td+"td" + c);
                if (td.Contains("<form"))
                {
                    tdElement.FindElement(By.XPath("//td/form/input[3]")).SendKeys(vlr1);
                    tdElement.FindElement(By.XPath("//td/form/input[4]")).Submit();
                    break;
                }
            }
        }

        //ACESSAR MENU DE CATEGORIA ESPECIFICA
        public void AcessarCateg(string vlr1)
        {
            var rows = TabCat.FindElements(By.TagName("tr")).Count;
            for (int c = 1; c <= rows; c++)
            {
                IWebElement tdElement = TabCat.FindElement(By.XPath("//tr[" + c + "]/td[1]"));
                string td = tdElement.GetAttribute("innerText");
                Console.WriteLine(td + "td" + c);
                if (td.Equals(vlr1))
                {
                    tdElement.FindElement(By.XPath("//tr[" + c + "]/td[3]/form[1]/input[2]")).Submit();
                    break;
                }
            }
        }

        public void AltCategNome(string vlr1)
        {
            InptAltCat.Clear();
            InptAltCat.SendKeys(vlr1);
        }

        public void AltAtribCateg(string vlr1)
        {
            var rows = SelectCat.FindElements(By.TagName("option")).Count;
            for (int c = 1; c <= rows; c++)
            {
                IWebElement optionElement = SelectCat.FindElement(By.XPath("//option[" + c + "]"));
                string option = optionElement.GetAttribute("innerText");
                Console.WriteLine(option + "option" + c);
                if (option.Equals(vlr1))
                {
                    optionElement.Click();
                }
            }
        }

        //CATEGORIAS - DELETAR
        public void DeletarCateg(string vlr1)
        {
            String pagSource = _driver.PageSource;
            if (pagSource.Contains("Edit Project Category"))
            {
                BtnMenuCatDel.Submit();
                ConfirmCatDel.Submit();
            }
            else
            {
                var rows = TabCat.FindElements(By.TagName("tr")).Count;
                for (int c = 1; c <= rows; c++)
                {
                    IWebElement tdElement = TabCat.FindElement(By.XPath("//tr[" + c + "]/td[1]"));
                    string td = tdElement.GetAttribute("innerText");
                    Console.WriteLine(td + "td" + c);
                    if (td.Equals(vlr1))
                    {
                        tdElement.FindElement(By.XPath("//tr[" + c + "]/td[3]/form[2]/input[2]")).Submit();
                        ConfirmCatDel.Submit();
                        break;
                    }
                }
            }    
        }

        //SCREENSHOT DE RESULTADOS
        public void Screenshot(string imgName)
        {
            ITakesScreenshot camera = _driver as ITakesScreenshot;
            try
            {
                Screenshot ss = camera.GetScreenshot();
                ss.SaveAsFile(PathScrenShotGet.PathScrenShot()+ imgName + " Teste " + contador++ + ".jpg");       
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //SCREENSHOT DE ELEMENTOS
        public void ScreenshotTabCat(string imgName, string vlr1)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)_driver;
            wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/a/div/table/tbody")));
            var rows = TabCat.FindElements(By.TagName("tr")).Count;      
            for (int c = 1; c <= rows; c++)
            {         
                IWebElement tdElement = TabCat.FindElement(By.XPath("//tr[" + c + "]/td[1]"));
                string td = tdElement.GetAttribute("innerText");
                if (td.Equals(vlr1))
                {
                    je.ExecuteScript("arguments[0].scrollIntoView(true);", tdElement);
                    break;
                }
            }
            ITakesScreenshot camera = _driver as ITakesScreenshot;
            try
            {
                Screenshot ss = camera.GetScreenshot();
                ss.SaveAsFile(PathScrenShotGet.PathScrenShot() + imgName + " Teste " + contador++ + ".jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}