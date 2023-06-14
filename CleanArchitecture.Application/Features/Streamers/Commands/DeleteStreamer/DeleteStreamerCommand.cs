using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteStreamerCommand(int id)
        {
            Id = id;
        }
    }
}
