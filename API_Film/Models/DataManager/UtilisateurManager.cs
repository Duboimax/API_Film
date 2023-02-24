﻿using API_Film.Models.EntityFramework;
using API_Film.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Film.Models.DataManager
{
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        readonly FilmRatingContext? filmsDbContext;

        public UtilisateurManager() { }

        public UtilisateurManager(FilmRatingContext context)
        {
            filmsDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAll()
        {
            return await filmsDbContext.Utilisateurs.ToListAsync();
        }
        public async Task<ActionResult<Utilisateur>> GetById(int id)
        {
            return await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
        }
        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }
        public async Task AddAsync(Utilisateur entity)
        {
            await filmsDbContext.Utilisateurs.AddAsync(entity);
            await filmsDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
        {
            filmsDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.UtilisateurId = entity.UtilisateurId;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.Rue = entity.Rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotationUtilisateur = entity.NotationUtilisateur;
            await filmsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Utilisateur entity)
        {
            filmsDbContext.Utilisateurs.Remove(entity);
            await filmsDbContext.SaveChangesAsync();
        }
    }
}