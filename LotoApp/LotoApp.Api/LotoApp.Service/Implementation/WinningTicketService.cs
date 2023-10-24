using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs.WinningTicketDTO;
using LotoApp.Mapper;
using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Service.Implementation
{
    public class WinningTicketService : IWinningTicketService
    {
        private readonly IWinningTicketRepository _winningTicketRepository;
        private readonly ISessionRepository _sessionRepository;
        public WinningTicketService(IWinningTicketRepository winningTicketRepository, ISessionRepository sessionRepository)
        {
            _winningTicketRepository = winningTicketRepository;
            _sessionRepository = sessionRepository;
        }

        public List<int> GenerateWinningTicket()
        {
            Random random = new Random();
            List<int> nums = new List<int>();
            for(int i=1;i<=8;i++)
            {
                int randomNum = random.Next(1,38);
                if (!(nums.Contains(randomNum)))
                {
                    nums.Add(randomNum);
                }
                else
                {
                    i--;
                }
            }
            return nums;
        }

        public List<WinningTicketDto> GetAllWinningTicket()
        {
           var allWt = _winningTicketRepository.GetAll().Select(x=>x.ToWinningTicketDto()).ToList(); 
            if(allWt.Count() ==0)
            {
                throw new WinningTicketNotFoundException("there are no winning tickets");
            }
            return allWt;
        }

        public WinningTicketDto GetWinningTicketById(int id)
        {
            var wt = _winningTicketRepository.GetById(id);
            if(wt == null)
            {
                throw new WinningTicketNotFoundException($"ticket wint id {id} does not exist");

            }
            return wt.ToWinningTicketDto();
        }

        public WinningTicketDto GetWinningTicketBySession(int sessionId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if(session == null)
            {
                throw new SessionNotFoundException("session not found");
            }
            var wt = _winningTicketRepository.GetById(session.WinningTicketId);
            if( wt == null)
            {
                throw new WinningTicketNotFoundException("err");
            }
            return wt.ToWinningTicketDto();
        }

    }
}
