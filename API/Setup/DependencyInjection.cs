﻿using Application.Catalogo.Services;
using Application.Pedidos.Commands;
using Domain.Autenticacao;
using Domain.Catalogo;
using Domain.Pedidos;
using Infra.Autenticacao;
using Infra.Autenticacao.Repository;
using Infra.Catalogo;
using Infra.Catalogo.Repository;
using Infra.Pedidos.Repository;
using Infra.Pedidos;
using MediatR;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Application.Pedidos.Queries;
using Application.Pedidos.Events;
using Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos;
using Application.Pagamentos.AntiCorrupition;
using Domain.Pagamentos.Interfaces;
using Domain.Pagamentos;
using Infra.Pagamentos.Repository;
using Infra.Pagamentos;
using Domain.Pagamentos.Events;
using Domain.Catalogo.Events;
using Application.Autenticacao.UseCases;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Handlers;
using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Queries;
using Application.Pedidos.Handlers;
using Application.Pedidos.Boundaries;
using Application.Pedidos.UseCases;

namespace API.Setup
{
    public static class DependencyInjection
    { 
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Domain Notifications 
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Autenticacao
            services.AddTransient<IRequestHandler<AdminAutenticaCommand, LogInUsuarioOutput>, AdminAutenticacaoCommandHandler>();
            services.AddTransient<IRequestHandler<AutenticaClienteCommand, AutenticaClienteOutput>, AutenticaClienteCommandHandler>();
            services.AddTransient<IAutenticacaoRepository, AutenticacaoRepository>();
            services.AddTransient<IRequestHandler<CadastraClienteCommand,AutenticaClienteOutput>,  CadastraClienteCommandHandler>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<IAutenticacaoQuery, AutenticacaoQuery>();
            services.AddScoped<AutenticacaoContext>();

            // Catalogo
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();

            // Pedidos
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<IStatusPedidoUseCase, StatusPedidoUseCase>();
            services.AddScoped<PedidosContext>();

            services.AddScoped<IRequestHandler<AtualizarStatusPedidoCommand, PedidoOutput>, AtualizarStatusPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();

            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoPagamentoRealizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoPagamentoRecusadoEvent>, PedidoEventHandler>();

            // Pagamento
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, Application.Pagamentos.AntiCorrupition.ConfigurationManager>();
            services.AddScoped<PagamentoContext>();

            services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();

        }
    }
}
