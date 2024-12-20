﻿using LivroMente.API.Commands.BookCommands;
using LivroMente.API.Handlers.BookHandler;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Service.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LivroMente.Tests.Handler.BookHandlerTest
{
    public class DeleteBookHandlerTest
    {
        private readonly Mock<IBookService> _service;
        private readonly DeleteBookHandler _handler;

        public DeleteBookHandlerTest()
        {
            _service = new Mock<IBookService>();
            _handler = new DeleteBookHandler(_service.Object);
        }

        [Fact]
        public async void Handle_ReturnFalse_WhenBookIsnotDelete()
        {
            var request = new BookDeleteCommand(Guid.NewGuid().ToString());
            var book = new Book("Original Title", "Original Author", "Original Synopsis", 10, 200, "Original Publisher", "1234567890", 29.99, "English", 12, true, "123", "http://example.com/book", "http://example.com/img");

            _service.Setup(service => service.GetById(request.Id)).ReturnsAsync(book);
            _service.Setup(x => x.Delete(request.Id)).ReturnsAsync(false);
            var result = await _handler.Handle(request,CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async void Handle_ReturnTrue_WhenBookIsDelete()
        {
            var request = new BookDeleteCommand(Guid.NewGuid().ToString());
            var book = new Book("Original Title", "Original Author", "Original Synopsis", 10, 200, "Original Publisher", "1234567890", 29.99, "English", 12, true, "123", "http://example.com/book", "http://example.com/img");

            _service.Setup(service => service.GetById(request.Id)).ReturnsAsync(book);
            _service.Setup(x => x.Delete(book.Id)).ReturnsAsync(true);
            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result);

        }
    }
}
