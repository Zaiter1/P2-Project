using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThePodcastProject.Application.Dtos;
using ThePodcastProject.Domain.Entities;
using ThePodcastProject.Infraestructure.Repositories;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Application.Services
{
    public class CabinService
    {
      
        private readonly UnitOfWork unitOfWork;

        public CabinService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<CabinDto>> GetAllCabinsAsync()
        {
            var cabins = await unitOfWork.Cabins.GetAllCabinsAsync();

            return cabins.Select(c => new CabinDto
            {
                Id = c.Id,
                Name = c.Name,
                Capacity = c.Capacity,
                Description = c.Description,
                Reservations = c.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    CabinId = r.CabinId
                }).ToList()
            }).ToList();
        }

        public async Task<CabinDto?> GetCabinByIdAsync(int id)
        {
            var cabin = await unitOfWork.Cabins.GetCabinByIdAsync(id);
            if (cabin == null) return null;

            return new CabinDto
            {
                Id = cabin.Id,
                Name = cabin.Name,
                Capacity = cabin.Capacity,
                Description = cabin.Description,
                Reservations = cabin.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    CabinId = r.CabinId
                }).ToList()
            };
        }


        public async Task AddCabinAsync(CabinDto cabin)
        {
            var entity = new Cabin
            {
                Name = cabin.Name,
                Capacity = cabin.Capacity,
                Description = cabin.Description,
             
            };

           
            await unitOfWork.Cabins.AddCabinAsync(entity);
            await unitOfWork.CompleteAsync();
        }

        public async Task UpdateCabinAsync(CabinDto cabin)
        {
            var entity= await unitOfWork.Cabins.GetCabinByIdAsync(cabin.Id);
            entity.Name = cabin.Name;
            entity.Capacity = cabin.Capacity;
            entity.Description = cabin.Description;

            await unitOfWork.Cabins.UpdateCabinAsync(entity);
            await unitOfWork.CompleteAsync();

        }

        public async Task DeleteCabinAsync(int id)
        {
           await unitOfWork.Cabins.DeleteCabinAsync(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
