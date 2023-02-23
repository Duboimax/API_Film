using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Film.Models.EntityFramework;
using NuGet.Versioning;
using API_Film.Models.DataManager;
using API_Film.Models.Repository;

namespace API_Film.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        
        private readonly IDataRepository<Utilisateur> datatRepository;

        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            datatRepository = dataRepo;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await datatRepository.GetAll();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            var utilisateur = await datatRepository.GetById(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }


        [HttpGet("{mail}")]
        [ActionName("GetByEmail")]
        public async Task<ActionResult<Utilisateur>>GetUtilisateurByEmail(string mail)
        {
            var utilisateur = await datatRepository.GetByStringAsync(mail);
            if(utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest();
            }

            var userToUpdate = datatRepository.GetById(id);
            if(userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                datatRepository.UpdateAsync(userToUpdate.Result.Value, utilisateur);
                return NoContent();
            }

        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ActionName("Post")]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            datatRepository.AddAsync(utilisateur);
            

            return CreatedAtAction("GetById", new { id = utilisateur.UtilisateurId }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = datatRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

           await datatRepository.DeleteAsync(utilisateur.Result.Value);

            return NoContent();
        }

        /*private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        }*/
    }
}
