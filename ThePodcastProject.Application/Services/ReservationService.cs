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
    public class ReservationService
    {
        
        private readonly UnitOfWork unitOfWork;

        public ReservationService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        public async Task<List<ReservationDto>> GetAllReservationsAsync()
        {

            //var entities = await unitOfWork.Reservations.GetAllReservationsAsync();

            //return entities.Select(r => new ReservationDto
            //{
            //    Id = r.Id,
            //    StartDate = r.StartDate,
            //    EndDate = r.EndDate,
            //    CabinId = r.CabinId
            //}).ToList();

            var entities = await unitOfWork.Reservations.GetAllReservationsAsync();

            return entities.Select(r => new ReservationDto
            {
                Id = r.Id,
                ClientId = r.ClientId,
                Client = r.Client == null ? null : new ClientDto
                {
                    Id = r.Client.Id,
                    Name = r.Client.Name,
                    Email = r.Client.Email,
                    PhoneNumber = r.Client.PhoneNumber
                },
                CabinId = r.CabinId,
                Cabin = r.Cabin == null ? null : new CabinDto
                {
                    Id = r.Cabin.Id,
                    Name = r.Cabin.Name,
                    Capacity = r.Cabin.Capacity,
                    Description = r.Cabin.Description
                },
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                ReservationState = r.ReservationState,
                Equipments = r.Equipments.Select(e => new EquipmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    Description = e.Description
                  }).ToList()
            }).ToList();

        }

        public async Task<ReservationDto?> GetReservationByIdAsync(int id)
        {
            var entity = await unitOfWork.Reservations.GetReservationByIdAsync(id);

            if (entity == null)
            {

                return null;
            }

            return new ReservationDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CabinId = entity.CabinId

            };
        }


        public async Task AddReservationAsync(ReservationDto dto)
        {
            //var entity = new Reservation
            //{
            //    //Id = dto.Id,
            //    //StartDate = dto.StartDate,
            //    //EndDate = dto.EndDate,
            //    //CabinId = dto.CabinId

            ////    ClientId = dto.ClientId,
            ////    CabinId = dto.CabinId,
            ////    StartDate = dto.StartDate,
            ////    EndDate = dto.EndDate,
            ////    ReservationState = dto.ReservationState,
            ////    Equipments = dto.Equipments
            ////.Select(e => new Equipment { Id = e.Id })
            ////.ToList()


            //};

            //    var equipments = dto.Equipments
            //.Select(e => new Equipment { Id = e.Id })
            //.ToList();

            //    // Attach para que EF no intente insertar equipos nuevos
            //    foreach (var eq in equipments)
            //        unitOfWork.Context.Attach(eq);

            //    var entity = new Reservation
            //    {
            //        ClientId = dto.ClientId,
            //        CabinId = dto.CabinId,
            //        StartDate = dto.StartDate,
            //        EndDate = dto.EndDate,
            //        ReservationState = dto.ReservationState,
            //        Equipments = equipments
            //    };

            var entity = new Reservation
            {
                ClientId = dto.ClientId,
                CabinId = dto.CabinId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ReservationState = dto.ReservationState,
            };
            //lo nuevo 

            if (dto.Equipments != null && dto.Equipments.Any())
            {
                var equipmentIds = dto.Equipments.Select(e => e.Id).ToList();
                var existingEquipments = await unitOfWork.Equipments.GetAllEquipmentsAsync();
                entity.Equipments = existingEquipments
                    .Where(e => equipmentIds.Contains(e.Id))
                    .ToList();
            }

            //
            var result = await unitOfWork.Reservations.AddReservationAsync(entity);

            if (result != "Reserva creada correctamente")
                throw new Exception(result);

            await unitOfWork.CompleteAsync();

            //await unitOfWork.Reservations.AddReservationAsync(entity);
            //await unitOfWork.CompleteAsync();
        }

        public async Task UpdateReservationAsync(ReservationDto dto)
        {
            var entity= await unitOfWork.Reservations.GetReservationByIdAsync(dto.Id);

            //entity.StartDate = dto.StartDate;
            //entity.EndDate = dto.EndDate;
            //entity.CabinId = dto.CabinId;

            entity.ClientId = dto.ClientId;
            entity.CabinId = dto.CabinId;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.ReservationState = dto.ReservationState;

            if (dto.Equipments != null && dto.Equipments.Any())
            {
                var equipmentIds = dto.Equipments.Select(e => e.Id).ToList();
                var existingEquipments = await unitOfWork.Equipments.GetAllEquipmentsAsync();
                entity.Equipments = existingEquipments
                    .Where(e => equipmentIds.Contains(e.Id))
                    .ToList();
            }
            else
            {
                entity.Equipments = new List<Equipment>();
            }

            await unitOfWork.Reservations.UpdateReservationAsync(entity);
            await unitOfWork.CompleteAsync();

        }

        public async Task DeleteReservationAsync(int id)
        {
           await unitOfWork.Reservations.DeleteReservationAsync(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
