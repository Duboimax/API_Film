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
        { }

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
            var result = controller.GetUtilisateurById(100).Result;

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_Return_OK()
        {
            string mail = "rrichings1@naver.com";
            var result = controller.GetUtilisateurByEmail(mail);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.Value, _context.Utilisateurs.Where(c => c.Mail == mail).FirstOrDefault(), "Test pas ok, aucun email n'est affilié a un client");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_Return_404()
        {
            string mail = "bigpack";
            var result = controller.GetUtilisateurByEmail(mail).Result;
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void PutUtilisateurTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
                                                                         // Assert
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            Assert.Fail();
        }
    }
}