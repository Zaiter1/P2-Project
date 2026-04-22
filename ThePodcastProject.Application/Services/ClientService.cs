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
    public class ClientService
    {
        
        private readonly UnitOfWork unitOfWork;

        public ClientService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            var entities = await unitOfWork.Clients.GetAllClientsAsync();

            return entities.Select(e => new ClientDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber= e.PhoneNumber,
                Reservations = e.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                }).ToList()

            }).ToList();      
        }




        public async Task<ClientDto?> GetClientByIdAsync(int id)
        {
            var entity = await unitOfWork.Clients.GetClientByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            return new ClientDto
            {
                 Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                PhoneNumber= entity.PhoneNumber,
                Reservations = entity.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                }).ToList()
            };
        }


        public async Task AddClientAsync(ClientDto dto)
        {
            var entity = new Client
            {
               
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
     
            };
           
            await unitOfWork.Clients.AddClientAsync(entity);
            await unitOfWork.CompleteAsync();
        }



        public async Task UpdateClientAsync(ClientDto dto)
        {
            var entity= await unitOfWork.Clients.GetClientByIdAsync(dto.Id);
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;

            await unitOfWork.Clients.UpdateClientAsync(entity);
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
           await unitOfWork.Clients.DeleteClientAsync(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
