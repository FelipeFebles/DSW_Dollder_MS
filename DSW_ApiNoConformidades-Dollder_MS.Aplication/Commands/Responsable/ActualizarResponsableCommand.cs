﻿using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Responsable;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable
{
    public class ActualizarResponsableCommand : IRequest<IdResponsableResponse>
    {
        public ResponsableRequest _request { get; set; }
        public ActualizarResponsableCommand(ResponsableRequest request)
        {
            _request = request;
        }
    }
}
