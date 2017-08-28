using Microsoft.VisualStudio.TestTools.UnitTesting;
using PavigymDeveloperTest.Model;
using PavigymDeveloperTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavigymDeveloperTest.ViewModel.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        [TestMethod()]
        public void ProcessServiceResponseTestStatusOK()
        {
            MainViewModel model = new MainViewModel();

            ServiceResponse response = new ServiceResponse()
            {
                Status = "OK"
            };

            model.ProcessServiceResponse(response);

            Assert.IsNull(model.ErrorMsg);
        }

        [TestMethod()]
        public void ProcessServiceResponseTestStatusERROR()
        {
            MainViewModel model = new MainViewModel();

            ServiceResponse response = new ServiceResponse()
            {
                Status = "ERROR",
                Msg = "Error message test"
            };

            model.ProcessServiceResponse(response);

            Assert.IsTrue(model.ErrorMsg == response.Msg);
        }
    }
}