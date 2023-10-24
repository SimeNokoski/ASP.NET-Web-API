using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs.TicketDTO;
using LotoApp.Mapper;
using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace LotoApp.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IWinningTicketRepository _winningTicketRepository;
        private readonly IUserRepository _userRepository;
        public TicketService(ITicketRepository ticketRepository, ISessionRepository sessionRepository,IWinningTicketRepository winningTicketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _sessionRepository = sessionRepository;
            _winningTicketRepository = winningTicketRepository;
            _userRepository = userRepository;
        }

        public void CreateTicket(CreateTicketDto createTicketDto, int userId)
        {
            if(createTicketDto.TicketNumbers.Count() != 7) {
                throw new TicketDataException("You must enter exactly 7 numbers");
            }
            foreach(var num in  createTicketDto.TicketNumbers)
            {
                if(num < 1 || num > 37)
                {
                    throw new TicketDataException("All numbers must be between 1 and 37");
                }
            }
            if (createTicketDto.TicketNumbers.Distinct().Count() != 7)
            {
                throw new TicketDataException("You have a duplicate number");
            }
            var user = _userRepository.GetById(userId);
            var ticket = createTicketDto.ToTicket();
            var activeSession = _sessionRepository.ActiveSession();
            if(activeSession == null)
            {
                throw new SessionNotFoundException("There is no currently active session");
            }

            ticket.SessionId = activeSession.Id;
            ticket.User = user;
            ticket.UserId = userId;
            ticket.Prizes = "?";
   
            _ticketRepository.Add(ticket);
        }

        public void DeleteTicket(int userId, int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if(ticket == null)
            {
                throw new TicketNotFoundException("invalid ticket id");
            }
            var user = _userRepository.GetById(userId);
            var session = _sessionRepository.ActiveSession();
            if(!session.Active)
            {
                throw new SessionNotFoundException("no active session");
            }
            if (ticket.SessionId != session.Id)
            {
                throw new SessionDataException("this ticket is in a closed session");
            }
            if (ticket.UserId != user.Id)
            {
                throw new TicketDataException("invalid ticket id");
            }
            _ticketRepository.Delete(ticket);
        }

        public List<TicketDto> GetAllMyTicket(int userId)
        {
            var user = _userRepository.GetById(userId);
            var tickets = _ticketRepository.GetAll().Where(x=>x.UserId == user.Id).ToList();
            if(tickets.Count() ==0)
            {
                throw new TicketNotFoundException("You have not created a ticket");
            }

            return tickets.Select(x=>x.ToTicketDto()).ToList();
        }

        public List<TicketDto> GetAllMyTicketBySesion(int sessionId, int userId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if(session == null)
            {
                throw new SessionDataException("invalid session");
            }
            var user = _userRepository.GetById(userId);
            var tickets = _ticketRepository.GetAll().Where(x => x.UserId == user.Id && x.SessionId == sessionId).ToList();
            if (tickets.Count() == 0)
            {
                throw new TicketNotFoundException("You have not created a ticket in that session ");
            }

            return tickets.Select(x=>x.ToTicketDto()).ToList() ;
        }

        public List<TicketDto> GetAllTicket()
        {
            var allTickets = _ticketRepository.GetAll();
            if(allTickets.Count() == 0)
            {
                throw new TicketNotFoundException("No tickets have been created yet");
            }
            return allTickets.Select(x=>x.ToTicketDto()).ToList();
        }

        public List<TicketDto> GetAllTicketBySesion(int sessionId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if(session == null)
            {
                throw new SessionDataException("Invalid session id");
            }
            var tickets =  _ticketRepository.GetAll().Where(x=>x.SessionId == session.Id).Select(x=>x.ToTicketDto()).ToList() ;
            if (tickets.Count() == 0)
            {
                throw new TicketDataException("No tickets have been created yet in this session");
            }
            return tickets;
        }

        public TicketDto GetTicketById(int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if (ticket == null)
            {
                throw new TicketNotFoundException($"Ticket with id {id} does not exist");
            }
            return ticket.ToTicketDto();
        }

        public string TicketCheck(int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if(ticket == null)
            {
                throw new TicketNotFoundException("invalid ticket ID");
            }

            if(ticket.Prizes == "Nothing")
            {
                return "The ticket is not received";
            }
            if(ticket.Prizes == "?")
            {
                return "The session is not completed";
            }
            return $"Prize: {ticket.Prizes}";
        }

        public void UpdateTicket(int userId, UpdateTicketDto updateTicketDto)
        {
            if(updateTicketDto.TicketNumbers.Count() != 7)
            {
                throw new TicketDataException("You must enter exactly 7 numbers");
            }
            foreach (var num in updateTicketDto.TicketNumbers)
            {
                if(num < 1 || num > 37)
                {
                    throw new TicketDataException("All numbers must be between 1 and 37");
                }
            }
            if (updateTicketDto.TicketNumbers.Distinct().Count() != 7)
            {
                throw new TicketDataException("You have a duplicate number");
            }
            var user = _userRepository.GetById(userId);
            var session = _sessionRepository.ActiveSession();
            if(session == null)
            {
                throw new SessionNotFoundException("no active session");
            }
            var ticket = _ticketRepository.GetById(updateTicketDto.Id);
            if(ticket == null)
            {
                throw new TicketNotFoundException("invalid ticket id");
            }
            ticket.Number1 = updateTicketDto.TicketNumbers[0];
            ticket.Number2 = updateTicketDto.TicketNumbers[1];
            ticket.Number3 = updateTicketDto.TicketNumbers[2];
            ticket.Number4 = updateTicketDto.TicketNumbers[3];
            ticket.Number5 = updateTicketDto.TicketNumbers[4];
            ticket.Number6 = updateTicketDto.TicketNumbers[5];
            ticket.Number7 = updateTicketDto.TicketNumbers[6];
            ticket.Session = session;

            if(ticket.SessionId != session.Id)
            {
                throw new SessionDataException("this ticket is in a closed session");
            }
            if (ticket.UserId != user.Id)
            {
                throw new SessionDataException("invalid ticket id");
            }
            _ticketRepository.Update(ticket);

        }

        public void WinningTickets(int sessionId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if(session == null)
            {
                throw new SessionDataException("no active session");
            }
          
            var allTicket = _ticketRepository.GetAll().Where(x => x.SessionId == session.Id);

            allTicket.ForEach(ticket =>
            {
                WinningTicketsTemp(ticket,sessionId);
            });
        }

        private void WinningTicketsTemp(Ticket ticket,int sessionId)
        {
            var user = _userRepository.GetById(ticket.UserId);
            var session = _sessionRepository.GetById(sessionId);
            var winningTicket = _winningTicketRepository.GetById(session.WinningTicketId).ToWinningTicketDto();
            ticket.Session = session;
            ticket.User = user;
       
            List<int> ticketNumbers = new List<int>()
            {
                ticket.Number1,
                ticket.Number2,
                ticket.Number3,
                ticket.Number4,
                ticket.Number5,
                ticket.Number6,
                ticket.Number7,
            };

            int counter = 0;
            int bonusCounter = 0;

            ticketNumbers.ForEach(number =>
            {
                if (winningTicket.Number8Bonus == number)
                {
                    bonusCounter++;
                }
                if (winningTicket.WinnerTicketNumbers.Contains(number))
                {
                    counter++;
                }
               
            });

            switch (counter)
            {
                case 7:
                    ticket.Prizes = "7 ( JackPot ) - Car";
                    break;

                case 6:
                    ticket.Prizes = "6 - Vacation";
                    if(bonusCounter != 0)
                    {
                        ticket.Prizes = "6 + 1 - Vacation + 500$";

                    }
                    break;

                case 5:
                    ticket.Prizes = "5 - TV";
                    if (bonusCounter != 0)
                    {
                        ticket.Prizes = "5 + 1 - TV + 300$";

                    }
                    break;

                case 4:
                    ticket.Prizes = "4 - 100$ Gift Card";
                    if (bonusCounter != 0)
                    {
                        ticket.Prizes = "4 + 1 - 200$ Gift Card";

                    }
                    break;

                case 3:
                    ticket.Prizes = "3 - 50$ Gift Card";
                    if (bonusCounter != 0)
                    {
                        ticket.Prizes = "3 + 1 - 100$ Gift Card";
                    }
                    break;

                default:
                    ticket.Prizes = "Nothing";
                    break;
            }

            _ticketRepository.Update(ticket);
        }

    }
}
