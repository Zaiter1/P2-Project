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
    public class EquipmentService
    {
        
        private readonly UnitOfWork unitOfWork;

        public EquipmentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        public async Task<List<EquipmentDto>> GetAllEquipmentsAsync()
        {
            var entities = await unitOfWork.Equipments.GetAllEquipmentsAsync();

            return entities.Select(e => new EquipmentDto
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                Description = e.Description,
                Reservations = e.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                }).ToList()
            }).ToList();      
        }




        public async Task<EquipmentDto?> GetEquipmentByIdAsync(int id)
        {
            var entity = await unitOfWork.Equipments.GetEquipmentByIdAsync(id);
            if (entity == null)
            {

                return null;
            }

            return new EquipmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Reservations = entity.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                }).ToList()
            };
        }


        public async Task AddEquipmentAsync(EquipmentDto dto)
        {
            var entity = new Equipment
            {
               Name= dto.Name,
                Type = dto.Type,  // ← agregar esta línea

                Description = dto.Description,
                
            };
           
            await unitOfWork.Equipments.AddEquipmentAsync(entity);
            await unitOfWork.CompleteAsync();
        }

        public async Task UpdateEquipmentAsync(EquipmentDto dto)
        {
            var entity= await unitOfWork.Equipments.GetEquipmentByIdAsync(dto.Id);
            entity.Name = dto.Name;
            entity.Type = dto.Type;
            entity.Description = dto.Description;

            await unitOfWork.Equipments.UpdateEquipmentAsync(entity);
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteEquipmentAsync(int id)
        {
           await unitOfWork.Equipments.DeleteEquipmentAsync(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
