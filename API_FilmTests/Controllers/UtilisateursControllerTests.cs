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

namespace API_Film.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private FilmRatingContext _context;
        private UtilisateursController controller;

        public UtilisateursControllerTests() 
        {
        }

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
            
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            Assert.Fail();
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