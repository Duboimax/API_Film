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
using API_Film.Models.Repository;
using API_Film.Models.DataManager;
using Moq;

namespace API_Film.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private FilmRatingContext _context;
        private UtilisateursController controller;

        private IDataRepository<Utilisateur> dataRepository;

        public UtilisateursControllerTests()
        { }

        [TestInitialize()]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmRating;uid=postgres;password=postgres");
            _context = new FilmRatingContext(builder.Options);
            dataRepository = new UtilisateurManager(_context);
            controller = new UtilisateursController(dataRepository);
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
            var result = controller.GetUtilisateurById(250).Result;
            Console.WriteLine("test");

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        public void GetUtilisateurByIdTest_testOK_avec_Moq()
        {
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
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
        public void GetUtilisateurByEmailTest_Return_OK_With_Moq()
        {
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByStringAsync(user.Mail).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail(user.Mail).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_Return_404_With_Moq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("a").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 200);

            Utilisateur userOld = new Utilisateur()
            {
                Nom = "Bidule",
                Prenom = "Jean",
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

            var userAdd = controller.PostUtilisateur(userOld).Result;
            Utilisateur? userSearch = _context.Utilisateurs.Where(c => c.Mail == userOld.Mail).FirstOrDefault();

            userOld.Nom = "Blanchar";
            var result = controller.PutUtilisateur(userSearch.UtilisateurId, userOld).Result;

            Assert.AreEqual("Blanchar", userOld.Nom, "Pas Identiques donc pas ok");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "Pas 204");

        }

        [TestMethod()]
        public void PututilisateurTest_With_Moq()
        {
            Utilisateur userOld = new Utilisateur
            {
                UtilisateurId = 30,
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            Utilisateur userNew = new Utilisateur
            {
                UtilisateurId=30,
                Nom = "POISSON",
                Prenom = "Pascale",
                Mobile = "200",
                Mail = "poisson22@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "33 route d'annecy",
                CodePostal = "74600",
                Ville = "Montagny-les-lanches",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(30).Result).Returns(userNew);
            var userController = new UtilisateursController(mockRepository.Object);

            var actionPost = userController.PostUtilisateur(userOld).Result;
            var actionPut = userController.PutUtilisateur(userOld.UtilisateurId, userNew).Result;
            var actionResult = userController.GetUtilisateurById(userOld.UtilisateurId);

            Assert.IsInstanceOfType(actionPost, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionPost.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            Assert.AreEqual(((NoContentResult)actionPut).StatusCode, StatusCodes.Status204NoContent, "Pas 204");
            Assert.AreEqual(userNew, actionResult.Result.Value as Utilisateur);
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 500);

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "Poulain",
                Prenom = "Florian",
                Mobile = "0707070707",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto9934!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            _context.Utilisateurs.Add(userAtester);
            _context.SaveChanges();

            Utilisateur? userAdd = _context.Utilisateurs.Where(c => c.Mail == userAtester.Mail).FirstOrDefault();

            var result = controller.DeleteUtilisateur(userAdd.UtilisateurId).Result;

            Utilisateur? userDelete = _context.Utilisateurs.Where(c => c.Mobile == userAtester.Mobile).FirstOrDefault();

            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "Pas 204");
            Assert.AreEqual(null, userDelete, "Test pas ok, utilisateur pas supprimer");


        }

        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);

            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}