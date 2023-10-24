using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs.SessionDTO;
using LotoApp.DTOs.TicketDTO;
using LotoApp.Mapper;
using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace LotoApp.Service.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IWinningTicketRepository _winningTicketRepository;
        private readonly ITicketRepository _ticketRepository;
        public SessionService(ISessionRepository sessionRepository, IWinningTicketRepository winningTicketRepository, ITicketRepository ticketRepository)
        {
            _sessionRepository = sessionRepository;
            _winningTicketRepository = winningTicketRepository;
            _ticketRepository = ticketRepository;
        }

        public SessionDto ActiveSession()
        {
            var session = _sessionRepository.ActiveSession();
            if (session == null)
            {
                throw new SessionNotFoundException("does not active session");
            }
            return session.ToSessinoDto();
        }

        public List<AllWinningTicketDto> AllWinningTickets()
        {
            var allticket = _ticketRepository.GetAll().Select(x=>x.ToAllWinningTicketDto()).ToList();
            var allWinningTicket = allticket.Where(x => x.Prizes != "Nothing" && x.Prizes != "?").ToList();
            if(allWinningTicket.Count() ==0) 
            {
                throw new SessionNotFoundException("They have no winning tickets");
            }
            return allWinningTicket;

        }

        public List<AllWinningTicketDto> AllWinningTicketsBySession(int sessionId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if (session == null)
            {
                throw new SessionNotFoundException($"Session with id {sessionId} does not exist");
            }
            var allticket = _ticketRepository.GetAll().Select(x => x.ToAllWinningTicketDto()).ToList();
            var allWinningTicket = allticket.Where(x => x.Prizes != "Nothing" && x.Prizes != "?" && x.SessionId == sessionId).ToList();
            if (allWinningTicket.Count() == 0)
            {
                throw new SessionNotFoundException("They have no winning tickets");
            }
            return allWinningTicket;
        }

        public void CreateSession()
        {
            var sessionA = _sessionRepository.ActiveSession();
            if(sessionA != null)
            {
                throw new SessionDataException("There is currently an active session");
            }

            WinningTicket winningTicket = new WinningTicket();
            _winningTicketRepository.Add(winningTicket);

                var session = new Session();
                session.Start = DateTime.Now;
                session.End = null;
                session.Active = true;
                session.WinningTicketId = winningTicket.Id;
                _sessionRepository.Add(session); 
        }

        public void EndSession(List<int> nums)
        {
            var session = _sessionRepository.ActiveSession();
            if( session == null )
            {
                throw new SessionDataException("No active session");
            }
            var wt = _winningTicketRepository.GetById(session.WinningTicketId);

            wt.Number1 = nums[0];
            wt.Number2 = nums[1];
            wt.Number3 = nums[2];
            wt.Number4 = nums[3];
            wt.Number5 = nums[4];
            wt.Number6 = nums[5];
            wt.Number7 = nums[6];
            wt.Number8 = nums[7];

            _winningTicketRepository.Update(wt);
            
            session.End = DateTime.Now;
            session.Active = false;

            _sessionRepository.Update(session);
        }

        public List<SessionDataDto> GetAllSessions()
        {
            var allSession = _sessionRepository.GetAll();
            if(allSession.Count() == 0 )
            {
                throw new SessionNotFoundException("There are no sessions");
            }
            return allSession.Select(x => x.ToSessionDataDto()).ToList();
        }

        public SessionDataDto GetSessionById(int id)
        {
            var session = _sessionRepository.GetById(id);
            if(session == null)
            {
                throw new SessionNotFoundException($"Sessio with id {id} does not exist");
            }
            return session.ToSessionDataDto();
        }

    }
}
