using NUnit.Framework;
using qa_dotnet_cucumber.Pages;
using qa_dotnet_cucumber.Utilities;
using qa_dotnet_cucumber.DataModels;
using System.Threading;

namespace qa_dotnet_cucumber.Tests
{
    [TestFixture]
    public class Education_Certifications_Tests : BaseTest
    {
        private EducationPage educationPage;
        private CertificationsPage certificationsPage;

        [SetUp]
        public void PageSetup()
        {
            educationPage = new EducationPage(driver);
            certificationsPage = new CertificationsPage(driver);
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl("http://10.211.55.2:5003/Account/Profile");
        }

        

        [Test, Order(1)]
        public void Add_Education_HappyPath()
        {
            EducationModel data = JsonHelper.ReadEducationData("Education_Happy");
            educationPage.DeleteEducation(data.University); 

            educationPage.AddEducation(data);
            Assert.That(educationPage.GetMessage(), Does.Contain("added"), "Education add failed");
        }

        [Test, Order(2)]
        public void Update_Education()
        {
            EducationModel oldData = JsonHelper.ReadEducationData("Education_Happy");
            EducationModel newData = JsonHelper.ReadEducationData("Education_Update");

           
            educationPage.DeleteEducation(newData.University);
            educationPage.DeleteEducation(oldData.University);
            educationPage.AddEducation(oldData);
            Thread.Sleep(1000);

            
            educationPage.UpdateEducation(oldData.University, newData);

            
            Assert.That(educationPage.GetMessage(), Does.Contain("updated"), "Update message not found");

           
            educationPage.DeleteEducation(newData.University);
        }

        [Test, Order(3)]
        public void Add_Education_Duplicate()
        {
            EducationModel data = JsonHelper.ReadEducationData("Education_Duplicate");
            educationPage.DeleteEducation(data.University);

            educationPage.AddEducation(data);  
            Thread.Sleep(5000); 

            educationPage.AddEducation(data); 
            Assert.That(educationPage.GetMessage(), Does.Contain("already exist"), "Duplicate check failed");

            educationPage.DeleteEducation(data.University); 
        }

        [Test, Order(4)]
        public void Add_Education_Negative_Empty()
        {
            EducationModel data = JsonHelper.ReadEducationData("Education_Empty");
            educationPage.AddEducation(data);
            Assert.That(educationPage.GetMessage(), Does.Contain("Please enter"), "Empty validation failed");
        }

        [Test, Order(5)]
        public void Add_Education_SpecialCharacters()
        {
            EducationModel data = JsonHelper.ReadEducationData("Education_Special");
            educationPage.DeleteEducation(data.University);

            educationPage.AddEducation(data);
            Assert.That(educationPage.GetMessage(), Does.Contain("added"), "Special char add failed");

            educationPage.DeleteEducation(data.University); 
        }

        [Test, Order(6)]
        public void Delete_Education_Destructive()
        {
            EducationModel data = JsonHelper.ReadEducationData("Education_Happy");

            
            educationPage.DeleteEducation(data.University);
            educationPage.AddEducation(data);

            driver.Navigate().Refresh();

            educationPage.DeleteEducation(data.University);

            Assert.That(educationPage.GetMessage(), Does.Contain("removed"), "Delete message failed");
        }



        [Test, Order(7)]
        public void Add_Certification_HappyPath()
        {
            CertificationModel data = JsonHelper.ReadCertificationData("Certification_Happy");
            certificationsPage.DeleteCertification(data.Certificate);

            certificationsPage.AddCertification(data);
            Assert.That(certificationsPage.GetMessage(), Does.Contain("added"));
        }

        [Test, Order(8)]
        public void Update_Certification()
        {
            CertificationModel oldData = JsonHelper.ReadCertificationData("Certification_Happy");
            CertificationModel newData = JsonHelper.ReadCertificationData("Certification_Update");

            certificationsPage.DeleteCertification(newData.Certificate);
            certificationsPage.DeleteCertification(oldData.Certificate);
            certificationsPage.AddCertification(oldData);
            Thread.Sleep(1000);

            certificationsPage.UpdateCertification(oldData.Certificate, newData);
            Assert.That(certificationsPage.GetMessage(), Does.Contain("updated"));

            certificationsPage.DeleteCertification(newData.Certificate);  
        }

        [Test, Order(9)]
        public void Add_Certification_Duplicate()
        {
            CertificationModel data = JsonHelper.ReadCertificationData("Certification_Duplicate");
            certificationsPage.DeleteCertification(data.Certificate);

            certificationsPage.AddCertification(data);
            Thread.Sleep(5000); 

            certificationsPage.AddCertification(data);
            Assert.That(certificationsPage.GetMessage(), Does.Contain("already exist"));

            certificationsPage.DeleteCertification(data.Certificate); 
        }

        [Test, Order(10)]
        public void Add_Certification_Negative_Empty()
        {
            CertificationModel data = JsonHelper.ReadCertificationData("Certification_Empty");
            certificationsPage.AddCertification(data);
            Assert.That(certificationsPage.GetMessage(), Does.Contain("Please enter"), "Empty validation failed");
        }

        [Test, Order(11)]
        public void Add_Certification_SpecialCharacters()
        {
            CertificationModel data = JsonHelper.ReadCertificationData("Certification_Special");
            certificationsPage.DeleteCertification(data.Certificate);

            certificationsPage.AddCertification(data);
            Assert.That(certificationsPage.GetMessage(), Does.Contain("added"), "Special char add failed");

            certificationsPage.DeleteCertification(data.Certificate); 
        }

        [Test, Order(12)]
        public void Delete_Certification_Destructive()
        {
            CertificationModel data = JsonHelper.ReadCertificationData("Certification_Happy");

           
            certificationsPage.DeleteCertification(data.Certificate);
            certificationsPage.AddCertification(data);

            
            driver.Navigate().Refresh();

           
            certificationsPage.DeleteCertification(data.Certificate);

            
            Assert.That(certificationsPage.GetMessage(), Does.Contain("deleted"));
        }
    }
}