using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Film.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Film.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Film.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private FilmRatingContext _context;
        private UtilisateursController controller;

        public UtilisateursControllerTests() 
        {        }

        [TestInitialize()]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmRating;uid=postgres;password=postgres");
            _context = new FilmRatingContext(builder.Options);
            controller = new UtilisateursController(_context);
        }


        [TestMethod()]
        public void UtilisateursControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            var result = controller.GetUtilisateurs();
            
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual((System.Collections.ICollection)result.Result.Value, _context.Utilisateurs.ToList(), "Erreur les utilisateurs ne sont pas tous la");
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_testOK()
        {
            var result = controller.GetUtilisateurById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.Value, _context.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault(), "Test pas Ok, L'utilisateur 1 n'est pas la");
        }
        [TestMethod()]
        public void GetUtilisateurByIdTest_test_Pas_Ok()
        {
            var result = controller.GetUtilisateurById(30);

            
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Test pas ok pas de not found");
           /* Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");*/
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            string mail = "rrichings1@naver.com";
            var result = controller.GetUtilisateurByEmail(mail);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.Value, _context.Utilisateurs.Where(c => c.Mail == mail).FirstOrDefault(), "Test pas ok, aucun email n'est affilié a un client");
        }

        [TestMethod()]
        public void PutUtilisateurTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostUtilisateurTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            Assert.Fail();
        }
    }
}